using Microsoft.Iris;

namespace ZuneXml;

internal class MediaInstance : XmlDataProviderObject
{
	private MediaRightsEnum _rightEnum;

	private VideoDefinitionEnum _videoDefinitionEnum;

	private VideoResolutionEnum _videoResolutionEnum;

	public bool HasPurchasedTrial => _rightEnum == MediaRightsEnum.PurchaseTrial;

	public bool HasPurchasedBeta => _rightEnum == MediaRightsEnum.PurchaseBeta;

	public bool HasPurchased
	{
		get
		{
			if (_rightEnum != MediaRightsEnum.Purchase && _rightEnum != MediaRightsEnum.PurchaseStream && _rightEnum != MediaRightsEnum.PurchaseTrial && _rightEnum != MediaRightsEnum.PurchaseBeta && _rightEnum != MediaRightsEnum.SubscriptionFreePurchase)
			{
				return _rightEnum == MediaRightsEnum.AlbumPurchase;
			}
			return true;
		}
	}

	internal MediaRightsEnum RightEnum => _rightEnum;

	internal VideoDefinitionEnum VideoDefinitionEnum => _videoDefinitionEnum;

	internal VideoResolutionEnum VideoResolutionEnum => _videoResolutionEnum;

	internal bool IsDownloadable => (bool)base.GetProperty("IsDownloadable");

	internal string LicenseRight => (string)base.GetProperty("LicenseRight");

	internal string VideoDefinition => (string)base.GetProperty("VideoDefinition");

	internal string VideoResolution => (string)base.GetProperty("VideoResolution");

	public override void SetProperty(string propertyName, object value)
	{
		switch (propertyName)
		{
		case "LicenseRight":
			_rightEnum = SchemaHelper.ToMediaRights((string)value);
			break;
		case "VideoDefinition":
			_videoDefinitionEnum = SchemaHelper.ToVideoDefinition((string)value);
			break;
		case "VideoResolution":
			_videoResolutionEnum = SchemaHelper.ToVideoResolution((string)value);
			break;
		}
		base.SetProperty(propertyName, value);
	}

	internal static XmlDataProviderObject ConstructMediaInstanceObject(DataProviderQuery owner, object objectTypeCookie)
	{
		return new MediaInstance(owner, objectTypeCookie);
	}

	internal MediaInstance(DataProviderQuery owner, object resultTypeCookie)
		: base(owner, resultTypeCookie)
	{
	}

	public override object GetProperty(string propertyName)
	{
		return propertyName switch
		{
			"HasPurchased" => HasPurchased, 
			"HasPurchasedTrial" => HasPurchasedTrial, 
			"HasPurchasedBeta" => HasPurchasedBeta, 
			_ => base.GetProperty(propertyName), 
		};
	}
}
