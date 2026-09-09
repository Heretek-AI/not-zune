using Microsoft.Zune.Service;

namespace ZuneXml;

internal class RecommendationsQueryHelper : CatalogServiceQueryHelper
{
	internal static ZuneServiceQueryHelper ConstructRecommendationsQueryHelper(ZuneServiceQuery query)
	{
		return new RecommendationsQueryHelper(query);
	}

	internal RecommendationsQueryHelper(ZuneServiceQuery query)
		: base(query)
	{
		//IL_0009: Unknown result type (might be due to invalid IL or missing references)
		_endPoint = (EServiceEndpointId)7;
		query.CachePolicy = (HttpRequestCachePolicy)0;
	}
}
