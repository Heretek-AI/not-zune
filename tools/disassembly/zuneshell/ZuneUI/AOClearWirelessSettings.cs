using Microsoft.Zune.Util;
using MicrosoftZuneLibrary;

namespace ZuneUI;

internal class AOClearWirelessSettings : AsyncOperation
{
	private WirelessStates[] _clearWirelessSettingsStatesMethod1 = new WirelessStates[2]
	{
		WirelessStates.CommitProfileToDevice,
		WirelessStates.UnassociateWlanDevice
	};

	private WirelessStates[] _clearWirelessSettingsStatesMethod2 = new WirelessStates[2]
	{
		WirelessStates.UnassociateWlanDevice,
		WirelessStates.UnassociateNetwork
	};

	private bool _fIgnoreErrors;

	public WirelessStateResults StartOperation(UIDevice device, AOComplete completeFunc, bool fIgnoreErrors)
	{
		ResetState(fIgnoreErrors);
		WirelessStates[] array = null;
		if (device.SupportsWirelessSetupMethod1)
		{
			array = _clearWirelessSettingsStatesMethod1;
		}
		else
		{
			if (!device.SupportsWirelessSetupMethod2)
			{
				ShipAssert.Assert(false);
				return WirelessStateResults.Error;
			}
			array = _clearWirelessSettingsStatesMethod2;
		}
		return StartOperation(device, completeFunc, array);
	}

	protected override WirelessStateResults DoStep(WirelessStates state)
	{
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Expected O, but got Unknown
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		//IL_004b: Unknown result type (might be due to invalid IL or missing references)
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_0059: Unknown result type (might be due to invalid IL or missing references)
		//IL_005e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0085: Unknown result type (might be due to invalid IL or missing references)
		//IL_008a: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ab: Unknown result type (might be due to invalid IL or missing references)
		HRESULT result;
		switch (state)
		{
		case WirelessStates.UnassociateWlanDevice:
			result = _device.RemoveWiFiAssociation();
			break;
		case WirelessStates.CommitProfileToDevice:
			result = _device.SetWiFiProfileList(new WlanProfileList());
			if (((HRESULT)(ref result)).IsSuccess)
			{
				result = _device.SendWiFiProfiles();
			}
			break;
		case WirelessStates.UnassociateNetwork:
			result = _device.SetWifiMediaSyncSSID(string.Empty);
			if (((HRESULT)(ref result)).IsSuccess || _fIgnoreErrors)
			{
				ShipAssert.Assert(((HRESULT)(ref result)).IsSuccess);
				StepComplete(WirelessStateResults.Success);
			}
			break;
		default:
			result = HRESULT._E_ABORT;
			break;
		}
		if (((HRESULT)(ref result)).IsSuccess || _fIgnoreErrors)
		{
			ShipAssert.Assert(((HRESULT)(ref result)).IsSuccess);
			return WirelessStateResults.Success;
		}
		SetResult(result);
		return WirelessStateResults.Error;
	}

	protected override void AddListeners()
	{
		_device.WiFiProfilesSentEvent += Device_SetDeviceWlanProfilesCompleteEvent;
		_device.WiFiRemovalCompletedEvent += Device_UnassociateWlanDeviceCompleteEvent;
	}

	protected override void RemoveListeners()
	{
		_device.WiFiProfilesSentEvent -= Device_SetDeviceWlanProfilesCompleteEvent;
		_device.WiFiRemovalCompletedEvent -= Device_UnassociateWlanDeviceCompleteEvent;
	}

	private void ResetState(bool fIgnoreErrors)
	{
		_fIgnoreErrors = fIgnoreErrors;
	}

	private void Device_SetDeviceWlanProfilesCompleteEvent(object sender, FallibleEventArgs args)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		HRESULT hR = args.HR;
		if (((HRESULT)(ref hR)).IsSuccess || _fIgnoreErrors)
		{
			StepComplete(WirelessStateResults.Success);
			return;
		}
		SetResult(args.HR);
		StepComplete(WirelessStateResults.Error);
	}

	private void Device_UnassociateWlanDeviceCompleteEvent(object sender, FallibleEventArgs args)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		HRESULT hR = args.HR;
		if (((HRESULT)(ref hR)).IsSuccess || _fIgnoreErrors)
		{
			StepComplete(WirelessStateResults.Success);
			return;
		}
		SetResult(args.HR);
		StepComplete(WirelessStateResults.Error);
	}
}
