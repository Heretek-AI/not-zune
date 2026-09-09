using System;

namespace ZuneUI;

public static class SyncErrorHelper
{
	public static Guid GetMediaGuidForLibraryId(int id, MediaType type)
	{
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		if (type == MediaType.PodcastEpisode)
		{
			id = PlaylistManager.GetFieldValue(id, PlaylistManager.MediaTypeToListType(type), 311, 0);
		}
		return PlaylistManager.GetFieldValue(id, PlaylistManager.MediaTypeToListType(type), 451, Guid.Empty);
	}

	public static string GetEpisodeUrlForLibraryId(int id)
	{
		//IL_0003: Unknown result type (might be due to invalid IL or missing references)
		return PlaylistManager.GetFieldValue(id, PlaylistManager.MediaTypeToListType(MediaType.PodcastEpisode), 317, string.Empty);
	}
}
