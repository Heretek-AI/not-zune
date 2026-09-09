using System;
using System.Collections.Generic;
using Microsoft.Iris;

namespace ZuneUI;

public class ConstantSplitSubList : SubList
{
	private int _splitSize = 1;

	public int SplitSize
	{
		get
		{
			return _splitSize;
		}
		set
		{
			if (_splitSize != value)
			{
				if (value <= 0)
				{
					throw new ArgumentOutOfRangeException("value", "SplitSize must be positive.");
				}
				_splitSize = value;
				((ModelItem)this).FirePropertyChanged("SplitSize");
				ProduceSubLists();
			}
		}
	}

	protected override List<int> GetSplits()
	{
		if (base.Source == null)
		{
			return null;
		}
		int count = base.Source.Count;
		int num = count / _splitSize;
		List<int> list = new List<int>(num);
		for (int i = 0; i < num; i++)
		{
			int item = (i + 1) * _splitSize;
			list.Add(item);
		}
		return list;
	}
}
