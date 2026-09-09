using System;
using Microsoft.Iris;

namespace ZuneXml;

internal abstract class Thumbnail : XmlDataProviderObject
{
	internal abstract string Id { get; }

	internal abstract string Title { get; }

	internal abstract string SortTitle { get; }

	internal abstract Guid ImageId { get; }

	protected Thumbnail(DataProviderQuery owner, object resultTypeCookie)
		: base(owner, resultTypeCookie)
	{
	}
}
