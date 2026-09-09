using System;
using System.Collections;
using Microsoft.Iris;

namespace ZuneXml;

internal abstract class TrackHistory : Track
{
	internal abstract DateTime Date { get; }

	internal abstract IList MediaInstances { get; }

	protected TrackHistory(DataProviderQuery owner, object resultTypeCookie)
		: base(owner, resultTypeCookie)
	{
	}
}
