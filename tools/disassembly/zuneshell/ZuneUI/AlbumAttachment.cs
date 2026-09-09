using System;
using Microsoft.Zune.Util;

namespace ZuneUI;

public class AlbumAttachment : Attachment
{
	public const string RequestTypeString = "album";

	public override MediaType MediaType => MediaType.Album;

	public override string RequestType => "album";

	public AlbumAttachment(Guid id, string title, string artist, string imageUri, Guid imageId)
		: base(id, title, artist, UrlHelper.MakeCatalogImageUri(imageId))
	{
		base.AttachmentUI = "res://ZuneShellResources!SocialComposer.uix#AlbumAttachmentUI";
		base.Description = Shell.LoadString(StringId.IDS_COMPOSE_MESSAGE_ALBUM_ATTACHMENT);
	}

	public override void LogSend()
	{
		SQMLog.Log((SQMDataId)56, 1);
	}
}
