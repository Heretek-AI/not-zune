using System.Collections;
using System.Collections.Generic;

namespace ZuneUI;

public class StringSet
{
	private SortedDictionary<string, object> _set = new SortedDictionary<string, object>();

	public void Add(string s)
	{
		_set[s] = null;
	}

	public void Clear()
	{
		_set.Clear();
	}

	public IList ToList()
	{
		List<string> list = new List<string>(_set.Count);
		foreach (KeyValuePair<string, object> item in _set)
		{
			list.Add(item.Key);
		}
		return list;
	}
}
