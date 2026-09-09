using System;
using Microsoft.Iris;

namespace ZuneXml;

internal class MiniAlbum : MiniMedia
{
	internal bool Premium => (bool)GetProperty("Premium");

	internal override Guid Id => (Guid)GetProperty("Id");

	internal override string Title => (string)GetProperty("Title");

	internal static XmlDataProviderObject ConstructMiniAlbumObject(DataProviderQuery owner, object objectTypeCookie)
	{
		return new MiniAlbum(owner, objectTypeCookie);
	}

	internal MiniAlbum(DataProviderQuery owner, object resultTypeCookie)
		: base(owner, resultTypeCookie)
	{
	}
}
