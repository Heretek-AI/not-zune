using System;
using Microsoft.Iris;

namespace ZuneXml;

internal class MiniArtist : MiniMedia
{
	internal override Guid Id => (Guid)GetProperty("Id");

	internal override string Title => (string)GetProperty("Title");

	internal static XmlDataProviderObject ConstructMiniArtistObject(DataProviderQuery owner, object objectTypeCookie)
	{
		return new MiniArtist(owner, objectTypeCookie);
	}

	internal MiniArtist(DataProviderQuery owner, object resultTypeCookie)
		: base(owner, resultTypeCookie)
	{
	}
}
