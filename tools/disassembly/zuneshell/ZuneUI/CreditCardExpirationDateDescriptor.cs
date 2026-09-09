using System;

namespace ZuneUI;

public class CreditCardExpirationDateDescriptor : DatePropertyDescriptor
{
	public CreditCardExpirationDateDescriptor(string name, string multiValueString, string unknownString, bool required)
		: base(name, multiValueString, unknownString, required)
	{
	}

	public override bool IsValidInternal(string value)
	{
		if (!StringParserHelper.IsNullOrEmptyOrBlank(value) && StringParserHelper.TryParseDate(value, out var dateTime))
		{
			DateTime now = DateTime.Now;
			if (dateTime.Year <= now.Year)
			{
				if (dateTime.Year == now.Year)
				{
					return dateTime.Month >= now.Month;
				}
				return false;
			}
			return true;
		}
		return false;
	}
}
