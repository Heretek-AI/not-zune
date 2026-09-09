using System.Collections;

namespace ZuneUI;

public class QuickplayNavigationCommandHandler : NavigationCommandHandlerBase
{
	protected override ZunePage GetPage(IDictionary args)
	{
		return new QuickplayPage();
	}
}
