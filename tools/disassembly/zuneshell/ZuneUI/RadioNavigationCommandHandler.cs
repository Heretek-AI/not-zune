using System.Collections;

namespace ZuneUI;

public class RadioNavigationCommandHandler : NavigationCommandHandlerBase
{
	protected override ZunePage GetPage(IDictionary args)
	{
		return new RadioPage();
	}
}
