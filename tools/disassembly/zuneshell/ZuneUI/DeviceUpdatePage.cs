namespace ZuneUI;

public abstract class DeviceUpdatePage : WizardPage
{
	protected DeviceUpdateWizard Wizard => (DeviceUpdateWizard)_owner;

	public override bool CanCancel
	{
		get
		{
			if (!Wizard.Disconnected)
			{
				if (Wizard.UIFirmwareUpdater != null)
				{
					return Wizard.UIFirmwareUpdater.CanCancel;
				}
				return false;
			}
			return true;
		}
	}

	protected DeviceUpdatePage(DeviceUpdateWizard wizard)
		: base(wizard)
	{
	}
}
