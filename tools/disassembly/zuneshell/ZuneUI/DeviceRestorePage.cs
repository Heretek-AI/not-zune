namespace ZuneUI;

public abstract class DeviceRestorePage : WizardPage
{
	protected DeviceRestoreWizard Wizard => (DeviceRestoreWizard)_owner;

	protected DeviceRestorePage(DeviceRestoreWizard wizard)
		: base(wizard)
	{
	}
}
