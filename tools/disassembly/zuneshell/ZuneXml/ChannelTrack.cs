using System.Collections;
using Microsoft.Iris;

namespace ZuneXml;

internal class ChannelTrack : PlaylistTrack
{
	internal IList Reasons => (IList)base.GetProperty("Reasons");

	internal static XmlDataProviderObject ConstructChannelTrackObject(DataProviderQuery owner, object objectTypeCookie)
	{
		return new ChannelTrack(owner, objectTypeCookie);
	}

	internal ChannelTrack(DataProviderQuery owner, object resultTypeCookie)
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
