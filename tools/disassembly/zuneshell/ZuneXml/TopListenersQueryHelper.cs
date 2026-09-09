using Microsoft.Iris;
using Microsoft.Zune.Service;

namespace ZuneXml;

internal class TopListenersQueryHelper : ZuneServiceQueryHelper
{
	internal static ZuneServiceQueryHelper ConstructTopListenersQueryHelper(ZuneServiceQuery query)
	{
		return new TopListenersQueryHelper(query);
	}

	internal TopListenersQueryHelper(ZuneServiceQuery query)
		: base(query)
	{
		query.PassportTicketType = (EPassportPolicyId)3;
	}

	internal override string GetResourceUri()
	{
		string text = (string)((DataProviderQuery)base.Query).GetProperty("ArtistId");
		if (string.IsNullOrEmpty(text))
		{
			return null;
		}
		string endPointUri = Service.GetEndPointUri((EServiceEndpointId)3);
		return $"{endPointUri}/music/artist/{text.ToLower()}/toplisteners";
	}
}
