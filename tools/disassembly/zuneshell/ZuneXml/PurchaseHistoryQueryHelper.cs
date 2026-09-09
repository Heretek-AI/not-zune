using Microsoft.Iris;

namespace ZuneXml;

internal class PurchaseHistoryQueryHelper : HistoryQueryHelper
{
	internal static PurchaseHistoryQueryHelper ConstructPurchaseHistoryQueryHelper(ZuneServiceQuery query)
	{
		return new PurchaseHistoryQueryHelper(query);
	}

	internal PurchaseHistoryQueryHelper(ZuneServiceQuery query)
		: base(query)
	{
		_api = "/billing/purchaseHistory";
	}

	internal override bool OnQueryFilterDataProviderObject(XmlDataProviderObject dataObject)
	{
		bool result = false;
		if (dataObject is VideoHistory)
		{
			bool flag = false;
			VideoHistory videoHistory = (VideoHistory)dataObject;
			if (videoHistory.MediaInstances != null)
			{
				foreach (MediaInstance mediaInstance in videoHistory.MediaInstances)
				{
					if (mediaInstance.LicenseRight == "Rent" || mediaInstance.LicenseRight == "RentStream")
					{
						flag = true;
						break;
					}
				}
			}
			object property = ((DataProviderQuery)base.Query).GetProperty("Rentals");
			bool flag2 = !(property is bool) || !(bool)property;
			result = flag == flag2;
		}
		return result;
	}
}
