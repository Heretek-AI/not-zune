using System;

namespace ZuneUI;

public static class LibraryHelper
{
	public static Guid GetZuneMediaId(MediaType type, int libraryId)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		return PlaylistManager.GetFieldValue(libraryId, PlaylistManager.MediaTypeToListType(type), 233, Guid.Empty);
	}
}
