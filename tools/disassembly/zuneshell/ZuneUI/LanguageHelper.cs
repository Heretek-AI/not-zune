using System;
using System.Globalization;

namespace ZuneUI;

internal class LanguageHelper
{
	internal static string GetDisplayLanguageName(string languageCode)
	{
		string result = null;
		try
		{
			if (!string.IsNullOrEmpty(languageCode))
			{
				CultureInfo cultureInfo = CultureInfo.CreateSpecificCulture(languageCode);
				while (!cultureInfo.IsNeutralCulture)
				{
					cultureInfo = cultureInfo.Parent;
				}
				result = GetDisplayName(cultureInfo.ToString());
			}
		}
		catch (ArgumentException)
		{
		}
		return result;
	}

	internal static string GetDisplayName(string languageCode)
	{
		string result = string.Empty;
		if (!string.IsNullOrEmpty(languageCode))
		{
			try
			{
				CultureInfo cultureInfo = new CultureInfo(languageCode);
				result = cultureInfo.DisplayName;
			}
			catch (ArgumentException)
			{
			}
		}
		return result;
	}

	internal static string GetAbbreviation(string languageName)
	{
		string result = string.Empty;
		if (!string.IsNullOrEmpty(languageName))
		{
			CultureInfo[] cultures = CultureInfo.GetCultures(CultureTypes.AllCultures);
			CultureInfo[] array = cultures;
			foreach (CultureInfo cultureInfo in array)
			{
				if (cultureInfo.DisplayName.Equals(languageName, StringComparison.CurrentCultureIgnoreCase))
				{
					result = cultureInfo.TwoLetterISOLanguageName;
					break;
				}
			}
		}
		return result;
	}
}
