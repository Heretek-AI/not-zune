using System;

namespace ZuneUI;

public class BirthdayPropertyDescriptor : DatePropertyDescriptor
{
	public BirthdayPropertyDescriptor(string name, string multiValueString, string unknownString)
		: base(name, multiValueString, unknownString)
	{
	}

	public BirthdayPropertyDescriptor(string name, string multiValueString, string unknownString, bool required)
		: base(name, multiValueString, unknownString, required)
	{
	}

	public override bool IsValidInternal(string value)
	{
		return IsValidInternal(value, null);
	}

	public override string ConvertToString(object value, object state)
	{
		string result = base.UnknownString;
		DateTime? dateTime = (DateTime?)value;
		if (dateTime.HasValue && dateTime.Value != DateTime.MinValue)
		{
			result = DateTimeHelper.ToString((DateTime)value, state as string, DateTimeFormatType.ShortDate);
		}
		return result;
	}

	public override object ConvertFromString(string value, object state)
	{
		DateTime dateTime = DateTime.MinValue;
		string cultureString = state as string;
		DateTimeHelper.TryParse(value, cultureString, out dateTime);
		return dateTime;
	}

	public override bool IsValidInternal(string value, object state)
	{
		bool flag = true;
		string cultureString = state as string;
		if (StringParserHelper.IsNullOrEmptyOrBlank(value))
		{
			flag = !IsRequired(state);
		}
		else
		{
			flag = DateTimeHelper.TryParse(value, cultureString, out var dateTime);
			if (flag && dateTime > DateTime.Now)
			{
				flag = false;
			}
		}
		return flag;
	}

	internal override string GetOverlayString(object state)
	{
		string text = state as string;
		if (!string.IsNullOrEmpty(text))
		{
			return DateTimeHelper.GetDisplayPattern(text);
		}
		return null;
	}
}
