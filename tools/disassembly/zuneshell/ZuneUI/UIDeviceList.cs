using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading;
using Microsoft.Iris;
using Microsoft.Zune.Configuration;
using MicrosoftZuneLibrary;

namespace ZuneUI;

public class UIDeviceList : SingletonModelItem<UIDeviceList>, IEnumerable<UIDevice>, IEnumerable
{
	private static UIDevice _nullDevice;

	private DeviceList _deviceList;

	private Dictionary<Device, UIDevice> _deviceToUiMap;

	private Dictionary<UIDevice, Device> _uiToDeviceMap;

	private bool _allowUnreadyDevices = true;

	private bool _allowDeviceConnections = true;

	private List<UIDevice> _deferredConnectedDevices;

	private List<UIDevice> _deferredUnreadyDevices;

	private bool _isDisposed;

	private object _initializationLock;

	public static UIDevice NullDevice
	{
		get
		{
			if (_nullDevice == null)
			{
				_nullDevice = new UIDevice((IModelItemOwner)(object)SingletonModelItem<UIDeviceList>.Instance, null);
			}
			return _nullDevice;
		}
	}

	public bool AllowUnreadyDevices
	{
		get
		{
			return _allowUnreadyDevices;
		}
		set
		{
			//IL_0059: Unknown result type (might be due to invalid IL or missing references)
			//IL_005f: Expected O, but got Unknown
			DeferredInvokeHandler val = null;
			if (_allowUnreadyDevices == value)
			{
				return;
			}
			_allowUnreadyDevices = value;
			((ModelItem)this).FirePropertyChanged("AllowUnreadyDevices");
			if (!_allowUnreadyDevices)
			{
				return;
			}
			if (_deferredUnreadyDevices.Count > 0)
			{
				_deferredConnectedDevices.AddRange(_deferredUnreadyDevices);
				_deferredUnreadyDevices.Clear();
			}
			if (val == null)
			{
				val = (DeferredInvokeHandler)delegate
				{
					HandleDeferredConnectedDevices();
				};
			}
			Application.DeferredInvoke(val, (DeferredInvokePriority)1);
		}
	}

	public bool AllowDeviceConnections
	{
		get
		{
			return _allowDeviceConnections;
		}
		set
		{
			//IL_002f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0035: Expected O, but got Unknown
			DeferredInvokeHandler val = null;
			if (_allowDeviceConnections == value)
			{
				return;
			}
			_allowDeviceConnections = value;
			((ModelItem)this).FirePropertyChanged("AllowDeviceConnections");
			if (!_allowDeviceConnections)
			{
				return;
			}
			if (val == null)
			{
				val = (DeferredInvokeHandler)delegate
				{
					HandleDeferredConnectedDevices();
				};
			}
			Application.DeferredInvoke(val, (DeferredInvokePriority)1);
		}
	}

	public string TranscodedFilesCachePath
	{
		get
		{
			string empty = string.Empty;
			if (IsListReady)
			{
				_deviceList.GetTranscodedFilesCachePath(ref empty);
			}
			return empty;
		}
		set
		{
			if (IsListReady)
			{
				_deviceList.SetTranscodedFilesCachePath(value);
				((ModelItem)this).FirePropertyChanged("TranscodeCachePath");
			}
		}
	}

	public int TranscodedFilesCacheSize
	{
		get
		{
			int result = 0;
			if (IsListReady)
			{
				result = ClientConfiguration.Transcode.TranscodedFilesCacheSize;
			}
			return result;
		}
		set
		{
			if (IsListReady && TranscodedFilesCacheSize != value)
			{
				_deviceList.SetTranscodedFilesCacheSize(value);
				((ModelItem)this).FirePropertyChanged("TranscodedFilesCacheSize");
			}
		}
	}

	private bool IsListReady
	{
		get
		{
			if (_deviceList != null)
			{
				return _deviceList.Initialized;
			}
			return false;
		}
	}

	internal event DeviceListEventHandler DeviceAddedEvent;

	internal event DeviceListEventHandler DeviceRemovedEvent;

	public event DeviceListEventHandler DeviceConnectedEvent;

	public event DeviceListEventHandler DeviceDisconnectedEvent;

	public UIDeviceList()
	{
		_deviceToUiMap = new Dictionary<Device, UIDevice>();
		_uiToDeviceMap = new Dictionary<UIDevice, Device>();
		_deferredConnectedDevices = new List<UIDevice>();
		_deferredUnreadyDevices = new List<UIDevice>();
		_initializationLock = new object();
	}

	public void Phase2Init()
	{
		ThreadPool.QueueUserWorkItem(delegate
		{
			//IL_0035: Unknown result type (might be due to invalid IL or missing references)
			//IL_003f: Expected O, but got Unknown
			//IL_0057: Unknown result type (might be due to invalid IL or missing references)
			lock (_initializationLock)
			{
				if (!_isDisposed)
				{
					_deviceList = DeviceList.Instance;
					if (_deviceList != null)
					{
						_deviceList.Added += new DeviceAddedHandler(OnDeviceAdded);
						if (!_deviceList.Initialized)
						{
							HRESULT.op_Implicit(_deviceList.InitializeAndEnumerate());
						}
					}
				}
			}
		}, null);
	}

	protected override void OnDispose(bool disposing)
	{
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_0033: Expected O, but got Unknown
		if (_deviceList != null)
		{
			lock (_initializationLock)
			{
				_isDisposed = true;
				_deviceList.Added -= new DeviceAddedHandler(OnDeviceAdded);
			}
			foreach (UIDevice value in _deviceToUiMap.Values)
			{
				((ModelItem)value).PropertyChanged -= OnUIDevicePropertyChanged;
			}
		}
		((ModelItem)this).OnDispose(disposing);
	}

	public void HideDevice(UIDevice device)
	{
		if (IsListReady)
		{
			Device val = _uiToDeviceMap[device];
			_deviceList.HideDevice(val);
		}
	}

	public HRESULT DeleteDevice(UIDevice device)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0005: Unknown result type (might be due to invalid IL or missing references)
		//IL_0064: Unknown result type (might be due to invalid IL or missing references)
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		HRESULT result = HRESULT._E_UNEXPECTED;
		if (IsListReady)
		{
			result = HRESULT.op_Implicit(_uiToDeviceMap[device].ClearCache());
			if (this.DeviceDisconnectedEvent != null)
			{
				this.DeviceDisconnectedEvent(this, new DeviceListEventArgs(device));
				((ModelItem)this).FirePropertyChanged("DeviceDisconnectedEvent");
			}
			if (this.DeviceRemovedEvent != null)
			{
				this.DeviceRemovedEvent(this, new DeviceListEventArgs(device));
			}
		}
		return result;
	}

	public HRESULT ClearTranscodeCache()
	{
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_0008: Unknown result type (might be due to invalid IL or missing references)
		if (!IsListReady)
		{
			return HRESULT._E_UNEXPECTED;
		}
		return HRESULT.op_Implicit(_deviceList.ClearTranscodeCache());
	}

	public IEnumerator<UIDevice> GetEnumerator()
	{
		if (!IsListReady)
		{
			yield break;
		}
		for (int i = 0; i < _deviceList.Count; i++)
		{
			UIDevice device = GetUIDevice(_deviceList.GetItem(i));
			if (device.IsConnectedToPC || !device.IsGuest)
			{
				yield return device;
			}
		}
	}

	IEnumerator IEnumerable.GetEnumerator()
	{
		return GetEnumerator();
	}

	private void OnDeviceAdded(Device rawDevice)
	{
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Expected O, but got Unknown
		Application.DeferredInvoke((DeferredInvokeHandler)delegate
		{
			if (!((ModelItem)this).IsDisposed)
			{
				GetUIDevice(rawDevice);
			}
		}, (object)null);
	}

	private void OnUIDevicePropertyChanged(object sender, PropertyChangedEventArgs args)
	{
		if (!(sender is UIDevice uIDevice))
		{
			return;
		}
		if (args.PropertyName == "IsValid")
		{
			if (uIDevice.IsValid)
			{
				OnDeviceValid(uIDevice);
			}
		}
		else if (args.PropertyName == "IsConnectedToClient")
		{
			if (uIDevice.IsConnectedToClient)
			{
				OnDeviceConnected(uIDevice);
			}
			else
			{
				OnDeviceDisconnected(uIDevice);
			}
		}
	}

	private void OnDeviceValid(UIDevice device)
	{
		if (!device.IsGuest && this.DeviceAddedEvent != null)
		{
			this.DeviceAddedEvent(this, new DeviceListEventArgs(device));
		}
		if (device.SupportsSyncApplications)
		{
			ClientConfiguration.Shell.ShowApplicationPivot = true;
			Shell.MainFrame.Collection.UpdateApplicationPivot();
		}
	}

	private void OnDeviceConnected(UIDevice device)
	{
		if (!AllowDeviceConnections)
		{
			if (!_deferredConnectedDevices.Contains(device))
			{
				_deferredConnectedDevices.Add(device);
			}
			return;
		}
		if (!AllowUnreadyDevices && !IsSuitableForConnection(device))
		{
			if (!_deferredUnreadyDevices.Contains(device))
			{
				_deferredUnreadyDevices.Add(device);
			}
			return;
		}
		if (device.IsGuest && this.DeviceAddedEvent != null)
		{
			this.DeviceAddedEvent(this, new DeviceListEventArgs(device));
		}
		if (this.DeviceConnectedEvent != null)
		{
			this.DeviceConnectedEvent(this, new DeviceListEventArgs(device));
			((ModelItem)this).FirePropertyChanged("DeviceConnectedEvent");
		}
	}

	private void OnDeviceDisconnected(UIDevice device)
	{
		if (_deferredConnectedDevices.Contains(device))
		{
			_deferredConnectedDevices.Remove(device);
		}
		if (_deferredUnreadyDevices.Contains(device))
		{
			_deferredUnreadyDevices.Remove(device);
		}
		if (this.DeviceDisconnectedEvent != null)
		{
			this.DeviceDisconnectedEvent(this, new DeviceListEventArgs(device));
			((ModelItem)this).FirePropertyChanged("DeviceDisconnectedEvent");
		}
		if (device.IsGuest && this.DeviceRemovedEvent != null)
		{
			this.DeviceRemovedEvent(this, new DeviceListEventArgs(device));
		}
	}

	private UIDevice GetUIDevice(Device device)
	{
		if (!_deviceToUiMap.ContainsKey(device))
		{
			UIDevice uIDevice = new UIDevice((IModelItemOwner)(object)this, device);
			_deviceToUiMap.Add(device, uIDevice);
			_uiToDeviceMap.Add(uIDevice, device);
			((ModelItem)uIDevice).PropertyChanged += OnUIDevicePropertyChanged;
		}
		return _deviceToUiMap[device];
	}

	private void HandleDeferredConnectedDevices()
	{
		if (AllowDeviceConnections)
		{
			for (int i = 0; i < _deferredConnectedDevices.Count; i++)
			{
				OnDeviceConnected(_deferredConnectedDevices[i]);
			}
			_deferredConnectedDevices.Clear();
		}
	}

	public static bool IsSuitableForConnection(UIDevice device)
	{
		bool requiresFirmwareUpdate = device.RequiresFirmwareUpdate;
		bool flag = device.Relationship == DeviceRelationship.None;
		if (!requiresFirmwareUpdate)
		{
			return !flag;
		}
		return false;
	}

	public static SyncCategory MapMediaTypeToSyncCategory(MediaType mediaType)
	{
		switch (mediaType)
		{
		case MediaType.Track:
		case MediaType.Playlist:
		case MediaType.Album:
		case MediaType.Genre:
		case MediaType.PlaylistContentItem:
		case MediaType.Artist:
			return SyncCategory.Music;
		case MediaType.Video:
			return SyncCategory.Video;
		case MediaType.Photo:
		case MediaType.MediaFolder:
			return SyncCategory.Photo;
		case MediaType.PodcastEpisode:
		case MediaType.Podcast:
			return SyncCategory.Podcast;
		case MediaType.UserCard:
			return SyncCategory.Friend;
		case MediaType.Application:
			return SyncCategory.Application;
		default:
			return SyncCategory.Undefined;
		}
	}

	public static MediaType MapSyncCategoryToMediaType(SyncCategory mediaType)
	{
		return mediaType switch
		{
			SyncCategory.Music => MediaType.Track, 
			SyncCategory.Video => MediaType.Video, 
			SyncCategory.Photo => MediaType.Photo, 
			SyncCategory.Podcast => MediaType.PodcastEpisode, 
			SyncCategory.Friend => MediaType.UserCard, 
			SyncCategory.Application => MediaType.Application, 
			_ => MediaType.Undefined, 
		};
	}
}
