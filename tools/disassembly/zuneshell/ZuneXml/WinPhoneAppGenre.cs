using Microsoft.Iris;

namespace ZuneXml;

internal class WinPhoneAppGenre : AppGenre
{
	internal static XmlDataProviderObject ConstructWinPhoneAppGenreObject(DataProviderQuery owner, object objectTypeCookie)
	{
		return new WinPhoneAppGenre(owner, objectTypeCookie);
	}

	internal WinPhoneAppGenre(DataProviderQuery owner, object resultTypeCookie)
		: base(owner, resultTypeCookie)
	{
	}
}
