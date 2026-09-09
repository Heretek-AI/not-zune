using System;
using Microsoft.Zune.Service;
using Microsoft.Zune.Shell;

namespace ZuneUI;

public static class DrmHelper
{
	public static bool IsRental(int state, bool fIncludeExpired)
	{
		if (state != 26 && state != 23)
		{
			if (fIncludeExpired)
			{
				return state == 20;
			}
			return false;
		}
		return true;
	}

	public static bool IsRentalExpired(int state, string filename)
	{
		bool result = false;
		switch ((DrmState)state)
		{
		case DrmState.Expired:
			result = true;
			break;
		case DrmState.Expiring:
		{
			DRMInfo fileDRMInfo = ZuneApplication.Service.GetFileDRMInfo(filename);
			if (fileDRMInfo != null && (fileDRMInfo.NoLicense || fileDRMInfo.LicenseExpired))
			{
				result = true;
			}
			break;
		}
		}
		return result;
	}

	public static bool IsRentalExpired(int state, Guid mediaId)
	{
		bool result = false;
		switch ((DrmState)state)
		{
		case DrmState.Expired:
			result = true;
			break;
		case DrmState.Expiring:
		{
			DRMInfo mediaDRMInfo = ZuneApplication.Service.GetMediaDRMInfo(mediaId, (EContentType)3);
			if (mediaDRMInfo != null && (mediaDRMInfo.NoLicense || mediaDRMInfo.LicenseExpired))
			{
				result = true;
			}
			break;
		}
		}
		return result;
	}

	public static void ShowDeviceRentalError()
	{
		Shell.ShowErrorDialog(((HRESULT)(ref HRESULT._NS_E_DRM_DEVICE_RENTAL_LICENSE)).Int, StringId.IDS_PLAYBACK_ERROR);
	}

	public static void ShowRentalExpiredError()
	{
		Shell.ShowErrorDialog(((HRESULT)(ref HRESULT._NS_E_DRM_RENTAL_LICENSE_EXPIRED)).Int, StringId.IDS_PLAYBACK_ERROR);
	}
}
