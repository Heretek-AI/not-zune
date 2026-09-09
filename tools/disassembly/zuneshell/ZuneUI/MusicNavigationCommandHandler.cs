using System.Collections;

namespace ZuneUI;

public class MusicNavigationCommandHandler : DeviceAwareNavigationHandler
{
	private MusicLibraryView _view;

	public MusicLibraryView View
	{
		get
		{
			return _view;
		}
		set
		{
			_view = value;
		}
	}

	protected override ZunePage GetPage(IDictionary args)
	{
		return new MusicLibraryPage(base.ShowDeviceContents, _view);
	}
}
