using System.Text.RegularExpressions;

namespace ZuneUI;

public static class EmailHelper
{
	private static Regex s_emailRegex = new Regex("^[A-Z0-9._%+-]+@[A-Z0-9.-]+\\.[A-Z]{2,6}$", RegexOptions.IgnoreCase);

	private static Regex s_emailDomainRegex = new Regex("^[A-Z0-9.-]+\\.[A-Z]{2,6}$", RegexOptions.IgnoreCase);

	public static bool IsValid(string email)
	{
		if (string.IsNullOrEmpty(email))
		{
			return false;
		}
		return s_emailRegex.IsMatch(email);
	}

	public static bool IsValidDomain(string domain)
	{
		if (string.IsNullOrEmpty(domain))
		{
			return false;
		}
		return s_emailDomainRegex.IsMatch(domain);
	}
}
