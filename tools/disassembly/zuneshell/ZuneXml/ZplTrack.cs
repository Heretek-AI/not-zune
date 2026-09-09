using Microsoft.Iris;

namespace ZuneXml;

internal class ZplTrack : PlaylistTrack
{
	internal static XmlDataProviderObject ConstructZplTrackObject(DataProviderQuery owner, object objectTypeCookie)
	{
		return new ZplTrack(owner, objectTypeCookie);
	}

	internal ZplTrack(DataProviderQuery owner, object resultTypeCookie)
		: base(owner, resultTypeCookie)
	{
	}

	public override object GetProperty(string propertyName)
	{
		return propertyName switch
		{
			"Actionable" => Actionable, 
			"UserRating" => UserRating, 
			"LibraryId" => LibraryId, 
			"PointsPrice" => PointsPrice, 
			"HasPoints" => HasPoints, 
			"CanPlay" => CanPlay, 
			"CanPreview" => CanPreview, 
			"CanSubscriptionPlay" => CanSubscriptionPlay, 
			"CanDownload" => CanDownload, 
			"CanPurchase" => CanPurchase, 
			"CanPurchaseFree" => CanPurchaseFree, 
			"CanPurchaseMP3" => CanPurchaseMP3, 
			"CanPurchaseAlbumOnly" => CanPurchaseAlbumOnly, 
			"CanPurchaseSubscriptionFree" => CanPurchaseSubscriptionFree, 
			"CanSync" => CanSync, 
			"CanBurn" => CanBurn, 
			"InCollection" => InCollection, 
			"IsDownloading" => IsDownloading, 
			"IsParentallyBlocked" => IsParentallyBlocked, 
			"Ordinal" => Ordinal, 
			"SortTitle" => SortTitle, 
			"ImageId" => ImageId, 
			"Rights" => Rights, 
			"PrimaryArtist" => PrimaryArtist, 
			"Artists" => Artists, 
			"Popularity" => Popularity, 
			_ => base.GetProperty(propertyName), 
		};
	}
}
