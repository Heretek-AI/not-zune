namespace ZuneUI;

public class ContactInfoPropertyEditor : BaseContactInfoPropertyEditor
{
	private static PropertyDescriptor[] s_dataProviderProperties;

	public static CountryFieldValidationPropertyDescriptor s_Street1 = new CountryFieldValidationPropertyDescriptor("Street1", CountryFieldValidatorType.Street1);

	public static CountryFieldValidationPropertyDescriptor s_Street2 = new CountryFieldValidationPropertyDescriptor("Street2", CountryFieldValidatorType.Street2);

	public static CountryFieldValidationPropertyDescriptor s_City = new CountryFieldValidationPropertyDescriptor("City", CountryFieldValidatorType.City);

	public static CountryFieldValidationPropertyDescriptor s_District = new CountryFieldValidationPropertyDescriptor("District", CountryFieldValidatorType.District);

	public static StateDescriptor s_State = new StateDescriptor("State");

	public static CountryPropertyDescriptor s_Country = new CountryPropertyDescriptor("Country", string.Empty, string.Empty, required: true);

	public static PostalCodeDescriptor s_PostalCode = new PostalCodeDescriptor("PostalCode");

	public static LanguagePropertyDescriptor s_Language = new LanguagePropertyDescriptor("Language", string.Empty, string.Empty, required: true);

	public override PropertyDescriptor[] PropertyDescriptors
	{
		get
		{
			if (s_dataProviderProperties == null)
			{
				s_dataProviderProperties = new PropertyDescriptor[13]
				{
					BaseContactInfoPropertyEditor.s_FirstName,
					BaseContactInfoPropertyEditor.s_LastName,
					s_Street1,
					s_Street2,
					s_City,
					s_District,
					s_State,
					s_Country,
					s_PostalCode,
					BaseContactInfoPropertyEditor.s_PhoneNumber,
					BaseContactInfoPropertyEditor.s_PhoneExtension,
					BaseContactInfoPropertyEditor.s_Email,
					s_Language
				};
			}
			return s_dataProviderProperties;
		}
	}

	public static PropertyDescriptor Street1 => s_Street1;

	public static PropertyDescriptor Street2 => s_Street2;

	public static PropertyDescriptor City => s_City;

	public static PropertyDescriptor District => s_District;

	public static PropertyDescriptor State => s_State;

	public static PropertyDescriptor Country => s_Country;

	public static PropertyDescriptor PostalCode => s_PostalCode;

	public static PropertyDescriptor Language => s_Language;
}
