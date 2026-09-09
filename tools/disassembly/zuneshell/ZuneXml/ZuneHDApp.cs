using Microsoft.Iris;

namespace ZuneXml;

internal class ZuneHDApp : App
{
	internal static XmlDataProviderObject ConstructZuneHDAppObject(DataProviderQuery owner, object objectTypeCookie)
	{
		return new ZuneHDApp(owner, objectTypeCookie);
	}

	internal ZuneHDApp(DataProviderQuery owner, object resultTypeCookie)
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
