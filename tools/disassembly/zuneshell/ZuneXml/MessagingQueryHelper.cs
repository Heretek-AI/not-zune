using Microsoft.Iris;
using Microsoft.Zune.Service;
using ZuneUI;

namespace ZuneXml;

internal class MessagingQueryHelper : ZuneServiceQueryHelper
{
	private EServiceEndpointId _endPoint;

	internal static ZuneServiceQueryHelper ConstructMessagingQueryHelper(ZuneServiceQuery query)
	{
		return new MessagingQueryHelper(query);
	}

	internal MessagingQueryHelper(ZuneServiceQuery query)
		: base(query)
	{
		//IL_0009: Unknown result type (might be due to invalid IL or missing references)
		_endPoint = (EServiceEndpointId)5;
		query.CachePolicy = (HttpRequestCachePolicy)0;
		query.PassportTicketType = (EPassportPolicyId)2;
	}

	internal override string GetResourceUri()
	{
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		string text = (string)((DataProviderQuery)base.Query).GetProperty("ZuneTag");
		if (string.IsNullOrEmpty(text))
		{
			return null;
		}
		string text2 = (string)((DataProviderQuery)base.Query).GetProperty("RequestType");
		if (string.IsNullOrEmpty(text2))
		{
			return null;
		}
		string endPointUri = Service.GetEndPointUri(_endPoint);
		return UrlHelper.MakeUrl($"{endPointUri}/messaging/{text.ToLower()}/inbox/{text2}");
	}
}
