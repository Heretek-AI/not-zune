using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using Microsoft.Iris;
using Microsoft.Zune.Configuration;
using Microsoft.Zune.Shell;
using Microsoft.Zune.Util;
using UIXControls;

namespace ZuneUI;

public class SyncControls : ModelItem
{
	private UIDeviceList _deviceList;

	private UIDevice _currentDevice;

	private UIDevice _deferredArrivalDevice;

	private string _canonicalNameToMakeActive;

	private bool _changeIntoSetupDevice;

	private Command _deviceSignInFailureEvent;

	private Queue<UIDevice> _failedSignInQueue;

	private DownloadTaskList _downloadTasks;

	private bool _haveSeenDownloadsOngoing;

	private MessageBox _currentUnlinkedDeviceDialog;

	private bool _showSyncInstructionsToast;

	private bool _displayWirelessSyncBanner;

	private static string _sizeInKB = Shell.LoadString(StringId.IDS_GAS_GAUGE_SIZE_IN_KB);

	private static string _sizeInMB = Shell.LoadString(StringId.IDS_GAS_GAUGE_SIZE_IN_MB);

	private static string _sizeInGB = Shell.LoadString(StringId.IDS_GAS_GAUGE_SIZE_IN_GB);

	private static long _devicelandKilobyte = 1024L;

	private static long _devicelandMegabyte = DevicelandKilobyte * DevicelandKilobyte;

	private static long _devicelandGigabyte = DevicelandMegabyte * DevicelandKilobyte;

	private static SyncControls s_singletonInstance;

	private static string _unlinkedDeviceNagDialogTitle = Shell.LoadString(StringId.IDS_DIALOG_TITLE_LINK_TO_SYNC_FRIENDS);

	private static string _unlinkedDeviceNagDialogMessageBase = Shell.LoadString(StringId.IDS_DIALOG_TEXT_LINK_TO_SYNC_FRIENDS);

	public bool ShowPhoneWelcomeMessage
	{
		get
		{
			return ClientConfiguration.FUE.ShowPhoneFUEDeviceLandOptions;
		}
		set
		{
			if (ClientConfiguration.FUE.ShowPhoneFUEDeviceLandOptions != value)
			{
				ClientConfiguration.FUE.ShowPhoneFUEDeviceLandOptions = value;
				((ModelItem)this).FirePropertyChanged("ShowPhoneWelcomeMessage");
			}
		}
	}

	public bool ShowSyncInstructionsToast
	{
		get
		{
			return _showSyncInstructionsToast;
		}
		set
		{
			if (_showSyncInstructionsToast != value)
			{
				if (CurrentDevice.Class == DeviceClass.Classic || CurrentDevice.Class == DeviceClass.ZuneHD)
				{
					bool flag = value && ClientConfiguration.Devices.ShowSyncInstructionsToast;
					value = flag;
					ClientConfiguration.Devices.ShowSyncInstructionsToast = flag;
				}
				_showSyncInstructionsToast = value;
				((ModelItem)this).FirePropertyChanged("ShowSyncInstructionsToast");
			}
		}
	}

	public bool DisplayWirelessSyncBanner
	{
		get
		{
			if (_displayWirelessSyncBanner)
			{
				return !CurrentDevice.IsWirelessSyncEnabled;
			}
			return false;
		}
		set
		{
			if (_displayWirelessSyncBanner != value)
			{
				_displayWirelessSyncBanner = value;
				((ModelItem)this).FirePropertyChanged("DisplayWirelessSyncBanner");
			}
		}
	}

	public UIDevice CurrentDevice
	{
		get
		{
			return _currentDevice ?? UIDeviceList.NullDevice;
		}
		private set
		{
			if (_currentDevice != value)
			{
				UIDevice uIDevice = value;
				if (uIDevice == null)
				{
					uIDevice = UIDeviceList.NullDevice;
				}
				_currentDevice = uIDevice;
				PhoneBrandingStringMap.Instance.BrandingEnabled = _currentDevice != null && _currentDevice.SupportsBrandingType(DeviceBranding.WindowsPhone);
				KinBrandingStringMap.Instance.BrandingEnabled = _currentDevice != null && _currentDevice.SupportsBrandingType(DeviceBranding.Kin);
				((ModelItem)this).FirePropertyChanged("CurrentDevice");
			}
		}
	}

	public UIDevice CurrentDeviceOverride
	{
		get
		{
			UIDevice uIDevice = DeviceManagement.SetupDevice;
			if (uIDevice == null)
			{
				uIDevice = CurrentDevice;
			}
			return uIDevice;
		}
	}

	public bool ChangeIntoSetupDevice
	{
		get
		{
			return _changeIntoSetupDevice;
		}
		set
		{
			if (_changeIntoSetupDevice != value)
			{
				_changeIntoSetupDevice = value;
				((ModelItem)this).FirePropertyChanged("ChangeIntoSetupDevice");
			}
		}
	}

	public Command DeviceSignInFailureEvent
	{
		get
		{
			//IL_000a: Unknown result type (might be due to invalid IL or missing references)
			//IL_0014: Expected O, but got Unknown
			if (_deviceSignInFailureEvent == null)
			{
				_deviceSignInFailureEvent = new Command((IModelItemOwner)(object)this);
			}
			return _deviceSignInFailureEvent;
		}
	}

	public Queue<UIDevice> FailedSignInDevices
	{
		get
		{
			if (_failedSignInQueue == null)
			{
				_failedSignInQueue = new Queue<UIDevice>();
			}
			return _failedSignInQueue;
		}
	}

	public UIDevice CurrentFailedSignInDevice
	{
		get
		{
			try
			{
				return FailedSignInDevices.Peek();
			}
			catch (InvalidOperationException)
			{
				return UIDeviceList.NullDevice;
			}
		}
	}

	public static long DevicelandKilobyte => _devicelandKilobyte;

	public static long DevicelandMegabyte => _devicelandMegabyte;

	public static long DevicelandGigabyte => _devicelandGigabyte;

	public static SyncControls Instance
	{
		get
		{
			if (s_singletonInstance == null)
			{
				s_singletonInstance = new SyncControls((IModelItemOwner)(object)ZuneShell.DefaultInstance);
			}
			return s_singletonInstance;
		}
	}

	private SyncControls(IModelItemOwner owner)
		: base(owner)
	{
		_deviceList = SingletonModelItem<UIDeviceList>.Instance;
		_deviceList.DeviceAddedEvent += OnDeviceAdded;
		_deviceList.DeviceRemovedEvent += OnDeviceRemoved;
		_deviceList.DeviceConnectedEvent += OnDeviceConnected;
		_deviceList.DeviceDisconnectedEvent += OnDeviceDisconnected;
		_currentDevice = UIDeviceList.NullDevice;
		((ModelItem)SignIn.Instance).PropertyChanged += SignInPropertyChanged;
		AccountCreationWizard.CreationCompleted += AccountCreationFinished;
		DeviceManagement.DeviceConnectionHandled += OnDeviceConnectionHandled;
		UpdateWirelessSyncBannerDisplay();
	}

	protected override void OnDispose(bool fDisposing)
	{
		if (fDisposing)
		{
			if (_deviceList != null)
			{
				_deviceList.DeviceAddedEvent -= OnDeviceAdded;
				_deviceList.DeviceRemovedEvent -= OnDeviceRemoved;
				_deviceList.DeviceConnectedEvent -= OnDeviceConnected;
				_deviceList.DeviceDisconnectedEvent -= OnDeviceDisconnected;
				_deviceList = null;
			}
			if (_downloadTasks != null)
			{
				((ModelItem)_downloadTasks.ActiveDownloads).PropertyChanged -= OnDownloadPropertyChanged;
				_downloadTasks = null;
			}
			((ModelItem)SignIn.Instance).PropertyChanged -= SignInPropertyChanged;
			AccountCreationWizard.CreationCompleted -= AccountCreationFinished;
			DeviceManagement.DeviceConnectionHandled -= OnDeviceConnectionHandled;
		}
		((ModelItem)this).OnDispose(fDisposing);
	}

	public void SetCurrentDevice(UIDevice device)
	{
		if (device == null)
		{
			device = UIDeviceList.NullDevice;
		}
		if (_canonicalNameToMakeActive != null && device.CanonicalName != null && device.CanonicalName.StartsWith(_canonicalNameToMakeActive))
		{
			_canonicalNameToMakeActive = null;
			if ((!(ZuneShell.DefaultInstance.CurrentPage is QuickplayPage) || !ClientConfiguration.FUE.ShowArtistChooser) && (ZuneShell.DefaultInstance.Management.CurrentCategoryPage == null || !ZuneShell.DefaultInstance.Management.CurrentCategoryPage.IsWizard))
			{
				ZuneShell.DefaultInstance.NavigateToPage(new Deviceland());
			}
		}
		if (device != CurrentDevice)
		{
			Management management = null;
			CategoryPage categoryPage = null;
			if (ZuneShell.DefaultInstance != null)
			{
				management = ZuneShell.DefaultInstance.Management;
			}
			if (management != null)
			{
				categoryPage = management.CurrentCategoryPage;
			}
			categoryPage?.RestartSyncIfNecessary();
			CurrentDevice = device;
			ClientConfiguration.Devices.CurrentDeviceID = CurrentDevice.ID;
			categoryPage?.PauseSyncIfNecessary();
			management?.DisposeDeviceManagement(deviceManagementLocked: false);
		}
	}

	public void Phase3Init()
	{
		_downloadTasks = DownloadTaskList.Instance;
		((ModelItem)_downloadTasks.ActiveDownloads).PropertyChanged += OnDownloadPropertyChanged;
		_haveSeenDownloadsOngoing = ((ListDataSet)_downloadTasks.ActiveDownloads).Count > 0;
	}

	public void SetCurrentDeviceByCanonicalName(string canonicalName)
	{
		_canonicalNameToMakeActive = canonicalName.ToLower();
		if (_deviceList == null)
		{
			return;
		}
		foreach (UIDevice device in _deviceList)
		{
			if (device.IsConnectedToClient && device.CanonicalName.StartsWith(_canonicalNameToMakeActive))
			{
				SetCurrentDevice(device);
				break;
			}
		}
	}

	public void SetCurrentDeviceIfNecessary(UIDevice device, bool comingOutOfFirstConnect)
	{
		if (device.IsConnectedToClientPhysically)
		{
			SetCurrentDevice(device);
			if (!comingOutOfFirstConnect)
			{
				ShowUnlinkedDeviceNagDialogIfNecessary();
			}
		}
	}

	public UIDevice FindNewActiveDevice()
	{
		return FindNewActiveDevice(null);
	}

	public UIDevice FindNewActiveDevice(UIDevice excludedDevice)
	{
		foreach (UIDevice device in _deviceList)
		{
			if (device != excludedDevice)
			{
				return device;
			}
		}
		return null;
	}

	public void DeleteCurrentDevice()
	{
		if (CurrentDevice.IsValid)
		{
			MessageBox.Show(Shell.LoadString(StringId.IDS_DELETE_DIALOG_TITLE), Shell.LoadString(StringId.IDS_DELETE_DIALOG_TEXT), (EventHandler)ConfirmedDeleteDevice);
		}
	}

	private void ConfirmedDeleteDevice(object sender, EventArgs e)
	{
		if (CurrentDevice.IsGuest || (!CurrentDevice.SupportsWirelessSetupMethod1 && !CurrentDevice.SupportsWirelessSetupMethod2))
		{
			DeleteCurrentDeviceWorker();
		}
		else
		{
			WirelessSync.Instance.ClearWirelessOnDeviceForForget();
		}
	}

	public void DeleteCurrentDeviceWorker()
	{
		//IL_005b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0060: Unknown result type (might be due to invalid IL or missing references)
		if (CurrentDevice != null)
		{
			string text = (CurrentDevice.SupportsBrandingType(DeviceBranding.WindowsPhone) ? Shell.LoadString(StringId.IDS_PHONE_FORGET_DEVICE_DIALOG_CONTENT) : Shell.LoadString(StringId.IDS_FORGET_DEVICE_DIALOG_CONTENT));
			ZuneShell.DefaultInstance.Management.CommitList.RemoveByIntValue(CurrentDevice.ID);
			HRESULT val = _deviceList.DeleteDevice(CurrentDevice);
			if (((HRESULT)(ref val)).IsError)
			{
				Shell.ShowErrorDialog(((HRESULT)(ref val)).Int, StringId.IDS_DELETE_DEVICE_FAILED);
			}
			else
			{
				MessageBox.Show(Shell.LoadString(StringId.IDS_FORGET_DEVICE_DIALOG_TITLE), text, (EventHandler)null);
			}
		}
	}

	public void PromptForAccountLinkage()
	{
		ShowUnlinkedDeviceNagDialog(allowUserToOptOut: false);
	}

	public void HideWirelessSyncBanner()
	{
		ClientConfiguration.Devices.ConnectionsUntilWirelessSyncBannerDisplay = -1;
		UpdateWirelessSyncBannerDisplay();
	}

	public void AddDeviceToFailedSignInQueue(UIDevice device)
	{
		device.IsLockedAgainstSyncing = true;
		FailedSignInDevices.Enqueue(device);
		if (FailedSignInDevices.Count == 1)
		{
			ShowFailedSignInMessageBox();
		}
	}

	public void HandleFailedSignInDevice(string username, string password)
	{
		CurrentFailedSignInDevice.SendMarketplaceCredentials(username, password);
		IgnoreFailedSignInDevice();
	}

	public void IgnoreFailedSignInDevice()
	{
		try
		{
			UIDevice uIDevice = FailedSignInDevices.Dequeue();
			uIDevice.IsLockedAgainstSyncing = false;
			uIDevice.BeginSync();
		}
		catch (InvalidOperationException)
		{
		}
		if (FailedSignInDevices.Count > 0)
		{
			ShowFailedSignInMessageBox();
		}
	}

	public void HandleFailedSignInDeviceGuidMismatch()
	{
		//IL_008b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a3: Expected O, but got Unknown
		string text = (string.IsNullOrEmpty(CurrentFailedSignInDevice.ZuneTag) ? string.Format(Shell.LoadString(StringId.IDS_DEVICE_CREDS_GUID_MISMATCH_TEXT_UNKNONW_ZUNETAG), CurrentFailedSignInDevice.Name, SignIn.Instance.ZuneTag) : string.Format(Shell.LoadString(StringId.IDS_DEVICE_CREDS_GUID_MISMATCH_TEXT), CurrentFailedSignInDevice.Name, CurrentFailedSignInDevice.ZuneTag, SignIn.Instance.ZuneTag));
		MessageBox.Show(Shell.LoadString(StringId.IDS_DEVICE_CREDS_GUID_MISMATCH_TITLE), text, new Command((IModelItemOwner)(object)this, Shell.LoadString(StringId.IDS_ENTER_CREDENTIALS), (EventHandler)delegate
		{
			DeviceSignInFailureEvent.Invoke();
		}), (string)null, (EventHandler)delegate
		{
			IgnoreFailedSignInDevice();
		}, true);
	}

	private void ShowFailedSignInMessageBox()
	{
		//IL_0077: Unknown result type (might be due to invalid IL or missing references)
		//IL_008f: Expected O, but got Unknown
		string text = (string.IsNullOrEmpty(CurrentFailedSignInDevice.ZuneTag) ? string.Format(Shell.LoadString(StringId.IDS_DEVICE_SIGN_IN_FAILURE_TEXT_UNKNONW_ZUNETAG), CurrentFailedSignInDevice.Name) : string.Format(Shell.LoadString(StringId.IDS_DEVICE_SIGN_IN_FAILURE_TEXT), CurrentFailedSignInDevice.Name, CurrentFailedSignInDevice.ZuneTag));
		MessageBox.Show(Shell.LoadString(StringId.IDS_DEVICE_SIGN_IN_FAILURE_TITLE), text, new Command((IModelItemOwner)(object)this, Shell.LoadString(StringId.IDS_ENTER_CREDENTIALS), (EventHandler)delegate
		{
			DeviceSignInFailureEvent.Invoke();
		}), (string)null, (EventHandler)delegate
		{
			IgnoreFailedSignInDevice();
		}, true);
	}

	private void OnFUECompleted(object sender, EventArgs args)
	{
		Fue.FUECompleted -= OnFUECompleted;
		if (_deferredArrivalDevice != null)
		{
			OnDeviceConnected(null, new DeviceListEventArgs(_deferredArrivalDevice));
		}
		_deferredArrivalDevice = null;
	}

	private void OnDeviceConnected(object sender, DeviceListEventArgs args)
	{
		if (args.Device.SupportsBrandingType(DeviceBranding.WindowsPhone) && ZuneApplication.IsDesktopLocked && !args.Device.IsConnectedToClientWirelessly)
		{
			SingletonModelItem<UIDeviceList>.Instance.HideDevice(args.Device);
			return;
		}
		if (Fue.Instance.IsFirstLaunch)
		{
			if (ZuneApplication.InstallContext == SetupInstallContext.Zune || !args.Device.SupportsBrandingType(DeviceBranding.WindowsPhone) || args.Device.RequiresAutoRestore)
			{
				_deferredArrivalDevice = args.Device;
				Fue.FUECompleted += OnFUECompleted;
			}
			return;
		}
		DeviceManagement.SetupQueue[args.Device.ID] = args.Device;
		if (ClientConfiguration.Devices.ConnectionsUntilWirelessSyncBannerDisplay > 0)
		{
			DevicesConfiguration devices = ClientConfiguration.Devices;
			devices.ConnectionsUntilWirelessSyncBannerDisplay -= 1;
		}
		UpdateWirelessSyncBannerDisplay();
	}

	private void OnDeviceDisconnected(object sender, DeviceListEventArgs args)
	{
		if (_deferredArrivalDevice == args.Device)
		{
			_deferredArrivalDevice = null;
			Fue.FUECompleted -= OnFUECompleted;
			return;
		}
		if (args.Device == CurrentDevice)
		{
			ZuneShell.DefaultInstance.Management.DisposeDeviceManagement(deviceManagementLocked: false);
			HideUnlinkedDeviceNagDialog();
			foreach (UIDevice device in _deviceList)
			{
				if (device != args.Device && device.IsConnectedToClientPhysically)
				{
					SetCurrentDevice(device);
					break;
				}
			}
		}
		if (DeviceManagement.SetupDevice == args.Device)
		{
			ZuneShell.DefaultInstance.Management.AlertedDeviceCategory = null;
		}
	}

	private void OnDeviceAdded(object sender, DeviceListEventArgs args)
	{
		if (args.Device.ID == ClientConfiguration.Devices.CurrentDeviceID || ClientConfiguration.Devices.CurrentDeviceID == 0 || (!CurrentDevice.IsConnectedToClient && args.Device.IsConnectedToClient))
		{
			SetCurrentDevice(args.Device);
		}
	}

	private void OnDeviceRemoved(object sender, DeviceListEventArgs args)
	{
		if (args.Device == CurrentDevice)
		{
			SetCurrentDevice(FindNewActiveDevice(CurrentDevice));
		}
	}

	private void OnDeviceConnectionHandled(object sender, DeviceConnectionHandledEventArgs args)
	{
		if (args.IsFirstConnect)
		{
			ClientConfiguration.Devices.ConnectionsUntilWirelessSyncBannerDisplay = 1;
			UpdateWirelessSyncBannerDisplay();
		}
	}

	private void UpdateWirelessSyncBannerDisplay()
	{
		DisplayWirelessSyncBanner = ClientConfiguration.Devices.ConnectionsUntilWirelessSyncBannerDisplay == 0;
	}

	private void OnDownloadPropertyChanged(object sender, PropertyChangedEventArgs args)
	{
		if (!(args.PropertyName == "Count"))
		{
			return;
		}
		if (((ListDataSet)_downloadTasks.ActiveDownloads).Count > 0)
		{
			_haveSeenDownloadsOngoing = true;
		}
		else
		{
			if (!_haveSeenDownloadsOngoing)
			{
				return;
			}
			foreach (UIDevice device in _deviceList)
			{
				if (device.IsConnectedToClient)
				{
					device.BeginSync(userInitiated: true, syncOnNextNotify: true);
				}
			}
			_haveSeenDownloadsOngoing = false;
		}
	}

	private void SignInPropertyChanged(object sender, PropertyChangedEventArgs args)
	{
		if (args.PropertyName == "SignedIn" && SignIn.Instance.SignedIn)
		{
			ShowUnlinkedDeviceNagDialogIfNecessary();
		}
	}

	private void AccountCreationFinished(object sender, EventArgs args)
	{
		ShowUnlinkedDeviceNagDialogIfNecessary();
	}

	private void ShowUnlinkedDeviceNagDialogIfNecessary()
	{
		if (CurrentDevice.PromptForAccountLinkage && !CurrentDevice.IsGuest && CurrentDevice.UserId == 0 && CurrentDevice.IsConnectedToClientPhysically && CurrentDevice.SupportsZuneTagLinking && SignIn.Instance.SignedIn && !AccountCreationWizard.AccountCreationInProgress && !(ZuneShell.DefaultInstance.CurrentPage is DialogPage))
		{
			ShowUnlinkedDeviceNagDialog(allowUserToOptOut: true);
		}
	}

	private void ShowUnlinkedDeviceNagDialog(bool allowUserToOptOut)
	{
		//IL_0059: Unknown result type (might be due to invalid IL or missing references)
		//IL_005f: Expected O, but got Unknown
		//IL_0070: Unknown result type (might be due to invalid IL or missing references)
		//IL_007a: Expected O, but got Unknown
		if (!SettingsExperience.ShouldShowDeviceMarketplaceCategory)
		{
			return;
		}
		HideUnlinkedDeviceNagDialog();
		SQMLog.Log((SQMDataId)213, 1);
		UIDevice device = CurrentDevice;
		BooleanChoice optOutChoice = null;
		Command val = new Command((IModelItemOwner)(object)this, DialogHelper.DialogYes, (EventHandler)delegate
		{
			SQMLog.Log((SQMDataId)215, 1);
			Shell.SettingsFrame.Settings.Device.Invoke(SettingCategories.DeviceMarketplace);
		});
		EventHandler eventHandler = delegate
		{
			SQMLog.Log((SQMDataId)214, 1);
			if (optOutChoice != null && !optOutChoice.Value)
			{
				SQMLog.Log((SQMDataId)216, 1);
			}
		};
		if (allowUserToOptOut)
		{
			optOutChoice = new BooleanChoice();
			optOutChoice.Value = !device.PromptForAccountLinkage;
			((Choice)optOutChoice).ChosenChanged += delegate
			{
				device.PromptForAccountLinkage = !optOutChoice.Value;
			};
			((ModelItem)optOutChoice).Description = Shell.LoadString(StringId.IDS_DONT_SHOW_THIS_MESSAGE_AGAIN);
		}
		_currentUnlinkedDeviceDialog = MessageBox.Show(_unlinkedDeviceNagDialogTitle, string.Format(_unlinkedDeviceNagDialogMessageBase, device.Name), DialogHelper.DialogNo, true, val, (Command)null, (Command)null, eventHandler, optOutChoice);
	}

	private void HideUnlinkedDeviceNagDialog()
	{
		if (_currentUnlinkedDeviceDialog != null && !((DialogHelper)_currentUnlinkedDeviceDialog).WouldLikeToBeHidden)
		{
			((DialogHelper)_currentUnlinkedDeviceDialog).Hide();
		}
	}

	public static int ConvertSyncOperationToInt(ESyncOperation operation)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0002: Expected I4, but got Unknown
		return (int)operation;
	}

	public static int ConvertSyncStateToInt(ESyncState state)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0002: Expected I4, but got Unknown
		return (int)state;
	}

	public static int ConvertSyncStatusToInt(TrackSyncStatus status)
	{
		return (int)status;
	}

	public static int GetGasGaugeSegmentWidth(int maxWidth, long size, long totalSize, int margins)
	{
		totalSize = Math.Max(totalSize, 1L);
		size = Math.Max(size, 0L);
		int val = (int)Math.Min(maxWidth * size / totalSize, maxWidth - margins);
		return Math.Max(val, 0);
	}

	public static string FormatLongAsSize(long sizeInBytes)
	{
		if (sizeInBytes < DevicelandMegabyte && sizeInBytes != 0)
		{
			return string.Format(_sizeInKB, (float)sizeInBytes / (float)DevicelandKilobyte);
		}
		if (sizeInBytes < DevicelandGigabyte)
		{
			return string.Format(_sizeInMB, (float)sizeInBytes / (float)DevicelandMegabyte);
		}
		return string.Format(_sizeInGB, (float)sizeInBytes / (float)DevicelandGigabyte);
	}

	public void BrowseAndReplaceMedia(IList items, int mediaType)
	{
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_003e: Expected O, but got Unknown
		FileOpenDialog.Show(Shell.LoadString(StringId.IDS_OPEN_FILE_DIALOG_TITLE), ZuneShell.DefaultInstance.Management.MediaFolder, (DeferredInvokeHandler)delegate(object args)
		{
			//IL_003e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0044: Expected O, but got Unknown
			DeferredInvokeHandler val = null;
			string path = (string)args;
			if (!string.IsNullOrEmpty(path))
			{
				Shell.DeleteMedia(items, deleteFileOnDisk: false);
				if (val == null)
				{
					val = (DeferredInvokeHandler)delegate
					{
						try
						{
							if (ZuneApplication.ZuneLibrary.CanAddMedia(path, (EMediaTypes)mediaType))
							{
								ZuneApplication.ZuneLibrary.AddMedia(path);
							}
						}
						catch (UnauthorizedAccessException)
						{
						}
						catch (IOException)
						{
						}
					};
				}
				Application.DeferredInvoke(val, (object)null);
			}
		});
	}
}
