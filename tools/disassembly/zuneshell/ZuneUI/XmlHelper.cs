using System.Security;
using System.Text.RegularExpressions;

namespace ZuneUI;

public static class XmlHelper
{
	private static Regex _xmlRegex = new Regex("<[^<>]+>", RegexOptions.IgnoreCase);

	public static string Escape(string text)
	{
		return SecurityElement.Escape(text);
	}

	public static string Unescape(string text)
	{
		if (text != null)
		{
			text = text.Replace("&quot;", "\"");
			text = text.Replace("&apos;", "'");
			text = text.Replace("&amp;", "&");
			text = text.Replace("&lt;", "<");
			text = text.Replace("&gt;", ">");
		}
		return text;
	}

	public static string Strip(string text)
	{
		if (text != null)
		{
			text = _xmlRegex.Replace(text, string.Empty);
		}
		return text;
	}
}
