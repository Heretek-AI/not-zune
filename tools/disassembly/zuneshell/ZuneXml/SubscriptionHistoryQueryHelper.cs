namespace ZuneXml;

internal class SubscriptionHistoryQueryHelper : HistoryQueryHelper
{
	internal static SubscriptionHistoryQueryHelper ConstructSubscriptionHistoryQueryHelper(ZuneServiceQuery query)
	{
		return new SubscriptionHistoryQueryHelper(query);
	}

	internal SubscriptionHistoryQueryHelper(ZuneServiceQuery query)
		: base(query)
	{
		_api = "/account/subscriptionhistory";
	}
}
