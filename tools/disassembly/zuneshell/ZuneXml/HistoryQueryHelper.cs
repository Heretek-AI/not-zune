using System.Text;
using Microsoft.Iris;
using Microsoft.Zune.Service;

namespace ZuneXml;

internal class HistoryQueryHelper : ZuneServiceQueryHelper
{
	protected string _api;

	internal HistoryQueryHelper(ZuneServiceQuery query)
		: base(query)
	{
		query.PassportTicketType = (EPassportPolicyId)2;
		query.CachePolicy = (HttpRequestCachePolicy)0;
	}

	internal override string GetResourceUri()
	{
		StringBuilder stringBuilder = new StringBuilder(128);
		stringBuilder.Append(Service.GetEndPointUri((EServiceEndpointId)23));
		stringBuilder.Append(_api);
		bool fFirst = true;
		ZuneServiceQueryHelper.AppendParam(stringBuilder, "tunerType", "zunePCClient", ref fFirst);
		if (((DataProviderQuery)base.Query).GetProperty("MediaType") is string value)
		{
			ZuneServiceQueryHelper.AppendParam(stringBuilder, "mediaTypeOrCategory", value, ref fFirst);
		}
		ZuneServiceQueryHelper.AppendParam(stringBuilder, "startIndex", "1", ref fFirst);
		object property = ((DataProviderQuery)base.Query).GetProperty("ChunkSize");
		if (property is int)
		{
			ZuneServiceQueryHelper.AppendParam(stringBuilder, "chunkSize", property.ToString(), ref fFirst);
		}
		return stringBuilder.ToString();
	}

	internal override string GetQueryPostBody()
	{
		return "";
	}
}
