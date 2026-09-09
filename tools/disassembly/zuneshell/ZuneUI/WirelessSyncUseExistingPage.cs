using Microsoft.Iris;

namespace ZuneUI;

public class WirelessSyncUseExistingPage : WizardPage
{
	public override string UI => "res://ZuneShellResources!WirelessSync.uix#WirelessSyncUseExistingPage";

	internal WirelessSyncUseExistingPage(WirelessSyncWizard wizard)
		: base(wizard)
	{
		((ModelItem)this).Description = Shell.LoadString(StringId.IDS_WIRELESS_SYNC_START_CONFIG);
	}
}
