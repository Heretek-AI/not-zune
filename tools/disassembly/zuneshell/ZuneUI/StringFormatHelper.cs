using System;
using System.Globalization;

namespace ZuneUI;

public static class StringFormatHelper
{
	private static string _unknown = Shell.LoadString(StringId.IDS_TYPE_UNKNOWN);

	public static string ShortDateFormat => "d";

	public static string ShortDatePatternForDisplay => DateTimeFormatInfo.CurrentInfo.ShortDatePattern.ToLower();

	public static string ShortTimeFormat => "t";

	public static string YearFormat => "yyyy";

	public static string NumericMonthDayPattern => Shell.LoadString(StringId.IDS_DATETIME_NUMERIC_MONTH_DAY_PATTERN);

	public static string NumericMonthYearPattern => Shell.LoadString(StringId.IDS_DATETIME_NUMERIC_MONTH_YEAR_PATTERN);

	public static string FriendlyMonthYearPattern => Shell.LoadString(StringId.IDS_DATETIME_MONTH_DAY_YEAR_PATTERN);

	private static string Format(DateTime date, string format, string unknown)
	{
		if (date == DateTime.MinValue)
		{
			return unknown;
		}
		return date.ToString(format);
	}

	public static string Format(DateTime date, string format)
	{
		return Format(date, format, _unknown);
	}

	public static string FormatShortDate(DateTime date, string unknown)
	{
		return Format(date, ShortDateFormat, unknown);
	}

	public static string FormatShortDate(DateTime date)
	{
		return FormatShortDate(date, _unknown);
	}

	public static string FormatYear(DateTime date, string unknown)
	{
		return Format(date, YearFormat, unknown);
	}

	public static string FormatYear(DateTime date)
	{
		return FormatYear(date, _unknown);
	}

	public static string FormatPrice(double price, string currencyCode)
	{
		CultureInfo cultureInfo = CultureInfo.CurrentCulture;
		bool flag = string.IsNullOrEmpty(currencyCode);
		if (!flag)
		{
			try
			{
				RegionInfo regionInfo = new RegionInfo(cultureInfo.LCID);
				flag = regionInfo.ISOCurrencySymbol.Equals(currencyCode, StringComparison.InvariantCultureIgnoreCase);
			}
			catch (ArgumentException)
			{
			}
		}
		if (!flag)
		{
			CultureInfo[] cultures = CultureInfo.GetCultures(CultureTypes.InstalledWin32Cultures);
			CultureInfo[] array = cultures;
			foreach (CultureInfo cultureInfo2 in array)
			{
				try
				{
					RegionInfo regionInfo2 = new RegionInfo(cultureInfo2.LCID);
					if (regionInfo2.ISOCurrencySymbol.Equals(currencyCode, StringComparison.InvariantCultureIgnoreCase))
					{
						cultureInfo = cultureInfo2;
						break;
					}
				}
				catch (ArgumentException)
				{
				}
			}
		}
		return string.Format(cultureInfo, "{0:c}", new object[1] { price });
	}
}
