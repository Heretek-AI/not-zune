using System;
using Microsoft.Iris;
using MicrosoftZuneLibrary;

namespace ZuneUI;

public class UIFirmwareRestorer : ModelItem
{
	private FirmwareRestorer _restorer;

	private FirmwareRestorePoint _restorePoint;

	private bool _restoreInProgress;

	private bool _restorePointCollectionRefreshInProgress;

	private HRESULT _lastRestoreResult;

	private HRESULT _lastRefreshRestorePointResult;

	private UpdateStep _currentStep;

	private FirmwareUpdateErrorInfo _lastFirmwareRestoreErrorInfo;

	private bool _inRecoveryMode;

	private bool _needsCollectionRefresh = true;

	private bool _fCancelInProgress;

	internal FirmwareRestorer FirmwareRestorer => _restorer;

	public int EstimatedRestoreTime
	{
		get
		{
			int result = 0;
			if (_restorePoint != null && _restorePoint.EstimatedRestoreTime != TimeSpan.Zero)
			{
				result = (int)_restorePoint.EstimatedRestoreTime.TotalMinutes;
			}
			return result;
		}
	}

	public FirmwareRestorePoint RestorePoint => _restorePoint;

	public bool IsCheckingForRestorePoints
	{
		get
		{
			return _restorePointCollectionRefreshInProgress;
		}
		private set
		{
			if (_restorePointCollectionRefreshInProgress != value)
			{
				_restorePointCollectionRefreshInProgress = value;
				((ModelItem)this).FirePropertyChanged("IsCheckingForRestorePoints");
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

	public bool RestoreInProgress
	{
		get
		{
			return _restoreInProgress;
		}
		private set
		{
			if (_restoreInProgress == value)
			{
				return;
			}
			_restoreInProgress = value;
			((ModelItem)this).FirePropertyChanged("RestoreInProgress");
			if (_restoreInProgress)
			{
				if (((FirmwareOperationBase)_restorer).EnterContinuousPowerMode())
				{
				}
			}
			else
			{
				((FirmwareOperationBase)_restorer).LeaveContinuousPowerMode();
			}
		}
	}

	public HRESULT LastRestoreResult
	{
		get
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			return _lastRestoreResult;
		}
		private set
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			//IL_0006: Unknown result type (might be due to invalid IL or missing references)
			//IL_000f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0010: Unknown result type (might be due to invalid IL or missing references)
			if (_lastRestoreResult != value)
			{
				_lastRestoreResult = value;
				((ModelItem)this).FirePropertyChanged("LastRestoreResult");
			}
		}
	}

	public HRESULT LastRefreshRestorePointResult
	{
		get
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			return _lastRefreshRestorePointResult;
		}
		private set
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			//IL_0006: Unknown result type (might be due to invalid IL or missing references)
			//IL_000f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0010: Unknown result type (might be due to invalid IL or missing references)
			if (_lastRefreshRestorePointResult != value)
			{
				_lastRefreshRestorePointResult = value;
				((ModelItem)this).FirePropertyChanged("LastRefreshRestorePointResult");
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
			}
		}
	}

	public string LastFirmwareRestoreErrorDescription
	{
		get
		{
			string result = string.Empty;
			if (_lastFirmwareRestoreErrorInfo != null && !string.IsNullOrEmpty(_lastFirmwareRestoreErrorInfo.Description))
			{
				result = _lastFirmwareRestoreErrorInfo.Description;
			}
			else if (RestorePoint == null)
			{
				result = ((!_inRecoveryMode) ? Shell.LoadString(StringId.IDS_DEVICE_RESTORE_ERROR_NO_POINTS) : Shell.LoadString(StringId.IDS_DEVICE_RECOVERY_ERROR_NO_POINTS));
			}
			return result;
		}
	}

	public string LastFirmwareRestoreErrorWebHelpUrl
	{
		get
		{
			if (_lastFirmwareRestoreErrorInfo == null || string.IsNullOrEmpty(_lastFirmwareRestoreErrorInfo.Url))
			{
				return null;
			}
			return _lastFirmwareRestoreErrorInfo.Url;
		}
	}

	public bool IsOnBatteryPower
	{
		get
		{
			//IL_0008: Unknown result type (might be due to invalid IL or missing references)
			//IL_000d: Unknown result type (might be due to invalid IL or missing references)
			bool result = default(bool);
			HRESULT val = ((FirmwareOperationBase)_restorer).CheckPowerRequirements(ref result);
			if (((HRESULT)(ref val)).IsError)
			{
				return true;
			}
			return result;
		}
	}

	internal UIFirmwareRestorer(FirmwareRestorer restorer, bool inRecoveryMode)
	{
		_restorer = restorer;
		_lastFirmwareRestoreErrorInfo = null;
		_inRecoveryMode = inRecoveryMode;
	}

	public void StartRefreshRestorePointCollection()
	{
		//IL_0008: Unknown result type (might be due to invalid IL or missing references)
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0029: Expected O, but got Unknown
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		IsCheckingForRestorePoints = true;
		LastRefreshRestorePointResult = HRESULT._E_PENDING;
		HRESULT val = _restorer.StartGetRestorePointCollection(new DeferredInvokeHandler(OnRefreshRestorePointCollectionComplete));
		if (((HRESULT)(ref val)).IsError)
		{
			LastRefreshRestorePointResult = HRESULT._E_FAIL;
			IsCheckingForRestorePoints = false;
		}
	}

	private void OnRefreshRestorePointCollectionComplete(object data)
	{
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_002b: Expected O, but got Unknown
		Application.DeferredInvoke((DeferredInvokeHandler)delegate
		{
			//IL_0006: Unknown result type (might be due to invalid IL or missing references)
			//IL_000c: Expected O, but got Unknown
			//IL_004e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0030: Unknown result type (might be due to invalid IL or missing references)
			FirmwareRestorePointCollection val = (FirmwareRestorePointCollection)data;
			if (val != null && val.Count > 0)
			{
				_restorePoint = val.GetRestorePoint(0);
				LastRefreshRestorePointResult = HRESULT._S_OK;
			}
			else
			{
				_restorePoint = null;
				LastRefreshRestorePointResult = HRESULT._ZUNE_E_NO_AVAILABLE_RESTORE_POINT;
			}
			NeedsCollectionRefresh = false;
			IsCheckingForRestorePoints = false;
		}, data);
	}

	public void CancelFirmwareRestore()
	{
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
		Shell.IgnoreAppNavigationsArgs = false;
		if (!_fCancelInProgress && (RestoreInProgress || IsCheckingForRestorePoints))
		{
			_fCancelInProgress = true;
			_restorer.Cancel();
		}
	}

	public void StartRestore()
	{
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_004c: Expected O, but got Unknown
		//IL_004c: Expected O, but got Unknown
		//IL_004c: Expected O, but got Unknown
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		//IL_0052: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		if (!RestoreInProgress)
		{
			RestoreInProgress = true;
			_lastFirmwareRestoreErrorInfo = null;
			LastRestoreResult = _restorer.StartFirmwareRestore(RestorePoint, new DeferredInvokeHandler(OnFirmwareRestoreStepBegin), new DeferredInvokeHandler(OnFirmwareRestoreStepProgress), new DeferredInvokeHandler(OnFirmwareRestoreCompleted));
			HRESULT lastRestoreResult = LastRestoreResult;
			if (((HRESULT)(ref lastRestoreResult)).IsError)
			{
				RestoreInProgress = false;
			}
			else
			{
				Shell.IgnoreAppNavigationsArgs = true;
			}
		}
	}

	private void OnFirmwareRestoreStepBegin(object data)
	{
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_002b: Expected O, but got Unknown
		Application.DeferredInvoke((DeferredInvokeHandler)delegate
		{
			//IL_0013: Unknown result type (might be due to invalid IL or missing references)
			//IL_0019: Expected O, but got Unknown
			if (RestoreInProgress)
			{
				FirmwareUpdateBeginArgs val = (FirmwareUpdateBeginArgs)data;
				CurrentStep = val.Step;
			}
		}, data);
	}

	private void OnFirmwareRestoreStepProgress(object data)
	{
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_002b: Expected O, but got Unknown
		Application.DeferredInvoke((DeferredInvokeHandler)delegate
		{
			//IL_0013: Unknown result type (might be due to invalid IL or missing references)
			//IL_0019: Expected O, but got Unknown
			if (RestoreInProgress)
			{
				FirmwareUpdateProgressArgs val = (FirmwareUpdateProgressArgs)data;
				CurrentStep = val.Step;
			}
		}, data);
	}

	private void OnFirmwareRestoreCompleted(object data)
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
			//IL_0074: Unknown result type (might be due to invalid IL or missing references)
			if (RestoreInProgress)
			{
				FirmwareProcessCompleteArgs val = (FirmwareProcessCompleteArgs)data;
				CompletionAction action = val.Action;
				switch (action - 2)
				{
				case 2:
					CurrentStep = null;
					NeedsCollectionRefresh = true;
					RestoreInProgress = false;
					_fCancelInProgress = false;
					LastRestoreResult = val.ErrorInfo.HrStatus;
					_lastFirmwareRestoreErrorInfo = val.ErrorInfo;
					Shell.IgnoreAppNavigationsArgs = false;
					break;
				case 0:
				case 1:
					break;
				}
			}
		}, data);
	}
}
