using Microsoft.Iris;
using Microsoft.Zune.Util;

namespace ZuneUI;

public class MobileWirelessSync : ModelItem
{
	private UIDevice _mobileDevice;

	private string _mediaSyncSSID;

	private string _connectedSSID;

	private int _errorCode = ((HRESULT)(ref HRESULT._S_OK)).Int;

	public string MediaSyncSSID
	{
		get
		{
			//IL_001b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0020: Unknown result type (might be due to invalid IL or missing references)
			//IL_0022: Unknown result type (might be due to invalid IL or missing references)
			//IL_0023: Unknown result type (might be due to invalid IL or missing references)
			if (string.IsNullOrEmpty(_mediaSyncSSID))
			{
				string mediaSyncSSID = string.Empty;
				HRESULT wifiMediaSyncSSID = _mobileDevice.GetWifiMediaSyncSSID(ref mediaSyncSSID);
				_mediaSyncSSID = ((wifiMediaSyncSSID == HRESULT._S_OK) ? mediaSyncSSID : string.Empty);
			}
			return _mediaSyncSSID ?? string.Empty;
		}
		set
		{
			//IL_0015: Unknown result type (might be due to invalid IL or missing references)
			//IL_001a: Unknown result type (might be due to invalid IL or missing references)
			//IL_001b: Unknown result type (might be due to invalid IL or missing references)
			//IL_001c: Unknown result type (might be due to invalid IL or missing references)
			if (value != _mediaSyncSSID)
			{
				HRESULT val = _mobileDevice.SetWifiMediaSyncSSID(value);
				if (val == HRESULT._S_OK)
				{
					_mediaSyncSSID = value;
				}
				else
				{
					_mediaSyncSSID = string.Empty;
					ErrorCode = ((HRESULT)(ref val)).Int;
				}
				((ModelItem)this).FirePropertyChanged("MediaSyncSSID");
			}
		}
	}

	public string ConnectedSSID
	{
		get
		{
			//IL_001b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0020: Unknown result type (might be due to invalid IL or missing references)
			//IL_0021: Unknown result type (might be due to invalid IL or missing references)
			//IL_0022: Unknown result type (might be due to invalid IL or missing references)
			if (string.IsNullOrEmpty(_connectedSSID))
			{
				string connectedSSID = string.Empty;
				HRESULT wifiConnectedSSID = _mobileDevice.GetWifiConnectedSSID(ref connectedSSID);
				if (wifiConnectedSSID == HRESULT._S_OK)
				{
					_connectedSSID = connectedSSID;
				}
				else
				{
					_connectedSSID = string.Empty;
					ErrorCode = ((HRESULT)(ref wifiConnectedSSID)).Int;
				}
			}
			return _connectedSSID ?? string.Empty;
		}
	}

	public bool IsWlanDeviceDisabled
	{
		get
		{
			//IL_000a: Unknown result type (might be due to invalid IL or missing references)
			//IL_000f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0010: Unknown result type (might be due to invalid IL or missing references)
			//IL_0011: Unknown result type (might be due to invalid IL or missing references)
			bool disabled = false;
			HRESULT val = _mobileDevice.IsWlanDeviceDisabled(ref disabled);
			if (val != HRESULT._S_OK)
			{
				ErrorCode = ((HRESULT)(ref val)).Int;
			}
			return disabled;
		}
	}

	public int ErrorCode
	{
		get
		{
			return _errorCode;
		}
		private set
		{
			ShipAssert.Assert(value != ((HRESULT)(ref HRESULT._S_OK)).Int);
			if (_errorCode != value)
			{
				_errorCode = value;
				((ModelItem)this).FirePropertyChanged("ErrorCode");
			}
		}
	}

	public bool WiFiSetupSuccess => true;

	public MobileWirelessSync(UIDevice endpoint)
		: base((IModelItemOwner)(object)ZuneShell.DefaultInstance.Management.DeviceManagement)
	{
		_mobileDevice = endpoint;
	}

	private void ResetErrorCode()
	{
		_errorCode = ((HRESULT)(ref HRESULT._S_OK)).Int;
	}

	public void TestMediaSyncConnection()
	{
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		ResetErrorCode();
		_mobileDevice.WiFiTestCompletedEvent += WiFiTestCompleted;
		HRESULT val = _mobileDevice.TestWiFi();
		if (((HRESULT)(ref val)).IsError)
		{
			_mobileDevice.WiFiTestCompletedEvent -= WiFiTestCompleted;
			ErrorCode = ((HRESULT)(ref val)).Int;
		}
	}

	public void RefreshConnectedNetwork()
	{
		_connectedSSID = string.Empty;
		((ModelItem)this).FirePropertyChanged("ConnectedSSID");
	}

	public void RefreshSyncNetwork()
	{
		_mediaSyncSSID = string.Empty;
		((ModelItem)this).FirePropertyChanged("MediaSyncSSID");
	}

	public void UnassociateNetwork()
	{
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		ResetErrorCode();
		_mobileDevice.WiFiRemovalCompletedEvent += WiFiRemovalCompleted;
		HRESULT val = _mobileDevice.RemoveWiFiAssociation();
		if (((HRESULT)(ref val)).IsError)
		{
			_mobileDevice.WiFiRemovalCompletedEvent -= WiFiRemovalCompleted;
			ErrorCode = ((HRESULT)(ref val)).Int;
		}
	}

	private void WiFiTestCompleted(object sender, FallibleEventArgs args)
	{
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		//IL_0086: Unknown result type (might be due to invalid IL or missing references)
		//IL_008b: Unknown result type (might be due to invalid IL or missing references)
		//IL_004b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		//IL_0052: Unknown result type (might be due to invalid IL or missing references)
		ResetErrorCode();
		_mobileDevice.WiFiTestCompletedEvent -= WiFiTestCompleted;
		HRESULT hR = args.HR;
		if (((HRESULT)(ref hR)).IsSuccess)
		{
			_mobileDevice.WiFiAssociationCompletedEvent += WiFiAssociationCompleted;
			HRESULT val = _mobileDevice.AssociateWiFi();
			if (val != HRESULT._S_OK)
			{
				_mobileDevice.WiFiAssociationCompletedEvent -= WiFiAssociationCompleted;
				ErrorCode = ((HRESULT)(ref val)).Int;
			}
		}
		else
		{
			HRESULT hR2 = args.HR;
			ErrorCode = ((HRESULT)(ref hR2)).Int;
		}
	}

	private void WiFiAssociationCompleted(object sender, FallibleEventArgs args)
	{
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		ResetErrorCode();
		_mobileDevice.WiFiAssociationCompletedEvent -= WiFiAssociationCompleted;
		HRESULT hR = args.HR;
		if (((HRESULT)(ref hR)).IsSuccess)
		{
			MediaSyncSSID = ConnectedSSID;
			((ModelItem)this).FirePropertyChanged("WiFiSetupSuccess");
		}
		else
		{
			HRESULT hR2 = args.HR;
			ErrorCode = ((HRESULT)(ref hR2)).Int;
		}
	}

	private void WiFiRemovalCompleted(object sender, FallibleEventArgs args)
	{
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		ResetErrorCode();
		_mobileDevice.WiFiRemovalCompletedEvent -= WiFiRemovalCompleted;
		HRESULT hR = args.HR;
		if (((HRESULT)(ref hR)).IsSuccess)
		{
			MediaSyncSSID = string.Empty;
			return;
		}
		HRESULT hR2 = args.HR;
		ErrorCode = ((HRESULT)(ref hR2)).Int;
	}
}
