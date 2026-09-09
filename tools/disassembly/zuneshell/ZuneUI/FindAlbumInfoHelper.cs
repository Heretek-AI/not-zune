using System.Collections.Generic;
using Microsoft.Iris;
using Microsoft.Zune.Shell;
using MicrosoftZuneLibrary;

namespace ZuneUI;

public class FindAlbumInfoHelper
{
	public static AlbumMetadata GetAlbumMetadata(int libraryId)
	{
		return ZuneApplication.ZuneLibrary.GetAlbumMetadata(libraryId);
	}

	public static List<SongMatchData> GetSongMatchDataList(AlbumMetadata localAlbum, AlbumMetadata mergedAlbum, uint wmisAlbumTrackCount)
	{
		List<SongMatchData> list = new List<SongMatchData>((int)localAlbum.TrackCount);
		for (uint num = 0u; num < localAlbum.TrackCount; num++)
		{
			TrackMetadata track = localAlbum.GetTrack(num);
			GroupedList optionsForTrack = GetOptionsForTrack(track, mergedAlbum, wmisAlbumTrackCount);
			int trackIndexFromAlbumMetadata = GetTrackIndexFromAlbumMetadata(mergedAlbum, track.MediaId);
			int selectedMatchIndex = ((trackIndexFromAlbumMetadata < wmisAlbumTrackCount) ? (trackIndexFromAlbumMetadata + 1) : (-1));
			SongMatchData item = new SongMatchData(mergedAlbum, track, optionsForTrack, trackIndexFromAlbumMetadata, selectedMatchIndex);
			list.Add(item);
		}
		return list;
	}

	private static int GetTrackIndexFromAlbumMetadata(AlbumMetadata albumMetadata, int mediaId)
	{
		for (uint num = 0u; num < albumMetadata.TrackCount; num++)
		{
			if (albumMetadata.GetTrack(num).MediaId == mediaId)
			{
				return (int)num;
			}
		}
		return -1;
	}

	public static void SetAlbumTrackMediaId(AlbumMetadata albumMetadata, int trackIndex, int mediaId)
	{
		for (uint num = 0u; num < albumMetadata.TrackCount; num++)
		{
			TrackMetadata track = albumMetadata.GetTrack(num);
			if (track.MediaId == mediaId)
			{
				track.MediaId = -1;
				break;
			}
		}
		albumMetadata.GetTrack((uint)trackIndex).MediaId = mediaId;
	}

	private static GroupedList GetOptionsForTrack(TrackMetadata track, AlbumMetadata mergedAlbum, uint wmisAlbumTrackCount)
	{
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		//IL_005a: Expected O, but got Unknown
		List<TrackOptionGroupItem> list = new List<TrackOptionGroupItem>((int)(wmisAlbumTrackCount + 1));
		TrackOptionGroupItem trackOptionGroupItem = new TrackOptionGroupItem();
		trackOptionGroupItem.TrackMetadata = track;
		trackOptionGroupItem.Original = true;
		list.Add(trackOptionGroupItem);
		for (int i = 0; i < wmisAlbumTrackCount; i++)
		{
			TrackOptionGroupItem trackOptionGroupItem2 = new TrackOptionGroupItem();
			trackOptionGroupItem2.TrackMetadata = mergedAlbum.GetTrack((uint)i);
			trackOptionGroupItem2.Original = false;
			list.Add(trackOptionGroupItem2);
		}
		GroupedList val = new GroupedList();
		val.Comparer = new TrackOptionsComparer();
		val.Source = list;
		return val;
	}
}
