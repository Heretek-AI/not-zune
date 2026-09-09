using Microsoft.Iris;

namespace ZuneUI;

public class DeviceRollbackErrorPage : WizardErrorPage
{
	public override string UI => "res://ZuneShellResources!DeviceRollback.uix#DeviceRollbackErrorPage";

	internal DeviceRollbackErrorPage(DeviceRollbackWizard wizard)
		: base(wizard)
	{
		((ModelItem)this).Description = Shell.LoadString(StringId.IDS_DEVICE_RESTORE_ERROR_TITLE);
	}
}
