using System.Collections;
using Microsoft.Iris;

namespace ZuneXml;

internal class RecommendedArtist : Artist
{
	internal IList Reasons => (IList)GetProperty("Reasons");

	internal string ReferrerContext => (string)GetProperty("ReferrerContext");

	internal static XmlDataProviderObject ConstructRecommendedArtistObject(DataProviderQuery owner, object objectTypeCookie)
	{
		return new RecommendedArtist(owner, objectTypeCookie);
	}

	internal RecommendedArtist(DataProviderQuery owner, object resultTypeCookie)
		: base(owner, resultTypeCookie)
	{
	}
}
