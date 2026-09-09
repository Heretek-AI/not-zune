using System.Collections;
using Microsoft.Iris;

namespace ZuneXml;

internal class RecommendedAlbum : Album
{
	internal IList Reasons => (IList)base.GetProperty("Reasons");

	internal string ReferrerContext => (string)base.GetProperty("ReferrerContext");

	internal static XmlDataProviderObject ConstructRecommendedAlbumObject(DataProviderQuery owner, object objectTypeCookie)
	{
		return new RecommendedAlbum(owner, objectTypeCookie);
	}

	internal RecommendedAlbum(DataProviderQuery owner, object resultTypeCookie)
		: base(owner, resultTypeCookie)
	{
	}

	public override object GetProperty(string propertyName)
	{
		return propertyName switch
		{
			"PointsPrice" => base.PointsPrice, 
			"CanPurchase" => base.CanPurchase, 
			"CanPurchaseMP3" => base.CanPurchaseMP3, 
			"InCollection" => base.InCollection, 
			"LibraryId" => base.LibraryId, 
			_ => base.GetProperty(propertyName), 
		};
	}
}
