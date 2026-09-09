using System;
using System.Collections;
using Microsoft.Iris;

namespace ZuneUI;

public class DataProviderPropertyComparer : IComparer
{
	private string _propertyName;

	public string PropertyName
	{
		get
		{
			return _propertyName;
		}
		set
		{
			_propertyName = value;
		}
	}

	public DataProviderPropertyComparer()
	{
	}

	public DataProviderPropertyComparer(string propertyName)
	{
		_propertyName = propertyName;
	}

	public int Compare(object x, object y)
	{
		DataProviderObject val = (DataProviderObject)((x is DataProviderObject) ? x : null);
		DataProviderObject val2 = (DataProviderObject)((y is DataProviderObject) ? y : null);
		if (val != null && val2 != null)
		{
			object property = val.GetProperty(PropertyName);
			object property2 = val2.GetProperty(PropertyName);
			if (property is IComparable comparable)
			{
				return comparable.CompareTo(property2);
			}
			if (object.Equals(property, property2))
			{
				return 0;
			}
		}
		return 1;
	}
}
