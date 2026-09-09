using System.Collections;

namespace ZuneUI;

public class PodcastNavigationCommandHandler : DeviceAwareNavigationHandler
{
	protected override ZunePage GetPage(IDictionary args)
	{
		return new PodcastLibraryPage(base.ShowDeviceContents);
	}
}
