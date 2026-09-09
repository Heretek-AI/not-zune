using System.Collections.Generic;
using MicrosoftZuneLibrary;

namespace ZuneUI;

internal class AOGetNetworkList : AsyncOperation
{
	private WlanProfileList _deviceNetworks;

	private WlanProfileList _computerNetworks;

	private WlanProfileList _networkList;

	private WirelessStates[] _getNetworkListStates = new WirelessStates[2]
	{
		WirelessStates.GetComputerProfiles,
		WirelessStates.SniffNetworks
	};

	public WlanProfileList NetworkList
	{
		get
		{
			if (base.Finished)
			{
				return _networkList;
			}
			return null;
		}
	}

	private void ResetState()
	{
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_001b: Expected O, but got Unknown
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Expected O, but got Unknown
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Expected O, but got Unknown
		if (base.Idle || base.Finished)
		{
			_computerNetworks = new WlanProfileList();
			_deviceNetworks = new WlanProfileList();
			_networkList = new WlanProfileList();
		}
	}

	public WirelessStateResults StartOperation(UIDevice device, AOComplete completeFunc)
	{
		ResetState();
		return StartOperation(device, completeFunc, _getNetworkListStates);
	}

	protected override WirelessStateResults DoStep(WirelessStates state)
	{
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		HRESULT result = (HRESULT)(state switch
		{
			WirelessStates.GetComputerProfiles => _device.LoadComputerWiFiProfiles(), 
			WirelessStates.SniffNetworks => _device.ScanForWiFiNetworks(), 
			_ => HRESULT._E_ABORT, 
		});
		if (((HRESULT)(ref result)).IsSuccess)
		{
			return WirelessStateResults.Success;
		}
		SetResult(result);
		return WirelessStateResults.Error;
	}

	protected override void EndOperation(WirelessStateResults result)
	{
		Dictionary<string, WlanProfile> dictionary = new Dictionary<string, WlanProfile>();
		if (_deviceNetworks != null)
		{
			foreach (WlanProfile item in (List<WlanProfile>)(object)_deviceNetworks)
			{
				if (dictionary.ContainsKey(item.SSID))
				{
					continue;
				}
				WlanProfile val = item;
				foreach (WlanProfile item2 in (List<WlanProfile>)(object)_computerNetworks)
				{
					if (item2.SSID == item.SSID)
					{
						val = item2;
						break;
					}
				}
				if (!val.Connected)
				{
					val.Key = string.Empty;
				}
				dictionary.Add(val.SSID, val);
				((List<WlanProfile>)(object)_networkList).Add(val);
			}
		}
		((List<WlanProfile>)(object)_networkList).Sort((IComparer<WlanProfile>?)new WlanSignalStrenghComparer());
	}

	protected override void AddListeners()
	{
		_device.WiFiScanCompletedEvent += Device_GetDeviceWlanNetworksCompleteEvent;
		_device.ComputerWiFiProfilesLoadedEvent += Device_GetWlanProfilesCompleteEvent;
	}

	protected override void RemoveListeners()
	{
		_device.WiFiScanCompletedEvent -= Device_GetDeviceWlanNetworksCompleteEvent;
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
			result = _device.GetWiFiProfileList(ref _computerNetworks);
		}
		StepComplete(WirelessStateResults.Success);
	}

	private void Device_GetDeviceWlanNetworksCompleteEvent(object sender, FallibleEventArgs args)
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
			result = _device.GetWiFiProfileList(ref _deviceNetworks);
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
