using System;
using System.Collections;
using Microsoft.Iris;

namespace ZuneUI;

public class ReleaseYearComparer : IComparer
{
	public int Compare(object x, object y)
	{
		DataProviderObject val = (DataProviderObject)((x is DataProviderObject) ? x : null);
		DataProviderObject val2 = (DataProviderObject)((y is DataProviderObject) ? y : null);
		if (val != null && val2 != null)
		{
			DateTime dateTime = (DateTime)val.GetProperty("ReleaseDate");
			DateTime dateTime2 = (DateTime)val2.GetProperty("ReleaseDate");
			return dateTime.Year.CompareTo(dateTime2.Year);
		}
		return 1;
	}
}
