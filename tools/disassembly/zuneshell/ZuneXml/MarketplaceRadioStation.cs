using System;
using System.Collections;
using Microsoft.Iris;

namespace ZuneXml;

internal class MarketplaceRadioStation : MiniMedia
{
	internal IList Genres => (IList)GetProperty("Genres");

	internal override Guid Id => (Guid)GetProperty("Id");

	internal override string Title => (string)GetProperty("Title");

	internal static XmlDataProviderObject ConstructMarketplaceRadioStationObject(DataProviderQuery owner, object objectTypeCookie)
	{
		return new MarketplaceRadioStation(owner, objectTypeCookie);
	}

	internal MarketplaceRadioStation(DataProviderQuery owner, object resultTypeCookie)
		: base(owner, resultTypeCookie)
	{
	}
}
