using Microsoft.Iris;
using MicrosoftZuneLibrary;
using ZuneXml;

namespace ZuneUI;

public class ProfileCardData : IDatabaseMedia
{
	private LibraryDataProviderListItem _libraryData;

	private XmlDataProviderObject _serviceData;

	private ProfileInterests _profileInterests;

	private bool _isFriend;

	private string _zuneTag;

	private int _badgeCount;

	public bool IsFriend
	{
		get
		{
			return _isFriend;
		}
		set
		{
			_isFriend = value;
		}
	}

	public int BadgeCount
	{
		get
		{
			return _badgeCount;
		}
		set
		{
			_badgeCount = value;
		}
	}

	public string ZuneTag
	{
		get
		{
			if (_zuneTag == null)
			{
				if (LibraryData != null)
				{
					_zuneTag = LibraryData.GetProperty("ZuneTag") as string;
				}
				else if (ServiceData != null)
				{
					_zuneTag = ServiceData.GetProperty("ZuneTag") as string;
				}
			}
			return _zuneTag;
		}
	}

	public DataProviderObject LibraryData => (DataProviderObject)(object)_libraryData;

	public DataProviderObject ServiceData => (DataProviderObject)(object)_serviceData;

	public ProfileInterests ProfileInterests
	{
		get
		{
			if (_profileInterests == null)
			{
				_profileInterests = new ProfileInterests();
			}
			return _profileInterests;
		}
	}

	internal ProfileCardData(LibraryDataProviderListItem libraryData, XmlDataProviderObject serviceData)
	{
		_libraryData = libraryData;
		_serviceData = serviceData;
		_isFriend = false;
		_badgeCount = -1;
	}

	public static ProfileCardData Create(object item1, object item2)
	{
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0013: Expected O, but got Unknown
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0033: Expected O, but got Unknown
		LibraryDataProviderListItem libraryData = null;
		XmlDataProviderObject serviceData = null;
		if (item1 is LibraryDataProviderListItem)
		{
			libraryData = (LibraryDataProviderListItem)item1;
		}
		else if (item1 is XmlDataProviderObject)
		{
			serviceData = (XmlDataProviderObject)item1;
		}
		if (item2 is LibraryDataProviderListItem)
		{
			libraryData = (LibraryDataProviderListItem)item2;
		}
		else if (item2 is XmlDataProviderObject)
		{
			serviceData = (XmlDataProviderObject)item2;
		}
		return new ProfileCardData(libraryData, serviceData);
	}

	public void GetMediaIdAndType(out int mediaId, out EMediaTypes mediaType)
	{
		if (_libraryData != null)
		{
			((LibraryDataProviderItemBase)_libraryData).GetMediaIdAndType(ref mediaId, ref mediaType);
			return;
		}
		mediaId = -1;
		mediaType = (EMediaTypes)(-1);
	}

	public static object GetDataProviderObject(object data)
	{
		object result = null;
		if (data is ProfileCardData)
		{
			ProfileCardData profileCardData = (ProfileCardData)data;
			result = ((profileCardData.ServiceData == null) ? profileCardData.LibraryData : profileCardData.ServiceData);
		}
		else if (data is DataProviderObject)
		{
			result = data;
		}
		return result;
	}

	public static object GetLibraryDataProviderListItem(object data)
	{
		object result = null;
		if (data is ProfileCardData)
		{
			result = ((ProfileCardData)data).LibraryData;
		}
		else if (data is LibraryDataProviderListItem)
		{
			result = data;
		}
		return result;
	}

	public static object GetXmlDataProviderObject(object data)
	{
		object result = null;
		if (data is ProfileCardData)
		{
			result = ((ProfileCardData)data).ServiceData;
		}
		else if (data is XmlDataProviderObject)
		{
			result = data;
		}
		return result;
	}
}
