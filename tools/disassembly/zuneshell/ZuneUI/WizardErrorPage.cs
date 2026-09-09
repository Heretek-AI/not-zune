using Microsoft.Iris;

namespace ZuneUI;

public class WizardErrorPage : WizardPage
{
	public override bool IsEnabled => _owner.ErrorPageIsEnabled;

	public override bool ShowClose => true;

	public override string UI => "res://ZuneShellResources!Wizard.uix#WizardErrorPage";

	internal WizardErrorPage(Wizard wizard)
		: base(wizard)
	{
		((ModelItem)this).Description = Shell.LoadString(StringId.IDS_GENERIC_ERROR);
	}
}
