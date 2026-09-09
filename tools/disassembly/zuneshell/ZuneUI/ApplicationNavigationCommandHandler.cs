using System.Collections;

namespace ZuneUI;

public class ApplicationNavigationCommandHandler : DeviceAwareNavigationHandler
{
	protected override ZunePage GetPage(IDictionary args)
	{
		return new ApplicationLibraryPage(base.ShowDeviceContents);
	}
}
