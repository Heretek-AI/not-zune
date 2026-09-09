using System;

namespace ZuneUI;

public class TrailerAttachment : VideoAttachment
{
	public const string RequestTypeString = "movietrailer";

	public override string RequestType => "movietrailer";

	public TrailerAttachment(Guid id, string title, string imageUri)
		: base(id, title, string.Empty, imageUri)
	{
		base.AttachmentUI = "res://ZuneShellResources!SocialComposer.uix#TrailerAttachmentUI";
		base.Description = Shell.LoadString(StringId.IDS_COMPOSE_MESSAGE_TRAILER_ATTACHMENT);
	}
}
