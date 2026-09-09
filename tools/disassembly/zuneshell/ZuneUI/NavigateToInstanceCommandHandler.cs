using System.Collections;

namespace ZuneUI;

public class NavigateToInstanceCommandHandler : NavigationCommandHandlerBase
{
	private ZunePage _page;

	public ZunePage Page
	{
		get
		{
			return _page;
		}
		set
		{
			_page = value;
		}
	}

	protected override ZunePage GetPage(IDictionary args)
	{
		return _page;
	}
}
