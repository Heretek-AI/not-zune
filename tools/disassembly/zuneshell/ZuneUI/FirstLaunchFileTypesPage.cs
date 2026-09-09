using Microsoft.Iris;

namespace ZuneUI;

public class FirstLaunchFileTypesPage : FirstLaunchSoftwareSettingBase
{
	public override string UI => "res://ZuneShellResources!FirstLaunch.uix#FirstLaunchFileTypesPage";

	internal FirstLaunchFileTypesPage(Wizard wizard)
		: base(wizard)
	{
		((ModelItem)this).Description = Shell.LoadString(StringId.IDS_ASSOCIATE_FILE_TYPES);
		base.EnableVerticalScrolling = true;
	}
}
