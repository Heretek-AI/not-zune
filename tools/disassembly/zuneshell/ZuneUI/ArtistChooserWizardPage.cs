using Microsoft.Iris;

namespace ZuneUI;

public class ArtistChooserWizardPage : WizardPage
{
	protected ArtistChooserWizard Wizard => (ArtistChooserWizard)_owner;

	public override string UI => "res://ZuneShellResources!ArtistChooser.uix#ArtistChooserWizardPage";

	internal ArtistChooserWizardPage(ArtistChooserWizard wizard)
		: base(wizard)
	{
		((ModelItem)this).Description = Shell.LoadString(StringId.IDS_ArtistChooserTitle);
	}
}
