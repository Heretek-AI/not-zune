using System.Collections;

namespace ZuneUI;

public class TracksPanel : MusicLibraryListPanelBase
{
	public override IList SelectedLibraryIds
	{
		get
		{
			return base.LibraryPage.SelectedTrackIds;
		}
		set
		{
			base.LibraryPage.SelectedTrackIds = value;
		}
	}

	internal TracksPanel(MusicLibraryPage libraryPage)
		: base(libraryPage)
	{
	}
}
