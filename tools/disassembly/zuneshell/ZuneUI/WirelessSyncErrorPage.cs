using Microsoft.Iris;

namespace ZuneUI;

public class WirelessSyncErrorPage : WizardErrorPage
{
	public override string UI => "res://ZuneShellResources!WirelessSync.uix#WirelessSyncErrorPage";

	internal WirelessSyncErrorPage(WirelessSyncWizard wizard)
		: base(wizard)
	{
		((ModelItem)this).Description = Shell.LoadString(StringId.IDS_WIRELESS_SYNC_GENERIC_ERROR);
	}
}
