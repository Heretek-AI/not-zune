using Microsoft.Iris;

namespace ZuneUI;

public class DeviceUpdateBatteryPowerPage : DeviceUpdatePage
{
	public override bool IsEnabled
	{
		get
		{
			DeviceUpdateWizard deviceUpdateWizard = (DeviceUpdateWizard)_owner;
			return !deviceUpdateWizard.IsChainedUpdate && (deviceUpdateWizard.UIFirmwareUpdater == null || deviceUpdateWizard.UIFirmwareUpdater.IsOnBatteryPower);
		}
	}

	public override string UI => "res://ZuneShellResources!DeviceUpdate.uix#DeviceUpdateBatteryPowerPage";

	internal DeviceUpdateBatteryPowerPage(DeviceUpdateWizard wizard)
		: base(wizard)
	{
		((ModelItem)this).Description = Shell.LoadString(StringId.IDS_AC_POWER_WARNING_TITLE);
	}
}
