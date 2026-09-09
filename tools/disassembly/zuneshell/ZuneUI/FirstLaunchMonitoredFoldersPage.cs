using Microsoft.Iris;
using Microsoft.Zune.Util;

namespace ZuneUI;

public class FirstLaunchMonitoredFoldersPage : FirstLaunchSoftwareSettingBase
{
	private bool _activated;

	public override string UI => "res://ZuneShellResources!FirstLaunch.uix#FirstLaunchMonitoredFoldersPage";

	internal FirstLaunchMonitoredFoldersPage(Wizard wizard)
		: base(wizard)
	{
		((ModelItem)this).Description = Shell.LoadString(StringId.IDS_CHOOSE_MEDIA_LOCATIONS);
		base.EnableVerticalScrolling = true;
	}

	internal override void Activate()
	{
		if (!_activated)
		{
			_activated = true;
			bool value = FeatureEnablement.IsFeatureEnabled((Features)25);
			ZuneShell.DefaultInstance.Management.MediaInfoChoice.Value = value;
		}
		base.Activate();
	}
}
