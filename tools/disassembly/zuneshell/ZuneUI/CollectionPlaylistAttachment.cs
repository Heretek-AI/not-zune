using System;
using System.Collections;
using Microsoft.Zune.Configuration;
using Microsoft.Zune.Messaging;
using Microsoft.Zune.Util;

namespace ZuneUI;

public class CollectionPlaylistAttachment : PropertySetAttachment
{
	public const string RequestTypeString = "playlist";

	private PlaylistMessageData m_playlistData;

	private IList m_tracks;

	private int m_mediaId;

	public override MediaType MediaType => MediaType.Playlist;

	public override string RequestType => "playlist";

	public override string[] Properties => new string[0];

	public override IPropertySetMessageData PropertySet
	{
		get
		{
			//IL_001b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0025: Expected O, but got Unknown
			if (m_playlistData == null)
			{
				m_playlistData = new PlaylistMessageData(base.Title, Author, Tracks);
			}
			return (IPropertySetMessageData)(object)m_playlistData;
		}
	}

	public string Author
	{
		get
		{
			return base.Subtitle;
		}
		set
		{
			base.Subtitle = value;
		}
	}

	public int MediaId => m_mediaId;

	public IList Tracks
	{
		get
		{
			return m_tracks;
		}
		set
		{
			if (m_tracks != value)
			{
				m_tracks = value;
				FirePropertyChanged("Tracks");
				FirePropertyChanged("IsReady");
			}
		}
	}

	public override bool IsReady => Tracks != null;

	public CollectionPlaylistAttachment(string author, string title, string imageUri, int mediaId, IList tracks)
		: base(Guid.Empty, title, author, imageUri)
	{
		base.AttachmentUI = "res://ZuneShellResources!SocialComposer.uix#CollectionPlaylistAttachmentUI";
		base.Description = Shell.LoadString(StringId.IDS_COMPOSE_MESSAGE_PLAYLIST_ATTACHMENT);
		base.AllowEmailRecipients = false;
		m_tracks = tracks;
		m_mediaId = mediaId;
	}

	protected override void FirePropertyChanged(string propertyName)
	{
		if (propertyName == "Subtitle")
		{
			FirePropertyChanged("Author");
		}
		if (propertyName == "Title" || propertyName == "Subtitle")
		{
			m_playlistData = null;
			FirePropertyChanged("PropertySet");
		}
		base.FirePropertyChanged(propertyName);
	}

	public override void LogSend()
	{
		SQMLog.Log((SQMDataId)54, 1);
	}

	public override bool IsValid(out string errorMessage)
	{
		bool result = true;
		errorMessage = null;
		int num = ClientConfiguration.Messaging.MaxSubMessagesPerMessage * ClientConfiguration.Messaging.MaxTracksPerMessage;
		if (m_tracks != null && m_tracks.Count > num)
		{
			result = false;
			errorMessage = string.Format(Shell.LoadString(StringId.IDS_COMPOSE_MESSAGE_ERROR_TOO_MANY_TRACKS), num);
		}
		return result;
	}
}
