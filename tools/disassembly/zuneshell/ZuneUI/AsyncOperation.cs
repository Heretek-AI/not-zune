using Microsoft.Zune.ErrorMapperApi;

namespace ZuneUI;

internal abstract class AsyncOperation
{
	public delegate void AOComplete(bool success);

	protected const int IdleState = -1;

	protected const int FinishedState = -2;

	protected UIDevice _device;

	protected string _error;

	protected string _detailedError;

	protected HRESULT _hr;

	private AOComplete _completeFunc;

	private WirelessStates[] _states;

	private bool _fListening;

	private bool _fTryToCancel;

	private bool _fCanceled;

	protected static int _iCurrentState = -1;

	public string Error
	{
		get
		{
			if (Finished)
			{
				return _error;
			}
			return null;
		}
	}

	public string DetailedError
	{
		get
		{
			if (Finished)
			{
				return _detailedError;
			}
			return null;
		}
	}

	public HRESULT Hr
	{
		get
		{
			//IL_000f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0009: Unknown result type (might be due to invalid IL or missing references)
			if (Finished)
			{
				return _hr;
			}
			return HRESULT._E_PENDING;
		}
	}

	public bool Canceled
	{
		get
		{
			if (Finished)
			{
				return _fCanceled;
			}
			return false;
		}
	}

	protected bool Idle => _iCurrentState == -1;

	protected bool Finished => _iCurrentState == -2;

	protected abstract WirelessStateResults DoStep(WirelessStates state);

	public virtual void Cancel()
	{
		if (!Idle && !Finished)
		{
			_fTryToCancel = true;
		}
	}

	protected virtual void EndOperation(WirelessStateResults result)
	{
	}

	protected virtual void AddListeners()
	{
	}

	protected virtual void RemoveListeners()
	{
	}

	protected WirelessStateResults StartOperation(UIDevice device, AOComplete completeFunc, WirelessStates[] states)
	{
		WirelessStateResults wirelessStateResults = WirelessStateResults.Error;
		if (!Idle)
		{
			return WirelessStateResults.NotAvailable;
		}
		ResetState();
		_device = device;
		_states = states;
		_completeFunc = completeFunc;
		_iCurrentState = 0;
		if (device.IsConnectedToClient)
		{
			AddListenersInternal();
			wirelessStateResults = DoNextStep();
		}
		if (wirelessStateResults != WirelessStateResults.Success)
		{
			EndOperationInternal(wirelessStateResults);
		}
		return WirelessStateResults.Success;
	}

	protected void StepComplete(WirelessStateResults result)
	{
		_iCurrentState++;
		if (result == WirelessStateResults.Success)
		{
			result = DoNextStep();
		}
		if (result != WirelessStateResults.Success)
		{
			EndOperationInternal(result);
		}
	}

	protected void ClearResult()
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		_hr = HRESULT._S_OK;
		_detailedError = null;
	}

	protected void SetResult(HRESULT hr)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		_hr = hr;
		if (((HRESULT)(ref hr)).IsError)
		{
			ErrorMapperResult mappedErrorDescriptionAndUrl = ErrorMapperApi.GetMappedErrorDescriptionAndUrl(((HRESULT)(ref hr)).Int);
			if (mappedErrorDescriptionAndUrl.Hr != (uint)((HRESULT)(ref HRESULT._E_FAIL)).Int && mappedErrorDescriptionAndUrl.Hr != (uint)((HRESULT)(ref HRESULT._NS_E_WMP_UNKNOWN_ERROR)).Int)
			{
				_detailedError = mappedErrorDescriptionAndUrl.Description;
			}
		}
	}

	private void EndOperationInternal(WirelessStateResults result)
	{
		RemoveListenersInternal();
		if (result == WirelessStateResults.Canceled)
		{
			_fCanceled = true;
		}
		EndOperation(result);
		_iCurrentState = -2;
		if (_completeFunc != null)
		{
			_completeFunc(result == WirelessStateResults.Finished);
		}
		_iCurrentState = -1;
	}

	private WirelessStateResults DoNextStep()
	{
		if (_iCurrentState == _states.Length)
		{
			return WirelessStateResults.Finished;
		}
		if (_iCurrentState > _states.Length)
		{
			return WirelessStateResults.Error;
		}
		if (_fTryToCancel)
		{
			return WirelessStateResults.Canceled;
		}
		WirelessStateResults wirelessStateResults = DoStep(_states[_iCurrentState]);
		if (wirelessStateResults != WirelessStateResults.Success)
		{
			EndOperationInternal(wirelessStateResults);
		}
		return wirelessStateResults;
	}

	private void ResetState()
	{
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		if (Idle || Finished)
		{
			_error = null;
			_detailedError = null;
			_fTryToCancel = false;
			_fCanceled = false;
			_fListening = false;
			_hr = HRESULT._S_OK;
		}
	}

	private void AddListenersInternal()
	{
		if (!_fListening)
		{
			AddListeners();
			_fListening = true;
		}
	}

	private void RemoveListenersInternal()
	{
		if (_fListening)
		{
			RemoveListeners();
			_fListening = false;
		}
	}
}
