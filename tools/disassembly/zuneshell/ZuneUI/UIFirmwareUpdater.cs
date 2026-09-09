using System;
using System.Threading;
using Microsoft.Iris;
using Microsoft.Zune.Configuration;
using MicrosoftZuneLibrary;

namespace ZuneUI;

public class UIFirmwareUpdater : ModelItem
{
	private FirmwareUpdater _updater;

	private bool _installFirmware;

	private bool _installGames;

	private UpdatePackageCollection _updateCollection;

	private bool _isUpdateAvailable;

	private bool _rollbackStarted;

	private bool _isCheckingForUpdates;

	private bool _updateInProgress;

	private UpdateStep _currentStep;

	private HRESULT _lastCheckForUpdatesResult;

	private HRESULT _lastCheckForDiskSpaceResult;

	private FirmwareUpdateErrorInfo _lastCheckForUpdateErrorInfo;

	private HRESULT _lastUpdateResult;

	private FirmwareUpdateErrorInfo _lastFirmwareUpdateErrorInfo;

	private bool _isSoftFailure;

	private bool _canCancel;

	private bool _needsCollectionRefresh;

	private bool _launchWizardIfUpdatesFound;

	private bool _fCancelInProgress;

	private int _estimatedUpdateTime;

	private bool _estimatedUpdateTimeInProgress;

	private UIDevice _device;

	private bool _requiresSyncBeforeUpdate;

	private FirmwareUpdateOption _updateOption;

	private CheckDiskSpaceArgs _diskSpaceInfo;

	internal FirmwareUpdater FirmwareUpdater => _updater;

	public UpdatePackageCollection UpdateCollection
	{
		get
		{
			return _updateCollection;
		}
		private set
		{
			if (_updateCollection != value)
			{
				if (_updateCollection != null)
				{
					_updateCollection.Dispose();
				}
				UpdateEstimatedTime = 0;
				_updateCollection = value;
				((ModelItem)this).FirePropertyChanged("UpdateCollection");
			}
		}
	}

	public UpdateStep CurrentStep
	{
		get
		{
			return _currentStep;
		}
		private set
		{
			if (_currentStep != value)
			{
				if (_currentStep != null)
				{
					_currentStep.Dispose();
				}
				_currentStep = value;
				((ModelItem)this).FirePropertyChanged("CurrentStep");
				CanCancel = _currentStep != null && _currentStep.Cancelable;
			}
		}
	}

	public bool CanCancel
	{
		get
		{
			return _canCancel;
		}
		private set
		{
			if (_canCancel != value)
			{
				_canCancel = value;
				((ModelItem)this).FirePropertyChanged("CanCancel");
			}
		}
	}

	public bool IsCheckingForUpdates
	{
		get
		{
			return _isCheckingForUpdates;
		}
		private set
		{
			if (_isCheckingForUpdates != value)
			{
				_isCheckingForUpdates = value;
				((ModelItem)this).FirePropertyChanged("IsCheckingForUpdates");
			}
		}
	}

	public bool NeedsCollectionRefresh
	{
		get
		{
			return _needsCollectionRefresh;
		}
		private set
		{
			if (_needsCollectionRefresh != value)
			{
				_needsCollectionRefresh = value;
				((ModelItem)this).FirePropertyChanged("NeedsCollectionRefresh");
			}
		}
	}

	public FirmwareUpdateOption UpdateOption
	{
		get
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			return _updateOption;
		}
		set
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			//IL_0006: Unknown result type (might be due to invalid IL or missing references)
			//IL_000a: Unknown result type (might be due to invalid IL or missing references)
			//IL_000b: Unknown result type (might be due to invalid IL or missing references)
			if (_updateOption != value)
			{
				_updateOption = value;
				((ModelItem)this).FirePropertyChanged("UpdateOption");
			}
		}
	}

	public CheckDiskSpaceArgs DiskSpaceInfo
	{
		get
		{
			return _diskSpaceInfo;
		}
		private set
		{
			if (_diskSpaceInfo != value)
			{
				_diskSpaceInfo = value;
				((ModelItem)this).FirePropertyChanged("DiskSpaceInfo");
			}
		}
	}

	public HRESULT LastCheckForUpdatesResult
	{
		get
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			return _lastCheckForUpdatesResult;
		}
		private set
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			//IL_0006: Unknown result type (might be due to invalid IL or missing references)
			//IL_000f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0010: Unknown result type (might be due to invalid IL or missing references)
			if (_lastCheckForUpdatesResult != value)
			{
				_lastCheckForUpdatesResult = value;
				((ModelItem)this).FirePropertyChanged("LastCheckForUpdatesResult");
			}
		}
	}

	public HRESULT LastCheckForDiskSpaceResult
	{
		get
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			return _lastCheckForDiskSpaceResult;
		}
		private set
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			//IL_0006: Unknown result type (might be due to invalid IL or missing references)
			//IL_000f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0010: Unknown result type (might be due to invalid IL or missing references)
			if (_lastCheckForDiskSpaceResult != value)
			{
				_lastCheckForDiskSpaceResult = value;
				((ModelItem)this).FirePropertyChanged("LastCheckForDiskSpaceResult");
			}
		}
	}

	public string LastCheckForUpdateErrorOverrideDescription
	{
		get
		{
			if (_lastCheckForUpdateErrorInfo == null || string.IsNullOrEmpty(_lastCheckForUpdateErrorInfo.Description))
			{
				return null;
			}
			return _lastCheckForUpdateErrorInfo.Description;
		}
	}

	public string LastCheckForUpdateErrorWebHelpUrl
	{
		get
		{
			if (_lastCheckForUpdateErrorInfo == null || string.IsNullOrEmpty(_lastCheckForUpdateErrorInfo.Url))
			{
				return null;
			}
			return _lastCheckForUpdateErrorInfo.Url;
		}
	}

	public HRESULT LastUpdateResult
	{
		get
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			return _lastUpdateResult;
		}
		private set
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			//IL_0006: Unknown result type (might be due to invalid IL or missing references)
			//IL_000f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0010: Unknown result type (might be due to invalid IL or missing references)
			//IL_0022: Unknown result type (might be due to invalid IL or missing references)
			//IL_0027: Unknown result type (might be due to invalid IL or missing references)
			if (_lastUpdateResult != value)
			{
				_lastUpdateResult = value;
				((ModelItem)this).FirePropertyChanged("LastUpdateResult");
				IsSoftFailure = _lastUpdateResult == HRESULT._NS_E_FIRMWARE_UPDATE_DISK_FULL;
			}
		}
	}

	public string LastFirmwareUpdateErrorOverrideDescription
	{
		get
		{
			if (_lastFirmwareUpdateErrorInfo == null || string.IsNullOrEmpty(_lastFirmwareUpdateErrorInfo.Description))
			{
				return null;
			}
			return _lastFirmwareUpdateErrorInfo.Description;
		}
	}

	public string LastFirmwareUpdateErrorWebHelpUrl
	{
		get
		{
			if (_lastFirmwareUpdateErrorInfo == null || string.IsNullOrEmpty(_lastFirmwareUpdateErrorInfo.Url))
			{
				return null;
			}
			return _lastFirmwareUpdateErrorInfo.Url;
		}
	}

	public bool IsSoftFailure
	{
		get
		{
			return _isSoftFailure;
		}
		private set
		{
			if (_isSoftFailure != value)
			{
				_isSoftFailure = value;
				((ModelItem)this).FirePropertyChanged("IsSoftFailure");
			}
		}
	}

	public bool UpdateInProgress
	{
		get
		{
			return _updateInProgress;
		}
		private set
		{
			if (_updateInProgress == value)
			{
				return;
			}
			_updateInProgress = value;
			((ModelItem)this).FirePropertyChanged("UpdateInProgress");
			if (_updateInProgress)
			{
				if (((FirmwareOperationBase)_updater).EnterContinuousPowerMode())
				{
				}
			}
			else
			{
				((FirmwareOperationBase)_updater).LeaveContinuousPowerMode();
			}
		}
	}

	public bool RollbackStarted
	{
		get
		{
			return _rollbackStarted;
		}
		set
		{
			if (_rollbackStarted != value)
			{
				_rollbackStarted = value;
				((ModelItem)this).FirePropertyChanged("RollbackStarted");
			}
		}
	}

	public bool IsUpdateAvailable
	{
		get
		{
			return _isUpdateAvailable;
		}
		private set
		{
			if (_isUpdateAvailable != value)
			{
				_isUpdateAvailable = value;
				((ModelItem)this).FirePropertyChanged("IsUpdateAvailable");
			}
		}
	}

	public string AvailableFirmwareDescription
	{
		get
		{
			string result = null;
			if (_updateCollection != null)
			{
				FirmwareUpdatePackage firmwarePackage = _updateCollection.FirmwarePackage;
				if (firmwarePackage != null)
				{
					result = ((!SyncControls.Instance.CurrentDeviceOverride.SupportsBrandingType(DeviceBranding.WindowsPhone)) ? StringParserHelper.FormatFirmwareVersion(firmwarePackage.Version) : firmwarePackage.Name);
				}
			}
			return result;
		}
	}

	public string NewFirmwareEULAContent
	{
		get
		{
			string result = null;
			if (_updateCollection != null)
			{
				FirmwareUpdatePackage firmwarePackage = _updateCollection.FirmwarePackage;
				if (firmwarePackage != null)
				{
					result = firmwarePackage.EULAContent;
				}
			}
			return result;
		}
	}

	public string MoreInfoURL
	{
		get
		{
			string result = null;
			if (_updateCollection != null)
			{
				FirmwareUpdatePackage firmwarePackage = _updateCollection.FirmwarePackage;
				if (firmwarePackage != null)
				{
					result = firmwarePackage.MoreInfoURL;
				}
			}
			return result;
		}
	}

	public int UpdateEstimatedTime
	{
		get
		{
			return _estimatedUpdateTime;
		}
		set
		{
			if (_estimatedUpdateTime != value)
			{
				_estimatedUpdateTime = value;
				((ModelItem)this).FirePropertyChanged("UpdateEstimatedTime");
			}
		}
	}

	public string NewFirmwareDescription
	{
		get
		{
			string result = string.Empty;
			if (_updateCollection != null)
			{
				FirmwareUpdatePackage firmwarePackage = _updateCollection.FirmwarePackage;
				if (firmwarePackage != null)
				{
					result = firmwarePackage.Description;
				}
			}
			return result;
		}
	}

	public bool GamesPackIsOnlyUpdateAvailable
	{
		get
		{
			if (_updateCollection != null && _updateCollection.FirmwarePackage == null && _updateCollection.GamesPackage != null)
			{
				return true;
			}
			return false;
		}
	}

	public bool InstallFirmware
	{
		get
		{
			return _installFirmware;
		}
		set
		{
			//IL_002d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0033: Invalid comparison between Unknown and I4
			if (_installFirmware == value)
			{
				return;
			}
			_installFirmware = value;
			((ModelItem)this).FirePropertyChanged("InstallFirmware");
			for (int i = 0; i < _updateCollection.Count; i++)
			{
				FirmwareUpdatePackage val = _updateCollection[i];
				if ((int)val.Type == 1)
				{
					_updateCollection[i] = _installFirmware;
				}
			}
		}
	}

	public bool InstallGames
	{
		get
		{
			return _installGames;
		}
		set
		{
			//IL_002d: Unknown result type (might be due to invalid IL or missing references)
			if (_installGames == value)
			{
				return;
			}
			_installGames = value;
			((ModelItem)this).FirePropertyChanged("InstallGames");
			for (int i = 0; i < _updateCollection.Count; i++)
			{
				FirmwareUpdatePackage val = _updateCollection[i];
				if ((int)val.Type == 0)
				{
					_updateCollection[i] = _installGames;
				}
			}
		}
	}

	public bool IsOnBatteryPower
	{
		get
		{
			//IL_0008: Unknown result type (might be due to invalid IL or missing references)
			//IL_000d: Unknown result type (might be due to invalid IL or missing references)
			bool result = default(bool);
			HRESULT val = ((FirmwareOperationBase)_updater).CheckPowerRequirements(ref result);
			if (((HRESULT)(ref val)).IsError)
			{
				return true;
			}
			return result;
		}
	}

	public bool RequiresSyncBeforeUpdate
	{
		get
		{
			return _requiresSyncBeforeUpdate;
		}
		private set
		{
			if (_requiresSyncBeforeUpdate != value)
			{
				_requiresSyncBeforeUpdate = value;
				((ModelItem)this).FirePropertyChanged("RequiresSyncBeforeUpdate");
			}
		}
	}

	internal UIFirmwareUpdater(UIDevice device, FirmwareUpdater updater)
	{
		_device = device;
		_updater = updater;
		ResetState();
	}

	private void ResetState()
	{
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a1: Unknown result type (might be due to invalid IL or missing references)
		_installFirmware = false;
		_installGames = false;
		_updateCollection = null;
		_isUpdateAvailable = false;
		_isCheckingForUpdates = false;
		_updateInProgress = false;
		_currentStep = null;
		_lastCheckForUpdatesResult = HRESULT._S_OK;
		_lastCheckForDiskSpaceResult = HRESULT._S_OK;
		_lastCheckForUpdateErrorInfo = null;
		_lastUpdateResult = HRESULT._S_OK;
		_lastFirmwareUpdateErrorInfo = null;
		_isSoftFailure = false;
		_canCancel = true;
		_needsCollectionRefresh = true;
		_launchWizardIfUpdatesFound = false;
		_fCancelInProgress = false;
		_rollbackStarted = false;
		_estimatedUpdateTime = 0;
		_estimatedUpdateTimeInProgress = false;
		_requiresSyncBeforeUpdate = false;
		_updateOption = (FirmwareUpdateOption)0;
		_diskSpaceInfo = null;
		((ModelItem)this).FirePropertyChanged("IsUpdateAvailable");
	}

	protected override void OnDispose(bool disposing)
	{
		if (disposing)
		{
			((FirmwareOperationBase)_updater).Dispose();
			_updater = null;
			UpdateCollection = null;
			CurrentStep = null;
		}
		((ModelItem)this).OnDispose(disposing);
	}

	public void StartEstimatedUpdateTimeCalculation()
	{
		if (!IsCheckingForUpdates && !_estimatedUpdateTimeInProgress && _updateCollection != null && _updateCollection.FirmwarePackage != null)
		{
			_estimatedUpdateTimeInProgress = true;
			ThreadPool.QueueUserWorkItem(EstimatedUpdateTimeCalculationWorker, null);
		}
	}

	private void EstimatedUpdateTimeCalculationWorker(object args)
	{
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0059: Expected O, but got Unknown
		int estimatedTime = 0;
		if (_updateCollection != null)
		{
			FirmwareUpdatePackage firmwarePackage = _updateCollection.FirmwarePackage;
			if (firmwarePackage != null)
			{
				TimeSpan updateEstimatedTime = firmwarePackage.UpdateEstimatedTime;
				_estimatedUpdateTime = -1;
				estimatedTime = (int)updateEstimatedTime.TotalMinutes;
			}
		}
		Application.DeferredInvoke((DeferredInvokeHandler)delegate
		{
			UpdateEstimatedTime = estimatedTime;
			_estimatedUpdateTimeInProgress = false;
		}, (object)null);
	}

	public void StartCheckForUpdates(bool forceServerRequest, bool launchWizardIfUpdatesFound)
	{
		//IL_00a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ac: Expected O, but got Unknown
		//IL_00a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0070: Expected O, but got Unknown
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Expected O, but got Unknown
		DeferredInvokeHandler val = null;
		if (IsCheckingForUpdates || !SyncControls.Instance.CurrentDeviceOverride.IsConnectedToClientPhysically)
		{
			return;
		}
		if (_device.AllowChainedUpdates && !forceServerRequest)
		{
			if (IsUpdateAvailable && launchWizardIfUpdatesFound)
			{
				Application.DeferredInvoke((DeferredInvokeHandler)delegate
				{
					ZuneShell.DefaultInstance.NavigateToPage(new DeviceUpdateLandPage());
				}, (object)null);
				return;
			}
			if (val == null)
			{
				val = (DeferredInvokeHandler)delegate
				{
					_device.AllowChainedUpdates = false;
					_device.NavigateToDeviceSummaryAfterUpdate = false;
					((Command)Shell.MainFrame.Device).Invoke();
				};
			}
			Application.DeferredInvoke(val, (object)null);
		}
		else
		{
			ResetState();
			IsCheckingForUpdates = true;
			_lastCheckForUpdateErrorInfo = null;
			_launchWizardIfUpdatesFound = launchWizardIfUpdatesFound;
			LastCheckForUpdatesResult = _updater.StartCheckForUpdates(forceServerRequest, new DeferredInvokeHandler(OnCheckForUpdatesComplete));
			HRESULT lastCheckForUpdatesResult = LastCheckForUpdatesResult;
			if (((HRESULT)(ref lastCheckForUpdatesResult)).IsError)
			{
				IsCheckingForUpdates = false;
			}
		}
	}

	private void OnCheckForUpdatesComplete(object data)
	{
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_002b: Expected O, but got Unknown
		Application.DeferredInvoke((DeferredInvokeHandler)delegate
		{
			//IL_0006: Unknown result type (might be due to invalid IL or missing references)
			//IL_000c: Expected O, but got Unknown
			//IL_0018: Unknown result type (might be due to invalid IL or missing references)
			//IL_005b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0060: Unknown result type (might be due to invalid IL or missing references)
			CheckForUpdatesArgs val = (CheckForUpdatesArgs)data;
			LastCheckForUpdatesResult = val.ErrorInfo.HrStatus;
			_lastCheckForUpdateErrorInfo = val.ErrorInfo;
			_lastFirmwareUpdateErrorInfo = val.ErrorInfo;
			RequiresSyncBeforeUpdate = val.RequiresSyncBeforeUpdate;
			HRESULT lastCheckForUpdatesResult = LastCheckForUpdatesResult;
			if (((HRESULT)(ref lastCheckForUpdatesResult)).IsSuccess)
			{
				UpdateCollection = val.UpdatePackages;
				IsUpdateAvailable = UpdateCollection != null && UpdateCollection.Count > 0;
			}
			else
			{
				UpdateCollection = null;
			}
			NeedsCollectionRefresh = false;
			IsCheckingForUpdates = false;
			if (IsUpdateAvailable && _launchWizardIfUpdatesFound && (!(ZuneShell.DefaultInstance.CurrentPage is QuickplayPage) || !ClientConfiguration.FUE.ShowArtistChooser) && (ZuneShell.DefaultInstance.Management.CurrentCategoryPage == null || !ZuneShell.DefaultInstance.Management.CurrentCategoryPage.IsWizard) && !Shell.SettingsFrame.IsCurrent && !(ZuneShell.DefaultInstance.CurrentPage is SetupLandPage))
			{
				ZuneShell.DefaultInstance.NavigateToPage(new DeviceUpdateLandPage());
			}
		}, data);
	}

	public void StartCheckForDiskSpace()
	{
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_001f: Expected O, but got Unknown
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		_diskSpaceInfo = null;
		LastCheckForDiskSpaceResult = _updater.StartCheckForDiskSpace(new DeferredInvokeHandler(OnCheckForDiskSpaceComplete));
	}

	private void OnCheckForDiskSpaceComplete(object data)
	{
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_002b: Expected O, but got Unknown
		Application.DeferredInvoke((DeferredInvokeHandler)delegate
		{
			//IL_0006: Unknown result type (might be due to invalid IL or missing references)
			//IL_000c: Expected O, but got Unknown
			//IL_0018: Unknown result type (might be due to invalid IL or missing references)
			CheckDiskSpaceArgs val = (CheckDiskSpaceArgs)data;
			LastCheckForDiskSpaceResult = HRESULT.op_Implicit(val.HrStatus);
			DiskSpaceInfo = val;
		}, data);
	}

	public void StartFirmwareUpdate()
	{
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0059: Unknown result type (might be due to invalid IL or missing references)
		//IL_0065: Unknown result type (might be due to invalid IL or missing references)
		//IL_006b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0075: Expected O, but got Unknown
		//IL_0075: Expected O, but got Unknown
		//IL_0075: Expected O, but got Unknown
		//IL_0070: Unknown result type (might be due to invalid IL or missing references)
		//IL_007b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0080: Unknown result type (might be due to invalid IL or missing references)
		if (!UpdateInProgress)
		{
			UpdateInProgress = true;
			_currentStep = null;
			_lastUpdateResult = HRESULT._S_OK;
			_lastFirmwareUpdateErrorInfo = null;
			_canCancel = true;
			_rollbackStarted = false;
			LastUpdateResult = _updater.StartFirmwareUpdate(_updateCollection, new DeferredInvokeHandler(OnFirmwareUpdateStepBegin), new DeferredInvokeHandler(OnFirmwareUpdateStepProgress), new DeferredInvokeHandler(OnFirmwareUpdateCompleted), UpdateOption);
			HRESULT lastUpdateResult = LastUpdateResult;
			if (((HRESULT)(ref lastUpdateResult)).IsError)
			{
				UpdateInProgress = false;
			}
			else
			{
				Shell.IgnoreAppNavigationsArgs = true;
			}
		}
	}

	private void OnFirmwareUpdateStepBegin(object data)
	{
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_002b: Expected O, but got Unknown
		Application.DeferredInvoke((DeferredInvokeHandler)delegate
		{
			//IL_0013: Unknown result type (might be due to invalid IL or missing references)
			//IL_0019: Expected O, but got Unknown
			if (UpdateInProgress)
			{
				FirmwareUpdateBeginArgs val = (FirmwareUpdateBeginArgs)data;
				CurrentStep = val.Step;
			}
		}, data);
	}

	private void OnFirmwareUpdateStepProgress(object data)
	{
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_002b: Expected O, but got Unknown
		Application.DeferredInvoke((DeferredInvokeHandler)delegate
		{
			//IL_0013: Unknown result type (might be due to invalid IL or missing references)
			//IL_0019: Expected O, but got Unknown
			if (UpdateInProgress)
			{
				FirmwareUpdateProgressArgs val = (FirmwareUpdateProgressArgs)data;
				CurrentStep = val.Step;
			}
		}, data);
	}

	private void OnFirmwareUpdateCompleted(object data)
	{
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_002b: Expected O, but got Unknown
		Application.DeferredInvoke((DeferredInvokeHandler)delegate
		{
			//IL_0016: Unknown result type (might be due to invalid IL or missing references)
			//IL_001c: Expected O, but got Unknown
			//IL_001d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0022: Unknown result type (might be due to invalid IL or missing references)
			//IL_0023: Unknown result type (might be due to invalid IL or missing references)
			//IL_0025: Unknown result type (might be due to invalid IL or missing references)
			//IL_0037: Expected I4, but got Unknown
			//IL_009d: Unknown result type (might be due to invalid IL or missing references)
			//IL_00c4: Unknown result type (might be due to invalid IL or missing references)
			//IL_00c9: Unknown result type (might be due to invalid IL or missing references)
			if (UpdateInProgress)
			{
				FirmwareProcessCompleteArgs val = (FirmwareProcessCompleteArgs)data;
				CompletionAction action = val.Action;
				switch (action - 2)
				{
				case 0:
					if (IsLegacyZuneDevice())
					{
						CanCancel = true;
					}
					break;
				case 2:
				{
					CurrentStep = null;
					CanCancel = true;
					NeedsCollectionRefresh = true;
					UpdateInProgress = false;
					_fCancelInProgress = false;
					LastUpdateResult = val.ErrorInfo.HrStatus;
					_lastFirmwareUpdateErrorInfo = val.ErrorInfo;
					UIFirmwareUpdater uIFirmwareUpdater = this;
					HRESULT lastUpdateResult = LastUpdateResult;
					uIFirmwareUpdater.IsUpdateAvailable = ((HRESULT)(ref lastUpdateResult)).IsError && !IsSoftFailure;
					Shell.IgnoreAppNavigationsArgs = false;
					break;
				}
				case 1:
					CanCancel = false;
					RollbackStarted = true;
					break;
				}
			}
		}, data);
	}

	public void CancelFirmwareUpdate()
	{
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
		Shell.IgnoreAppNavigationsArgs = false;
		if (!_fCancelInProgress && (UpdateInProgress || IsCheckingForUpdates))
		{
			_fCancelInProgress = true;
			_updater.Cancel();
		}
	}

	private bool IsLegacyZuneDevice()
	{
		UIDevice currentDeviceOverride = SyncControls.Instance.CurrentDeviceOverride;
		return currentDeviceOverride.Class == DeviceClass.Classic;
	}
}
