using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Iris;

namespace ZuneUI;

public abstract class SubList : ModelItem
{
	private class SubListSection : VirtualList
	{
		private int _begin;

		private int _end;

		private IList _source;

		public SubListSection(IModelItemOwner owner, int begin, int end, IList source)
			: base(owner, false, (ItemCountHandler)null)
		{
			_begin = begin;
			_end = end;
			_source = source;
			((VirtualList)this).Count = _end - _begin;
		}

		protected override object OnRequestItem(int index)
		{
			int index2 = index + _begin;
			return _source[index2];
		}
	}

	private IList _source;

	private List<IList> _subLists;

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
				ProduceSubLists();
			}
		}
	}

	public List<IList> SubLists
	{
		get
		{
			return _subLists;
		}
		private set
		{
			if (_subLists != value)
			{
				_subLists = value;
				((ModelItem)this).FirePropertyChanged("SubLists");
			}
		}
	}

	protected void ProduceSubLists()
	{
		int num = 0;
		if (_source != null)
		{
			num = _source.Count;
		}
		List<IList> list = new List<IList>();
		List<int> splits = GetSplits();
		int num2 = 0;
		if (splits != null)
		{
			for (int i = 0; i < splits.Count; i++)
			{
				int val = splits[i];
				num2 = Math.Min(num2, num);
				val = Math.Min(val, num);
				list.Add((IList)new SubListSection((IModelItemOwner)(object)this, num2, val, _source));
				num2 = val;
			}
		}
		if (num2 < num || num == 0)
		{
			list.Add((IList)new SubListSection((IModelItemOwner)(object)this, num2, num, _source));
		}
		SubLists = list;
	}

	protected abstract List<int> GetSplits();

	protected void ValidateSplits(List<int> splits)
	{
		int num = 0;
		foreach (int split in splits)
		{
			if (split < num)
			{
				throw new ArgumentException("Split values must be sequential");
			}
			num = split;
		}
	}
}
