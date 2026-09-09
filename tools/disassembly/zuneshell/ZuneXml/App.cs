using System;
using Microsoft.Iris;

namespace ZuneXml;

internal class App : MiniMedia
{
	internal double Price => SelectBestOfferRight()?.CurrencyPrice ?? 0.0;

	internal string DisplayPrice => SelectBestOfferRight()?.DisplayPrice;

	internal string DisplayPriceFull => Rights.GetOfferRight(MediaRightsEnum.Purchase, ClientTypeEnum.WindowsPhone, PriceTypeEnum.Currency)?.DisplayPrice;

	internal string DisplayPriceTrial => Rights.GetOfferRight(MediaRightsEnum.PurchaseTrial, ClientTypeEnum.WindowsPhone, PriceTypeEnum.Currency)?.DisplayPrice;

	internal bool CanDownload
	{
		get
		{
			if (!Rights.HasRights(MediaRightsEnum.Purchase, ClientTypeEnum.Zune))
			{
				return Rights.HasRights(MediaRightsEnum.Download, ClientTypeEnum.Zune);
			}
			return true;
		}
	}

	internal bool CanPurchase
	{
		get
		{
			if (!CanPurchaseFull)
			{
				return CanPurchaseTrial;
			}
			return true;
		}
	}

	internal bool CanPurchaseFull => Rights.HasOfferRights(MediaRightsEnum.Purchase, ClientTypeEnum.WindowsPhone, PriceTypeEnum.Currency);

	internal bool CanPurchaseTrial => Rights.HasOfferRights(MediaRightsEnum.PurchaseTrial, ClientTypeEnum.WindowsPhone, PriceTypeEnum.Currency);

	internal string SortTitle => (string)base.GetProperty("SortTitle");

	internal Guid ImageId => (Guid)base.GetProperty("ImageId");

	internal DateTime ReleaseDate => (DateTime)base.GetProperty("ReleaseDate");

	internal string GenreName => (string)base.GetProperty("GenreName");

	internal string Version => (string)base.GetProperty("Version");

	internal string Author => (string)base.GetProperty("Author");

	internal string Publisher => (string)base.GetProperty("Publisher");

	internal string ShortDescription => (string)base.GetProperty("ShortDescription");

	internal float AverageRating => (float)base.GetProperty("AverageRating");

	internal AppMediaRights Rights => (AppMediaRights)base.GetProperty("Rights");

	internal override Guid Id => (Guid)base.GetProperty("Id");

	internal override string Title => (string)base.GetProperty("Title");

	private Right SelectBestOfferRight()
	{
		Right offerRight = Rights.GetOfferRight(MediaRightsEnum.Purchase, ClientTypeEnum.WindowsPhone, PriceTypeEnum.Currency);
		if (offerRight == null)
		{
			offerRight = Rights.GetOfferRight(MediaRightsEnum.PurchaseTrial, ClientTypeEnum.WindowsPhone, PriceTypeEnum.Currency);
		}
		return offerRight;
	}

	internal static XmlDataProviderObject ConstructAppObject(DataProviderQuery owner, object objectTypeCookie)
	{
		return new App(owner, objectTypeCookie);
	}

	internal App(DataProviderQuery owner, object resultTypeCookie)
		: base(owner, resultTypeCookie)
	{
	}

	public override object GetProperty(string propertyName)
	{
		return propertyName switch
		{
			"Price" => Price, 
			"DisplayPrice" => DisplayPrice, 
			"DisplayPriceFull" => DisplayPriceFull, 
			"DisplayPriceTrial" => DisplayPriceTrial, 
			"CanPurchase" => CanPurchase, 
			"CanPurchaseFull" => CanPurchaseFull, 
			"CanPurchaseTrial" => CanPurchaseTrial, 
			"CanDownload" => CanDownload, 
			_ => base.GetProperty(propertyName), 
		};
	}
}
