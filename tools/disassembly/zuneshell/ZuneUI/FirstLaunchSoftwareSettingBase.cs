namespace ZuneUI;

public abstract class FirstLaunchSoftwareSettingBase : WizardPage
{
	public override bool IsEnabled
	{
		get
		{
			if (_owner is FirstLaunchForPhoneWizard)
			{
				FirstLaunchForPhoneWizard firstLaunchForPhoneWizard = (FirstLaunchForPhoneWizard)_owner;
				return firstLaunchForPhoneWizard.IsSoftwareSettingsEnabled;
			}
			return true;
		}
	}

	internal FirstLaunchSoftwareSettingBase(Wizard wizard)
		: base(wizard)
	{
	}
}
