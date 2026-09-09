using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Microsoft.Iris;
using MicrosoftZuneLibrary;
using UIXControls;

namespace ZuneUI;

public class WirelessSync : ModelItem
{
	private const int NS_E_INVALID_TRANSCODE_CACHE_SIZE = -1072876833;

	private const uint NS_E_WLAN_PROFILE_START = 3222082080u;

	private const uint NS_E_WLAN_PROFILE_END = 3222082085u;

	private static WirelessSync _singletonInstance;

	private Choice _existingNetworkChoice;

	private Choice wirelessNetworkTypesChoice;

	private AOSetProfile wirelessSetProfileHelper = new AOSetProfile();

	private AOGetNetworkList wirelessGetNetworkListHelper = new AOGetNetworkList();

	private AOGetConnectedNetwork wirelessGetConnectedNetworkHelper = new AOGetConnectedNetwork();

	private AOClearWirelessSettings wirelessClearHelper = new AOClearWirelessSettings();

	private AOGetDeviceProfile wirelessDeviceProfileHelper = new AOGetDeviceProfile();

	private string wirelessDeviceErrorDescription;

	private string wirelessDeviceErrorCaption;

	private HRESULT wirelessDeviceErrorCode = HRESULT._S_OK;

	private bool wirelessDeviceCanceled;

	private Category wirelessBlockedPage;

	private WlanProfile wirelessDeviceProfile;

	private WlanProfile wirelessConnectedProfile;

	private WlanProfile wirelessProfileToSave;

	private ArrayListDataSet wirelessNetworksList;

	private HRESULT wirelessGetConnectedProfileResult = HRESULT._S_OK;

	private HRESULT _profileSavedResult = HRESULT._E_PENDING;

	private static readonly WirelessType[] wirelessRadioGroupItems = new WirelessType[11]
	{
		new WirelessType(new WlanAuthCipherPair((WirelessAuthenticationTypes)1, (WirelessCiphers)0), Shell.LoadString(StringId.IDS_WIRELESS_NETWORK_NO_ENCRYPTION), displayType: true, alwaysSupported: true, newGroup: true),
		new WirelessType(new WlanAuthCipherPair((WirelessAuthenticationTypes)1, (WirelessCiphers)257), Shell.LoadString(StringId.IDS_WIRELESS_NETWORK_WEP_OPEN), displayType: true, alwaysSupported: false, newGroup: true),
		new WirelessType(new WlanAuthCipherPair((WirelessAuthenticationTypes)1, (WirelessCiphers)1), Shell.LoadString(StringId.IDS_WIRELESS_NETWORK_WEP_OPEN), displayType: false, alwaysSupported: false, newGroup: true),
		new WirelessType(new WlanAuthCipherPair((WirelessAuthenticationTypes)1, (WirelessCiphers)5), Shell.LoadString(StringId.IDS_WIRELESS_NETWORK_WEP_OPEN), displayType: false, alwaysSupported: false, newGroup: true),
		new WirelessType(new WlanAuthCipherPair((WirelessAuthenticationTypes)2, (WirelessCiphers)257), Shell.LoadString(StringId.IDS_WIRELESS_NETWORK_WEP_SHARED), displayType: true, alwaysSupported: false, newGroup: false),
		new WirelessType(new WlanAuthCipherPair((WirelessAuthenticationTypes)2, (WirelessCiphers)1), Shell.LoadString(StringId.IDS_WIRELESS_NETWORK_WEP_SHARED), displayType: false, alwaysSupported: false, newGroup: true),
		new WirelessType(new WlanAuthCipherPair((WirelessAuthenticationTypes)2, (WirelessCiphers)5), Shell.LoadString(StringId.IDS_WIRELESS_NETWORK_WEP_SHARED), displayType: false, alwaysSupported: false, newGroup: true),
		new WirelessType(new WlanAuthCipherPair((WirelessAuthenticationTypes)4, (WirelessCiphers)4), Shell.LoadString(StringId.IDS_WIRELESS_NETWORK_WPA_AES), displayType: true, alwaysSupported: false, newGroup: true),
		new WirelessType(new WlanAuthCipherPair((WirelessAuthenticationTypes)4, (WirelessCiphers)2), Shell.LoadString(StringId.IDS_WIRELESS_NETWORK_WPA_TKIP), displayType: true, alwaysSupported: false, newGroup: false),
		new WirelessType(new WlanAuthCipherPair((WirelessAuthenticationTypes)7, (WirelessCiphers)4), Shell.LoadString(StringId.IDS_WIRELESS_NETWORK_WPA2_AES), displayType: true, alwaysSupported: false, newGroup: true),
		new WirelessType(new WlanAuthCipherPair((WirelessAuthenticationTypes)7, (WirelessCiphers)2), Shell.LoadString(StringId.IDS_WIRELESS_NETWORK_WPA2_TKIP), displayType: true, alwaysSupported: false, newGroup: false)
	};

	public static WirelessSync Instance
	{
		get
		{
			if (_singletonInstance == null)
			{
				_singletonInstance = new WirelessSync();
			}
			return _singletonInstance;
		}
		set
		{
			if (_singletonInstance != value)
			{
				_singletonInstance = value;
			}
		}
	}

	private UIDevice ActiveDevice => SyncControls.Instance.CurrentDeviceOverride;

	public Choice ExistingNetworkChoice
	{
		get
		{
			//IL_000c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0016: Expected O, but got Unknown
			//IL_0031: Unknown result type (might be due to invalid IL or missing references)
			//IL_0037: Expected O, but got Unknown
			//IL_0045: Unknown result type (might be due to invalid IL or missing references)
			//IL_004b: Expected O, but got Unknown
			if (_existingNetworkChoice == null)
			{
				_existingNetworkChoice = new Choice((IModelItemOwner)(object)this);
				_existingNetworkChoice.Options = new Command[2]
				{
					new Command((IModelItemOwner)(object)this, Shell.LoadString(StringId.IDS_WIRELESS_USE_CONNECTED_YES), (EventHandler)null),
					new Command((IModelItemOwner)(object)this, Shell.LoadString(StringId.IDS_WIRELESS_USE_CONNECTED_NO), (EventHandler)null)
				};
				_existingNetworkChoice.ChosenChanged += delegate
				{
					((ModelItem)this).FirePropertyChanged("ExistingNetworkChoice");
				};
			}
			return _existingNetworkChoice;
		}
	}

	public WlanProfile WirelessDeviceProfile
	{
		get
		{
			//IL_0009: Unknown result type (might be due to invalid IL or missing references)
			//IL_0013: Expected O, but got Unknown
			if (wirelessDeviceProfile == null)
			{
				wirelessDeviceProfile = new WlanProfile();
				wirelessDeviceProfile.SSID = string.Empty;
				wirelessDeviceProfile.Key = string.Empty;
				wirelessDeviceProfile.Auth = (WirelessAuthenticationTypes)1;
				wirelessDeviceProfile.Cipher = (WirelessCiphers)0;
			}
			return wirelessDeviceProfile;
		}
		private set
		{
			wirelessDeviceProfile = value;
			((ModelItem)this).FirePropertyChanged("WirelessDeviceProfile");
		}
	}

	public ArrayListDataSet WirelessNetworksList
	{
		get
		{
			//IL_0009: Unknown result type (might be due to invalid IL or missing references)
			//IL_0013: Expected O, but got Unknown
			if (wirelessNetworksList == null)
			{
				wirelessNetworksList = new ArrayListDataSet();
			}
			return wirelessNetworksList;
		}
		private set
		{
			if (value != wirelessNetworksList)
			{
				wirelessNetworksList = value;
				((ModelItem)this).FirePropertyChanged("WirelessNetworksList");
			}
		}
	}

	public HRESULT ProfileSavedResult
	{
		get
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			return _profileSavedResult;
		}
		set
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			//IL_0006: Unknown result type (might be due to invalid IL or missing references)
			//IL_000f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0010: Unknown result type (might be due to invalid IL or missing references)
			if (_profileSavedResult != value)
			{
				_profileSavedResult = value;
				((ModelItem)this).FirePropertyChanged("ProfileSavedResult");
			}
		}
	}

	public string WirelessDeviceErrorDescription => wirelessDeviceErrorDescription;

	public bool WirelessDeviceShowError => wirelessDeviceErrorCode != HRESULT._S_OK;

	public bool WirelessDeviceShowErrorNow
	{
		get
		{
			if ((uint)((HRESULT)(ref wirelessDeviceErrorCode)).Int >= 3222082080u && (uint)((HRESULT)(ref wirelessDeviceErrorCode)).Int <= 3222082085u)
			{
				return true;
			}
			return false;
		}
	}

	public bool WirelessDeviceCanceled => wirelessDeviceCanceled;

	public Choice WirelessNetworkTypes
	{
		get
		{
			//IL_0000: Unknown result type (might be due to invalid IL or missing references)
			//IL_0006: Expected O, but got Unknown
			//IL_0021: Unknown result type (might be due to invalid IL or missing references)
			//IL_0083: Unknown result type (might be due to invalid IL or missing references)
			//IL_008d: Expected O, but got Unknown
			WlanAuthCipherPairList list = new WlanAuthCipherPairList();
			IList<WirelessNetworkTypeCommand> list2 = new List<WirelessNetworkTypeCommand>();
			if (ActiveDevice.IsConnectedToClient)
			{
				ActiveDevice.GetWiFiAuthorizationCipherList(ref list);
			}
			WirelessType[] array = wirelessRadioGroupItems;
			foreach (WirelessType wirelessType in array)
			{
				if (wirelessType.DisplayType && (wirelessType.AlwaysSupported || DeviceSupportsType(list, wirelessType.Type)))
				{
					WirelessNetworkTypeCommand item = new WirelessNetworkTypeCommand((IModelItemOwner)(object)this, wirelessType.Description, null, wirelessType.Type);
					list2.Add(item);
				}
			}
			wirelessNetworkTypesChoice = new Choice((IModelItemOwner)(object)this);
			wirelessNetworkTypesChoice.Options = (IList)list2;
			return wirelessNetworkTypesChoice;
		}
	}

	public string ConnectedWirelessNetwork
	{
		get
		{
			if (wirelessConnectedProfile != null)
			{
				return wirelessConnectedProfile.SSID;
			}
			return null;
		}
	}

	public bool WirelessGetConnectedProfileFailed => wirelessGetConnectedProfileResult != HRESULT._S_OK;

	public string WirelessGetConnectedProfileResult => ((HRESULT)(ref wirelessGetConnectedProfileResult)).Int.ToString("X");

	public string SavingProfileName => wirelessSetProfileHelper.AttemptingName;

	private WirelessSync()
		: base((IModelItemOwner)(object)ZuneShell.DefaultInstance.Management.DeviceManagement)
	{
	}//IL_0038: Unknown result type (might be due to invalid IL or missing references)
	//IL_003d: Unknown result type (might be due to invalid IL or missing references)
	//IL_0043: Unknown result type (might be due to invalid IL or missing references)
	//IL_0048: Unknown result type (might be due to invalid IL or missing references)
	//IL_004e: Unknown result type (might be due to invalid IL or missing references)
	//IL_0053: Unknown result type (might be due to invalid IL or missing references)


	public string GetWirelessDeviceNetworkType()
	{
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		WlanProfile val = WirelessDeviceProfile;
		string result = Shell.LoadString(StringId.IDS_TYPE_UNKNOWN);
		WirelessType[] array = wirelessRadioGroupItems;
		foreach (WirelessType wirelessType in array)
		{
			if (val.Auth == wirelessType.Type.Auth && val.Cipher == wirelessType.Type.Cipher)
			{
				result = wirelessType.Description;
			}
		}
		return result;
	}

	public int GetNetworkTypeIndex(WirelessAuthenticationTypes auth, WirelessCiphers cipher)
	{
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		//IL_004b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_0062: Unknown result type (might be due to invalid IL or missing references)
		int result = 0;
		if (wirelessNetworkTypesChoice != null && wirelessNetworkTypesChoice.Options != null)
		{
			for (int i = 0; i < wirelessNetworkTypesChoice.Options.Count; i++)
			{
				WirelessNetworkTypeCommand wirelessNetworkTypeCommand = wirelessNetworkTypesChoice.Options[i] as WirelessNetworkTypeCommand;
				WlanAuthCipherPair val = null;
				if (wirelessNetworkTypeCommand != null)
				{
					val = wirelessNetworkTypeCommand.NetworkType;
				}
				if (val != null && val.Auth == auth && (val.Cipher == cipher || (IsWEP(val.Cipher) && IsWEP(cipher))))
				{
					result = i;
					break;
				}
			}
		}
		return result;
	}

	public WirelessStateResults RequestWirelessNetworksList()
	{
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		wirelessDeviceErrorDescription = null;
		wirelessDeviceErrorCaption = Shell.LoadString(StringId.IDS_WIRELESS_SNIFF_FAILED);
		wirelessDeviceErrorCode = HRESULT._S_OK;
		WirelessStateResults result = wirelessGetNetworkListHelper.StartOperation(ActiveDevice, GetNetworkListDone);
		WirelessHandleDeviceBusy(result);
		return result;
	}

	public void GetNetworkListDone(bool success)
	{
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_001b: Expected O, but got Unknown
		//IL_007a: Unknown result type (might be due to invalid IL or missing references)
		//IL_007f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0092: Unknown result type (might be due to invalid IL or missing references)
		//IL_0097: Unknown result type (might be due to invalid IL or missing references)
		if (WirelessUnblockPage())
		{
			return;
		}
		WlanProfileList networkList = wirelessGetNetworkListHelper.NetworkList;
		ArrayListDataSet val = new ArrayListDataSet();
		if (success && networkList != null)
		{
			foreach (WlanProfile item in (List<WlanProfile>)(object)networkList)
			{
				((ListDataSet)val).Add((object)new WlanCommand(item));
			}
		}
		if (!success)
		{
			if (string.IsNullOrEmpty(wirelessDeviceErrorDescription))
			{
				wirelessDeviceErrorDescription = wirelessGetNetworkListHelper.Error;
			}
			if (wirelessDeviceErrorCode == HRESULT._S_OK)
			{
				wirelessDeviceErrorCode = wirelessGetNetworkListHelper.Hr;
			}
		}
		WirelessNetworksList = val;
	}

	public bool SetConnectedNetwork()
	{
		return SetWirelessSettings(wirelessConnectedProfile);
	}

	public WirelessStateResults SetWirelessSettings()
	{
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		WirelessStateResults wirelessStateResults = WirelessStateResults.Error;
		WlanProfile val = wirelessProfileToSave;
		wirelessDeviceErrorDescription = null;
		wirelessDeviceErrorCaption = Shell.LoadString(StringId.IDS_WIRELESS_SYNC_POST_SETUP_FAILED);
		wirelessDeviceErrorCode = HRESULT._S_OK;
		wirelessDeviceCanceled = false;
		if (val != null)
		{
			wirelessStateResults = wirelessSetProfileHelper.StartOperation(ActiveDevice, SetProfileDone, val);
			WirelessHandleDeviceBusy(wirelessStateResults);
		}
		if (wirelessStateResults != WirelessStateResults.Success)
		{
			if (val != null)
			{
				wirelessDeviceErrorDescription = string.Format(Shell.LoadString(StringId.IDS_WIRELESS_SYNC_SETUP_FAILED), val.SSID);
			}
			else
			{
				wirelessDeviceErrorDescription = Shell.LoadString(StringId.IDS_WIRELESS_SYNC_SETUP_FAILED_GENERIC);
			}
		}
		return wirelessStateResults;
	}

	public bool SetWirelessSettings(object selected)
	{
		if (selected is WlanCommand wlanCommand)
		{
			return SetWirelessSettings(wlanCommand.Profile);
		}
		wirelessDeviceErrorDescription = Shell.LoadString(StringId.IDS_WIRELESS_SYNC_SETUP_FAILED_GENERIC);
		return false;
	}

	public WlanCommand CreateWlanCommand(string name, object networkType, string key)
	{
		WlanProfile val = CreateWlanProfile(name, networkType, key);
		if (val != null)
		{
			return new WlanCommand(val);
		}
		return null;
	}

	public void ClearWirelessOnDevice()
	{
		RequestClearWirelessOnDevice(ClearWirelessOnDeviceDone, fIgnoreErrors: true);
	}

	public void ClearWirelessOnDeviceDone(bool success)
	{
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		if (!WirelessUnblockPage() && !success)
		{
			if (string.IsNullOrEmpty(wirelessDeviceErrorDescription))
			{
				wirelessDeviceErrorDescription = wirelessClearHelper.Error;
			}
			if (wirelessDeviceErrorCode == HRESULT._S_OK)
			{
				wirelessDeviceErrorCode = wirelessClearHelper.Hr;
			}
		}
	}

	public void ClearWirelessOnDeviceForForget()
	{
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0064: Unknown result type (might be due to invalid IL or missing references)
		//IL_0069: Unknown result type (might be due to invalid IL or missing references)
		if (ActiveDevice.IsConnectedToClient && !ActiveDevice.IsGuest)
		{
			RequestClearWirelessOnDevice(ClearWirelessOnDeviceForForgetDone, fIgnoreErrors: true);
			return;
		}
		string uuid = null;
		HRESULT val = ActiveDevice.GetDisconnectedWiFiUUID(ref uuid);
		if (((HRESULT)(ref val)).IsError)
		{
			ClearWirelessOnDeviceForForgetDone(((HRESULT)(ref val)).IsSuccess);
		}
		else if (!string.IsNullOrEmpty(uuid))
		{
			val = ActiveDevice.UnassociateWiFiUUID(uuid);
			ClearWirelessOnDeviceForForgetDone(((HRESULT)(ref val)).IsSuccess);
		}
		else
		{
			ClearWirelessOnDeviceForForgetDone(success: true);
		}
	}

	public void ClearWirelessOnDeviceForForgetDone(bool success)
	{
		if (success)
		{
			SyncControls.Instance.DeleteCurrentDeviceWorker();
		}
		else
		{
			MessageBox.Show(Shell.LoadString(StringId.IDS_WIRELESS_CLEAR_UUID_FAILED_TITLE), Shell.LoadString(StringId.IDS_WIRELESS_CLEAR_UUID_FAILED), (EventHandler)null);
		}
		WirelessUnblockPage();
	}

	private void RequestClearWirelessOnDevice(AsyncOperation.AOComplete completeFunc, bool fIgnoreErrors)
	{
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		WirelessStateResults wirelessStateResults = WirelessStateResults.Error;
		wirelessDeviceErrorDescription = null;
		wirelessDeviceErrorCaption = Shell.LoadString(StringId.IDS_WIRELESS_SYNC_PRE_SETUP_FAILED);
		wirelessDeviceErrorCode = HRESULT._S_OK;
		do
		{
			wirelessStateResults = wirelessClearHelper.StartOperation(ActiveDevice, completeFunc, fIgnoreErrors);
			if (wirelessStateResults == WirelessStateResults.NotAvailable)
			{
				Thread.Sleep(200);
			}
		}
		while (wirelessStateResults == WirelessStateResults.NotAvailable);
	}

	public void SetProfileDone(bool success)
	{
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_005e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0063: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_0098: Unknown result type (might be due to invalid IL or missing references)
		//IL_0076: Unknown result type (might be due to invalid IL or missing references)
		//IL_007b: Unknown result type (might be due to invalid IL or missing references)
		if (WirelessUnblockPage())
		{
			return;
		}
		if (success)
		{
			WirelessDeviceProfile = wirelessProfileToSave;
		}
		_profileSavedResult = HRESULT._E_UNEXPECTED;
		if (success && wirelessSetProfileHelper.WlanTestSucceeded)
		{
			ProfileSavedResult = HRESULT._S_OK;
			return;
		}
		if (string.IsNullOrEmpty(wirelessDeviceErrorDescription))
		{
			wirelessDeviceErrorDescription = wirelessSetProfileHelper.Error;
		}
		if (wirelessDeviceErrorCode == HRESULT._S_OK)
		{
			wirelessDeviceErrorCode = wirelessSetProfileHelper.Hr;
		}
		wirelessDeviceCanceled = wirelessSetProfileHelper.Canceled;
		ProfileSavedResult = wirelessSetProfileHelper.Hr;
	}

	public void WirelessDeviceShowErrorDialog()
	{
		if (WirelessDeviceShowError)
		{
			if (string.IsNullOrEmpty(wirelessDeviceErrorCaption))
			{
				Shell.ShowErrorDialog(((HRESULT)(ref wirelessDeviceErrorCode)).Int, StringId.IDS_WIRELESS_SYNC_GENERIC_SETUP_FAILED);
			}
			else
			{
				ErrorDialogInfo.Show(((HRESULT)(ref wirelessDeviceErrorCode)).Int, wirelessDeviceErrorCaption);
			}
		}
	}

	public void CancelSetWirelessSettings()
	{
		wirelessSetProfileHelper.Cancel();
	}

	public WirelessStateResults RequestDeviceWirelessProfile()
	{
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		wirelessDeviceErrorDescription = null;
		wirelessDeviceErrorCaption = Shell.LoadString(StringId.IDS_WIRELESS_SYNC_WLAN_UUID_FAILED);
		wirelessDeviceErrorCode = HRESULT._S_OK;
		WirelessStateResults result = wirelessDeviceProfileHelper.StartOperation(ActiveDevice, GetDeviceWirelessProfileDone);
		WirelessHandleDeviceBusy(result);
		return result;
	}

	public void GetDeviceWirelessProfileDone(bool success)
	{
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0061: Unknown result type (might be due to invalid IL or missing references)
		if (WirelessUnblockPage())
		{
			return;
		}
		if (success)
		{
			WirelessDeviceProfile = wirelessDeviceProfileHelper.DeviceProfile;
			return;
		}
		WirelessDeviceProfile = null;
		if (string.IsNullOrEmpty(wirelessDeviceErrorDescription))
		{
			wirelessDeviceErrorDescription = wirelessDeviceProfileHelper.Error;
		}
		if (wirelessDeviceErrorCode == HRESULT._S_OK)
		{
			wirelessDeviceErrorCode = wirelessDeviceProfileHelper.Hr;
		}
	}

	public WirelessStateResults RequestConnectedWirelessNetwork()
	{
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		wirelessDeviceErrorDescription = null;
		wirelessDeviceErrorCaption = Shell.LoadString(StringId.IDS_WIRELESS_SYNC_PRE_SETUP_FAILED);
		wirelessDeviceErrorCode = HRESULT._S_OK;
		wirelessGetConnectedProfileResult = HRESULT._S_OK;
		WirelessStateResults result = wirelessGetConnectedNetworkHelper.StartOperation(ActiveDevice, GetConnectedNetworkDone);
		WirelessHandleDeviceBusy(result);
		return result;
	}

	public void GetConnectedNetworkDone(bool success)
	{
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0070: Unknown result type (might be due to invalid IL or missing references)
		//IL_0075: Unknown result type (might be due to invalid IL or missing references)
		if (WirelessUnblockPage())
		{
			return;
		}
		WlanProfile connectedNetwork = wirelessGetConnectedNetworkHelper.ConnectedNetwork;
		if (success)
		{
			wirelessConnectedProfile = connectedNetwork;
		}
		else
		{
			wirelessGetConnectedProfileResult = wirelessGetConnectedNetworkHelper.Hr;
			wirelessConnectedProfile = null;
			if (string.IsNullOrEmpty(wirelessDeviceErrorDescription))
			{
				wirelessDeviceErrorDescription = wirelessGetConnectedNetworkHelper.Error;
			}
			if (wirelessDeviceErrorCode == HRESULT._S_OK)
			{
				wirelessDeviceErrorCode = wirelessGetConnectedNetworkHelper.Hr;
			}
		}
		((ModelItem)this).FirePropertyChanged("ConnectedWirelessNetwork");
	}

	private bool SetWirelessSettings(WlanProfile profile)
	{
		if (profile != null)
		{
			wirelessProfileToSave = profile;
			return true;
		}
		return false;
	}

	private WlanProfile CreateWlanProfile(string name, object networkType, string key)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Expected O, but got Unknown
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		//IL_004c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f1: Expected O, but got Unknown
		//IL_0101: Unknown result type (might be due to invalid IL or missing references)
		//IL_0113: Unknown result type (might be due to invalid IL or missing references)
		//IL_006b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0076: Unknown result type (might be due to invalid IL or missing references)
		//IL_007f: Unknown result type (might be due to invalid IL or missing references)
		//IL_008a: Unknown result type (might be due to invalid IL or missing references)
		WlanAuthCipherPairList list = new WlanAuthCipherPairList();
		WirelessNetworkTypeCommand wirelessNetworkTypeCommand = networkType as WirelessNetworkTypeCommand;
		bool flag = false;
		WlanProfile val = null;
		if (string.IsNullOrEmpty(name) || wirelessNetworkTypeCommand == null || wirelessNetworkTypeCommand.NetworkType == null || ActiveDevice == null)
		{
			wirelessDeviceErrorDescription = Shell.LoadString(StringId.IDS_WIRELESS_SYNC_SETUP_FAILED_GENERIC);
			return null;
		}
		HRESULT wiFiAuthorizationCipherList = ActiveDevice.GetWiFiAuthorizationCipherList(ref list);
		if (((HRESULT)(ref wiFiAuthorizationCipherList)).IsSuccess)
		{
			foreach (WlanAuthCipherPair item in (List<WlanAuthCipherPair>)(object)list)
			{
				if (item.Auth == wirelessNetworkTypeCommand.NetworkType.Auth && item.Cipher == wirelessNetworkTypeCommand.NetworkType.Cipher)
				{
					flag = true;
					break;
				}
			}
		}
		if (!flag)
		{
			wirelessDeviceErrorDescription = string.Format(Shell.LoadString(StringId.IDS_WIRELESS_ERROR_UNSUPPORTED_AUTH), name);
			wirelessDeviceErrorCode = wiFiAuthorizationCipherList;
			return null;
		}
		if (string.IsNullOrEmpty(name))
		{
			wirelessDeviceErrorDescription = Shell.LoadString(StringId.IDS_WIRELESS_ERROR_INVALID_NAME);
			return null;
		}
		val = new WlanProfile();
		val.SSID = name;
		val.Auth = wirelessNetworkTypeCommand.NetworkType.Auth;
		val.Cipher = wirelessNetworkTypeCommand.NetworkType.Cipher;
		val.Key = key;
		return val;
	}

	private bool IsWEP(WirelessCiphers cipher)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Invalid comparison between Unknown and I4
		//IL_0008: Unknown result type (might be due to invalid IL or missing references)
		//IL_000a: Invalid comparison between Unknown and I4
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_000e: Invalid comparison between Unknown and I4
		if ((int)cipher == 257 || (int)cipher == 1 || (int)cipher == 5)
		{
			return true;
		}
		return false;
	}

	private bool IsWPA(WirelessAuthenticationTypes auth)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0002: Invalid comparison between Unknown and I4
		//IL_0004: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Invalid comparison between Unknown and I4
		if ((int)auth == 7 || (int)auth == 4)
		{
			return true;
		}
		return false;
	}

	private bool DeviceSupportsType(WlanAuthCipherPairList supportedList, WlanAuthCipherPair displayType)
	{
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Invalid comparison between Unknown and I4
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		foreach (WlanAuthCipherPair item in (List<WlanAuthCipherPair>)(object)supportedList)
		{
			if ((int)displayType.Cipher == 257)
			{
				if (item.Auth == displayType.Auth && IsWEP(item.Cipher))
				{
					return true;
				}
			}
			else if (item.Auth == displayType.Auth && item.Cipher == displayType.Cipher)
			{
				return true;
			}
		}
		return false;
	}

	private void WirelessHandleDeviceBusy(WirelessStateResults result)
	{
		if (result == WirelessStateResults.NotAvailable)
		{
			wirelessBlockedPage = ZuneShell.DefaultInstance.Management.CurrentCategoryPage.CurrentCategory;
			Management.NavigateToCategory(SettingCategories.WirelessSetupDeviceBusy);
		}
	}

	private bool WirelessUnblockPage()
	{
		if (wirelessBlockedPage != null)
		{
			Management.NavigateToCategory(wirelessBlockedPage);
			wirelessBlockedPage = null;
			return true;
		}
		return false;
	}
}
