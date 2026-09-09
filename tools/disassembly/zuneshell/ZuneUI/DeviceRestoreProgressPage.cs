using Microsoft.Iris;

namespace ZuneUI;

public class DeviceRestoreProgressPage : DeviceRestorePage
{
	public override string UI => "res://ZuneShellResources!DeviceRestore.uix#DeviceRestoreProgressPage";

	internal DeviceRestoreProgressPage(DeviceRestoreWizard wizard)
		: base(wizard)
	{
		((ModelItem)this).Description = Shell.LoadString(StringId.IDS_DEVICE_RESTORE_IN_PROGRESS);
	}
}
