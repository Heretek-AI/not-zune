using Microsoft.Iris;

namespace ZuneUI;

public class DeviceRestoreSyncingPage : DeviceRestorePage
{
	private bool _isLockedAgainstSyncing;

	public override string UI => "res://ZuneShellResources!DeviceRestore.uix#DeviceRestoreSyncingPage";

	public override bool IsEnabled => base.Wizard.ActiveDevice.SupportsBrandingType(DeviceBranding.WindowsPhone) && !base.Wizard.ActiveDevice.IsGuest && base.Wizard.ActiveDevice.InStandardMode;

	public override bool CanCancel => true;

	internal DeviceRestoreSyncingPage(DeviceRestoreWizard wizard)
		: base(wizard)
	{
		((ModelItem)this).Description = Shell.LoadString(StringId.IDS_DEVICE_RESTORE_YOUR_PHONE);
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
