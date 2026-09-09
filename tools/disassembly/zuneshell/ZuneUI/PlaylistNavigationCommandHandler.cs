using System.Collections;

namespace ZuneUI;

public class PlaylistNavigationCommandHandler : DeviceAwareNavigationHandler
{
	protected override ZunePage GetPage(IDictionary args)
	{
		return new MusicLibraryPage(base.ShowDeviceContents, MusicLibraryView.Playlist);
	}
}
