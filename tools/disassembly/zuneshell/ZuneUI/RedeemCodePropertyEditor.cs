namespace ZuneUI;

public class RedeemCodePropertyEditor : WizardPropertyEditor
{
	private static PropertyDescriptor[] s_dataProviderProperties;

	public static PrepaidCodePropertyDescriptor s_code = new PrepaidCodePropertyDescriptor("Code", string.Empty, string.Empty, required: true);

	public override PropertyDescriptor[] PropertyDescriptors
	{
		get
		{
			if (s_dataProviderProperties == null)
			{
				s_dataProviderProperties = new PropertyDescriptor[1] { s_code };
			}
			return s_dataProviderProperties;
		}
	}

	public static PropertyDescriptor Code => s_code;
}
