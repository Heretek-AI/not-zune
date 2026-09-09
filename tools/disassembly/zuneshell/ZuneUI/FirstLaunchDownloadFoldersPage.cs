using Microsoft.Iris;

namespace ZuneUI;

public class FirstLaunchDownloadFoldersPage : FirstLaunchSoftwareSettingBase
{
	public override bool IsEnabled
	{
		get
		{
			if (ZuneShell.DefaultInstance.Management.UsingWin7Libraries)
			{
				return false;
			}
			if (_owner is FirstLaunchForPhoneWizard)
			{
				FirstLaunchForPhoneWizard firstLaunchForPhoneWizard = (FirstLaunchForPhoneWizard)_owner;
				return firstLaunchForPhoneWizard.IsSoftwareSettingsEnabled;
			}
			return true;
		}
	}

	public override string UI => "res://ZuneShellResources!FirstLaunch.uix#FirstLaunchDownloadFoldersPage";

	internal FirstLaunchDownloadFoldersPage(Wizard wizard)
		: base(wizard)
	{
		((ModelItem)this).Description = Shell.LoadString(StringId.IDS_CHANGE_WHERE_ZUNE_STORES_MEDIA);
		base.EnableVerticalScrolling = true;
	}
}
