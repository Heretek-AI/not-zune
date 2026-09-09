using System.Collections;
using Microsoft.Iris;

namespace ZuneUI;

public class SearchableGroupedList : GroupedList, ISearchableList
{
	public SearchableGroupedList(IList source, IComparer comparer, int count)
		: base(source, comparer, count)
	{
	}

	int ISearchableList.SearchForString(string str)
	{
		int result = -1;
		IList source = ((GroupedList)this).Source;
		ISearchableList val = (ISearchableList)((source is ISearchableList) ? source : null);
		if (val != null)
		{
			result = val.SearchForString(str);
		}
		return result;
	}
}
