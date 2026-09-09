using System.Collections;
using Microsoft.Iris;

namespace ZuneUI;

public abstract class FilterList : ModelItem
{
	private IList _source;

	private IList _filteredList;

	public IList Source
	{
		get
		{
			return _source;
		}
		set
		{
			if (_source != value)
			{
				_source = value;
				((ModelItem)this).FirePropertyChanged("Source");
				ProduceFilteredList();
			}
		}
	}

	public IList FilteredList
	{
		get
		{
			return _filteredList;
		}
		private set
		{
			if (_filteredList != value)
			{
				_filteredList = value;
				((ModelItem)this).FirePropertyChanged("FilteredList");
			}
		}
	}

	protected void ProduceFilteredList()
	{
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0010: Expected O, but got Unknown
		IList list = null;
		if (_source != null)
		{
			list = (IList)new ArrayListDataSet();
			for (int i = 0; i < _source.Count; i++)
			{
				object obj = _source[i];
				int count = list.Count;
				if (ShouldIncludeItem(i, count, obj))
				{
					list.Insert(count, obj);
				}
			}
		}
		FilteredList = list;
	}

	protected abstract bool ShouldIncludeItem(int sourceIndex, int targetIndex, object item);
}
