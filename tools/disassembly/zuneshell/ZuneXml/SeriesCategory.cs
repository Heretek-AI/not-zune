using Microsoft.Iris;

namespace ZuneXml;

internal class SeriesCategory : PodcastSubCategory
{
	internal override string Id => (string)GetProperty("Id");

	internal override string Title => (string)GetProperty("Title");

	internal static XmlDataProviderObject ConstructSeriesCategoryObject(DataProviderQuery owner, object objectTypeCookie)
	{
		return new SeriesCategory(owner, objectTypeCookie);
	}

	internal SeriesCategory(DataProviderQuery owner, object resultTypeCookie)
		: base(owner, resultTypeCookie)
	{
	}
}
