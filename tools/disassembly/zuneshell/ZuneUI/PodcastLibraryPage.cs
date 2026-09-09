using System;
using System.Collections;

namespace ZuneUI;

public class PodcastLibraryPage : SubscriptionLibraryPage
{
	protected override string LandUI => "res://ZuneShellResources!PodcastLibrary.uix#PodcastLibrary";

	protected override EMediaTypes SeriesMediaType => (EMediaTypes)18;

	protected override EListType SeriesListType => (EListType)7;

	protected override StringId SubscriptionErrorStringId => StringId.IDS_PODCAST_SUBSCRIPTION_ERROR;

	public PodcastLibraryPage()
		: this(showDevice: false)
	{
	}

	public static Guid GetZuneMediaId(int seriesId)
	{
		return SubscriptionLibraryPage.GetZuneMediaId(seriesId, (EListType)6);
	}

	public PodcastLibraryPage(bool showDevice)
		: base(showDevice, MediaType.Podcast)
	{
		base.UIPath = "Collection\\Podcasts";
		if (showDevice)
		{
			base.PivotPreference = Shell.MainFrame.Device.Podcasts;
			Deviceland.InitDevicePage(this);
		}
		else
		{
			base.PivotPreference = Shell.MainFrame.Collection.Podcasts;
		}
		base.IsRootPage = true;
		base.SeriesPanel = new SubscriptionSeriesPanel(this);
		base.EpisodePanel = new SubscriptionEpisodePanel(this);
		if (!showDevice)
		{
			base.DetailsPanel = new SubscriptionDetailsPanel(this);
		}
		m_markAllAsPlayed = new MenuItemCommand(Shell.LoadString(StringId.IDS_PODCAST_MARK_AS_PLAYED_MENUITEM));
		m_markAllAsUnplayed = new MenuItemCommand(Shell.LoadString(StringId.IDS_PODCAST_MARK_AS_UNPLAYED_MENUITEM));
		base.TransportControlStyle = TransportControlStyle.Music;
		base.PlaybackContext = PlaybackContext.LibraryPodcast;
		base.ShowPlaylistIcon = false;
	}

	public static void FindInCollection(int seriesId)
	{
		FindInCollection(seriesId, -1);
	}

	public static void FindInCollection(int seriesId, int libraryId)
	{
		if (seriesId > 0 || libraryId > 0)
		{
			Hashtable hashtable = new Hashtable();
			if (seriesId > 0)
			{
				hashtable.Add("SeriesLibraryId", seriesId);
			}
			if (libraryId > 0)
			{
				hashtable.Add("EpisodeLibraryId", libraryId);
			}
			ZuneShell.DefaultInstance.Execute("Collection\\Podcasts", hashtable);
		}
	}
}
