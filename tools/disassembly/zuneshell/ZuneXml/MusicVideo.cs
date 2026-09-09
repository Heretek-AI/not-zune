using System;
using System.Collections;
using Microsoft.Iris;
using Microsoft.Zune.Shell;
using Microsoft.Zune.Util;

namespace ZuneXml;

internal class MusicVideo : Video
{
	internal virtual int LibraryId
	{
		get
		{
			return GetLibraryId();
		}
		set
		{
			SetLibraryId(value);
		}
	}

	internal override bool CanSync
	{
		get
		{
			if (!CanPurchase && !InCollection)
			{
				return IsDownloading;
			}
			return true;
		}
	}

	internal override int PointsPrice
	{
		get
		{
			int result = -1;
			Right offerRight = Rights.GetOfferRight(MediaRightsEnum.Purchase, VideoDefinitionEnum.None, PriceTypeEnum.Points);
			if (offerRight == null)
			{
				offerRight = Rights.GetOfferRight(MediaRightsEnum.AlbumPurchase, VideoDefinitionEnum.None, PriceTypeEnum.Points);
			}
			if (offerRight != null)
			{
				result = offerRight.PointsPrice;
			}
			return result;
		}
	}

	internal override bool HasPreview => GetHasPreview();

	internal override bool CanPreview => GetCanPreview();

	internal override bool CanSubscriptionPlay
	{
		get
		{
			if (FeatureEnablement.IsFeatureEnabled((Features)19))
			{
				return GetCanSubscriptionPlay();
			}
			return false;
		}
	}

	internal override bool CanPurchase
	{
		get
		{
			if (!Rights.HasRights(MediaRightsEnum.Purchase, VideoDefinitionEnum.None, PriceTypeEnum.Points))
			{
				if (Rights.HasRights(MediaRightsEnum.AlbumPurchase, VideoDefinitionEnum.None, PriceTypeEnum.Points))
				{
					return Album.Id != Guid.Empty;
				}
				return false;
			}
			return true;
		}
	}

	internal override bool CanPurchaseHD => false;

	internal override bool CanPurchaseSD => false;

	internal override bool CanPurchaseSeason => false;

	internal override bool CanPurchaseSeasonHD => false;

	internal override bool CanPurchaseSeasonSD => false;

	internal override bool CanPurchaseAlbumOnly => GetCanPurchaseAlbumOnly();

	internal override bool CanRent => false;

	internal override bool CanRentHD => false;

	internal override bool CanRentSD => false;

	internal override bool InCollection => GetInCollection();

	internal override bool InCollectionShortcut => GetInCollectionShortcut();

	internal override bool IsDownloading => GetIsDownloading();

	internal override bool IsParentallyBlocked
	{
		get
		{
			if (Explicit)
			{
				return ZuneApplication.Service.BlockExplicitContent();
			}
			return false;
		}
	}

	internal string Label => (string)base.GetProperty("Label");

	internal Guid TrackId => (Guid)base.GetProperty("TrackId");

	internal MiniAlbum Album => (MiniAlbum)base.GetProperty("Album");

	internal Genre PrimaryGenre => (Genre)base.GetProperty("PrimaryGenre");

	internal bool Explicit => (bool)base.GetProperty("Explicit");

	internal int TrackNumber => (int)base.GetProperty("TrackNumber");

	internal override string Title => (string)base.GetProperty("Title");

	internal override Guid Id => (Guid)base.GetProperty("Id");

	internal override string SortTitle => (string)base.GetProperty("SortTitle");

	internal override double Popularity => (double)base.GetProperty("Popularity");

	internal override DateTime ReleaseDate => (DateTime)base.GetProperty("ReleaseDate");

	internal override TimeSpan Duration => (TimeSpan)base.GetProperty("Duration");

	internal override Guid ImageId => (Guid)base.GetProperty("ImageId");

	internal override MiniArtist PrimaryArtist => (MiniArtist)base.GetProperty("PrimaryArtist");

	internal override IList Artists => (IList)base.GetProperty("Artists");

	internal override MediaRights Rights => (MediaRights)base.GetProperty("Rights");

	internal static XmlDataProviderObject ConstructMusicVideoObject(DataProviderQuery owner, object objectTypeCookie)
	{
		return new MusicVideo(owner, objectTypeCookie);
	}

	internal MusicVideo(DataProviderQuery owner, object resultTypeCookie)
		: base(owner, resultTypeCookie)
	{
	}

	public override object GetProperty(string propertyName)
	{
		return propertyName switch
		{
			"LibraryId" => LibraryId, 
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
			_ => base.GetProperty(propertyName), 
		};
	}
}
