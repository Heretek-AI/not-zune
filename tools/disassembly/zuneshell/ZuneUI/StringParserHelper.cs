using System;
using System.Collections;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace ZuneUI;

public static class StringParserHelper
{
	private static Regex s_notNumberRegex = new Regex("[^0-9]");

	private static string s_validNumberChars = "0123456789";

	public static int ParseInt32(string input)
	{
		int.TryParse(input, out var result);
		return result;
	}

	public static int NumberFilter(string input)
	{
		return ParseInt32(s_notNumberRegex.Replace(input, string.Empty));
	}

	public static bool TryParseDate(string input, DateTimeKind dateTimeKind, out DateTime dateTime)
	{
		switch (dateTimeKind)
		{
		case DateTimeKind.Utc:
			if (!DateTime.TryParse(input, out dateTime) && !DateTime.TryParse(input, CultureInfo.CurrentCulture.DateTimeFormat, DateTimeStyles.RoundtripKind, out dateTime))
			{
				if (!int.TryParse(input, out var result2) || result2 < 1 || result2 > 9999)
				{
					return false;
				}
				dateTime = new DateTime(result2, 1, 1, 0, 0, 0, DateTimeKind.Utc);
			}
			else
			{
				dateTime = new DateTime(dateTime.Ticks, DateTimeKind.Utc);
			}
			return true;
		case DateTimeKind.Local:
			if (!DateTime.TryParse(input, out dateTime) && !DateTime.TryParse(input, CultureInfo.CurrentCulture.DateTimeFormat, DateTimeStyles.RoundtripKind, out dateTime))
			{
				if (!int.TryParse(input, out var result) || result < 1 || result > 9999)
				{
					return false;
				}
				dateTime = new DateTime(result, 1, 1, 0, 0, 0, DateTimeKind.Local);
				dateTime = dateTime.ToUniversalTime();
			}
			else
			{
				dateTime = dateTime.ToUniversalTime();
			}
			return true;
		default:
			return TryParseDate(input, out dateTime);
		}
	}

	public static bool TryParseDate(string input, out DateTime dateTime)
	{
		if (!DateTime.TryParse(input, out dateTime) && !DateTime.TryParse(input, CultureInfo.CurrentCulture.DateTimeFormat, DateTimeStyles.RoundtripKind, out dateTime))
		{
			if (!int.TryParse(input, out var result) || result < 1 || result > 9999)
			{
				return false;
			}
			dateTime = new DateTime(result, 1, 1, 0, 0, 0, DateTimeKind.Utc);
		}
		else
		{
			dateTime = dateTime.ToUniversalTime();
		}
		return true;
	}

	public static string CoerceToNonNegativeInt(string input)
	{
		if (input != null)
		{
			StringBuilder stringBuilder = null;
			int i = 0;
			int num = 0;
			for (; i < input.Length; i++)
			{
				if (s_validNumberChars.IndexOf(input[i]) < 0)
				{
					if (stringBuilder == null)
					{
						stringBuilder = new StringBuilder(input);
					}
					stringBuilder.Remove(num, 1);
				}
				else
				{
					num++;
				}
			}
			if (stringBuilder != null)
			{
				input = stringBuilder.ToString();
			}
		}
		return input;
	}

	public static int ParseYear(string input)
	{
		int result = 0;
		if (input != null && input.Length <= 4 && int.TryParse(input, out result))
		{
			if (result >= 0)
			{
				if (result < 20)
				{
					result += 2000;
				}
				else if (result < 100)
				{
					result += 1900;
				}
				else if (result < 1000)
				{
					result += 2000;
				}
			}
			else
			{
				result = 0;
			}
		}
		return result;
	}

	public static IList Split(string source, string splitCharacter)
	{
		return source.Split(new string[1] { splitCharacter }, StringSplitOptions.None);
	}

	public static bool IsNullOrEmptyOrBlank(string value)
	{
		if (!string.IsNullOrEmpty(value))
		{
			return value.Trim().Length == 0;
		}
		return true;
	}

	public static string HtmlTagsToLowerCase(string html)
	{
		if (!string.IsNullOrEmpty(html))
		{
			StringBuilder stringBuilder = null;
			bool flag = false;
			for (int i = 0; i < html.Length; i++)
			{
				char c = html[i];
				if (c == '<')
				{
					flag = true;
				}
				else if (c == '>' || char.IsWhiteSpace(c))
				{
					flag = false;
				}
				if (flag && char.IsUpper(c))
				{
					if (stringBuilder == null)
					{
						stringBuilder = new StringBuilder(html);
					}
					stringBuilder[i] = char.ToLower(c);
				}
			}
			if (stringBuilder != null)
			{
				html = stringBuilder.ToString();
			}
		}
		return html;
	}

	public static string StripHtmlTags(string html)
	{
		if (!string.IsNullOrEmpty(html))
		{
			StringBuilder stringBuilder = null;
			bool flag = false;
			foreach (char c in html)
			{
				if (c == '<')
				{
					flag = true;
				}
				if (!flag)
				{
					if (stringBuilder == null)
					{
						stringBuilder = new StringBuilder(html.Length);
					}
					stringBuilder.Append(c);
				}
				if (c == '>')
				{
					flag = false;
				}
			}
			if (stringBuilder != null)
			{
				html = stringBuilder.ToString();
			}
		}
		return html;
	}

	public static string FormatFirmwareVersion(string version)
	{
		if (string.IsNullOrEmpty(version))
		{
			return string.Empty;
		}
		string[] array = version.Split(new char[1] { '.' });
		if (array.Length < 3)
		{
			return version;
		}
		int num = ParseInt32(array[0]);
		int num2 = ParseInt32(array[1]);
		int num3 = ParseInt32(array[2]);
		version = ((num != 0) ? string.Format(Shell.LoadString(StringId.IDS_FIRMWARE_VERSION_FORMAT), num, num2, num3) : Shell.LoadString(StringId.IDS_DEVICE_RECOVERY_MODE));
		return version;
	}

	public static int CompareFirmwareVersions(string versionA, string versionB)
	{
		string[] array = new string[3] { "0", "0", "0" };
		string[] array2 = ((!string.IsNullOrEmpty(versionA)) ? versionA.Split(new char[1] { '.' }) : array);
		string[] array3 = ((!string.IsNullOrEmpty(versionB)) ? versionB.Split(new char[1] { '.' }) : array);
		int num = ParseInt32(array2[0]);
		int num2 = ParseInt32(array2[1]);
		int num3 = ParseInt32(array2[2]);
		int value = ParseInt32(array3[0]);
		int value2 = ParseInt32(array3[1]);
		int value3 = ParseInt32(array3[2]);
		int num4 = num.CompareTo(value);
		int num5 = num2.CompareTo(value2);
		int result = num3.CompareTo(value3);
		if (num4 != 0)
		{
			return num4;
		}
		if (num5 != 0)
		{
			return num5;
		}
		return result;
	}
}
