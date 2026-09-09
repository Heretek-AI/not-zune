using Microsoft.Iris;

namespace ZuneUI;

public class DeviceUpdateCheckingPage : DeviceUpdatePage
{
	public override bool IsEnabled => base.Wizard.ActiveDevice.SupportsBrandingType(DeviceBranding.WindowsPhone);

	public override string UI => "res://ZuneShellResources!DeviceUpdate.uix#DeviceUpdateCheckingPage";

	internal DeviceUpdateCheckingPage(DeviceUpdateWizard wizard)
		: base(wizard)
	{
		((ModelItem)this).Description = Shell.LoadString(StringId.IDS_PHONE_UPDATE_CHECKING_TITLE);
	}

	internal override bool OnMovingNext()
	{
		if (SoftwareUpdates.Instance.LastUpdateCheckResult != null && SoftwareUpdates.Instance.LastUpdateCheckResult.UpdateFound)
		{
			SoftwareUpdates.Instance.InstallUpdates();
			return false;
		}
		return base.OnMovingNext();
	}
}
