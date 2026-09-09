namespace ZuneUI;

public abstract class MobileWirelessSyncWizardPage : WizardPage
{
	public MobileWirelessSyncWizardPage(Wizard owner)
		: base(owner)
	{
	}

	public abstract void OnCancel();
}
