using System;
using Microsoft.Zune.Util;

namespace ZuneUI;

public class PodcastAttachment : Attachment
{
	public const string RequestTypeString = "podcast";

	private string _url;

	public string Url
	{
		get
		{
			return _url;
		}
		set
		{
			_url = value;
			FirePropertyChanged("Url");
		}
	}

	public override MediaType MediaType => MediaType.Podcast;

	public override string RequestType => "podcast";

	public override string[] Properties => new string[8]
	{
		"type",
		RequestType,
		"podcasturl",
		_url ?? string.Empty,
		"podcastmediaid",
		base.Id.ToString(),
		"podcastname",
		base.Title
	};

	public PodcastAttachment(Guid id, string title, string author, string url, string imageUri)
		: base(id, title, author, imageUri)
	{
		base.AttachmentUI = "res://ZuneShellResources!SocialComposer.uix#PodcastAttachmentUI";
		base.Description = Shell.LoadString(StringId.IDS_COMPOSE_MESSAGE_PODCAST_ATTACHMENT);
		_url = url;
	}

	public override void LogSend()
	{
		SQMLog.Log((SQMDataId)60, 1);
	}
}
