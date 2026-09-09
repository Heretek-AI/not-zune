using Microsoft.Iris;

namespace ZuneUI;

public class SubscriptionDetailsPanel : LibraryPanel
{
	public SubscriptionDetailsPanel(SubscriptionLibraryPage page)
		: base((IModelItemOwner)(object)page)
	{
	}
}
