using System.Collections;

namespace ZuneUI;

public class VideoNavigationCommandHandler : DeviceAwareNavigationHandler
{
	private VideoLibraryView _view = VideoLibraryView.Invalid;

	public VideoLibraryView View
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
		return new VideoLibraryPage(base.ShowDeviceContents, _view);
	}
}
