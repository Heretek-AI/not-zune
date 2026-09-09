using Microsoft.Iris;

namespace ZuneUI;

public class WirelessSyncChooseNetworkPage : WizardPage
{
	public override bool IsEnabled => WirelessSync.Instance.ExistingNetworkChoice.ChosenIndex == 1;

	public override string UI => "res://ZuneShellResources!WirelessSync.uix#WirelessSyncChooseNetworkPage";

	internal WirelessSyncChooseNetworkPage(WirelessSyncWizard wizard)
		: base(wizard)
	{
		((ModelItem)this).Description = Shell.LoadString(StringId.IDS_WIRELESS_SYNC_START_CONFIG);
	}
}
