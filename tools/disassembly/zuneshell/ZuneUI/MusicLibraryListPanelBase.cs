using Microsoft.Iris;

namespace ZuneUI;

public class MusicLibraryListPanelBase : ListPanel
{
	protected new MusicLibraryPage LibraryPage => base.LibraryPage as MusicLibraryPage;

	internal MusicLibraryListPanelBase(MusicLibraryPage libraryPage)
		: base((IModelItemOwner)(object)libraryPage)
	{
	}
}
