using Microsoft.Iris;

namespace ZuneUI;

public class SubscriptionSeriesPanel : ListPanel
{
	public SubscriptionSeriesPanel(SubscriptionLibraryPage page)
		: base((IModelItemOwner)(object)page)
	{
	}
}
