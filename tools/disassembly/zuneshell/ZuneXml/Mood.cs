using Microsoft.Iris;

namespace ZuneXml;

internal class Mood : XmlDataProviderObject
{
	internal string Id => (string)GetProperty("Id");

	internal string Title => (string)GetProperty("Title");

	internal static XmlDataProviderObject ConstructMoodObject(DataProviderQuery owner, object objectTypeCookie)
	{
		return new Mood(owner, objectTypeCookie);
	}

	internal Mood(DataProviderQuery owner, object resultTypeCookie)
		: base(owner, resultTypeCookie)
	{
	}
}
