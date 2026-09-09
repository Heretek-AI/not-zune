namespace ZuneUI;

public class EmailSelectionPropertyEditor : WizardPropertyEditor
{
	private static PropertyDescriptor[] s_dataProviderProperties;

	public static PropertyDescriptor s_Email = new EmailPropertyDescriptor("Email", string.Empty, string.Empty, required: true);

	public override PropertyDescriptor[] PropertyDescriptors
	{
		get
		{
			if (s_dataProviderProperties == null)
			{
				s_dataProviderProperties = new PropertyDescriptor[1] { s_Email };
			}
			return s_dataProviderProperties;
		}
	}

	public static PropertyDescriptor Email => s_Email;
}
