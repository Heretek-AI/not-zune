using System;
using Microsoft.Iris;

namespace ZuneXml;

internal abstract class MiniMedia : XmlDataProviderObject
{
	internal abstract Guid Id { get; }

	internal abstract string Title { get; }

	protected MiniMedia(DataProviderQuery owner, object resultTypeCookie)
		: base(owner, resultTypeCookie)
	{
	}
}
