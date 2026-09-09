namespace ZuneUI;

public class ContactInfoParentPropertyEditor : BaseContactInfoPropertyEditor
{
	private static PropertyDescriptor[] s_dataProviderProperties;

	public static BirthdayPropertyDescriptor s_Birthday = new BirthdayPropertyDescriptor("Birthday", string.Empty, string.Empty, required: true);

	public static CountryPropertyDescriptor s_Country = new CountryPropertyDescriptor("Country", string.Empty, string.Empty, required: true);

	public override PropertyDescriptor[] PropertyDescriptors
	{
		get
		{
			if (s_dataProviderProperties == null)
			{
				s_dataProviderProperties = new PropertyDescriptor[7]
				{
					BaseContactInfoPropertyEditor.s_FirstName,
					BaseContactInfoPropertyEditor.s_LastName,
					BaseContactInfoPropertyEditor.s_PhoneNumber,
					BaseContactInfoPropertyEditor.s_PhoneExtension,
					BaseContactInfoPropertyEditor.s_Email,
					s_Birthday,
					s_Country
				};
			}
			return s_dataProviderProperties;
		}
	}

	public static PropertyDescriptor Birthday => s_Birthday;

	public static PropertyDescriptor Country => s_Country;
}
