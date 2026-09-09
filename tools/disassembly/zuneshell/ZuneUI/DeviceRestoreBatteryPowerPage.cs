using Microsoft.Iris;

namespace ZuneUI;

public class DeviceRestoreBatteryPowerPage : WizardPage
{
	public override bool IsEnabled
	{
		get
		{
			DeviceRestoreWizard deviceRestoreWizard = (DeviceRestoreWizard)_owner;
			if (deviceRestoreWizard.UIFirmwareRestorer == null)
			{
				return true;
			}
			return deviceRestoreWizard.UIFirmwareRestorer.IsOnBatteryPower;
		}
	}

	public override string UI => "res://ZuneShellResources!DeviceRestore.uix#DeviceRestoreBatteryPowerPage";

	internal DeviceRestoreBatteryPowerPage(DeviceRestoreWizard wizard)
		: base(wizard)
	{
		((ModelItem)this).Description = Shell.LoadString(StringId.IDS_AC_POWER_WARNING_TITLE);
	}
}
