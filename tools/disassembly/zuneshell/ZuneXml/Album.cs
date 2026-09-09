using System;
using System.Collections;
using Microsoft.Iris;
using Microsoft.Zune.Service;
using Microsoft.Zune.Shell;

namespace ZuneXml;

internal class Album : Media
{
	private int _dbMediaId = -1;

	internal bool CanPurchaseMP3
	{
		get
		{
			Right offer;
			return Rights.HasOfferRights(MediaRightsEnum.AlbumPurchase, AudioEncodingEnum.MP3, PriceTypeEnum.Points, out offer);
		}
	}

	internal bool CanPurchase
	{
		get
		{
			Right offer;
			return Rights.HasOfferRights(MediaRightsEnum.AlbumPurchase, AudioEncodingEnum.WMA, PriceTypeEnum.Points, out offer) || Rights.HasOfferRights(MediaRightsEnum.AlbumPurchase, AudioEncodingEnum.MP3, PriceTypeEnum.Points, out offer);
		}
	}

	internal int PointsPrice
	{
		get
		{
			int result = -1;
			if (Rights.HasOfferRights(MediaRightsEnum.AlbumPurchase, AudioEncodingEnum.MP3, PriceTypeEnum.Points, out var offer))
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

	internal bool InCollection
	{
		get
		{
			bool result = false;
			if (Id != Guid.Empty)
			{
				int num = default(int);
				result = ZuneApplication.Service.InVisibleCollection(Id, (EContentType)1, ref num);
			}
			return result;
		}
	}

	internal int LibraryId
	{
		get
		{
			int num = -1;
			if (Id != Guid.Empty)
			{
				ZuneApplication.Service.InVisibleCollection(Id, (EContentType)1, ref num);
				if (num == -1)
				{
					num = _dbMediaId;
				}
			}
			return num;
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

	internal Genre PrimaryGenre => (Genre)base.GetProperty("PrimaryGenre");

	internal DateTime ReleaseDate => (DateTime)base.GetProperty("ReleaseDate");

	internal bool Explicit => (bool)base.GetProperty("Explicit");

	internal bool Actionable => (bool)base.GetProperty("Actionable");

	internal bool Premium => (bool)base.GetProperty("Premium");

	internal string Label => (string)base.GetProperty("Label");

	internal string ReviewLink => (string)base.GetProperty("ReviewLink");

	internal IList Genres => (IList)base.GetProperty("Genres");

	internal IList Tracks => (IList)base.GetProperty("Tracks");

	internal IList MusicVideos => (IList)base.GetProperty("MusicVideos");

	internal override string Title => (string)base.GetProperty("Title");

	internal override string SortTitle => (string)base.GetProperty("SortTitle");

	internal override Guid Id => (Guid)base.GetProperty("Id");

	internal override MiniArtist PrimaryArtist => (MiniArtist)base.GetProperty("PrimaryArtist");

	internal override Guid ImageId => (Guid)base.GetProperty("ImageId");

	internal override double Popularity => (double)base.GetProperty("Popularity");

	internal override MediaRights Rights => (MediaRights)base.GetProperty("Rights");

	internal override IList Artists => (IList)base.GetProperty("Artists");

	internal static XmlDataProviderObject ConstructAlbumObject(DataProviderQuery owner, object objectTypeCookie)
	{
		return new Album(owner, objectTypeCookie);
	}

	internal Album(DataProviderQuery owner, object resultTypeCookie)
		: base(owner, resultTypeCookie)
	{
	}

	public override object GetProperty(string propertyName)
	{
		return propertyName switch
		{
			"PointsPrice" => PointsPrice, 
			"CanPurchase" => CanPurchase, 
			"CanPurchaseMP3" => CanPurchaseMP3, 
			"InCollection" => InCollection, 
			"LibraryId" => LibraryId, 
			_ => base.GetProperty(propertyName), 
		};
	}
}
