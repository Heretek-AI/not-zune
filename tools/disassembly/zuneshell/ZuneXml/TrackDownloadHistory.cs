using System;
using System.Collections;
using Microsoft.Iris;
using ZuneUI;

namespace ZuneXml;

internal class TrackDownloadHistory : TrackHistory
{
	internal override bool InCollection => GetInCollection();

	internal override bool IsDownloading => GetIsDownloading();

	internal override int LibraryId => GetLibraryId();

	internal override bool CanPlay => false;

	internal override bool CanSync => false;

	internal override bool CanBurn => false;

	internal override int PointsPrice => 0;

	internal override bool CanPurchaseSubscriptionFree => false;

	internal override bool CanDownload
	{
		get
		{
			bool flag = false;
			if (!InCollection && MediaInstances != null)
			{
				foreach (MediaInstance mediaInstance in MediaInstances)
				{
					flag = mediaInstance.IsDownloadable;
					if (!flag)
					{
						break;
					}
				}
			}
			if (flag && Download.Instance.GetErrorCode(Id) == ((HRESULT)(ref HRESULT._ZUNE_E_NO_SUBSCRIPTION_DOWNLOAD_RIGHTS)).Int)
			{
				flag = false;
			}
			return flag;
		}
	}

	internal override bool CanPurchase => true;

	internal override bool CanPreview => false;

	internal override bool CanSubscriptionPlay => false;

	internal override bool CanPurchaseMP3 => false;

	internal override bool CanPurchaseAlbumOnly => false;

	internal override bool Actionable => false;

	internal override int UserRating => 0;

	internal override bool IsParentallyBlocked => false;

	internal override DateTime Date => (DateTime)base.GetProperty("Date");

	internal override IList MediaInstances => (IList)base.GetProperty("MediaInstances");

	internal static XmlDataProviderObject ConstructTrackDownloadHistoryObject(DataProviderQuery owner, object objectTypeCookie)
	{
		return new TrackDownloadHistory(owner, objectTypeCookie);
	}

	internal TrackDownloadHistory(DataProviderQuery owner, object resultTypeCookie)
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
			"Artists" => Artists, 
			"Popularity" => Popularity, 
			_ => base.GetProperty(propertyName), 
		};
	}
}
