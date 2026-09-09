using Microsoft.Iris;

namespace ZuneXml;

internal class WinPhoneApp : App
{
	internal static XmlDataProviderObject ConstructWinPhoneAppObject(DataProviderQuery owner, object objectTypeCookie)
	{
		return new WinPhoneApp(owner, objectTypeCookie);
	}

	internal WinPhoneApp(DataProviderQuery owner, object resultTypeCookie)
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
