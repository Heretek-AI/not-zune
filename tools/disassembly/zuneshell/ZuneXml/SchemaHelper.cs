namespace ZuneXml;

internal class SchemaHelper
{
	internal static MediaRightsEnum ToMediaRights(string value)
	{
		MediaRightsEnum result = MediaRightsEnum.None;
		if (!string.IsNullOrEmpty(value))
		{
			switch (value)
			{
			case "Preview":
				result = MediaRightsEnum.Preview;
				break;
			case "PreviewStream":
				result = MediaRightsEnum.PreviewStream;
				break;
			case "Stream":
				result = MediaRightsEnum.SubscriptionStream;
				break;
			case "Subscription":
				result = MediaRightsEnum.SubscriptionDownload;
				break;
			case "Purchase":
				result = MediaRightsEnum.Purchase;
				break;
			case "PurchaseStream":
				result = MediaRightsEnum.PurchaseStream;
				break;
			case "SeasonPurchase":
				result = MediaRightsEnum.SeasonPurchase;
				break;
			case "SeasonPurchaseStream":
				result = MediaRightsEnum.SeasonPurchaseStream;
				break;
			case "AlbumPurchase":
				result = MediaRightsEnum.AlbumPurchase;
				break;
			case "SubscriptionFree":
				result = MediaRightsEnum.SubscriptionFreePurchase;
				break;
			case "TransferToPortableDevice":
				result = MediaRightsEnum.TransferToPortableDevice;
				break;
			case "Rent":
				result = MediaRightsEnum.Rent;
				break;
			case "RentStream":
				result = MediaRightsEnum.RentStream;
				break;
			case "Trial":
				result = MediaRightsEnum.PurchaseTrial;
				break;
			case "Download":
				result = MediaRightsEnum.Download;
				break;
			case "Beta":
				result = MediaRightsEnum.PurchaseBeta;
				break;
			}
		}
		return result;
	}

	internal static AudioEncodingEnum ToAudioEncoding(string value)
	{
		AudioEncodingEnum result = AudioEncodingEnum.None;
		if (!string.IsNullOrEmpty(value))
		{
			switch (value)
			{
			case "MP3":
				result = AudioEncodingEnum.MP3;
				break;
			case "WMA":
				result = AudioEncodingEnum.WMA;
				break;
			}
		}
		return result;
	}

	internal static VideoResolutionEnum ToVideoResolution(string value)
	{
		VideoResolutionEnum result = VideoResolutionEnum.None;
		if (!string.IsNullOrEmpty(value))
		{
			switch (value)
			{
			case "1080p":
				result = VideoResolutionEnum.VR_1080P;
				break;
			case "720p":
				result = VideoResolutionEnum.VR_720P;
				break;
			case "480p":
				result = VideoResolutionEnum.VR_480P;
				break;
			case "240p":
				result = VideoResolutionEnum.VR_240P;
				break;
			}
		}
		return result;
	}

	internal static VideoDefinitionEnum ToVideoDefinition(string value)
	{
		VideoDefinitionEnum result = VideoDefinitionEnum.None;
		if (!string.IsNullOrEmpty(value))
		{
			switch (value)
			{
			case "HD":
				result = VideoDefinitionEnum.HD;
				break;
			case "SD":
				result = VideoDefinitionEnum.SD;
				break;
			case "XD":
				result = VideoDefinitionEnum.XD;
				break;
			}
		}
		return result;
	}

	internal static ClientTypeEnum ToClientType(string value)
	{
		ClientTypeEnum result = ClientTypeEnum.None;
		if (!string.IsNullOrEmpty(value))
		{
			switch (value)
			{
			case "Zune 3.0":
				result = ClientTypeEnum.Zune;
				break;
			case "WinMobile 7.0":
			case "WinMobile 7.1":
				result = ClientTypeEnum.WindowsPhone;
				break;
			}
		}
		return result;
	}

	internal static DisclosureEnum ToDisclosureEnum(string value)
	{
		DisclosureEnum result = DisclosureEnum.None;
		if (!string.IsNullOrEmpty(value))
		{
			switch (value)
			{
			case "Disclose":
				result = DisclosureEnum.Disclose;
				break;
			case "DiscloseANDPrompt":
				result = DisclosureEnum.DiscloseAndPrompt;
				break;
			case "Prompt":
				result = DisclosureEnum.Prompt;
				break;
			}
		}
		return result;
	}

	internal static PriceTypeEnum ToPriceType(string value)
	{
		PriceTypeEnum result = PriceTypeEnum.None;
		if (!string.IsNullOrEmpty(value))
		{
			string text;
			result = (((text = value.ToUpper()) != null && text == "MPT") ? PriceTypeEnum.Points : PriceTypeEnum.Currency);
		}
		return result;
	}

	internal static MessageTypeEnum ToMessageType(string value)
	{
		MessageTypeEnum result = MessageTypeEnum.Invalid;
		if (!string.IsNullOrEmpty(value))
		{
			result = value switch
			{
				"album" => MessageTypeEnum.Album, 
				"card" => MessageTypeEnum.Card, 
				"forums" => MessageTypeEnum.Forums, 
				"friendrequest" => MessageTypeEnum.FriendRequest, 
				"message" => MessageTypeEnum.Message, 
				"musicvideo" => MessageTypeEnum.MusicVideo, 
				"notification" => MessageTypeEnum.Notification, 
				"photos" => MessageTypeEnum.Photos, 
				"playlist" => MessageTypeEnum.Playlist, 
				"podcast" => MessageTypeEnum.Podcast, 
				"song" => MessageTypeEnum.Song, 
				"video" => MessageTypeEnum.Video, 
				"movie" => MessageTypeEnum.Movie, 
				"movietrailer" => MessageTypeEnum.MovieTrailer, 
				_ => MessageTypeEnum.Invalid, 
			};
		}
		return result;
	}

	internal static MediaTypeEnum ToMediaTypeEnum(string value)
	{
		MediaTypeEnum result = MediaTypeEnum.None;
		string text;
		if (!string.IsNullOrEmpty(value) && (text = value) != null && text == "TVSeason")
		{
			result = MediaTypeEnum.TVSeason;
		}
		return result;
	}
}
