using System;
using System.Globalization;

namespace ZuneUI;

internal class CountryHelper
{
	internal static string GetDisplayName(string countryCode)
	{
		string result = string.Empty;
		if (!string.IsNullOrEmpty(countryCode))
		{
			try
			{
				RegionInfo regionInfo = new RegionInfo(countryCode);
				result = regionInfo.DisplayName;
			}
			catch (ArgumentException)
			{
			}
		}
		return result;
	}

	internal static string GetAbbreviation(string countryName)
	{
		string result = string.Empty;
		if (!string.IsNullOrEmpty(countryName))
		{
			CultureInfo[] cultures = CultureInfo.GetCultures(CultureTypes.SpecificCultures);
			CultureInfo[] array = cultures;
			foreach (CultureInfo cultureInfo in array)
			{
				try
				{
					if (!string.IsNullOrEmpty(cultureInfo.Name))
					{
						RegionInfo regionInfo = new RegionInfo(cultureInfo.Name);
						if (regionInfo.DisplayName.Equals(countryName, StringComparison.CurrentCultureIgnoreCase))
						{
							result = regionInfo.TwoLetterISORegionName;
							break;
						}
					}
				}
				catch (ArgumentException)
				{
				}
			}
		}
		return result;
	}
}
