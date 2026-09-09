using System;

namespace ZuneUI;

public class StringHelper
{
	public static bool IsEqualCaseInsensitive(string first, string second)
	{
		return string.Compare(first, second, StringComparison.InvariantCultureIgnoreCase) == 0;
	}

	public static string[] Split(string value, string splitPattern)
	{
		if (string.IsNullOrEmpty(value))
		{
			return null;
		}
		return value.Split(new string[1] { splitPattern }, StringSplitOptions.RemoveEmptyEntries);
	}

	public static bool CaseInsensitiveCompare(string str1, string str2)
	{
		if (!string.IsNullOrEmpty(str1) && !string.IsNullOrEmpty(str2))
		{
			return string.Compare(str1, str2, StringComparison.CurrentCultureIgnoreCase) == 0;
		}
		return false;
	}
}
