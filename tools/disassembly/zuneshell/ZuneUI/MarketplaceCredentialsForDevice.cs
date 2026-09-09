using System;
using Microsoft.Iris;

namespace ZuneUI;

public class MarketplaceCredentialsForDevice : ModelItem
{
	private string _email;

	private string _password;

	private string _zuneTag;

	private Guid _userGuid;

	private bool _purchaseEnabled;

	private bool _newTagSet;

	private Choice _enableMarketplaceChoice;

	private int _hr = ((HRESULT)(ref HRESULT._E_PENDING)).Int;

	public string Email
	{
		get
		{
			return _email;
		}
		set
		{
			_email = value;
		}
	}

	public string Password
	{
		get
		{
			return _password;
		}
		set
		{
			_password = value;
		}
	}

	public string ZuneTag
	{
		get
		{
			return _zuneTag;
		}
		set
		{
			if (_zuneTag != value)
			{
				_zuneTag = value;
				_newTagSet = !string.IsNullOrEmpty(ZuneTag);
				((ModelItem)this).FirePropertyChanged("IsAssociated");
			}
		}
	}

	public Guid UserGuid
	{
		get
		{
			return _userGuid;
		}
		set
		{
			_userGuid = value;
		}
	}

	public bool PurchaseEnabled
	{
		get
		{
			return _purchaseEnabled;
		}
		set
		{
			if (_purchaseEnabled != value)
			{
				_purchaseEnabled = value;
				((ModelItem)this).FirePropertyChanged("PurchaseEnabled");
			}
		}
	}

	public bool IsAssociated
	{
		get
		{
			if (_enableMarketplaceChoice.ChosenIndex != 0)
			{
				return _newTagSet;
			}
			return false;
		}
	}

	public int hr
	{
		get
		{
			return _hr;
		}
		set
		{
			_hr = value;
		}
	}

	public MarketplaceCredentialsForDevice(string email, string password, bool purchaseEnabled, bool alreadyAssociated, string zuneTag, Choice enableMarketplaceChoice)
	{
		_email = email;
		_password = password;
		_purchaseEnabled = purchaseEnabled;
		_zuneTag = zuneTag;
		_enableMarketplaceChoice = enableMarketplaceChoice;
		_newTagSet = alreadyAssociated;
	}
}
