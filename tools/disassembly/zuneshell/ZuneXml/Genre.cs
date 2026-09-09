using Microsoft.Iris;

namespace ZuneXml;

internal class Genre : Category
{
	internal override string Id => (string)GetProperty("Id");

	internal override string Title => (string)GetProperty("Title");

	internal static XmlDataProviderObject ConstructGenreObject(DataProviderQuery owner, object objectTypeCookie)
	{
		return new Genre(owner, objectTypeCookie);
	}

	internal Genre(DataProviderQuery owner, object resultTypeCookie)
		: base(owner, resultTypeCookie)
	{
	}
}
