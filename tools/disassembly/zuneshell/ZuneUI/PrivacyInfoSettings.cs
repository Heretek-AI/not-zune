using System;

namespace ZuneUI;

[Flags]
public enum PrivacyInfoSettings
{
	None = 0,
	AllowExplicitContent = 1,
	AllowFriends = 2,
	AllowMicrosoftCommunications = 4,
	AllowPartnerCommunications = 8,
	AllowPurchase = 0x10,
	Communications = 0x20,
	FriendsSharing = 0x40,
	MusicSharing = 0x80,
	ProfileCustomization = 0x100,
	AllSocial = 0x200,
	UsageCollection = 0x400,
	CreateNewAccount = 0x404,
	CreateNewAccountWithSocial = 0x604,
	CreateChildAccount = 0x411,
	CreateChildAccountWithSocial = 0x5F3,
	SocialSettings = 0x5E0,
	NewsletterSettings = 4,
	NoNewsletterSettings = 0x7F3,
	All = 0x7FF
}
