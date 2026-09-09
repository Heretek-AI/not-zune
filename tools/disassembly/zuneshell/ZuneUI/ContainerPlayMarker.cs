using System;
using Microsoft.Zune.Playlist;

namespace ZuneUI;

[Serializable]
public class ContainerPlayMarker
{
	private int _libraryId = -1;

	private MediaType _mediaType;

	private PlaylistType _playlistType;

	private int _playlistSubType;

	private bool _marked;

	public int LibraryId
	{
		get
		{
			return _libraryId;
		}
		set
		{
			_libraryId = value;
		}
	}

	public MediaType MediaType
	{
		get
		{
			return _mediaType;
		}
		set
		{
			_mediaType = value;
		}
	}

	public PlaylistType PlaylistType
	{
		get
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			return _playlistType;
		}
		set
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			//IL_0002: Unknown result type (might be due to invalid IL or missing references)
			_playlistType = value;
		}
	}

	public int PlaylistSubType
	{
		get
		{
			return _playlistSubType;
		}
		set
		{
			_playlistSubType = value;
		}
	}

	public bool Marked
	{
		get
		{
			return _marked;
		}
		set
		{
			_marked = value;
		}
	}
}
