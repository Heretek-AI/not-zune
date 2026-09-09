using System;
using Microsoft.Zune.Util;

namespace ZuneUI;

public class TrackAttachment : Attachment
{
	public const string RequestTypeString = "song";

	public override MediaType MediaType => MediaType.Track;

	public override string RequestType => "song";

	public TrackAttachment(Guid id, string title, string artist, string imageUri)
		: base(id, title, artist, imageUri)
	{
		base.AttachmentUI = "res://ZuneShellResources!SocialComposer.uix#TrackAttachmentUI";
		base.Description = Shell.LoadString(StringId.IDS_COMPOSE_MESSAGE_TRACK_ATTACHMENT);
	}

	public override void LogSend()
	{
		SQMLog.Log((SQMDataId)58, 1);
	}
}
