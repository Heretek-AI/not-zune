using System.Collections;

namespace ZuneUI;

public class ChannelsNavigationCommandHandler : DeviceAwareNavigationHandler
{
	protected override ZunePage GetPage(IDictionary args)
	{
		return new ChannelLibraryPage(base.ShowDeviceContents);
	}
}
