using System.Collections;
using Microsoft.Iris;

namespace ZuneUI;

public class StringExtractorList : VirtualList, ISearchableList
{
	private IList _source;

	private ISearchableList _searchableSource;

	private bool _canSearchForString;

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
				IList source = _source;
				_searchableSource = (ISearchableList)((source is ISearchableList) ? source : null);
				Reset();
			}
		}
	}

	public bool CanSearchForString
	{
		get
		{
			return _canSearchForString;
		}
		set
		{
			_canSearchForString = value;
		}
	}

	protected void Reset()
	{
		((VirtualList)this).Clear();
		((VirtualList)this).Count = ((_source != null) ? _source.Count : 0);
	}

	protected override object OnRequestItem(int index)
	{
		return ExtractString(_source[index]);
	}

	int ISearchableList.SearchForString(string str)
	{
		if (_searchableSource != null && _canSearchForString)
		{
			return _searchableSource.SearchForString(str);
		}
		return -1;
	}

	protected virtual string ExtractString(object item)
	{
		return item.ToString();
	}
}
