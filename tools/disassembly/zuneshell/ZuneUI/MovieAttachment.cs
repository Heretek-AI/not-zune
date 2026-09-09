using System;

namespace ZuneUI;

public class MovieAttachment : VideoAttachment
{
	public const string RequestTypeString = "movie";

	public override string RequestType => "movie";

	public MovieAttachment(Guid id, string title, string imageUri)
		: base(id, title, string.Empty, imageUri)
	{
		base.AttachmentUI = "res://ZuneShellResources!SocialComposer.uix#MovieAttachmentUI";
		base.Description = Shell.LoadString(StringId.IDS_COMPOSE_MESSAGE_MOVIE_ATTACHMENT);
	}
}
