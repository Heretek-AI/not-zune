using System.Collections;

namespace ZuneUI;

public class StatusNavigationCommandHandler : NavigationCommandHandlerBase
{
	protected override ZunePage GetPage(IDictionary args)
	{
		return new Deviceland();
	}
}
