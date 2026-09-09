using System;
using Microsoft.Iris;

namespace ZuneXml;

internal class ArtistEvent : XmlDataProviderObject
{
	internal string Type => (string)GetProperty("Type");

	internal string WebLinkUrl => (string)GetProperty("WebLinkUrl");

	internal DateTime Date => (DateTime)GetProperty("Date");

	internal string Venue => (string)GetProperty("Venue");

	internal string City => (string)GetProperty("City");

	internal string State => (string)GetProperty("State");

	internal string Country => (string)GetProperty("Country");

	internal static XmlDataProviderObject ConstructArtistEventObject(DataProviderQuery owner, object objectTypeCookie)
	{
		return new ArtistEvent(owner, objectTypeCookie);
	}

	internal ArtistEvent(DataProviderQuery owner, object resultTypeCookie)
		: base(owner, resultTypeCookie)
	{
	}
}
