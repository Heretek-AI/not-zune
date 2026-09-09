using System.Collections;
using Microsoft.Iris;

namespace ZuneUI;

public class SubscriptionEpisodePanel : ListPanel
{
	private ArrayList _selectedLibraryIds = new ArrayList();

	public override IList SelectedLibraryIds => _selectedLibraryIds;

	public SubscriptionEpisodePanel(SubscriptionLibraryPage page)
		: base((IModelItemOwner)(object)page)
	{
	}
}
