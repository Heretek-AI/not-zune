using System.Collections;

namespace ZuneUI;

public class DownloadsNavigationCommandHandler : NavigationCommandHandlerBase
{
	public bool _collection;

	public bool Collection
	{
		get
		{
			return _collection;
		}
		set
		{
			_collection = value;
		}
	}

	protected override ZunePage GetPage(IDictionary args)
	{
		return new DownloadsPage(_collection);
	}
}
