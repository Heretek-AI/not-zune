namespace ZuneUI;

public class ZuneTagPropertyEditor : WizardPropertyEditor
{
	private static PropertyDescriptor[] s_dataProviderProperties;

	public static PropertyDescriptor s_ZuneTag = new ZuneTagPropertyDescriptor("ZuneTag", string.Empty, string.Empty);

	public override PropertyDescriptor[] PropertyDescriptors
	{
		get
		{
			if (s_dataProviderProperties == null)
			{
				s_dataProviderProperties = new PropertyDescriptor[1] { s_ZuneTag };
			}
			return s_dataProviderProperties;
		}
	}

	public static PropertyDescriptor ZuneTag => s_ZuneTag;
}
