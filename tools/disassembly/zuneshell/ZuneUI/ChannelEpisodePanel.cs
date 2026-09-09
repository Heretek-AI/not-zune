namespace ZuneUI;

public class ChannelEpisodePanel : SubscriptionEpisodePanel
{
	public override SyncCategory SyncCategory => SyncCategory.Channel;

	public ChannelEpisodePanel(ChannelLibraryPage page)
		: base(page)
	{
	}
}
