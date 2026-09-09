using Microsoft.Iris;

namespace ZuneUI;

public class WirelessSyncSummaryPage : WizardPage
{
	public override string UI => "res://ZuneShellResources!WirelessSync.uix#WirelessSyncSummaryPage";

	internal WirelessSyncSummaryPage(WirelessSyncWizard wizard)
		: base(wizard)
	{
		((ModelItem)this).Description = Shell.LoadString(StringId.IDS_WIRELESS_SYNC_START_CONFIG);
	}
}
