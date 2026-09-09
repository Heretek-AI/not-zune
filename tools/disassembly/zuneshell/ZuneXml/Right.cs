using System;
using Microsoft.Iris;
using ZuneUI;

namespace ZuneXml;

internal class Right : XmlDataProviderObject
{
	private MediaRightsEnum _rightEnum;

	private AudioEncodingEnum _audioEncodingEnum;

	private VideoDefinitionEnum _videoDefinitionEnum;

	private VideoResolutionEnum _videoResolutionEnum;

	private ClientTypeEnum _clientTypeEnum;

	private PriceTypeEnum _priceTypeEnum;

	public bool HasPoints => PriceTypeEnum == PriceTypeEnum.Points;

	public bool HasCurrency => PriceTypeEnum == PriceTypeEnum.Currency;

	public int PointsPrice
	{
		get
		{
			if (HasPoints)
			{
				return (int)Price;
			}
			return 0;
		}
	}

	public double CurrencyPrice
	{
		get
		{
			if (HasCurrency)
			{
				return Price;
			}
			return 0.0;
		}
	}

	public bool IsFree
	{
		get
		{
			if (!HasPoints || PointsPrice > 0)
			{
				if (HasCurrency)
				{
					return CurrencyPrice <= 0.0;
				}
				return false;
			}
			return true;
		}
	}

	public string Language
	{
		get
		{
			string result = null;
			if (!string.IsNullOrEmpty(AudioLocale))
			{
				string displayLanguageName = LanguageHelper.GetDisplayLanguageName(AudioLocale);
				if (!string.IsNullOrEmpty(displayLanguageName))
				{
					result = displayLanguageName;
					if (!string.IsNullOrEmpty(SubtitleLocale))
					{
						string displayLanguageName2 = LanguageHelper.GetDisplayLanguageName(SubtitleLocale);
						if (!string.IsNullOrEmpty(displayLanguageName2))
						{
							string format = Shell.LoadString(StringId.IDS_VIDEO_SUBTITLE);
							result = string.Format(format, displayLanguageName, displayLanguageName2);
						}
					}
				}
			}
			return result;
		}
	}

	internal MediaRightsEnum RightEnum
	{
		get
		{
			return _rightEnum;
		}
		set
		{
			if (_rightEnum != value)
			{
				_rightEnum = value;
				((DataProviderObject)this).FirePropertyChanged("RightEnum");
			}
		}
	}

	internal AudioEncodingEnum AudioEncodingEnum
	{
		get
		{
			return _audioEncodingEnum;
		}
		set
		{
			if (_audioEncodingEnum != value)
			{
				_audioEncodingEnum = value;
				((DataProviderObject)this).FirePropertyChanged("AudioEncodingEnum");
			}
		}
	}

	internal VideoDefinitionEnum VideoDefinitionEnum
	{
		get
		{
			return _videoDefinitionEnum;
		}
		set
		{
			if (_videoDefinitionEnum != value)
			{
				_videoDefinitionEnum = value;
				((DataProviderObject)this).FirePropertyChanged("VideoDefinitionEnum");
			}
		}
	}

	internal VideoResolutionEnum VideoResolutionEnum
	{
		get
		{
			return _videoResolutionEnum;
		}
		set
		{
			if (_videoResolutionEnum != value)
			{
				_videoResolutionEnum = value;
				((DataProviderObject)this).FirePropertyChanged("VideoResolutionEnum");
			}
		}
	}

	internal ClientTypeEnum ClientTypeEnum
	{
		get
		{
			return _clientTypeEnum;
		}
		set
		{
			if (_clientTypeEnum != value)
			{
				_clientTypeEnum = value;
				((DataProviderObject)this).FirePropertyChanged("ClientTypeEnum");
			}
		}
	}

	internal PriceTypeEnum PriceTypeEnum
	{
		get
		{
			return _priceTypeEnum;
		}
		set
		{
			if (_priceTypeEnum != value)
			{
				_priceTypeEnum = value;
				((DataProviderObject)this).FirePropertyChanged("PriceTypeEnum");
				((DataProviderObject)this).FirePropertyChanged("HasPoints");
				((DataProviderObject)this).FirePropertyChanged("HasCurrency");
			}
		}
	}

	internal string LicenseType => (string)base.GetProperty("LicenseType");

	internal string LicenseRight => (string)base.GetProperty("LicenseRight");

	internal string ProviderName => (string)base.GetProperty("ProviderName");

	internal string ProviderCode => (string)base.GetProperty("ProviderCode");

	internal Guid OfferId => (Guid)base.GetProperty("OfferId");

	internal double Price => (double)base.GetProperty("Price");

	internal double OriginalPrice => (double)base.GetProperty("OriginalPrice");

	internal string CurrencyCode => (string)base.GetProperty("CurrencyCode");

	internal string DisplayPrice => (string)base.GetProperty("DisplayPrice");

	internal string AudioEncoding => (string)base.GetProperty("AudioEncoding");

	internal string AudioLocale => (string)base.GetProperty("AudioLocale");

	internal string SubtitleLocale => (string)base.GetProperty("SubtitleLocale");

	internal string VideoEncoding => (string)base.GetProperty("VideoEncoding");

	internal string VideoDefinition => (string)base.GetProperty("VideoDefinition");

	internal string VideoResolution => (string)base.GetProperty("VideoResolution");

	internal string ClientType => (string)base.GetProperty("ClientType");

	public override void SetProperty(string propertyName, object value)
	{
		switch (propertyName)
		{
		case "LicenseRight":
			RightEnum = SchemaHelper.ToMediaRights((string)value);
			break;
		case "AudioEncoding":
			AudioEncodingEnum = SchemaHelper.ToAudioEncoding((string)value);
			break;
		case "VideoDefinition":
			VideoDefinitionEnum = SchemaHelper.ToVideoDefinition((string)value);
			break;
		case "VideoResolution":
			VideoResolutionEnum = SchemaHelper.ToVideoResolution((string)value);
			break;
		case "ClientType":
			ClientTypeEnum = SchemaHelper.ToClientType((string)value);
			break;
		case "CurrencyCode":
			PriceTypeEnum = SchemaHelper.ToPriceType((string)value);
			break;
		}
		base.SetProperty(propertyName, value);
	}

	internal static XmlDataProviderObject ConstructRightObject(DataProviderQuery owner, object objectTypeCookie)
	{
		return new Right(owner, objectTypeCookie);
	}

	internal Right(DataProviderQuery owner, object resultTypeCookie)
		: base(owner, resultTypeCookie)
	{
	}

	public override object GetProperty(string propertyName)
	{
		return propertyName switch
		{
			"HasPoints" => HasPoints, 
			"HasCurrency" => HasCurrency, 
			"PointsPrice" => PointsPrice, 
			"CurrencyPrice" => CurrencyPrice, 
			"Language" => Language, 
			_ => base.GetProperty(propertyName), 
		};
	}
}
