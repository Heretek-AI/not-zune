using System.Collections.Generic;
using System.Threading;
using Microsoft.Zune.Util;
using MicrosoftZuneLibrary;

namespace ZuneUI;

internal class AOSetProfile : AsyncOperation
{
	private WirelessStates[] _setProfileStates = new WirelessStates[4]
	{
		WirelessStates.GetDeviceProfiles,
		WirelessStates.AssociateWlanDevice,
		WirelessStates.CommitProfileToDevice,
		WirelessStates.TestDeviceProfile
	};

	private WlanProfile _deviceProfile;

	private WlanProfile _attemptingProfile;

	private AutoResetEvent _restore = new AutoResetEvent(initialState: false);

	private string _attemptingName = string.Empty;

	private bool _fProfileSaved;

	private string _wlanTestResult;

	private HRESULT _wlanTestResultCode;

	public string AttemptingName => _attemptingName;

	public bool WlanTestSucceeded
	{
		get
		{
			if (base.Finished)
			{
				return string.IsNullOrEmpty(_wlanTestResult);
			}
			return false;
		}
	}

	public override void Cancel()
	{
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
		base.Cancel();
		if (!base.Idle && !base.Finished && _setProfileStates[AsyncOperation._iCurrentState] == WirelessStates.TestDeviceProfile)
		{
			_device.CancelWiFiTest();
		}
	}

	private void ResetState(WlanProfile profile)
	{
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		if (base.Idle || base.Finished)
		{
			_attemptingName = profile.SSID;
			_attemptingProfile = profile;
			_deviceProfile = null;
			_fProfileSaved = false;
			_wlanTestResult = null;
			_wlanTestResultCode = HRESULT._S_OK;
			_restore.Reset();
		}
	}

	public WirelessStateResults StartOperation(UIDevice device, AOComplete completeFunc, WlanProfile newProfile)
	{
		if (newProfile != null)
		{
			ResetState(newProfile);
			return base.StartOperation(device, completeFunc, _setProfileStates);
		}
		return WirelessStateResults.Error;
	}

	protected override WirelessStateResults DoStep(WirelessStates state)
	{
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Expected O, but got Unknown
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0071: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0082: Unknown result type (might be due to invalid IL or missing references)
		//IL_0087: Unknown result type (might be due to invalid IL or missing references)
		//IL_007a: Unknown result type (might be due to invalid IL or missing references)
		//IL_007f: Unknown result type (might be due to invalid IL or missing references)
		//IL_005e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0063: Unknown result type (might be due to invalid IL or missing references)
		//IL_0094: Unknown result type (might be due to invalid IL or missing references)
		HRESULT result;
		switch (state)
		{
		case WirelessStates.GetDeviceProfiles:
			result = _device.ReceiveWiFiProfiles();
			break;
		case WirelessStates.CommitProfileToDevice:
		{
			WlanProfileList val = new WlanProfileList();
			((List<WlanProfile>)(object)val).Add(_attemptingProfile);
			result = _device.SetWiFiProfileList(val);
			if (((HRESULT)(ref result)).IsSuccess)
			{
				result = _device.SendWiFiProfiles();
			}
			break;
		}
		case WirelessStates.TestDeviceProfile:
			result = _device.TestWiFi();
			break;
		case WirelessStates.AssociateWlanDevice:
			result = _device.AssociateWiFi();
			break;
		default:
			result = HRESULT._E_ABORT;
			break;
		}
		if (((HRESULT)(ref result)).IsSuccess)
		{
			return WirelessStateResults.Success;
		}
		SetResult(result);
		return WirelessStateResults.Error;
	}

	protected override void EndOperation(WirelessStateResults result)
	{
		//IL_00ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		if (result != WirelessStateResults.Finished)
		{
			if (_fProfileSaved)
			{
				RestoreProfile();
			}
			if (result == WirelessStateResults.Canceled)
			{
				_error = Shell.LoadString(StringId.IDS_WIRELESS_SYNC_SETUP_CANCELED);
				_detailedError = null;
				_hr = HRESULT._S_OK;
			}
			else if (_attemptingProfile != null)
			{
				_error = string.Format(Shell.LoadString(StringId.IDS_WIRELESS_SYNC_SETUP_FAILED), _attemptingProfile.SSID);
			}
			else
			{
				_error = Shell.LoadString(StringId.IDS_WIRELESS_SYNC_SETUP_FAILED_GENERIC);
			}
		}
		else if (!string.IsNullOrEmpty(_wlanTestResult))
		{
			_error = string.Format(Shell.LoadString(StringId.IDS_WIRELESS_SYNC_TEST_FAILED), _attemptingProfile.SSID);
			_detailedError = _wlanTestResult;
			_hr = _wlanTestResultCode;
		}
		else
		{
			SQMLog.Log((SQMDataId)6, 1);
		}
	}

	protected override void AddListeners()
	{
		_device.WiFiProfilesReceivedEvent += Device_GetDeviceWlanProfilesCompleteEvent;
		_device.WiFiTestCompletedEvent += Device_TestDeviceWlanCompleteEvent;
		_device.WiFiProfilesSentEvent += Device_SetDeviceWlanProfilesCompleteEvent;
		_device.WiFiAssociationCompletedEvent += Device_AssociateWlanDeviceCompleteEvent;
	}

	protected override void RemoveListeners()
	{
		_device.WiFiProfilesReceivedEvent -= Device_GetDeviceWlanProfilesCompleteEvent;
		_device.WiFiTestCompletedEvent -= Device_TestDeviceWlanCompleteEvent;
		_device.WiFiProfilesSentEvent -= Device_SetDeviceWlanProfilesCompleteEvent;
		_device.WiFiAssociationCompletedEvent -= Device_AssociateWlanDeviceCompleteEvent;
	}

	private bool IsWirelessTestFailure(HRESULT hr)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		if (hr == HRESULT._NS_E_MTPZ_WLAN_TEST_FAIL_NO_CONFIG || hr == HRESULT._NS_E_MTPZ_WLAN_TEST_FAIL_ASSOCIATE || hr == HRESULT._NS_E_MTPZ_WLAN_TEST_FAIL_DHCP || hr == HRESULT._NS_E_MTPZ_WLAN_TEST_FAIL_TIMEOUT || hr == HRESULT._NS_E_MTPZ_WLAN_TEST_FAIL_INTERNAL || hr == HRESULT._NS_E_MTPZ_WLAN_TEST_RUNNING || hr == HRESULT._NS_E_MTPZ_WLAN_TEST_UNKNOWN)
		{
			return true;
		}
		return false;
	}

	private void RestoreProfile()
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Expected O, but got Unknown
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0052: Unknown result type (might be due to invalid IL or missing references)
		WlanProfileList val = new WlanProfileList();
		_device.WiFiProfilesSentEvent += Device_RestoreDeviceWlanProfilesCompleteEvent;
		if (_deviceProfile != null)
		{
			((List<WlanProfile>)(object)val).Add(_deviceProfile);
		}
		HRESULT val2 = _device.SetWiFiProfileList(val);
		if (((HRESULT)(ref val2)).IsSuccess)
		{
			val2 = _device.SendWiFiProfiles();
		}
		_restore.WaitOne(5000, exitContext: false);
		_device.WiFiProfilesSentEvent -= Device_RestoreDeviceWlanProfilesCompleteEvent;
	}

	private void Device_GetDeviceWlanProfilesCompleteEvent(object sender, FallibleEventArgs args)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_0009: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_001e: Expected O, but got Unknown
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
		HRESULT result = args.HR;
		SetResult(result);
		if (((HRESULT)(ref result)).IsSuccess)
		{
			WlanProfileList list = new WlanProfileList();
			result = _device.GetWiFiProfileList(ref list);
			if (((List<WlanProfile>)(object)list).Count > 0)
			{
				_deviceProfile = ((List<WlanProfile>)(object)list)[0];
			}
			else
			{
				_deviceProfile = null;
			}
		}
		if (((HRESULT)(ref result)).IsSuccess)
		{
			StepComplete(WirelessStateResults.Success);
		}
		else
		{
			StepComplete(WirelessStateResults.Error);
		}
	}

	private void Device_AssociateWlanDeviceCompleteEvent(object sender, FallibleEventArgs args)
	{
		//IL_0003: Unknown result type (might be due to invalid IL or missing references)
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		SetResult(args.HR);
		HRESULT hR = args.HR;
		if (((HRESULT)(ref hR)).IsSuccess)
		{
			StepComplete(WirelessStateResults.Success);
		}
		else
		{
			StepComplete(WirelessStateResults.Error);
		}
	}

	private void Device_SetDeviceWlanProfilesCompleteEvent(object sender, FallibleEventArgs args)
	{
		//IL_0003: Unknown result type (might be due to invalid IL or missing references)
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		SetResult(args.HR);
		HRESULT hR = args.HR;
		if (((HRESULT)(ref hR)).IsSuccess && _attemptingProfile != null)
		{
			_fProfileSaved = true;
			StepComplete(WirelessStateResults.Success);
		}
		else
		{
			StepComplete(WirelessStateResults.Error);
		}
	}

	private void Device_RestoreDeviceWlanProfilesCompleteEvent(object sender, FallibleEventArgs args)
	{
		_restore.Set();
	}

	private void Device_TestDeviceWlanCompleteEvent(object sender, FallibleEventArgs args)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_0008: Unknown result type (might be due to invalid IL or missing references)
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		HRESULT val = args.HR;
		bool flag = HRESULT._NS_E_MTPZ_WLAN_TEST_FAIL_CANCELLED == val;
		SetResult(val);
		if (IsWirelessTestFailure(val))
		{
			_wlanTestResult = _detailedError;
			_wlanTestResultCode = _hr;
			val = HRESULT._S_OK;
			ClearResult();
		}
		if (((HRESULT)(ref val)).IsSuccess && _attemptingProfile != null)
		{
			StepComplete(WirelessStateResults.Success);
		}
		else if (flag)
		{
			StepComplete(WirelessStateResults.Canceled);
		}
		else
		{
			StepComplete(WirelessStateResults.Error);
		}
	}
}
