using System.Text.RegularExpressions;

namespace ZuneUI;

public static class ZuneTagHelper
{
	private static int s_maxLength = 15;

	private static Regex s_zuneTagRegex = new Regex("^[A-Z]( ?[A-Z0-9]){0,14}$", RegexOptions.IgnoreCase);

	public static int MaxLength => s_maxLength;

	public static bool IsValid(string zuneTag)
	{
		if (string.IsNullOrEmpty(zuneTag))
		{
			return false;
		}
		return s_zuneTagRegex.IsMatch(zuneTag);
	}
}
