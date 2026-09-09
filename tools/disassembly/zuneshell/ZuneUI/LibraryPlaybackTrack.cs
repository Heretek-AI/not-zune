using System;
using System.Threading;
using Microsoft.Iris;
using Microsoft.Zune.Service;
using Microsoft.Zune.Shell;
using Microsoft.Zune.Util;
using MicrosoftZuneLibrary;
using MicrosoftZunePlayback;

namespace ZuneUI;

[Serializable]
public class LibraryPlaybackTrack : PlaybackTrack
{
	private class UpdatePlayedStatesTask
	{
		public readonly bool MarkPlayed;

		public readonly bool IncrementPlayCount;

		public readonly bool IncrementSkipCount;

		public readonly int MediaID;

		public readonly EListType ListType;

		public readonly ContainerPlayMarker ContainerPlayMarker;

		public UpdatePlayedStatesTask(bool markPlayed, bool incrementPlayCount, bool incrementSkipCount, int mediaID, EListType listType, ContainerPlayMarker containerPlayMarker)
		{
			//IL_0024: Unknown result type (might be due to invalid IL or missing references)
			//IL_0026: Unknown result type (might be due to invalid IL or missing references)
			MarkPlayed = markPlayed;
			IncrementPlayCount = incrementPlayCount;
			IncrementSkipCount = incrementSkipCount;
			MediaID = mediaID;
			ListType = listType;
			ContainerPlayMarker = containerPlayMarker;
		}
	}

	public enum Streaming
	{
		Unknown,
		Yes,
		No
	}

	private enum PodcastVideoLengthGroup
	{
		Short,
		Medium,
		Long
	}

	private const long c_bookmarkInterval = 10000000L;

	private int _mediaId;

	private Guid _zuneMediaInstanceId;

	private Guid? _zuneMediaId;

	private MediaType _mediaType;

	private EListType _listType;

	private Streaming _isStreaming;

	private bool _isInCollection;

	private bool? _isVideo;

	private int? _userRating;

	private long _markPlayedAt;

	private bool _hasMarkedPlayed;

	private bool _hasIncrementedPlayCount;

	private bool _hasIncrementedSkipCount;

	private long _lastStoredBookmark;

	private long _duration;

	private static long[] c_podcastVideoLengths = new long[3] { 0L, 3000000000L, 6000000000L };

	private static long[] c_podcastVideoMarkPlayedAtEOFMinus = new long[3] { 300000000L, 600000000L, 1200000000L };

	public int MediaId => _mediaId;

	public override Guid ZuneMediaInstanceId => _zuneMediaInstanceId;

	public override MediaType MediaType => _mediaType;

	public EListType ListType => _listType;

	public override bool IsVideo
	{
		get
		{
			if (!_isVideo.HasValue)
			{
				_isVideo = PlaylistManager.IsVideo(_mediaId, _mediaType);
			}
			return _isVideo.Value;
		}
	}

	public override bool IsMusic => _mediaType == MediaType.Track;

	public override bool IsHD
	{
		get
		{
			//IL_000f: Unknown result type (might be due to invalid IL or missing references)
			if (IsVideo)
			{
				VideoDefinition fieldValue = (VideoDefinition)PlaylistManager.GetFieldValue(_mediaId, _listType, 440, -1);
				return VideoDefinitionHelper.IsHD(fieldValue);
			}
			return false;
		}
	}

	public override bool IsStreaming
	{
		get
		{
			//IL_000f: Unknown result type (might be due to invalid IL or missing references)
			if (_isStreaming == Streaming.Unknown)
			{
				int fieldValue = PlaylistManager.GetFieldValue(_mediaId, _listType, 177, -1);
				_isStreaming = ((fieldValue == 43) ? Streaming.Yes : Streaming.No);
			}
			return _isStreaming == Streaming.Yes;
		}
	}

	public override Guid ZuneMediaId
	{
		get
		{
			//IL_0015: Unknown result type (might be due to invalid IL or missing references)
			if (!_zuneMediaId.HasValue)
			{
				_zuneMediaId = PlaylistManager.GetFieldValue(_mediaId, _listType, 451, Guid.Empty);
			}
			return _zuneMediaId.Value;
		}
	}

	public override string Title => PlaylistManager.GetFieldValue(_mediaId, _listType, 344, string.Empty);

	public override TimeSpan Duration => PlaylistManager.GetFieldValue(_mediaId, _listType, 151, TimeSpan.Zero);

	public int AlbumLibraryId => PlaylistManager.GetFieldValue(_mediaId, _listType, 11, -1);

	public int AlbumArtistLibraryId
	{
		get
		{
			int albumLibraryId = AlbumLibraryId;
			if (albumLibraryId >= 0)
			{
				return PlaylistManager.GetFieldValue(albumLibraryId, (EListType)1, 78, -1);
			}
			return -1;
		}
	}

	public override bool CanRate
	{
		get
		{
			//IL_000e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0014: Invalid comparison between Unknown and I4
			bool flag = _containerPlayMarker != null && (int)_containerPlayMarker.PlaylistType == 7;
			bool result = false;
			if (MediaType == MediaType.Track)
			{
				result = flag || IsInCollection;
			}
			return result;
		}
	}

	public override int UserRating
	{
		get
		{
			//IL_001d: Unknown result type (might be due to invalid IL or missing references)
			if (CanRate)
			{
				if (!_userRating.HasValue)
				{
					_userRating = PlaylistManager.GetFieldValue(_mediaId, _listType, 372, 0);
				}
				return _userRating.Value;
			}
			return 0;
		}
		set
		{
			//IL_001b: Unknown result type (might be due to invalid IL or missing references)
			if (CanRate)
			{
				_userRating = value;
				PlaylistManager.SetFieldValue(_mediaId, _listType, 372, value);
				base.RatingChanged.Invoke();
			}
		}
	}

	public override bool IsInCollection => _isInCollection;

	public override bool IsInVisibleCollection => PlaylistManager.IsInVisibleCollection(_mediaId, _mediaType);

	private long Bookmark
	{
		get
		{
			//IL_001a: Unknown result type (might be due to invalid IL or missing references)
			if (_mediaType == MediaType.PodcastEpisode || _mediaType == MediaType.Video)
			{
				return PlaylistManager.GetFieldValue(_mediaId, _listType, 35, 0L);
			}
			return 0L;
		}
	}

	private string Album => PlaylistManager.GetFieldValue(_mediaId, _listType, (_mediaType == MediaType.PodcastEpisode) ? 312 : 382, string.Empty);

	public string DisplayArtist => PlaylistManager.GetFieldValue(_mediaId, _listType, (_mediaType == MediaType.PodcastEpisode) ? 24 : 138, string.Empty);

	public override string ServiceContext
	{
		get
		{
			//IL_0013: Unknown result type (might be due to invalid IL or missing references)
			int num = 0;
			if (MediaType != MediaType.PodcastEpisode)
			{
				num = PlaylistManager.GetFieldValue(_mediaId, _listType, 358, 0);
			}
			if (num != 0)
			{
				return num.ToString();
			}
			return null;
		}
	}

	private int TrackNumber
	{
		get
		{
			//IL_0011: Unknown result type (might be due to invalid IL or missing references)
			if (_mediaType != MediaType.PodcastEpisode)
			{
				return PlaylistManager.GetFieldValue(_mediaId, _listType, 437, 0);
			}
			return 0;
		}
	}

	public LibraryPlaybackTrack(int mediaId, MediaType mediaType, ContainerPlayMarker containerPlayMarker)
	{
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		_mediaId = mediaId;
		_mediaType = mediaType;
		_listType = PlaylistManager.MediaTypeToListType(mediaType);
		_containerPlayMarker = containerPlayMarker;
		WaitCallback callBack = delegate
		{
			//IL_001e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0029: Expected O, but got Unknown
			_isInCollection = PlaylistManager.IsInCollection(_mediaId, _mediaType);
			Application.DeferredInvoke((DeferredInvokeHandler)delegate
			{
				base.RatingChanged.Invoke();
			}, (object)null);
		};
		ThreadPool.QueueUserWorkItem(callBack, null);
	}

	public override HRESULT GetURI(out string uri)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_009a: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a9: Unknown result type (might be due to invalid IL or missing references)
		string text = null;
		HRESULT result = HRESULT._S_OK;
		int fieldValue = PlaylistManager.GetFieldValue(_mediaId, _listType, 177, -1);
		EContentType val;
		if (fieldValue == 43)
		{
			val = (EContentType)3;
		}
		else
		{
			val = (EContentType)0;
			text = PlaylistManager.GetFieldValue<string>(_mediaId, _listType, 317, null);
			if (!string.IsNullOrEmpty(text))
			{
				try
				{
					Uri uri2 = new Uri(text);
					if (uri2.Scheme == Uri.UriSchemeFile && !ZuneLibrary.DoesFileExist(text))
					{
						text = null;
					}
				}
				catch (UriFormatException)
				{
				}
			}
		}
		if (string.IsNullOrEmpty(text) && ZuneMediaId != Guid.Empty)
		{
			result = ZuneApplication.Service.GetContentUri(ZuneMediaId, val, (EContentUriFlags)0, ref text, ref _zuneMediaInstanceId);
		}
		uri = text;
		return result;
	}

	internal override void OnBeginPlayback(PlayerInterop playbackWrapper)
	{
		//IL_0109: Unknown result type (might be due to invalid IL or missing references)
		//IL_010d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0117: Unknown result type (might be due to invalid IL or missing references)
		//IL_0112: Unknown result type (might be due to invalid IL or missing references)
		//IL_0116: Unknown result type (might be due to invalid IL or missing references)
		base.OnBeginPlayback(playbackWrapper);
		_hasMarkedPlayed = false;
		_markPlayedAt = 0L;
		_hasIncrementedPlayCount = false;
		_hasIncrementedSkipCount = false;
		_lastStoredBookmark = 0L;
		_duration = playbackWrapper.Duration;
		if (_mediaType == MediaType.PodcastEpisode || _mediaType == MediaType.Video)
		{
			PodcastVideoLengthGroup podcastVideoLengthGroup = PodcastVideoLengthGroup.Long;
			while (podcastVideoLengthGroup > PodcastVideoLengthGroup.Short && _duration <= c_podcastVideoLengths[(int)podcastVideoLengthGroup])
			{
				podcastVideoLengthGroup--;
			}
			_markPlayedAt = _duration - c_podcastVideoMarkPlayedAtEOFMinus[(int)podcastVideoLengthGroup];
			if (_markPlayedAt < 1)
			{
				_markPlayedAt = 1L;
			}
			_lastStoredBookmark = Bookmark;
			if (_lastStoredBookmark != 0)
			{
				playbackWrapper.SeekToAbsolutePosition(_lastStoredBookmark);
				if (_lastStoredBookmark > 200000000)
				{
					_hasIncrementedPlayCount = true;
				}
			}
			if (_mediaType == MediaType.PodcastEpisode)
			{
				if (IsVideo)
				{
					SQMLog.Log((SQMDataId)29, 1);
				}
				else
				{
					SQMLog.Log((SQMDataId)30, 1);
				}
			}
		}
		Notification.BroadcastNowPlaying((EMediaTypes)(MediaType switch
		{
			MediaType.Track => 3, 
			MediaType.Video => 4, 
			MediaType.PodcastEpisode => 17, 
			_ => -1, 
		}), Album, DisplayArtist, Title, TrackNumber, ZuneMediaId);
	}

	internal override void OnPositionChanged(long position)
	{
		bool flag = false;
		bool flag2 = false;
		if (position > 0 && position < _duration)
		{
			if (!_hasMarkedPlayed && _markPlayedAt > 0 && position >= _markPlayedAt)
			{
				_hasMarkedPlayed = true;
				flag = true;
			}
			if (!_hasIncrementedPlayCount && position >= 200000000)
			{
				_hasIncrementedPlayCount = true;
				flag2 = true;
			}
			long num = 0L;
			if (!_hasMarkedPlayed)
			{
				num = position / 10000000 * 10000000;
			}
			if (_lastStoredBookmark != num)
			{
				_lastStoredBookmark = num;
			}
			if (flag || flag2)
			{
				UpdatePlayedStates(flag, flag2, incrementSkipCount: false);
			}
		}
	}

	internal override void OnSkip()
	{
		if (!_hasIncrementedPlayCount && !_hasIncrementedSkipCount && MediaType != MediaType.PodcastEpisode)
		{
			_hasIncrementedSkipCount = true;
			UpdatePlayedStates(markPlayed: false, incrementPlayCount: false, incrementSkipCount: true);
		}
	}

	internal override void OnEndPlayback(bool endOfMedia)
	{
		base.OnEndPlayback(endOfMedia);
		Notification.ResetNowPlaying();
		if (endOfMedia)
		{
			_lastStoredBookmark = 0L;
			if (!_hasIncrementedPlayCount)
			{
				UpdatePlayedStates(markPlayed: false, incrementPlayCount: true, incrementSkipCount: false);
			}
		}
		CommitLastStoredBookmark();
	}

	private void UpdatePlayedStates(bool markPlayed, bool incrementPlayCount, bool incrementSkipCount)
	{
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		//IL_004d: Invalid comparison between Unknown and I4
		int mediaId = MediaId;
		if (mediaId == -1)
		{
			return;
		}
		EListType listType = ListType;
		ContainerPlayMarker containerPlayMarker = _containerPlayMarker;
		ThreadPool.QueueUserWorkItem(UpdatePlayedStatesWorker, new UpdatePlayedStatesTask(markPlayed, incrementPlayCount, incrementSkipCount, mediaId, listType, containerPlayMarker));
		bool flag = _containerPlayMarker != null && (int)_containerPlayMarker.PlaylistType == 7;
		if (incrementPlayCount)
		{
			if (_mediaType == MediaType.Track)
			{
				Shell.MainFrame.Social.PlayCount++;
			}
			if (flag)
			{
				SQMLog.Log((SQMDataId)202, 1);
			}
		}
		if (incrementSkipCount && flag)
		{
			if (IsInVisibleCollection)
			{
				SQMLog.Log((SQMDataId)200, 1);
			}
			else
			{
				SQMLog.Log((SQMDataId)201, 1);
			}
		}
	}

	private static void UpdatePlayedStatesWorker(object o)
	{
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_0059: Unknown result type (might be due to invalid IL or missing references)
		//IL_0083: Unknown result type (might be due to invalid IL or missing references)
		//IL_0097: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_024a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0250: Invalid comparison between Unknown and I4
		//IL_03aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_03af: Unknown result type (might be due to invalid IL or missing references)
		//IL_03bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_027b: Unknown result type (might be due to invalid IL or missing references)
		//IL_028f: Unknown result type (might be due to invalid IL or missing references)
		//IL_02cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0319: Unknown result type (might be due to invalid IL or missing references)
		//IL_032d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0360: Unknown result type (might be due to invalid IL or missing references)
		if (!(o is UpdatePlayedStatesTask updatePlayedStatesTask))
		{
			return;
		}
		int num = 0;
		int num2 = 0;
		int num3 = 0;
		int[] array = new int[7];
		object[] array2 = new object[7];
		if (updatePlayedStatesTask.IncrementPlayCount)
		{
			array[0] = 367;
			array2[0] = 0;
			ZuneLibrary.GetFieldValues(updatePlayedStatesTask.MediaID, updatePlayedStatesTask.ListType, 1, array, array2, PlaylistManager.Instance.QueryContext);
			num2 = (int)array2[0];
			array[0] = 366;
			array2[0] = 0;
			ZuneLibrary.GetFieldValues(updatePlayedStatesTask.MediaID, updatePlayedStatesTask.ListType, 1, array, array2, PlaylistManager.Instance.QueryContext);
			num = (int)array2[0];
		}
		if (updatePlayedStatesTask.IncrementSkipCount)
		{
			array[0] = 374;
			array2[0] = 0;
			ZuneLibrary.GetFieldValues(updatePlayedStatesTask.MediaID, updatePlayedStatesTask.ListType, 1, array, array2, PlaylistManager.Instance.QueryContext);
			num3 = (int)array2[0];
		}
		int num4 = 0;
		if (updatePlayedStatesTask.MarkPlayed)
		{
			array[num4] = 262;
			array2[num4] = 1;
			num4++;
		}
		if (updatePlayedStatesTask.IncrementPlayCount)
		{
			num2++;
			array[num4] = 367;
			array2[num4] = num2;
			num4++;
			num++;
			array[num4] = 366;
			array2[num4] = num;
			num4++;
			array[num4] = 363;
			array2[num4] = DateTime.UtcNow;
			num4++;
		}
		if (updatePlayedStatesTask.IncrementSkipCount)
		{
			num3++;
			array[num4] = 374;
			array2[num4] = num3;
			num4++;
			array[num4] = 365;
			array2[num4] = DateTime.UtcNow;
			num4++;
		}
		if (num4 > 0)
		{
			ZuneLibrary.SetFieldValues(updatePlayedStatesTask.MediaID, updatePlayedStatesTask.ListType, num4, array, array2, PlaylistManager.Instance.QueryContext);
		}
		if (!updatePlayedStatesTask.IncrementPlayCount || updatePlayedStatesTask.ContainerPlayMarker == null)
		{
			return;
		}
		bool flag = false;
		lock (updatePlayedStatesTask.ContainerPlayMarker)
		{
			if (!updatePlayedStatesTask.ContainerPlayMarker.Marked)
			{
				updatePlayedStatesTask.ContainerPlayMarker.Marked = true;
				flag = true;
			}
		}
		if (!flag)
		{
			return;
		}
		if (updatePlayedStatesTask.ContainerPlayMarker.LibraryId == -1 && (int)updatePlayedStatesTask.ListType == 2)
		{
			array2[0] = -1;
			if (updatePlayedStatesTask.ContainerPlayMarker.MediaType == MediaType.Album)
			{
				array[0] = 11;
				ZuneLibrary.GetFieldValues(updatePlayedStatesTask.MediaID, updatePlayedStatesTask.ListType, 1, array, array2, PlaylistManager.Instance.QueryContext);
				updatePlayedStatesTask.ContainerPlayMarker.LibraryId = (int)array2[0];
			}
			else if (updatePlayedStatesTask.ContainerPlayMarker.MediaType == MediaType.Genre)
			{
				array[0] = 399;
				ZuneLibrary.GetFieldValues(updatePlayedStatesTask.MediaID, updatePlayedStatesTask.ListType, 1, array, array2, PlaylistManager.Instance.QueryContext);
				updatePlayedStatesTask.ContainerPlayMarker.LibraryId = (int)array2[0];
			}
			else if (updatePlayedStatesTask.ContainerPlayMarker.MediaType == MediaType.Artist)
			{
				array[0] = 11;
				ZuneLibrary.GetFieldValues(updatePlayedStatesTask.MediaID, updatePlayedStatesTask.ListType, 1, array, array2, PlaylistManager.Instance.QueryContext);
				int num5 = (int)array2[0];
				array2[0] = -1;
				array[0] = 78;
				ZuneLibrary.GetFieldValues(num5, (EListType)1, 1, array, array2, PlaylistManager.Instance.QueryContext);
				updatePlayedStatesTask.ContainerPlayMarker.LibraryId = (int)array2[0];
			}
		}
		if (updatePlayedStatesTask.ContainerPlayMarker.LibraryId != -1)
		{
			array[0] = 363;
			array2[0] = DateTime.UtcNow;
			EListType val = PlaylistManager.MediaTypeToListType(updatePlayedStatesTask.ContainerPlayMarker.MediaType);
			ZuneLibrary.SetFieldValues(updatePlayedStatesTask.ContainerPlayMarker.LibraryId, val, 1, array, array2, PlaylistManager.Instance.QueryContext);
		}
	}

	private void CommitLastStoredBookmark()
	{
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		if (MediaType == MediaType.PodcastEpisode || MediaType == MediaType.Video)
		{
			int[] array = new int[1];
			object[] array2 = new object[1];
			array[0] = 35;
			array2[0] = _lastStoredBookmark;
			ZuneLibrary.SetFieldValues(MediaId, ListType, 1, array, array2, PlaylistManager.Instance.QueryContext);
		}
	}

	public void FindInCollection()
	{
		if (_mediaType == MediaType.Track)
		{
			MusicLibraryPage.FindInCollection(AlbumArtistLibraryId, AlbumLibraryId, MediaId);
		}
	}

	public void RatingUpdatedExternally(int newRating)
	{
		_userRating = newRating;
		base.RatingChanged.Invoke();
	}
}
