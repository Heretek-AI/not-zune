using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Iris;

namespace ZuneUI;

public abstract class MixPriorityList
{
	protected class PriorityResult : IComparable
	{
		public int Priority;

		public MixResult Result;

		public PriorityResult(int priority, MixResult result)
		{
			Priority = priority;
			Result = result;
		}

		public int CompareTo(object obj)
		{
			PriorityResult priorityResult = obj as PriorityResult;
			return Priority - priorityResult.Priority;
		}
	}

	protected delegate int GetItemPriorityDelegate(DataProviderObject item, int startPriority);

	protected delegate MixResult CreateItemInstanceDelegate(DataProviderObject item, string reason);

	private MixResult _mixResultSeed;

	private List<PriorityResult> _list;

	private List<MixResult> _sortedList;

	public IList List
	{
		get
		{
			if (_sortedList == null)
			{
				_sortedList = new List<MixResult>(_list.Count);
				_list.Sort();
				foreach (PriorityResult item in _list)
				{
					_sortedList.Add(item.Result);
				}
			}
			return _sortedList;
		}
	}

	protected MixPriorityList(MixResult mixResultSeed)
	{
		_list = new List<PriorityResult>();
		_mixResultSeed = mixResultSeed;
	}

	public void Add(DataProviderObject item, string reason)
	{
		List<DataProviderObject> list = new List<DataProviderObject>();
		list.Add(item);
		AddList(list, reason, 1);
	}

	public abstract void AddList(IList sourceList, string reason, int maxItems);

	protected void AddList(IList sourceList, string reason, int maxItems, GetItemPriorityDelegate getItemPriorityDelegate, CreateItemInstanceDelegate createItemInstanceDelegate)
	{
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_0017: Expected O, but got Unknown
		int num = 0;
		foreach (DataProviderObject source in sourceList)
		{
			DataProviderObject item = source;
			if (Add(createItemInstanceDelegate(item, reason), getItemPriorityDelegate(item, num)))
			{
				num++;
			}
			if (num >= maxItems)
			{
				break;
			}
		}
	}

	protected bool Add(MixResult newResult, int priority)
	{
		bool flag = false;
		if (newResult.IsDuplicate(_mixResultSeed))
		{
			flag = true;
		}
		if (!flag && newResult.ResultType == MixResultType.Profile)
		{
			flag = SignIn.Instance.IsSignedInUser(newResult.Id);
		}
		if (!flag)
		{
			foreach (PriorityResult item in _list)
			{
				if (newResult.IsDuplicate(item.Result))
				{
					flag = true;
					break;
				}
			}
		}
		if (!flag)
		{
			_list.Add(new PriorityResult(priority, newResult));
			_sortedList = null;
		}
		return !flag;
	}
}
