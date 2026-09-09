using System;
using System.Collections;
using Microsoft.Iris;
using MicrosoftZuneLibrary;

namespace ZuneUI;

public class TrackDetails
{
	private static int[] ColumnIndexes = new int[19]
	{
		344, 151, 32, 177, 181, 317, 138, 65, 382, 380,
		292, 135, 437, 389, 390, 398, 175, 176, 68
	};

	private static string[] DataProperties = new string[19]
	{
		"Title", "Duration", "Bitrate", "MediaType", "FolderName", "FilePath", "ArtistName", "ContributingArtistNames", "AlbumName", "AlbumArtistName",
		"ReleaseDate", "DiscNumber", "TrackNumber", "ComposerName", "ConductorName", "Genre", "FileName", "FileSize", "Copyright"
	};

	private static string _contributingArtistFormat = Shell.LoadString(StringId.IDS_CONTRIBUTING_ARTISTS_SEPARATION_FORMAT);

	private static string _contributingArtistSeperator = Shell.LoadString(StringId.IDS_CONTRIBUTING_ARTISTS_SEPARATOR);

	public static void Populate(object dataContainer, int libraryId)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Expected O, but got Unknown
		//IL_00fb: Unknown result type (might be due to invalid IL or missing references)
		DataProviderObject val = (DataProviderObject)dataContainer;
		object[] array = new object[19]
		{
			string.Empty,
			TimeSpan.Zero,
			0,
			0,
			string.Empty,
			string.Empty,
			string.Empty,
			new ArrayList(),
			string.Empty,
			string.Empty,
			DateTime.MinValue,
			0,
			0,
			string.Empty,
			string.Empty,
			string.Empty,
			string.Empty,
			0L,
			string.Empty
		};
		bool[] array2 = new bool[array.Length];
		ZuneLibrary.GetFieldValues(libraryId, (EListType)2, ColumnIndexes.Length, ColumnIndexes, array, array2, PlaylistManager.Instance.QueryContext);
		for (int i = 0; i < ColumnIndexes.Length; i++)
		{
			if (ColumnIndexes[i] == 177)
			{
				array[i] = MediaDescriptions.Map((MediaType)array[i]);
			}
			val.SetProperty(DataProperties[i], array[i]);
		}
	}

	public static string GetGenreHelper(DataProviderObject item)
	{
		int mediaId = (int)item.GetProperty("LibraryId");
		return PlaylistManager.GetFieldValue(mediaId, (EListType)2, 398, "");
	}

	public static void SetGenreHelper(DataProviderObject item, string genre)
	{
		int mediaId = (int)item.GetProperty("LibraryId");
		PlaylistManager.SetFieldValue(mediaId, (EListType)2, 398, genre);
	}

	public static Guid GetServiceId(int mediaId)
	{
		return PlaylistManager.GetFieldValue(mediaId, (EListType)2, 451, Guid.Empty);
	}

	public static string ContributingArtistListToString(IList artists)
	{
		return ContributingArtistListToString(artists, _contributingArtistFormat);
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

	public static IList ContributingArtistStringToList(string contributingArtists)
	{
		return ContributingArtistStringToList(contributingArtists, _contributingArtistSeperator);
	}

	private static IList ContributingArtistStringToList(string contributingArtists, string separator)
	{
		ArrayList arrayList = null;
		string[] array = contributingArtists.Split(separator.ToCharArray(0, 1));
		string[] array2 = array;
		foreach (string text in array2)
		{
			string text2 = text.Trim();
			if (text2.Length > 0)
			{
				if (arrayList == null)
				{
					arrayList = new ArrayList();
				}
				arrayList.Add(text2);
			}
		}
		return arrayList;
	}
}
