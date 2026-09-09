using Microsoft.Iris;
using Microsoft.Zune.Util;

namespace ZuneUI;

public class MobileWirelessSyncConfirmPage : MobileWirelessSyncWizardPage
{
	public override string UI => "res://ZuneShellResources!MobileWirelessSync.uix#MobileWirelessSyncConfirmPage";

	public MobileWirelessSyncConfirmPage(Wizard owner)
		: base(owner)
	{
		((ModelItem)this).Description = Shell.LoadString(StringId.IDS_WIRELESS_MOBILE_WIZARD_CONFIRM_TITLE);
	}

	public override void OnCancel()
	{
		SQMLog.Log((SQMDataId)8, 1);
	}
}
