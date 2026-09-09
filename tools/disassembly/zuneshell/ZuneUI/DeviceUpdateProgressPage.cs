using Microsoft.Iris;

namespace ZuneUI;

public class DeviceUpdateProgressPage : DeviceUpdatePage
{
	public override string UI => "res://ZuneShellResources!DeviceUpdate.uix#DeviceUpdateProgressPage";

	internal DeviceUpdateProgressPage(DeviceUpdateWizard wizard)
		: base(wizard)
	{
		((ModelItem)this).Description = Shell.LoadString(StringId.IDS_FIRMWARE_UPDATE_IN_PROGRESS_HEADER);
	}
}
