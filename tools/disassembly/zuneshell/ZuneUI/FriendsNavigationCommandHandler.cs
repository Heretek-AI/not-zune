using System.Collections;

namespace ZuneUI;

public class FriendsNavigationCommandHandler : DeviceAwareNavigationHandler
{
	protected override ZunePage GetPage(IDictionary args)
	{
		return FriendsPage.CreateInstance(base.ShowDeviceContents);
	}
}
