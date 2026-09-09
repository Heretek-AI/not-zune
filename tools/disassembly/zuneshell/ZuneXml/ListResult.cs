using System.Collections;
using Microsoft.Iris;

namespace ZuneXml;

internal abstract class ListResult : XmlDataProviderObject
{
	internal abstract IList Items { get; }

	protected ListResult(DataProviderQuery owner, object resultTypeCookie)
		: base(owner, resultTypeCookie)
	{
	}
}
