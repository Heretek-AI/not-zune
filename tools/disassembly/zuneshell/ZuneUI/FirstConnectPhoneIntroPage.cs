using Microsoft.Iris;

namespace ZuneUI;

public class FirstConnectPhoneIntroPage : FirstConnectPage
{
	public override bool IsEnabled
	{
		get
		{
			if (!(_owner is FirstLaunchForPhoneWizard))
			{
				return _owner is FirstConnectForPhoneWizard;
			}
			return true;
		}
	}

	public override string UI => "res://ZuneShellResources!FirstConnect.uix#FirstConnectPhoneIntroPage";

	internal FirstConnectPhoneIntroPage(Wizard wizard)
		: base(wizard)
	{
		((ModelItem)this).Description = Shell.LoadString(StringId.IDS_PHONE_INTRO_TITLE);
	}
}
