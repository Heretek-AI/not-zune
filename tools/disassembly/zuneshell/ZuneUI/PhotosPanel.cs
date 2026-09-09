using System.Collections;
using Microsoft.Iris;

namespace ZuneUI;

public class PhotosPanel : ListPanel
{
	protected new PhotoLibraryPage LibraryPage => base.LibraryPage as PhotoLibraryPage;

	public override IList SelectedLibraryIds
	{
		get
		{
			return LibraryPage.SelectedPhotoIds;
		}
		set
		{
			LibraryPage.SelectedPhotoIds = value;
		}
	}

	public PhotosPanel(PhotoLibraryPage library)
		: base((IModelItemOwner)(object)library)
	{
	}
}
