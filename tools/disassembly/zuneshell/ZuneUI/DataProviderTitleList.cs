using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Iris;

namespace ZuneUI;

public class DataProviderTitleList : IList, ICollection, IEnumerable
{
	private List<DataProviderObject> _dataProviders = new List<DataProviderObject>();

	public IList DataProviders => _dataProviders;

	public bool IsFixedSize => ((IList)_dataProviders).IsFixedSize;

	public bool IsReadOnly => ((IList)_dataProviders).IsReadOnly;

	public object this[int index]
	{
		get
		{
			return _dataProviders[index].GetProperty("Title");
		}
		set
		{
			throw new NotImplementedException();
		}
	}

	public int Count => _dataProviders.Count;

	public bool IsSynchronized => ((ICollection)_dataProviders).IsSynchronized;

	public object SyncRoot => ((ICollection)_dataProviders).SyncRoot;

	public void InitializeDataProviders(IList list)
	{
		_dataProviders = new List<DataProviderObject>();
		foreach (object item in list)
		{
			_dataProviders.Add((DataProviderObject)((item is DataProviderObject) ? item : null));
		}
	}

	public int Add(object value)
	{
		throw new NotImplementedException();
	}

	public void Clear()
	{
		_dataProviders.Clear();
	}

	public bool Contains(object value)
	{
		return IndexOf(value) >= 0;
	}

	public int IndexOf(object value)
	{
		string b = value as string;
		for (int i = 0; i < _dataProviders.Count; i++)
		{
			if (string.Equals((string)_dataProviders[i].GetProperty("Title"), b, StringComparison.CurrentCultureIgnoreCase))
			{
				return i;
			}
		}
		return -1;
	}

	public void Insert(int index, object value)
	{
		throw new NotImplementedException();
	}

	public void Remove(object value)
	{
		string value2 = value as string;
		int num = IndexOf(value2);
		if (num >= 0)
		{
			_dataProviders.RemoveAt(num);
		}
	}

	public void RemoveAt(int index)
	{
		_dataProviders.RemoveAt(index);
	}

	public void CopyTo(Array array, int index)
	{
		throw new NotImplementedException();
	}

	public IEnumerator GetEnumerator()
	{
		for (int i = 0; i < _dataProviders.Count; i++)
		{
			yield return _dataProviders[i].GetProperty("Title");
		}
	}
}
