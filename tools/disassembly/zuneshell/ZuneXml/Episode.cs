using System;
using System.Collections;
using Microsoft.Iris;

namespace ZuneXml;

internal class Episode : TVVideo
{
	internal IList Languages => Rights.Languages;

	internal int EpisodeNumber => (int)base.GetProperty("EpisodeNumber");

	internal int SeasonNumber => (int)base.GetProperty("SeasonNumber");

	internal Series Series => (Series)base.GetProperty("Series");

	internal IList Categories => (IList)base.GetProperty("Categories");

	internal override Guid Id => (Guid)base.GetProperty("Id");

	internal override string Title => (string)base.GetProperty("Title");

	internal override string SortTitle => (string)base.GetProperty("SortTitle");

	internal override string Description => (string)base.GetProperty("Description");

	internal override DateTime ReleaseDate => (DateTime)base.GetProperty("ReleaseDate");

	internal override string Copyright => (string)base.GetProperty("Copyright");

	internal override string Rating => (string)base.GetProperty("Rating");

	internal override TimeSpan Duration => (TimeSpan)base.GetProperty("Duration");

	internal override double Popularity => (double)base.GetProperty("Popularity");

	internal override Network Network => (Network)base.GetProperty("Network");

	internal override string ProductionCompany => (string)base.GetProperty("ProductionCompany");

	internal override Guid ImageId => (Guid)base.GetProperty("ImageId");

	internal override MediaRights Rights => (MediaRights)base.GetProperty("Rights");

	internal static XmlDataProviderObject ConstructEpisodeObject(DataProviderQuery owner, object objectTypeCookie)
	{
		return new Episode(owner, objectTypeCookie);
	}

	internal Episode(DataProviderQuery owner, object resultTypeCookie)
		: base(owner, resultTypeCookie)
	{
	}

	public override object GetProperty(string propertyName)
	{
		return propertyName switch
		{
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
