using System;
using Microsoft.Zune.Util;

namespace ZuneUI;

public class PlaylistAttachment : Attachment
{
	public const string RequestTypeString = "playlist";

	public override MediaType MediaType => MediaType.Playlist;

	public override string RequestType => "playlist";

	public PlaylistAttachment(Guid id, string title, string imageUri)
		: base(id, title, null, imageUri)
	{
		base.AttachmentUI = "res://ZuneShellResources!SocialComposer.uix#PlaylistAttachmentUI";
		base.Description = Shell.LoadString(StringId.IDS_COMPOSE_MESSAGE_PLAYLIST_ATTACHMENT);
	}

	public override void LogSend()
	{
		SQMLog.Log((SQMDataId)55, 1);
	}
}
