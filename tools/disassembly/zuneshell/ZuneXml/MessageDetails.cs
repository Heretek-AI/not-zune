using System;
using Microsoft.Iris;

namespace ZuneXml;

internal class MessageDetails : XmlDataProviderObject
{
	internal string TextContent => (string)GetProperty("TextContent");

	internal Guid MediaId => (Guid)GetProperty("MediaId");

	internal string ReplyLink => (string)GetProperty("ReplyLink");

	internal string AltLink => (string)GetProperty("AltLink");

	internal string AlbumTitle => (string)GetProperty("AlbumTitle");

	internal string ArtistName => (string)GetProperty("ArtistName");

	internal string SongTitle => (string)GetProperty("SongTitle");

	internal int TrackNumber => (int)GetProperty("TrackNumber");

	internal string PlaylistName => (string)GetProperty("PlaylistName");

	internal string PodcastName => (string)GetProperty("PodcastName");

	internal string PodcastUrl => (string)GetProperty("PodcastUrl");

	internal Guid PodcastMediaId => (Guid)GetProperty("PodcastMediaId");

	internal string UserTile => (string)GetProperty("UserTile");

	internal string ZuneTag => (string)GetProperty("ZuneTag");

	internal string ForumsMsgUrl => (string)GetProperty("ForumsMsgUrl");

	internal string NotifSubject => (string)GetProperty("NotifSubject");

	internal string NotifSource => (string)GetProperty("NotifSource");

	internal static XmlDataProviderObject ConstructMessageDetailsObject(DataProviderQuery owner, object objectTypeCookie)
	{
		return new MessageDetails(owner, objectTypeCookie);
	}

	internal MessageDetails(DataProviderQuery owner, object resultTypeCookie)
		: base(owner, resultTypeCookie)
	{
	}
}
