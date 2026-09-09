namespace ZuneUI;

public static class DrmStateDescriptions
{
	private static string _noLicenseDescription = Shell.LoadString(StringId.IDS_DRM_HEADER_NO_LICENSE);

	private static string _personalDescription = Shell.LoadString(StringId.IDS_DRM_HEADER_PERSONAL);

	private static string _purchasedDescription = Shell.LoadString(StringId.IDS_DRM_HEADER_PURCHASED);

	private static string _rentalDescription = Shell.LoadString(StringId.IDS_DRM_HEADER_RENTAL);

	private static string _subscriptionDescription = Shell.LoadString(StringId.IDS_DRM_HEADER_SUBSCRIPTION);

	private static string _subscriptionExpiredDescription = Shell.LoadString(StringId.IDS_DRM_HEADER_SUBSCRIPTION_EXPIRED);

	private static string _protectedDescription = Shell.LoadString(StringId.IDS_DRM_HEADER_PROTECTED);

	private static string _unknownDescription = Shell.LoadString(StringId.IDS_DRM_HEADER_UNKNOWN);

	public static string GetAudioDescription(int stateId)
	{
		return GetDescription(MediaType.Track, (DrmState)stateId);
	}

	public static string GetVideoDescription(int stateId)
	{
		return GetDescription(MediaType.Video, (DrmState)stateId);
	}

	public static string GetDescription(MediaType mediaType, DrmState state)
	{
		switch (state)
		{
		case DrmState.Expired:
			if (MediaType.Track == mediaType)
			{
				return _subscriptionExpiredDescription;
			}
			return _rentalDescription;
		case DrmState.Expiring:
			if (MediaType.Track == mediaType)
			{
				return _subscriptionDescription;
			}
			return _rentalDescription;
		case DrmState.Protected:
			if (MediaType.Track == mediaType)
			{
				return _protectedDescription;
			}
			return _purchasedDescription;
		case DrmState.Free:
			return _personalDescription;
		case DrmState.NoLicense:
			return _noLicenseDescription;
		case DrmState.DeviceLicense:
			return _rentalDescription;
		case DrmState.Unknown:
			return _unknownDescription;
		default:
			return _unknownDescription;
		}
	}
}
