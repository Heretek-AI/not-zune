using System;
using System.Collections;
using Microsoft.Iris;

namespace ZuneXml;

internal class WinPhoneAppPurchaseHistory : WinPhoneAppHistory
{
	internal override string Title => (string)base.GetProperty("Title");

	internal override Guid Id => (Guid)base.GetProperty("Id");

	internal override DateTime Date => (DateTime)base.GetProperty("Date");

	internal override IList MediaInstances => (IList)base.GetProperty("MediaInstances");

	internal static XmlDataProviderObject ConstructWinPhoneAppPurchaseHistoryObject(DataProviderQuery owner, object objectTypeCookie)
	{
		return new WinPhoneAppPurchaseHistory(owner, objectTypeCookie);
	}

	internal WinPhoneAppPurchaseHistory(DataProviderQuery owner, object resultTypeCookie)
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
