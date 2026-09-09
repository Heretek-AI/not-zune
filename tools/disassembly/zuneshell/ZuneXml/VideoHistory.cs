using System;
using System.Collections;
using Microsoft.Iris;
using ZuneUI;

namespace ZuneXml;

internal class VideoHistory : Video
{
	private MediaTypeEnum _mediaTypeEnum;

	internal override bool CanSync => false;

	internal override bool InCollection => GetInCollection();

	internal override bool InCollectionShortcut => GetInCollectionShortcut();

	internal override bool IsDownloading => GetIsDownloading();

	internal override bool CanPreview => false;

	internal override bool CanSubscriptionPlay => false;

	internal override int PointsPrice => 0;

	internal bool CanDownloadHD
	{
		get
		{
			bool flag = false;
			if (!InCollection && MediaInstances != null)
			{
				foreach (MediaInstance mediaInstance in MediaInstances)
				{
					if ((mediaInstance.RightEnum == MediaRightsEnum.AlbumPurchase || mediaInstance.RightEnum == MediaRightsEnum.Purchase || mediaInstance.RightEnum == MediaRightsEnum.SeasonPurchase || mediaInstance.RightEnum == MediaRightsEnum.Rent) && mediaInstance.VideoDefinitionEnum == VideoDefinitionEnum.HD && mediaInstance.VideoResolutionEnum == VideoResolutionEnum.VR_720P)
					{
						flag = mediaInstance.IsDownloadable;
						if (!flag)
						{
							break;
						}
					}
				}
			}
			if (flag && Download.Instance.GetErrorCode(Id) == ((HRESULT)(ref HRESULT._NS_E_MEDIA_DOWNLOAD_MAXIMUM_EXCEEDED)).Int)
			{
				flag = false;
			}
			return flag;
		}
	}

	internal bool CanDownload
	{
		get
		{
			bool flag = false;
			if (!InCollection && MediaInstances != null)
			{
				foreach (MediaInstance mediaInstance in MediaInstances)
				{
					if ((mediaInstance.RightEnum == MediaRightsEnum.AlbumPurchase || mediaInstance.RightEnum == MediaRightsEnum.Purchase || mediaInstance.RightEnum == MediaRightsEnum.SeasonPurchase || mediaInstance.RightEnum == MediaRightsEnum.Rent) && (mediaInstance.VideoDefinitionEnum == VideoDefinitionEnum.XD || mediaInstance.VideoDefinitionEnum == VideoDefinitionEnum.SD || (mediaInstance.VideoDefinitionEnum == VideoDefinitionEnum.HD && mediaInstance.VideoResolutionEnum == VideoResolutionEnum.VR_720P)))
					{
						flag = mediaInstance.IsDownloadable;
						if (!flag)
						{
							break;
						}
					}
				}
			}
			if (flag && Download.Instance.GetErrorCode(Id) == ((HRESULT)(ref HRESULT._NS_E_MEDIA_DOWNLOAD_MAXIMUM_EXCEEDED)).Int)
			{
				flag = false;
			}
			return flag;
		}
	}

	internal override bool CanPurchase
	{
		get
		{
			if (!CanDownload)
			{
				if (MediaInstances != null)
				{
					return MediaInstances.Count > 0;
				}
				return false;
			}
			return false;
		}
	}

	internal override bool CanPurchaseAlbumOnly => false;

	internal override bool CanRent
	{
		get
		{
			if (!CanDownload)
			{
				if (MediaInstances != null)
				{
					return MediaInstances.Count > 0;
				}
				return false;
			}
			return false;
		}
	}

	internal override DateTime ReleaseDate => DateTime.MinValue;

	internal override TimeSpan Duration => TimeSpan.Zero;

	internal override double Popularity => 0.0;

	internal override bool HasPreview => false;

	internal override bool CanPurchaseHD => false;

	internal override bool CanPurchaseSD => false;

	internal override bool CanPurchaseSeason => false;

	internal override bool CanPurchaseSeasonHD => false;

	internal override bool CanPurchaseSeasonSD => false;

	internal override bool CanRentHD => false;

	internal override bool CanRentSD => false;

	internal override bool IsParentallyBlocked => false;

	internal override Guid ImageId => Guid.Empty;

	internal override string SortTitle => string.Empty;

	internal override MediaRights Rights => null;

	internal override IList Artists => null;

	internal virtual bool IsTVSeason => _mediaTypeEnum == MediaTypeEnum.TVSeason;

	internal DateTime Date => (DateTime)base.GetProperty("Date");

	internal string MediaType => (string)base.GetProperty("MediaType");

	internal string SeasonTitle => (string)base.GetProperty("SeasonTitle");

	internal int SeasonNumber => (int)base.GetProperty("SeasonNumber");

	internal string SeriesTitle => (string)base.GetProperty("SeriesTitle");

	internal IList SeasonEpisodes => (IList)base.GetProperty("SeasonEpisodes");

	internal IList MediaInstances => (IList)base.GetProperty("MediaInstances");

	internal override string Title => (string)base.GetProperty("Title");

	internal override Guid Id => (Guid)base.GetProperty("Id");

	internal override MiniArtist PrimaryArtist => (MiniArtist)base.GetProperty("PrimaryArtist");

	public override void SetProperty(string propertyName, object value)
	{
		string text;
		if ((text = propertyName) != null && text == "MediaType")
		{
			_mediaTypeEnum = SchemaHelper.ToMediaTypeEnum((string)value);
		}
		base.SetProperty(propertyName, value);
	}

	internal static XmlDataProviderObject ConstructVideoHistoryObject(DataProviderQuery owner, object objectTypeCookie)
	{
		return new VideoHistory(owner, objectTypeCookie);
	}

	internal VideoHistory(DataProviderQuery owner, object resultTypeCookie)
		: base(owner, resultTypeCookie)
	{
	}

	public override object GetProperty(string propertyName)
	{
		return propertyName switch
		{
			"CanDownload" => CanDownload, 
			"CanDownloadHD" => CanDownloadHD, 
			"IsTVSeason" => IsTVSeason, 
			"ReleaseDate" => ReleaseDate, 
			"Duration" => Duration, 
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
			"SortTitle" => SortTitle, 
			"ImageId" => ImageId, 
			"Rights" => Rights, 
			"Artists" => Artists, 
			"Popularity" => Popularity, 
			_ => base.GetProperty(propertyName), 
		};
	}
}
