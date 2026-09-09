using System;
using System.Collections;
using Microsoft.Iris;

namespace ZuneXml;

internal class Artist : XmlDataProviderObject
{
	internal Guid Id => (Guid)GetProperty("Id");

	internal string Title => (string)GetProperty("Title");

	internal string SortTitle => (string)GetProperty("SortTitle");

	internal Guid ImageId => (Guid)GetProperty("ImageId");

	internal double Popularity => (double)GetProperty("Popularity");

	internal bool IsVariousArtist => (bool)GetProperty("IsVariousArtist");

	internal string BiographyLink => (string)GetProperty("BiographyLink");

	internal int PlayCount => (int)GetProperty("PlayCount");

	internal Genre PrimaryGenre => (Genre)GetProperty("PrimaryGenre");

	internal IList Genres => (IList)GetProperty("Genres");

	internal IList Moods => (IList)GetProperty("Moods");

	internal Guid AlbumImageId => (Guid)GetProperty("AlbumImageId");

	internal Guid BackgroundImageId => (Guid)GetProperty("BackgroundImageId");

	internal bool HasRadioChannel => (bool)GetProperty("HasRadioChannel");

	internal static XmlDataProviderObject ConstructArtistObject(DataProviderQuery owner, object objectTypeCookie)
	{
		return new Artist(owner, objectTypeCookie);
	}

	internal Artist(DataProviderQuery owner, object resultTypeCookie)
		: base(owner, resultTypeCookie)
	{
	}
}
