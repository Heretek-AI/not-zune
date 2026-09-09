using Microsoft.Iris;

namespace ZuneUI;

public class DeviceRollbackProgressPage : DeviceRollbackPage
{
	public override string UI => "res://ZuneShellResources!DeviceRollback.uix#DeviceRollbackProgressPage";

	internal DeviceRollbackProgressPage(DeviceRollbackWizard wizard)
		: base(wizard)
	{
		((ModelItem)this).Description = Shell.LoadString(StringId.IDS_DEVICE_ROLLBACK_IN_PROGRESS);
	}
}
