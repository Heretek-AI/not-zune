namespace ZuneUI;

public class PassportPasswordPropertyEditor : WizardPropertyEditor
{
	private static PropertyDescriptor[] s_dataProviderProperties;

	public static PropertyDescriptor s_Email = new PropertyDescriptor("Email", string.Empty, string.Empty, required: true);

	public static PropertyDescriptor s_Password = new PropertyDescriptor("Password", string.Empty, string.Empty, required: true);

	public override PropertyDescriptor[] PropertyDescriptors
	{
		get
		{
			if (s_dataProviderProperties == null)
			{
				s_dataProviderProperties = new PropertyDescriptor[2] { s_Email, s_Password };
			}
			return s_dataProviderProperties;
		}
	}

	public static PropertyDescriptor Password => s_Password;

	public static PropertyDescriptor Email => s_Email;
}
