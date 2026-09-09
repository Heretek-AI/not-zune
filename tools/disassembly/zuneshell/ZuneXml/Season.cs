using System;
using Microsoft.Iris;

namespace ZuneXml;

internal class Season : XmlDataProviderObject
{
	internal bool IsPriceDiscounted
	{
		get
		{
			Right offerRight = Rights.GetOfferRight(MediaRightsEnum.SeasonPurchase, ClientTypeEnum.None, PriceTypeEnum.Points);
			if (offerRight == null)
			{
				offerRight = Rights.GetOfferRight(MediaRightsEnum.SeasonPurchaseStream, ClientTypeEnum.None, PriceTypeEnum.Points);
			}
			if (offerRight != null)
			{
				return offerRight.Price < offerRight.OriginalPrice;
			}
			return false;
		}
	}

	internal int Id => (int)base.GetProperty("Id");

	internal string Title => (string)base.GetProperty("Title");

	internal Guid ImageId => (Guid)base.GetProperty("ImageId");

	internal DateTime ReleaseDate => (DateTime)base.GetProperty("ReleaseDate");

	internal int EpisodeCount => (int)base.GetProperty("EpisodeCount");

	internal string Description => (string)base.GetProperty("Description");

	internal string Rating => (string)base.GetProperty("Rating");

	internal MediaRights Rights => (MediaRights)base.GetProperty("Rights");

	internal bool IsComplete => (bool)base.GetProperty("IsComplete");

	internal static XmlDataProviderObject ConstructSeasonObject(DataProviderQuery owner, object objectTypeCookie)
	{
		return new Season(owner, objectTypeCookie);
	}

	internal Season(DataProviderQuery owner, object resultTypeCookie)
		: base(owner, resultTypeCookie)
	{
	}

	public override object GetProperty(string propertyName)
	{
		string text;
		if ((text = propertyName) != null && text == "IsPriceDiscounted")
		{
			return IsPriceDiscounted;
		}
		return base.GetProperty(propertyName);
	}
}
