using System;
using System.Collections;
using Microsoft.Iris;

namespace ZuneXml;

internal class Movie : RatableVideo
{
	internal override int PointsPrice => GetPointsPrice();

	internal int PointsRental => GetPointsRental();

	internal override bool HasPreview => GetHasPreview();

	internal override bool CanPreview => GetCanPreview();

	internal override bool CanSubscriptionPlay => GetCanSubscriptionPlay();

	internal override bool CanPurchase => GetCanPurchase();

	internal override bool CanPurchaseHD => GetCanPurchaseHD();

	internal override bool CanPurchaseSD => GetCanPurchaseSD();

	internal override bool CanPurchaseSeason => false;

	internal override bool CanPurchaseSeasonHD => false;

	internal override bool CanPurchaseSeasonSD => false;

	internal override bool CanPurchaseAlbumOnly => false;

	internal override bool CanRent => GetCanRent();

	internal override bool CanRentHD => GetCanRentHD();

	internal override bool CanRentSD => GetCanRentSD();

	internal override bool InCollection => GetInCollection();

	internal override bool InCollectionShortcut => GetInCollectionShortcut();

	internal override bool IsDownloading => GetIsDownloading();

	internal override bool IsParentallyBlocked => GetIsParentallyBlocked(Rating);

	internal override MiniArtist PrimaryArtist => null;

	internal override IList Artists => null;

	internal IList Languages => Rights.Languages;

	internal string ProductionCompany => (string)base.GetProperty("ProductionCompany");

	internal IList Contributors => (IList)base.GetProperty("Contributors");

	internal IList Genres => (IList)base.GetProperty("Genres");

	internal override Guid Id => (Guid)base.GetProperty("Id");

	internal override string Title => (string)base.GetProperty("Title");

	internal override string Rating => (string)base.GetProperty("Rating");

	internal override string Description => (string)base.GetProperty("Description");

	internal override string SortTitle => (string)base.GetProperty("SortTitle");

	internal override double Popularity => (double)base.GetProperty("Popularity");

	internal override DateTime ReleaseDate => (DateTime)base.GetProperty("ReleaseDate");

	internal override Guid ImageId => (Guid)base.GetProperty("ImageId");

	internal override MediaRights Rights => (MediaRights)base.GetProperty("Rights");

	internal override TimeSpan Duration => (TimeSpan)base.GetProperty("Duration");

	internal static XmlDataProviderObject ConstructMovieObject(DataProviderQuery owner, object objectTypeCookie)
	{
		return new Movie(owner, objectTypeCookie);
	}

	internal Movie(DataProviderQuery owner, object resultTypeCookie)
		: base(owner, resultTypeCookie)
	{
	}

	public override object GetProperty(string propertyName)
	{
		return propertyName switch
		{
			"PointsRental" => PointsRental, 
			"Languages" => Languages, 
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
