using Microsoft.Iris;

namespace ZuneUI;

public class DeviceRestoreSummaryPage : DeviceRestorePage
{
	public override string UI => "res://ZuneShellResources!DeviceRestore.uix#DeviceRestoreSummaryPage";

	internal DeviceRestoreSummaryPage(DeviceRestoreWizard wizard)
		: base(wizard)
	{
		((ModelItem)this).Description = Shell.LoadString(StringId.IDS_DEVICE_RESTORE_SUCCESS);
	}
}
