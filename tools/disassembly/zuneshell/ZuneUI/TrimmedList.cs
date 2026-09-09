using System;
using System.Collections;
using Microsoft.Iris;

namespace ZuneUI;

public class TrimmedList : VirtualList
{
	private IList _source;

	private int _maxCount;

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
				Reset();
			}
		}
	}

	public int MaxCount
	{
		get
		{
			return _maxCount;
		}
		set
		{
			if (_maxCount != value)
			{
				_maxCount = value;
				((ModelItem)this).FirePropertyChanged("MaxCount");
				Reset();
			}
		}
	}

	protected void Reset()
	{
		((VirtualList)this).Clear();
		((VirtualList)this).Count = ((_source != null) ? Math.Min(_source.Count, _maxCount) : 0);
	}

	protected override object OnRequestItem(int index)
	{
		return _source[index];
	}
}
