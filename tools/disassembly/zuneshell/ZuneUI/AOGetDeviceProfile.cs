using System.Collections.Generic;
using MicrosoftZuneLibrary;

namespace ZuneUI;

internal class AOGetDeviceProfile : AsyncOperation
{
	private WirelessStates[] _getDeviceProfileStates = new WirelessStates[1] { WirelessStates.GetDeviceProfiles };

	private WlanProfile _deviceProfile;

	public WlanProfile DeviceProfile
	{
		get
		{
			if (base.Finished)
			{
				return _deviceProfile;
			}
			return null;
		}
	}

	public WirelessStateResults StartOperation(UIDevice device, AOComplete completeFunc)
	{
		ResetState();
		return StartOperation(device, completeFunc, _getDeviceProfileStates);
	}

	protected override WirelessStateResults DoStep(WirelessStates state)
	{
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		HRESULT result = ((state != WirelessStates.GetDeviceProfiles) ? HRESULT._E_ABORT : _device.ReceiveWiFiProfiles());
		if (((HRESULT)(ref result)).IsSuccess)
		{
			return WirelessStateResults.Success;
		}
		SetResult(result);
		return WirelessStateResults.Error;
	}

	protected override void AddListeners()
	{
		_device.WiFiProfilesReceivedEvent += Device_GetDeviceWlanProfilesCompleteEvent;
	}

	protected override void RemoveListeners()
	{
		_device.WiFiProfilesReceivedEvent -= Device_GetDeviceWlanProfilesCompleteEvent;
	}

	private void ResetState()
	{
		_deviceProfile = null;
	}

	private void Device_GetDeviceWlanProfilesCompleteEvent(object sender, FallibleEventArgs args)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Expected O, but got Unknown
		//IL_0008: Unknown result type (might be due to invalid IL or missing references)
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
		WlanProfileList list = new WlanProfileList();
		HRESULT result = args.HR;
		SetResult(result);
		if (((HRESULT)(ref result)).IsSuccess)
		{
			result = _device.GetWiFiProfileList(ref list);
			if (((HRESULT)(ref result)).IsSuccess && ((List<WlanProfile>)(object)list).Count > 0)
			{
				_deviceProfile = ((List<WlanProfile>)(object)list)[0];
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
}
