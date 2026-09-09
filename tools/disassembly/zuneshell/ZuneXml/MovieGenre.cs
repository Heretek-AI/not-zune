using Microsoft.Iris;

namespace ZuneXml;

internal class MovieGenre : Category
{
	internal override string Id => (string)GetProperty("Id");

	internal override string Title => (string)GetProperty("Title");

	internal static XmlDataProviderObject ConstructMovieGenreObject(DataProviderQuery owner, object objectTypeCookie)
	{
		return new MovieGenre(owner, objectTypeCookie);
	}

	internal MovieGenre(DataProviderQuery owner, object resultTypeCookie)
		: base(owner, resultTypeCookie)
	{
	}
}
