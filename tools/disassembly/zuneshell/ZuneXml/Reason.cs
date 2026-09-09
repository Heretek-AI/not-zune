using Microsoft.Iris;

namespace ZuneXml;

internal class Reason : XmlDataProviderObject
{
	internal string Description => (string)GetProperty("Description");

	internal static XmlDataProviderObject ConstructReasonObject(DataProviderQuery owner, object objectTypeCookie)
	{
		return new Reason(owner, objectTypeCookie);
	}

	internal Reason(DataProviderQuery owner, object resultTypeCookie)
		: base(owner, resultTypeCookie)
	{
	}
}
