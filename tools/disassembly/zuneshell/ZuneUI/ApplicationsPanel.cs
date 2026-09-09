using System.Collections;
using Microsoft.Iris;

namespace ZuneUI;

public class ApplicationsPanel : ListPanel
{
	protected new ApplicationLibraryPage LibraryPage => base.LibraryPage as ApplicationLibraryPage;

	public override IList SelectedLibraryIds
	{
		get
		{
			return LibraryPage.SelectedApplicationIDs;
		}
		set
		{
			LibraryPage.SelectedApplicationIDs = value;
		}
	}

	internal ApplicationsPanel(ApplicationLibraryPage library)
		: base((IModelItemOwner)(object)library)
	{
	}
}
