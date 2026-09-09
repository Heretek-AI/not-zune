using System.Collections;
using Microsoft.Iris;
using Microsoft.Zune.Playlist;

namespace ZuneUI;

public class PlaylistTypeFilterList : FilterList
{
	private IList _typesToInclude = AllTypes;

	private static int[] _allTypes = new int[0];

	public IList TypesToInclude
	{
		get
		{
			return _typesToInclude;
		}
		set
		{
			if (_typesToInclude != value)
			{
				_typesToInclude = value;
				((ModelItem)this).FirePropertyChanged("TypesToInclude");
				ProduceFilteredList();
			}
		}
	}

	public static IList AllTypes => _allTypes;

	protected override bool ShouldIncludeItem(int sourceIndex, int targetIndex, object item)
	{
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		if (TypesToInclude == AllTypes)
		{
			return true;
		}
		if (TypesToInclude == null || TypesToInclude.Count <= 0)
		{
			return false;
		}
		DataProviderObject val = (DataProviderObject)((item is DataProviderObject) ? item : null);
		if (val != null)
		{
			object property = val.GetProperty("PlaylistType");
			if (property != null && property is int)
			{
				return TypesToInclude.Contains((object)(PlaylistType)property);
			}
		}
		return false;
	}
}
