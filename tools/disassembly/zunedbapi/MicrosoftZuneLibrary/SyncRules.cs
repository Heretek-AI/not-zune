using System;
using System.Collections;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace MicrosoftZuneLibrary;

public class SyncRules : IDisposable
{
	private readonly CComPtrMgd_003CIEndpointHost_003E m_spEndpointHost;

	private int m_iDeviceID;

	private ArrayList m_listAllowedToAdd;

	private ArrayList m_listAllowedToExclude;

	internal unsafe SyncRules(IEndpointHost* pEndpointHost)
	{
		CComPtrMgd_003CIEndpointHost_003E spEndpointHost = new CComPtrMgd_003CIEndpointHost_003E();
		try
		{
			m_spEndpointHost = spEndpointHost;
			base._002Ector();
			if (global::_003CModule_003E.WPP_GLOBAL_Control != Unsafe.AsPointer(ref global::_003CModule_003E.WPP_GLOBAL_Control) && (((int*)global::_003CModule_003E.WPP_GLOBAL_Control)[15] & 0x200) != 0 && (uint)((byte*)global::_003CModule_003E.WPP_GLOBAL_Control)[57] >= 5u)
			{
				global::_003CModule_003E.WPP_SF_(((ulong*)global::_003CModule_003E.WPP_GLOBAL_Control)[6], 10, (_GUID*)Unsafe.AsPointer(ref global::_003CModule_003E._003FA0x556d048d_002EWPP_SyncRulesAPI_cpp_Traceguids));
			}
			m_spEndpointHost.op_Assign(pEndpointHost);
			IEndpointHost* p = m_spEndpointHost.p;
			Unsafe.SkipInit(out int iDeviceID);
			((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, EEndpointHostProperty, int*, int>)(int)(*(uint*)(*(int*)p + 68)))((nint)p, EEndpointHostProperty.eEndpointHostPropertyDatabaseEndpointId, &iDeviceID);
			m_iDeviceID = iDeviceID;
			(m_listAllowedToAdd = new ArrayList()).Add(EMediaTypes.eMediaTypeAudio);
			m_listAllowedToAdd.Add(EMediaTypes.eMediaTypeAudioAlbum);
			m_listAllowedToAdd.Add(EMediaTypes.eMediaTypePersonArtist);
			m_listAllowedToAdd.Add(EMediaTypes.eMediaTypeImage);
			m_listAllowedToAdd.Add(EMediaTypes.eMediaTypeVideo);
			m_listAllowedToAdd.Add(EMediaTypes.eMediaTypePodcastEpisode);
			m_listAllowedToAdd.Add(EMediaTypes.eMediaTypePodcastSeries);
			m_listAllowedToAdd.Add(EMediaTypes.eMediaTypePlaylist);
			m_listAllowedToAdd.Add(EMediaTypes.eMediaTypeFolder);
			m_listAllowedToAdd.Add(EMediaTypes.eMediaTypeGenre);
			m_listAllowedToAdd.Add(EMediaTypes.eMediaTypeUserCard);
			m_listAllowedToAdd.Add(EMediaTypes.eMediaTypeApp);
			(m_listAllowedToExclude = new ArrayList()).Add(EMediaTypes.eMediaTypeAudio);
			m_listAllowedToExclude.Add(EMediaTypes.eMediaTypeAudioAlbum);
			m_listAllowedToExclude.Add(EMediaTypes.eMediaTypePersonArtist);
			m_listAllowedToExclude.Add(EMediaTypes.eMediaTypeImage);
			m_listAllowedToExclude.Add(EMediaTypes.eMediaTypeVideo);
			m_listAllowedToExclude.Add(EMediaTypes.eMediaTypePodcastEpisode);
			m_listAllowedToExclude.Add(EMediaTypes.eMediaTypePodcastSeries);
			m_listAllowedToExclude.Add(EMediaTypes.eMediaTypePlaylist);
			m_listAllowedToExclude.Add(EMediaTypes.eMediaTypeFolder);
			m_listAllowedToExclude.Add(EMediaTypes.eMediaTypeGenre);
			m_listAllowedToExclude.Add(EMediaTypes.eMediaTypeUserCard);
			m_listAllowedToExclude.Add(EMediaTypes.eMediaTypeApp);
			if (global::_003CModule_003E.WPP_GLOBAL_Control != Unsafe.AsPointer(ref global::_003CModule_003E.WPP_GLOBAL_Control) && (((int*)global::_003CModule_003E.WPP_GLOBAL_Control)[15] & 0x200) != 0 && (uint)((byte*)global::_003CModule_003E.WPP_GLOBAL_Control)[57] >= 5u)
			{
				global::_003CModule_003E.WPP_SF_(((ulong*)global::_003CModule_003E.WPP_GLOBAL_Control)[6], 11, (_GUID*)Unsafe.AsPointer(ref global::_003CModule_003E._003FA0x556d048d_002EWPP_SyncRulesAPI_cpp_Traceguids));
			}
			return;
		}
		catch
		{
			//try-fault
			((IDisposable)m_spEndpointHost).Dispose();
			throw;
		}
	}

	private unsafe void _007ESyncRules()
	{
		if (global::_003CModule_003E.WPP_GLOBAL_Control != Unsafe.AsPointer(ref global::_003CModule_003E.WPP_GLOBAL_Control) && (((int*)global::_003CModule_003E.WPP_GLOBAL_Control)[15] & 0x200) != 0 && (uint)((byte*)global::_003CModule_003E.WPP_GLOBAL_Control)[57] >= 5u)
		{
			global::_003CModule_003E.WPP_SF_(((ulong*)global::_003CModule_003E.WPP_GLOBAL_Control)[6], 12, (_GUID*)Unsafe.AsPointer(ref global::_003CModule_003E._003FA0x556d048d_002EWPP_SyncRulesAPI_cpp_Traceguids));
		}
		m_spEndpointHost.Release();
		if (global::_003CModule_003E.WPP_GLOBAL_Control != Unsafe.AsPointer(ref global::_003CModule_003E.WPP_GLOBAL_Control) && (((int*)global::_003CModule_003E.WPP_GLOBAL_Control)[15] & 0x200) != 0 && (uint)((byte*)global::_003CModule_003E.WPP_GLOBAL_Control)[57] >= 5u)
		{
			global::_003CModule_003E.WPP_SF_(((ulong*)global::_003CModule_003E.WPP_GLOBAL_Control)[6], 13, (_GUID*)Unsafe.AsPointer(ref global::_003CModule_003E._003FA0x556d048d_002EWPP_SyncRulesAPI_cpp_Traceguids));
		}
	}

	public unsafe int Add(int[] rgIds, EMediaTypes mediaType)
	{
		if (global::_003CModule_003E.WPP_GLOBAL_Control != Unsafe.AsPointer(ref global::_003CModule_003E.WPP_GLOBAL_Control) && (((int*)global::_003CModule_003E.WPP_GLOBAL_Control)[15] & 0x200) != 0 && (uint)((byte*)global::_003CModule_003E.WPP_GLOBAL_Control)[57] >= 5u)
		{
			global::_003CModule_003E.WPP_SF_(((ulong*)global::_003CModule_003E.WPP_GLOBAL_Control)[6], 14, (_GUID*)Unsafe.AsPointer(ref global::_003CModule_003E._003FA0x556d048d_002EWPP_SyncRulesAPI_cpp_Traceguids));
		}
		int num;
		if (!m_listAllowedToAdd.Contains(mediaType))
		{
			num = -2147024809;
		}
		else
		{
			num = RemoveInternal(rgIds, mediaType, EDeviceSyncRuleType.eDeviceSyncRuleTypeExclude, fDeviceFolderIds: false);
			if (num >= 0)
			{
				num = AddInternal(rgIds, mediaType, EDeviceSyncRuleType.eDeviceSyncRuleTypeIncludeAll, fAutoSelectRuleType: true);
			}
		}
		if (global::_003CModule_003E.WPP_GLOBAL_Control != Unsafe.AsPointer(ref global::_003CModule_003E.WPP_GLOBAL_Control) && (((int*)global::_003CModule_003E.WPP_GLOBAL_Control)[15] & 0x200) != 0 && (uint)((byte*)global::_003CModule_003E.WPP_GLOBAL_Control)[57] >= 5u)
		{
			global::_003CModule_003E.WPP_SF_d(((ulong*)global::_003CModule_003E.WPP_GLOBAL_Control)[6], 15, (_GUID*)Unsafe.AsPointer(ref global::_003CModule_003E._003FA0x556d048d_002EWPP_SyncRulesAPI_cpp_Traceguids), num);
		}
		return num;
	}

	public unsafe int Add(int[] rgIds, EMediaTypes mediaType, EDeviceSyncRuleType ruleType)
	{
		if (global::_003CModule_003E.WPP_GLOBAL_Control != Unsafe.AsPointer(ref global::_003CModule_003E.WPP_GLOBAL_Control) && (((int*)global::_003CModule_003E.WPP_GLOBAL_Control)[15] & 0x200) != 0 && (uint)((byte*)global::_003CModule_003E.WPP_GLOBAL_Control)[57] >= 5u)
		{
			global::_003CModule_003E.WPP_SF_(((ulong*)global::_003CModule_003E.WPP_GLOBAL_Control)[6], 16, (_GUID*)Unsafe.AsPointer(ref global::_003CModule_003E._003FA0x556d048d_002EWPP_SyncRulesAPI_cpp_Traceguids));
		}
		int num;
		if (EMediaTypes.eMediaTypePodcastSeries != mediaType)
		{
			num = -2147024809;
		}
		else
		{
			num = RemoveInternal(rgIds, EMediaTypes.eMediaTypePodcastSeries, EDeviceSyncRuleType.eDeviceSyncRuleTypeExclude, fDeviceFolderIds: false);
			if (num >= 0)
			{
				num = Remove(rgIds, EMediaTypes.eMediaTypePodcastSeries, fDeviceFolderIds: false);
				if (num >= 0)
				{
					num = AddInternal(rgIds, EMediaTypes.eMediaTypePodcastSeries, ruleType, fAutoSelectRuleType: false);
				}
			}
		}
		if (global::_003CModule_003E.WPP_GLOBAL_Control != Unsafe.AsPointer(ref global::_003CModule_003E.WPP_GLOBAL_Control) && (((int*)global::_003CModule_003E.WPP_GLOBAL_Control)[15] & 0x200) != 0 && (uint)((byte*)global::_003CModule_003E.WPP_GLOBAL_Control)[57] >= 5u)
		{
			global::_003CModule_003E.WPP_SF_d(((ulong*)global::_003CModule_003E.WPP_GLOBAL_Control)[6], 17, (_GUID*)Unsafe.AsPointer(ref global::_003CModule_003E._003FA0x556d048d_002EWPP_SyncRulesAPI_cpp_Traceguids), num);
		}
		return num;
	}

	public unsafe int AddDeviceSyncRuleWithValue(int[] rgIds, int value)
	{
		int num = 0;
		if (global::_003CModule_003E.WPP_GLOBAL_Control != Unsafe.AsPointer(ref global::_003CModule_003E.WPP_GLOBAL_Control) && (((int*)global::_003CModule_003E.WPP_GLOBAL_Control)[15] & 0x200) != 0 && (uint)((byte*)global::_003CModule_003E.WPP_GLOBAL_Control)[57] >= 5u)
		{
			global::_003CModule_003E.WPP_SF_(((ulong*)global::_003CModule_003E.WPP_GLOBAL_Control)[6], 18, (_GUID*)Unsafe.AsPointer(ref global::_003CModule_003E._003FA0x556d048d_002EWPP_SyncRulesAPI_cpp_Traceguids));
		}
		int num2 = 0;
		if (0 < (nint)rgIds.LongLength)
		{
			while (num >= 0)
			{
				num = global::_003CModule_003E.ZuneLibraryExports_002EAddDeviceSyncRuleWithValue(EDeviceSyncRuleType.eDeviceSyncRuleTypeSyncEpisodesCount, m_iDeviceID, EMediaTypes.eMediaTypePodcastSeries, rgIds[num2], value);
				if (num < 0)
				{
					break;
				}
				num2++;
				if (num2 >= (nint)rgIds.LongLength)
				{
					break;
				}
			}
		}
		if (global::_003CModule_003E.WPP_GLOBAL_Control != Unsafe.AsPointer(ref global::_003CModule_003E.WPP_GLOBAL_Control) && (((int*)global::_003CModule_003E.WPP_GLOBAL_Control)[15] & 0x200) != 0 && (uint)((byte*)global::_003CModule_003E.WPP_GLOBAL_Control)[57] >= 5u)
		{
			global::_003CModule_003E.WPP_SF_d(((ulong*)global::_003CModule_003E.WPP_GLOBAL_Control)[6], 19, (_GUID*)Unsafe.AsPointer(ref global::_003CModule_003E._003FA0x556d048d_002EWPP_SyncRulesAPI_cpp_Traceguids), num);
		}
		return num;
	}

	public unsafe int Remove(int[] rgIds, EMediaTypes mediaType, [MarshalAs(UnmanagedType.U1)] bool fDeviceFolderIds)
	{
		if (global::_003CModule_003E.WPP_GLOBAL_Control != Unsafe.AsPointer(ref global::_003CModule_003E.WPP_GLOBAL_Control) && (((int*)global::_003CModule_003E.WPP_GLOBAL_Control)[15] & 0x200) != 0 && (uint)((byte*)global::_003CModule_003E.WPP_GLOBAL_Control)[57] >= 5u)
		{
			global::_003CModule_003E.WPP_SF_(((ulong*)global::_003CModule_003E.WPP_GLOBAL_Control)[6], 24, (_GUID*)Unsafe.AsPointer(ref global::_003CModule_003E._003FA0x556d048d_002EWPP_SyncRulesAPI_cpp_Traceguids));
		}
		int num = (m_listAllowedToAdd.Contains(mediaType) ? RemoveInternal(rgIds, mediaType, EDeviceSyncRuleType.eDeviceSyncRuleTypeIncludeAll, fDeviceFolderIds) : (-2147024809));
		if (EMediaTypes.eMediaTypePodcastSeries == mediaType && num >= 0)
		{
			num = RemoveInternal(rgIds, EMediaTypes.eMediaTypePodcastSeries, EDeviceSyncRuleType.eDeviceSyncRuleTypeNone, fDeviceFolderIds);
			if (num >= 0)
			{
				num = RemoveInternal(rgIds, EMediaTypes.eMediaTypePodcastSeries, EDeviceSyncRuleType.eDeviceSyncRuleTypeAllUnplayed, fDeviceFolderIds);
				if (num >= 0)
				{
					num = RemoveInternal(rgIds, EMediaTypes.eMediaTypePodcastSeries, EDeviceSyncRuleType.eDeviceSyncRuleTypeFirstUnplayed, fDeviceFolderIds);
				}
			}
		}
		if (global::_003CModule_003E.WPP_GLOBAL_Control != Unsafe.AsPointer(ref global::_003CModule_003E.WPP_GLOBAL_Control) && (((int*)global::_003CModule_003E.WPP_GLOBAL_Control)[15] & 0x200) != 0 && (uint)((byte*)global::_003CModule_003E.WPP_GLOBAL_Control)[57] >= 5u)
		{
			global::_003CModule_003E.WPP_SF_d(((ulong*)global::_003CModule_003E.WPP_GLOBAL_Control)[6], 25, (_GUID*)Unsafe.AsPointer(ref global::_003CModule_003E._003FA0x556d048d_002EWPP_SyncRulesAPI_cpp_Traceguids), num);
		}
		return num;
	}

	public unsafe int Exclude(int[] rgIds, EMediaTypes mediaType)
	{
		if (global::_003CModule_003E.WPP_GLOBAL_Control != Unsafe.AsPointer(ref global::_003CModule_003E.WPP_GLOBAL_Control) && (((int*)global::_003CModule_003E.WPP_GLOBAL_Control)[15] & 0x200) != 0 && (uint)((byte*)global::_003CModule_003E.WPP_GLOBAL_Control)[57] >= 5u)
		{
			global::_003CModule_003E.WPP_SF_(((ulong*)global::_003CModule_003E.WPP_GLOBAL_Control)[6], 20, (_GUID*)Unsafe.AsPointer(ref global::_003CModule_003E._003FA0x556d048d_002EWPP_SyncRulesAPI_cpp_Traceguids));
		}
		int num;
		if (!m_listAllowedToExclude.Contains(mediaType))
		{
			num = -2147024809;
		}
		else
		{
			num = Remove(rgIds, mediaType, fDeviceFolderIds: false);
			if (num >= 0)
			{
				num = AddInternal(rgIds, mediaType, EDeviceSyncRuleType.eDeviceSyncRuleTypeExclude, fAutoSelectRuleType: false);
			}
		}
		if (global::_003CModule_003E.WPP_GLOBAL_Control != Unsafe.AsPointer(ref global::_003CModule_003E.WPP_GLOBAL_Control) && (((int*)global::_003CModule_003E.WPP_GLOBAL_Control)[15] & 0x200) != 0 && (uint)((byte*)global::_003CModule_003E.WPP_GLOBAL_Control)[57] >= 5u)
		{
			global::_003CModule_003E.WPP_SF_d(((ulong*)global::_003CModule_003E.WPP_GLOBAL_Control)[6], 21, (_GUID*)Unsafe.AsPointer(ref global::_003CModule_003E._003FA0x556d048d_002EWPP_SyncRulesAPI_cpp_Traceguids), num);
		}
		return num;
	}

	public unsafe int Unexclude(int[] rgIds, EMediaTypes mediaType)
	{
		if (global::_003CModule_003E.WPP_GLOBAL_Control != Unsafe.AsPointer(ref global::_003CModule_003E.WPP_GLOBAL_Control) && (((int*)global::_003CModule_003E.WPP_GLOBAL_Control)[15] & 0x200) != 0 && (uint)((byte*)global::_003CModule_003E.WPP_GLOBAL_Control)[57] >= 5u)
		{
			global::_003CModule_003E.WPP_SF_(((ulong*)global::_003CModule_003E.WPP_GLOBAL_Control)[6], 26, (_GUID*)Unsafe.AsPointer(ref global::_003CModule_003E._003FA0x556d048d_002EWPP_SyncRulesAPI_cpp_Traceguids));
		}
		int num = (m_listAllowedToExclude.Contains(mediaType) ? RemoveInternal(rgIds, mediaType, EDeviceSyncRuleType.eDeviceSyncRuleTypeExclude, fDeviceFolderIds: false) : (-2147024809));
		if (global::_003CModule_003E.WPP_GLOBAL_Control != Unsafe.AsPointer(ref global::_003CModule_003E.WPP_GLOBAL_Control) && (((int*)global::_003CModule_003E.WPP_GLOBAL_Control)[15] & 0x200) != 0 && (uint)((byte*)global::_003CModule_003E.WPP_GLOBAL_Control)[57] >= 5u)
		{
			global::_003CModule_003E.WPP_SF_d(((ulong*)global::_003CModule_003E.WPP_GLOBAL_Control)[6], 27, (_GUID*)Unsafe.AsPointer(ref global::_003CModule_003E._003FA0x556d048d_002EWPP_SyncRulesAPI_cpp_Traceguids), num);
		}
		return num;
	}

	public unsafe int GetSyncRuleForMedia(EMediaTypes mediaType, int iMediaItemId, ref EDeviceSyncRuleType ruleType)
	{
		Unsafe.SkipInit(out EDeviceSyncRuleType eDeviceSyncRuleType);
		int num = global::_003CModule_003E.ZuneLibraryExports_002EGetSyncRuleForMedia(m_iDeviceID, mediaType, iMediaItemId, &eDeviceSyncRuleType);
		if (num >= 0)
		{
			ruleType = eDeviceSyncRuleType;
		}
		return num;
	}

	public unsafe int GetSyncRuleValueForMedia(int iMediaItemId, ref int iValue)
	{
		Unsafe.SkipInit(out int num2);
		int num = global::_003CModule_003E.ZuneLibraryExports_002EGetSyncRuleValueForMedia(EDeviceSyncRuleType.eDeviceSyncRuleTypeSyncEpisodesCount, m_iDeviceID, EMediaTypes.eMediaTypePodcastSeries, iMediaItemId, &num2);
		if (num >= 0)
		{
			iValue = num2;
		}
		return num;
	}

	public unsafe int GetCategorySyncMode(ESyncCategory cat, ref ESyncMode mode, [MarshalAs(UnmanagedType.U1)] bool fEstablishingPartnership)
	{
		if (m_spEndpointHost.p == null)
		{
			global::_003CModule_003E._ZuneShipAssert(1002u, 454u);
			return -2147418113;
		}
		Unsafe.SkipInit(out CComPtrNtv_003CIMetadataManager_003E cComPtrNtv_003CIMetadataManager_003E);
		*(int*)(&cComPtrNtv_003CIMetadataManager_003E) = 0;
		int num;
		try
		{
			Unsafe.SkipInit(out CComPtrNtv_003CIDeviceSyncRulesProvider_003E cComPtrNtv_003CIDeviceSyncRulesProvider_003E);
			*(int*)(&cComPtrNtv_003CIDeviceSyncRulesProvider_003E) = 0;
			try
			{
				mode = ESyncMode.eSyncModeInvalid;
				num = global::_003CModule_003E.GetSingleton((_GUID)global::_003CModule_003E._GUID_6dd7146d_7a19_4fbb_9235_9e6c382fcc71, (void**)(&cComPtrNtv_003CIMetadataManager_003E));
				if (num >= 0)
				{
					num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, _GUID, void**, int>)(int)(*(uint*)(*(int*)(int)(*(uint*)(&cComPtrNtv_003CIMetadataManager_003E)) + 12)))((IntPtr)(*(int*)(&cComPtrNtv_003CIMetadataManager_003E)), (_GUID)global::_003CModule_003E._GUID_b12dc962_cc1b_46c5_a92a_68f1f2b9bff3, (void**)(&cComPtrNtv_003CIDeviceSyncRulesProvider_003E));
					if (num >= 0)
					{
						ESyncMode eSyncMode = ESyncMode.eSyncModeInvalid;
						num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, int, ESyncCategory, ESyncMode*, byte, int>)(int)(*(uint*)(*(int*)(int)(*(uint*)(&cComPtrNtv_003CIDeviceSyncRulesProvider_003E)) + 64)))((IntPtr)(*(int*)(&cComPtrNtv_003CIDeviceSyncRulesProvider_003E)), m_iDeviceID, cat, &eSyncMode, fEstablishingPartnership ? ((byte)1) : ((byte)0));
						if (num >= 0)
						{
							mode = eSyncMode;
						}
					}
				}
				if (global::_003CModule_003E.WPP_GLOBAL_Control != Unsafe.AsPointer(ref global::_003CModule_003E.WPP_GLOBAL_Control) && (((int*)global::_003CModule_003E.WPP_GLOBAL_Control)[15] & 0x200) != 0 && (uint)((byte*)global::_003CModule_003E.WPP_GLOBAL_Control)[57] >= 5u)
				{
					global::_003CModule_003E.WPP_SF_LLd(((ulong*)global::_003CModule_003E.WPP_GLOBAL_Control)[6], 30, (_GUID*)Unsafe.AsPointer(ref global::_003CModule_003E._003FA0x556d048d_002EWPP_SyncRulesAPI_cpp_Traceguids), (uint)cat, (uint)mode, num);
				}
			}
			catch
			{
				//try-fault
				global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<CComPtrNtv_003CIDeviceSyncRulesProvider_003E*, void>)(&global::_003CModule_003E.CComPtrNtv_003CIDeviceSyncRulesProvider_003E_002E_007Bdtor_007D), &cComPtrNtv_003CIDeviceSyncRulesProvider_003E);
				throw;
			}
			global::_003CModule_003E.CComPtrNtv_003CIDeviceSyncRulesProvider_003E_002ERelease(&cComPtrNtv_003CIDeviceSyncRulesProvider_003E);
		}
		catch
		{
			//try-fault
			global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<CComPtrNtv_003CIMetadataManager_003E*, void>)(&global::_003CModule_003E.CComPtrNtv_003CIMetadataManager_003E_002E_007Bdtor_007D), &cComPtrNtv_003CIMetadataManager_003E);
			throw;
		}
		global::_003CModule_003E.CComPtrNtv_003CIMetadataManager_003E_002ERelease(&cComPtrNtv_003CIMetadataManager_003E);
		return num;
	}

	public unsafe int SetCategorySyncMode(ESyncCategory cat, ESyncMode mode)
	{
		if (m_spEndpointHost.p == null)
		{
			global::_003CModule_003E._ZuneShipAssert(1002u, 504u);
			return -2147418113;
		}
		Unsafe.SkipInit(out CComPtrNtv_003CIMetadataManager_003E cComPtrNtv_003CIMetadataManager_003E);
		*(int*)(&cComPtrNtv_003CIMetadataManager_003E) = 0;
		int num;
		try
		{
			Unsafe.SkipInit(out CComPtrNtv_003CIDeviceSyncRulesProvider_003E cComPtrNtv_003CIDeviceSyncRulesProvider_003E);
			*(int*)(&cComPtrNtv_003CIDeviceSyncRulesProvider_003E) = 0;
			try
			{
				num = global::_003CModule_003E.GetSingleton((_GUID)global::_003CModule_003E._GUID_6dd7146d_7a19_4fbb_9235_9e6c382fcc71, (void**)(&cComPtrNtv_003CIMetadataManager_003E));
				if (num >= 0)
				{
					num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, _GUID, void**, int>)(int)(*(uint*)(*(int*)(int)(*(uint*)(&cComPtrNtv_003CIMetadataManager_003E)) + 12)))((IntPtr)(*(int*)(&cComPtrNtv_003CIMetadataManager_003E)), (_GUID)global::_003CModule_003E._GUID_b12dc962_cc1b_46c5_a92a_68f1f2b9bff3, (void**)(&cComPtrNtv_003CIDeviceSyncRulesProvider_003E));
					if (num >= 0)
					{
						num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, int, ESyncCategory, ESyncMode, int>)(int)(*(uint*)(*(int*)(int)(*(uint*)(&cComPtrNtv_003CIDeviceSyncRulesProvider_003E)) + 68)))((IntPtr)(*(int*)(&cComPtrNtv_003CIDeviceSyncRulesProvider_003E)), m_iDeviceID, cat, mode);
						if (num >= 0)
						{
							IEndpointHost* p = m_spEndpointHost.p;
							num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, EEndpointHostAction, int>)(int)(*(uint*)(*(int*)p + 148)))((nint)p, EEndpointHostAction.eEndpointHostActionResetGasGauge);
						}
					}
				}
				if (global::_003CModule_003E.WPP_GLOBAL_Control != Unsafe.AsPointer(ref global::_003CModule_003E.WPP_GLOBAL_Control) && (((int*)global::_003CModule_003E.WPP_GLOBAL_Control)[15] & 0x200) != 0 && (uint)((byte*)global::_003CModule_003E.WPP_GLOBAL_Control)[57] >= 5u)
				{
					global::_003CModule_003E.WPP_SF_LLd(((ulong*)global::_003CModule_003E.WPP_GLOBAL_Control)[6], 31, (_GUID*)Unsafe.AsPointer(ref global::_003CModule_003E._003FA0x556d048d_002EWPP_SyncRulesAPI_cpp_Traceguids), (uint)cat, (uint)mode, num);
				}
			}
			catch
			{
				//try-fault
				global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<CComPtrNtv_003CIDeviceSyncRulesProvider_003E*, void>)(&global::_003CModule_003E.CComPtrNtv_003CIDeviceSyncRulesProvider_003E_002E_007Bdtor_007D), &cComPtrNtv_003CIDeviceSyncRulesProvider_003E);
				throw;
			}
			global::_003CModule_003E.CComPtrNtv_003CIDeviceSyncRulesProvider_003E_002ERelease(&cComPtrNtv_003CIDeviceSyncRulesProvider_003E);
		}
		catch
		{
			//try-fault
			global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<CComPtrNtv_003CIMetadataManager_003E*, void>)(&global::_003CModule_003E.CComPtrNtv_003CIMetadataManager_003E_002E_007Bdtor_007D), &cComPtrNtv_003CIMetadataManager_003E);
			throw;
		}
		global::_003CModule_003E.CComPtrNtv_003CIMetadataManager_003E_002ERelease(&cComPtrNtv_003CIMetadataManager_003E);
		return num;
	}

	public unsafe int GetDontSyncHatedContent(ref bool fDontSyncHatedContent)
	{
		CComPtrMgd_003CIEndpointHost_003E spEndpointHost = m_spEndpointHost;
		if (spEndpointHost.p == null)
		{
			global::_003CModule_003E._ZuneShipAssert(1002u, 554u);
			return -2147418113;
		}
		bool flag = false;
		IEndpointHost* p = spEndpointHost.p;
		int num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, EEndpointHostSetting, bool*, int>)(int)(*(uint*)(*(int*)p + 124)))((nint)p, EEndpointHostSetting.eEndpointHostSettingDontSyncHatedContent, &flag);
		if (num >= 0)
		{
			fDontSyncHatedContent = flag;
		}
		return num;
	}

	public unsafe int SetDontSyncHatedContent([MarshalAs(UnmanagedType.U1)] bool fDontSyncHatedContent)
	{
		IEndpointHost* p = m_spEndpointHost.p;
		if (p == null)
		{
			global::_003CModule_003E._ZuneShipAssert(1002u, 575u);
			return -2147418113;
		}
		return ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, EEndpointHostSetting, byte, int>)(int)(*(uint*)(*(int*)p + 140)))((nint)p, EEndpointHostSetting.eEndpointHostSettingDontSyncHatedContent, fDontSyncHatedContent ? ((byte)1) : ((byte)0));
	}

	public unsafe int GenerateSnapshot([MarshalAs(UnmanagedType.U1)] bool expandSyncAll, ref SyncRulesView syncRulesView)
	{
		CComPtrMgd_003CIEndpointHost_003E spEndpointHost = m_spEndpointHost;
		if (spEndpointHost.p == null)
		{
			global::_003CModule_003E._ZuneShipAssert(1002u, 592u);
			return -2147418113;
		}
		ISyncRulesView* ptr = null;
		EEndpointHostProperty propId = (expandSyncAll ? EEndpointHostProperty.eEndpointHostPropertyExpandedRulesView : EEndpointHostProperty.eEndpointHostPropertyCollapsedRulesView);
		int num = global::_003CModule_003E.GetInterfaceProperty_003Cstruct_0020IEndpointHost_002Cenum_0020EEndpointHostProperty_002Cstruct_0020ISyncRulesView_003E(spEndpointHost.p, propId, &ptr);
		if (num >= 0)
		{
			syncRulesView = new SyncRulesView(ptr);
		}
		if (null != ptr)
		{
			ISyncRulesView* intPtr = ptr;
			((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint>)(int)(*(uint*)(*(int*)intPtr + 8)))((nint)intPtr);
		}
		return num;
	}

	private unsafe int AddInternal(int[] rgIds, EMediaTypes mediaType, EDeviceSyncRuleType ruleType, [MarshalAs(UnmanagedType.U1)] bool fAutoSelectRuleType)
	{
		int num = 0;
		if (global::_003CModule_003E.WPP_GLOBAL_Control != Unsafe.AsPointer(ref global::_003CModule_003E.WPP_GLOBAL_Control) && (((int*)global::_003CModule_003E.WPP_GLOBAL_Control)[15] & 0x200) != 0 && (uint)((byte*)global::_003CModule_003E.WPP_GLOBAL_Control)[57] >= 5u)
		{
			global::_003CModule_003E.WPP_SF_(((ulong*)global::_003CModule_003E.WPP_GLOBAL_Control)[6], 22, (_GUID*)Unsafe.AsPointer(ref global::_003CModule_003E._003FA0x556d048d_002EWPP_SyncRulesAPI_cpp_Traceguids));
		}
		int num2 = 0;
		if (0 < (nint)rgIds.LongLength)
		{
			while (num >= 0)
			{
				num = global::_003CModule_003E.ZuneLibraryExports_002EAddDeviceSyncRule(ruleType, fAutoSelectRuleType, m_iDeviceID, mediaType, rgIds[num2]);
				if (num < 0)
				{
					break;
				}
				num2++;
				if (num2 >= (nint)rgIds.LongLength)
				{
					break;
				}
			}
		}
		if (global::_003CModule_003E.WPP_GLOBAL_Control != Unsafe.AsPointer(ref global::_003CModule_003E.WPP_GLOBAL_Control) && (((int*)global::_003CModule_003E.WPP_GLOBAL_Control)[15] & 0x200) != 0 && (uint)((byte*)global::_003CModule_003E.WPP_GLOBAL_Control)[57] >= 5u)
		{
			global::_003CModule_003E.WPP_SF_d(((ulong*)global::_003CModule_003E.WPP_GLOBAL_Control)[6], 23, (_GUID*)Unsafe.AsPointer(ref global::_003CModule_003E._003FA0x556d048d_002EWPP_SyncRulesAPI_cpp_Traceguids), num);
		}
		return num;
	}

	private unsafe int RemoveInternal(int[] rgIds, EMediaTypes mediaType, EDeviceSyncRuleType ruleType, [MarshalAs(UnmanagedType.U1)] bool fDeviceFolderIds)
	{
		int num = 0;
		if (global::_003CModule_003E.WPP_GLOBAL_Control != Unsafe.AsPointer(ref global::_003CModule_003E.WPP_GLOBAL_Control) && (((int*)global::_003CModule_003E.WPP_GLOBAL_Control)[15] & 0x200) != 0 && (uint)((byte*)global::_003CModule_003E.WPP_GLOBAL_Control)[57] >= 5u)
		{
			global::_003CModule_003E.WPP_SF_(((ulong*)global::_003CModule_003E.WPP_GLOBAL_Control)[6], 28, (_GUID*)Unsafe.AsPointer(ref global::_003CModule_003E._003FA0x556d048d_002EWPP_SyncRulesAPI_cpp_Traceguids));
		}
		if (rgIds != null)
		{
			fixed (int* ptr = &rgIds[0])
			{
				try
				{
					int* ptr2 = ptr;
					num = global::_003CModule_003E.ZuneLibraryExports_002EDeleteDeviceSyncRules(ruleType, m_iDeviceID, mediaType, ptr2, rgIds.Length, fDeviceFolderIds);
				}
				catch
				{
					//try-fault
					ptr = null;
					throw;
				}
			}
		}
		if (global::_003CModule_003E.WPP_GLOBAL_Control != Unsafe.AsPointer(ref global::_003CModule_003E.WPP_GLOBAL_Control) && (((int*)global::_003CModule_003E.WPP_GLOBAL_Control)[15] & 0x200) != 0 && (uint)((byte*)global::_003CModule_003E.WPP_GLOBAL_Control)[57] >= 5u)
		{
			global::_003CModule_003E.WPP_SF_d(((ulong*)global::_003CModule_003E.WPP_GLOBAL_Control)[6], 29, (_GUID*)Unsafe.AsPointer(ref global::_003CModule_003E._003FA0x556d048d_002EWPP_SyncRulesAPI_cpp_Traceguids), num);
		}
		return num;
	}

	protected virtual void Dispose([MarshalAs(UnmanagedType.U1)] bool P_0)
	{
		if (P_0)
		{
			try
			{
				_007ESyncRules();
				return;
			}
			finally
			{
				((IDisposable)m_spEndpointHost).Dispose();
			}
		}
		base.Finalize();
	}

	public virtual sealed void Dispose()
	{
		Dispose(true);
		GC.SuppressFinalize(this);
	}
}
