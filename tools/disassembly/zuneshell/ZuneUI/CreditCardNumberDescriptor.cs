using System.Text.RegularExpressions;

namespace ZuneUI;

public class CreditCardNumberDescriptor : PropertyDescriptor
{
	private bool _allowSeperators;

	private static Regex s_numbersWithSeperatorsRegex = new Regex("^\\d(\\d*(-)*( )*)*$", RegexOptions.IgnoreCase);

	private static Regex s_numbersRegex = new Regex("^\\d+$", RegexOptions.IgnoreCase);

	public CreditCardNumberDescriptor(string name, string multiValueString, string unknownString, bool required, bool allowSeperators)
		: base(name, multiValueString, unknownString, required)
	{
		_allowSeperators = allowSeperators;
	}

	public override string ConvertToString(object value)
	{
		if (value == null)
		{
			return string.Empty;
		}
		return value as string;
	}

	public override object ConvertFromString(string value)
	{
		if (value == null)
		{
			return string.Empty;
		}
		return RemoveSeperators(value);
	}

	public override bool IsValidInternal(string value)
	{
		if (string.IsNullOrEmpty(value))
		{
			return !base.Required;
		}
		if (_allowSeperators)
		{
			return s_numbersWithSeperatorsRegex.IsMatch(value);
		}
		return s_numbersRegex.IsMatch(value);
	}

	private string RemoveSeperators(string value)
	{
		if (_allowSeperators)
		{
			value = value.Replace("-", "");
			value = value.Replace(" ", "");
		}
		return value;
	}
}
