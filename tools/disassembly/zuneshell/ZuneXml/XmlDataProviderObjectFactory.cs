using System.Collections.Generic;
using Microsoft.Iris;

namespace ZuneXml;

public class XmlDataProviderObjectFactory
{
	private static IDictionary<object, ConstructObject> _mapMarkupCookieToConstructor;

	private static IDictionary<string, object> _mapTypeNameToMarkupCookie;

	public static void ClearBindings()
	{
		_mapMarkupCookieToConstructor = null;
	}

	public static void Bind(string typeName, object markupTypeCookie)
	{
		if (_mapTypeNameToMarkupCookie == null)
		{
			_mapTypeNameToMarkupCookie = new Dictionary<string, object>(40);
		}
		_mapTypeNameToMarkupCookie[typeName] = markupTypeCookie;
	}

	private static void RegisterTypeConstructor(string typeName, ConstructObject constructor)
	{
		object key = _mapTypeNameToMarkupCookie[typeName];
		if (_mapMarkupCookieToConstructor == null)
		{
			_mapMarkupCookieToConstructor = new Dictionary<object, ConstructObject>(40);
		}
		_mapMarkupCookieToConstructor[key] = constructor;
	}

	internal static ConstructObject GetConstructor(object objectTypeCookie)
	{
		if (_mapMarkupCookieToConstructor == null)
		{
			RegisterTypeConstructors();
			_mapTypeNameToMarkupCookie = null;
		}
		ConstructObject result = null;
		if (_mapMarkupCookieToConstructor.ContainsKey(objectTypeCookie))
		{
			result = _mapMarkupCookieToConstructor[objectTypeCookie];
		}
		return result;
	}

	internal static XmlDataProviderObject CreateObject(DataProviderQuery owner, object objectTypeCookie)
	{
		ConstructObject constructor = GetConstructor(objectTypeCookie);
		if (constructor != null)
		{
			return constructor(owner, objectTypeCookie);
		}
		return new XmlDataProviderObject(owner, objectTypeCookie);
	}

	internal static void RegisterTypeConstructors()
	{
		RegisterTypeConstructor("BadgeData", BadgeData.ConstructBadgeDataObject);
		RegisterTypeConstructor("TrackPurchaseHistory", TrackPurchaseHistory.ConstructTrackPurchaseHistoryObject);
		RegisterTypeConstructor("AppCapabilities", AppCapabilities.ConstructAppCapabilitiesObject);
		RegisterTypeConstructor("Artist", Artist.ConstructArtistObject);
		RegisterTypeConstructor("PlaylistTrack", PlaylistTrack.ConstructPlaylistTrackObject);
		RegisterTypeConstructor("ZuneHDApp", ZuneHDApp.ConstructZuneHDAppObject);
		RegisterTypeConstructor("MessageDetails", MessageDetails.ConstructMessageDetailsObject);
		RegisterTypeConstructor("Season", Season.ConstructSeasonObject);
		RegisterTypeConstructor("MusicVideo", MusicVideo.ConstructMusicVideoObject);
		RegisterTypeConstructor("MediaInstance", MediaInstance.ConstructMediaInstanceObject);
		RegisterTypeConstructor("SeriesCategory", SeriesCategory.ConstructSeriesCategoryObject);
		RegisterTypeConstructor("RecommendedTrack", RecommendedTrack.ConstructRecommendedTrackObject);
		RegisterTypeConstructor("AppCapability", AppCapability.ConstructAppCapabilityObject);
		RegisterTypeConstructor("MovieGenre", MovieGenre.ConstructMovieGenreObject);
		RegisterTypeConstructor("MarketplaceRadioStation", MarketplaceRadioStation.ConstructMarketplaceRadioStationObject);
		RegisterTypeConstructor("RecommendedAlbum", RecommendedAlbum.ConstructRecommendedAlbumObject);
		RegisterTypeConstructor("Episode", Episode.ConstructEpisodeObject);
		RegisterTypeConstructor("AppMediaRights", AppMediaRights.ConstructAppMediaRightsObject);
		RegisterTypeConstructor("Movie", Movie.ConstructMovieObject);
		RegisterTypeConstructor("Track", Track.ConstructTrackObject);
		RegisterTypeConstructor("Right", Right.ConstructRightObject);
		RegisterTypeConstructor("Album", Album.ConstructAlbumObject);
		RegisterTypeConstructor("MovieStudio", MovieStudio.ConstructMovieStudioObject);
		RegisterTypeConstructor("Review", Review.ConstructReviewObject);
		RegisterTypeConstructor("AppGenre", AppGenre.ConstructAppGenreObject);
		RegisterTypeConstructor("ReviewListEntry", ReviewListEntry.ConstructReviewListEntryObject);
		RegisterTypeConstructor("ChannelReason", ChannelReason.ConstructChannelReasonObject);
		RegisterTypeConstructor("MiniAlbum", MiniAlbum.ConstructMiniAlbumObject);
		RegisterTypeConstructor("ZuneHDAppData", ZuneHDAppData.ConstructZuneHDAppDataObject);
		RegisterTypeConstructor("ArtistEvent", ArtistEvent.ConstructArtistEventObject);
		RegisterTypeConstructor("AppData", AppData.ConstructAppDataObject);
		RegisterTypeConstructor("WinPhoneAppPurchaseHistory", WinPhoneAppPurchaseHistory.ConstructWinPhoneAppPurchaseHistoryObject);
		RegisterTypeConstructor("Series", Series.ConstructSeriesObject);
		RegisterTypeConstructor("VideoCategory", VideoCategory.ConstructVideoCategoryObject);
		RegisterTypeConstructor("MessageRoot", MessageRoot.ConstructMessageRootObject);
		RegisterTypeConstructor("App", App.ConstructAppObject);
		RegisterTypeConstructor("Genre", Genre.ConstructGenreObject);
		RegisterTypeConstructor("VideoHistory", VideoHistory.ConstructVideoHistoryObject);
		RegisterTypeConstructor("WinPhoneAppGenre", WinPhoneAppGenre.ConstructWinPhoneAppGenreObject);
		RegisterTypeConstructor("ChannelTrack", ChannelTrack.ConstructChannelTrackObject);
		RegisterTypeConstructor("ZuneHDAppGenre", ZuneHDAppGenre.ConstructZuneHDAppGenreObject);
		RegisterTypeConstructor("ProfileTrack", ProfileTrack.ConstructProfileTrackObject);
		RegisterTypeConstructor("WinPhoneAppData", WinPhoneAppData.ConstructWinPhoneAppDataObject);
		RegisterTypeConstructor("TrackDownloadHistory", TrackDownloadHistory.ConstructTrackDownloadHistoryObject);
		RegisterTypeConstructor("PodcastSeries", PodcastSeries.ConstructPodcastSeriesObject);
		RegisterTypeConstructor("MiniArtist", MiniArtist.ConstructMiniArtistObject);
		RegisterTypeConstructor("WinPhoneApp", WinPhoneApp.ConstructWinPhoneAppObject);
		RegisterTypeConstructor("Mood", Mood.ConstructMoodObject);
		RegisterTypeConstructor("MediaRights", MediaRights.ConstructMediaRightsObject);
		RegisterTypeConstructor("Reason", Reason.ConstructReasonObject);
		RegisterTypeConstructor("AppScreenshot", AppScreenshot.ConstructAppScreenshotObject);
		RegisterTypeConstructor("ZplTrack", ZplTrack.ConstructZplTrackObject);
		RegisterTypeConstructor("Network", Network.ConstructNetworkObject);
		RegisterTypeConstructor("RecommendedArtist", RecommendedArtist.ConstructRecommendedArtistObject);
		RegisterTypeConstructor("MovieTrailer", MovieTrailer.ConstructMovieTrailerObject);
		RegisterTypeConstructor("ArtistEventList", ArtistEventList.ConstructArtistEventListObject);
		RegisterTypeConstructor("Short", Short.ConstructShortObject);
		RegisterTypeConstructor("Contributor", Contributor.ConstructContributorObject);
	}
}
