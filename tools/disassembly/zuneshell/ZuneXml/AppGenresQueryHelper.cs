using System;
using Microsoft.Iris;

namespace ZuneXml;

internal class AppGenresQueryHelper : CatalogServiceQueryHelper
{
	internal static AppGenresQueryHelper ConstructAppGenresQueryHelper(ZuneServiceQuery query)
	{
		return new AppGenresQueryHelper(query);
	}

	internal AppGenresQueryHelper(ZuneServiceQuery query)
		: base(query)
	{
	}

	internal override bool OnQueryFilterDataProviderObject(XmlDataProviderObject dataObject)
	{
		bool result = false;
		if (((DataProviderObject)dataObject).GetProperty("Id") is string text && text.Equals("apps.games", StringComparison.OrdinalIgnoreCase))
		{
			result = true;
		}
		return result;
	}
}
