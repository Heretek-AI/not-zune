namespace ZuneUI;

public abstract class DeviceRollbackPage : WizardPage
{
	protected DeviceRollbackWizard Wizard => (DeviceRollbackWizard)_owner;

	public override bool CanCancel
	{
		get
		{
			if (Wizard.UIFirmwareUpdater != null)
			{
				return Wizard.UIFirmwareUpdater.CanCancel;
			}
			return false;
		}
	}

	protected DeviceRollbackPage(DeviceRollbackWizard wizard)
		: base(wizard)
	{
	}
}
