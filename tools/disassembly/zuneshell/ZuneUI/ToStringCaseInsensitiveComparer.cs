using System;
using System.Collections;

namespace ZuneUI;

internal class ToStringCaseInsensitiveComparer : IComparer
{
	private static ToStringCaseInsensitiveComparer s_instance;

	public static IComparer Instance
	{
		get
		{
			if (s_instance == null)
			{
				s_instance = new ToStringCaseInsensitiveComparer();
			}
			return s_instance;
		}
	}

	private ToStringCaseInsensitiveComparer()
	{
	}

	int IComparer.Compare(object x, object y)
	{
		return string.Compare(x.ToString(), y.ToString(), StringComparison.CurrentCultureIgnoreCase);
	}
}
