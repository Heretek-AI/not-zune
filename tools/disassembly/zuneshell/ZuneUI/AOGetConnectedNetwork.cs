using System.Collections.Generic;
using MicrosoftZuneLibrary;

namespace ZuneUI;

internal class AOGetConnectedNetwork : AsyncOperation
{
	private WirelessStates[] _getConnectedNetworkStates = new WirelessStates[1] { WirelessStates.GetComputerProfiles };

	private WlanProfileList _computerProfiles;

	private WlanProfile _connectedNetwork;

	public WlanProfile ConnectedNetwork
	{
		get
		{
			if (base.Finished)
			{
				return _connectedNetwork;
			}
			return null;
		}
	}

	private void ResetState()
	{
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_001b: Expected O, but got Unknown
		if (base.Idle || base.Finished)
		{
			_computerProfiles = new WlanProfileList();
			_connectedNetwork = null;
		}
	}

	public WirelessStateResults StartOperation(UIDevice device, AOComplete completeFunc)
	{
		ResetState();
		return StartOperation(device, completeFunc, _getConnectedNetworkStates);
	}

	protected override WirelessStateResults DoStep(WirelessStates state)
	{
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		HRESULT result = ((state != WirelessStates.GetComputerProfiles) ? HRESULT._E_ABORT : _device.LoadComputerWiFiProfiles());
		if (((HRESULT)(ref result)).IsSuccess)
		{
			return WirelessStateResults.Success;
		}
		SetResult(result);
		return WirelessStateResults.Error;
	}

	protected override void EndOperation(WirelessStateResults result)
	{
		_connectedNetwork = null;
		if (_computerProfiles == null)
		{
			return;
		}
		foreach (WlanProfile item in (List<WlanProfile>)(object)_computerProfiles)
		{
			if (item.Connected)
			{
				_connectedNetwork = item;
				break;
			}
		}
	}

	protected override void AddListeners()
	{
		_device.ComputerWiFiProfilesLoadedEvent += Device_GetWlanProfilesCompleteEvent;
	}

	protected override void RemoveListeners()
	{
		_device.ComputerWiFiProfilesLoadedEvent -= Device_GetWlanProfilesCompleteEvent;
	}

	private void Device_GetWlanProfilesCompleteEvent(object sender, FallibleEventArgs args)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_0009: Unknown result type (might be due to invalid IL or missing references)
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		HRESULT result = args.HR;
		SetResult(result);
		if (((HRESULT)(ref result)).IsSuccess)
		{
			result = _device.GetWiFiProfileList(ref _computerProfiles);
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
