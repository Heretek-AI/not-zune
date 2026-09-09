using System.Collections;
using Microsoft.Iris;

namespace ZuneXml;

internal class AppCapabilities : XmlDataProviderObject
{
	internal IList Items => (IList)GetProperty("Items");

	internal static XmlDataProviderObject ConstructAppCapabilitiesObject(DataProviderQuery owner, object objectTypeCookie)
	{
		return new AppCapabilities(owner, objectTypeCookie);
	}

	internal AppCapabilities(DataProviderQuery owner, object resultTypeCookie)
		: base(owner, resultTypeCookie)
	{
	}
}
