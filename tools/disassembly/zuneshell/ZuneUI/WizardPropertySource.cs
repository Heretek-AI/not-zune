namespace ZuneUI;

public class WizardPropertySource : PropertySource
{
	private static PropertySource _instance;

	public static PropertySource Instance
	{
		get
		{
			if (_instance == null)
			{
				_instance = new WizardPropertySource();
			}
			return _instance;
		}
	}

	private WizardPropertySource()
	{
	}

	public override object Get(object model, PropertyDescriptor property)
	{
		if (!(model is WizardPropertyEditorPage wizardPropertyEditorPage))
		{
			return null;
		}
		return wizardPropertyEditorPage.GetCommittedValue(property);
	}

	public override void Set(object model, PropertyDescriptor property, object value)
	{
		if (model is WizardPropertyEditorPage wizardPropertyEditorPage)
		{
			wizardPropertyEditorPage.SetCommittedValue(property, value);
		}
	}
}
