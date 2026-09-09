using System;
using System.Collections;
using Microsoft.Iris;
using Microsoft.Zune.Playlist;

namespace ZuneUI;

public class ChannelLibraryPage : SubscriptionLibraryPage
{
	private static string _saveAsPlaylistMessageSuccess = Shell.LoadString(StringId.IDS_CHANNEL_SAVE_PLAYLIST_SUCCESS);

	private static string _saveAsPlaylistMessageFailure = Shell.LoadString(StringId.IDS_CHANNEL_SAVE_PLAYLIST_FAILURE);

	protected override string LandUI => "res://ZuneShellResources!ChannelLibrary.uix#ChannelLibrary";

	protected override EMediaTypes SeriesMediaType => (EMediaTypes)9;

	protected override EListType SeriesListType => (EListType)12;

	protected override StringId SubscriptionErrorStringId => StringId.IDS_PLAYLIST_SUBSCRIPTION_ERROR;

	public ChannelLibraryPage()
		: this(showDevice: false)
	{
	}

	public ChannelLibraryPage(bool showDevice)
		: base(showDevice, MediaType.Playlist)
	{
		if (showDevice)
		{
			base.PivotPreference = Shell.MainFrame.Device.Channels;
			Deviceland.InitDevicePage(this);
			base.ShowComputerIcon = ComputerIconState.Show;
		}
		else
		{
			base.DetailsPanel = new ChannelDetailsPanel(this);
			base.PivotPreference = Shell.MainFrame.Collection.Channels;
		}
		base.IsRootPage = true;
		base.UIPath = "Collection\\Channels";
		base.SeriesPanel = new ChannelSeriesPanel(this);
		base.EpisodePanel = new ChannelEpisodePanel(this);
		base.TransportControlStyle = TransportControlStyle.Music;
		base.PlaybackContext = PlaybackContext.Music;
	}

	public static Guid GetZuneMediaId(int seriesId)
	{
		return SubscriptionLibraryPage.GetZuneMediaId(seriesId, (EListType)12);
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
			ZuneShell.DefaultInstance.Execute("Collection\\Channels", hashtable);
		}
	}

	public static void SaveAsPlaylist(int playlistId)
	{
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0017: Expected O, but got Unknown
		PlaylistManager.Instance.SavePlaylistAsStatic(playlistId, new PlaylistAsyncOperationCompleted(SaveAsPlaylistCompleted));
	}

	public static void SaveAsPlaylistCompleted(HRESULT hr)
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_0008: Unknown result type (might be due to invalid IL or missing references)
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_001f: Expected O, but got Unknown
		Application.DeferredInvoke((DeferredInvokeHandler)delegate
		{
			string message = ((!((HRESULT)(ref hr)).IsSuccess) ? _saveAsPlaylistMessageFailure : _saveAsPlaylistMessageSuccess);
			NotificationArea.Instance.Add(new MessageNotification(message, NotificationTask.Library, NotificationState.Completed));
		}, (object)null);
	}
}
