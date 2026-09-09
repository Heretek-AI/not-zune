using System;
using System.Collections.Generic;

namespace ZuneUI;

internal class CountryNameComparer : IComparer<string>
{
	private static CountryNameComparer s_instance;

	public static IComparer<string> Instance
	{
		get
		{
			if (s_instance == null)
			{
				s_instance = new CountryNameComparer();
			}
			return s_instance;
		}
	}

	private CountryNameComparer()
	{
	}

	int IComparer<string>.Compare(string x, string y)
	{
		string displayName = CountryHelper.GetDisplayName(x);
		string displayName2 = CountryHelper.GetDisplayName(y);
		return string.Compare(displayName, displayName2, StringComparison.CurrentCultureIgnoreCase);
	}
}
