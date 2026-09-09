using System;
using System.Collections;
using Microsoft.Iris;

namespace ZuneXml;

internal class AppData : App
{
	internal string Copyright => (string)base.GetProperty("Copyright");

	internal string Description => (string)base.GetProperty("Description");

	internal IList Screenshots => (IList)base.GetProperty("Screenshots");

	internal IList Genres => (IList)base.GetProperty("Genres");

	internal AppCapabilities Capabilities => (AppCapabilities)base.GetProperty("Capabilities");

	internal Guid BackgroundImageId => (Guid)base.GetProperty("BackgroundImageId");

	internal Guid RatingImageId => (Guid)base.GetProperty("RatingImageId");

	internal static XmlDataProviderObject ConstructAppDataObject(DataProviderQuery owner, object objectTypeCookie)
	{
		return new AppData(owner, objectTypeCookie);
	}

	internal AppData(DataProviderQuery owner, object resultTypeCookie)
		: base(owner, resultTypeCookie)
	{
	}

	public override object GetProperty(string propertyName)
	{
		return propertyName switch
		{
			"Price" => base.Price, 
			"DisplayPrice" => base.DisplayPrice, 
			"DisplayPriceFull" => base.DisplayPriceFull, 
			"DisplayPriceTrial" => base.DisplayPriceTrial, 
			"CanPurchase" => base.CanPurchase, 
			"CanPurchaseFull" => base.CanPurchaseFull, 
			"CanPurchaseTrial" => base.CanPurchaseTrial, 
			"CanDownload" => base.CanDownload, 
			_ => base.GetProperty(propertyName), 
		};
	}
}
