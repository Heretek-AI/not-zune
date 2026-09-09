using System;
using System.Collections;

namespace ZuneXml;

public class PropertyComparer<TPropertyType> : IComparer where TPropertyType : IComparable<TPropertyType>
{
	private Converter<object, TPropertyType> _propertyGetter;

	private bool _sortDescending;

	public PropertyComparer(Converter<object, TPropertyType> propertyGetter, bool sortDescending)
	{
		if (propertyGetter == null)
		{
			throw new ArgumentNullException("propertyGetter");
		}
		_propertyGetter = propertyGetter;
		_sortDescending = sortDescending;
	}

	public int Compare(object x, object y)
	{
		int comparisonResult = 0;
		if (!IsNullComparison(x, y, out comparisonResult))
		{
			TPropertyType val = _propertyGetter(x);
			TPropertyType val2 = _propertyGetter(y);
			if (!IsNullComparison(val, val2, out comparisonResult))
			{
				comparisonResult = val.CompareTo(val2);
			}
		}
		if (_sortDescending)
		{
			comparisonResult = -comparisonResult;
		}
		return comparisonResult;
	}

	private static bool IsNullComparison(object x, object y, out int comparisonResult)
	{
		comparisonResult = 0;
		bool result = false;
		if (x == null)
		{
			result = true;
			if (y != null)
			{
				comparisonResult = -1;
			}
		}
		if (y == null)
		{
			result = true;
			comparisonResult = 1;
		}
		return result;
	}
}
