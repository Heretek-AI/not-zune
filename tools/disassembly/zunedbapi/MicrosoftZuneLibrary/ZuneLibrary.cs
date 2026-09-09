using System;
using System.Collections;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using MicrosoftZuneInterop;
using ZuneUI;

namespace MicrosoftZuneLibrary;

public class ZuneLibrary : IDisposable
{
	private static object m_shutdownLock = new object();

	private static bool s_fShutdown = false;

	private void _007EZuneLibrary()
	{
		ManagedLock managedLock = null;
		ManagedLock managedLock2 = new ManagedLock(m_shutdownLock);
		try
		{
			managedLock = managedLock2;
			s_fShutdown = true;
		}
		catch
		{
			//try-fault
			((IDisposable)managedLock).Dispose();
			throw;
		}
		((IDisposable)managedLock).Dispose();
		global::_003CModule_003E.ZuneLibraryExports_002EStopGroveler(false);
		global::_003CModule_003E.ZuneLibraryExports_002EShutdownZuneNativeLib();
		global::_003CModule_003E.WppCleanupUm();
		global::_003CModule_003E.ZuneEtwShutdown();
	}

	public unsafe int Initialize(string path, out bool dbRebuilt)
	{
		global::_003CModule_003E.ZuneEtwInit();
		global::_003CModule_003E.WPP_INIT_CONTROL_ARRAY((WPP_PROJECT_CONTROL_BLOCK*)Unsafe.AsPointer(ref global::_003CModule_003E.WPP_MAIN_CB));
		global::_003CModule_003E.WPP_INIT_GUID_ARRAY((_GUID**)Unsafe.AsPointer(ref global::_003CModule_003E.WPP_REGISTRATION_GUIDS));
		global::_003CModule_003E.WPP_GLOBAL_Control = (WPP_PROJECT_CONTROL_BLOCK*)Unsafe.AsPointer(ref global::_003CModule_003E.WPP_MAIN_CB);
		global::_003CModule_003E.WppInitUm((ushort*)Unsafe.AsPointer(ref global::_003CModule_003E._003F_003F_C_0040_1BK_0040DJDIBCLF_0040_003F_0024AAZ_003F_0024AAu_003F_0024AAn_003F_0024AAe_003F_0024AA_003F5_003F_0024AAI_003F_0024AAn_003F_0024AAt_003F_0024AAe_003F_0024AAr_003F_0024AAo_003F_0024AAp_003F_0024AA_003F_0024AA_0040));
		fixed (ushort* ptr = &Unsafe.As<char, ushort>(ref global::_003CModule_003E.PtrToStringChars(path)))
		{
			int num = 0;
			int result = global::_003CModule_003E.ZuneLibraryExports_002EStartupZuneNativeLib(ptr, &num);
			bool flag = ((num != 0) ? true : false);
			dbRebuilt = flag;
			return result;
		}
	}

	[return: MarshalAs(UnmanagedType.U1)]
	public bool Phase2Initialization(out int hr)
	{
		hr = 0;
		return (hr = global::_003CModule_003E.ZuneLibraryExports_002EPhase2Initialization()) >= 0;
	}

	public unsafe static string LoadStringFromResource(uint dwResourceNumber)
	{
		HINSTANCE__* ptr = global::_003CModule_003E.ZuneLibraryExports_002EGetLocResourceInstance();
		if (ptr == null)
		{
			return null;
		}
		object result = null;
		IntPtr intPtr = default(IntPtr);
		int len = global::_003CModule_003E.LoadStringW(ptr, dwResourceNumber, (ushort*)(&intPtr), 0);
		if (intPtr != IntPtr.Zero)
		{
			result = Marshal.PtrToStringUni(intPtr, len);
		}
		return (string)result;
	}

	public unsafe ZuneQueryList QueryDatabase(EQueryType QueryType, int LibraryView, EQuerySortType SortType, uint SortAtom, QueryPropertyBag propertyBag)
	{
		IDatabaseQueryResults* ptr = null;
		Unsafe.SkipInit(out CComPtrNtv_003CIQueryPropertyBag_003E cComPtrNtv_003CIQueryPropertyBag_003E);
		*(int*)(&cComPtrNtv_003CIQueryPropertyBag_003E) = 0;
		ZuneQueryList result;
		try
		{
			if (propertyBag != null)
			{
				global::_003CModule_003E.CComPtrNtv_003CIQueryPropertyBag_003E_002E_003D(&cComPtrNtv_003CIQueryPropertyBag_003E, propertyBag.GetIQueryPropertyBag());
			}
			else
			{
				int num = global::_003CModule_003E.ZuneLibraryExports_002ECreatePropertyBag((IQueryPropertyBag**)(&cComPtrNtv_003CIQueryPropertyBag_003E));
				if (num < 0)
				{
					result = null;
					goto IL_003c;
				}
			}
		}
		catch
		{
			//try-fault
			global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<CComPtrNtv_003CIQueryPropertyBag_003E*, void>)(&global::_003CModule_003E.CComPtrNtv_003CIQueryPropertyBag_003E_002E_007Bdtor_007D), &cComPtrNtv_003CIQueryPropertyBag_003E);
			throw;
		}
		ZuneQueryList result2;
		try
		{
			((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, EQueryPropertyBagProp, int, int>)(int)(*(uint*)(*(int*)(int)(*(uint*)(&cComPtrNtv_003CIQueryPropertyBag_003E)) + 28)))((IntPtr)(*(int*)(&cComPtrNtv_003CIQueryPropertyBag_003E)), (EQueryPropertyBagProp)21, (int)SortAtom);
			((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, EQueryPropertyBagProp, int, int>)(int)(*(uint*)(*(int*)(int)(*(uint*)(&cComPtrNtv_003CIQueryPropertyBag_003E)) + 28)))((IntPtr)(*(int*)(&cComPtrNtv_003CIQueryPropertyBag_003E)), (EQueryPropertyBagProp)22, (int)SortType);
			((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, EQueryPropertyBagProp, int, int>)(int)(*(uint*)(*(int*)(int)(*(uint*)(&cComPtrNtv_003CIQueryPropertyBag_003E)) + 28)))((IntPtr)(*(int*)(&cComPtrNtv_003CIQueryPropertyBag_003E)), (EQueryPropertyBagProp)15, LibraryView);
			int num = global::_003CModule_003E.ZuneLibraryExports_002EQueryDatabase(QueryType, (IQueryPropertyBag*)(int)(*(uint*)(&cComPtrNtv_003CIQueryPropertyBag_003E)), &ptr, null);
			if (num >= 0)
			{
				result2 = new ZuneQueryList(ptr, "*** Test Only ***");
				IDatabaseQueryResults* intPtr = ptr;
				((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint>)(int)(*(uint*)(*(int*)intPtr + 8)))((nint)intPtr);
				goto IL_00bf;
			}
		}
		catch
		{
			//try-fault
			global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<CComPtrNtv_003CIQueryPropertyBag_003E*, void>)(&global::_003CModule_003E.CComPtrNtv_003CIQueryPropertyBag_003E_002E_007Bdtor_007D), &cComPtrNtv_003CIQueryPropertyBag_003E);
			throw;
		}
		ZuneQueryList result3;
		try
		{
			result3 = null;
		}
		catch
		{
			//try-fault
			global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<CComPtrNtv_003CIQueryPropertyBag_003E*, void>)(&global::_003CModule_003E.CComPtrNtv_003CIQueryPropertyBag_003E_002E_007Bdtor_007D), &cComPtrNtv_003CIQueryPropertyBag_003E);
			throw;
		}
		global::_003CModule_003E.CComPtrNtv_003CIQueryPropertyBag_003E_002ERelease(&cComPtrNtv_003CIQueryPropertyBag_003E);
		return result3;
		IL_003c:
		global::_003CModule_003E.CComPtrNtv_003CIQueryPropertyBag_003E_002ERelease(&cComPtrNtv_003CIQueryPropertyBag_003E);
		return result;
		IL_00bf:
		global::_003CModule_003E.CComPtrNtv_003CIQueryPropertyBag_003E_002ERelease(&cComPtrNtv_003CIQueryPropertyBag_003E);
		return result2;
	}

	public unsafe ZuneLibraryCDDeviceList GetCDDeviceList()
	{
		IWMPCDDeviceList* ptr = null;
		if (global::_003CModule_003E.ZuneLibraryExports_002EGetCDDeviceList(&ptr) >= 0)
		{
			ZuneLibraryCDDeviceList zuneLibraryCDDeviceList = new ZuneLibraryCDDeviceList(ptr);
			IWMPCDDeviceList* intPtr = ptr;
			((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint>)(int)(*(uint*)(*(int*)intPtr + 8)))((nint)intPtr);
			zuneLibraryCDDeviceList?.AddRef();
			return zuneLibraryCDDeviceList;
		}
		return null;
	}

	public unsafe ZuneLibraryCDRecorder GetRecorder()
	{
		IRecordManager* ptr = null;
		if (global::_003CModule_003E.GetSingleton((_GUID)global::_003CModule_003E._GUID_c1cad55a_5652_40c7_842c_39cbe209379e, (void**)(&ptr)) >= 0)
		{
			ZuneLibraryCDRecorder zuneLibraryCDRecorder = new ZuneLibraryCDRecorder(ptr);
			IRecordManager* intPtr = ptr;
			((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint>)(int)(*(uint*)(*(int*)intPtr + 8)))((nint)intPtr);
			zuneLibraryCDRecorder?.AddRef();
			return zuneLibraryCDRecorder;
		}
		return null;
	}

	public unsafe ZuneQueryList GetTracksByArtist(int LibraryView, int ArtistId, EQuerySortType SortOrder, uint SortAtom)
	{
		IDatabaseQueryResults* ptr = null;
		Unsafe.SkipInit(out IQueryPropertyBag* ptr2);
		if (global::_003CModule_003E.ZuneLibraryExports_002ECreatePropertyBag(&ptr2) < 0)
		{
			return null;
		}
		((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, EQueryPropertyBagProp, int, int>)(int)(*(uint*)(*(int*)ptr2 + 28)))((nint)ptr2, (EQueryPropertyBagProp)3, ArtistId);
		((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, EQueryPropertyBagProp, int, int>)(int)(*(uint*)(*(int*)ptr2 + 28)))((nint)ptr2, (EQueryPropertyBagProp)21, (int)SortAtom);
		((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, EQueryPropertyBagProp, int, int>)(int)(*(uint*)(*(int*)ptr2 + 28)))((nint)ptr2, (EQueryPropertyBagProp)22, (int)SortOrder);
		((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, EQueryPropertyBagProp, int, int>)(int)(*(uint*)(*(int*)ptr2 + 28)))((nint)ptr2, (EQueryPropertyBagProp)15, LibraryView);
		int num = global::_003CModule_003E.ZuneLibraryExports_002EQueryDatabase(EQueryType.eQueryTypeTracksForAlbumArtistId, ptr2, &ptr, null);
		if (null != ptr2)
		{
			IQueryPropertyBag* intPtr = ptr2;
			((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint>)(int)(*(uint*)(*(int*)intPtr + 8)))((nint)intPtr);
			ptr2 = null;
		}
		if (num >= 0)
		{
			ZuneQueryList result = new ZuneQueryList(ptr, "TracksByArtist");
			IDatabaseQueryResults* intPtr2 = ptr;
			((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint>)(int)(*(uint*)(*(int*)intPtr2 + 8)))((nint)intPtr2);
			return result;
		}
		return null;
	}

	public unsafe ZuneQueryList GetTracksByAlbum(int LibraryView, int AlbumId, EQuerySortType SortOrder, uint SortAtom)
	{
		IDatabaseQueryResults* ptr = null;
		IQueryPropertyBag* ptr2 = null;
		if (global::_003CModule_003E.ZuneLibraryExports_002ECreatePropertyBag(&ptr2) < 0)
		{
			return null;
		}
		((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, EQueryPropertyBagProp, int, int>)(int)(*(uint*)(*(int*)ptr2 + 28)))((nint)ptr2, (EQueryPropertyBagProp)6, AlbumId);
		((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, EQueryPropertyBagProp, int, int>)(int)(*(uint*)(*(int*)ptr2 + 28)))((nint)ptr2, (EQueryPropertyBagProp)21, (int)SortAtom);
		((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, EQueryPropertyBagProp, int, int>)(int)(*(uint*)(*(int*)ptr2 + 28)))((nint)ptr2, (EQueryPropertyBagProp)22, (int)SortOrder);
		((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, EQueryPropertyBagProp, int, int>)(int)(*(uint*)(*(int*)ptr2 + 28)))((nint)ptr2, (EQueryPropertyBagProp)15, LibraryView);
		int num = global::_003CModule_003E.ZuneLibraryExports_002EQueryDatabase(EQueryType.eQueryTypeTracksForAlbumId, ptr2, &ptr, null);
		if (null != ptr2)
		{
			IQueryPropertyBag* intPtr = ptr2;
			((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint>)(int)(*(uint*)(*(int*)intPtr + 8)))((nint)intPtr);
			ptr2 = null;
		}
		if (num >= 0)
		{
			ZuneQueryList result = new ZuneQueryList(ptr, "TracksByAlbum");
			IDatabaseQueryResults* intPtr2 = ptr;
			((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint>)(int)(*(uint*)(*(int*)intPtr2 + 8)))((nint)intPtr2);
			return result;
		}
		return null;
	}

	public ZuneQueryList GetTracksByArtists(IList artistIds, string sort)
	{
		return ExecuteQueryHelper(EQueryType.eQueryTypeTracksForAlbumArtistId, (EQueryPropertyBagProp)4, artistIds, sort);
	}

	public ZuneQueryList GetTracksByGenres(IList genreIds, string sort)
	{
		return ExecuteQueryHelper(EQueryType.eQueryTypeTracksByGenreId, (EQueryPropertyBagProp)12, genreIds, sort);
	}

	public ZuneQueryList GetTracksByAlbums(IList albumIds, string sort)
	{
		return ExecuteQueryHelper(EQueryType.eQueryTypeTracksForAlbumId, (EQueryPropertyBagProp)7, albumIds, sort);
	}

	public unsafe ZuneQueryList GetTracksByPlaylist(int LibraryView, int PlaylistId, EQuerySortType SortOrder, uint SortAtom)
	{
		IQueryPropertyBag* ptr = null;
		if (global::_003CModule_003E.ZuneLibraryExports_002ECreatePropertyBag(&ptr) < 0)
		{
			return null;
		}
		((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, EQueryPropertyBagProp, int, int>)(int)(*(uint*)(*(int*)ptr + 28)))((nint)ptr, (EQueryPropertyBagProp)10, PlaylistId);
		((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, EQueryPropertyBagProp, int, int>)(int)(*(uint*)(*(int*)ptr + 28)))((nint)ptr, (EQueryPropertyBagProp)21, (int)SortAtom);
		((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, EQueryPropertyBagProp, int, int>)(int)(*(uint*)(*(int*)ptr + 28)))((nint)ptr, (EQueryPropertyBagProp)22, (int)SortOrder);
		((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, EQueryPropertyBagProp, int, int>)(int)(*(uint*)(*(int*)ptr + 28)))((nint)ptr, (EQueryPropertyBagProp)15, LibraryView);
		IDatabaseQueryResults* ptr2 = null;
		int num = global::_003CModule_003E.ZuneLibraryExports_002EQueryDatabase(EQueryType.eQueryTypePlaylistContentByPlaylistId, ptr, &ptr2, null);
		if (null != ptr)
		{
			IQueryPropertyBag* intPtr = ptr;
			((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint>)(int)(*(uint*)(*(int*)intPtr + 8)))((nint)intPtr);
			ptr = null;
		}
		if (num >= 0)
		{
			ZuneQueryList result = new ZuneQueryList(ptr2, "TracksByPlaylist");
			IDatabaseQueryResults* intPtr2 = ptr2;
			((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint>)(int)(*(uint*)(*(int*)intPtr2 + 8)))((nint)intPtr2);
			return result;
		}
		return null;
	}

	public ZuneQueryList GetAlbumsByArtists(IList artistIds, string sort)
	{
		return ExecuteQueryHelper(EQueryType.eQueryTypeAlbumsForAlbumArtistId, (EQueryPropertyBagProp)4, artistIds, sort);
	}

	public ZuneQueryList GetAlbumsByGenres(IList genreIds, string sort)
	{
		return ExecuteQueryHelper(EQueryType.eQueryTypeAlbumsByGenreId, (EQueryPropertyBagProp)12, genreIds, sort);
	}

	public unsafe AlbumMetadata GetAlbumMetadata(int iAlbumId)
	{
		IAlbumInfo* ptr = null;
		int num = global::_003CModule_003E.ZuneLibraryExports_002EGetAlbumMetadata(iAlbumId, true, &ptr);
		if (num != 0)
		{
			throw new COMException("IAlbumInfo::GetAlbumMetadata failed", num);
		}
		AlbumMetadata result = new AlbumMetadata(ptr);
		IAlbumInfo* intPtr = ptr;
		((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint>)(int)(*(uint*)(*(int*)intPtr + 8)))((nint)intPtr);
		return result;
	}

	public unsafe void UpdateAlbumMetadata(int iAlbumId, AlbumMetadata albumMetadata)
	{
		if (albumMetadata == null)
		{
			throw new COMException("UpdateAlbumMetadata: albumMetadata is null", -2147024809);
		}
		IAlbumInfo* albumInfo = albumMetadata.AlbumInfo;
		int num = global::_003CModule_003E.ZuneLibraryExports_002EUpdateAlbumMetadata(iAlbumId, albumInfo);
		if (num != 0)
		{
			throw new COMException("IAlbumInfo::UpdateAlbumMetadata failed", num);
		}
	}

	public unsafe void GetAlbumMetadataForAlbumId(long WMISAlbumId, int WMISVolume, AlbumMetadata dbAlbumMetadata, GetAlbumForAlbumIdCompleteHandler handler)
	{
		if (!(handler == null))
		{
			WMISGetAlbumForAlbumIdCallbackWrapper* ptr = (WMISGetAlbumForAlbumIdCallbackWrapper*)global::_003CModule_003E.@new(12u);
			WMISGetAlbumForAlbumIdCallbackWrapper* ptr2;
			try
			{
				ptr2 = ((ptr == null) ? null : global::_003CModule_003E.MicrosoftZuneLibrary_002EWMISGetAlbumForAlbumIdCallbackWrapper_002E_007Bctor_007D(ptr, handler));
			}
			catch
			{
				//try-fault
				global::_003CModule_003E.delete(ptr);
				throw;
			}
			IAlbumInfo* ptr3 = null;
			if (dbAlbumMetadata != null)
			{
				ptr3 = dbAlbumMetadata.AlbumInfo;
			}
			global::_003CModule_003E.ZuneLibraryExports_002EGetAlbumMetadataForAlbumId(WMISAlbumId, WMISVolume, ptr3, (IWMISGetAlbumForAlbumIdCallback*)ptr2);
			((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint>)(int)(*(uint*)(*(int*)ptr2 + 8)))((nint)ptr2);
		}
	}

	public static void SplitAudioTrack(int iTrackMediaId)
	{
		int num = global::_003CModule_003E.ZuneLibraryExports_002ESplitAudioTrack(iTrackMediaId);
		if (num < 0)
		{
			throw new COMException("ZuneLibraryExports::SplitAudioTrack failed", num);
		}
	}

	[return: MarshalAs(UnmanagedType.U1)]
	public unsafe bool CanAddFromFolder(string folder)
	{
		ManagedLock managedLock = null;
		ManagedLock managedLock2 = new ManagedLock(m_shutdownLock);
		bool result;
		try
		{
			managedLock = managedLock2;
			if (!s_fShutdown)
			{
				fixed (ushort* ptr = &Unsafe.As<char, ushort>(ref global::_003CModule_003E.PtrToStringChars(folder)))
				{
					try
					{
						result = global::_003CModule_003E.ZuneLibraryExports_002ECanAddFromFolder(ptr) == 0;
					}
					catch
					{
						//try-fault
						ptr = null;
						throw;
					}
				}
				goto IL_003a;
			}
		}
		catch
		{
			//try-fault
			((IDisposable)managedLock).Dispose();
			throw;
		}
		((IDisposable)managedLock).Dispose();
		return false;
		IL_003a:
		((IDisposable)managedLock).Dispose();
		return result;
	}

	[return: MarshalAs(UnmanagedType.U1)]
	public unsafe bool CanAddMedia(string filename, EMediaTypes mediaType)
	{
		ManagedLock managedLock = null;
		ManagedLock managedLock2 = new ManagedLock(m_shutdownLock);
		bool result;
		try
		{
			managedLock = managedLock2;
			if (!s_fShutdown)
			{
				fixed (ushort* ptr = &Unsafe.As<char, ushort>(ref global::_003CModule_003E.PtrToStringChars(filename)))
				{
					try
					{
						result = (byte)((global::_003CModule_003E.ZuneLibraryExports_002ECanAddMedia(ptr, mediaType) != 0) ? 1u : 0u) != 0;
					}
					catch
					{
						//try-fault
						ptr = null;
						throw;
					}
				}
				goto IL_003f;
			}
		}
		catch
		{
			//try-fault
			((IDisposable)managedLock).Dispose();
			throw;
		}
		((IDisposable)managedLock).Dispose();
		return false;
		IL_003f:
		((IDisposable)managedLock).Dispose();
		return result;
	}

	[return: MarshalAs(UnmanagedType.U1)]
	public unsafe bool AddGrovelerScanDirectory(string path, EMediaTypes mediaType)
	{
		ManagedLock managedLock = null;
		ManagedLock managedLock2 = new ManagedLock(m_shutdownLock);
		bool result;
		try
		{
			managedLock = managedLock2;
			if (!s_fShutdown)
			{
				fixed (ushort* ptr = &Unsafe.As<char, ushort>(ref global::_003CModule_003E.PtrToStringChars(path)))
				{
					try
					{
						result = global::_003CModule_003E.ZuneLibraryExports_002EAddGrovelerScanDirectory(ptr, mediaType) == 0;
					}
					catch
					{
						//try-fault
						ptr = null;
						throw;
					}
				}
				goto IL_003b;
			}
		}
		catch
		{
			//try-fault
			((IDisposable)managedLock).Dispose();
			throw;
		}
		((IDisposable)managedLock).Dispose();
		return false;
		IL_003b:
		((IDisposable)managedLock).Dispose();
		return result;
	}

	public unsafe int AddMedia(string filename)
	{
		ManagedLock managedLock = null;
		int num = -1;
		int num2 = 0;
		ManagedLock managedLock2 = new ManagedLock(m_shutdownLock);
		int result;
		try
		{
			managedLock = managedLock2;
			if (!s_fShutdown)
			{
				fixed (ushort* ptr = &Unsafe.As<char, ushort>(ref global::_003CModule_003E.PtrToStringChars(filename)))
				{
					try
					{
						num2 = global::_003CModule_003E.ZuneLibraryExports_002EAddMedia(ptr, EMediaTypes.eMediaTypeInvalid, 0u, null, &num);
					}
					catch
					{
						//try-fault
						ptr = null;
						throw;
					}
				}
			}
			IMetadataManager* ptr2 = null;
			if (num2 >= 0)
			{
				if (!s_fShutdown)
				{
					num2 = global::_003CModule_003E.GetSingleton((_GUID)global::_003CModule_003E._GUID_6dd7146d_7a19_4fbb_9235_9e6c382fcc71, (void**)(&ptr2));
				}
				if (num2 >= 0 && !s_fShutdown)
				{
					IMetadataManager* intPtr = ptr2;
					((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, int>)(int)(*(uint*)(*(int*)intPtr + 16)))((nint)intPtr);
				}
				if (ptr2 != null)
				{
					IMetadataManager* intPtr2 = ptr2;
					((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint>)(int)(*(uint*)(*(int*)intPtr2 + 8)))((nint)intPtr2);
				}
			}
			result = num;
		}
		catch
		{
			//try-fault
			((IDisposable)managedLock).Dispose();
			throw;
		}
		((IDisposable)managedLock).Dispose();
		return result;
	}

	public unsafe int AddTrack(Guid guidTrackServiceMediaId, Guid guidAlbumServiceMediaId, int iTrackNumber, string strTitle, TimeSpan duration, string strAlbum, string strArtist, string strGenre)
	{
		int result = -1;
		fixed (ushort* ptr = &Unsafe.As<char, ushort>(ref global::_003CModule_003E.PtrToStringChars(strTitle)))
		{
			fixed (ushort* ptr2 = &Unsafe.As<char, ushort>(ref global::_003CModule_003E.PtrToStringChars(strAlbum)))
			{
				fixed (ushort* ptr3 = &Unsafe.As<char, ushort>(ref global::_003CModule_003E.PtrToStringChars(strArtist)))
				{
					fixed (ushort* ptr4 = &Unsafe.As<char, ushort>(ref global::_003CModule_003E.PtrToStringChars(strGenre)))
					{
						int num = (int)duration.TotalMilliseconds;
						IMSMediaSchemaPropertySet* ptr5 = null;
						_GUID gUID = global::_003CModule_003E.GuidToGUID(guidAlbumServiceMediaId);
						if (global::_003CModule_003E.ZuneLibraryExports_002ECreateTrackPropSet(global::_003CModule_003E.GuidToGUID(guidTrackServiceMediaId), gUID, iTrackNumber, ptr, num, ptr2, ptr3, ptr4, &ptr5) >= 0)
						{
							global::_003CModule_003E.ZuneLibraryExports_002EAddMedia(ptr5, EMediaTypes.eMediaTypeAudio, &result);
						}
						if (ptr5 != null)
						{
							IMSMediaSchemaPropertySet* intPtr = ptr5;
							((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint>)(int)(*(uint*)(*(int*)intPtr + 8)))((nint)intPtr);
						}
						return result;
					}
				}
			}
		}
	}

	public unsafe int AddVideo(Guid guidVideoMediaId, string strTitle, TimeSpan duration)
	{
		int num = -1;
		fixed (ushort* ptr = &Unsafe.As<char, ushort>(ref global::_003CModule_003E.PtrToStringChars(strTitle)))
		{
			int num2 = (int)duration.TotalMilliseconds;
			Unsafe.SkipInit(out CComPtrNtv_003CIMSMediaSchemaPropertySet_003E cComPtrNtv_003CIMSMediaSchemaPropertySet_003E);
			*(int*)(&cComPtrNtv_003CIMSMediaSchemaPropertySet_003E) = 0;
			int result;
			try
			{
				if (global::_003CModule_003E.ZuneLibraryExports_002ECreateVideoPropSet(global::_003CModule_003E.GuidToGUID(guidVideoMediaId), ptr, num2, (IMSMediaSchemaPropertySet**)(&cComPtrNtv_003CIMSMediaSchemaPropertySet_003E)) >= 0)
				{
					global::_003CModule_003E.ZuneLibraryExports_002EAddMedia((IMSMediaSchemaPropertySet*)(int)(*(uint*)(&cComPtrNtv_003CIMSMediaSchemaPropertySet_003E)), EMediaTypes.eMediaTypeVideo, &num);
				}
				result = num;
			}
			catch
			{
				//try-fault
				global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<CComPtrNtv_003CIMSMediaSchemaPropertySet_003E*, void>)(&global::_003CModule_003E.CComPtrNtv_003CIMSMediaSchemaPropertySet_003E_002E_007Bdtor_007D), &cComPtrNtv_003CIMSMediaSchemaPropertySet_003E);
				throw;
			}
			global::_003CModule_003E.CComPtrNtv_003CIMSMediaSchemaPropertySet_003E_002ERelease(&cComPtrNtv_003CIMSMediaSchemaPropertySet_003E);
			return result;
		}
	}

	public unsafe int AddAlbum(Guid guidServiceMediaId, string strAlbum, string strArtist)
	{
		int result = -1;
		fixed (ushort* ptr = &Unsafe.As<char, ushort>(ref global::_003CModule_003E.PtrToStringChars(strAlbum)))
		{
			fixed (ushort* ptr2 = &Unsafe.As<char, ushort>(ref global::_003CModule_003E.PtrToStringChars(strArtist)))
			{
				IMSMediaSchemaPropertySet* ptr3 = null;
				if (global::_003CModule_003E.ZuneLibraryExports_002ECreateAlbumPropSet(global::_003CModule_003E.GuidToGUID(guidServiceMediaId), ptr, ptr2, &ptr3) >= 0)
				{
					global::_003CModule_003E.ZuneLibraryExports_002EAddMedia(ptr3, EMediaTypes.eMediaTypeAudioAlbum, &result);
				}
				if (ptr3 != null)
				{
					IMSMediaSchemaPropertySet* intPtr = ptr3;
					((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint>)(int)(*(uint*)(*(int*)intPtr + 8)))((nint)intPtr);
				}
				return result;
			}
		}
	}

	[return: MarshalAs(UnmanagedType.U1)]
	public unsafe bool AddTransientMedia(string filename, EMediaTypes mediaType, out int libraryID, out bool fFileAlreadyExists)
	{
		libraryID = -1;
		fixed (ushort* ptr = &Unsafe.As<char, ushort>(ref global::_003CModule_003E.PtrToStringChars(filename)))
		{
			fixed (int* ptr2 = &libraryID)
			{
				int num = global::_003CModule_003E.ZuneLibraryExports_002EAddTransientMedia(ptr, mediaType, ptr2);
				if (num < 0)
				{
					global::_003CModule_003E.SQMAddNumbersToStream("IgnoredErrorEvent", 1u, (uint)num);
				}
				int num2 = ((num == 1) ? 1 : 0);
				fFileAlreadyExists = (byte)num2 != 0;
				return num >= 0;
			}
		}
	}

	public void CleanupTransientMedia()
	{
		int num = global::_003CModule_003E.ZuneLibraryExports_002ECleanupTransientMedia();
		if (num < 0)
		{
			global::_003CModule_003E.SQMAddNumbersToStream("IgnoredErrorEvent", 1u, (uint)num);
		}
	}

	public void MarkAllDRMFilesAsNeedingLicenseRefresh()
	{
		int num = global::_003CModule_003E.ZuneLibraryExports_002EMarkAllDRMFilesAsNeedingLicenseRefresh();
		if (num < 0)
		{
			global::_003CModule_003E.SQMAddNumbersToStream("IgnoredErrorEvent", 1u, (uint)num);
		}
	}

	[return: MarshalAs(UnmanagedType.U1)]
	public unsafe bool DeleteMedia(int[] mediaIds, EMediaTypes mediaType, [MarshalAs(UnmanagedType.U1)] bool fDeleteFileOnDisk)
	{
		if (mediaIds == null)
		{
			return false;
		}
		fixed (int* ptr = &mediaIds[0])
		{
			return global::_003CModule_003E.ZuneLibraryExports_002EDeleteMedia(mediaType, ptr, mediaIds.Length, fDeleteFileOnDisk ? 1 : 0, 1) >= 0;
		}
	}

	[return: MarshalAs(UnmanagedType.U1)]
	public unsafe bool DeleteRootFolder(string folderName, EMediaTypes mediaType)
	{
		fixed (ushort* ptr = &Unsafe.As<char, ushort>(ref global::_003CModule_003E.PtrToStringChars(folderName)))
		{
			int num = global::_003CModule_003E.ZuneLibraryExports_002EDeleteRootFolder(ptr, mediaType);
			if (num < 0)
			{
				global::_003CModule_003E.SQMAddNumbersToStream("IgnoredErrorEvent", 1u, (uint)num);
			}
			return num == 0;
		}
	}

	[return: MarshalAs(UnmanagedType.U1)]
	public bool DeleteFilesystemFolder(int folderId, EMediaTypes mediaType)
	{
		return global::_003CModule_003E.ZuneLibraryExports_002EDeleteFSFolder(folderId, mediaType) == 0;
	}

	public static void ScanAndClearDeletedMedia()
	{
		int num = global::_003CModule_003E.ZuneLibraryExports_002EScanAndClearDeletedMedia();
		if (num < 0)
		{
			global::_003CModule_003E.SQMAddNumbersToStream("IgnoredErrorEvent", 1u, (uint)num);
		}
	}

	[return: MarshalAs(UnmanagedType.U1)]
	public static bool ImportSharedRatingsForUser(int iUserId, EMediaTypes mediaType)
	{
		return global::_003CModule_003E.ZuneLibraryExports_002EImportSharedRatingsForUser(iUserId, mediaType) >= 0;
	}

	[return: MarshalAs(UnmanagedType.U1)]
	public static bool ExportUserRatings(int iUserId, EMediaTypes mediaType)
	{
		return global::_003CModule_003E.ZuneLibraryExports_002EExportUserRatings(iUserId, mediaType) >= 0;
	}

	public static HRESULT GetFieldValues(int iMediaId, EListType eList, int cValues, int[] columnIndexes, object[] fieldValues, QueryPropertyBag propertyBag)
	{
		return GetFieldValues(iMediaId, eList, cValues, columnIndexes, fieldValues, null, propertyBag);
	}

	public unsafe static HRESULT GetFieldValues(int iMediaId, EListType eList, int cValues, int[] columnIndexes, object[] fieldValues, bool[] isEmptyValues, QueryPropertyBag propertyBag)
	{
		if (propertyBag == null)
		{
			return new HRESULT(-2147024809);
		}
		int num = 0;
		uint num2 = (((uint)cValues > 178956970u) ? uint.MaxValue : ((uint)(cValues * 24)));
		DBPropertyRequestStruct* ptr = (DBPropertyRequestStruct*)global::_003CModule_003E.new_005B_005D((num2 > 4294967291u) ? uint.MaxValue : (num2 + 4));
		DBPropertyRequestStruct* ptr3;
		try
		{
			if (ptr != null)
			{
				*(int*)ptr = cValues;
				DBPropertyRequestStruct* ptr2 = (DBPropertyRequestStruct*)((byte*)ptr + 4);
				global::_003CModule_003E.__ehvec_ctor(ptr2, 24u, cValues, (delegate*<void*, void>)(delegate*<DBPropertyRequestStruct*, void>)(&global::_003CModule_003E.DBPropertyRequestStruct_002E__dflt_ctor_closure), (delegate*<void*, void>)(delegate*<DBPropertyRequestStruct*, void>)(&global::_003CModule_003E.DBPropertyRequestStruct_002E_007Bdtor_007D));
				ptr3 = ptr2;
			}
			else
			{
				ptr3 = null;
			}
		}
		catch
		{
			//try-fault
			global::_003CModule_003E.delete_005B_005D(ptr);
			throw;
		}
		DBPropertyRequestStruct* ptr4 = ptr3;
		Unsafe.SkipInit(out CComPtrNtv_003CIQueryPropertyBag_003E cComPtrNtv_003CIQueryPropertyBag_003E);
		global::_003CModule_003E.CComPtrNtv_003CIQueryPropertyBag_003E_002E_007Bctor_007D(&cComPtrNtv_003CIQueryPropertyBag_003E, propertyBag.GetIQueryPropertyBag());
		HRESULT result;
		try
		{
			try
			{
				for (int i = 0; i < cValues; i++)
				{
					*(int*)(i * 24 + (byte*)ptr4) = columnIndexes[i];
				}
				IQueryPropertyBag* ptr5 = (IQueryPropertyBag*)(int)(*(uint*)(&cComPtrNtv_003CIQueryPropertyBag_003E));
				num = global::_003CModule_003E.ZuneLibraryExports_002EGetFieldValues(iMediaId, eList, cValues, ptr4, (IQueryPropertyBag*)(int)(*(uint*)(&cComPtrNtv_003CIQueryPropertyBag_003E)));
				if (num >= 0)
				{
					for (int j = 0; j < cValues; j++)
					{
						DBPropertyRequestStruct* ptr6 = (DBPropertyRequestStruct*)((byte*)ptr4 + j * 24);
						int num3 = ((int*)ptr6)[1];
						if (num3 >= 0)
						{
							ushort num4 = ((ushort*)ptr6)[4];
							if (num4 != 1 && num4 != 0)
							{
								Type type = null;
								if (fieldValues[j] != null)
								{
									type = fieldValues[j].GetType();
								}
								else
								{
									switch ((int)num4)
									{
									case 20:
										type = typeof(long);
										break;
									case 11:
										type = typeof(bool);
										break;
									case 8:
										type = typeof(string);
										break;
									case 3:
										type = typeof(int);
										break;
									}
								}
								fieldValues[j] = ZuneQueryList.MarshalResult(type, (tagPROPVARIANT*)((byte*)ptr6 + 8), fieldValues[j]);
							}
							else if (isEmptyValues != null)
							{
								isEmptyValues[j] = true;
							}
						}
						else if (num >= 0)
						{
							num = num3;
						}
					}
				}
			}
			finally
			{
				if (ptr4 != null)
				{
					global::_003CModule_003E.DBPropertyRequestStruct_002E__vecDelDtor(ptr4, 3u);
				}
			}
			result = new HRESULT(num);
		}
		catch
		{
			//try-fault
			global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<CComPtrNtv_003CIQueryPropertyBag_003E*, void>)(&global::_003CModule_003E.CComPtrNtv_003CIQueryPropertyBag_003E_002E_007Bdtor_007D), &cComPtrNtv_003CIQueryPropertyBag_003E);
			throw;
		}
		global::_003CModule_003E.CComPtrNtv_003CIQueryPropertyBag_003E_002ERelease(&cComPtrNtv_003CIQueryPropertyBag_003E);
		return result;
	}

	public unsafe static void SetFieldValues(int iMediaId, EListType eList, int cValues, int[] columnIndexes, object[] fieldValues, QueryPropertyBag propertyBag)
	{
		if (propertyBag == null)
		{
			return;
		}
		uint num = (((uint)cValues > 536870911u) ? uint.MaxValue : ((uint)(cValues << 3)));
		DBPropertySubmitStruct* ptr = (DBPropertySubmitStruct*)global::_003CModule_003E.new_005B_005D((num > 4294967291u) ? uint.MaxValue : (num + 4));
		DBPropertySubmitStruct* ptr3;
		try
		{
			if (ptr != null)
			{
				*(int*)ptr = cValues;
				DBPropertySubmitStruct* ptr2 = (DBPropertySubmitStruct*)((byte*)ptr + 4);
				global::_003CModule_003E.__ehvec_ctor(ptr2, 8u, cValues, (delegate*<void*, void>)(delegate*<DBPropertySubmitStruct*, void>)(&global::_003CModule_003E.DBPropertySubmitStruct_002E__dflt_ctor_closure), (delegate*<void*, void>)(delegate*<DBPropertySubmitStruct*, void>)(&global::_003CModule_003E.DBPropertySubmitStruct_002E_007Bdtor_007D));
				ptr3 = ptr2;
			}
			else
			{
				ptr3 = null;
			}
		}
		catch
		{
			//try-fault
			global::_003CModule_003E.delete_005B_005D(ptr);
			throw;
		}
		DBPropertySubmitStruct* ptr4 = ptr3;
		uint num2 = (((uint)cValues > 268435455u) ? uint.MaxValue : ((uint)(cValues * 16)));
		CComPropVariant* ptr5 = (CComPropVariant*)global::_003CModule_003E.new_005B_005D((num2 > 4294967291u) ? uint.MaxValue : (num2 + 4));
		CComPropVariant* ptr7;
		try
		{
			if (ptr5 != null)
			{
				*(int*)ptr5 = cValues;
				CComPropVariant* ptr6 = (CComPropVariant*)((byte*)ptr5 + 4);
				global::_003CModule_003E.__ehvec_ctor(ptr6, 16u, cValues, (delegate*<void*, void>)(delegate*<CComPropVariant*, CComPropVariant*>)(&global::_003CModule_003E.CComPropVariant_002E_007Bctor_007D), (delegate*<void*, void>)(delegate*<CComPropVariant*, void>)(&global::_003CModule_003E.CComPropVariant_002E_007Bdtor_007D));
				ptr7 = ptr6;
			}
			else
			{
				ptr7 = null;
			}
		}
		catch
		{
			//try-fault
			global::_003CModule_003E.delete_005B_005D(ptr5);
			throw;
		}
		CComPropVariant* ptr8 = ptr7;
		Unsafe.SkipInit(out CComPtrNtv_003CIQueryPropertyBag_003E cComPtrNtv_003CIQueryPropertyBag_003E);
		global::_003CModule_003E.CComPtrNtv_003CIQueryPropertyBag_003E_002E_007Bctor_007D(&cComPtrNtv_003CIQueryPropertyBag_003E, propertyBag.GetIQueryPropertyBag());
		try
		{
			try
			{
				for (int i = 0; i < cValues; i++)
				{
					DBPropertySubmitStruct* ptr9 = (DBPropertySubmitStruct*)(i * 8 + (byte*)ptr4);
					*(int*)ptr9 = columnIndexes[i];
					CComPropVariant* ptr10 = (CComPropVariant*)((byte*)ptr8 + i * 16);
					ZuneQueryList.ConvertTypeToPropVariant(null, fieldValues[i], (tagPROPVARIANT*)ptr10);
					((int*)ptr9)[1] = (int)ptr10;
				}
				global::_003CModule_003E.ZuneLibraryExports_002ESetFieldValues(iMediaId, eList, cValues, ptr4, (IQueryPropertyBag*)(int)(*(uint*)(&cComPtrNtv_003CIQueryPropertyBag_003E)));
			}
			finally
			{
				if (ptr4 != null)
				{
					global::_003CModule_003E.DBPropertySubmitStruct_002E__vecDelDtor(ptr4, 3u);
				}
				if (ptr8 != null)
				{
					global::_003CModule_003E.CComPropVariant_002E__vecDelDtor(ptr8, 3u);
				}
			}
		}
		catch
		{
			//try-fault
			global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<CComPtrNtv_003CIQueryPropertyBag_003E*, void>)(&global::_003CModule_003E.CComPtrNtv_003CIQueryPropertyBag_003E_002E_007Bdtor_007D), &cComPtrNtv_003CIQueryPropertyBag_003E);
			throw;
		}
		global::_003CModule_003E.CComPtrNtv_003CIQueryPropertyBag_003E_002ERelease(&cComPtrNtv_003CIQueryPropertyBag_003E);
	}

	public unsafe int GetKnownFolders(out string[] music, out string[] videos, out string[] pictures, out string[] podcasts, out string[] applications, out string ripFolder, out string videoMediaFolder, out string photoMediaFolder, out string podcastMediaFolder, out string applicationsFolder)
	{
		Unsafe.SkipInit(out DynamicArray_003Cunsigned_0020short_0020_002A_003E obj);
		global::_003CModule_003E.DynamicArray_003Cunsigned_0020short_0020_002A_003E_002E_007Bctor_007D(&obj);
		int result;
		try
		{
			Unsafe.SkipInit(out DynamicArray_003Cunsigned_0020short_0020_002A_003E obj2);
			global::_003CModule_003E.DynamicArray_003Cunsigned_0020short_0020_002A_003E_002E_007Bctor_007D(&obj2);
			try
			{
				Unsafe.SkipInit(out DynamicArray_003Cunsigned_0020short_0020_002A_003E obj3);
				global::_003CModule_003E.DynamicArray_003Cunsigned_0020short_0020_002A_003E_002E_007Bctor_007D(&obj3);
				try
				{
					Unsafe.SkipInit(out DynamicArray_003Cunsigned_0020short_0020_002A_003E obj4);
					global::_003CModule_003E.DynamicArray_003Cunsigned_0020short_0020_002A_003E_002E_007Bctor_007D(&obj4);
					try
					{
						Unsafe.SkipInit(out DynamicArray_003Cunsigned_0020short_0020_002A_003E obj5);
						global::_003CModule_003E.DynamicArray_003Cunsigned_0020short_0020_002A_003E_002E_007Bctor_007D(&obj5);
						try
						{
							ushort* ptr = null;
							ushort* ptr2 = null;
							ushort* ptr3 = null;
							ushort* ptr4 = null;
							ushort* ptr5 = null;
							result = global::_003CModule_003E.ZuneLibraryExports_002EGetKnownFolders(&obj, &obj2, &obj3, &obj4, &obj5, &ptr, &ptr2, &ptr3, &ptr4, &ptr5);
							music = global::_003CModule_003E.BstrArrayToStringArray(&obj);
							videos = global::_003CModule_003E.BstrArrayToStringArray(&obj2);
							pictures = global::_003CModule_003E.BstrArrayToStringArray(&obj3);
							podcasts = global::_003CModule_003E.BstrArrayToStringArray(&obj4);
							applications = global::_003CModule_003E.BstrArrayToStringArray(&obj5);
							ripFolder = new string((char*)ptr);
							global::_003CModule_003E.SysFreeString(ptr);
							videoMediaFolder = new string((char*)ptr2);
							global::_003CModule_003E.SysFreeString(ptr2);
							photoMediaFolder = new string((char*)ptr3);
							global::_003CModule_003E.SysFreeString(ptr3);
							podcastMediaFolder = new string((char*)ptr4);
							global::_003CModule_003E.SysFreeString(ptr4);
							applicationsFolder = new string((char*)ptr5);
							global::_003CModule_003E.SysFreeString(ptr5);
						}
						catch
						{
							//try-fault
							global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<DynamicArray_003Cunsigned_0020short_0020_002A_003E*, void>)(&global::_003CModule_003E.DynamicArray_003Cunsigned_0020short_0020_002A_003E_002E_007Bdtor_007D), &obj5);
							throw;
						}
						global::_003CModule_003E.DynamicArray_003Cunsigned_0020short_0020_002A_003E_002E_007Bdtor_007D(&obj5);
					}
					catch
					{
						//try-fault
						global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<DynamicArray_003Cunsigned_0020short_0020_002A_003E*, void>)(&global::_003CModule_003E.DynamicArray_003Cunsigned_0020short_0020_002A_003E_002E_007Bdtor_007D), &obj4);
						throw;
					}
					global::_003CModule_003E.DynamicArray_003Cunsigned_0020short_0020_002A_003E_002E_007Bdtor_007D(&obj4);
				}
				catch
				{
					//try-fault
					global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<DynamicArray_003Cunsigned_0020short_0020_002A_003E*, void>)(&global::_003CModule_003E.DynamicArray_003Cunsigned_0020short_0020_002A_003E_002E_007Bdtor_007D), &obj3);
					throw;
				}
				global::_003CModule_003E.DynamicArray_003Cunsigned_0020short_0020_002A_003E_002E_007Bdtor_007D(&obj3);
			}
			catch
			{
				//try-fault
				global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<DynamicArray_003Cunsigned_0020short_0020_002A_003E*, void>)(&global::_003CModule_003E.DynamicArray_003Cunsigned_0020short_0020_002A_003E_002E_007Bdtor_007D), &obj2);
				throw;
			}
			global::_003CModule_003E.DynamicArray_003Cunsigned_0020short_0020_002A_003E_002E_007Bdtor_007D(&obj2);
		}
		catch
		{
			//try-fault
			global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<DynamicArray_003Cunsigned_0020short_0020_002A_003E*, void>)(&global::_003CModule_003E.DynamicArray_003Cunsigned_0020short_0020_002A_003E_002E_007Bdtor_007D), &obj);
			throw;
		}
		global::_003CModule_003E.DynamicArray_003Cunsigned_0020short_0020_002A_003E_002E_007Bdtor_007D(&obj);
		return result;
	}

	public unsafe int GetLocalizedPathOfFolder([In] string physicalPath, [In][MarshalAs(UnmanagedType.U1)] bool fNetworkPathsAllowed, out string localizedPath)
	{
		ushort* ptr = null;
		fixed (ushort* ptr2 = &Unsafe.As<char, ushort>(ref global::_003CModule_003E.PtrToStringChars(physicalPath)))
		{
			int num = global::_003CModule_003E.ZuneLibraryExports_002EGetLocalizedPathOfFolder(ptr2, fNetworkPathsAllowed, &ptr);
			if (num >= 0)
			{
				localizedPath = new string((char*)ptr);
				global::_003CModule_003E.SysFreeString(ptr);
			}
			return num;
		}
	}

	public unsafe static int CompareWithoutArticles(string prefix, string @string)
	{
		fixed (ushort* ptr = &Unsafe.As<char, ushort>(ref global::_003CModule_003E.PtrToStringChars(prefix)))
		{
			fixed (ushort* ptr2 = &Unsafe.As<char, ushort>(ref global::_003CModule_003E.PtrToStringChars(@string)))
			{
				Unsafe.SkipInit(out int result);
				if (global::_003CModule_003E.ZuneLibraryExports_002ECompareWithoutArticles(ptr, ptr2, &result) != 0)
				{
					result = -1;
					return result;
				}
				return result;
			}
		}
	}

	[return: MarshalAs(UnmanagedType.U1)]
	public unsafe static bool DoesFileExist(string path)
	{
		fixed (ushort* ptr = &Unsafe.As<char, ushort>(ref global::_003CModule_003E.PtrToStringChars(path)))
		{
			int num = 0;
			int num2 = ((global::_003CModule_003E.ZuneLibraryExports_002EDoesFileExist(ptr, &num) >= 0 && num != 0) ? 1 : 0);
			return (byte)num2 != 0;
		}
	}

	private unsafe ZuneQueryList ExecuteQueryHelper(EQueryType eQueryType, EQueryPropertyBagProp idPropType, IList ids, string sort)
	{
		bool[] ascendings = null;
		string[] sorts = null;
		IDatabaseQueryResults* ptr = null;
		QueryPropertyBag queryPropertyBag = new QueryPropertyBag();
		Unsafe.SkipInit(out CComPtrNtv_003CIQueryPropertyBag_003E cComPtrNtv_003CIQueryPropertyBag_003E);
		global::_003CModule_003E.CComPtrNtv_003CIQueryPropertyBag_003E_002E_007Bctor_007D(&cComPtrNtv_003CIQueryPropertyBag_003E, queryPropertyBag.GetIQueryPropertyBag());
		ZuneQueryList result;
		try
		{
			if (ids.Count == 1)
			{
				((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, EQueryPropertyBagProp, int, int>)(int)(*(uint*)(*(int*)(int)(*(uint*)(&cComPtrNtv_003CIQueryPropertyBag_003E)) + 28)))((IntPtr)(*(int*)(&cComPtrNtv_003CIQueryPropertyBag_003E)), (EQueryPropertyBagProp)15, 0);
				int num = (int)ids[0];
				switch (eQueryType)
				{
				default:
					((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, EQueryPropertyBagProp, int, int>)(int)(*(uint*)(*(int*)(int)(*(uint*)(&cComPtrNtv_003CIQueryPropertyBag_003E)) + 28)))((IntPtr)(*(int*)(&cComPtrNtv_003CIQueryPropertyBag_003E)), (EQueryPropertyBagProp)6, num);
					break;
				case EQueryType.eQueryTypeTracksByGenreId:
				case EQueryType.eQueryTypeAlbumsByGenreId:
					((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, EQueryPropertyBagProp, int, int>)(int)(*(uint*)(*(int*)(int)(*(uint*)(&cComPtrNtv_003CIQueryPropertyBag_003E)) + 28)))((IntPtr)(*(int*)(&cComPtrNtv_003CIQueryPropertyBag_003E)), (EQueryPropertyBagProp)11, num);
					break;
				case EQueryType.eQueryTypeAlbumsForAlbumArtistId:
				case EQueryType.eQueryTypeTracksForAlbumArtistId:
					((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, EQueryPropertyBagProp, int, int>)(int)(*(uint*)(*(int*)(int)(*(uint*)(&cComPtrNtv_003CIQueryPropertyBag_003E)) + 28)))((IntPtr)(*(int*)(&cComPtrNtv_003CIQueryPropertyBag_003E)), (EQueryPropertyBagProp)3, num);
					break;
				}
			}
			else
			{
				((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, EQueryPropertyBagProp, int, int>)(int)(*(uint*)(*(int*)(int)(*(uint*)(&cComPtrNtv_003CIQueryPropertyBag_003E)) + 28)))((IntPtr)(*(int*)(&cComPtrNtv_003CIQueryPropertyBag_003E)), (EQueryPropertyBagProp)15, 150994944);
				int num2 = *(int*)(int)(*(uint*)(&cComPtrNtv_003CIQueryPropertyBag_003E)) + 12;
				((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, EQueryPropertyBagProp, IDList*, int>)(int)(*(uint*)num2))((IntPtr)(*(int*)(&cComPtrNtv_003CIQueryPropertyBag_003E)), idPropType, queryPropertyBag.PackIDList(ids));
				((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, EQueryPropertyBagProp, int, int>)(int)(*(uint*)(*(int*)(int)(*(uint*)(&cComPtrNtv_003CIQueryPropertyBag_003E)) + 28)))((IntPtr)(*(int*)(&cComPtrNtv_003CIQueryPropertyBag_003E)), (EQueryPropertyBagProp)3, -1);
				((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, EQueryPropertyBagProp, int, int>)(int)(*(uint*)(*(int*)(int)(*(uint*)(&cComPtrNtv_003CIQueryPropertyBag_003E)) + 28)))((IntPtr)(*(int*)(&cComPtrNtv_003CIQueryPropertyBag_003E)), (EQueryPropertyBagProp)6, -1);
			}
			if (LibraryDataProvider.GetSortAttributes(sort, out sorts, out ascendings))
			{
				IMultiSortAttributes* ptr2 = queryPropertyBag.PackMultiSortAttributes(sorts, ascendings);
				((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, EQueryPropertyBagProp, IMultiSortAttributes*, int>)(int)(*(uint*)(*(int*)(int)(*(uint*)(&cComPtrNtv_003CIQueryPropertyBag_003E)) + 16)))((IntPtr)(*(int*)(&cComPtrNtv_003CIQueryPropertyBag_003E)), (EQueryPropertyBagProp)23, ptr2);
				if (ptr2 != null)
				{
					((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint>)(int)(*(uint*)(*(int*)ptr2 + 8)))((nint)ptr2);
				}
			}
			IService* ptr3 = null;
			if (global::_003CModule_003E.GetSingleton((_GUID)global::_003CModule_003E._GUID_bb2d1edd_1bd5_4be1_8d38_36d4f0849911, (void**)(&ptr3)) >= 0)
			{
				Unsafe.SkipInit(out _GUID gUID);
				Unsafe.SkipInit(out int num4);
				int num3 = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, _GUID*, int*, int>)(int)(*(uint*)(*(int*)ptr3 + 192)))((nint)ptr3, &gUID, &num4);
				IService* intPtr = ptr3;
				((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint>)(int)(*(uint*)(*(int*)intPtr + 8)))((nint)intPtr);
				if (num3 >= 0)
				{
					((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, EQueryPropertyBagProp, int, int>)(int)(*(uint*)(*(int*)(int)(*(uint*)(&cComPtrNtv_003CIQueryPropertyBag_003E)) + 28)))((IntPtr)(*(int*)(&cComPtrNtv_003CIQueryPropertyBag_003E)), (EQueryPropertyBagProp)0, num4);
				}
			}
			int num5 = global::_003CModule_003E.ZuneLibraryExports_002EQueryDatabase(eQueryType, (IQueryPropertyBag*)(int)(*(uint*)(&cComPtrNtv_003CIQueryPropertyBag_003E)), &ptr, null);
			((IDisposable)queryPropertyBag)?.Dispose();
			if (num5 >= 0)
			{
				result = new ZuneQueryList(ptr, "ExecuteQueryHelper");
				IDatabaseQueryResults* intPtr2 = ptr;
				((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint>)(int)(*(uint*)(*(int*)intPtr2 + 8)))((nint)intPtr2);
				goto IL_01c4;
			}
		}
		catch
		{
			//try-fault
			global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<CComPtrNtv_003CIQueryPropertyBag_003E*, void>)(&global::_003CModule_003E.CComPtrNtv_003CIQueryPropertyBag_003E_002E_007Bdtor_007D), &cComPtrNtv_003CIQueryPropertyBag_003E);
			throw;
		}
		ZuneQueryList result2;
		try
		{
			result2 = null;
		}
		catch
		{
			//try-fault
			global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<CComPtrNtv_003CIQueryPropertyBag_003E*, void>)(&global::_003CModule_003E.CComPtrNtv_003CIQueryPropertyBag_003E_002E_007Bdtor_007D), &cComPtrNtv_003CIQueryPropertyBag_003E);
			throw;
		}
		global::_003CModule_003E.CComPtrNtv_003CIQueryPropertyBag_003E_002ERelease(&cComPtrNtv_003CIQueryPropertyBag_003E);
		return result2;
		IL_01c4:
		global::_003CModule_003E.CComPtrNtv_003CIQueryPropertyBag_003E_002ERelease(&cComPtrNtv_003CIQueryPropertyBag_003E);
		return result;
	}

	protected virtual void Dispose([MarshalAs(UnmanagedType.U1)] bool P_0)
	{
		if (P_0)
		{
			_007EZuneLibrary();
		}
		else
		{
			base.Finalize();
		}
	}

	public virtual sealed void Dispose()
	{
		Dispose(true);
		GC.SuppressFinalize(this);
	}
}
