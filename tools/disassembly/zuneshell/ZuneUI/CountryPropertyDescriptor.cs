namespace ZuneUI;

public class CountryPropertyDescriptor : PropertyDescriptor
{
	public CountryPropertyDescriptor(string name, string multiValueString, string unknownString, bool required)
		: base(name, multiValueString, unknownString, required)
	{
		base.DefaultValue = CultureHelper.GetDefaultCountry();
	}

	public override object ConvertFromString(string value)
	{
		return CountryHelper.GetAbbreviation(value);
	}

	public override string ConvertToString(object value)
	{
		return CountryHelper.GetDisplayName(value as string);
	}
}
