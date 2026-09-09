using System.Collections;

namespace ZuneUI;

internal class MessageTypeInfo
{
	private string _UIText;

	private string _detailsTemplate;

	private static Hashtable s_MessageTypes;

	public string UIText => _UIText;

	public string DetailsTemplate => _detailsTemplate;

	public MessageTypeInfo(StringId uiText, string detailsTemplate)
	{
		_UIText = Shell.LoadString(uiText);
		_detailsTemplate = detailsTemplate;
	}

	public static MessageTypeInfo GetMessageType(string serviceType)
	{
		if (s_MessageTypes == null)
		{
			s_MessageTypes = new Hashtable(17);
			s_MessageTypes.Add("album", new MessageTypeInfo(StringId.IDS_TYPE_ALBUM, "res://ZuneShellResources!InboxTrackDetails.uix#AlbumDetailsUI"));
			s_MessageTypes.Add("card", new MessageTypeInfo(StringId.IDS_TYPE_CARD, "res://ZuneShellResources!InboxProfileDetails.uix#CardDetailsUI"));
			s_MessageTypes.Add("forums", new MessageTypeInfo(StringId.IDS_TYPE_FORUM, "res://ZuneShellResources!InboxTextDetails.uix#ForumDetailsUI"));
			s_MessageTypes.Add("friendrequest", new MessageTypeInfo(StringId.IDS_TYPE_FRIENDREQUEST, "res://ZuneShellResources!InboxProfileDetails.uix#FriendRequestUI"));
			s_MessageTypes.Add("message", new MessageTypeInfo(StringId.IDS_TYPE_TEXT, "res://ZuneShellResources!InboxTextDetails.uix#TextDetailsUI"));
			s_MessageTypes.Add("musicvideo", new MessageTypeInfo(StringId.IDS_TYPE_MUSICVIDEO, "res://ZuneShellResources!InboxTextDetails.uix#UnknownDetails"));
			s_MessageTypes.Add("notification", new MessageTypeInfo(StringId.IDS_TYPE_NOTIFICATION, "res://ZuneShellResources!InboxTextDetails.uix#NotificationDetailsUI"));
			s_MessageTypes.Add("photos", new MessageTypeInfo(StringId.IDS_TYPE_PHOTOS, "res://ZuneShellResources!InboxPhotoDetails.uix#PhotoDetailsUI"));
			s_MessageTypes.Add("playlist", new MessageTypeInfo(StringId.IDS_TYPE_PLAYLIST, "res://ZuneShellResources!InboxTrackDetails.uix#PlaylistDetailsUI"));
			s_MessageTypes.Add("podcast", new MessageTypeInfo(StringId.IDS_TYPE_PODCASTSERIES, "res://ZuneShellResources!InboxPodcastDetails.uix#PodcastDetailsUI"));
			s_MessageTypes.Add("song", new MessageTypeInfo(StringId.IDS_TYPE_SONG, "res://ZuneShellResources!InboxTrackDetails.uix#TrackDetailsUI"));
			s_MessageTypes.Add("video", new MessageTypeInfo(StringId.IDS_TYPE_VIDEO, "res://ZuneShellResources!InboxVideoDetails.uix#EpisodeMessageDetailsUI"));
			s_MessageTypes.Add("movie", new MessageTypeInfo(StringId.IDS_TYPE_VIDEO, "res://ZuneShellResources!InboxVideoDetails.uix#MovieDetailsUI"));
			s_MessageTypes.Add("movietrailer", new MessageTypeInfo(StringId.IDS_TYPE_VIDEO, "res://ZuneShellResources!InboxVideoDetails.uix#TrailerDetailsUI"));
			s_MessageTypes.Add("", new MessageTypeInfo(StringId.IDS_TYPE_UNKNOWN, "res://ZuneShellResources!InboxTextDetails.uix#UnknownDetails"));
		}
		object obj = s_MessageTypes[serviceType];
		if (obj == null)
		{
			obj = s_MessageTypes[""];
		}
		return (MessageTypeInfo)obj;
	}
}
