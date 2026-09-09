using Microsoft.Iris;

namespace ZuneUI;

public class InboxPanel : ListPanel
{
	internal InboxPanel(InboxPage page)
		: base((IModelItemOwner)(object)page)
	{
	}
}
