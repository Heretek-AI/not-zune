using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Iris;
using Microsoft.Zune.Service;
using Microsoft.Zune.Shell;
using MicrosoftZuneLibrary;

namespace ZuneUI;

public class VideoDetails
{
	private static int[] ColumnIndexes = new int[15]
	{
		344, 151, 32, 177, 181, 317, 138, 443, 442, 175,
		176, 68, 49, 149, 451
	};

	private static string[] DataProperties = new string[15]
	{
		"Title", "Duration", "Bitrate", "FileType", "FolderName", "FilePath", "ArtistName", "Width", "Height", "FileName",
		"FileSize", "Copyright", "CategoryId", "DrmState", "ZuneMediaId"
	};

	private static string _actorFormat = Shell.LoadString(StringId.IDS_ACTORS_SEPARATION_FORMAT);

	private static string _directorFormat = Shell.LoadString(StringId.IDS_DIRECTORS_SEPARATION_FORMAT);

	private static string _daysExpirationFormat = Shell.LoadString(StringId.IDS_VIDEO_EXPIRATION_DAYS);

	private static string _hoursExpirationFormat = Shell.LoadString(StringId.IDS_VIDEO_EXPIRATION_HOURS);

	private static string _deviceRentalFormat = Shell.LoadString(StringId.IDS_VIDEO_DEVICE_RENTAL_FORMAT);

	public static string ActorListToString(IList artists)
	{
		return ContributingArtistListToString(artists, _actorFormat);
	}

	public static string DirectorListToString(IList directors)
	{
		return ContributingArtistListToString(directors, _directorFormat);
	}

	private static string ContributingArtistListToString(IList artists, string format)
	{
		string text = "";
		if (artists != null)
		{
			foreach (string artist in artists)
			{
				text = ((text.Length != 0) ? string.Format(format, text, artist) : artist);
			}
		}
		return text;
	}

	public static IList DeviceRentalStatusList(Guid zuneMediaId)
	{
		IList list = new List<string>();
		foreach (UIDevice item in SingletonModelItem<UIDeviceList>.Instance)
		{
			if (ZuneApplication.Service.InCompleteCollection(zuneMediaId, (EContentType)3, item.EndpointId))
			{
				list.Add(string.Format(_deviceRentalFormat, item.Name.ToUpper()));
			}
		}
		return list;
	}

	public static string PCExpirationToString(int drmState, string fileName, int fileType, Guid zuneMediaId)
	{
		string result = "";
		switch (drmState)
		{
		case 20:
			result = Shell.LoadString(StringId.IDS_VIDEO_EXPIRED);
			break;
		case 26:
		{
			if (string.IsNullOrEmpty(fileName))
			{
				break;
			}
			DRMInfo val = null;
			val = ((fileType != 43) ? ZuneApplication.Service.GetFileDRMInfo(fileName) : ZuneApplication.Service.GetMediaDRMInfo(zuneMediaId, (EContentType)3));
			if (val == null)
			{
				break;
			}
			if (val.ValidLicense && val.HasExpiryDate)
			{
				DateTime value = DateTime.UtcNow.ToLocalTime();
				if (val.ExpiryDate.CompareTo(value) > 0)
				{
					TimeSpan timeSpan = val.ExpiryDate.Subtract(value);
					result = ((!(timeSpan.TotalHours >= 49.0)) ? ((!(timeSpan.TotalHours >= 2.0)) ? ((!(timeSpan.TotalHours >= 1.0)) ? Shell.LoadString(StringId.IDS_VIDEO_EXPIRATION_MINUTES) : Shell.LoadString(StringId.IDS_VIDEO_EXPIRATION_ONEHOUR)) : string.Format(_hoursExpirationFormat, (int)timeSpan.TotalHours)) : string.Format(_daysExpirationFormat, timeSpan.Days));
				}
				else
				{
					result = Shell.LoadString(StringId.IDS_VIDEO_EXPIRED);
				}
			}
			else if (val.LicenseExpired)
			{
				result = Shell.LoadString(StringId.IDS_VIDEO_EXPIRED);
			}
			break;
		}
		}
		return result;
	}

	public static void Populate(object dataContainer, int libraryId)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Expected O, but got Unknown
		//IL_00ba: Unknown result type (might be due to invalid IL or missing references)
		DataProviderObject val = (DataProviderObject)dataContainer;
		object[] array = new object[15]
		{
			string.Empty,
			TimeSpan.Zero,
			0,
			0,
			string.Empty,
			string.Empty,
			string.Empty,
			0,
			0,
			string.Empty,
			0L,
			string.Empty,
			0,
			0,
			Guid.Empty
		};
		ZuneLibrary.GetFieldValues(libraryId, (EListType)4, ColumnIndexes.Length, ColumnIndexes, array, PlaylistManager.Instance.QueryContext);
		for (int i = 0; i < ColumnIndexes.Length; i++)
		{
			if (ColumnIndexes[i] == 177)
			{
				val.SetProperty("MediaType", (object)MediaDescriptions.Map((MediaType)array[i]));
			}
			val.SetProperty(DataProperties[i], array[i]);
		}
	}
}
