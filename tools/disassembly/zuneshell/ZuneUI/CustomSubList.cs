using System.Collections;
using System.Collections.Generic;
using Microsoft.Iris;

namespace ZuneUI;

public class CustomSubList : SubList
{
	private List<int> _splits;

	public List<int> Splits
	{
		get
		{
			return _splits;
		}
		set
		{
			if (_splits != value)
			{
				if (value != null)
				{
					ValidateSplits(value);
				}
				_splits = value;
				((ModelItem)this).FirePropertyChanged("Splits");
				ProduceSubLists();
			}
		}
	}

	public static void AssignSplits(CustomSubList list, IList inList)
	{
		List<int> list2 = new List<int>(inList.Count);
		foreach (object @in in inList)
		{
			if (@in is int)
			{
				list2.Add((int)@in);
			}
		}
		list.Splits = list2;
	}

	protected override List<int> GetSplits()
	{
		return Splits;
	}
}
