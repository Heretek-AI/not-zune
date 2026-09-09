namespace ZuneUI;

public class ChannelSeriesPanel : SubscriptionSeriesPanel
{
	public override SyncCategory SyncCategory => SyncCategory.Channel;

	public ChannelSeriesPanel(ChannelLibraryPage page)
		: base(page)
	{
	}
}
