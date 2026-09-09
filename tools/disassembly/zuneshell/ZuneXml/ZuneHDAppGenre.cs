using Microsoft.Iris;

namespace ZuneXml;

internal class ZuneHDAppGenre : AppGenre
{
	internal static XmlDataProviderObject ConstructZuneHDAppGenreObject(DataProviderQuery owner, object objectTypeCookie)
	{
		return new ZuneHDAppGenre(owner, objectTypeCookie);
	}

	internal ZuneHDAppGenre(DataProviderQuery owner, object resultTypeCookie)
		: base(owner, resultTypeCookie)
	{
	}
}
