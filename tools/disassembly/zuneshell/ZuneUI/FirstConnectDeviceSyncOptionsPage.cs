using Microsoft.Iris;

namespace ZuneUI;

public class FirstConnectDeviceSyncOptionsPage : FirstConnectPage
{
	public override string UI => "res://ZuneShellResources!FirstConnect.uix#FirstConnectDeviceSyncOptionsPage";

	internal FirstConnectDeviceSyncOptionsPage(Wizard wizard)
		: base(wizard)
	{
		((ModelItem)this).Description = Shell.LoadString(StringId.IDS_CHOOSE_MEDIA_TYPES_TO_SYNC);
	}
}
