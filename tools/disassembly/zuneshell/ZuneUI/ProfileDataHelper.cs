using System;
using Microsoft.Win32;
using Microsoft.Zune.Configuration;

namespace ZuneUI;

public static class ProfileDataHelper
{
	private const int c_updateServicePlayCountHours = 2;

	public static int ProfilePlayCount
	{
		get
		{
			return GetSetPlayCountCache(-1);
		}
		set
		{
			GetSetPlayCountCache(value);
		}
	}

	public static DateTime CommentsLastRead
	{
		get
		{
			return GetCommentsLastRead();
		}
		set
		{
			SetCommentsLastRead(value);
		}
	}

	private static SocialUserGuidConfiguration GetSocialUserGuidConfiguration(string userGuid)
	{
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Expected O, but got Unknown
		return new SocialUserGuidConfiguration(RegistryHive.CurrentUser, ((CConfigurationManagedBase)ClientConfiguration.Social).ConfigurationPath, userGuid);
	}

	public static void InitializePlayCountCache()
	{
		GetSetPlayCountCache(ClientConfiguration.Service.LastSignedInUserGuid, -1, clearStaleCache: true);
	}

	private static int GetSetPlayCountCache(int newValue)
	{
		return GetSetPlayCountCache(ClientConfiguration.Service.LastSignedInUserGuid, newValue, clearStaleCache: false);
	}

	private static int GetSetPlayCountCache(string userGuid, int newValue, bool clearStaleCache)
	{
		int num = -1;
		if (!string.IsNullOrEmpty(userGuid) && userGuid != Guid.Empty.ToString())
		{
			SocialUserGuidConfiguration socialUserGuidConfiguration = GetSocialUserGuidConfiguration(userGuid);
			num = socialUserGuidConfiguration.ProfilePlayCount;
			bool flag = false;
			if (num < newValue)
			{
				flag = true;
			}
			else if (clearStaleCache)
			{
				DateTime dateTime = socialUserGuidConfiguration.ProfilePlayCountUpdated.AddHours(2.0);
				flag = dateTime <= DateTime.UtcNow;
			}
			if (flag)
			{
				socialUserGuidConfiguration.ProfilePlayCount = newValue;
				num = newValue;
				if (clearStaleCache)
				{
					socialUserGuidConfiguration.ProfilePlayCountUpdated = DateTime.UtcNow;
				}
			}
		}
		return num;
	}

	private static DateTime GetCommentsLastRead()
	{
		return GetCommentsLastRead(ClientConfiguration.Service.LastSignedInUserGuid);
	}

	private static DateTime GetCommentsLastRead(string userGuid)
	{
		DateTime result = DateTime.MinValue;
		if (!string.IsNullOrEmpty(userGuid) && userGuid != Guid.Empty.ToString())
		{
			SocialUserGuidConfiguration socialUserGuidConfiguration = GetSocialUserGuidConfiguration(userGuid);
			result = socialUserGuidConfiguration.ProfileCommentsLastRead;
		}
		return result;
	}

	private static void SetCommentsLastRead(DateTime commentsLastRead)
	{
		SetCommentsLastRead(ClientConfiguration.Service.LastSignedInUserGuid, commentsLastRead);
	}

	private static void SetCommentsLastRead(string userGuid, DateTime commentsLastRead)
	{
		if (!string.IsNullOrEmpty(userGuid) && userGuid != Guid.Empty.ToString())
		{
			SocialUserGuidConfiguration socialUserGuidConfiguration = GetSocialUserGuidConfiguration(userGuid);
			socialUserGuidConfiguration.ProfileCommentsLastRead = commentsLastRead;
		}
	}
}
