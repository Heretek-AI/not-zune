using System;
using System.Collections;
using Microsoft.Iris;
using Microsoft.Zune.Service;
using Microsoft.Zune.Shell;
using ZuneUI;

namespace ZuneXml;

internal class Track : Media
{
	private int _dbMediaId = -1;

	private int _userRating;

	private int _ordinal;

	internal virtual bool CanPlay
	{
		get
		{
			bool flag = Rights.HasRights(MediaRightsEnum.Preview, AudioEncodingEnum.WMA);
			if (!flag)
			{
				flag = CanSubscriptionPlay;
			}
			if (!flag && Id != Guid.Empty)
			{
				flag = ZuneApplication.Service.InVisibleCollection(Id, (EContentType)0);
			}
			return flag;
		}
	}

	internal virtual bool CanPreview => Rights.HasRights(MediaRightsEnum.Preview, AudioEncodingEnum.WMA);

	internal virtual bool CanSubscriptionPlay
	{
		get
		{
			if (Rights.HasRights(MediaRightsEnum.SubscriptionStream, AudioEncodingEnum.WMA))
			{
				return ZuneApplication.Service.IsSignedInWithSubscription();
			}
			return false;
		}
	}

	internal virtual bool CanDownload
	{
		get
		{
			bool flag = false;
			if (ZuneApplication.Service.CanDownloadSubscriptionContent())
			{
				bool flag2 = false;
				bool flag3 = false;
				if (Id != Guid.Empty && (!ZuneApplication.Service.IsDownloading(Id, (EContentType)0, ref flag2, ref flag3) || flag3) && !ZuneApplication.Service.InVisibleCollection(Id, (EContentType)0))
				{
					flag = Rights.HasRights(MediaRightsEnum.SubscriptionDownload, AudioEncodingEnum.WMA);
				}
			}
			if (flag)
			{
				return !IsParentallyBlocked;
			}
			return false;
		}
	}

	internal virtual bool CanPurchase
	{
		get
		{
			Right right = SelectPurchaseRight();
			return right != null;
		}
	}

	internal virtual bool CanPurchaseFree
	{
		get
		{
			bool result = false;
			Right offer = null;
			if (Rights.HasOfferRights(MediaRightsEnum.Purchase, AudioEncodingEnum.MP3, PriceTypeEnum.Points, out offer) && offer.IsFree)
			{
				result = true;
			}
			else if (Rights.HasOfferRights(MediaRightsEnum.AlbumPurchase, AudioEncodingEnum.MP3, PriceTypeEnum.Points, out offer) && offer.IsFree)
			{
				result = true;
			}
			else if (Rights.HasOfferRights(MediaRightsEnum.Purchase, AudioEncodingEnum.WMA, PriceTypeEnum.Points, out offer) && offer.IsFree)
			{
				result = true;
			}
			else if (Rights.HasOfferRights(MediaRightsEnum.AlbumPurchase, AudioEncodingEnum.WMA, PriceTypeEnum.Points, out offer) && offer.IsFree)
			{
				result = true;
			}
			return result;
		}
	}

	internal virtual bool CanPurchaseMP3
	{
		get
		{
			Right offer;
			return Rights.HasOfferRights(MediaRightsEnum.Purchase, AudioEncodingEnum.MP3, PriceTypeEnum.Points, out offer) || Rights.HasOfferRights(MediaRightsEnum.AlbumPurchase, AudioEncodingEnum.MP3, PriceTypeEnum.Points, out offer);
		}
	}

	internal virtual bool CanPurchaseAlbumOnly
	{
		get
		{
			Right offer;
			return !Rights.HasOfferRights(MediaRightsEnum.Purchase, AudioEncodingEnum.WMA, PriceTypeEnum.Points, out offer) && !Rights.HasOfferRights(MediaRightsEnum.Purchase, AudioEncodingEnum.MP3, PriceTypeEnum.Points, out offer) && (Rights.HasOfferRights(MediaRightsEnum.AlbumPurchase, AudioEncodingEnum.WMA, PriceTypeEnum.Points, out offer) || Rights.HasOfferRights(MediaRightsEnum.AlbumPurchase, AudioEncodingEnum.MP3, PriceTypeEnum.Points, out offer));
		}
	}

	internal virtual bool CanPurchaseSubscriptionFree
	{
		get
		{
			Right offer;
			return ZuneApplication.Service.GetSubscriptionFreeTrackBalance() > 0 && (Rights.HasOfferRights(MediaRightsEnum.SubscriptionFreePurchase, AudioEncodingEnum.WMA, PriceTypeEnum.Points, out offer) || Rights.HasOfferRights(MediaRightsEnum.SubscriptionFreePurchase, AudioEncodingEnum.MP3, PriceTypeEnum.Points, out offer));
		}
	}

	internal virtual bool HasPoints
	{
		get
		{
			bool result = false;
			Right right = SelectPurchaseRight();
			if (right != null)
			{
				result = right.HasPoints;
			}
			return result;
		}
	}

	internal virtual bool InCollection => GetInCollection();

	internal virtual bool IsDownloading => GetIsDownloading();

	internal virtual int LibraryId
	{
		get
		{
			return GetLibraryId();
		}
		set
		{
			if (_dbMediaId != value)
			{
				_dbMediaId = value;
				((DataProviderObject)this).FirePropertyChanged("LibraryId");
			}
		}
	}

	internal virtual bool CanSync
	{
		get
		{
			if (!CanPurchase && !InCollection && !IsDownloading)
			{
				return CanDownload;
			}
			return true;
		}
	}

	internal virtual bool CanBurn => CanPurchase;

	internal virtual int PointsPrice
	{
		get
		{
			int result = -1;
			if (Rights.HasOfferRights(MediaRightsEnum.Purchase, AudioEncodingEnum.MP3, PriceTypeEnum.Points, out var offer))
			{
				result = offer.PointsPrice;
			}
			else if (Rights.HasOfferRights(MediaRightsEnum.Purchase, AudioEncodingEnum.WMA, PriceTypeEnum.Points, out offer))
			{
				result = offer.PointsPrice;
			}
			else if (Rights.HasOfferRights(MediaRightsEnum.AlbumPurchase, AudioEncodingEnum.MP3, PriceTypeEnum.Points, out offer))
			{
				result = offer.PointsPrice;
			}
			else if (Rights.HasOfferRights(MediaRightsEnum.AlbumPurchase, AudioEncodingEnum.WMA, PriceTypeEnum.Points, out offer))
			{
				result = offer.PointsPrice;
			}
			return result;
		}
	}

	internal virtual bool Actionable => Rights.HasAnyRights();

	internal virtual int Ordinal
	{
		get
		{
			return _ordinal;
		}
		set
		{
			if (_ordinal != value)
			{
				_ordinal = value;
				((DataProviderObject)this).FirePropertyChanged("Ordinal");
			}
		}
	}

	internal virtual int UserRating
	{
		get
		{
			_userRating = RecommendationsHelper.GetUserRating((DataProviderObject)(object)this);
			return _userRating;
		}
		set
		{
			if (_userRating != value)
			{
				_userRating = value;
				((DataProviderObject)this).FirePropertyChanged("UserRating");
			}
		}
	}

	internal virtual bool IsParentallyBlocked
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

	internal TimeSpan Duration => (TimeSpan)base.GetProperty("Duration");

	internal int TrackNumber => (int)base.GetProperty("TrackNumber");

	internal int DiscNumber => (int)base.GetProperty("DiscNumber");

	internal string AlbumTitle => (string)base.GetProperty("AlbumTitle");

	internal Guid AlbumId => (Guid)base.GetProperty("AlbumId");

	internal MiniArtist AlbumArtist => (MiniArtist)base.GetProperty("AlbumArtist");

	internal string ArtistName => (string)base.GetProperty("ArtistName");

	internal Genre PrimaryGenre => (Genre)base.GetProperty("PrimaryGenre");

	internal bool Explicit => (bool)base.GetProperty("Explicit");

	internal int PlayCount => (int)base.GetProperty("PlayCount");

	internal string ReferrerContext => (string)base.GetProperty("ReferrerContext");

	internal Guid MusicVideoId => (Guid)base.GetProperty("MusicVideoId");

	internal override Guid Id => (Guid)base.GetProperty("Id");

	internal override string Title => (string)base.GetProperty("Title");

	internal override string SortTitle => (string)base.GetProperty("SortTitle");

	internal override MiniArtist PrimaryArtist => (MiniArtist)base.GetProperty("PrimaryArtist");

	internal override IList Artists => (IList)base.GetProperty("Artists");

	internal override double Popularity => (double)base.GetProperty("Popularity");

	internal override Guid ImageId => (Guid)base.GetProperty("ImageId");

	internal override MediaRights Rights => (MediaRights)base.GetProperty("Rights");

	protected bool GetInCollection()
	{
		bool result = false;
		if (Id != Guid.Empty)
		{
			int num = default(int);
			result = ZuneApplication.Service.InVisibleCollection(Id, (EContentType)0, ref num);
		}
		return result;
	}

	protected bool GetIsDownloading()
	{
		bool result = false;
		if (Id != Guid.Empty)
		{
			bool flag = false;
			bool flag2 = false;
			result = ZuneApplication.Service.IsDownloading(Id, (EContentType)0, ref flag, ref flag2);
		}
		return result;
	}

	protected int GetLibraryId()
	{
		int result = -1;
		if (Id != Guid.Empty)
		{
			ZuneApplication.Service.InVisibleCollection(Id, (EContentType)0, ref result);
		}
		return result;
	}

	private Right SelectPurchaseRight()
	{
		if (!Rights.HasOfferRights(MediaRightsEnum.Purchase, AudioEncodingEnum.MP3, PriceTypeEnum.Points, out var offer) && !Rights.HasOfferRights(MediaRightsEnum.AlbumPurchase, AudioEncodingEnum.MP3, PriceTypeEnum.Points, out offer) && !Rights.HasOfferRights(MediaRightsEnum.Purchase, AudioEncodingEnum.WMA, PriceTypeEnum.Points, out offer) && !Rights.HasOfferRights(MediaRightsEnum.AlbumPurchase, AudioEncodingEnum.WMA, PriceTypeEnum.Points, out offer))
		{
			return null;
		}
		return offer;
	}

	internal static XmlDataProviderObject ConstructTrackObject(DataProviderQuery owner, object objectTypeCookie)
	{
		return new Track(owner, objectTypeCookie);
	}

	internal Track(DataProviderQuery owner, object resultTypeCookie)
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
			_ => base.GetProperty(propertyName), 
		};
	}
}
