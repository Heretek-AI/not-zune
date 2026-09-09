using System;
using Microsoft.Iris;
using MicrosoftZuneLibrary;

namespace ZuneUI;

public class PodcastEpisodeDetails
{
	private static int[] ColumnIndexes = new int[14]
	{
		344, 312, 172, 24, 292, 151, 177, 317, 222, 175,
		181, 176, 32, 68
	};

	private static string[] DataProperties = new string[14]
	{
		"Title", "SeriesTitle", "SeriesFeedUrl", "Author", "ReleaseDate", "Duration", "MediaType", "SourceUrl", "EnclosureUrl", "FileName",
		"FolderName", "FileSize", "Bitrate", "Copyright"
	};

	public static void Populate(object dataContainer, int libraryId)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Expected O, but got Unknown
		//IL_00ad: Unknown result type (might be due to invalid IL or missing references)
		DataProviderObject val = (DataProviderObject)dataContainer;
		object[] array = new object[14]
		{
			string.Empty,
			string.Empty,
			string.Empty,
			string.Empty,
			DateTime.MinValue,
			TimeSpan.Zero,
			(object)(EMediaTypes)3,
			string.Empty,
			string.Empty,
			string.Empty,
			string.Empty,
			0L,
			0,
			string.Empty
		};
		ZuneLibrary.GetFieldValues(libraryId, (EListType)7, ColumnIndexes.Length, ColumnIndexes, array, PlaylistManager.Instance.QueryContext);
		for (int i = 0; i < ColumnIndexes.Length; i++)
		{
			if (ColumnIndexes[i] == 177)
			{
				array[i] = MediaDescriptions.Map((MediaType)array[i]);
			}
			val.SetProperty(DataProperties[i], array[i]);
		}
	}
}
