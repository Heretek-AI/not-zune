namespace ZuneUI;

public abstract class WizardPropertyEditor : MetadataEditMedia
{
	private WizardPropertyEditorPage _page;

	public abstract PropertyDescriptor[] PropertyDescriptors { get; }

	public WizardPropertyEditorPage Page => _page;

	protected WizardPropertyEditor()
	{
		_source = WizardPropertySource.Instance;
	}

	internal void Initialize(WizardPropertyEditorPage page)
	{
		_page = page;
		Initialize(new object[1] { page }, PropertyDescriptors);
	}
}
