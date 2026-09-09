using System;
using System.Globalization;
using System.Text.RegularExpressions;

namespace ZuneUI;

public static class DateTimeHelper
{
	private static string _unknown = Shell.LoadString(StringId.IDS_TYPE_UNKNOWN);

	public static DateTime Empty => DateTime.MinValue;

	public static bool IsEmpty(DateTime date)
	{
		return date == DateTime.MinValue;
	}

	public static string GetDisplayPattern(string cultureString)
	{
		string result = null;
		CultureInfo cultureInfo = null;
		if (!string.IsNullOrEmpty(cultureString))
		{
			try
			{
				cultureInfo = new CultureInfo(cultureString, useUserOverride: false);
			}
			catch
			{
			}
		}
		if (cultureInfo == null)
		{
			cultureInfo = CultureInfo.CurrentUICulture;
		}
		if (cultureInfo != null)
		{
			string newValue = Shell.LoadString(StringId.IDS_DATETIME_YEAR_ABBREVIATION);
			string text = Shell.LoadString(StringId.IDS_DATETIME_MONTH_ABBREVIATION);
			string text2 = Shell.LoadString(StringId.IDS_DATETIME_DAY_ABBREVIATION);
			result = cultureInfo.DateTimeFormat.ShortDatePattern.Replace("y", newValue);
			result = Regex.Replace(result, "M{1,2}", text + text);
			result = Regex.Replace(result, "d{1,2}", text2 + text2);
		}
		return result;
	}

	public static bool TryParse(string dateTimeString, string cultureString, out DateTime dateTime)
	{
		bool result = false;
		CultureInfo culture = CultureHelper.GetCulture(cultureString);
		dateTime = DateTime.MaxValue;
		if (culture != null)
		{
			string shortDatePattern = culture.DateTimeFormat.ShortDatePattern;
			shortDatePattern = Regex.Replace(shortDatePattern, "M{1,2}", "M");
			shortDatePattern = Regex.Replace(shortDatePattern, "d{1,2}", "d");
			result = DateTime.TryParseExact(dateTimeString, shortDatePattern, culture, DateTimeStyles.None, out dateTime);
		}
		return result;
	}

	public static string ToString(DateTime dateTime, DateTimeFormatType format)
	{
		return ToString(dateTime, null, format);
	}

	public static string ToString(DateTime dateTime, string cultureString, DateTimeFormatType format)
	{
		string result = null;
		CultureInfo culture = CultureHelper.GetCulture(cultureString);
		try
		{
			if (culture != null)
			{
				string text = null;
				result = dateTime.ToString(format switch
				{
					DateTimeFormatType.ShortDate => culture.DateTimeFormat.ShortDatePattern, 
					DateTimeFormatType.LongDate => culture.DateTimeFormat.LongDatePattern, 
					DateTimeFormatType.FullLongDateLongTime => culture.DateTimeFormat.FullDateTimePattern, 
					DateTimeFormatType.ShortTime => culture.DateTimeFormat.ShortTimePattern, 
					DateTimeFormatType.LongTime => culture.DateTimeFormat.LongTimePattern, 
					DateTimeFormatType.YearMonth => culture.DateTimeFormat.YearMonthPattern, 
					DateTimeFormatType.MonthDay => culture.DateTimeFormat.MonthDayPattern, 
					_ => throw new ArgumentException(), 
				}, culture);
			}
		}
		catch (ArgumentOutOfRangeException)
		{
			result = _unknown;
		}
		return result;
	}
}
