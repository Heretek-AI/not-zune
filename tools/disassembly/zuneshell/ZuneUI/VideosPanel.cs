using System.Collections;
using Microsoft.Iris;

namespace ZuneUI;

public class VideosPanel : ListPanel
{
	protected new VideoLibraryPage LibraryPage => base.LibraryPage as VideoLibraryPage;

	public override IList SelectedLibraryIds
	{
		get
		{
			return LibraryPage.SelectedVideoIds;
		}
		set
		{
			LibraryPage.SelectedVideoIds = value;
		}
	}

	internal VideosPanel(VideoLibraryPage library)
		: base((IModelItemOwner)(object)library)
	{
	}
}
