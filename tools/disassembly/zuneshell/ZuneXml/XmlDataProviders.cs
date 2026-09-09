using Microsoft.Iris;

namespace ZuneXml;

internal static class XmlDataProviders
{
	internal static void Register()
	{
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Expected O, but got Unknown
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_002c: Expected O, but got Unknown
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_0042: Expected O, but got Unknown
		Application.RegisterDataProvider("ZuneService", new DataProviderQueryFactory(ZuneServiceQuery.ConstructZuneServiceQuery));
		Application.RegisterDataProvider("WMISFAI", new DataProviderQueryFactory(WMISServiceDataProviderQuery.ConstructWmisQuery));
		Application.RegisterDataProvider("InboxImage", new DataProviderQueryFactory(InboxImageQuery.ConstructQuery));
	}
}
