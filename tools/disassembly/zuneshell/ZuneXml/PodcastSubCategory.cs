using Microsoft.Iris;

namespace ZuneXml;

internal abstract class PodcastSubCategory : Category
{
	protected PodcastSubCategory(DataProviderQuery owner, object resultTypeCookie)
		: base(owner, resultTypeCookie)
	{
	}
}
