namespace ZuneUI;

public class MarketplacePlaylistTrack : MarketplaceTrackActionCommand
{
	public MarketplacePlaylistTrack()
	{
		Download.Instance.DownloadAllPendingEvent += OnDownloadAllPendingEvent;
	}

	protected override void OnDispose(bool disposing)
	{
		base.OnDispose(disposing);
		if (disposing)
		{
			Download.Instance.DownloadAllPendingEvent -= OnDownloadAllPendingEvent;
		}
	}

	private void OnDownloadAllPendingEvent(object sender, object args)
	{
		if (!base.Downloading && (!base.DownloadingHidden || ShowHiddenProgress))
		{
			UpdateState();
		}
	}
}
