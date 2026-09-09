namespace ZuneUI;

public class CreatePassportPropertyEditor : WizardPropertyEditor
{
	private static PropertyDescriptor[] s_dataProviderProperties;

	public static PropertyDescriptor s_PassportId = new PropertyDescriptor("Name", string.Empty, string.Empty, required: true);

	public static PropertyDescriptor s_PassportDomain = new EmailDomainPropertyDescriptor("Domain", string.Empty, string.Empty);

	public static PropertyDescriptor s_Password1 = new NonBlankPropertyDescriptor("Password", string.Empty, string.Empty, 6, required: true);

	public static PropertyDescriptor s_Password2 = new NonBlankPropertyDescriptor("Password2", string.Empty, string.Empty, 6, required: true);

	public static PropertyDescriptor s_SecretQuestion = new PropertyDescriptor("SecretQuestion", string.Empty, string.Empty, required: true);

	public static NonBlankPropertyDescriptor s_SecretAnswer = new NonBlankPropertyDescriptor("SecretAnswer", string.Empty, string.Empty, 5, required: true);

	public override PropertyDescriptor[] PropertyDescriptors
	{
		get
		{
			if (s_dataProviderProperties == null)
			{
				s_dataProviderProperties = new PropertyDescriptor[6] { s_PassportId, s_PassportDomain, s_Password1, s_Password2, s_SecretQuestion, s_SecretAnswer };
			}
			return s_dataProviderProperties;
		}
	}

	public static PropertyDescriptor PassportId => s_PassportId;

	public static PropertyDescriptor PassportDomain => s_PassportDomain;

	public static PropertyDescriptor Password1 => s_Password1;

	public static PropertyDescriptor Password2 => s_Password2;

	public static PropertyDescriptor SecretQuestion => s_SecretQuestion;

	public static NonBlankPropertyDescriptor SecretAnswer => s_SecretAnswer;

	public override bool IsValid()
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0005: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		//IL_0075: Unknown result type (might be due to invalid IL or missing references)
		//IL_0063: Unknown result type (might be due to invalid IL or missing references)
		//IL_0068: Unknown result type (might be due to invalid IL or missing references)
		HRESULT externalError = HRESULT._S_OK;
		MetadataEditProperty property = GetProperty(s_Password1);
		MetadataEditProperty property2 = GetProperty(s_Password2);
		if (property.Value != property2.Value)
		{
			externalError = HRESULT._ZUNE_E_SIGNUP_PASSWORDS_DONT_MATCH;
		}
		if (((HRESULT)(ref externalError)).IsError || property.ExternalError == HRESULT._ZUNE_E_SIGNUP_INVALID_PARENT_EMAIL)
		{
			property.ExternalError = externalError;
		}
		if (((HRESULT)(ref externalError)).IsError || property2.ExternalError == HRESULT._ZUNE_E_SIGNUP_INVALID_PARENT_EMAIL)
		{
			property2.ExternalError = externalError;
		}
		return base.IsValid();
	}
}
