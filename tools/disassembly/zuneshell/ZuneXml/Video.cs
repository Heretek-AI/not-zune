using System;
using Microsoft.Iris;
using Microsoft.Zune.Configuration;
using Microsoft.Zune.Service;
using Microsoft.Zune.Shell;

namespace ZuneXml;

internal abstract class Video : Media
{
	private int _dbMediaId = -1;

	internal abstract DateTime ReleaseDate { get; }

	internal abstract TimeSpan Duration { get; }

	internal abstract int PointsPrice { get; }

	internal abstract bool HasPreview { get; }

	internal abstract bool CanPreview { get; }

	internal abstract bool CanSubscriptionPlay { get; }

	internal abstract bool CanPurchase { get; }

	internal abstract bool CanPurchaseHD { get; }

	internal abstract bool CanPurchaseSD { get; }

	internal abstract bool CanPurchaseSeason { get; }

	internal abstract bool CanPurchaseSeasonHD { get; }

	internal abstract bool CanPurchaseSeasonSD { get; }

	internal abstract bool CanRent { get; }

	internal abstract bool CanRentHD { get; }

	internal abstract bool CanRentSD { get; }

	internal abstract bool CanPurchaseAlbumOnly { get; }

	internal abstract bool CanSync { get; }

	internal abstract bool InCollection { get; }

	internal abstract bool InCollectionShortcut { get; }

	internal abstract bool IsDownloading { get; }

	internal abstract bool IsParentallyBlocked { get; }

	internal void SetLibraryId(int dbMediaId)
	{
		if (_dbMediaId != dbMediaId)
		{
			_dbMediaId = dbMediaId;
			((DataProviderObject)this).FirePropertyChanged("LibraryId");
		}
	}

	internal int GetLibraryId()
	{
		int result = -1;
		if (Id != Guid.Empty)
		{
			ZuneApplication.Service.InVisibleCollection(Id, (EContentType)3, ref result);
		}
		return result;
	}

	internal int GetPointsPrice()
	{
		int result = -1;
		int num = -1;
		Right offerRight = Rights.GetOfferRight(MediaRightsEnum.Purchase, VideoDefinitionEnum.HD, VideoDefinitionEnum.XD, PriceTypeEnum.Points);
		if (offerRight == null)
		{
			offerRight = Rights.GetOfferRight(MediaRightsEnum.AlbumPurchase, VideoDefinitionEnum.HD, VideoDefinitionEnum.XD, PriceTypeEnum.Points);
		}
		if (offerRight != null)
		{
			num = offerRight.PointsPrice;
		}
		int num2 = -1;
		offerRight = Rights.GetOfferRight(MediaRightsEnum.Purchase, VideoDefinitionEnum.SD, VideoDefinitionEnum.XD, PriceTypeEnum.Points);
		if (offerRight == null)
		{
			offerRight = Rights.GetOfferRight(MediaRightsEnum.AlbumPurchase, VideoDefinitionEnum.SD, VideoDefinitionEnum.XD, PriceTypeEnum.Points);
		}
		if (offerRight != null)
		{
			num2 = offerRight.PointsPrice;
		}
		if (num > 0 && num2 > 0)
		{
			result = (ClientConfiguration.Service.PurchaseHD ? num : num2);
		}
		else if (num > 0)
		{
			result = num;
		}
		else if (num2 > 0)
		{
			result = num2;
		}
		return result;
	}

	internal int GetPointsRental()
	{
		int result = -1;
		int num = -1;
		Right offerRight = Rights.GetOfferRight(MediaRightsEnum.Rent, VideoDefinitionEnum.HD, VideoDefinitionEnum.XD, PriceTypeEnum.Points);
		if (offerRight != null)
		{
			num = offerRight.PointsPrice;
		}
		int num2 = -1;
		offerRight = Rights.GetOfferRight(MediaRightsEnum.Rent, VideoDefinitionEnum.SD, VideoDefinitionEnum.XD, PriceTypeEnum.Points);
		if (offerRight != null)
		{
			num2 = offerRight.PointsPrice;
		}
		if (num > 0 && num2 > 0)
		{
			result = (ClientConfiguration.Service.PurchaseHD ? num : num2);
		}
		else if (num > 0)
		{
			result = num;
		}
		else if (num2 > 0)
		{
			result = num2;
		}
		return result;
	}

	internal bool GetHasPreview()
	{
		if (!Rights.HasRights(MediaRightsEnum.Preview, VideoDefinitionEnum.None, PriceTypeEnum.None))
		{
			return Rights.HasRights(MediaRightsEnum.PreviewStream, VideoDefinitionEnum.None, PriceTypeEnum.None);
		}
		return true;
	}

	internal bool GetCanPreview()
	{
		if (Rights.HasRights(MediaRightsEnum.Preview, VideoDefinitionEnum.None, PriceTypeEnum.None) || Rights.HasRights(MediaRightsEnum.PreviewStream, VideoDefinitionEnum.None, PriceTypeEnum.None))
		{
			return !IsParentallyBlocked;
		}
		return false;
	}

	internal bool GetCanSubscriptionPlay()
	{
		if (Rights.HasRights(MediaRightsEnum.SubscriptionStream, VideoDefinitionEnum.None, PriceTypeEnum.None) && ZuneApplication.Service.IsSignedInWithSubscription())
		{
			return !IsParentallyBlocked;
		}
		return false;
	}

	internal bool GetCanPurchase()
	{
		if (!GetCanPurchaseHD())
		{
			return GetCanPurchaseSD();
		}
		return true;
	}

	internal bool GetCanPurchaseHD()
	{
		bool flag = false;
		return Rights.HasRights(MediaRightsEnum.Purchase, VideoDefinitionEnum.HD, VideoDefinitionEnum.XD, PriceTypeEnum.Points) || Rights.HasRights(MediaRightsEnum.PurchaseStream, VideoDefinitionEnum.HD, PriceTypeEnum.Points) || Rights.HasRights(MediaRightsEnum.AlbumPurchase, VideoDefinitionEnum.HD, PriceTypeEnum.Points);
	}

	internal bool GetCanPurchaseSD()
	{
		bool flag = false;
		return Rights.HasRights(MediaRightsEnum.Purchase, VideoDefinitionEnum.SD, VideoDefinitionEnum.XD, PriceTypeEnum.Points) || Rights.HasRights(MediaRightsEnum.PurchaseStream, VideoDefinitionEnum.SD, PriceTypeEnum.Points) || Rights.HasRights(MediaRightsEnum.AlbumPurchase, VideoDefinitionEnum.SD, VideoDefinitionEnum.XD, PriceTypeEnum.Points);
	}

	internal bool GetCanPurchaseSeason()
	{
		if (!GetCanPurchaseSeasonHD())
		{
			return GetCanPurchaseSeasonSD();
		}
		return true;
	}

	internal bool GetCanPurchaseSeasonHD()
	{
		bool flag = false;
		return Rights.HasRights(MediaRightsEnum.SeasonPurchase, VideoDefinitionEnum.HD, VideoDefinitionEnum.XD, PriceTypeEnum.Points) || Rights.HasRights(MediaRightsEnum.SeasonPurchaseStream, VideoDefinitionEnum.HD, PriceTypeEnum.Points);
	}

	internal bool GetCanPurchaseSeasonSD()
	{
		bool flag = false;
		return Rights.HasRights(MediaRightsEnum.SeasonPurchase, VideoDefinitionEnum.SD, VideoDefinitionEnum.XD, PriceTypeEnum.Points) || Rights.HasRights(MediaRightsEnum.SeasonPurchaseStream, VideoDefinitionEnum.SD, PriceTypeEnum.Points);
	}

	internal bool GetCanPurchaseAlbumOnly()
	{
		return !Rights.HasRights(MediaRightsEnum.Purchase, VideoDefinitionEnum.None, PriceTypeEnum.Points) && Rights.HasRights(MediaRightsEnum.AlbumPurchase, VideoDefinitionEnum.None, PriceTypeEnum.Points);
	}

	internal bool GetCanRent()
	{
		if (!GetCanRentHD())
		{
			return GetCanRentSD();
		}
		return true;
	}

	internal bool GetCanRentHD()
	{
		bool flag = false;
		return Rights.HasRights(MediaRightsEnum.Rent, VideoDefinitionEnum.HD, PriceTypeEnum.Points) || Rights.HasRights(MediaRightsEnum.RentStream, VideoDefinitionEnum.HD, PriceTypeEnum.Points);
	}

	internal bool GetCanRentSD()
	{
		bool flag = false;
		return Rights.HasRights(MediaRightsEnum.Rent, VideoDefinitionEnum.SD, PriceTypeEnum.Points) || Rights.HasRights(MediaRightsEnum.RentStream, VideoDefinitionEnum.SD, PriceTypeEnum.Points);
	}

	internal bool GetInCollection()
	{
		bool result = false;
		if (Id != Guid.Empty)
		{
			result = ZuneApplication.Service.InVisibleCollection(Id, (EContentType)3);
		}
		return result;
	}

	internal bool GetInCollectionShortcut()
	{
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Invalid comparison between Unknown and I4
		bool result = false;
		if (Id != Guid.Empty)
		{
			result = (int)ZuneApplication.Service.GetMediaStatus(Id, (EContentType)3) == 7;
		}
		return result;
	}

	internal bool GetIsDownloading()
	{
		bool result = false;
		if (Id != Guid.Empty)
		{
			bool flag = false;
			bool flag2 = default(bool);
			result = ZuneApplication.Service.IsDownloading(Id, (EContentType)3, ref flag2, ref flag);
		}
		return result;
	}

	internal bool GetIsParentallyBlocked(string rating)
	{
		bool flag = false;
		string empty = string.Empty;
		if (this is Movie)
		{
			empty = "Movies";
		}
		else
		{
			if (!(this is TVVideo))
			{
				return false;
			}
			empty = "TV";
		}
		return ZuneApplication.Service.BlockRatedContent(empty, rating);
	}

	protected Video(DataProviderQuery owner, object resultTypeCookie)
		: base(owner, resultTypeCookie)
	{
	}
}
