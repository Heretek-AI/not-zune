using Microsoft.Iris;

namespace ZuneUI;

public class DeviceRollbackSummaryPage : DeviceRollbackPage
{
	public override string UI => "res://ZuneShellResources!DeviceRollback.uix#DeviceRollbackSummaryPage";

	internal DeviceRollbackSummaryPage(DeviceRollbackWizard wizard)
		: base(wizard)
	{
		((ModelItem)this).Description = Shell.LoadString(StringId.IDS_DEVICE_ROLLBACK_COMPLETE_TITLE);
	}
}
