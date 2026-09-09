using Microsoft.Iris;

namespace ZuneUI;

public class DeviceRestoreConfirmationPage : DeviceRestorePage
{
	public override string UI => "res://ZuneShellResources!DeviceRestore.uix#DeviceRestoreConfirmationPage";

	internal DeviceRestoreConfirmationPage(DeviceRestoreWizard wizard)
		: base(wizard)
	{
		((ModelItem)this).Description = Shell.LoadString(StringId.IDS_DEVICE_RESTORE_YOUR_PHONE);
	}
}
