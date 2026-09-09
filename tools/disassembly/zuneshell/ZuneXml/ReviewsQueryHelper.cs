using Microsoft.Zune.Service;

namespace ZuneXml;

internal class ReviewsQueryHelper : SubRepresentationCatalogServiceQueryHelper
{
	internal static ZuneServiceQueryHelper ConstructReviewsQueryHelper(ZuneServiceQuery query)
	{
		return new ReviewsQueryHelper(query);
	}

	internal ReviewsQueryHelper(ZuneServiceQuery query)
		: base(query)
	{
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		_endPoint = (EServiceEndpointId)32;
	}
}
