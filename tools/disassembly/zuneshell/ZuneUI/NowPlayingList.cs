using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.Serialization;
using Microsoft.Iris;
using Microsoft.Zune.Configuration;
using Microsoft.Zune.Playlist;
using Microsoft.Zune.QuickMix;
using Microsoft.Zune.Shell;
using Microsoft.Zune.Util;
using MicrosoftZuneLibrary;
using UIXControls;
using ZuneXml;

namespace ZuneUI;

[Serializable]
internal class NowPlayingList : IDisposable
{
	private enum MovingMode
	{
		Advancing,
		Retreating,
		Jumping
	}

	private const int s_quickMixCreationTimeoutMilliseconds = 15000;

	private const int s_nearingCompletionThreshold = 3;

	[NonSerialized]
	private ArrayListDataSet _tracks;

	private List<PlaybackTrack> _savedTracks;

	private int[] _shuffleOrder;

	private bool _shuffling;

	private bool _repeating;

	private int _indexShuffleStart;

	private PlaybackContext _playbackContext;

	private int _currentRandomSeed;

	private PlayNavigationOptions _playNavigationOptions;

	private bool _dontPlayMarketplaceTracks;

	private bool _playWhenReady;

	private EQuickMixType _quickMixType = (EQuickMixType)(-1);

	private string _quickMixTitle;

	[NonSerialized]
	private int _transientTracksPlaylistId;

	[NonSerialized]
	private static int _transientPlaylistCount;

	[NonSerialized]
	private PlaybackTrack _trackCurrent;

	[NonSerialized]
	private PlaybackTrack _trackNext;

	private int _indexCurrent;

	[NonSerialized]
	private QuickMixSession _quickMixSession;

	[NonSerialized]
	private QuickMixNotification _quickMixCreatingNotification;

	[NonSerialized]
	private StringId _quickMixNoResultsStringId;

	private static Random s_random = new Random();

	public ArrayListDataSet TrackList => _tracks;

	public int Count
	{
		get
		{
			if (_tracks == null)
			{
				return 0;
			}
			return ((ListDataSet)_tracks).Count;
		}
	}

	public PlaybackContext PlaybackContext => _playbackContext;

	public PlayNavigationOptions PlayNavigationOptions
	{
		get
		{
			return _playNavigationOptions;
		}
		set
		{
			_playNavigationOptions = value;
		}
	}

	public string QuickMixTitle
	{
		get
		{
			//IL_0017: Unknown result type (might be due to invalid IL or missing references)
			string quickMixTitle = _quickMixTitle;
			if (_quickMixSession != null)
			{
				_quickMixSession.GetPlaylistTitle(ref quickMixTitle);
			}
			return quickMixTitle;
		}
	}

	public bool DontPlayMarketplaceTracks
	{
		get
		{
			return _dontPlayMarketplaceTracks;
		}
		set
		{
			if (value != _dontPlayMarketplaceTracks)
			{
				_dontPlayMarketplaceTracks = value;
				UpdateTracks();
			}
		}
	}

	public bool PlayWhenReady
	{
		get
		{
			return _playWhenReady;
		}
		set
		{
			if (value != _playWhenReady)
			{
				_playWhenReady = value;
			}
		}
	}

	public QuickMixSession QuickMixSession => _quickMixSession;

	public EQuickMixType QuickMixType => _quickMixType;

	public PlaybackTrack CurrentTrack => _trackCurrent;

	public PlaybackTrack NextTrack => _trackNext;

	public int ListIndexOfCurrentTrack
	{
		get
		{
			int num = _indexCurrent;
			if (num != -1 && _shuffling)
			{
				num = _shuffleOrder[num];
			}
			return num;
		}
	}

	public bool CanAdvance => _trackNext != null;

	public bool CanRetreat
	{
		get
		{
			if (_repeating)
			{
				return true;
			}
			if (_shuffling)
			{
				return _indexCurrent != _indexShuffleStart;
			}
			return _indexCurrent > 0;
		}
	}

	public NowPlayingList(IList items, int startIndex, PlaybackContext context, PlayNavigationOptions playNavigationOptions, bool shuffling, ContainerPlayMarker containerPlayMarker, bool dontPlayMarketplaceTracks)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		_transientTracksPlaylistId = int.MinValue;
		_playbackContext = context;
		_playNavigationOptions = playNavigationOptions;
		_dontPlayMarketplaceTracks = dontPlayMarketplaceTracks;
		if (_playbackContext == PlaybackContext.QuickMix)
		{
			AddQuickMixItemsWorker(items);
		}
		else
		{
			AddItemsWorker(items, startIndex, shuffling, containerPlayMarker);
		}
	}

	public void Dispose()
	{
		if (_quickMixSession != null)
		{
			_quickMixSession.Dispose();
			_quickMixSession = null;
		}
		CleanupTransientTracks();
	}

	private void CleanupTransientTracks()
	{
		if (_transientTracksPlaylistId != int.MinValue)
		{
			int[] array = new int[1] { _transientTracksPlaylistId };
			ZuneApplication.ZuneLibrary.DeleteMedia(array, (EMediaTypes)9, false);
			_transientTracksPlaylistId = int.MinValue;
		}
	}

	private void AddQuickMixItemsWorker(IList items)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0071: Unknown result type (might be due to invalid IL or missing references)
		//IL_0076: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0304: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_02cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d9: Expected O, but got Unknown
		//IL_02d9: Expected O, but got Unknown
		//IL_02d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_0340: Unknown result type (might be due to invalid IL or missing references)
		//IL_0345: Unknown result type (might be due to invalid IL or missing references)
		//IL_0168: Unknown result type (might be due to invalid IL or missing references)
		//IL_0271: Unknown result type (might be due to invalid IL or missing references)
		//IL_0278: Unknown result type (might be due to invalid IL or missing references)
		//IL_027d: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e7: Unknown result type (might be due to invalid IL or missing references)
		QuickMix instance = QuickMix.Instance;
		HRESULT val = HRESULT._S_OK;
		EMediaTypes val2 = (EMediaTypes)(-1);
		_quickMixNoResultsStringId = StringId.IDS_QUICKMIX_ITEM_CREATION_UNAVAILABLE_TEXT;
		if (_quickMixSession != null)
		{
			_quickMixSession.Dispose();
			_quickMixSession = null;
		}
		NotificationArea.Instance.RemoveAll(NotificationTask.QuickMix, NotificationState.OneShot);
		if (items[0] is Artist artist)
		{
			_quickMixNoResultsStringId = StringId.IDS_QUICKMIX_ARTIST_CREATION_UNAVAILABLE_TEXT;
			val = instance.CreateSession((EQuickMixMode)1, artist.Id, (EMediaTypes)65, artist.Title, ref _quickMixSession);
			if (((HRESULT)(ref val)).IsSuccess)
			{
				string format = Shell.LoadString(StringId.IDS_QUICKMIX_NOTIFICATION_CREATION_BUSY_ONE_PARAM_TEXT);
				format = string.Format(format, artist.Title);
				_quickMixCreatingNotification = new QuickMixNotification(Shell.LoadString(StringId.IDS_QUICKMIX_NOTIFICATION_CREATION_BUSY_TITLE), format, NotificationState.OneShot, showWebHelpLink: false, 15000);
				SQMLog.Log((SQMDataId)195, 1);
			}
		}
		if (((HRESULT)(ref val)).IsSuccess)
		{
			object? obj = items[0];
			LibraryDataProviderItemBase val3 = (LibraryDataProviderItemBase)((obj is LibraryDataProviderItemBase) ? obj : null);
			if (val3 != null)
			{
				if (((DataProviderObject)val3).TypeName == "Artist")
				{
					val2 = (EMediaTypes)65;
					_quickMixNoResultsStringId = StringId.IDS_QUICKMIX_ARTIST_CREATION_UNAVAILABLE_TEXT;
					string arg = (string)((DataProviderObject)val3).GetProperty("Title");
					string format2 = Shell.LoadString(StringId.IDS_QUICKMIX_NOTIFICATION_CREATION_BUSY_ONE_PARAM_TEXT);
					format2 = string.Format(format2, arg);
					_quickMixCreatingNotification = new QuickMixNotification(Shell.LoadString(StringId.IDS_QUICKMIX_NOTIFICATION_CREATION_BUSY_TITLE), format2, NotificationState.OneShot, showWebHelpLink: false, 15000);
				}
				else if (((DataProviderObject)val3).TypeName == "Album")
				{
					val2 = (EMediaTypes)11;
					_quickMixNoResultsStringId = StringId.IDS_QUICKMIX_ALBUM_CREATION_UNAVAILABLE_TEXT;
					string arg2 = (string)((DataProviderObject)val3).GetProperty("ArtistName");
					string arg3 = (string)((DataProviderObject)val3).GetProperty("Title");
					string format3 = Shell.LoadString(StringId.IDS_QUICKMIX_NOTIFICATION_CREATION_BUSY_TWO_PARAM_TEXT);
					format3 = string.Format(format3, arg2, arg3);
					_quickMixCreatingNotification = new QuickMixNotification(Shell.LoadString(StringId.IDS_QUICKMIX_NOTIFICATION_CREATION_BUSY_TITLE), format3, NotificationState.OneShot, showWebHelpLink: false, 15000);
				}
				else if (((DataProviderObject)val3).TypeName == "Track")
				{
					val2 = (EMediaTypes)3;
					_quickMixNoResultsStringId = StringId.IDS_QUICKMIX_SONG_CREATION_UNAVAILABLE_TEXT;
					string arg4 = (string)((DataProviderObject)val3).GetProperty("ArtistName");
					string arg5 = (string)((DataProviderObject)val3).GetProperty("Title");
					string format4 = Shell.LoadString(StringId.IDS_QUICKMIX_NOTIFICATION_CREATION_BUSY_TWO_PARAM_TEXT);
					format4 = string.Format(format4, arg4, arg5);
					_quickMixCreatingNotification = new QuickMixNotification(Shell.LoadString(StringId.IDS_QUICKMIX_NOTIFICATION_CREATION_BUSY_TITLE), format4, NotificationState.OneShot, showWebHelpLink: false, 15000);
				}
				val = instance.CreateSession((EQuickMixMode)1, new int[1] { (int)((DataProviderObject)val3).GetProperty("LibraryId") }, val2, ref _quickMixSession);
				SQMLog.Log((SQMDataId)194, 1);
			}
		}
		if (((HRESULT)(ref val)).IsSuccess)
		{
			if (_quickMixSession != null)
			{
				TimeSpan timeSpan = TimeSpan.FromMilliseconds(15000.0);
				val = _quickMixSession.GetSimilarMedia((uint)ClientConfiguration.QuickMix.DefaultPlaylistLength, timeSpan, new SimilarMediaBatchHandler(SimilarBatchHandler), new BatchEndHandler(BatchEndHandler));
				_quickMixType = _quickMixSession.GetQuickMixType();
				_quickMixSession.GetPlaylistTitle(ref _quickMixTitle);
			}
			else
			{
				val = HRESULT._E_UNEXPECTED;
			}
		}
		if (((HRESULT)(ref val)).IsSuccess)
		{
			NotificationArea.Instance.Add(_quickMixCreatingNotification);
			return;
		}
		if (_quickMixSession != null)
		{
			_quickMixSession.Dispose();
			_quickMixSession = null;
		}
		if (HRESULT.op_Implicit(((HRESULT)(ref val)).Int) == HRESULT._ZUNE_E_QUICKMIX_MEDIA_NOT_FOUND)
		{
			MessageBox.Show(Shell.LoadString(StringId.IDS_QUICKMIX_CREATION_UNAVAILABLE_NO_RESULTS_TITLE), Shell.LoadString(_quickMixNoResultsStringId), (EventHandler)null);
		}
		else
		{
			ErrorDialogInfo.Show(((HRESULT)(ref val)).Int, Shell.LoadString(StringId.IDS_QUICKMIX_CREATION_UNAVAILABLE_NO_RESULTS_TITLE));
		}
	}

	private void SimilarBatchHandler(IList itemList)
	{
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Expected O, but got Unknown
		Application.DeferredInvoke((DeferredInvokeHandler)delegate
		{
			//IL_0035: Unknown result type (might be due to invalid IL or missing references)
			//IL_003f: Expected I4, but got Unknown
			if (_quickMixSession != null)
			{
				ContainerPlayMarker playMarker = new ContainerPlayMarker
				{
					LibraryId = -1,
					MediaType = MediaType.Playlist,
					PlaylistType = (PlaylistType)7,
					PlaylistSubType = (int)_quickMixSession.GetQuickMixType()
				};
				AddItems(itemList, playMarker);
				if (PlayWhenReady)
				{
					PlayWhenReady = false;
					SingletonModelItem<TransportControls>.Instance.PlayPendingList();
				}
			}
		}, (object)null);
	}

	private void BatchEndHandler(HRESULT hrAsync)
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_0008: Unknown result type (might be due to invalid IL or missing references)
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Expected O, but got Unknown
		Application.DeferredInvoke((DeferredInvokeHandler)delegate
		{
			//IL_0046: Unknown result type (might be due to invalid IL or missing references)
			//IL_004b: Unknown result type (might be due to invalid IL or missing references)
			NotificationArea.Instance.Remove(_quickMixCreatingNotification);
			_quickMixCreatingNotification = null;
			if (_quickMixSession != null && ((HRESULT)(ref hrAsync)).IsError)
			{
				if (HRESULT.op_Implicit(((HRESULT)(ref hrAsync)).Int) == HRESULT._ZUNE_E_QUICKMIX_MEDIA_NOT_FOUND)
				{
					MessageBox.Show(Shell.LoadString(StringId.IDS_QUICKMIX_CREATION_UNAVAILABLE_NO_RESULTS_TITLE), Shell.LoadString(_quickMixNoResultsStringId), (EventHandler)null);
				}
				else
				{
					ErrorDialogInfo.Show(((HRESULT)(ref hrAsync)).Int, Shell.LoadString(StringId.IDS_QUICKMIX_CREATION_UNAVAILABLE_NO_RESULTS_TITLE));
				}
			}
		}, (object)null);
	}

	public void SetShuffling(bool shuffling)
	{
		if (_quickMixSession != null)
		{
			return;
		}
		if (_tracks == null || ((ListDataSet)_tracks).Count == 0)
		{
			_shuffling = shuffling;
		}
		else if (shuffling != _shuffling)
		{
			int num = ((!_shuffling) ? _indexCurrent : _shuffleOrder[_indexCurrent]);
			if (shuffling)
			{
				RebuildShuffleOrder();
			}
			else
			{
				_shuffleOrder = null;
			}
			if (shuffling)
			{
				_indexCurrent = FindShuffleIndexFromListIndex(num);
			}
			else
			{
				_indexCurrent = num;
			}
			_shuffling = shuffling;
			UpdateTracks();
		}
	}

	public void SetRepeating(bool repeating)
	{
		if (_repeating != repeating)
		{
			_repeating = repeating;
			UpdateTracks();
		}
	}

	public void SyncCurrentTrackTo(PlaybackTrack track)
	{
		if (track == _trackCurrent)
		{
			return;
		}
		int num = -1;
		if (track == _trackNext)
		{
			num = ComputeNextIndex(MovingMode.Advancing);
		}
		else
		{
			num = ((ListDataSet)_tracks).IndexOf((object)track);
			if (num != -1 && _shuffling)
			{
				num = FindShuffleIndexFromListIndex(num);
			}
		}
		if (num != -1)
		{
			MoveToWorker(num, MovingMode.Advancing);
		}
	}

	public void MoveToTrackIndex(int indexNew)
	{
		if (_shuffling)
		{
			indexNew = FindShuffleIndexFromListIndex(indexNew);
		}
		MoveToWorker(indexNew, MovingMode.Jumping);
	}

	public void Advance()
	{
		MoveToWorker(ComputeNextIndex(MovingMode.Advancing), MovingMode.Advancing);
	}

	public void Retreat()
	{
		if (_shuffling && _repeating && _indexCurrent == _indexShuffleStart)
		{
			RebuildShuffleOrder(_currentRandomSeed - 1);
		}
		MoveToWorker(ComputeNextIndex(MovingMode.Retreating), MovingMode.Retreating);
	}

	public int AddItems(IList items)
	{
		return AddItems(items, null);
	}

	public int AddItems(IList items, ContainerPlayMarker playMarker)
	{
		return AddItemsWorker(items, -1, shuffling: false, playMarker);
	}

	public void ResetForReplay()
	{
		if (_shuffling)
		{
			_indexCurrent = s_random.Next(((ListDataSet)_tracks).Count);
		}
		else
		{
			_indexCurrent = 0;
		}
		if (_shuffling)
		{
			RebuildShuffleOrder();
		}
		UpdateTracks();
	}

	private void MoveToWorker(int indexNew, MovingMode mode)
	{
		//IL_0077: Unknown result type (might be due to invalid IL or missing references)
		//IL_0083: Unknown result type (might be due to invalid IL or missing references)
		//IL_008d: Expected O, but got Unknown
		//IL_008d: Expected O, but got Unknown
		//IL_0088: Unknown result type (might be due to invalid IL or missing references)
		//IL_008d: Unknown result type (might be due to invalid IL or missing references)
		//IL_009e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a3: Unknown result type (might be due to invalid IL or missing references)
		_indexCurrent = indexNew;
		if (_shuffling)
		{
			if (mode == MovingMode.Advancing && indexNew == _indexShuffleStart)
			{
				RebuildShuffleOrder(_currentRandomSeed + 1);
			}
			else if (mode == MovingMode.Jumping)
			{
				RebuildShuffleOrder();
			}
		}
		UpdateTracks();
		if (CanGetMoreQuickMixTracks() && ListNearingCompletion())
		{
			TimeSpan timeSpan = TimeSpan.FromMilliseconds(15000.0);
			HRESULT similarMedia = _quickMixSession.GetSimilarMedia((uint)ClientConfiguration.QuickMix.DefaultPlaylistLength, timeSpan, new SimilarMediaBatchHandler(SimilarBatchHandler), new BatchEndHandler(BatchEndHandler));
			if (((HRESULT)(ref similarMedia)).IsError && !(HRESULT.op_Implicit(((HRESULT)(ref similarMedia)).Int) == HRESULT._ZUNE_E_QUICKMIX_SESSION_IN_USE))
			{
				ErrorDialogInfo.Show(((HRESULT)(ref similarMedia)).Int, Shell.LoadString(StringId.IDS_QUICKMIX_CREATION_UNAVAILABLE_NO_RESULTS_TITLE));
			}
		}
	}

	private bool CanGetMoreQuickMixTracks()
	{
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Invalid comparison between Unknown and I4
		bool result = false;
		if (_quickMixSession != null)
		{
			result = (int)_quickMixSession.GetQuickMixType() != 2 || SignIn.Instance.SignedIn;
		}
		return result;
	}

	private int ComputeNextIndex(MovingMode mode)
	{
		return ComputeNextIndex(mode, _indexCurrent, allowLooping: true);
	}

	private int ComputeNextIndex(MovingMode mode, int fromIndex, bool allowLooping)
	{
		if (mode != MovingMode.Advancing && mode != MovingMode.Retreating)
		{
			return fromIndex;
		}
		int num;
		if (!_dontPlayMarketplaceTracks)
		{
			num = ComputeNextIndexUnfiltered(mode, fromIndex, allowLooping);
		}
		else
		{
			num = fromIndex;
			bool flag = false;
			for (int i = 0; i < ((ListDataSet)_tracks).Count; i++)
			{
				num = ComputeNextIndexUnfiltered(mode, num, allowLooping);
				if (num == -1)
				{
					break;
				}
				PlaybackTrack playbackTrack = ((!_shuffling) ? (((ListDataSet)_tracks)[num] as PlaybackTrack) : (((ListDataSet)_tracks)[_shuffleOrder[num]] as PlaybackTrack));
				if (playbackTrack != null && playbackTrack.IsInVisibleCollection)
				{
					flag = true;
					break;
				}
			}
			if (!flag)
			{
				num = -1;
			}
		}
		return num;
	}

	private int ComputeNextIndexUnfiltered(MovingMode mode, int indexCurrent, bool allowLooping)
	{
		int num = ((mode != MovingMode.Advancing) ? (indexCurrent - 1) : (indexCurrent + 1));
		if (_shuffling)
		{
			if (num == ((ListDataSet)_tracks).Count)
			{
				num = 0;
			}
			else if (num == -1)
			{
				num = ((ListDataSet)_tracks).Count - 1;
			}
			if (((mode == MovingMode.Advancing && num == _indexShuffleStart) || (mode == MovingMode.Retreating && num == _indexShuffleStart - 1)) && (!_repeating || !allowLooping))
			{
				num = -1;
			}
		}
		else if (num == ((ListDataSet)_tracks).Count)
		{
			num = ((!_repeating || !allowLooping) ? (-1) : 0);
		}
		else if (num == -1)
		{
			num = ((_repeating && allowLooping) ? (((ListDataSet)_tracks).Count - 1) : (-1));
		}
		return num;
	}

	private bool ListNearingCompletion()
	{
		if (((ListDataSet)_tracks).Count == 0)
		{
			return false;
		}
		int num = _indexCurrent;
		_ = ((ListDataSet)_tracks).Count;
		bool result = false;
		for (int i = 0; i < 3; i++)
		{
			num = ComputeNextIndex(MovingMode.Advancing, num, allowLooping: false);
			if (num == -1)
			{
				result = true;
				break;
			}
		}
		return result;
	}

	internal void UpdateTracks()
	{
		object obj = null;
		object obj2 = null;
		if (_tracks != null && _indexCurrent != -1)
		{
			int num = (_shuffling ? _shuffleOrder[_indexCurrent] : _indexCurrent);
			obj = ((ListDataSet)_tracks)[num];
			num = ComputeNextIndex(MovingMode.Advancing);
			if (num != -1)
			{
				if (_shuffling)
				{
					num = _shuffleOrder[num];
				}
				obj2 = ((ListDataSet)_tracks)[num];
			}
		}
		_trackCurrent = (PlaybackTrack)obj;
		_trackNext = (PlaybackTrack)obj2;
	}

	private int FindShuffleIndexFromListIndex(int index)
	{
		int result = -1;
		for (int i = 0; i < ((ListDataSet)_tracks).Count; i++)
		{
			if (_shuffleOrder[i] == index)
			{
				result = i;
				break;
			}
		}
		return result;
	}

	private void RebuildShuffleOrder(int? seed)
	{
		int num = -1;
		num = ((!_shuffling) ? _indexCurrent : _shuffleOrder[_indexCurrent]);
		if (_tracks == null)
		{
			_shuffleOrder = null;
		}
		else
		{
			if (_shuffleOrder == null || _shuffleOrder.Length != ((ListDataSet)_tracks).Count)
			{
				_shuffleOrder = new int[((ListDataSet)_tracks).Count];
			}
			for (int i = 0; i < ((ListDataSet)_tracks).Count; i++)
			{
				_shuffleOrder[i] = i;
			}
			if (seed.HasValue)
			{
				_currentRandomSeed = seed.Value;
			}
			else
			{
				_currentRandomSeed = (int)DateTime.Now.Ticks;
			}
			Random random = new Random(_currentRandomSeed);
			for (int i = ((ListDataSet)_tracks).Count - 1; i > 0; i--)
			{
				int num2 = random.Next(i + 1);
				int num3 = _shuffleOrder[num2];
				_shuffleOrder[num2] = _shuffleOrder[i];
				_shuffleOrder[i] = num3;
			}
		}
		_indexShuffleStart = FindShuffleIndexFromListIndex(num);
		if (_shuffling)
		{
			_indexCurrent = _indexShuffleStart;
		}
	}

	private void RebuildShuffleOrder()
	{
		RebuildShuffleOrder(null);
	}

	private int AddItemsWorker(IList items, int startIndex, bool shuffling, ContainerPlayMarker containerPlayMarker)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Expected O, but got Unknown
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Invalid comparison between Unknown and I4
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ee: Expected O, but got Unknown
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0087: Unknown result type (might be due to invalid IL or missing references)
		//IL_008a: Invalid comparison between Unknown and I4
		//IL_00a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_0200: Invalid comparison between Unknown and I4
		ArrayListDataSet val = new ArrayListDataSet();
		if (containerPlayMarker != null && containerPlayMarker.MediaType == MediaType.Playlist && (int)containerPlayMarker.PlaylistType == 7)
		{
			_playbackContext = PlaybackContext.QuickMix;
			_quickMixType = (EQuickMixType)containerPlayMarker.PlaylistSubType;
			_quickMixTitle = PlaylistManager.GetPlaylistName(containerPlayMarker.LibraryId);
			SQMLog.Log((SQMDataId)197, 1);
		}
		else if (items != null && items.Count == 1 && items[0] is LibraryDataProviderItemBase)
		{
			int playlistId = -1;
			EMediaTypes val2 = (EMediaTypes)(-1);
			object? obj = items[0];
			LibraryDataProviderItemBase val3 = (LibraryDataProviderItemBase)((obj is LibraryDataProviderItemBase) ? obj : null);
			val3.GetMediaIdAndType(ref playlistId, ref val2);
			if ((int)val2 == 9 && PlaylistManager.GetPlaylistType(playlistId) == 7)
			{
				_playbackContext = PlaybackContext.QuickMix;
				_quickMixType = (EQuickMixType)PlaylistManager.GetSubType(playlistId);
				_quickMixTitle = PlaylistManager.GetPlaylistName(playlistId);
				SQMLog.Log((SQMDataId)197, 1);
			}
		}
		PlaylistManager.AddItemsToTrackList(items, (IList)val, ref startIndex, allowVideo: true, allowPictures: false, materializeMarketplaceTracks: false, containerPlayMarker);
		if (((ListDataSet)val).Count != 0)
		{
			bool flag = false;
			if (_tracks == null)
			{
				_tracks = new ArrayListDataSet();
				if (startIndex == -1)
				{
					startIndex = (shuffling ? s_random.Next(((ListDataSet)val).Count) : 0);
				}
				flag = true;
			}
			bool flag2 = false;
			bool flag3 = false;
			if (((ListDataSet)_tracks).Count > 0)
			{
				PlaybackTrack playbackTrack = (PlaybackTrack)((ListDataSet)_tracks)[0];
				if (playbackTrack.IsVideo)
				{
					flag2 = true;
				}
				else
				{
					flag3 = true;
				}
			}
			for (int i = 0; i < ((ListDataSet)val).Count; i++)
			{
				PlaybackTrack playbackTrack2 = (PlaybackTrack)((ListDataSet)val)[i];
				bool flag4 = true;
				if (playbackTrack2.IsVideo)
				{
					if (flag2)
					{
						flag4 = false;
					}
					else if (flag3)
					{
						flag4 = false;
					}
					else
					{
						flag2 = true;
					}
				}
				else if (flag2)
				{
					flag4 = false;
				}
				else
				{
					flag3 = true;
				}
				if (flag4)
				{
					((ListDataSet)_tracks).Add(((ListDataSet)val)[i]);
				}
				if (flag && i == startIndex)
				{
					if (((ListDataSet)_tracks).Count > 0)
					{
						_indexCurrent = ((ListDataSet)_tracks).Count - 1;
					}
					else
					{
						_indexCurrent = 0;
					}
				}
			}
			if (_shuffling)
			{
				RebuildShuffleOrder();
			}
			if ((int)_quickMixType != -1)
			{
				AddReferencesToRemoteTracks((IList)val);
			}
		}
		UpdateTracks();
		return ((ListDataSet)val).Count;
	}

	private void AddReferencesToRemoteTracks(IList tracks)
	{
		if (tracks == null)
		{
			return;
		}
		if (_transientTracksPlaylistId == int.MinValue)
		{
			PlaylistResult playlistResult = PlaylistManager.Instance.CreatePlaylist("Now Playing Transient Tracks Playlist [" + _transientPlaylistCount + "]", privatePlaylist: true);
			_transientPlaylistCount++;
			_transientTracksPlaylistId = playlistResult.PlaylistId;
		}
		List<LibraryPlaybackTrack> list = new List<LibraryPlaybackTrack>(tracks.Count);
		foreach (object track in tracks)
		{
			if (track is LibraryPlaybackTrack { IsInVisibleCollection: false } libraryPlaybackTrack)
			{
				list.Add(libraryPlaybackTrack);
			}
		}
		PlaylistManager.Instance.AddToPlaylist(_transientTracksPlaylistId, list, rememberAsDefault: false, notify: false);
	}

	public void Reorder(IList indices, int targetIndex)
	{
		PlaybackTrack currentTrack = CurrentTrack;
		((ListDataSet)_tracks).Reorder(indices, targetIndex);
		if (_shuffling)
		{
			RebuildShuffleOrder();
		}
		_indexCurrent = ((ListDataSet)_tracks).IndexOf((object)currentTrack);
		if (_shuffling)
		{
			_indexCurrent = FindShuffleIndexFromListIndex(_indexCurrent);
		}
		UpdateTracks();
	}

	public bool Remove(IList indices)
	{
		bool shuffling = _shuffling;
		SetShuffling(shuffling: false);
		int num = _indexCurrent;
		bool result = false;
		for (int num2 = indices.Count - 1; num2 >= 0; num2--)
		{
			int num3 = (int)indices[num2];
			((ListDataSet)_tracks).RemoveAt(num3);
			if (num == num3)
			{
				result = true;
			}
			else if (num > num3)
			{
				num--;
			}
		}
		if (num >= ((ListDataSet)_tracks).Count)
		{
			num = ((ListDataSet)_tracks).Count - 1;
		}
		_indexCurrent = num;
		SetShuffling(shuffling);
		UpdateTracks();
		return result;
	}

	public IList GetNextTracks(int count)
	{
		List<PlaybackTrack> list = null;
		if (_tracks != null && ((ListDataSet)_tracks).Count > 0)
		{
			list = new List<PlaybackTrack>(count);
			for (int num = 0; num < count; num++)
			{
				int num2 = _indexCurrent + num;
				if (_repeating || _shuffling)
				{
					while (num2 >= ((ListDataSet)_tracks).Count)
					{
						num2 -= ((ListDataSet)_tracks).Count;
					}
				}
				if (num2 < 0)
				{
					break;
				}
				bool num3;
				if (!_shuffling)
				{
					num3 = num2 == ((ListDataSet)_tracks).Count;
				}
				else
				{
					if (num2 != _indexShuffleStart)
					{
						goto IL_0094;
					}
					num3 = num != 0;
				}
				if (num3)
				{
					break;
				}
				goto IL_0094;
				IL_0094:
				if (_shuffling)
				{
					num2 = _shuffleOrder[num2];
				}
				list.Add((PlaybackTrack)((ListDataSet)_tracks)[num2]);
			}
		}
		return list;
	}

	[OnSerializing]
	internal void PrepareForSerialization(StreamingContext context)
	{
		if (_tracks != null)
		{
			int num = Math.Min(_indexCurrent + 380, ((ListDataSet)_tracks).Count);
			int num2 = 400 - (num - _indexCurrent);
			int num3 = Math.Max(_indexCurrent - num2, 0);
			_savedTracks = new List<PlaybackTrack>(num - num3);
			for (int i = num3; i < num; i++)
			{
				int num4 = ((!_shuffling) ? i : _shuffleOrder[i]);
				_savedTracks.Add((PlaybackTrack)((ListDataSet)_tracks)[num4]);
			}
			int num5 = _indexCurrent - num3;
			if (_shuffling)
			{
				_indexCurrent = 0;
				_shuffleOrder[0] = num5;
			}
			else
			{
				_indexCurrent = num5;
			}
		}
	}

	[OnDeserialized]
	internal void HandleDeserialization(StreamingContext context)
	{
		//IL_007c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0087: Expected O, but got Unknown
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_001e: Expected O, but got Unknown
		_transientTracksPlaylistId = int.MinValue;
		if (_savedTracks != null)
		{
			_tracks = new ArrayListDataSet();
			foreach (PlaybackTrack savedTrack in _savedTracks)
			{
				((ListDataSet)_tracks).Add((object)savedTrack);
			}
			_savedTracks = null;
			if (_shuffling)
			{
				RebuildShuffleOrder();
			}
			UpdateTracks();
		}
		Application.DeferredInvoke((DeferredInvokeHandler)delegate
		{
			SingletonModelItem<TransportControls>.Instance.Shuffling.Value = _shuffling;
		}, (object)null);
	}

	[Conditional("DEBUG_NOW_PLAYING_LIST")]
	private static void _DEBUG_Trace(string message, params object[] args)
	{
	}
}
