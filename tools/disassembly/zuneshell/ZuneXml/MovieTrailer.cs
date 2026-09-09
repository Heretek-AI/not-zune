using Microsoft.Iris;

namespace ZuneXml;

internal class MovieTrailer : Movie
{
	internal static XmlDataProviderObject ConstructMovieTrailerObject(DataProviderQuery owner, object objectTypeCookie)
	{
		return new MovieTrailer(owner, objectTypeCookie);
	}

	internal MovieTrailer(DataProviderQuery owner, object resultTypeCookie)
		: base(owner, resultTypeCookie)
	{
	}

	public override object GetProperty(string propertyName)
	{
		return propertyName switch
		{
			"PointsRental" => base.PointsRental, 
			"Languages" => base.Languages, 
			"PointsPrice" => PointsPrice, 
			"HasPreview" => HasPreview, 
			"CanPreview" => CanPreview, 
			"CanSubscriptionPlay" => CanSubscriptionPlay, 
			"CanPurchase" => CanPurchase, 
			"CanPurchaseHD" => CanPurchaseHD, 
			"CanPurchaseSD" => CanPurchaseSD, 
			"CanPurchaseSeason" => CanPurchaseSeason, 
			"CanPurchaseSeasonHD" => CanPurchaseSeasonHD, 
			"CanPurchaseSeasonSD" => CanPurchaseSeasonSD, 
			"CanRent" => CanRent, 
			"CanRentHD" => CanRentHD, 
			"CanRentSD" => CanRentSD, 
			"CanPurchaseAlbumOnly" => CanPurchaseAlbumOnly, 
			"CanSync" => CanSync, 
			"InCollection" => InCollection, 
			"InCollectionShortcut" => InCollectionShortcut, 
			"IsDownloading" => IsDownloading, 
			"IsParentallyBlocked" => IsParentallyBlocked, 
			"PrimaryArtist" => PrimaryArtist, 
			"Artists" => Artists, 
			_ => base.GetProperty(propertyName), 
		};
	}
}
