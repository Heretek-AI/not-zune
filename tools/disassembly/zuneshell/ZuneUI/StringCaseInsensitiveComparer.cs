using System;
using System.Collections.Generic;

namespace ZuneUI;

internal class StringCaseInsensitiveComparer : IComparer<string>
{
	private static StringCaseInsensitiveComparer s_instance;

	public static IComparer<string> Instance
	{
		get
		{
			if (s_instance == null)
			{
				s_instance = new StringCaseInsensitiveComparer();
			}
			return s_instance;
		}
	}

	private StringCaseInsensitiveComparer()
	{
	}

	int IComparer<string>.Compare(string x, string y)
	{
		return string.Compare(x, y, StringComparison.CurrentCultureIgnoreCase);
	}
}
