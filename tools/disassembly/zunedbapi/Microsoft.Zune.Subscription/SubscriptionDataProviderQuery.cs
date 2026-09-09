using Microsoft.Iris;

namespace Microsoft.Zune.Subscription;

public class SubscriptionDataProviderQuery : DataProviderQuery
{
	internal SubscriptionDataProviderQuery(object queryTypeCookie)
		: base(queryTypeCookie)
	{
	}

	protected override void BeginExecute()
	{
		string text = (string)((DataProviderQuery)this).GetProperty("FeedUrl");
		string text2 = (string)((DataProviderQuery)this).GetProperty("Sort");
		string serviceId = (string)((DataProviderQuery)this).GetProperty("ServiceID");
		if (((DataProviderQuery)this).Result != null)
		{
			VirtualSubscriptionEpisodeList virtualSubscriptionEpisodeList = (VirtualSubscriptionEpisodeList)((SubscriptionDataProviderQueryResult)((DataProviderQuery)this).Result).GetProperty("Items");
			if (null != virtualSubscriptionEpisodeList && virtualSubscriptionEpisodeList.FeedUrl == text)
			{
				virtualSubscriptionEpisodeList.Sort(text2);
				((DataProviderQuery)this).FirePropertyChanged("Result");
			}
			else
			{
				((SubscriptionDataProviderQueryResult)((DataProviderQuery)this).Result).OnDispose();
				((DataProviderQuery)this).Result = new SubscriptionDataProviderQueryResult((DataProviderQuery)(object)this, ((DataProviderQuery)this).ResultTypeCookie, text, serviceId, text2);
			}
		}
		else
		{
			((DataProviderQuery)this).Result = new SubscriptionDataProviderQueryResult((DataProviderQuery)(object)this, ((DataProviderQuery)this).ResultTypeCookie, text, serviceId, text2);
		}
	}

	protected override void OnDispose()
	{
		if (((DataProviderQuery)this).Result != null)
		{
			((SubscriptionDataProviderQueryResult)((DataProviderQuery)this).Result).OnDispose();
		}
		((DataProviderQuery)this).Result = null;
	}
}
