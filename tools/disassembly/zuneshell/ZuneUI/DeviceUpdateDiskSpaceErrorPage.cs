using Microsoft.Iris;
using MicrosoftZuneLibrary;

namespace ZuneUI;

public class DeviceUpdateDiskSpaceErrorPage : DeviceUpdatePage
{
	public override string UI => "res://ZuneShellResources!DeviceUpdate.uix#DeviceUpdateDiskSpaceErrorPage";

	public override bool IsEnabled
	{
		get
		{
			if (base.Wizard.ActiveDevice.SupportsBrandingType(DeviceBranding.WindowsPhone))
			{
				return base.Wizard.UIFirmwareUpdater != null;
			}
			return false;
		}
	}

	public override bool CanCancel => true;

	internal DeviceUpdateDiskSpaceErrorPage(DeviceUpdateWizard wizard)
		: base(wizard)
	{
		((ModelItem)this).Description = Shell.LoadString(StringId.IDS_FIRMWARE_UPDATE_IN_PROGRESS_HEADER);
	}

	internal override void Activate()
	{
		base.Activate();
		if (base.Wizard.ActiveDevice.SkipFutureBackupRequests)
		{
			base.Wizard.UIFirmwareUpdater.UpdateOption = (FirmwareUpdateOption)1;
			base.Wizard.MoveNext();
		}
		else
		{
			base.Wizard.UIFirmwareUpdater.StartCheckForDiskSpace();
		}
	}
}
