using System;
using Microsoft.Iris;
using Microsoft.Zune.Playlist;
using ZuneXml;

namespace ZuneUI;

public abstract class Attachment : NotifyPropertyChangedImpl
{
	private string _attachmentUI;

	private string _description;

	private string _title;

	private string _subtitle;

	private string _imageUri;

	private Guid _imageId;

	private Guid _id;

	private bool _wishlist;

	private bool _allowEmailRecipients;

	public string AttachmentUI
	{
		get
		{
			return _attachmentUI;
		}
		set
		{
			if (_attachmentUI != value)
			{
				_attachmentUI = value;
				FirePropertyChanged("AttachmentUI");
			}
		}
	}

	public string Description
	{
		get
		{
			return _description;
		}
		set
		{
			if (_description != null)
			{
				_description = value;
				FirePropertyChanged("Description");
			}
		}
	}

	public string ImageUri
	{
		get
		{
			return _imageUri;
		}
		set
		{
			if (_imageUri != value)
			{
				_imageUri = value;
				FirePropertyChanged("ImageUri");
			}
		}
	}

	public Guid ImageId
	{
		get
		{
			return _imageId;
		}
		set
		{
			if (_imageId != value)
			{
				_imageId = value;
				FirePropertyChanged("ImageId");
			}
		}
	}

	public string Title
	{
		get
		{
			return _title;
		}
		set
		{
			if (_title != value)
			{
				_title = value;
				FirePropertyChanged("Title");
			}
		}
	}

	public string Subtitle
	{
		get
		{
			return _subtitle;
		}
		set
		{
			if (_subtitle != value)
			{
				_subtitle = value;
				FirePropertyChanged("Subtitle");
			}
		}
	}

	public Guid Id => _id;

	public bool Wishlist
	{
		get
		{
			return _wishlist;
		}
		set
		{
			if (_wishlist != value)
			{
				_wishlist = value;
				FirePropertyChanged("Wishlist");
			}
		}
	}

	public bool AllowEmailRecipients
	{
		get
		{
			return _allowEmailRecipients;
		}
		set
		{
			if (_allowEmailRecipients != value)
			{
				_allowEmailRecipients = value;
				FirePropertyChanged("AllowEmailRecipients");
			}
		}
	}

	public abstract MediaType MediaType { get; }

	public abstract string RequestType { get; }

	public virtual string[] Properties => new string[6]
	{
		"type",
		RequestType,
		"mediaid",
		Id.ToString(),
		"wishlist",
		Wishlist.ToString()
	};

	public virtual bool IsReady => true;

	protected Attachment(Guid id, string title, string subtitle, string imageUri)
	{
		_attachmentUI = "res://ZuneShellResources!SocialComposer.uix#AttachmentUI";
		_id = id;
		_title = title;
		_subtitle = subtitle;
		_description = Shell.LoadString(StringId.IDS_COMPOSE_MESSAGE_DEFAULT_ATTACHMENT);
		_imageUri = imageUri;
		_allowEmailRecipients = true;
	}

	public virtual void LogSend()
	{
	}

	public static bool CanCreateAttachment(object obj)
	{
		//IL_018f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0194: Unknown result type (might be due to invalid IL or missing references)
		//IL_0196: Unknown result type (might be due to invalid IL or missing references)
		//IL_0199: Invalid comparison between Unknown and I4
		//IL_019b: Unknown result type (might be due to invalid IL or missing references)
		//IL_019e: Invalid comparison between Unknown and I4
		MediaType mediaType = MediaType.Undefined;
		Guid guid = Guid.Empty;
		int num = -1;
		bool flag = false;
		DataProviderObject val = (DataProviderObject)((obj is DataProviderObject) ? obj : null);
		if (val != null)
		{
			object property = val.GetProperty("ZuneMediaId");
			if (property != null)
			{
				guid = (Guid)property;
			}
			property = val.GetProperty("LibraryId");
			if (property != null)
			{
				num = (int)property;
			}
			if (val.GetProperty("Type") is string typeName)
			{
				mediaType = ZuneShell.MapStringToMediaType(typeName);
			}
		}
		else if (obj is PlaybackTrack)
		{
			PlaybackTrack playbackTrack = (PlaybackTrack)obj;
			guid = playbackTrack.ZuneMediaId;
			mediaType = playbackTrack.MediaType;
		}
		switch (mediaType)
		{
		case MediaType.Track:
		case MediaType.Album:
			flag = guid != Guid.Empty;
			break;
		case MediaType.Podcast:
			flag = guid != Guid.Empty;
			if (!flag && val != null)
			{
				string value = val.GetProperty("FeedUrl") as string;
				flag = !string.IsNullOrEmpty(value);
			}
			break;
		case MediaType.Video:
			if (val != null && guid != Guid.Empty)
			{
				object property3 = val.GetProperty("CategoryId");
				if (property3 != null)
				{
					VideoCategory videoCategory = (VideoCategory)property3;
					flag = videoCategory == VideoCategory.TV || videoCategory == VideoCategory.Movies;
				}
			}
			break;
		case MediaType.Playlist:
			if (val != null && (guid != Guid.Empty || num != -1))
			{
				object property4 = val.GetProperty("PlaylistType");
				if (property4 != null)
				{
					PlaylistType val2 = (PlaylistType)property4;
					flag = (int)val2 != 5 && (int)val2 != 6;
				}
			}
			break;
		case MediaType.PodcastEpisode:
			if (val != null)
			{
				object property2 = val.GetProperty("SeriesId");
				if (property2 != null)
				{
					int seriesId = (int)property2;
					flag = PodcastLibraryPage.GetZuneMediaId(seriesId) != Guid.Empty || !string.IsNullOrEmpty(val.GetProperty("SeriesFeedUrl") as string);
				}
			}
			break;
		}
		return flag;
	}

	public static Attachment CreateAttachment(object obj)
	{
		DataProviderObject val = (DataProviderObject)((obj is DataProviderObject) ? obj : null);
		MediaType mediaType = MediaType.Undefined;
		Guid zuneMediaId = Guid.Empty;
		VideoCategory videoCategory = VideoCategory.Other;
		string title = null;
		string url = null;
		int num = -1;
		if (val != null)
		{
			object property = val.GetProperty("ZuneMediaId");
			if (property != null)
			{
				zuneMediaId = (Guid)property;
			}
			if (val.GetProperty("Type") is string typeName)
			{
				mediaType = ZuneShell.MapStringToMediaType(typeName);
			}
			property = val.GetProperty("LibraryId");
			if (property != null)
			{
				num = (int)property;
			}
			property = val.GetProperty("CategoryId");
			if (property != null)
			{
				videoCategory = (VideoCategory)property;
			}
			title = val.GetProperty("Title") as string;
			switch (mediaType)
			{
			case MediaType.PodcastEpisode:
			{
				object property2 = val.GetProperty("SeriesId");
				if (property2 != null)
				{
					num = (int)property2;
				}
				zuneMediaId = PodcastLibraryPage.GetZuneMediaId(num);
				mediaType = MediaType.Podcast;
				title = val.GetProperty("SeriesTitle") as string;
				url = val.GetProperty("SeriesFeedUrl") as string;
				break;
			}
			case MediaType.Podcast:
				url = val.GetProperty("FeedUrl") as string;
				break;
			}
		}
		return CreateAttachment(zuneMediaId, mediaType, num, title, url, videoCategory, MovieType.Other);
	}

	public static Attachment CreateAttachment(Guid zuneMediaId, MediaType mediaType)
	{
		return CreateAttachment(zuneMediaId, mediaType, -1, null);
	}

	public static Attachment CreateAttachment(Guid zuneMediaId, MediaType mediaType, int libraryId, string title)
	{
		return CreateAttachment(zuneMediaId, mediaType, libraryId, title, null, VideoCategory.Other, MovieType.Other);
	}

	public static Attachment CreateAttachment(Guid zuneMediaId, MediaType mediaType, string title, VideoCategory videoCategory, MovieType movieType)
	{
		return CreateAttachment(zuneMediaId, mediaType, -1, title, null, videoCategory, movieType);
	}

	private static Attachment CreateAttachment(Guid zuneMediaId, MediaType mediaType, int libraryId, string title, string url, VideoCategory videoCategory, MovieType movieType)
	{
		Attachment result = null;
		if (libraryId != -1 && mediaType == MediaType.Playlist)
		{
			result = new CollectionPlaylistAttachment(null, title, null, libraryId, null);
		}
		else
		{
			switch (mediaType)
			{
			case MediaType.Album:
				if (zuneMediaId != Guid.Empty)
				{
					result = new AlbumAttachment(zuneMediaId, title, null, null, Guid.Empty);
				}
				break;
			case MediaType.Playlist:
				if (zuneMediaId != Guid.Empty)
				{
					result = new PlaylistAttachment(zuneMediaId, title, null);
				}
				break;
			case MediaType.Podcast:
				if (zuneMediaId != Guid.Empty || !string.IsNullOrEmpty(url))
				{
					result = new PodcastAttachment(zuneMediaId, title, null, url, null);
				}
				break;
			case MediaType.Track:
				if (zuneMediaId != Guid.Empty)
				{
					result = new TrackAttachment(zuneMediaId, title, null, null);
				}
				break;
			case MediaType.Video:
				if (zuneMediaId != Guid.Empty)
				{
					switch (videoCategory)
					{
					case VideoCategory.TV:
						result = new EpisodeAttachment(zuneMediaId, title, null, null);
						break;
					case VideoCategory.Movies:
						result = ((movieType != MovieType.Trailer) ? ((VideoAttachment)new MovieAttachment(zuneMediaId, title, null)) : ((VideoAttachment)new TrailerAttachment(zuneMediaId, title, null)));
						break;
					}
				}
				break;
			}
		}
		return result;
	}

	public static Attachment CreateAttachmentFromMessage(object message, object messageDetails)
	{
		return CreateAttachmentFromMessage(message as MessageRoot, messageDetails as MessageDetails);
	}

	private static Attachment CreateAttachmentFromMessage(MessageRoot message, MessageDetails messageDetails)
	{
		Attachment result = null;
		if (message != null)
		{
			string type = message.Type;
			if (message.MediaId != Guid.Empty)
			{
				switch (type)
				{
				case "album":
					result = new AlbumAttachment(message.MediaId, null, null, null, Guid.Empty);
					break;
				case "playlist":
					result = new PlaylistAttachment(message.MediaId, null, null);
					break;
				case "podcast":
					result = new PodcastAttachment(message.MediaId, null, null, null, null);
					break;
				case "song":
					result = new TrackAttachment(message.MediaId, null, null, null);
					break;
				case "video":
					result = new EpisodeAttachment(message.MediaId, null, null, null);
					break;
				case "movie":
					result = new MovieAttachment(message.MediaId, null, null);
					break;
				case "movietrailer":
					result = new TrailerAttachment(message.MediaId, null, null);
					break;
				}
			}
			else if (messageDetails != null)
			{
				if (messageDetails.PodcastMediaId != Guid.Empty)
				{
					if (type == "podcast")
					{
						result = new PodcastAttachment(messageDetails.PodcastMediaId, null, null, null, null);
					}
				}
				else if (!string.IsNullOrEmpty(messageDetails.ZuneTag) && type == "card")
				{
					result = new ProfileAttachment(messageDetails.ZuneTag, null, null);
				}
			}
		}
		return result;
	}

	public static Attachment CreateAttachment(Guid id, MediaType mediaType, bool wishlist)
	{
		Attachment attachment = CreateAttachment(id, mediaType);
		if (attachment != null)
		{
			attachment.Wishlist = wishlist;
		}
		return attachment;
	}

	public bool ContainsImage()
	{
		if (string.IsNullOrEmpty(_imageUri))
		{
			return _imageId != Guid.Empty;
		}
		return true;
	}

	public virtual bool IsValid(out string errorMessage)
	{
		errorMessage = null;
		return true;
	}
}
