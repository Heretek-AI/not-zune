using Microsoft.Iris;

namespace ZuneUI;

public class FirstLaunchForPhoneWelcomePage : WizardPage
{
	private Choice _welcomeOptionsChoice;

	public override string UI => "res://ZuneShellResources!FirstLaunch.uix#FirstLaunchForPhoneWelcomePage";

	public Choice WelcomeOptionsChoice
	{
		get
		{
			//IL_000f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0019: Expected O, but got Unknown
			if (_welcomeOptionsChoice == null)
			{
				_welcomeOptionsChoice = new Choice((IModelItemOwner)(object)this);
				_welcomeOptionsChoice.Options = new Command[2]
				{
					new RadioOptionWithSecondaryText((IModelItemOwner)(object)this, Shell.LoadString(StringId.IDS_PHONE_WELCOME_DEFAULT_HEADER), Shell.LoadString(StringId.IDS_PHONE_WELCOME_DEFAULT_TEXT)),
					new RadioOptionWithSecondaryText((IModelItemOwner)(object)this, Shell.LoadString(StringId.IDS_PHONE_WELCOME_CONFIG_HEADER), Shell.LoadString(StringId.IDS_PHONE_WELCOME_CONFIG_TEXT))
				};
				_welcomeOptionsChoice.Clear();
				_welcomeOptionsChoice.ChosenChanged += delegate
				{
					FirstLaunchForPhoneWizard firstLaunchForPhoneWizard = (FirstLaunchForPhoneWizard)_owner;
					firstLaunchForPhoneWizard.IsSoftwareSettingsEnabled = _welcomeOptionsChoice.ChosenIndex == 1;
				};
			}
			return _welcomeOptionsChoice;
		}
	}

	internal FirstLaunchForPhoneWelcomePage(Wizard wizard)
		: base(wizard)
	{
		((ModelItem)this).Description = Shell.LoadString(StringId.IDS_PHONE_WELCOME_TITLE);
		base.EnableVerticalScrolling = true;
		base.CanNavigateInto = false;
	}

	internal override bool OnMovingNext()
	{
		if (_welcomeOptionsChoice.ChosenIndex == 1)
		{
			Fue.Instance.ProxyDefaultPaths();
		}
		return base.OnMovingNext();
	}
}
