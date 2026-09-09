using Microsoft.Iris;

namespace ZuneUI;

public class FirstLaunchWelcomePage : WizardPage
{
	public override bool ShowNavigation => false;

	public override bool CanCancel => false;

	public override string UI => "res://ZuneShellResources!FirstLaunch.uix#FirstLaunchWelcomePage";

	internal FirstLaunchWelcomePage(Wizard wizard)
		: base(wizard)
	{
		((ModelItem)this).Description = Shell.LoadString(StringId.IDS_FIRSTRUN_GREETING);
		base.EnableVerticalScrolling = true;
	}
}
