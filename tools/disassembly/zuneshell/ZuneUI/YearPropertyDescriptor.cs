using System;

namespace ZuneUI;

public class YearPropertyDescriptor : PropertyDescriptor
{
	public YearPropertyDescriptor(string name, string multiValueString, string unknownString, int maxTextLength)
		: base(name, multiValueString, unknownString, maxTextLength)
	{
	}

	public override string ConvertToString(object value)
	{
		if (value != null)
		{
			return StringFormatHelper.FormatYear((DateTime)value, base.UnknownString);
		}
		return null;
	}

	public override object ConvertFromString(string value)
	{
		if (StringParserHelper.TryParseDate(value, out var dateTime))
		{
			return dateTime;
		}
		return null;
	}

	public override bool IsValidInternal(string value)
	{
		if (!int.TryParse(value, out var result))
		{
			return false;
		}
		if (result <= 0 || result > 9999)
		{
			return false;
		}
		if (result > 99 && result < 1800)
		{
			return false;
		}
		return true;
	}
}
