using System;
using Microsoft.Zune.Service;
using Microsoft.Zune.Util;

namespace ZuneUI;

public static class ZuneDotNet
{
	public static string GetViewTrackUri(string trackId)
	{
		return GetViewUri(trackId, "track");
	}

	public static void ViewTrack(Guid trackId)
	{
		ZuneShell.DefaultInstance.Execute(GetViewTrackUri(trackId.ToString()), null);
	}

	public static string GetViewAlbumUri(string albumId)
	{
		return GetViewUri(albumId, "album");
	}

	public static void ViewAlbum(Guid albumId)
	{
		ZuneShell.DefaultInstance.Execute(GetViewAlbumUri(albumId.ToString()), null);
	}

	public static string GetViewArtistUri(string artistId)
	{
		return GetViewUri(artistId, "artist");
	}

	public static void ViewArtist(Guid artistId)
	{
		ZuneShell.DefaultInstance.Execute(GetViewArtistUri(artistId.ToString()), null);
	}

	public static string GetViewPodcastSeriesUri(string podcastSeriesId)
	{
		return GetViewUri(podcastSeriesId, "podcastSeries");
	}

	public static void ViewPodcastSeries(Guid podcastSeriesId)
	{
		ZuneShell.DefaultInstance.Execute(GetViewPodcastSeriesUri(podcastSeriesId.ToString()), null);
	}

	private static string GetViewUri(string id, string type)
	{
		string endPointUri = Service.GetEndPointUri((EServiceEndpointId)4);
		string text = (FeatureEnablement.IsFeatureEnabled((Features)5) ? "View" : "ViewUnsupportedMarket");
		return "Web\\" + UrlHelper.MakeUrlEx(endPointUri + "/redirect", "type", type, "id", id, "target", "web", "action", text);
	}
}
