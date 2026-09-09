namespace ZuneUI;

public abstract class BaseContactInfoPropertyEditor : WizardPropertyEditor
{
	public static CountryFieldValidationPropertyDescriptor s_FirstName = new CountryFieldValidationPropertyDescriptor("FirstName", CountryFieldValidatorType.FirstName);

	public static CountryFieldValidationPropertyDescriptor s_LastName = new CountryFieldValidationPropertyDescriptor("LastName", CountryFieldValidatorType.LastName);

	public static PhoneNumberDescriptors s_PhoneNumber = new PhoneNumberDescriptors("PhoneNumber", CountryFieldValidatorType.PhoneNumber);

	public static CountryFieldValidationPropertyDescriptor s_PhoneExtension = new CountryFieldValidationPropertyDescriptor("PhoneExtension", CountryFieldValidatorType.PhoneExtension);

	public static EmailPropertyDescriptor s_Email = new EmailPropertyDescriptor("Email", string.Empty, string.Empty, required: true);

	public static PropertyDescriptor FirstName => s_FirstName;

	public static PropertyDescriptor LastName => s_LastName;

	public static PropertyDescriptor PhoneNumber => s_PhoneNumber;

	public static PropertyDescriptor PhoneExtension => s_PhoneExtension;

	public static PropertyDescriptor Email => s_Email;

	public BaseContactInfoPropertyEditor()
	{
	}
}
