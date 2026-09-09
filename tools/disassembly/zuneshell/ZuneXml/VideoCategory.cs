using Microsoft.Iris;

namespace ZuneXml;

internal class VideoCategory : Category
{
	internal override string Id => (string)GetProperty("Id");

	internal override string Title => (string)GetProperty("Title");

	internal static XmlDataProviderObject ConstructVideoCategoryObject(DataProviderQuery owner, object objectTypeCookie)
	{
		return new VideoCategory(owner, objectTypeCookie);
	}

	internal VideoCategory(DataProviderQuery owner, object resultTypeCookie)
		: base(owner, resultTypeCookie)
	{
	}
}
