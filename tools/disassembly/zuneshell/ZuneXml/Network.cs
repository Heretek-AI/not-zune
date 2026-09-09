using Microsoft.Iris;

namespace ZuneXml;

internal class Network : Category
{
	internal override string Id => (string)GetProperty("Id");

	internal override string Title => (string)GetProperty("Title");

	internal static XmlDataProviderObject ConstructNetworkObject(DataProviderQuery owner, object objectTypeCookie)
	{
		return new Network(owner, objectTypeCookie);
	}

	internal Network(DataProviderQuery owner, object resultTypeCookie)
		: base(owner, resultTypeCookie)
	{
	}
}
