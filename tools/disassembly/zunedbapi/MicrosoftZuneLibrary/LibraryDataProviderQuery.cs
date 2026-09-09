using System;
using System.Collections;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Microsoft.Iris;
using Microsoft.Zune.Playlist;
using MicrosoftZuneInterop;

namespace MicrosoftZuneLibrary;

internal class LibraryDataProviderQuery : DataProviderQuery
{
	protected LibraryVirtualList m_virtualListResultSet;

	private bool m_disposed = false;

	private int m_requestGeneration;

	private string m_thumbnailFallbackImageUrl;

	private static WorkerQueue m_libQueriesQueue = WorkerQueue.CreateInstance();

	public string ThumbnailFallbackImageUrl => m_thumbnailFallbackImageUrl;

	internal LibraryDataProviderQuery(object queryTypeCookie)
		: base(queryTypeCookie)
	{
		LibraryDataProviderQueryResult libraryDataProviderQueryResult = new LibraryDataProviderQueryResult(this, null, ((DataProviderQuery)this).ResultTypeCookie);
		libraryDataProviderQueryResult.SetIsEmpty(isEmpty: true);
		((DataProviderQuery)this).Result = libraryDataProviderQueryResult;
	}

	[return: MarshalAs(UnmanagedType.U1)]
	public bool GetSortAttributes(out string[] sorts, out bool[] ascendings)
	{
		return LibraryDataProvider.GetSortAttributes((string)((DataProviderQuery)this).GetProperty("Sort"), out sorts, out ascendings);
	}

	protected unsafe override void BeginExecute()
	{
		if (m_disposed)
		{
			return;
		}
		if ((uint)Unsafe.As<EtwControlerState, byte>(ref Unsafe.AddByteOffset(ref global::_003CModule_003E.g_EtwControlerState, 8)) > 1u && (Unsafe.As<EtwControlerState, int>(ref Unsafe.AddByteOffset(ref global::_003CModule_003E.g_EtwControlerState, 4)) & 0x10) != 0)
		{
			fixed (ushort* pwszDetail = &Unsafe.As<char, ushort>(ref global::_003CModule_003E.PtrToStringChars(((DataProviderQuery)this).ToString())))
			{
				try
				{
					global::_003CModule_003E.PERFTRACE_COLLECTIONEVENT((_COLLECTION_EVENT)28, pwszDetail);
				}
				catch
				{
					//try-fault
					pwszDetail = null;
					throw;
				}
			}
		}
		LibraryVirtualList virtualListResultSet = m_virtualListResultSet;
		if (virtualListResultSet != null)
		{
			((IDisposable)virtualListResultSet).Dispose();
			m_virtualListResultSet = null;
		}
		m_thumbnailFallbackImageUrl = null;
		m_requestGeneration++;
		((DataProviderQuery)this).Status = (DataProviderQueryStatus)1;
		m_libQueriesQueue.QueueSequentialWorkItem(BeginExecuteWorker, m_requestGeneration);
	}

	protected override void OnDispose()
	{
		m_disposed = true;
		LibraryVirtualList virtualListResultSet = m_virtualListResultSet;
		if (virtualListResultSet != null)
		{
			((IDisposable)virtualListResultSet).Dispose();
			m_virtualListResultSet = null;
		}
	}

	private unsafe void BeginExecuteWorker(object state)
	{
		//IL_0c02: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c17: Expected O, but got Unknown
		bool[] ascendings = null;
		string[] sorts = null;
		string text = null;
		int num = (int)state;
		if (num != m_requestGeneration)
		{
			return;
		}
		QueryPropertyBag queryPropertyBag = new QueryPropertyBag();
		IQueryPropertyBag* iQueryPropertyBag = queryPropertyBag.GetIQueryPropertyBag();
		bool retainedList = false;
		if (GetSortAttributes(out sorts, out ascendings))
		{
			IMultiSortAttributes* ptr = queryPropertyBag.PackMultiSortAttributes(sorts, ascendings);
			((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, EQueryPropertyBagProp, IMultiSortAttributes*, int>)(int)(*(uint*)(*(int*)iQueryPropertyBag + 16)))((nint)iQueryPropertyBag, (EQueryPropertyBagProp)23, ptr);
			if (ptr != null)
			{
				((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint>)(int)(*(uint*)(*(int*)ptr + 8)))((nint)ptr);
			}
		}
		object property = ((DataProviderQuery)this).GetProperty("ArtistIds");
		if (property != null)
		{
			int num2 = *(int*)iQueryPropertyBag + 12;
			((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, EQueryPropertyBagProp, IDList*, int>)(int)(*(uint*)num2))((nint)iQueryPropertyBag, (EQueryPropertyBagProp)4, queryPropertyBag.PackIDList((IList)property));
		}
		object property2 = ((DataProviderQuery)this).GetProperty("GenreIds");
		if (property2 != null)
		{
			int num3 = *(int*)iQueryPropertyBag + 12;
			((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, EQueryPropertyBagProp, IDList*, int>)(int)(*(uint*)num3))((nint)iQueryPropertyBag, (EQueryPropertyBagProp)12, queryPropertyBag.PackIDList((IList)property2));
		}
		object property3 = ((DataProviderQuery)this).GetProperty("AlbumIds");
		if (property3 != null)
		{
			int num4 = *(int*)iQueryPropertyBag + 12;
			((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, EQueryPropertyBagProp, IDList*, int>)(int)(*(uint*)num4))((nint)iQueryPropertyBag, (EQueryPropertyBagProp)7, queryPropertyBag.PackIDList((IList)property3));
		}
		object property4 = ((DataProviderQuery)this).GetProperty("UserCardIds");
		if (property4 != null)
		{
			int num5 = *(int*)iQueryPropertyBag + 12;
			((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, EQueryPropertyBagProp, IDList*, int>)(int)(*(uint*)num5))((nint)iQueryPropertyBag, (EQueryPropertyBagProp)30, queryPropertyBag.PackIDList((IList)property4));
		}
		object property5 = ((DataProviderQuery)this).GetProperty("DeviceId");
		if (property5 != null)
		{
			((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, EQueryPropertyBagProp, int, int>)(int)(*(uint*)(*(int*)iQueryPropertyBag + 28)))((nint)iQueryPropertyBag, (EQueryPropertyBagProp)1, (int)property5);
		}
		else
		{
			((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, EQueryPropertyBagProp, int, int>)(int)(*(uint*)(*(int*)iQueryPropertyBag + 28)))((nint)iQueryPropertyBag, (EQueryPropertyBagProp)1, 1);
		}
		object property6 = ((DataProviderQuery)this).GetProperty("SyncMappedError");
		if (property6 != null)
		{
			((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, EQueryPropertyBagProp, int, int>)(int)(*(uint*)(*(int*)iQueryPropertyBag + 28)))((nint)iQueryPropertyBag, (EQueryPropertyBagProp)18, (int)property6);
		}
		property6 = ((DataProviderQuery)this).GetProperty("UserId");
		if (property6 != null)
		{
			((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, EQueryPropertyBagProp, int, int>)(int)(*(uint*)(*(int*)iQueryPropertyBag + 28)))((nint)iQueryPropertyBag, (EQueryPropertyBagProp)0, (int)property6);
		}
		else
		{
			((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, EQueryPropertyBagProp, int, int>)(int)(*(uint*)(*(int*)iQueryPropertyBag + 28)))((nint)iQueryPropertyBag, (EQueryPropertyBagProp)0, 1);
		}
		object property7 = ((DataProviderQuery)this).GetProperty("InLibrary");
		if (property7 != null)
		{
			((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, EQueryPropertyBagProp, int, int>)(int)(*(uint*)(*(int*)iQueryPropertyBag + 28)))((nint)iQueryPropertyBag, (EQueryPropertyBagProp)26, (int)property7);
		}
		((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, EQueryPropertyBagProp, int, int>)(int)(*(uint*)(*(int*)iQueryPropertyBag + 28)))((nint)iQueryPropertyBag, (EQueryPropertyBagProp)2, 0);
		EQueryTypeView eQueryTypeView = EQueryTypeView.eQueryTypeLibraryView;
		object property8 = ((DataProviderQuery)this).GetProperty("ShowDeviceContents");
		if (property8 != null && (bool)property8)
		{
			eQueryTypeView = EQueryTypeView.eQueryTypeDeviceView;
		}
		property8 = ((DataProviderQuery)this).GetProperty("DiscMediaView");
		if (property8 != null && (bool)property8)
		{
			eQueryTypeView = EQueryTypeView.eQueryTypeDiscMediaView;
		}
		property8 = ((DataProviderQuery)this).GetProperty("Remaining");
		if (property8 != null && (bool)property8)
		{
			eQueryTypeView = EQueryTypeView.eQueryTypeSyncRemaining;
		}
		property8 = ((DataProviderQuery)this).GetProperty("Complete");
		if (property8 != null && (bool)property8)
		{
			eQueryTypeView = EQueryTypeView.eQueryTypeSyncSucceeded;
		}
		property8 = ((DataProviderQuery)this).GetProperty("Failed");
		if (property8 != null && (bool)property8)
		{
			eQueryTypeView = EQueryTypeView.eQueryTypeSyncFailed;
		}
		property8 = ((DataProviderQuery)this).GetProperty("MultiSelect");
		if (property8 != null && (bool)property8)
		{
			eQueryTypeView = ((EQueryTypeView.eQueryTypeDeviceView == eQueryTypeView) ? EQueryTypeView.eQueryTypeDeviceMultiSelectView : EQueryTypeView.eQueryTypeLibraryMultiSelectView);
		}
		object property9 = ((DataProviderQuery)this).GetProperty("RulesOnly");
		if (property9 != null && (bool)property9)
		{
			eQueryTypeView = EQueryTypeView.eQueryTypeDeviceSyncRuleView;
		}
		((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, EQueryPropertyBagProp, int, int>)(int)(*(uint*)(*(int*)iQueryPropertyBag + 28)))((nint)iQueryPropertyBag, (EQueryPropertyBagProp)15, (int)eQueryTypeView);
		object property10 = ((DataProviderQuery)this).GetProperty("Keywords");
		if (property10 != null)
		{
			fixed (ushort* ptr2 = &Unsafe.As<char, ushort>(ref global::_003CModule_003E.PtrToStringChars((string)property10)))
			{
				try
				{
					int num6 = *(int*)iQueryPropertyBag + 20;
					((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, EQueryPropertyBagProp, ushort*, int>)(int)(*(uint*)num6))((nint)iQueryPropertyBag, (EQueryPropertyBagProp)19, ptr2);
				}
				catch
				{
					//try-fault
					ptr2 = null;
					throw;
				}
			}
		}
		object property11 = ((DataProviderQuery)this).GetProperty("ContributingArtistId");
		if (property11 != null)
		{
			((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, EQueryPropertyBagProp, int, int>)(int)(*(uint*)(*(int*)iQueryPropertyBag + 28)))((nint)iQueryPropertyBag, (EQueryPropertyBagProp)5, (int)property11);
		}
		property11 = ((DataProviderQuery)this).GetProperty("ArtistId");
		if (property11 != null)
		{
			((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, EQueryPropertyBagProp, int, int>)(int)(*(uint*)(*(int*)iQueryPropertyBag + 28)))((nint)iQueryPropertyBag, (EQueryPropertyBagProp)3, (int)property11);
		}
		property11 = ((DataProviderQuery)this).GetProperty("GenreId");
		if (property11 != null)
		{
			((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, EQueryPropertyBagProp, int, int>)(int)(*(uint*)(*(int*)iQueryPropertyBag + 28)))((nint)iQueryPropertyBag, (EQueryPropertyBagProp)11, (int)property11);
		}
		property11 = ((DataProviderQuery)this).GetProperty("AlbumId");
		if (property11 != null)
		{
			((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, EQueryPropertyBagProp, int, int>)(int)(*(uint*)(*(int*)iQueryPropertyBag + 28)))((nint)iQueryPropertyBag, (EQueryPropertyBagProp)6, (int)property11);
		}
		property11 = ((DataProviderQuery)this).GetProperty("FolderId");
		if (property11 != null)
		{
			((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, EQueryPropertyBagProp, int, int>)(int)(*(uint*)(*(int*)iQueryPropertyBag + 28)))((nint)iQueryPropertyBag, (EQueryPropertyBagProp)9, (int)property11);
		}
		property11 = ((DataProviderQuery)this).GetProperty("RecurseIntoFolders");
		if (property11 != null && (int)property11 != 0)
		{
			((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, EQueryPropertyBagProp, int, int>)(int)(*(uint*)(*(int*)iQueryPropertyBag + 28)))((nint)iQueryPropertyBag, (EQueryPropertyBagProp)36, 1);
		}
		object property12 = ((DataProviderQuery)this).GetProperty("FolderMediaType");
		if (property12 != null)
		{
			EMediaTypes eMediaTypes = LibraryDataProvider.NameToMediaType((string)property12);
			((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, EQueryPropertyBagProp, int, int>)(int)(*(uint*)(*(int*)iQueryPropertyBag + 28)))((nint)iQueryPropertyBag, (EQueryPropertyBagProp)13, (int)eMediaTypes);
		}
		object property13 = ((DataProviderQuery)this).GetProperty("MediaType");
		if (property13 != null)
		{
			((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, EQueryPropertyBagProp, int, int>)(int)(*(uint*)(*(int*)iQueryPropertyBag + 28)))((nint)iQueryPropertyBag, (EQueryPropertyBagProp)13, (int)property13);
		}
		property13 = ((DataProviderQuery)this).GetProperty("TOC");
		if (property13 != null)
		{
			fixed (ushort* ptr3 = &Unsafe.As<char, ushort>(ref global::_003CModule_003E.PtrToStringChars((string)property13)))
			{
				try
				{
					int num7 = *(int*)iQueryPropertyBag + 20;
					((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, EQueryPropertyBagProp, ushort*, int>)(int)(*(uint*)num7))((nint)iQueryPropertyBag, (EQueryPropertyBagProp)20, ptr3);
				}
				catch
				{
					//try-fault
					ptr3 = null;
					throw;
				}
			}
		}
		object property14 = ((DataProviderQuery)this).GetProperty("SeriesId");
		if (property14 != null)
		{
			((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, EQueryPropertyBagProp, int, int>)(int)(*(uint*)(*(int*)iQueryPropertyBag + 28)))((nint)iQueryPropertyBag, (EQueryPropertyBagProp)8, (int)property14);
		}
		property14 = ((DataProviderQuery)this).GetProperty("WatchType");
		if (property14 != null)
		{
			((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, EQueryPropertyBagProp, int, int>)(int)(*(uint*)(*(int*)iQueryPropertyBag + 28)))((nint)iQueryPropertyBag, (EQueryPropertyBagProp)32, (int)property14);
		}
		if (((DataProviderQuery)this).GetProperty("ExpiresOnly") != null)
		{
			((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, EQueryPropertyBagProp, int, int>)(int)(*(uint*)(*(int*)iQueryPropertyBag + 28)))((nint)iQueryPropertyBag, (EQueryPropertyBagProp)33, 1);
		}
		object property15 = ((DataProviderQuery)this).GetProperty("Operation");
		if (property15 != null)
		{
			((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, EQueryPropertyBagProp, int, int>)(int)(*(uint*)(*(int*)iQueryPropertyBag + 28)))((nint)iQueryPropertyBag, (EQueryPropertyBagProp)16, (int)property15);
		}
		property15 = ((DataProviderQuery)this).GetProperty("InitTime");
		if (property15 != null)
		{
			fixed (ushort* ptr4 = &Unsafe.As<char, ushort>(ref global::_003CModule_003E.PtrToStringChars((string)property15)))
			{
				try
				{
					int num8 = *(int*)iQueryPropertyBag + 20;
					((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, EQueryPropertyBagProp, ushort*, int>)(int)(*(uint*)num8))((nint)iQueryPropertyBag, (EQueryPropertyBagProp)17, ptr4);
				}
				catch
				{
					//try-fault
					ptr4 = null;
					throw;
				}
			}
		}
		object property16 = ((DataProviderQuery)this).GetProperty("PlaylistId");
		if (property16 != null)
		{
			((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, EQueryPropertyBagProp, int, int>)(int)(*(uint*)(*(int*)iQueryPropertyBag + 28)))((nint)iQueryPropertyBag, (EQueryPropertyBagProp)10, (int)property16);
		}
		property16 = ((DataProviderQuery)this).GetProperty("CategoryId");
		if (property16 != null)
		{
			((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, EQueryPropertyBagProp, int, int>)(int)(*(uint*)(*(int*)iQueryPropertyBag + 28)))((nint)iQueryPropertyBag, (EQueryPropertyBagProp)27, (int)property16);
		}
		property16 = ((DataProviderQuery)this).GetProperty("PlaylistType");
		if (property16 != null && !string.IsNullOrEmpty((string)property16))
		{
			PlaylistType playlistType = (PlaylistType)Enum.Parse(typeof(PlaylistType), (string)property16);
			((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, EQueryPropertyBagProp, int, int>)(int)(*(uint*)(*(int*)iQueryPropertyBag + 28)))((nint)iQueryPropertyBag, (EQueryPropertyBagProp)24, (int)playlistType);
		}
		object property17 = ((DataProviderQuery)this).GetProperty("PlaylistTypeMask");
		if (property17 != null)
		{
			int num9 = (int)property17;
			if (num9 != 0)
			{
				((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, EQueryPropertyBagProp, int, int>)(int)(*(uint*)(*(int*)iQueryPropertyBag + 28)))((nint)iQueryPropertyBag, (EQueryPropertyBagProp)25, num9);
			}
		}
		object property18 = ((DataProviderQuery)this).GetProperty("MaxResultCount");
		if (property18 != null)
		{
			((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, EQueryPropertyBagProp, int, int>)(int)(*(uint*)(*(int*)iQueryPropertyBag + 28)))((nint)iQueryPropertyBag, (EQueryPropertyBagProp)31, (int)property18);
		}
		property18 = ((DataProviderQuery)this).GetProperty("DrmStateMask");
		if (property18 != null)
		{
			ulong num10 = (ulong)(long)property18;
			if (num10 != 0)
			{
				((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, EQueryPropertyBagProp, ulong, int>)(int)(*(uint*)(*(int*)iQueryPropertyBag + 24)))((nint)iQueryPropertyBag, (EQueryPropertyBagProp)34, num10);
			}
		}
		string text2 = (string)((DataProviderQuery)this).GetProperty("QueryType");
		EQueryType eQueryType = EQueryType.eQueryTypeInvalid;
		if (queryPropertyBag.IsSet("Keywords"))
		{
			switch (text2)
			{
			case "Artist":
				eQueryType = EQueryType.eQueryTypeArtistsWithKeyword;
				break;
			case "Album":
				eQueryType = EQueryType.eQueryTypeAlbumsWithKeyword;
				break;
			case "Track":
				eQueryType = EQueryType.eQueryTypeTracksWithKeyword;
				break;
			case "Playlist":
				eQueryType = EQueryType.eQueryTypePlaylistsWithKeyword;
				break;
			case "Photo":
				eQueryType = EQueryType.eQueryTypePhotosWithKeyword;
				break;
			case "PodcastSeries":
				eQueryType = EQueryType.eQueryTypeSubscriptionsSeriesWithKeyword;
				break;
			case "PodcastEpisode":
				eQueryType = EQueryType.eQueryTypeSubscriptionsEpisodesWithKeyword;
				break;
			case "Video":
				eQueryType = EQueryType.eQueryTypeVideoWithKeyword;
				break;
			}
		}
		else
		{
			switch (text2)
			{
			case "Artist":
				eQueryType = EQueryType.eQueryTypeAllAlbumArtists;
				break;
			case "Genres":
			{
				object property19 = ((DataProviderQuery)this).GetProperty("MediaType");
				int num11 = ((property19 == null) ? 3 : ((int)property19));
				((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, EQueryPropertyBagProp, int, int>)(int)(*(uint*)(*(int*)iQueryPropertyBag + 28)))((nint)iQueryPropertyBag, (EQueryPropertyBagProp)13, num11);
				eQueryType = EQueryType.eQueryTypeAllGenres;
				break;
			}
			case "Album":
				property18 = ((DataProviderQuery)this).GetProperty("ArtistId");
				if ((property18 != null && (int)property18 != -1) || queryPropertyBag.IsSet("ArtistIds"))
				{
					eQueryType = EQueryType.eQueryTypeAlbumsForAlbumArtistId;
				}
				else
				{
					property18 = ((DataProviderQuery)this).GetProperty("GenreId");
					eQueryType = (((property18 == null || (int)property18 == -1) && !queryPropertyBag.IsSet("GenreIds")) ? EQueryType.eQueryTypeAllAlbums : EQueryType.eQueryTypeAlbumsByGenreId);
				}
				retainedList = true;
				break;
			case "Track":
			{
				object property20 = ((DataProviderQuery)this).GetProperty("RulesOnly");
				if (property20 != null && (bool)property20)
				{
					eQueryType = EQueryType.eQueryTypeAllTracks;
					break;
				}
				object property21 = ((DataProviderQuery)this).GetProperty("AlbumId");
				if ((property21 != null && (int)property21 != -1) || queryPropertyBag.IsSet("AlbumIds"))
				{
					property18 = ((DataProviderQuery)this).GetProperty("ArtistId");
					eQueryType = (((property18 == null || (int)property18 == -1) && !queryPropertyBag.IsSet("ArtistIds")) ? EQueryType.eQueryTypeTracksForAlbumId : EQueryType.eQueryTypeTracksForAlbumArtistId);
					break;
				}
				property18 = ((DataProviderQuery)this).GetProperty("ArtistId");
				if ((property18 != null && (int)property18 != -1) || queryPropertyBag.IsSet("ArtistIds"))
				{
					eQueryType = EQueryType.eQueryTypeTracksForAlbumArtistId;
					break;
				}
				property18 = ((DataProviderQuery)this).GetProperty("GenreId");
				if ((property18 != null && (int)property18 != -1) || queryPropertyBag.IsSet("GenreIds"))
				{
					eQueryType = EQueryType.eQueryTypeTracksByGenreId;
					break;
				}
				property18 = ((DataProviderQuery)this).GetProperty("Detailed");
				if (property18 != null && (bool)property18)
				{
					eQueryType = EQueryType.eQueryTypeAllTracksDetailed;
					break;
				}
				property18 = ((DataProviderQuery)this).GetProperty("TOC");
				eQueryType = ((property18 != null && !string.IsNullOrEmpty((string)property18)) ? EQueryType.eQueryTypeTracksForTOC : EQueryType.eQueryTypeAllTracks);
				break;
			}
			case "AlbumByTOC":
				eQueryType = EQueryType.eQueryTypeAlbumsByTOC;
				if (!queryPropertyBag.IsSet("TOC"))
				{
					eQueryType = EQueryType.eQueryTypeInvalid;
				}
				break;
			case "Photo":
				eQueryType = ((eQueryTypeView == EQueryTypeView.eQueryTypeDeviceSyncRuleView) ? EQueryType.eQueryTypeAllPhotos : EQueryType.eQueryTypePhotosByFolderId);
				break;
			case "MediaFolder":
				eQueryType = EQueryType.eQueryTypeMediaFolders;
				break;
			case "Video":
				property18 = ((DataProviderQuery)this).GetProperty("CategoryId");
				eQueryType = ((property18 == null || (int)property18 == -1) ? EQueryType.eQueryTypeAllVideos : EQueryType.eQueryTypeVideosByCategoryId);
				break;
			case "PodcastSeries":
				eQueryType = EQueryType.eQueryTypeAllPodcastSeries;
				break;
			case "PodcastEpisode":
				property18 = ((DataProviderQuery)this).GetProperty("SeriesId");
				eQueryType = ((property18 == null || (int)property18 == -1) ? EQueryType.eQueryTypeAllPodcastEpisodes : EQueryType.eQueryTypeEpisodesForSeriesId);
				break;
			case "SyncItem":
				eQueryType = EQueryType.eQueryTypeSyncProgress;
				break;
			case "Playlist":
				eQueryType = EQueryType.eQueryTypeAllPlaylists;
				break;
			case "PlaylistContent":
				eQueryType = EQueryType.eQueryTypePlaylistContentByPlaylistId;
				break;
			case "UserCard":
				eQueryType = EQueryType.eQueryTypeUserCards;
				break;
			case "Person":
			{
				eQueryType = EQueryType.eQueryTypePersonsByTypeId;
				EMediaTypes eMediaTypes2 = EMediaTypes.eMediaTypePersonArtist;
				if ((string)((DataProviderQuery)this).GetProperty("PersonType") == "Composer")
				{
					eMediaTypes2 = EMediaTypes.eMediaTypePersonComposer;
				}
				((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, EQueryPropertyBagProp, int, int>)(int)(*(uint*)(*(int*)iQueryPropertyBag + 28)))((nint)iQueryPropertyBag, (EQueryPropertyBagProp)28, (int)eMediaTypes2);
				break;
			}
			case "ArtistsRanking":
				eQueryType = EQueryType.eQueryTypeArtistsRanking;
				break;
			case "TVSeries":
				eQueryType = EQueryType.eQueryTypeVideoSeriesTitles;
				break;
			case "Pin":
			{
				eQueryType = EQueryType.eQueryTypePinsByPinType;
				EPinType ePinType = EPinType.ePinTypeGeneric;
				property18 = ((DataProviderQuery)this).GetProperty("PinType");
				if (property18 != null)
				{
					ePinType = (EPinType)property18;
				}
				((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, EQueryPropertyBagProp, int, int>)(int)(*(uint*)(*(int*)iQueryPropertyBag + 28)))((nint)iQueryPropertyBag, (EQueryPropertyBagProp)35, (int)ePinType);
				break;
			}
			case "App":
				eQueryType = EQueryType.eQueryTypeAllApps;
				break;
			}
		}
		int num12 = 0;
		IDatabaseQueryResults* ptr5 = null;
		if ((byte)((num != m_requestGeneration) ? 1 : 0) == 0)
		{
			if (eQueryType != EQueryType.eQueryTypeInvalid)
			{
				ushort* ptr6 = null;
				num12 = global::_003CModule_003E.ZuneLibraryExports_002EQueryDatabase(eQueryType, iQueryPropertyBag, &ptr5, &ptr6);
				text = new string((char*)ptr6);
				global::_003CModule_003E.SysFreeString(ptr6);
			}
			else
			{
				num12 = 0;
			}
		}
		if ((byte)((num != m_requestGeneration) ? 1 : 0) == 0 && num12 >= 0)
		{
			ZuneQueryList queryList = null;
			if (ptr5 != null)
			{
				queryList = new ZuneQueryList(ptr5, text2);
			}
			Application.DeferredInvoke(new DeferredInvokeHandler(DeferredSetResult), (object)new DeferredSetResultArgs(num, queryList, retainedList));
		}
		((IDisposable)queryPropertyBag)?.Dispose();
		if (null != ptr5)
		{
			IDatabaseQueryResults* intPtr = ptr5;
			((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint>)(int)(*(uint*)(*(int*)intPtr + 8)))((nint)intPtr);
			ptr5 = null;
		}
		if (num12 < 0)
		{
			throw new COMException("Could not perform query " + text, num12);
		}
	}

	private unsafe void DeferredSetResult(object state)
	{
		//IL_00c1: Unknown result type (might be due to invalid IL or missing references)
		DeferredSetResultArgs deferredSetResultArgs = (DeferredSetResultArgs)state;
		if (!m_disposed && deferredSetResultArgs.RequestGeneration == m_requestGeneration)
		{
			bool isEmpty = true;
			if (deferredSetResultArgs.QueryList != null)
			{
				object property = ((DataProviderQuery)this).GetProperty("AutoRefresh");
				bool autoRefresh = property == null || (bool)property;
				object property2 = ((DataProviderQuery)this).GetProperty("AntialiasImageEdges");
				bool antialiasEdges = property2 != null && (bool)property2;
				m_thumbnailFallbackImageUrl = (string)((DataProviderQuery)this).GetProperty("ThumbnailFallbackImageUrl");
				((IDisposable)m_virtualListResultSet)?.Dispose();
				LibraryVirtualList libraryVirtualList = (m_virtualListResultSet = new LibraryVirtualList(this, deferredSetResultArgs.QueryList, autoRefresh, antialiasEdges));
				ReleaseBehavior val = (ReleaseBehavior)(!deferredSetResultArgs.RetainedList);
				((VirtualList)libraryVirtualList).VisualReleaseBehavior = (ReleaseBehavior)(int)val;
				isEmpty = deferredSetResultArgs.QueryList.IsEmpty;
			}
			LibraryDataProviderQueryResult libraryDataProviderQueryResult = new LibraryDataProviderQueryResult(this, m_virtualListResultSet, ((DataProviderQuery)this).ResultTypeCookie);
			libraryDataProviderQueryResult.SetIsEmpty(isEmpty);
			((DataProviderQuery)this).Result = libraryDataProviderQueryResult;
			((DataProviderQuery)this).Status = (DataProviderQueryStatus)3;
			if ((uint)Unsafe.As<EtwControlerState, byte>(ref Unsafe.AddByteOffset(ref global::_003CModule_003E.g_EtwControlerState, 8)) <= 1u || (Unsafe.As<EtwControlerState, int>(ref Unsafe.AddByteOffset(ref global::_003CModule_003E.g_EtwControlerState, 4)) & 0x10) == 0)
			{
				return;
			}
			fixed (ushort* pwszDetail = &Unsafe.As<char, ushort>(ref global::_003CModule_003E.PtrToStringChars(((DataProviderQuery)this).ToString())))
			{
				try
				{
					global::_003CModule_003E.PERFTRACE_COLLECTIONEVENT((_COLLECTION_EVENT)29, pwszDetail);
				}
				catch
				{
					//try-fault
					pwszDetail = null;
					throw;
				}
			}
		}
		else
		{
			((IDisposable)deferredSetResultArgs.QueryList)?.Dispose();
		}
	}
}
