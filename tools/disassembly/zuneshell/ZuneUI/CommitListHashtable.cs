using System.Collections;

namespace ZuneUI;

public class CommitListHashtable
{
	private Hashtable _hashtable;

	public object this[object key]
	{
		get
		{
			return _hashtable[key];
		}
		set
		{
			_hashtable[key] = value;
			ZuneShell.DefaultInstance.Management.HasPendingCommits = true;
		}
	}

	public int Count => _hashtable.Count;

	public CommitListHashtable()
	{
		_hashtable = new Hashtable();
	}

	public void Clear()
	{
		_hashtable.Clear();
		ZuneShell.DefaultInstance.Management.HasPendingCommits = false;
	}

	public bool ContainsKey(object key)
	{
		return _hashtable.ContainsKey(key);
	}

	public bool ContainsValue(object value)
	{
		return _hashtable.ContainsValue(value);
	}

	public bool ContainsIntValue(int searchValue)
	{
		Hashtable hashtable = (Hashtable)_hashtable.Clone();
		foreach (DictionaryEntry item in hashtable)
		{
			if (item.Value is int && (int)item.Value == searchValue)
			{
				return true;
			}
		}
		return false;
	}

	public void RemoveByIntValue(int removeValue)
	{
		Hashtable hashtable = (Hashtable)_hashtable.Clone();
		foreach (DictionaryEntry item in hashtable)
		{
			if (item.Value is int && (int)item.Value == removeValue)
			{
				_hashtable.Remove(item.Key);
			}
		}
		CheckForPendingCommits();
	}

	public void RemoveByStringValue(string removeValue)
	{
		if (removeValue == null)
		{
			return;
		}
		Hashtable hashtable = (Hashtable)_hashtable.Clone();
		foreach (DictionaryEntry item in hashtable)
		{
			if (item.Value is string && (string)item.Value == removeValue)
			{
				_hashtable.Remove(item.Key);
			}
		}
		CheckForPendingCommits();
	}

	public void Remove(object key)
	{
		if (key != null)
		{
			if (key != null)
			{
				_hashtable.Remove(key);
			}
			CheckForPendingCommits();
		}
	}

	public void Save()
	{
		Hashtable hashtable = (Hashtable)_hashtable.Clone();
		Clear();
		foreach (DictionaryEntry item in hashtable)
		{
			((ProxySettingDelegate)item.Key)(item.Value);
		}
	}

	private void CheckForPendingCommits()
	{
		if (_hashtable != null && _hashtable.Count == 0)
		{
			Clear();
		}
	}
}
