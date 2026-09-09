using Microsoft.Iris;

namespace ZuneXml;

internal class ReviewListEntry : Review
{
	internal static XmlDataProviderObject ConstructReviewListEntryObject(DataProviderQuery owner, object objectTypeCookie)
	{
		return new ReviewListEntry(owner, objectTypeCookie);
	}

	internal ReviewListEntry(DataProviderQuery owner, object resultTypeCookie)
		: base(owner, resultTypeCookie)
	{
	}
}
