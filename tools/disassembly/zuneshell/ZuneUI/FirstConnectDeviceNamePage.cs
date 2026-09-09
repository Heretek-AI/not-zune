using Microsoft.Iris;

namespace ZuneUI;

public class FirstConnectDeviceNamePage : FirstConnectPage
{
	public override string UI => "res://ZuneShellResources!FirstConnect.uix#FirstConnectDeviceNamePage";

	internal FirstConnectDeviceNamePage(Wizard wizard)
		: base(wizard)
	{
		((ModelItem)this).Description = Shell.LoadString(StringId.IDS_NAME_ZUNE_HEADER);
	}

	internal override bool OnMovingNext()
	{
		bool result = base.OnMovingNext();
		if (_owner is FirstConnectForPhoneWizard || _owner is FirstLaunchForPhoneWizard)
		{
			_owner.CommitChanges();
		}
		return result;
	}
}
