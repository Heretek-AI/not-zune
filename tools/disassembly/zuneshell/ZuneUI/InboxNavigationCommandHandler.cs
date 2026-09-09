using System.Collections;

namespace ZuneUI;

public class InboxNavigationCommandHandler : NavigationCommandHandlerBase
{
	protected override ZunePage GetPage(IDictionary args)
	{
		return new InboxPage();
	}
}
