using Microsoft.Iris;

namespace ZuneUI;

public class DeviceUpdateEULAPage : DeviceUpdatePage
{
	public override string UI => "res://ZuneShellResources!DeviceUpdate.uix#DeviceUpdateEULAPage";

	public override bool IsEnabled
	{
		get
		{
			bool result = true;
			if (base.Wizard.ActiveDevice.SupportsBrandingType(DeviceBranding.WindowsPhone))
			{
				result = !base.Wizard.IsChainedUpdate && base.Wizard.UIFirmwareUpdater != null && !string.IsNullOrEmpty(base.Wizard.UIFirmwareUpdater.NewFirmwareEULAContent);
			}
			return result;
		}
	}

	internal DeviceUpdateEULAPage(DeviceUpdateWizard wizard)
		: base(wizard)
	{
		((ModelItem)this).Description = Shell.LoadString(SyncControls.Instance.CurrentDeviceOverride.RequiresFirmwareUpdate ? StringId.IDS_EULA_DIALOG_TITLE_REQUIRED : StringId.IDS_EULA_DIALOG_TITLE);
	}
}
