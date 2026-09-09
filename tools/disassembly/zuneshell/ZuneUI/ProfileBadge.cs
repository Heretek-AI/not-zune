using System.Collections;
using Microsoft.Iris;
using ZuneXml;

namespace ZuneUI;

public class ProfileBadge : NotifyPropertyChangedImpl
{
	public class GroupingComparer : IComparer
	{
		public int Compare(object x, object y)
		{
			int result = -1;
			BadgeData badgeData = null;
			BadgeData badgeData2 = null;
			if (x is ProfileBadge)
			{
				badgeData = ((ProfileBadge)x)._rawData;
			}
			else if (x is BadgeData)
			{
				badgeData = (BadgeData)x;
			}
			if (y is ProfileBadge)
			{
				badgeData2 = ((ProfileBadge)y)._rawData;
			}
			else if (y is BadgeData)
			{
				badgeData2 = (BadgeData)y;
			}
			if (badgeData != null && badgeData2 != null)
			{
				int typeId = badgeData.TypeId;
				int typeId2 = badgeData2.TypeId;
				if (typeId < 0 && typeId2 < 0)
				{
					string type = badgeData.Type;
					string type2 = badgeData2.Type;
					if (type != null && type2 != null)
					{
						result = string.Compare(type, type2);
					}
				}
				else if (typeId == typeId2)
				{
					result = 0;
				}
				else if (typeId > typeId2)
				{
					result = 1;
				}
			}
			return result;
		}
	}

	private BadgeData _rawData;

	private DataProviderObject _mediaData;

	public DataProviderObject RawData => (DataProviderObject)(object)_rawData;

	public DataProviderObject MediaData
	{
		get
		{
			return _mediaData;
		}
		set
		{
			if (_mediaData != value)
			{
				_mediaData = value;
				FirePropertyChanged("MediaData");
			}
		}
	}

	public ProfileBadge(DataProviderObject rawData, DataProviderObject mediaData)
	{
		_rawData = (BadgeData)(object)rawData;
		_mediaData = mediaData;
	}

	public static GroupingComparer CreateGroupingComparer()
	{
		return new GroupingComparer();
	}
}
