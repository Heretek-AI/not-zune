using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Iris;
using Microsoft.Zune.Util;

namespace ZuneXml;

internal class MediaRights : XmlDataProviderObject
{
	internal IList Languages
	{
		get
		{
			List<string> list = new List<string>();
			foreach (Right right in Rights)
			{
				if (IsAssociatedFeatureEnabled(right.RightEnum) && (right.VideoDefinitionEnum == VideoDefinitionEnum.HD || right.VideoDefinitionEnum == VideoDefinitionEnum.SD) && right.PriceTypeEnum == PriceTypeEnum.Points)
				{
					string language = right.Language;
					if (!string.IsNullOrEmpty(language) && !list.Contains(language))
					{
						list.Add(language);
					}
				}
			}
			return list;
		}
	}

	internal IList Rights => (IList)base.GetProperty("Rights");

	internal Right GetOfferRight(ClientTypeEnum clientType, PriceTypeEnum priceType)
	{
		if (FeatureEnablement.IsFeatureEnabled((Features)2) && Rights != null)
		{
			foreach (Right right in Rights)
			{
				if (clientType == right.ClientTypeEnum && priceType == right.PriceTypeEnum && Guid.Empty != right.OfferId)
				{
					return right;
				}
			}
		}
		return null;
	}

	internal Right GetOfferRight(MediaRightsEnum right, ClientTypeEnum clientType, PriceTypeEnum priceType)
	{
		if (FeatureEnablement.IsFeatureEnabled((Features)2) && Rights != null)
		{
			foreach (Right right2 in Rights)
			{
				if (right == right2.RightEnum && clientType == right2.ClientTypeEnum && priceType == right2.PriceTypeEnum && Guid.Empty != right2.OfferId)
				{
					return right2;
				}
			}
		}
		return null;
	}

	internal Right GetOfferRight(MediaRightsEnum right, AudioEncodingEnum encoding, PriceTypeEnum priceType)
	{
		if (FeatureEnablement.IsFeatureEnabled((Features)28) && Rights != null)
		{
			foreach (Right right2 in Rights)
			{
				if (right == right2.RightEnum && priceType == right2.PriceTypeEnum && encoding == right2.AudioEncodingEnum && right2.OfferId != Guid.Empty)
				{
					return right2;
				}
			}
		}
		return null;
	}

	internal bool HasOfferRights(MediaRightsEnum right, ClientTypeEnum clientType, PriceTypeEnum priceType)
	{
		Right offerRight = GetOfferRight(right, clientType, priceType);
		return offerRight != null;
	}

	internal bool HasOfferRights(MediaRightsEnum right, AudioEncodingEnum encoding, PriceTypeEnum priceType, out Right offer)
	{
		offer = GetOfferRight(right, encoding, priceType);
		return offer != null;
	}

	internal bool HasRights(ClientTypeEnum clientType)
	{
		if (FeatureEnablement.IsFeatureEnabled((Features)2) && Rights != null)
		{
			foreach (Right right in Rights)
			{
				if (clientType == right.ClientTypeEnum)
				{
					return true;
				}
			}
		}
		return false;
	}

	internal bool HasRights(MediaRightsEnum right, ClientTypeEnum clientType)
	{
		if (FeatureEnablement.IsFeatureEnabled((Features)2) && Rights != null)
		{
			foreach (Right right2 in Rights)
			{
				if (right == right2.RightEnum && clientType == right2.ClientTypeEnum)
				{
					return true;
				}
			}
		}
		return false;
	}

	internal bool HasRights(MediaRightsEnum right, AudioEncodingEnum encoding)
	{
		if (FeatureEnablement.IsFeatureEnabled((Features)28) && Rights != null)
		{
			foreach (Right right2 in Rights)
			{
				if (right == right2.RightEnum && encoding == right2.AudioEncodingEnum)
				{
					return true;
				}
			}
		}
		return false;
	}

	internal Right GetOfferRight(MediaRightsEnum right, VideoDefinitionEnum definition, PriceTypeEnum priceType)
	{
		if (FeatureEnablement.IsFeatureEnabled((Features)2) && IsAssociatedFeatureEnabled(right))
		{
			if (definition == VideoDefinitionEnum.None)
			{
				Right offerRight = GetOfferRight(right, VideoDefinitionEnum.HD, priceType);
				if (offerRight == null)
				{
					offerRight = GetOfferRight(right, VideoDefinitionEnum.SD, priceType);
				}
				if (offerRight == null)
				{
					offerRight = GetOfferRight(right, VideoDefinitionEnum.XD, priceType);
				}
				if (offerRight != null)
				{
					return offerRight;
				}
			}
			if (Rights != null)
			{
				foreach (Right right2 in Rights)
				{
					if (right == right2.RightEnum && priceType == right2.PriceTypeEnum && (definition == right2.VideoDefinitionEnum || definition == VideoDefinitionEnum.None) && (right2.VideoResolutionEnum != VideoResolutionEnum.VR_1080P || right2.RightEnum != MediaRightsEnum.Purchase))
					{
						return right2;
					}
				}
			}
		}
		return null;
	}

	internal bool HasRights(MediaRightsEnum right, VideoDefinitionEnum definition, PriceTypeEnum priceType)
	{
		Right offerRight = GetOfferRight(right, definition, priceType);
		if (right == MediaRightsEnum.PurchaseStream)
		{
			if (offerRight != null)
			{
				return HasRights(MediaRightsEnum.Purchase, definition, VideoDefinitionEnum.XD, priceType);
			}
			return false;
		}
		return offerRight != null;
	}

	internal Right GetOfferRight(MediaRightsEnum right, VideoDefinitionEnum definition1, VideoDefinitionEnum definition2, PriceTypeEnum priceType)
	{
		if (FeatureEnablement.IsFeatureEnabled((Features)2) && IsAssociatedFeatureEnabled(right) && Rights != null)
		{
			foreach (Right right4 in Rights)
			{
				if (right != right4.RightEnum || definition1 != right4.VideoDefinitionEnum || priceType != right4.PriceTypeEnum)
				{
					continue;
				}
				foreach (Right right5 in Rights)
				{
					if (right4 != right5 && right == right5.RightEnum && definition2 == right5.VideoDefinitionEnum && priceType == right5.PriceTypeEnum && right4.OfferId == right5.OfferId)
					{
						return right5;
					}
				}
			}
		}
		return null;
	}

	private bool IsAssociatedFeatureEnabled(MediaRightsEnum right)
	{
		bool result = true;
		if ((right == MediaRightsEnum.RentStream && !FeatureEnablement.IsFeatureEnabled((Features)22)) || (right == MediaRightsEnum.PreviewStream && !FeatureEnablement.IsFeatureEnabled((Features)24)) || (right == MediaRightsEnum.PurchaseStream && !FeatureEnablement.IsFeatureEnabled((Features)23)))
		{
			result = false;
		}
		return result;
	}

	internal bool HasRights(MediaRightsEnum right, VideoDefinitionEnum definition1, VideoDefinitionEnum definition2, PriceTypeEnum priceType)
	{
		Right offerRight = GetOfferRight(right, definition1, definition2, priceType);
		return offerRight != null;
	}

	internal bool HasAnyRights()
	{
		if (FeatureEnablement.IsFeatureEnabled((Features)2) && Rights != null)
		{
			foreach (Right right in Rights)
			{
				if (IsAssociatedFeatureEnabled(right.RightEnum))
				{
					return true;
				}
			}
		}
		return false;
	}

	internal string GetCompId()
	{
		string result = null;
		if (Rights != null)
		{
			foreach (Right right in Rights)
			{
				string providerCode = right.ProviderCode;
				if (!string.IsNullOrEmpty(providerCode))
				{
					int num = providerCode.IndexOf(':');
					result = ((num <= 0) ? providerCode : providerCode.Substring(0, num));
					break;
				}
			}
		}
		return result;
	}

	internal static XmlDataProviderObject ConstructMediaRightsObject(DataProviderQuery owner, object objectTypeCookie)
	{
		return new MediaRights(owner, objectTypeCookie);
	}

	internal MediaRights(DataProviderQuery owner, object resultTypeCookie)
		: base(owner, resultTypeCookie)
	{
	}

	public override object GetProperty(string propertyName)
	{
		string text;
		if ((text = propertyName) != null && text == "Languages")
		{
			return Languages;
		}
		return base.GetProperty(propertyName);
	}
}
