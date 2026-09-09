using Microsoft.Iris;

namespace ZuneXml;

internal class MovieStudio : Category
{
	internal override string Id => (string)GetProperty("Id");

	internal override string Title => (string)GetProperty("Title");

	internal static XmlDataProviderObject ConstructMovieStudioObject(DataProviderQuery owner, object objectTypeCookie)
	{
		return new MovieStudio(owner, objectTypeCookie);
	}

	internal MovieStudio(DataProviderQuery owner, object resultTypeCookie)
		: base(owner, resultTypeCookie)
	{
	}
}
