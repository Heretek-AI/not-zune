using Microsoft.Iris;

namespace ZuneUI;

public class CollectionRadioPanel : ListPanel
{
	public CollectionRadioPanel(RadioPage page)
		: base((IModelItemOwner)(object)page)
	{
	}
}
