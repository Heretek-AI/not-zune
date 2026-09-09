using System.Text;

namespace ZuneUI;

public class PhoneNumberDescriptors : CountryFieldValidationPropertyDescriptor
{
	public PhoneNumberDescriptors(string name, CountryFieldValidatorType type)
		: base(name, type)
	{
	}

	public bool Split(string value, out string areaCode, out string number)
	{
		value = RemoveNonDigits(value);
		if (value.Length >= 10)
		{
			number = value.Substring(value.Length - 7, 7);
			areaCode = value.Substring(value.Length - 10, 3);
			return true;
		}
		areaCode = string.Empty;
		number = string.Empty;
		return false;
	}

	public string Combine(string areaCode, string number)
	{
		string result = null;
		if (!string.IsNullOrEmpty(number))
		{
			StringBuilder stringBuilder = new StringBuilder();
			if (!string.IsNullOrEmpty(areaCode))
			{
				stringBuilder.Append(areaCode);
				stringBuilder.Append('-');
			}
			if (number.Length >= 7)
			{
				stringBuilder.Append(number.Substring(0, 3));
				stringBuilder.Append('-');
				stringBuilder.Append(number.Substring(3));
			}
			else
			{
				stringBuilder.Append(number);
			}
			result = stringBuilder.ToString();
		}
		return result;
	}

	private static string RemoveNonDigits(string value)
	{
		if (value == null)
		{
			return string.Empty;
		}
		StringBuilder stringBuilder = new StringBuilder(value.Length);
		for (int i = 0; i < value.Length; i++)
		{
			if (char.IsDigit(value[i]))
			{
				stringBuilder.Append(value[i]);
			}
		}
		return stringBuilder.ToString();
	}
}
