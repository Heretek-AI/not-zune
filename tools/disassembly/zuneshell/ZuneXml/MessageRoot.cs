using System;
using Microsoft.Iris;
using Microsoft.Zune.Service;
using ZuneUI;

namespace ZuneXml;

internal class MessageRoot : XmlDataProviderObject
{
	private MessageTypeEnum _messageType = MessageTypeEnum.Invalid;

	private EContentType _contentType = (EContentType)(-1);

	internal string UserTileUrl => ComposerHelper.GetUserTileUri(From);

	internal MessageTypeEnum MessageType
	{
		get
		{
			return _messageType;
		}
		private set
		{
			//IL_0012: Unknown result type (might be due to invalid IL or missing references)
			if (_messageType != value)
			{
				_messageType = value;
				ContentType = ToContentType(value);
				((DataProviderObject)this).FirePropertyChanged("MessageType");
				((DataProviderObject)this).FirePropertyChanged("IsSupported");
			}
		}
	}

	internal EContentType ContentType
	{
		get
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			return _contentType;
		}
		private set
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			//IL_0006: Unknown result type (might be due to invalid IL or missing references)
			//IL_000a: Unknown result type (might be due to invalid IL or missing references)
			//IL_000b: Unknown result type (might be due to invalid IL or missing references)
			if (_contentType != value)
			{
				_contentType = value;
				((DataProviderObject)this).FirePropertyChanged("ContentType");
			}
		}
	}

	internal bool IsSupported
	{
		get
		{
			if (Wishlist)
			{
				if (MessageType != MessageTypeEnum.Song)
				{
					return MessageType == MessageTypeEnum.Album;
				}
				return true;
			}
			return MessageType != MessageTypeEnum.Invalid;
		}
	}

	internal string MessagingId => (string)base.GetProperty("MessagingId");

	internal string From => (string)base.GetProperty("From");

	internal string Type => (string)base.GetProperty("Type");

	internal string Subject => (string)base.GetProperty("Subject");

	internal DateTime Received => (DateTime)base.GetProperty("Received");

	internal string DetailsLink => (string)base.GetProperty("DetailsLink");

	internal string Status
	{
		get
		{
			return (string)base.GetProperty("Status");
		}
		set
		{
			((DataProviderObject)this).SetProperty("Status", (object)value);
		}
	}

	internal bool Wishlist => (bool)base.GetProperty("Wishlist");

	internal Guid MediaId => (Guid)base.GetProperty("MediaId");

	private static EContentType ToContentType(MessageTypeEnum messageType)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_0059: Unknown result type (might be due to invalid IL or missing references)
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		//IL_005a: Unknown result type (might be due to invalid IL or missing references)
		EContentType val = (EContentType)(-1);
		switch (messageType)
		{
		case MessageTypeEnum.MusicVideo:
		case MessageTypeEnum.Video:
		case MessageTypeEnum.Movie:
		case MessageTypeEnum.MovieTrailer:
			return (EContentType)3;
		case MessageTypeEnum.Playlist:
			return (EContentType)8;
		case MessageTypeEnum.Podcast:
			return (EContentType)6;
		case MessageTypeEnum.Song:
			return (EContentType)0;
		case MessageTypeEnum.Album:
			return (EContentType)1;
		default:
			return (EContentType)(-1);
		}
	}

	public override void SetProperty(string propertyName, object value)
	{
		base.SetProperty(propertyName, value);
		string text;
		if ((text = propertyName) != null && text == "Type")
		{
			MessageType = SchemaHelper.ToMessageType(value as string);
		}
		base.SetProperty(propertyName, value);
	}

	internal static XmlDataProviderObject ConstructMessageRootObject(DataProviderQuery owner, object objectTypeCookie)
	{
		return new MessageRoot(owner, objectTypeCookie);
	}

	internal MessageRoot(DataProviderQuery owner, object resultTypeCookie)
		: base(owner, resultTypeCookie)
	{
	}//IL_0009: Unknown result type (might be due to invalid IL or missing references)


	public override object GetProperty(string propertyName)
	{
		return propertyName switch
		{
			"UserTileUrl" => UserTileUrl, 
			"IsSupported" => IsSupported, 
			_ => base.GetProperty(propertyName), 
		};
	}
}
