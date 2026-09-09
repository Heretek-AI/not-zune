using Microsoft.Iris;
using Microsoft.Zune.Util;

namespace ZuneUI;

public class MobileWirelessSyncSummaryPage : MobileWirelessSyncWizardPage
{
	public override string UI => "res://ZuneShellResources!MobileWirelessSync.uix#MobileWirelessSyncSummaryPage";

	public MobileWirelessSyncSummaryPage(Wizard owner)
		: base(owner)
	{
		((ModelItem)this).Description = Shell.LoadString(StringId.IDS_WIRELESS_MOBILE_WIZARD_CONFIRM_TITLE);
	}

	public override void OnCancel()
	{
		SQMLog.Log((SQMDataId)7, 1);
	}
}
