namespace ZuneUI;

public class PostalCodeDescriptor : CountryFieldValidationPropertyDescriptor
{
	public PostalCodeDescriptor(string name)
		: base(name, CountryFieldValidatorType.PostalCode)
	{
	}
}
