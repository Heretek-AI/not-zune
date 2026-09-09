using System.Collections;

namespace ZuneUI;

public class ArtistsPanel : MusicLibraryListPanelBase
{
	public override MediaType MediaType => MediaType.Artist;

	public override IList SelectedLibraryIds
	{
		get
		{
			return base.LibraryPage.SelectedArtistIds;
		}
		set
		{
			base.LibraryPage.SelectedArtistIds = value;
		}
	}

	internal ArtistsPanel(MusicLibraryPage libraryPage)
		: base(libraryPage)
	{
	}
}
