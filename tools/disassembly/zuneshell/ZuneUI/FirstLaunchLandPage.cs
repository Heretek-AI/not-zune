using System;
using System.IO;
using Microsoft.Iris;
using Microsoft.Zune.Configuration;
using Microsoft.Zune.Shell;
using Microsoft.Zune.Util;
using ZuneXml;

namespace ZuneUI;

public class FirstLaunchLandPage : SetupLandPage
{
	private Timer _deviceArrivalTimer;

	private int _deviceArrivalTimerInterval = 10000;

	private bool _phoneDisconnected;

	private static string _commonAppDataPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), Shell.LoadString(StringId.IDS_APPDATAFOLDERNAME).TrimStart(new char[1] { '\\' }));

	private static string _customFirstLaunchMovieUri = ClientConfiguration.FUE.FirstLaunchVideo;

	private bool _playbackFailed;

	private FirstLaunchWizardType _wizardType = FirstLaunchWizardType.Undefined;

	public FirstLaunchWizardType WizardType
	{
		get
		{
			return _wizardType;
		}
		set
		{
			if (_wizardType != value)
			{
				_wizardType = value;
				((ModelItem)this).FirePropertyChanged("WizardType");
			}
		}
	}

	public bool PhoneDisconnected
	{
		get
		{
			return _phoneDisconnected;
		}
		set
		{
			if (_phoneDisconnected != value)
			{
				_phoneDisconnected = value;
				((ModelItem)this).FirePropertyChanged("PhoneDisconnected");
			}
		}
	}

	public bool PlaybackFailed
	{
		get
		{
			return _playbackFailed;
		}
		set
		{
			if (_playbackFailed != value)
			{
				_playbackFailed = value;
				((ModelItem)this).FirePropertyChanged("PlaybackFailed");
			}
		}
	}

	public FirstLaunchLandPage()
	{
		base.UI = "res://ZuneShellResources!SetupLand.uix#FirstLaunch";
		if (Fue.Instance.AutoFUE)
		{
			base.BackgroundUI = "res://ZuneShellResources!SetupLand.uix#SetupLandBackground";
		}
		else if (FeatureEnablement.IsFeatureEnabled((Features)21))
		{
			base.BackgroundUI = "res://ZuneShellResources!SetupLand.uix#FirstLaunchBackground";
		}
		else
		{
			base.BackgroundUI = "res://ZuneShellResources!SetupLand.uix#FirstLaunchNoVideoBackground";
		}
	}

	protected override void SetConnectionRule()
	{
		SingletonModelItem<UIDeviceList>.Instance.AllowDeviceConnections = true;
	}

	protected override void OnDispose(bool disposing)
	{
		SingletonModelItem<UIDeviceList>.Instance.DeviceDisconnectedEvent -= OnDeviceDisconnected;
		base.OnDispose(disposing);
	}

	public void WaitForPhoneArrival()
	{
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Expected O, but got Unknown
		UIDevice uIDevice = FindConnectedPhone();
		if (uIDevice == UIDeviceList.NullDevice)
		{
			if (_deviceArrivalTimer == null)
			{
				_deviceArrivalTimer = new Timer();
				_deviceArrivalTimer.Enabled = true;
				_deviceArrivalTimer.Tick += OnDeviceConnectedTimeout;
				_deviceArrivalTimer.Interval = _deviceArrivalTimerInterval;
			}
			SingletonModelItem<UIDeviceList>.Instance.DeviceConnectedEvent += OnDeviceConnected;
		}
		else
		{
			ProcessPhoneForWizard(uIDevice);
		}
	}

	private UIDevice FindConnectedPhone()
	{
		UIDevice result = UIDeviceList.NullDevice;
		foreach (UIDevice item in SingletonModelItem<UIDeviceList>.Instance)
		{
			if (item.IsConnectedToClientPhysically && item.Class == DeviceClass.WindowsPhone)
			{
				result = item;
				break;
			}
		}
		return result;
	}

	private void OnDeviceConnected(object sender, DeviceListEventArgs args)
	{
		UIDevice device = args.Device;
		if (device.Class == DeviceClass.WindowsPhone)
		{
			ProcessPhoneForWizard(args.Device);
		}
	}

	private void OnDeviceDisconnected(object sender, DeviceListEventArgs args)
	{
		if (SyncControls.Instance.CurrentDeviceOverride == args.Device)
		{
			PhoneDisconnected = true;
		}
	}

	private void OnDeviceConnectedTimeout(object sender, EventArgs args)
	{
		ProcessPhoneForWizard(UIDeviceList.NullDevice);
	}

	private void ProcessPhoneForWizard(UIDevice device)
	{
		if (_deviceArrivalTimer != null)
		{
			_deviceArrivalTimer.Stop();
		}
		SingletonModelItem<UIDeviceList>.Instance.DeviceConnectedEvent -= OnDeviceConnected;
		SingletonModelItem<UIDeviceList>.Instance.AllowDeviceConnections = false;
		if (device.Class == DeviceClass.WindowsPhone && device.Relationship == DeviceRelationship.None && !device.RequiresAutoRestore)
		{
			if (device.SupportsOOBECompleted && !device.OOBECompleted)
			{
				SingletonModelItem<UIDeviceList>.Instance.HideDevice(device);
				DeviceManagement.ShowDeviceOOBEIncompleteDialog();
				WizardType = FirstLaunchWizardType.Standard;
			}
			else
			{
				SingletonModelItem<UIDeviceList>.Instance.DeviceDisconnectedEvent += OnDeviceDisconnected;
				WizardType = FirstLaunchWizardType.PhoneFirstConnect;
			}
		}
		else
		{
			WizardType = FirstLaunchWizardType.Standard;
		}
	}

	public void InvokePlayback()
	{
		string text = string.Empty;
		if (!string.IsNullOrEmpty(_customFirstLaunchMovieUri))
		{
			text = _customFirstLaunchMovieUri;
		}
		else
		{
			PlaybackFailed = true;
		}
		if (!string.IsNullOrEmpty(text))
		{
			Uri uri = new Uri(text);
			PlaybackTrack playbackTrack = null;
			if (uri.IsLoopback)
			{
				if (VideoExists(text))
				{
					int num = ZuneApplication.ZuneLibrary.AddMedia(text);
					if (num != -1)
					{
						playbackTrack = new LibraryPlaybackTrack(num, MediaType.Video, null);
					}
				}
				else
				{
					PlaybackFailed = true;
				}
			}
			else
			{
				playbackTrack = new VideoPlaybackTrack(Guid.Empty, "", null, text, isDownloading: false, isStreaming: true, ignoreCollection: false, fallbackToPreview: false, forcePreview: false, VideoDefinitionEnum.HD);
			}
			if (playbackTrack != null)
			{
				SingletonModelItem<TransportControls>.Instance.PlayItem(playbackTrack, PlayNavigationOptions.None);
			}
		}
		else
		{
			PlaybackFailed = true;
		}
	}

	private bool VideoExists(string uri)
	{
		if (!string.IsNullOrEmpty(uri))
		{
			return File.Exists(uri);
		}
		return false;
	}
}
