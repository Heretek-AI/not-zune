using System.Collections;

namespace ZuneUI;

public class ProfileNavigationCommandHandler : NavigationCommandHandlerBase
{
	protected override ZunePage GetPage(IDictionary args)
	{
		return ProfilePage.CreateInstance(args);
	}
}
