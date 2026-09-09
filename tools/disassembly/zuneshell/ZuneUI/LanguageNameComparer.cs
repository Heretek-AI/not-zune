using System;
using System.Collections.Generic;

namespace ZuneUI;

internal class LanguageNameComparer : IComparer<string>
{
	private static LanguageNameComparer s_instance;

	public static IComparer<string> Instance
	{
		get
		{
			if (s_instance == null)
			{
				s_instance = new LanguageNameComparer();
			}
			return s_instance;
		}
	}

	private LanguageNameComparer()
	{
	}

	int IComparer<string>.Compare(string x, string y)
	{
		string displayName = LanguageHelper.GetDisplayName(x);
		string displayName2 = LanguageHelper.GetDisplayName(y);
		return string.Compare(displayName, displayName2, StringComparison.CurrentCultureIgnoreCase);
	}
}
