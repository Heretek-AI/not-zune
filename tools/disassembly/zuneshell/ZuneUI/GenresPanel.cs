using System.Collections;

namespace ZuneUI;

public class GenresPanel : MusicLibraryListPanelBase
{
	public override MediaType MediaType => MediaType.Genre;

	public override IList SelectedLibraryIds
	{
		get
		{
			return base.LibraryPage.SelectedGenreIds;
		}
		set
		{
			base.LibraryPage.SelectedGenreIds = value;
		}
	}

	internal GenresPanel(MusicLibraryPage libraryPage)
		: base(libraryPage)
	{
	}
}
