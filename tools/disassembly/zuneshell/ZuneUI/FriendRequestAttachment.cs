using System;

namespace ZuneUI;

public class FriendRequestAttachment : Attachment
{
	public const string RequestTypeString = "friendrequest";

	public override MediaType MediaType => MediaType.Undefined;

	public override string RequestType => "friendrequest";

	public FriendRequestAttachment()
		: base(Guid.Empty, null, null, null)
	{
		base.AttachmentUI = null;
		base.Description = Shell.LoadString(StringId.IDS_COMPOSE_MESSAGE_FRIENDREQUEST_ATTACHMENT);
	}
}
