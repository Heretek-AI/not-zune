using Microsoft.Iris;

namespace ZuneXml;

internal class ChannelReason : XmlDataProviderObject
{
	internal string Id => (string)GetProperty("Id");

	internal string Description => (string)GetProperty("Description");

	internal static XmlDataProviderObject ConstructChannelReasonObject(DataProviderQuery owner, object objectTypeCookie)
	{
		return new ChannelReason(owner, objectTypeCookie);
	}

	internal ChannelReason(DataProviderQuery owner, object resultTypeCookie)
		: base(owner, resultTypeCookie)
	{
	}
}
