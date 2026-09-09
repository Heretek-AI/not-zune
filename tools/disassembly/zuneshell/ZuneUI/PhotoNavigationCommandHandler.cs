using System.Collections;

namespace ZuneUI;

public class PhotoNavigationCommandHandler : DeviceAwareNavigationHandler
{
	protected override ZunePage GetPage(IDictionary args)
	{
		return new PhotoLibraryPage(base.ShowDeviceContents);
	}
}
