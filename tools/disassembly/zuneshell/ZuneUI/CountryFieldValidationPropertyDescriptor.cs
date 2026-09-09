using System.Text.RegularExpressions;
using Microsoft.Zune.Service;

namespace ZuneUI;

public class CountryFieldValidationPropertyDescriptor : PropertyDescriptor
{
	private CountryFieldValidatorType _fieldValidatorType;

	public CountryFieldValidationPropertyDescriptor(string name, CountryFieldValidatorType fieldValidatorType)
		: base(name, string.Empty, string.Empty, required: false)
	{
		_fieldValidatorType = fieldValidatorType;
	}

	internal override string GetOverlayString(object state)
	{
		string result = string.Empty;
		AccountCountry accountCountry = GetAccountCountry(state);
		if (accountCountry != null)
		{
			CountryFieldValidator validator = accountCountry.GetValidator(_fieldValidatorType);
			if (validator != null)
			{
				result = validator.FriendlyFormat;
			}
		}
		return result;
	}

	internal override string GetLabelString(object state)
	{
		string result = string.Empty;
		AccountCountry accountCountry = GetAccountCountry(state);
		if (accountCountry != null)
		{
			CountryFieldValidator validator = accountCountry.GetValidator(_fieldValidatorType);
			if (validator != null)
			{
				result = Shell.LoadString(validator.NameStringId);
			}
		}
		return result;
	}

	public override bool IsValidInternal(string value, object state)
	{
		string text = null;
		AccountCountry accountCountry = GetAccountCountry(state);
		if (accountCountry == null)
		{
			return true;
		}
		if (accountCountry != null)
		{
			CountryFieldValidator validator = accountCountry.GetValidator(_fieldValidatorType);
			if (validator != null && !string.IsNullOrEmpty(validator.Regex))
			{
				text = validator.Regex;
			}
		}
		return string.IsNullOrEmpty(text) || (value != null && Regex.IsMatch(value, text));
	}

	public override bool IsRequiredInternal(object state)
	{
		AccountCountry accountCountry = GetAccountCountry(state);
		if (accountCountry != null)
		{
			CountryFieldValidator validator = accountCountry.GetValidator(_fieldValidatorType);
			if (validator != null && !string.IsNullOrEmpty(validator.Regex))
			{
				return true;
			}
		}
		return false;
	}

	private AccountCountry GetAccountCountry(object state)
	{
		AccountCountry result = null;
		if (state != null)
		{
			result = AccountCountryList.Instance.GetCountry(state as string);
		}
		return result;
	}
}
