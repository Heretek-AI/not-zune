using System;
using Microsoft.Iris;

namespace ZuneXml;

internal class ProfileTrack : PlaylistTrack
{
	internal string TopArtistName => (string)base.GetProperty("TopArtistName");

	internal Guid TopArtistId => (Guid)base.GetProperty("TopArtistId");

	internal static XmlDataProviderObject ConstructProfileTrackObject(DataProviderQuery owner, object objectTypeCookie)
	{
		return new ProfileTrack(owner, objectTypeCookie);
	}

	internal ProfileTrack(DataProviderQuery owner, object resultTypeCookie)
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
			"ImageId" => ImageId, 
			_ => base.GetProperty(propertyName), 
		};
	}
}
