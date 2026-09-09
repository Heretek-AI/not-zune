using Microsoft.Zune.Service;

namespace ZuneUI;

public class CardTypePropertyDescriptor : PropertyDescriptor
{
	public CardTypePropertyDescriptor(string name, string multiValueString, string unknownString, bool required)
		: base(name, multiValueString, unknownString, required)
	{
	}

	public override string ConvertToString(object value)
	{
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		if (value == null)
		{
			return CreditCardHelper.CardTypeToString((CreditCardType)(-1));
		}
		return CreditCardHelper.CardTypeToString((CreditCardType)value);
	}

	public override object ConvertFromString(string value)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		return CreditCardHelper.CardTypeFromString(value);
	}

	public override bool IsValidInternal(string value)
	{
		if (StringParserHelper.IsNullOrEmptyOrBlank(value))
		{
			return false;
		}
		return true;
	}
}
