using System.Collections;

namespace ZuneUI;

public class CartNavigationCommandHandler : NavigationCommandHandlerBase
{
	protected override ZunePage GetPage(IDictionary args)
	{
		ZunePage zunePage = new CartPage();
		zunePage.UIPath = base.UIPath;
		return zunePage;
	}
}
