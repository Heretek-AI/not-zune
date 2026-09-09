using System;
using Microsoft.Zune.Util;

namespace ZuneUI;

public abstract class VideoAttachment : Attachment
{
	public override MediaType MediaType => MediaType.Video;

	public VideoAttachment(Guid id, string title, string subtitle, string imageUri)
		: base(id, title, subtitle, imageUri)
	{
	}

	public override void LogSend()
	{
		SQMLog.Log((SQMDataId)59, 1);
	}
}
