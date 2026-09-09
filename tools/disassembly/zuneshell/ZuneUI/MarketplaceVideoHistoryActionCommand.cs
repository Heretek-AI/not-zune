using Microsoft.Iris;
using Microsoft.Zune.Service;
using ZuneXml;

namespace ZuneUI;

public abstract class MarketplaceVideoHistoryActionCommand : MarketplaceActionCommand
{
	internal VideoHistory VideoHistoryModel => (VideoHistory)(object)base.Model;

	protected override EContentType ContentType => (EContentType)3;

	public MarketplaceVideoHistoryActionCommand()
	{
		base.AllowPlay = false;
	}

	public override void FindInCollection()
	{
		if (CanFindInCollection)
		{
			VideoLibraryPage.FindInCollection(base.CollectionId);
		}
	}

	protected override void OnInvoked()
	{
		if (!base.Downloading)
		{
			((Command)this).OnInvoked();
		}
	}
}
