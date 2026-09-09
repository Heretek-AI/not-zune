using Microsoft.Iris;

namespace ZuneXml;

internal abstract class Category : XmlDataProviderObject
{
	internal abstract string Id { get; }

	internal abstract string Title { get; }

	protected Category(DataProviderQuery owner, object resultTypeCookie)
		: base(owner, resultTypeCookie)
	{
	}
}
