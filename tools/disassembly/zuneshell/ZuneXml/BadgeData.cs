using System;
using Microsoft.Iris;

namespace ZuneXml;

internal class BadgeData : XmlDataProviderObject
{
	internal int TypeId => (int)GetBadgeType(this);

	internal string Title => (string)base.GetProperty("Title");

	internal string Image => (string)base.GetProperty("Image");

	internal string Type => (string)base.GetProperty("Type");

	internal Guid MediaId => (Guid)base.GetProperty("MediaId");

	internal string MediaType => (string)base.GetProperty("MediaType");

	internal string Description => (string)base.GetProperty("Description");

	internal static BadgeType GetBadgeType(BadgeData badgeData)
	{
		BadgeType result = BadgeType.Invalid;
		switch (badgeData.Type)
		{
		case "ActiveAlbumListener_Bronze":
			result = BadgeType.BronzeAlbum;
			break;
		case "ActiveAlbumListener_Silver":
			result = BadgeType.SilverAlbum;
			break;
		case "ActiveAlbumListener_Gold":
			result = BadgeType.GoldAlbum;
			break;
		case "ActiveArtistListener_Bronze":
			result = BadgeType.BronzeArtist;
			break;
		case "ActiveArtistListener_Silver":
			result = BadgeType.SilverArtist;
			break;
		case "ActiveArtistListener_Gold":
			result = BadgeType.GoldArtist;
			break;
		case "ActiveForumsBadge_Bronze":
			result = BadgeType.BronzeForums;
			break;
		case "ActiveForumsBadge_Silver":
			result = BadgeType.SilverForums;
			break;
		case "ActiveForumsBadge_Gold":
			result = BadgeType.GoldForums;
			break;
		case "ActiveReviewBadge_Bronze":
			result = BadgeType.BronzeReview;
			break;
		case "ActiveReviewBadge_Silver":
			result = BadgeType.SilverReview;
			break;
		case "ActiveReviewBadge_Gold":
			result = BadgeType.GoldReview;
			break;
		}
		return result;
	}

	internal static XmlDataProviderObject ConstructBadgeDataObject(DataProviderQuery owner, object objectTypeCookie)
	{
		return new BadgeData(owner, objectTypeCookie);
	}

	internal BadgeData(DataProviderQuery owner, object resultTypeCookie)
		: base(owner, resultTypeCookie)
	{
	}

	public override object GetProperty(string propertyName)
	{
		string text;
		if ((text = propertyName) != null && text == "TypeId")
		{
			return TypeId;
		}
		return base.GetProperty(propertyName);
	}
}
