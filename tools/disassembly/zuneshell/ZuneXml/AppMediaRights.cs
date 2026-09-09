using Microsoft.Iris;

namespace ZuneXml;

internal class AppMediaRights : MediaRights
{
	internal static XmlDataProviderObject ConstructAppMediaRightsObject(DataProviderQuery owner, object objectTypeCookie)
	{
		return new AppMediaRights(owner, objectTypeCookie);
	}

	internal AppMediaRights(DataProviderQuery owner, object resultTypeCookie)
		: base(owner, resultTypeCookie)
	{
	}

	public override object GetProperty(string propertyName)
	{
		string text;
		if ((text = propertyName) != null && text == "Languages")
		{
			return base.Languages;
		}
		return base.GetProperty(propertyName);
	}
}
