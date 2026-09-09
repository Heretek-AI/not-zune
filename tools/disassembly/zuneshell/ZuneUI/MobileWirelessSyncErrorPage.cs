using Microsoft.Iris;
using Microsoft.Zune.Util;

namespace ZuneUI;

public class MobileWirelessSyncErrorPage : WizardErrorPage
{
	public override string UI => "res://ZuneShellResources!MobileWirelessSync.uix#MobileWirelessSyncErrorPage";

	public MobileWirelessSyncErrorPage(Wizard wizard)
		: base(wizard)
	{
		((ModelItem)this).Description = Shell.LoadString(StringId.IDS_WIRELESS_MOBILE_WIZARD_TROUBLESHOOT);
	}

	public void OnCancel()
	{
		SQMLog.Log((SQMDataId)9, 1);
	}
}
