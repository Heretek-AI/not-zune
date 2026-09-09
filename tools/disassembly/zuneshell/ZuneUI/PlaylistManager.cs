using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using Microsoft.Iris;
using Microsoft.Zune.Configuration;
using Microsoft.Zune.Playlist;
using Microsoft.Zune.QuickMix;
using Microsoft.Zune.Service;
using Microsoft.Zune.Shell;
using Microsoft.Zune.Subscription;
using Microsoft.Zune.Util;
using MicrosoftZuneInterop;
using MicrosoftZuneLibrary;
using UIXControls;
using ZuneXml;

namespace ZuneUI;

public class PlaylistManager : ModelItem
{
	private const int DefaultAutoRefreshFreq = 7;

	private const PlaylistLimitType DefaultPlaylistLimitType = (PlaylistLimitType)2;

	private static object s_invalidMediaId = -1;

	private static object s_invalidMediaType = MediaType.Undefined;

	private static char[] s_autoPlaylistSplitChars = new char[1] { ';' };

	private static PlaylistManager _instance;

	private QueryPropertyBag _queryContext;

	private int _defaultPlaylistId = int.MinValue;

	private PlaylistManager _playlistManagerInterop;

	private Notification _notification;

	public static PlaylistManager Instance
	{
		get
		{
			if (_instance == null)
			{
				_instance = new PlaylistManager();
			}
			return _instance;
		}
	}

	public int DefaultPlaylistId
	{
		get
		{
			return _defaultPlaylistId;
		}
		private set
		{
			if (_defaultPlaylistId != value)
			{
				_defaultPlaylistId = value;
				((ModelItem)this).FirePropertyChanged("DefaultPlaylistId");
				((ModelItem)this).FirePropertyChanged("DefaultPlaylistName");
			}
		}
	}

	public string DefaultPlaylistName => GetPlaylistName(_defaultPlaylistId);

	internal QueryPropertyBag QueryContext
	{
		get
		{
			if (_queryContext != null)
			{
				_queryContext.SetValue("UserId", (object)SignIn.Instance.LastSignedInUserId);
			}
			return _queryContext;
		}
	}

	public static int InvalidPlaylistId => int.MinValue;

	public static int NowPlayingId => -1;

	public static int ImportErrorCode => ((HRESULT)(ref HRESULT._NS_E_WMP_PLAYLIST_IMPORT_ERROR)).Int;

	private PlaylistManager()
	{
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Expected O, but got Unknown
		_playlistManagerInterop = PlaylistManager.Instance;
		_queryContext = new QueryPropertyBag();
		_queryContext.SetValue("QueryView", (object)0);
	}

	protected override void OnDispose(bool fDisposing)
	{
		((ModelItem)this).OnDispose(fDisposing);
		_queryContext = null;
	}

	public PlaylistResult CreatePlaylist(string title)
	{
		return CreatePlaylist(title, (CreatePlaylistOption)0);
	}

	public PlaylistResult CreateAutoPlaylist(string title, CreatePlaylistOption options)
	{
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		SQMLog.Log((SQMDataId)152, 1);
		return CreatePlaylist(title, (CreatePlaylistOption)(8 | options));
	}

	public PlaylistResult CreateComplexSyncRulePlaylist(string title)
	{
		return CreatePlaylist(title, (CreatePlaylistOption)24);
	}

	internal PlaylistResult CreatePlaylist(string title, bool privatePlaylist)
	{
		return CreatePlaylist(title, (CreatePlaylistOption)(privatePlaylist ? 1 : 0));
	}

	public PlaylistResult CreatePlaylist(string title, CreatePlaylistOption options)
	{
		//IL_0009: Unknown result type (might be due to invalid IL or missing references)
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		int playlistId = default(int);
		HRESULT hr = _playlistManagerInterop.CreatePlaylist(title, (string)null, (ValueType)null, options, ref playlistId);
		return new PlaylistResult(playlistId, hr);
	}

	public PlaylistResult SavePlaylistAsStatic(int playlistId, PlaylistAsyncOperationCompleted completedDelegate)
	{
		//IL_0008: Unknown result type (might be due to invalid IL or missing references)
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		HRESULT hr = _playlistManagerInterop.SavePlaylistAsStatic(playlistId, completedDelegate);
		return new PlaylistResult(playlistId, hr);
	}

	public PlaylistResult GetPlaylistByServiceMediaId(Guid serviceMediaId)
	{
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		int invalidPlaylistId = InvalidPlaylistId;
		HRESULT playlistByServiceMediaId = _playlistManagerInterop.GetPlaylistByServiceMediaId(serviceMediaId, ref invalidPlaylistId);
		return new PlaylistResult(invalidPlaylistId, playlistByServiceMediaId);
	}

	public PlaylistResult CreateAndAddToUniquePlaylist(string title, Guid? serviceMediaId, IList items)
	{
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		int playlistId = default(int);
		HRESULT hr = _playlistManagerInterop.CreatePlaylist(title, (string)null, (ValueType)(object)serviceMediaId, (CreatePlaylistOption)2, ref playlistId);
		if (((HRESULT)(ref hr)).IsError)
		{
			return new PlaylistResult(playlistId, hr);
		}
		PlaylistError error = AddToPlaylist(playlistId, items);
		return new PlaylistResult(playlistId, error);
	}

	public PlaylistResult CreateAndAddToUniquePlaylist(string title, IList items)
	{
		return CreateAndAddToUniquePlaylist(title, null, items);
	}

	public PlaylistError AddToPlaylist(int playlistId, IList items)
	{
		return AddToPlaylist(playlistId, items, rememberAsDefault: true);
	}

	public PlaylistError AddToPlaylist(int playlistId, IList items, bool rememberAsDefault)
	{
		return AddToPlaylist(playlistId, items, rememberAsDefault, notify: true);
	}

	public PlaylistError AddToPlaylist(int playlistId, IList items, bool rememberAsDefault, bool notify)
	{
		//IL_00c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c9: Unknown result type (might be due to invalid IL or missing references)
		int num = items?.Count ?? 0;
		int num2 = 0;
		if (num > 0)
		{
			int startIndex = 0;
			List<PlaybackTrack> list = new List<PlaybackTrack>(num);
			bool flag = playlistId == NowPlayingId;
			bool materializeMarketplaceTracks = !flag;
			bool flag2 = playlistId == CDAccess.Instance.BurnListId;
			bool allowPictures = flag2;
			AddItemsToTrackList(items, list, ref startIndex, flag2, allowPictures, materializeMarketplaceTracks, null);
			num2 = list.Count;
			if (num2 > 0)
			{
				if (!flag)
				{
					int[] array = new int[num2];
					int[] array2 = new int[num2];
					for (int i = 0; i < num2; i++)
					{
						if (list[i] is LibraryPlaybackTrack libraryPlaybackTrack)
						{
							array[i] = libraryPlaybackTrack.MediaId;
							array2[i] = (int)libraryPlaybackTrack.MediaType;
						}
					}
					HRESULT val = _playlistManagerInterop.AddMediaToPlaylist(playlistId, array.Length, array, array2, -1, (int[])null);
					if (((HRESULT)(ref val)).IsError)
					{
						if (playlistId == DefaultPlaylistId)
						{
							DefaultPlaylistId = InvalidPlaylistId;
						}
						return PlaylistError.InvalidId;
					}
				}
				else
				{
					SingletonModelItem<TransportControls>.Instance.AddToNowPlaying(items);
				}
			}
		}
		if (rememberAsDefault)
		{
			DefaultPlaylistId = playlistId;
		}
		if (notify)
		{
			NotifyItemsAdded(playlistId, num2);
		}
		return PlaylistError.Success;
	}

	public void NotifyItemsAdded(int playlistId, int count)
	{
		string text = null;
		string playlistName = GetPlaylistName(playlistId);
		text = ((count <= 0) ? string.Format(Shell.LoadString(StringId.IDS_PLAYLIST_NEW_DEFAULT), playlistName) : ((count != 1) ? string.Format(Shell.LoadString(StringId.IDS_PLAYLIST_ADDED_N_ITEMS), count, playlistName) : string.Format(Shell.LoadString(StringId.IDS_PLAYLIST_ADDED_1_ITEM), playlistName)));
		ShowNewNotification(text);
	}

	public void NotifyAutoPlaylistCreated()
	{
		ShowNewNotification(Shell.LoadString(StringId.IDS_AUTOPLAYLIST_CREATED));
	}

	private void ShowNewNotification(string message)
	{
		if (_notification != null && !((ModelItem)_notification).IsDisposed)
		{
			NotificationArea.Instance.Remove(_notification);
		}
		_notification = new MessageNotification(message, NotificationTask.EditPlaylist, NotificationState.OneShot, 5000);
		NotificationArea.Instance.Add(_notification);
	}

	public void RemoveFromPlaylist(int playlistId, IList items)
	{
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		int count = items.Count;
		int[] array = new int[count];
		for (int i = 0; i < count; i++)
		{
			array[i] = (int)((DataProviderObject)items[i]).GetProperty("LibraryId");
		}
		_playlistManagerInterop.RemoveMediaFromPlaylist(playlistId, count, array);
	}

	public void ReorderInPlaylist(int playlistId, IList items, int newIndex)
	{
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		int count = items.Count;
		int[] array = new int[count];
		for (int i = 0; i < count; i++)
		{
			array[i] = (int)((DataProviderObject)items[i]).GetProperty("LibraryId");
		}
		_playlistManagerInterop.UpdateMediaPositionInPlaylist(playlistId, count, array, newIndex + 1);
	}

	public PlaylistResult RenamePlaylist(int playlistId, string newTitle)
	{
		//IL_0008: Unknown result type (might be due to invalid IL or missing references)
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		HRESULT hr = _playlistManagerInterop.RenamePlaylist(playlistId, newTitle);
		return new PlaylistResult(playlistId, hr);
	}

	public void SetSubType(int playlistId, int subtype)
	{
		SQMLog.LogToStream((SQMDataId)207, (uint)subtype);
		SetFieldValue(playlistId, (EListType)12, 324, subtype);
	}

	public void SetLimitType(int playlistId, int type)
	{
		SQMLog.LogToStream((SQMDataId)204, (uint)type);
		SetFieldValue(playlistId, (EListType)12, 220, type);
	}

	public void SetLimitValue(int playlistId, int value, int type)
	{
		SQMLog.LogToStream((SQMDataId)((type == 0) ? 205 : 206), (uint)value);
		SetFieldValue(playlistId, (EListType)12, 221, value);
	}

	public void SetAutoRefresh(int playlistId, bool enable)
	{
		SQMLog.Log((SQMDataId)203, enable ? 1 : 0);
		SetFieldValue(playlistId, (EListType)12, 25, enable);
	}

	public void SetAutoRefreshFreq(int playlistId, int freq)
	{
		SetFieldValue(playlistId, (EListType)12, 26, freq);
	}

	public PlaylistResult RefreshAutoPlaylist(int playlistId)
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		HRESULT hr = _playlistManagerInterop.RefreshAutoPlaylist(playlistId);
		return new PlaylistResult(playlistId, hr);
	}

	public PlaylistResult FreezeAutoPlaylist(int playlistId)
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		HRESULT hr = _playlistManagerInterop.DisableAutomaticRefresh(playlistId);
		return new PlaylistResult(playlistId, hr);
	}

	public PlaylistResult UnfreezeAutoPlaylist(int playlistId)
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		HRESULT hr = _playlistManagerInterop.EnableAutomaticRefresh(playlistId);
		return new PlaylistResult(playlistId, hr);
	}

	public void ValidateDefaultPlaylist()
	{
		if (DefaultPlaylistId >= 0)
		{
			string fieldValue = GetFieldValue<string>(DefaultPlaylistId, (EListType)12, 317, null);
			if (string.IsNullOrEmpty(fieldValue))
			{
				DefaultPlaylistId = InvalidPlaylistId;
			}
			else
			{
				((ModelItem)this).FirePropertyChanged("DefaultPlaylistName");
			}
		}
	}

	public static string GetPlaylistName(int playlistId)
	{
		if (playlistId == NowPlayingId)
		{
			return Shell.LoadString(StringId.IDS_NOW_PLAYING);
		}
		if (playlistId >= 0)
		{
			return GetFieldValue(playlistId, (EListType)12, 344, string.Empty);
		}
		return null;
	}

	public static bool GetPlaylistAutoRefresh(int playlistId)
	{
		if (playlistId >= 0)
		{
			return GetFieldValue(playlistId, (EListType)12, 25, defaultValue: false);
		}
		return false;
	}

	public static int GetPlaylistAutoRefreshFreq(int playlistId)
	{
		if (playlistId >= 0)
		{
			return GetFieldValue(playlistId, (EListType)12, 26, 7);
		}
		return 7;
	}

	public Choice GetSubTypeChoice(QuickMixSessionManager manager)
	{
		//IL_0063: Unknown result type (might be due to invalid IL or missing references)
		//IL_0069: Expected O, but got Unknown
		List<Command> list = new List<Command>();
		list.Add((Command)(object)new QuickMixSubTypeCommand(Shell.LoadString(StringId.IDS_QUICKMIXPLAYLIST_LOCAL_CONTENT_ONLY), (EQuickMixType)0, manager.QuickMixSession));
		if (FeatureEnablement.IsFeatureEnabled((Features)27))
		{
			list.Add((Command)(object)new QuickMixSubTypeCommand(Shell.LoadString(StringId.IDS_QUICKMIXPLAYLIST_MIXED_CONTENT), (EQuickMixType)1, manager.QuickMixSession));
			list.Add((Command)(object)new QuickMixSubTypeCommand(Shell.LoadString(StringId.IDS_QUICKMIXPLAYLIST_MP_CONTENT_ONLY), (EQuickMixType)2, manager.QuickMixSession));
		}
		Choice val = new Choice();
		val.Options = list;
		return val;
	}

	public static int GetSubType(int playlistId)
	{
		if (playlistId >= 0)
		{
			return GetFieldValue(playlistId, (EListType)12, 324, 0);
		}
		return -1;
	}

	public static int GetLimitType(int playlistId)
	{
		if (playlistId >= 0)
		{
			return GetFieldValue(playlistId, (EListType)12, 220, 2);
		}
		return 2;
	}

	public static int GetLimitValue(int playlistId)
	{
		if (playlistId >= 0)
		{
			return GetFieldValue(playlistId, (EListType)12, 221, 0);
		}
		return 0;
	}

	public static int GetPlaylistType(int playlistId)
	{
		if (playlistId >= 0)
		{
			return GetFieldValue(playlistId, (EListType)12, 265, 0);
		}
		return 0;
	}

	public static bool IsChannel(int playlistId)
	{
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_000d: Invalid comparison between Unknown and I4
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Invalid comparison between Unknown and I4
		if (playlistId >= 0)
		{
			PlaylistType val = (PlaylistType)GetPlaylistType(playlistId);
			if ((int)val == 5 || (int)val == 6)
			{
				return true;
			}
		}
		return false;
	}

	public static MediaType GetAutoPlaylistSchema(int playlistId)
	{
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Expected I4, but got Unknown
		if (playlistId >= 0)
		{
			EMediaTypes val = default(EMediaTypes);
			HRESULT autoPlaylistSchema = PlaylistManager.Instance.GetAutoPlaylistSchema(playlistId, ref val);
			if (((HRESULT)(ref autoPlaylistSchema)).IsSuccess)
			{
				return (MediaType)val;
			}
		}
		return MediaType.Undefined;
	}

	public static void AddItemsToTrackList(IList items, IList tracks, ref int startIndex, bool allowVideo, bool allowPictures, bool materializeMarketplaceTracks, ContainerPlayMarker containerPlayMarkerOverride)
	{
		//IL_009d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a4: Expected O, but got Unknown
		//IL_02c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_02de: Unknown result type (might be due to invalid IL or missing references)
		//IL_02eb: Expected I4, but got Unknown
		//IL_02fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0309: Expected O, but got Unknown
		//IL_031c: Unknown result type (might be due to invalid IL or missing references)
		//IL_032d: Expected O, but got Unknown
		//IL_0341: Unknown result type (might be due to invalid IL or missing references)
		//IL_0353: Expected O, but got Unknown
		//IL_0396: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a3: Expected O, but got Unknown
		int num = startIndex;
		if (items == null || tracks == null)
		{
			return;
		}
		int count = items.Count;
		if (count <= 0)
		{
			return;
		}
		MediaType mediaType = MediaType.Undefined;
		object? obj = items[0];
		LibraryDataProviderItemBase val = (LibraryDataProviderItemBase)((obj is LibraryDataProviderItemBase) ? obj : null);
		if (val != null)
		{
			if (((DataProviderObject)val).TypeName == "Artist")
			{
				mediaType = MediaType.Artist;
			}
			else if (((DataProviderObject)val).TypeName == "Album")
			{
				mediaType = MediaType.Album;
			}
			else if (((DataProviderObject)val).TypeName == "Genre")
			{
				mediaType = MediaType.Genre;
			}
		}
		if (mediaType == MediaType.Artist || mediaType == MediaType.Genre || mediaType == MediaType.Album)
		{
			IList list = new ArrayList(count);
			foreach (LibraryDataProviderItemBase item in items)
			{
				LibraryDataProviderItemBase val2 = item;
				list.Add((int)((DataProviderObject)val2).GetProperty("LibraryId"));
			}
			bool singleAlbum = list.Count == 1;
			if ((mediaType == MediaType.Artist || mediaType == MediaType.Genre) && list.Count == 1)
			{
				ZuneQueryList val3 = null;
				val3 = ((mediaType != MediaType.Artist) ? ZuneApplication.ZuneLibrary.GetAlbumsByGenres(list, (string)null) : ZuneApplication.ZuneLibrary.GetAlbumsByArtists(list, (string)null));
				val3.AddRef();
				singleAlbum = val3.Count == 1;
				val3.Release();
				val3.Dispose();
			}
			string text = null;
			if (ZuneShell.DefaultInstance.CurrentPage is MusicLibraryPage musicLibraryPage)
			{
				text = musicLibraryPage.GetSort(singleAlbum, mediaType);
			}
			if (text == null)
			{
				text = "+WM/TrackNumber";
			}
			ZuneQueryList val4 = null;
			switch (mediaType)
			{
			case MediaType.Artist:
				val4 = ZuneApplication.ZuneLibrary.GetTracksByArtists(list, text);
				break;
			case MediaType.Genre:
				val4 = ZuneApplication.ZuneLibrary.GetTracksByGenres(list, text);
				break;
			case MediaType.Album:
				val4 = ZuneApplication.ZuneLibrary.GetTracksByAlbums(list, text);
				break;
			}
			val4.AddRef();
			ArrayList uniqueIds = val4.GetUniqueIds();
			int num2 = uniqueIds?.Count ?? 0;
			for (int i = 0; i < num2; i++)
			{
				ContainerPlayMarker containerPlayMarker = null;
				if (containerPlayMarkerOverride != null)
				{
					containerPlayMarker = containerPlayMarkerOverride;
				}
				else
				{
					containerPlayMarker = new ContainerPlayMarker();
					containerPlayMarker.MediaType = mediaType;
				}
				LibraryPlaybackTrack value = new LibraryPlaybackTrack((int)uniqueIds[i], MediaType.Track, containerPlayMarker);
				tracks.Add(value);
			}
			val4.Release();
			val4.Dispose();
			return;
		}
		bool flag = CanFastAddList(items);
		ArrayList arrayList = null;
		int num3 = count;
		bool flag2 = false;
		if (flag)
		{
			arrayList = GetUniqueIdsFromList(items);
			if (arrayList != null)
			{
				num3 = arrayList.Count;
			}
			else
			{
				flag = false;
			}
		}
		for (int j = 0; j < num3; j++)
		{
			if (j == num)
			{
				startIndex = tracks.Count;
			}
			if (flag)
			{
				AddLibraryDataProviderItemToTrackList((int)arrayList[j], MediaType.Track, tracks, allowVideo, allowPictures, containerPlayMarkerOverride);
				continue;
			}
			object obj2 = items[j];
			if (obj2 != null)
			{
				if (obj2 is LibraryDataProviderItemBase)
				{
					int libraryId = -1;
					EMediaTypes val5 = (EMediaTypes)(-1);
					LibraryDataProviderItemBase val6 = (LibraryDataProviderItemBase)((obj2 is LibraryDataProviderItemBase) ? obj2 : null);
					val6.GetMediaIdAndType(ref libraryId, ref val5);
					AddLibraryDataProviderItemToTrackList(libraryId, (MediaType)val5, tracks, allowVideo, allowPictures, containerPlayMarkerOverride);
				}
				else if (obj2 is SubscriptionDataProviderItem)
				{
					AddSubscriptionDataProviderItemToTrackList((SubscriptionDataProviderItem)obj2, tracks, allowVideo, allowPictures);
				}
				else if (obj2 is DataProviderObject)
				{
					bool blockedExplicitContent = false;
					AddDataProviderObjectToTrackList((DataProviderObject)obj2, tracks, materializeMarketplaceTracks, containerPlayMarkerOverride, out blockedExplicitContent);
					flag2 = flag2 || blockedExplicitContent;
				}
				else if (obj2 is FileEntry)
				{
					AddFileEntryToTrackList((FileEntry)obj2, tracks, allowVideo, allowPictures, materializeMarketplaceTracks, containerPlayMarkerOverride);
				}
				else if (obj2 is LibraryPlaybackTrack || obj2 is VideoPlaybackTrack)
				{
					AddLibraryOrVideoPlaybackTrackToTrackList(obj2, tracks);
				}
				else if (obj2 is MarketplacePlaybackTrack)
				{
					AddMarketplacePlaybackTrackToTrackList((MarketplacePlaybackTrack)obj2, tracks, materializeMarketplaceTracks);
				}
				else if (obj2 is QuickMixItem)
				{
					AddQuickMixItemToTrackList((QuickMixItem)obj2, tracks, containerPlayMarkerOverride);
				}
			}
		}
		if (flag2)
		{
			if (SignIn.Instance.SignedIn)
			{
				MessageBox.Show(Shell.LoadString(StringId.IDS_ExplicitErrorHeading), Shell.LoadString(StringId.IDS_ExplicitNeedAdult), (EventHandler)null);
			}
			else
			{
				MessageBox.Show(Shell.LoadString(StringId.IDS_ExplicitErrorHeading), Shell.LoadString(StringId.IDS_ExplicitMustLogin), (EventHandler)null);
			}
		}
	}

	private static bool CanFastAddList(IList list)
	{
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		bool result = false;
		if (list != null && list is LibraryVirtualList && list[0] is LibraryDataProviderItemBase && ((DataProviderObject)(LibraryDataProviderItemBase)list[0]).TypeName == "Track")
		{
			result = true;
		}
		return result;
	}

	private static ArrayList GetUniqueIdsFromList(IList list)
	{
		ArrayList result = null;
		LibraryVirtualList val = (LibraryVirtualList)((list is LibraryVirtualList) ? list : null);
		if (val != null)
		{
			result = val.GetUniqueIds();
		}
		return result;
	}

	private static void AddLibraryDataProviderItemToTrackList(int libraryId, MediaType mediaType, IList tracks, bool allowVideo, bool allowPictures, ContainerPlayMarker containerPlayMarkerOverride)
	{
		if (libraryId == -1)
		{
			return;
		}
		switch (mediaType)
		{
		case MediaType.Track:
		case MediaType.Video:
		case MediaType.Photo:
		case MediaType.PodcastEpisode:
		{
			if (!CanEnqueue(libraryId, mediaType, allowVideo, allowPictures))
			{
				break;
			}
			ContainerPlayMarker containerPlayMarker2 = null;
			if (mediaType == MediaType.Track)
			{
				if (containerPlayMarkerOverride != null)
				{
					containerPlayMarker2 = containerPlayMarkerOverride;
				}
				else
				{
					containerPlayMarker2 = new ContainerPlayMarker();
					containerPlayMarker2.MediaType = MediaType.Album;
					containerPlayMarker2.LibraryId = -1;
				}
			}
			tracks.Add(new LibraryPlaybackTrack(libraryId, mediaType, containerPlayMarker2));
			break;
		}
		case MediaType.Playlist:
		{
			ZuneQueryList val = null;
			val = ZuneApplication.ZuneLibrary.GetTracksByPlaylist(0, libraryId, (EQuerySortType)1, 437u);
			val.AddRef();
			ContainerPlayMarker containerPlayMarker = new ContainerPlayMarker();
			containerPlayMarker.LibraryId = libraryId;
			containerPlayMarker.MediaType = MediaType.Playlist;
			containerPlayMarker.PlaylistType = (PlaylistType)GetPlaylistType(libraryId);
			containerPlayMarker.PlaylistSubType = GetSubType(libraryId);
			int count = val.Count;
			for (uint num = 0u; num < count; num++)
			{
				int num2 = (int)val.GetFieldValue(num, typeof(int), 233u, s_invalidMediaId);
				int mediaType2 = (int)val.GetFieldValue(num, typeof(int), 234u, s_invalidMediaType);
				if (num2 != -1)
				{
					tracks.Add(new LibraryPlaybackTrack(num2, (MediaType)mediaType2, containerPlayMarker));
				}
			}
			val.Release();
			val.Dispose();
			break;
		}
		default:
			_ = 20;
			break;
		}
	}

	private static void AddSubscriptionDataProviderItemToTrackList(SubscriptionDataProviderItem podcastEpisode, IList tracks, bool allowVideo, bool allowPictures)
	{
		int mediaId = (int)((DataProviderObject)podcastEpisode).GetProperty("LibraryId");
		if (CanEnqueue(mediaId, MediaType.PodcastEpisode, allowVideo, allowPictures))
		{
			tracks.Add(new LibraryPlaybackTrack(mediaId, MediaType.PodcastEpisode, null));
		}
	}

	private static void AddDataProviderObjectToTrackList(DataProviderObject dpItem, IList tracks, bool materializeMarketplaceTracks, ContainerPlayMarker containerPlayMarkerOverride, out bool blockedExplicitContent)
	{
		blockedExplicitContent = false;
		if (dpItem == null)
		{
			return;
		}
		if (dpItem is Track)
		{
			Track track = (Track)(object)dpItem;
			int num = -1;
			if (track.IsParentallyBlocked && !track.InCollection)
			{
				blockedExplicitContent = true;
				return;
			}
			bool flag = default(bool);
			if (!ZuneApplication.Service.InCompleteCollection(track.Id, (EContentType)0, ref num, ref flag) && materializeMarketplaceTracks && (track.IsDownloading || track.CanDownload || track.CanPurchase))
			{
				num = ZuneApplication.ZuneLibrary.AddTrack(track.Id, track.AlbumId, track.TrackNumber, track.Title, track.Duration, track.AlbumTitle, track.Artist, track.PrimaryGenre.Title);
			}
			if (num >= 0)
			{
				tracks.Add(new LibraryPlaybackTrack(num, MediaType.Track, null));
			}
			else if (track.CanPlay)
			{
				tracks.Add(new MarketplacePlaybackTrack(track.CanSubscriptionPlay, track.Id, track.Title, track.Duration, track.AlbumTitle, track.Artist, track.TrackNumber, track.PrimaryGenre.Title, track.AlbumId, track.ReferrerContext));
			}
		}
		else if (dpItem is Video)
		{
			Video video = (Video)(object)dpItem;
			if (video.IsParentallyBlocked && !video.InCollection)
			{
				blockedExplicitContent = true;
			}
			else
			{
				tracks.Add(new VideoPlaybackTrack(video.Id, video.Title, null, null, isDownloading: false, isStreaming: false, ignoreCollection: false, fallbackToPreview: true, forcePreview: false, VideoDefinitionEnum.None));
			}
		}
		else if (dpItem.TypeName == "RadioStation")
		{
			tracks.Add(new StreamingRadioPlaybackTrack((string)dpItem.GetProperty("SourceURL"), (string)dpItem.GetProperty("Title"), MediaType.Track));
		}
	}

	private static void AddFileEntryToTrackList(FileEntry file, IList tracks, bool allowVideo, bool allowPictures, bool materializeMarketplaceTracks, ContainerPlayMarker containerPlayMarkerOverride)
	{
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Expected I4, but got Unknown
		//IL_00f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0102: Expected I4, but got Unknown
		//IL_007f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0085: Invalid comparison between Unknown and I4
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_0061: Expected I4, but got Unknown
		//IL_0069: Unknown result type (might be due to invalid IL or missing references)
		//IL_0074: Expected I4, but got Unknown
		if (file == null)
		{
			return;
		}
		int dbMediaId;
		bool fFileAlreadyExists;
		bool fTimedout;
		bool flag = AddTransientMediaTask.AddTransientMediaWithTimeout(file.Path, (MediaType)file.MediaType, TimeSpan.FromSeconds((double)ClientConfiguration.GeneralSettings.AccessMediaHangTimeoutSec), out dbMediaId, out fFileAlreadyExists, out fTimedout);
		if (fTimedout)
		{
			MessageBox.Show(Shell.LoadString(StringId.IDS_GENERIC_ERROR), Shell.LoadString(StringId.IDS_PLAYLIST_MEDIA_NOTACCESSIBLE), (EventHandler)null);
			return;
		}
		if (flag && !fFileAlreadyExists)
		{
			if (CanEnqueue(dbMediaId, (MediaType)file.MediaType, allowVideo, allowPictures))
			{
				tracks.Add(new LibraryPlaybackTrack(dbMediaId, (MediaType)file.MediaType, null));
			}
			return;
		}
		bool flag2 = false;
		if ((int)file.MediaType == 4 && fFileAlreadyExists)
		{
			MediaType type = MediaType.Undefined;
			Guid zuneMediaId = Guid.Empty;
			string title = string.Empty;
			bool isHD = false;
			GetVideoValues(dbMediaId, out type, out zuneMediaId, out title, out isHD);
			if (type == MediaType.VideoMBR)
			{
				tracks.Add(new VideoPlaybackTrack(zuneMediaId, title, null, null, isDownloading: false, isStreaming: true, ignoreCollection: false, fallbackToPreview: false, forcePreview: false, isHD ? VideoDefinitionEnum.HD : VideoDefinitionEnum.SD));
				flag2 = true;
			}
		}
		if (!flag2)
		{
			string text = file.Path;
			try
			{
				text = Path.GetFileName(text);
			}
			catch (ArgumentException)
			{
			}
			tracks.Add(new StreamingPlaybackTrack(file.Path, text, (MediaType)file.MediaType));
		}
	}

	private static void AddLibraryOrVideoPlaybackTrackToTrackList(object item, IList tracks)
	{
		tracks.Add(item);
	}

	private static void AddMarketplacePlaybackTrackToTrackList(MarketplacePlaybackTrack marketplacePlaybackTrack, IList tracks, bool materializeMarketplaceTracks)
	{
		if (marketplacePlaybackTrack != null)
		{
			int num = default(int);
			bool flag = default(bool);
			if (!ZuneApplication.Service.InCompleteCollection(marketplacePlaybackTrack.ZuneMediaId, (EContentType)0, ref num, ref flag))
			{
				num = ZuneApplication.ZuneLibrary.AddTrack(marketplacePlaybackTrack.ZuneMediaId, marketplacePlaybackTrack.AlbumId, marketplacePlaybackTrack.TrackNumber, marketplacePlaybackTrack.Title, marketplacePlaybackTrack.Duration, marketplacePlaybackTrack.Album, marketplacePlaybackTrack.Artist, marketplacePlaybackTrack.Genre);
			}
			if (num >= 0)
			{
				tracks.Add(new LibraryPlaybackTrack(num, MediaType.Track, null));
			}
		}
	}

	private static void AddQuickMixItemToTrackList(QuickMixItem quickMixItem, IList tracks, ContainerPlayMarker containerPlayMarkerOverride)
	{
		if (quickMixItem != null)
		{
			tracks.Add(new LibraryPlaybackTrack(quickMixItem.MediaId, MediaType.Track, containerPlayMarkerOverride));
		}
	}

	public static T GetFieldValue<T>(int mediaId, EListType listType, int atom, T defaultValue)
	{
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		int[] array = new int[1] { atom };
		object[] array2 = new object[1] { defaultValue };
		ZuneLibrary.GetFieldValues(mediaId, listType, 1, array, array2, Instance.QueryContext);
		return (T)array2[0];
	}

	public static void SetFieldValue<T>(int mediaId, EListType listType, int atom, T value)
	{
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		int[] array = new int[1] { atom };
		object[] array2 = new object[1] { value };
		ZuneLibrary.SetFieldValues(mediaId, listType, 1, array, array2, Instance.QueryContext);
	}

	public static bool IsVideo(int mediaId, MediaType mediaType)
	{
		switch (mediaType)
		{
		case MediaType.PodcastEpisode:
			if (GetFieldValue(mediaId, (EListType)7, 161, MediaType.Undefined) == MediaType.Video)
			{
				return true;
			}
			break;
		case MediaType.Video:
			return true;
		}
		return false;
	}

	public static void GetVideoValues(int dbMediaId, out MediaType type, out Guid zuneMediaId, out string title, out bool isHD)
	{
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		int[] array = new int[4] { 177, 451, 344, 440 };
		object[] array2 = new object[4]
		{
			MediaType.Undefined,
			Guid.Empty,
			string.Empty,
			VideoDefinition.Unknown
		};
		ZuneLibrary.GetFieldValues(dbMediaId, (EListType)4, array.Length, array, array2, Instance.QueryContext);
		type = (MediaType)array2[0];
		zuneMediaId = (Guid)array2[1];
		title = (string)array2[2];
		isHD = VideoDefinitionHelper.IsHD((VideoDefinition)array2[3]);
	}

	public static bool CanEnqueue(int mediaId, MediaType mediaType, bool allowVideo, bool allowPictures)
	{
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0013: Invalid comparison between Unknown and I4
		switch (mediaType)
		{
		case MediaType.PodcastEpisode:
			if ((int)GetFieldValue<EItemDownloadState>(mediaId, (EListType)7, 145, (EItemDownloadState)0) != 3)
			{
				return false;
			}
			break;
		case MediaType.Photo:
			return allowPictures;
		}
		if (!allowVideo)
		{
			return !IsVideo(mediaId, mediaType);
		}
		return true;
	}

	public static bool IsInCollection(int mediaId, MediaType mediaType)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_000a: Invalid comparison between Unknown and I4
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		EListType val = MediaTypeToListType(mediaType);
		if ((int)val == 23)
		{
			return false;
		}
		string fieldValue = GetFieldValue<string>(mediaId, val, 317, null);
		bool result = false;
		if (!string.IsNullOrEmpty(fieldValue) && !fieldValue.StartsWith("zunecd://"))
		{
			result = true;
		}
		return result;
	}

	public static bool IsInVisibleCollection(int mediaId, MediaType mediaType)
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0010: Invalid comparison between Unknown and I4
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		bool result = false;
		if (mediaId != -1)
		{
			EListType val = MediaTypeToListType(mediaType);
			if ((int)val != 23)
			{
				result = GetFieldValue(mediaId, val, 204, defaultValue: false);
			}
		}
		return result;
	}

	public static EListType MediaTypeToListType(MediaType mediaType)
	{
		return (EListType)(mediaType switch
		{
			MediaType.Track => 2, 
			MediaType.Video => 4, 
			MediaType.Podcast => 6, 
			MediaType.PodcastEpisode => 7, 
			MediaType.Photo => 3, 
			MediaType.Album => 1, 
			MediaType.Artist => 0, 
			MediaType.Genre => 15, 
			MediaType.Playlist => 12, 
			_ => 23, 
		});
	}

	public static int GetPlaylistId(int contentItemId)
	{
		return GetFieldValue(contentItemId, (EListType)13, 263, -1);
	}

	public static List<string> SplitAutoPlaylistValue(string value)
	{
		List<string> list = null;
		if (value != null)
		{
			if (value.IndexOf(';') >= 0)
			{
				string[] array = value.Split(s_autoPlaylistSplitChars, StringSplitOptions.RemoveEmptyEntries);
				string[] array2 = array;
				foreach (string text in array2)
				{
					string text2 = text.Trim();
					if (text2.Length > 0)
					{
						if (list == null)
						{
							list = new List<string>(array.Length);
						}
						list.Add(text2);
					}
				}
			}
			else
			{
				value = value.Trim();
				if (value.Length > 0)
				{
					list = new List<string>(1);
					list.Add(value);
				}
			}
		}
		return list;
	}

	[Conditional("DEBUG_PLAYLIST")]
	private static void _DEBUG_Trace(string message, params object[] args)
	{
	}
}
