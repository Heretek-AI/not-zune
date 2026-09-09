using Microsoft.Iris;

namespace ZuneUI;

public class DeviceUpdateErrorPage : WizardErrorPage
{
	public override string UI => "res://ZuneShellResources!DeviceUpdate.uix#DeviceUpdateErrorPage";

	internal DeviceUpdateErrorPage(DeviceUpdateWizard wizard)
		: base(wizard)
	{
		((ModelItem)this).Description = Shell.LoadString(StringId.IDS_FIRMWARE_UPDATE_ERROR_TITLE);
	}
}
