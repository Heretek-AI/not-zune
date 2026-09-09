using Microsoft.Iris;

namespace ZuneUI;

public class DeviceUpdateSyncingPage : DeviceUpdatePage
{
	private bool _isLockedAgainstSyncing;

	public override string UI => "res://ZuneShellResources!DeviceUpdate.uix#DeviceUpdateSyncingPage";

	public override bool IsEnabled => base.Wizard.ActiveDevice.SupportsBrandingType(DeviceBranding.WindowsPhone) && !base.Wizard.ActiveDevice.IsGuest && base.Wizard.UIFirmwareUpdater != null && base.Wizard.UIFirmwareUpdater.RequiresSyncBeforeUpdate && !base.Wizard.IsChainedUpdate;

	public override bool CanCancel => true;

	internal DeviceUpdateSyncingPage(DeviceUpdateWizard wizard)
		: base(wizard)
	{
		((ModelItem)this).Description = Shell.LoadString(StringId.IDS_FIRMWARE_UPDATE_IN_PROGRESS_HEADER);
	}

	internal override void Activate()
	{
		base.Activate();
		_isLockedAgainstSyncing = base.Wizard.ActiveDevice.IsLockedAgainstSyncing;
	}

	internal override void Deactivate()
	{
		base.Wizard.ActiveDevice.EndSync();
		base.Wizard.ActiveDevice.IsLockedAgainstSyncing = _isLockedAgainstSyncing;
		base.Deactivate();
	}

	public bool BeginSync()
	{
		if (base.Wizard.ActiveDevice.IsReadyForSync)
		{
			base.Wizard.ActiveDevice.IsLockedAgainstSyncing = false;
			base.Wizard.ActiveDevice.BeginSync(userInitiated: true, syncOnNextNotify: false);
			return true;
		}
		return false;
	}
}
