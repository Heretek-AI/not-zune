using Microsoft.Iris;
using Microsoft.Zune.Util;

namespace ZuneUI;

public class FirstLaunchPrivacyPage : FirstLaunchSoftwareSettingBase
{
	private bool _activated;

	public override string UI => "res://ZuneShellResources!FirstLaunch.uix#FirstLaunchPrivacyPage";

	internal FirstLaunchPrivacyPage(Wizard wizard)
		: base(wizard)
	{
		((ModelItem)this).Description = Shell.LoadString(StringId.IDS_CHANGE_PRIVACY_SETTINGS);
	}

	internal override void Activate()
	{
		if (!_activated)
		{
			_activated = true;
			bool value = FeatureEnablement.IsFeatureEnabled((Features)25);
			ZuneShell.DefaultInstance.Management.SqmChoice.Value = value;
		}
		base.Activate();
	}
}
