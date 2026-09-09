using System;

namespace ZuneUI;

public class EpisodeAttachment : VideoAttachment
{
	public const string RequestTypeString = "video";

	public override string RequestType => "video";

	public EpisodeAttachment(Guid id, string title, string subtitle, string imageUri)
		: base(id, title, subtitle, imageUri)
	{
		base.AttachmentUI = "res://ZuneShellResources!SocialComposer.uix#EpisodeAttachmentUI";
		base.Description = Shell.LoadString(StringId.IDS_COMPOSE_MESSAGE_VIDEO_ATTACHMENT);
	}
}
