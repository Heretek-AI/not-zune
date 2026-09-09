using System;
using Microsoft.Zune.Util;

namespace ZuneUI;

public class ProfileAttachment : Attachment
{
	public const string RequestTypeString = "card";

	public override MediaType MediaType => MediaType.UserCard;

	public override string RequestType => "card";

	public override string[] Properties => new string[4] { "type", RequestType, "zunetag", base.Title };

	public ProfileAttachment(string zuneTag, string status, string imageUri)
		: base(Guid.Empty, zuneTag, status, imageUri)
	{
		base.AttachmentUI = "res://ZuneShellResources!SocialComposer.uix#ProfileAttachmentUI";
		base.Description = Shell.LoadString(StringId.IDS_COMPOSE_MESSAGE_FRIEND_ATTACHMENT);
	}

	public override void LogSend()
	{
		SQMLog.Log((SQMDataId)57, 1);
	}
}
