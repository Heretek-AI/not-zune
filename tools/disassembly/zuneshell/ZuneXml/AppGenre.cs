using Microsoft.Iris;

namespace ZuneXml;

internal class AppGenre : Category
{
	internal bool IsRoot => (bool)GetProperty("IsRoot");

	internal override string Id => (string)GetProperty("Id");

	internal override string Title => (string)GetProperty("Title");

	internal static XmlDataProviderObject ConstructAppGenreObject(DataProviderQuery owner, object objectTypeCookie)
	{
		return new AppGenre(owner, objectTypeCookie);
	}

	internal AppGenre(DataProviderQuery owner, object resultTypeCookie)
		: base(owner, resultTypeCookie)
	{
	}
}
