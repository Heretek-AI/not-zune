using System;
using System.Collections;
using Microsoft.Zune.Service;

namespace ZuneUI;

public static class CreditCardHelper
{
	private static string[] s_CardTypeStrs;

	private static string[] s_SupportedCardTypeStrs;

	private static string[] s_JpnSupportedCardTypeStrs;

	public static IList CardTypes
	{
		get
		{
			if (s_SupportedCardTypeStrs == null || s_JpnSupportedCardTypeStrs == null)
			{
				string[] creditCardMappings = CreditCardMappings;
				s_SupportedCardTypeStrs = new string[4]
				{
					creditCardMappings[2],
					creditCardMappings[3],
					creditCardMappings[1],
					creditCardMappings[0]
				};
				s_JpnSupportedCardTypeStrs = new string[4]
				{
					creditCardMappings[2],
					creditCardMappings[1],
					creditCardMappings[0],
					creditCardMappings[4]
				};
				Array.Sort(s_SupportedCardTypeStrs);
				Array.Sort(s_JpnSupportedCardTypeStrs);
			}
			if (SignIn.Instance.SignedIn && string.Compare(SignIn.Instance.CountryCode, "JP", StringComparison.OrdinalIgnoreCase) == 0)
			{
				return s_JpnSupportedCardTypeStrs;
			}
			return s_SupportedCardTypeStrs;
		}
	}

	private static string[] CreditCardMappings
	{
		get
		{
			if (s_CardTypeStrs == null)
			{
				s_CardTypeStrs = new string[7]
				{
					Shell.LoadString(StringId.IDS_BILLING_CC_VISA),
					Shell.LoadString(StringId.IDS_BILLING_CC_MASTERCARD),
					Shell.LoadString(StringId.IDS_BILLING_CC_AMEX),
					Shell.LoadString(StringId.IDS_BILLING_CC_DISCOVER),
					Shell.LoadString(StringId.IDS_BILLING_CC_JCB),
					Shell.LoadString(StringId.IDS_BILLING_CC_DINERS),
					Shell.LoadString(StringId.IDS_BILLING_CC_KLCC)
				};
			}
			return s_CardTypeStrs;
		}
	}

	public static string CardTypeToString(CreditCardType cardType)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Expected I4, but got Unknown
		return CardTypeToString((int)cardType);
	}

	public static string CardTypeToString(int cardType)
	{
		string[] creditCardMappings = CreditCardMappings;
		if (cardType >= 0 && cardType < creditCardMappings.Length)
		{
			return creditCardMappings[cardType];
		}
		return null;
	}

	public static CreditCardType CardTypeFromString(string cardTypeStr)
	{
		string[] creditCardMappings = CreditCardMappings;
		for (int i = 0; i < creditCardMappings.Length; i++)
		{
			if (creditCardMappings[i] == cardTypeStr)
			{
				return (CreditCardType)i;
			}
		}
		return (CreditCardType)(-1);
	}
}
