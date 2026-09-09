using Microsoft.Iris;

namespace Microsoft.Zune.Subscription;

public class SubscriptionDataProvider
{
	public static void Register()
	{
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Expected O, but got Unknown
		Application.RegisterDataProvider("DynamicRssView", new DataProviderQueryFactory(ConstructQuery));
	}

	public static DataProviderQuery ConstructQuery(object queryTypeCookie)
	{
		return (DataProviderQuery)(object)new SubscriptionDataProviderQuery(queryTypeCookie);
	}

	private SubscriptionDataProvider()
	{
	}
}
