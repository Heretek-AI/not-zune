using System;
using System.Collections;
using Microsoft.Iris;

namespace ZuneXml;

internal abstract class WinPhoneAppHistory : App
{
	internal abstract DateTime Date { get; }

	internal abstract IList MediaInstances { get; }

	protected WinPhoneAppHistory(DataProviderQuery owner, object resultTypeCookie)
		: base(owner, resultTypeCookie)
	{
	}
}
