using System.Collections;
using Microsoft.Iris;

namespace ZuneUI;

public class SearchableAggregateList : AggregateList, ISearchableList
{
	private IList[] _lists;

	private int _searchListIndex;

	public SearchableAggregateList(IList list1, IList list2, int searchListIndex)
		: base(list1, list2)
	{
		_lists = new IList[2] { list1, list2 };
		_searchListIndex = searchListIndex;
	}

	int ISearchableList.SearchForString(string str)
	{
		int num = -1;
		IList obj = _lists[_searchListIndex];
		ISearchableList val = (ISearchableList)((obj is ISearchableList) ? obj : null);
		if (val != null)
		{
			num = val.SearchForString(str);
			if (num >= 0)
			{
				for (int i = 0; i < _searchListIndex; i++)
				{
					num += _lists[i].Count;
				}
			}
		}
		return num;
	}
}
