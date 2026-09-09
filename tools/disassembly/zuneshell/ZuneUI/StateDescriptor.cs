using System.Globalization;

namespace ZuneUI;

public class StateDescriptor : CountryFieldValidationPropertyDescriptor
{
	public StateDescriptor(string name)
		: base(name, CountryFieldValidatorType.State)
	{
	}

	public override object ConvertFromString(string value)
	{
		string state = (SignIn.Instance.SignedIn ? SignIn.Instance.CountryCode : RegionInfo.CurrentRegion.TwoLetterISORegionName);
		return ConvertFromString(value, state);
	}

	public override object ConvertFromString(string value, object country)
	{
		string text = null;
		AccountCountry country2 = AccountCountryList.Instance.GetCountry(country as string);
		if (country2 != null)
		{
			text = country2.GetStateAbbreviation(value);
		}
		if (text != null)
		{
			return text;
		}
		return value;
	}

	public override string ConvertToString(object value)
	{
		string state = (SignIn.Instance.SignedIn ? SignIn.Instance.CountryCode : RegionInfo.CurrentRegion.TwoLetterISORegionName);
		return ConvertToString(value, state);
	}

	public override string ConvertToString(object value, object country)
	{
		string text = null;
		AccountCountry country2 = AccountCountryList.Instance.GetCountry(country as string);
		if (country2 != null)
		{
			text = country2.GetState(value as string);
		}
		if (text != null)
		{
			return text;
		}
		return value as string;
	}
}
