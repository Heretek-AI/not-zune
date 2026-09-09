using Microsoft.Iris;

namespace ZuneUI;

public class DeviceUpdateSummaryPage : DeviceUpdatePage
{
	public override string UI => "res://ZuneShellResources!DeviceUpdate.uix#DeviceUpdateSummaryPage";

	internal DeviceUpdateSummaryPage(DeviceUpdateWizard wizard)
		: base(wizard)
	{
		((ModelItem)this).Description = Shell.LoadString(StringId.IDS_FIRMWARE_UPDATE_SUCCESSFUL_HEADER);
	}
}
