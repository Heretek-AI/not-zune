using Microsoft.Iris;

namespace ZuneUI;

public class DeviceUpdateGuestWarningPage : DeviceUpdatePage
{
	public override string UI => "res://ZuneShellResources!DeviceUpdate.uix#DeviceUpdateGuestWarningPage";

	public override bool IsEnabled => base.Wizard.ActiveDevice.SupportsBrandingType(DeviceBranding.WindowsPhone) && base.Wizard.ActiveDevice.IsGuest && !base.Wizard.IsChainedUpdate;

	public override bool CanCancel => true;

	internal DeviceUpdateGuestWarningPage(DeviceUpdateWizard wizard)
		: base(wizard)
	{
		((ModelItem)this).Description = Shell.LoadString(StringId.IDS_PHONE_UPDATE_GUEST_WARNING_TITLE);
	}
}
