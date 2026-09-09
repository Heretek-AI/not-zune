using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Iris;
using Microsoft.Win32;
using Microsoft.Zune.Configuration;
using Microsoft.Zune.Service;
using Microsoft.Zune.Shell;
using ZuneXml;

namespace ZuneUI;

public class RecommendationsHelper
{
	private static List<int> _uploadedArtistList;

	private static int _maxSongsForYouTrackCount;

	public static DateTime RecommendationsDate
	{
		get
		{
			return ServiceUserGuidConfiguration.RecommendationsDate.ToUniversalTime();
		}
		set
		{
			ServiceUserGuidConfiguration.RecommendationsDate = value;
		}
	}

	public static DateTime LastRecommendationsShuffleDate
	{
		get
		{
			return ServiceUserGuidConfiguration.RecommendationsRefreshDate.ToUniversalTime();
		}
		set
		{
			ServiceUserGuidConfiguration.RecommendationsRefreshDate = value;
		}
	}

	public static TimeSpan RefreshPeriod => new TimeSpan(24, 0, 0);

	public static int ShuffleSeed
	{
		get
		{
			return ServiceUserGuidConfiguration.Seed;
		}
		set
		{
			ServiceUserGuidConfiguration.Seed = value;
		}
	}

	public static int MaxSongsForYouTrackCount
	{
		get
		{
			if (_maxSongsForYouTrackCount == 0)
			{
				_maxSongsForYouTrackCount = ClientConfiguration.Service.RecommendationsMaxTrackCount;
			}
			return _maxSongsForYouTrackCount;
		}
	}

	public static ServiceUserGuidConfiguration ServiceUserGuidConfiguration
	{
		get
		{
			//IL_0025: Unknown result type (might be due to invalid IL or missing references)
			//IL_002b: Expected O, but got Unknown
			string text = $"{ClientConfiguration.Service.LastSignedInUserGuid}";
			return new ServiceUserGuidConfiguration(RegistryHive.CurrentUser, ((CConfigurationManagedBase)ClientConfiguration.Service).ConfigurationPath, text);
		}
	}

	public static bool IncludeTrackCollection(ICollection tracks, TrackCollectionFilterType filterType)
	{
		bool result = false;
		if (tracks != null)
		{
			foreach (Track track in tracks)
			{
				if (track.Id == Guid.Empty)
				{
					continue;
				}
				bool flag = ZuneApplication.Service.InVisibleCollection(track.Id, (EContentType)0);
				if (filterType == TrackCollectionFilterType.IncludeIfAnyTrackNotInCollection)
				{
					if (!flag)
					{
						result = true;
						break;
					}
					continue;
				}
				if (flag)
				{
					result = false;
					break;
				}
				result = true;
			}
		}
		return result;
	}

	public static bool IsArtistInCollection(Guid artistId)
	{
		bool result = false;
		if (artistId != Guid.Empty)
		{
			result = ZuneApplication.Service.InVisibleCollection(artistId, (EContentType)4);
		}
		return result;
	}

	public static object GetKeyValue(IDictionary dictionary, object key)
	{
		object result = null;
		if (dictionary != null && dictionary.Contains(key))
		{
			result = dictionary[key];
		}
		return result;
	}

	public static int GetKeyValueInt(IDictionary dictionary, object key, int defaultValue)
	{
		int result = defaultValue;
		if (dictionary != null && dictionary.Contains(key))
		{
			object obj = dictionary[key];
			if (obj is int)
			{
				result = (int)obj;
			}
		}
		return result;
	}

	public static int GetUserRating(DataProviderObject item)
	{
		int result = 0;
		if (item is Track)
		{
			Guid id = ((Track)(object)item).Id;
			if (id != Guid.Empty)
			{
				int num = 0;
				if (ZuneApplication.Service.GetUserRating(SignIn.Instance.LastSignedInUserId, id, (EContentType)0, ref num))
				{
					result = num;
				}
			}
		}
		return result;
	}

	public static bool SetUserRating(DataProviderObject item, string propertyName, int rating)
	{
		bool flag = false;
		if (item is Track)
		{
			Track track = (Track)(object)item;
			flag = ZuneApplication.Service.SetUserTrackRating(SignIn.Instance.LastSignedInUserId, rating, track.Id, track.AlbumId, track.TrackNumber, track.Title, track.Duration.Milliseconds, track.AlbumTitle, track.Artist, track.PrimaryGenre.Title, (string)((DataProviderObject)track).GetProperty("ReferrerContext"));
			if (flag)
			{
				track.UserRating = rating;
			}
		}
		return flag;
	}

	public static bool SetArtistUserRating(Guid id, string name, int rating)
	{
		bool flag = false;
		return ZuneApplication.Service.SetUserArtistRating(SignIn.Instance.LastSignedInUserId, rating, id, name);
	}

	public static string GetRecommendationsEndpoint()
	{
		return Service.GetEndPointUri((EServiceEndpointId)7);
	}

	private static List<int> GetUploadedArtistUserList()
	{
		if (_uploadedArtistList != null)
		{
			return _uploadedArtistList;
		}
		string usersWhoHaveUploadedArtists = ClientConfiguration.Picks.UsersWhoHaveUploadedArtists;
		if (!string.IsNullOrEmpty(usersWhoHaveUploadedArtists))
		{
			string[] array = usersWhoHaveUploadedArtists.Split(new char[1] { ',' });
			_uploadedArtistList = new List<int>(array.Length);
			string[] array2 = array;
			foreach (string text in array2)
			{
				if (!string.IsNullOrEmpty(text) && int.TryParse(text.Trim(), out var result))
				{
					_uploadedArtistList.Add(result);
				}
			}
		}
		return _uploadedArtistList;
	}

	private static void AddUserToArtistUploadList(int userId)
	{
		if (_uploadedArtistList == null)
		{
			_uploadedArtistList = new List<int>();
		}
		foreach (int uploadedArtist in _uploadedArtistList)
		{
			if (uploadedArtist == userId)
			{
				return;
			}
		}
		_uploadedArtistList.Add(userId);
	}

	private static void SaveArtistUserListToRegistry()
	{
		string text = string.Empty;
		foreach (int uploadedArtist in _uploadedArtistList)
		{
			text = ((!string.IsNullOrEmpty(text)) ? $"{text},{uploadedArtist.ToString()}" : uploadedArtist.ToString());
		}
		ClientConfiguration.Picks.UsersWhoHaveUploadedArtists = text;
	}

	public static bool HasUserUploadedArtists(int userId)
	{
		List<int> uploadedArtistUserList = GetUploadedArtistUserList();
		if (uploadedArtistUserList != null)
		{
			foreach (int item in uploadedArtistUserList)
			{
				if (item == userId)
				{
					return true;
				}
			}
		}
		return false;
	}

	public static void UserHasUploadArtists(int userId)
	{
		AddUserToArtistUploadList(userId);
		SaveArtistUserListToRegistry();
	}
}
