using System;

namespace ZuneUI;

public class DatePropertyDescriptor : PropertyDescriptor
{
	private DateTimeKind timeZoneOverride;

	public DatePropertyDescriptor(string name, string multiValueString, string unknownString)
		: base(name, multiValueString, unknownString, 1000, required: false, DateTime.MinValue)
	{
	}

	public DatePropertyDescriptor(string name, string multiValueString, string unknownString, DateTimeKind timeZoneOverride)
		: base(name, multiValueString, unknownString, 1000, required: false, DateTime.MinValue)
	{
		this.timeZoneOverride = timeZoneOverride;
	}

	public DatePropertyDescriptor(string name, string multiValueString, string unknownString, bool required)
		: base(name, multiValueString, unknownString, 1000, required, DateTime.MinValue)
	{
	}

	public override string ConvertToString(object value)
	{
		return StringFormatHelper.FormatShortDate((DateTime)value, base.UnknownString);
	}

	public override object ConvertFromString(string value)
	{
		if (StringParserHelper.TryParseDate(value, timeZoneOverride, out var dateTime))
		{
			return dateTime;
		}
		return DateTime.MinValue;
	}

	public override bool IsValidInternal(string value)
	{
		if (StringParserHelper.IsNullOrEmptyOrBlank(value))
		{
			return true;
		}
		DateTime dateTime;
		return StringParserHelper.TryParseDate(value, out dateTime);
	}
}
