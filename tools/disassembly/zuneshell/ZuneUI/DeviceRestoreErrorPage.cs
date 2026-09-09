using Microsoft.Iris;

namespace ZuneUI;

public class DeviceRestoreErrorPage : WizardErrorPage
{
	public override string UI => "res://ZuneShellResources!DeviceRestore.uix#DeviceRestoreErrorPage";

	internal DeviceRestoreErrorPage(DeviceRestoreWizard wizard)
		: base(wizard)
	{
		((ModelItem)this).Description = Shell.LoadString(StringId.IDS_DEVICE_RESTORE_ERROR_TITLE);
	}
}
