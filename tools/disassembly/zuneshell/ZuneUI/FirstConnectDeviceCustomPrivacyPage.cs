using Microsoft.Iris;

namespace ZuneUI;

public class FirstConnectDeviceCustomPrivacyPage : FirstConnectPage
{
	public override string UI => "res://ZuneShellResources!FirstConnect.uix#FirstConnectDeviceCustomPrivacyPage";

	internal FirstConnectDeviceCustomPrivacyPage(Wizard wizard)
		: base(wizard)
	{
		((ModelItem)this).Description = Shell.LoadString(StringId.IDS_SELECT_YOUR_PRIVACY_OPTIONS_HEADER);
	}
}
