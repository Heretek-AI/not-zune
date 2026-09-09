using System.Collections;

namespace ZuneUI;

public class AlbumsPanel : MusicLibraryListPanelBase
{
	public override MediaType MediaType => MediaType.Album;

	public override IList SelectedLibraryIds
	{
		get
		{
			return base.LibraryPage.SelectedAlbumIds;
		}
		set
		{
			base.LibraryPage.SelectedAlbumIds = value;
		}
	}

	internal AlbumsPanel(MusicLibraryPage libraryPage)
		: base(libraryPage)
	{
	}
}
