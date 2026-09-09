namespace ZuneUI;

public class HipPropertyEditor : WizardPropertyEditor
{
	private static PropertyDescriptor[] s_dataProviderProperties;

	public static PropertyDescriptor s_HipCharacters = new PropertyDescriptor("HIPSolution", string.Empty, string.Empty, required: true);

	public override PropertyDescriptor[] PropertyDescriptors
	{
		get
		{
			if (s_dataProviderProperties == null)
			{
				s_dataProviderProperties = new PropertyDescriptor[1] { s_HipCharacters };
			}
			return s_dataProviderProperties;
		}
	}

	public static PropertyDescriptor HipCharacters => s_HipCharacters;
}
