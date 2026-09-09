using Microsoft.Zune.Util;

namespace ZuneUI;

public class DownloadsPage : ZunePage
{
	public DownloadsPage(bool collection)
	{
		base.UI = "res://ZuneMarketplaceResources!DownloadsData.uix#MarketplaceDownloads";
		if (collection || !FeatureEnablement.IsFeatureEnabled((Features)2))
		{
			base.PivotPreference = Shell.MainFrame.Collection.Downloads;
			base.UIPath = "Collection\\Downloads";
		}
		else
		{
			base.PivotPreference = Shell.MainFrame.Marketplace.Downloads;
			base.UIPath = "Marketplace\\Downloads\\Home";
		}
		base.IsRootPage = true;
	}

	public override IPageState SaveAndRelease()
	{
		return new DownloadsPageState(this);
	}
}
