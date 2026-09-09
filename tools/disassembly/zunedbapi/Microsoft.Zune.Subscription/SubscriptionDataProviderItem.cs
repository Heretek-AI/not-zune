using System;
using System.Runtime.CompilerServices;
using _003CCppImplementationDetails_003E;
using Microsoft.Iris;

namespace Microsoft.Zune.Subscription;

public class SubscriptionDataProviderItem : DataProviderObject
{
	private string m_feedUrl;

	private int m_nSeriesId = -1;

	private int m_nEpisodeId = -1;

	private EItemDownloadState m_eLastDownloadState = EItemDownloadState.eDownloadStateNone;

	private unsafe IMSMediaSchemaPropertySet* m_pEpisodePropertySet;

	public unsafe SubscriptionDataProviderItem(DataProviderQuery owner, object typeCookie, string feedUrl, IMSMediaSchemaPropertySet* pEpisodePropertySet)
		: base(owner, typeCookie)
	{
		m_feedUrl = feedUrl;
		m_pEpisodePropertySet = pEpisodePropertySet;
		((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint>)(int)(*(uint*)(*(int*)pEpisodePropertySet + 4)))((nint)pEpisodePropertySet);
		BindToLocalEpisode();
	}

	public unsafe override object GetProperty(string propertyName)
	{
		Unsafe.SkipInit(out _0024ArrayType_0024_0024_0024BY07UPROPERTY_TO_PID_MAP_0040Subscription_0040Zune_0040Microsoft_0040_0040 _0024ArrayType_0024_0024_0024BY07UPROPERTY_TO_PID_MAP_0040Subscription_0040Zune_0040Microsoft_0040_00402);
		*(int*)(&_0024ArrayType_0024_0024_0024BY07UPROPERTY_TO_PID_MAP_0040Subscription_0040Zune_0040Microsoft_0040_00402) = (int)Unsafe.AsPointer(ref global::_003CModule_003E._003F_003F_C_0040_1M_0040MNHBCACD_0040_003F_0024AAT_003F_0024AAi_003F_0024AAt_003F_0024AAl_003F_0024AAe_003F_0024AA_003F_0024AA_0040);
		Unsafe.As<_0024ArrayType_0024_0024_0024BY07UPROPERTY_TO_PID_MAP_0040Subscription_0040Zune_0040Microsoft_0040_0040, int>(ref Unsafe.AddByteOffset(ref _0024ArrayType_0024_0024_0024BY07UPROPERTY_TO_PID_MAP_0040Subscription_0040Zune_0040Microsoft_0040_00402, 4)) = 16797697;
		Unsafe.As<_0024ArrayType_0024_0024_0024BY07UPROPERTY_TO_PID_MAP_0040Subscription_0040Zune_0040Microsoft_0040_0040, int>(ref Unsafe.AddByteOffset(ref _0024ArrayType_0024_0024_0024BY07UPROPERTY_TO_PID_MAP_0040Subscription_0040Zune_0040Microsoft_0040_00402, 8)) = (int)Unsafe.AsPointer(ref global::_003CModule_003E._003F_003F_C_0040_1BI_0040DLMANABL_0040_003F_0024AAD_003F_0024AAe_003F_0024AAs_003F_0024AAc_003F_0024AAr_003F_0024AAi_003F_0024AAp_003F_0024AAt_003F_0024AAi_003F_0024AAo_003F_0024AAn_003F_0024AA_003F_0024AA_0040);
		Unsafe.As<_0024ArrayType_0024_0024_0024BY07UPROPERTY_TO_PID_MAP_0040Subscription_0040Zune_0040Microsoft_0040_0040, int>(ref Unsafe.AddByteOffset(ref _0024ArrayType_0024_0024_0024BY07UPROPERTY_TO_PID_MAP_0040Subscription_0040Zune_0040Microsoft_0040_00402, 12)) = 134238211;
		Unsafe.As<_0024ArrayType_0024_0024_0024BY07UPROPERTY_TO_PID_MAP_0040Subscription_0040Zune_0040Microsoft_0040_0040, int>(ref Unsafe.AddByteOffset(ref _0024ArrayType_0024_0024_0024BY07UPROPERTY_TO_PID_MAP_0040Subscription_0040Zune_0040Microsoft_0040_00402, 16)) = (int)Unsafe.AsPointer(ref global::_003CModule_003E._003F_003F_C_0040_1BC_0040CGFFANJJ_0040_003F_0024AAD_003F_0024AAu_003F_0024AAr_003F_0024AAa_003F_0024AAt_003F_0024AAi_003F_0024AAo_003F_0024AAn_003F_0024AA_003F_0024AA_0040);
		Unsafe.As<_0024ArrayType_0024_0024_0024BY07UPROPERTY_TO_PID_MAP_0040Subscription_0040Zune_0040Microsoft_0040_0040, int>(ref Unsafe.AddByteOffset(ref _0024ArrayType_0024_0024_0024BY07UPROPERTY_TO_PID_MAP_0040Subscription_0040Zune_0040Microsoft_0040_00402, 20)) = 16797701;
		Unsafe.As<_0024ArrayType_0024_0024_0024BY07UPROPERTY_TO_PID_MAP_0040Subscription_0040Zune_0040Microsoft_0040_0040, int>(ref Unsafe.AddByteOffset(ref _0024ArrayType_0024_0024_0024BY07UPROPERTY_TO_PID_MAP_0040Subscription_0040Zune_0040Microsoft_0040_00402, 24)) = (int)Unsafe.AsPointer(ref global::_003CModule_003E._003F_003F_C_0040_1BI_0040IMGEIAFE_0040_003F_0024AAR_003F_0024AAe_003F_0024AAl_003F_0024AAe_003F_0024AAa_003F_0024AAs_003F_0024AAe_003F_0024AAD_003F_0024AAa_003F_0024AAt_003F_0024AAe_003F_0024AA_003F_0024AA_0040);
		Unsafe.As<_0024ArrayType_0024_0024_0024BY07UPROPERTY_TO_PID_MAP_0040Subscription_0040Zune_0040Microsoft_0040_0040, int>(ref Unsafe.AddByteOffset(ref _0024ArrayType_0024_0024_0024BY07UPROPERTY_TO_PID_MAP_0040Subscription_0040Zune_0040Microsoft_0040_00402, 28)) = 150995204;
		Unsafe.As<_0024ArrayType_0024_0024_0024BY07UPROPERTY_TO_PID_MAP_0040Subscription_0040Zune_0040Microsoft_0040_0040, int>(ref Unsafe.AddByteOffset(ref _0024ArrayType_0024_0024_0024BY07UPROPERTY_TO_PID_MAP_0040Subscription_0040Zune_0040Microsoft_0040_00402, 32)) = (int)Unsafe.AsPointer(ref global::_003CModule_003E._003F_003F_C_0040_1BC_0040BFOBHOBE_0040_003F_0024AAE_003F_0024AAx_003F_0024AAp_003F_0024AAl_003F_0024AAi_003F_0024AAc_003F_0024AAi_003F_0024AAt_003F_0024AA_003F_0024AA_0040);
		Unsafe.As<_0024ArrayType_0024_0024_0024BY07UPROPERTY_TO_PID_MAP_0040Subscription_0040Zune_0040Microsoft_0040_0040, int>(ref Unsafe.AddByteOffset(ref _0024ArrayType_0024_0024_0024BY07UPROPERTY_TO_PID_MAP_0040Subscription_0040Zune_0040Microsoft_0040_00402, 36)) = 83906566;
		Unsafe.As<_0024ArrayType_0024_0024_0024BY07UPROPERTY_TO_PID_MAP_0040Subscription_0040Zune_0040Microsoft_0040_0040, int>(ref Unsafe.AddByteOffset(ref _0024ArrayType_0024_0024_0024BY07UPROPERTY_TO_PID_MAP_0040Subscription_0040Zune_0040Microsoft_0040_00402, 40)) = (int)Unsafe.AsPointer(ref global::_003CModule_003E._003F_003F_C_0040_1O_0040IKCDCNCP_0040_003F_0024AAA_003F_0024AAu_003F_0024AAt_003F_0024AAh_003F_0024AAo_003F_0024AAr_003F_0024AA_003F_0024AA_0040);
		Unsafe.As<_0024ArrayType_0024_0024_0024BY07UPROPERTY_TO_PID_MAP_0040Subscription_0040Zune_0040Microsoft_0040_0040, int>(ref Unsafe.AddByteOffset(ref _0024ArrayType_0024_0024_0024BY07UPROPERTY_TO_PID_MAP_0040Subscription_0040Zune_0040Microsoft_0040_00402, 44)) = 16797704;
		Unsafe.As<_0024ArrayType_0024_0024_0024BY07UPROPERTY_TO_PID_MAP_0040Subscription_0040Zune_0040Microsoft_0040_0040, int>(ref Unsafe.AddByteOffset(ref _0024ArrayType_0024_0024_0024BY07UPROPERTY_TO_PID_MAP_0040Subscription_0040Zune_0040Microsoft_0040_00402, 48)) = (int)Unsafe.AsPointer(ref global::_003CModule_003E._003F_003F_C_0040_1BK_0040DIFENCED_0040_003F_0024AAE_003F_0024AAn_003F_0024AAc_003F_0024AAl_003F_0024AAo_003F_0024AAs_003F_0024AAu_003F_0024AAr_003F_0024AAe_003F_0024AAU_003F_0024AAr_003F_0024AAl_003F_0024AA_003F_0024AA_0040);
		Unsafe.As<_0024ArrayType_0024_0024_0024BY07UPROPERTY_TO_PID_MAP_0040Subscription_0040Zune_0040Microsoft_0040_0040, int>(ref Unsafe.AddByteOffset(ref _0024ArrayType_0024_0024_0024BY07UPROPERTY_TO_PID_MAP_0040Subscription_0040Zune_0040Microsoft_0040_00402, 52)) = 134238215;
		Unsafe.As<_0024ArrayType_0024_0024_0024BY07UPROPERTY_TO_PID_MAP_0040Subscription_0040Zune_0040Microsoft_0040_0040, int>(ref Unsafe.AddByteOffset(ref _0024ArrayType_0024_0024_0024BY07UPROPERTY_TO_PID_MAP_0040Subscription_0040Zune_0040Microsoft_0040_00402, 56)) = (int)Unsafe.AsPointer(ref global::_003CModule_003E._003F_003F_C_0040_1CC_0040PFACMMFM_0040_003F_0024AAE_003F_0024AAp_003F_0024AAi_003F_0024AAs_003F_0024AAo_003F_0024AAd_003F_0024AAe_003F_0024AAM_003F_0024AAe_003F_0024AAd_003F_0024AAi_003F_0024AAa_003F_0024AAT_003F_0024AAy_003F_0024AAp_003F_0024AAe_003F_0024AA_003F_0024AA_0040);
		Unsafe.As<_0024ArrayType_0024_0024_0024BY07UPROPERTY_TO_PID_MAP_0040Subscription_0040Zune_0040Microsoft_0040_0040, int>(ref Unsafe.AddByteOffset(ref _0024ArrayType_0024_0024_0024BY07UPROPERTY_TO_PID_MAP_0040Subscription_0040Zune_0040Microsoft_0040_00402, 60)) = 100683786;
		int num = 0;
		Unsafe.SkipInit(out CComPropVariant cComPropVariant);
		// IL initblk instruction
		Unsafe.InitBlock(ref cComPropVariant, 0, 16);
		object result;
		try
		{
			fixed (ushort* ptr = &Unsafe.As<char, ushort>(ref global::_003CModule_003E.PtrToStringChars(propertyName)))
			{
				if (global::_003CModule_003E._wcsicmp(ptr, (ushort*)Unsafe.AsPointer(ref global::_003CModule_003E._003F_003F_C_0040_1BE_0040NCILDCLH_0040_003F_0024AAL_003F_0024AAi_003F_0024AAb_003F_0024AAr_003F_0024AAa_003F_0024AAr_003F_0024AAy_003F_0024AAI_003F_0024AAd_003F_0024AA_003F_0024AA_0040)) == 0)
				{
					Unsafe.As<CComPropVariant, int>(ref Unsafe.AddByteOffset(ref cComPropVariant, 8)) = m_nEpisodeId;
					*(short*)(&cComPropVariant) = 3;
				}
				else if (global::_003CModule_003E._wcsicmp(ptr, (ushort*)Unsafe.AsPointer(ref global::_003CModule_003E._003F_003F_C_0040_1BC_0040JCKMNCPP_0040_003F_0024AAS_003F_0024AAe_003F_0024AAr_003F_0024AAi_003F_0024AAe_003F_0024AAs_003F_0024AAI_003F_0024AAd_003F_0024AA_003F_0024AA_0040)) == 0)
				{
					Unsafe.As<CComPropVariant, int>(ref Unsafe.AddByteOffset(ref cComPropVariant, 8)) = m_nSeriesId;
					*(short*)(&cComPropVariant) = 3;
				}
				else
				{
					if (global::_003CModule_003E._wcsicmp(ptr, (ushort*)Unsafe.AsPointer(ref global::_003CModule_003E._003F_003F_C_0040_1BM_0040BGELMDBM_0040_003F_0024AAD_003F_0024AAo_003F_0024AAw_003F_0024AAn_003F_0024AAl_003F_0024AAo_003F_0024AAa_003F_0024AAd_003F_0024AAS_003F_0024AAt_003F_0024AAa_003F_0024AAt_003F_0024AAe_003F_0024AA_003F_0024AA_0040)) == 0)
					{
						int nEpisodeId = m_nEpisodeId;
						if (nEpisodeId >= 0)
						{
							num = GetDatabaseValue(nEpisodeId, 145u, (tagPROPVARIANT*)(&cComPropVariant));
							if (num < 0)
							{
								goto IL_02a2;
							}
							if (m_eLastDownloadState != (EItemDownloadState)Unsafe.As<CComPropVariant, int>(ref Unsafe.AddByteOffset(ref cComPropVariant, 8)))
							{
								InvalidateDownloadState();
							}
							m_eLastDownloadState = Unsafe.As<CComPropVariant, EItemDownloadState>(ref Unsafe.AddByteOffset(ref cComPropVariant, 8));
						}
						else
						{
							Unsafe.As<CComPropVariant, int>(ref Unsafe.AddByteOffset(ref cComPropVariant, 8)) = 0;
							*(short*)(&cComPropVariant) = 3;
						}
					}
					else if (global::_003CModule_003E._wcsicmp(ptr, (ushort*)Unsafe.AsPointer(ref global::_003CModule_003E._003F_003F_C_0040_1BK_0040EPGEAFPP_0040_003F_0024AAD_003F_0024AAo_003F_0024AAw_003F_0024AAn_003F_0024AAl_003F_0024AAo_003F_0024AAa_003F_0024AAd_003F_0024AAT_003F_0024AAy_003F_0024AAp_003F_0024AAe_003F_0024AA_003F_0024AA_0040)) == 0)
					{
						int nEpisodeId2 = m_nEpisodeId;
						if (nEpisodeId2 >= 0)
						{
							num = GetDatabaseValue(nEpisodeId2, 146u, (tagPROPVARIANT*)(&cComPropVariant));
						}
						else
						{
							Unsafe.As<CComPropVariant, int>(ref Unsafe.AddByteOffset(ref cComPropVariant, 8)) = 0;
							*(short*)(&cComPropVariant) = 3;
						}
					}
					else if (global::_003CModule_003E._wcsicmp(ptr, (ushort*)Unsafe.AsPointer(ref global::_003CModule_003E._003F_003F_C_0040_1BK_0040JJCAOHOO_0040_003F_0024AAP_003F_0024AAl_003F_0024AAa_003F_0024AAy_003F_0024AAe_003F_0024AAd_003F_0024AAS_003F_0024AAt_003F_0024AAa_003F_0024AAt_003F_0024AAu_003F_0024AAs_003F_0024AA_003F_0024AA_0040)) == 0)
					{
						*(short*)(&cComPropVariant) = 1;
						int nEpisodeId3 = m_nEpisodeId;
						if (nEpisodeId3 < 0)
						{
							goto IL_027e;
						}
						num = GetDatabaseValue(nEpisodeId3, 262u, (tagPROPVARIANT*)(&cComPropVariant));
					}
					else if (global::_003CModule_003E._wcsicmp(ptr, (ushort*)Unsafe.AsPointer(ref global::_003CModule_003E._003F_003F_C_0040_1BE_0040HHOJDPEL_0040_003F_0024AAS_003F_0024AAo_003F_0024AAu_003F_0024AAr_003F_0024AAc_003F_0024AAe_003F_0024AAU_003F_0024AAr_003F_0024AAl_003F_0024AA_003F_0024AA_0040)) == 0)
					{
						*(short*)(&cComPropVariant) = 1;
						int nEpisodeId4 = m_nEpisodeId;
						if (nEpisodeId4 < 0)
						{
							goto IL_027e;
						}
						num = GetDatabaseValue(nEpisodeId4, 317u, (tagPROPVARIANT*)(&cComPropVariant));
					}
					else if (global::_003CModule_003E._wcsicmp(ptr, (ushort*)Unsafe.AsPointer(ref global::_003CModule_003E._003F_003F_C_0040_1CE_0040ELHJHONN_0040_003F_0024AAD_003F_0024AAo_003F_0024AAw_003F_0024AAn_003F_0024AAl_003F_0024AAo_003F_0024AAa_003F_0024AAd_003F_0024AAE_003F_0024AAr_003F_0024AAr_003F_0024AAo_003F_0024AAr_003F_0024AAC_003F_0024AAo_003F_0024AAd_003F_0024AAe_003F_0024AA_003F_0024AA_0040)) == 0)
					{
						*(short*)(&cComPropVariant) = 1;
						int nEpisodeId5 = m_nEpisodeId;
						if (nEpisodeId5 < 0)
						{
							goto IL_027e;
						}
						num = GetDatabaseValue(nEpisodeId5, 144u, (tagPROPVARIANT*)(&cComPropVariant));
					}
					else
					{
						int num2 = 0;
						while (global::_003CModule_003E._wcsicmp(ptr, (ushort*)(int)(*(uint*)((ref *(_003F*)(num2 * 8)) + (ref *(_003F*)(&_0024ArrayType_0024_0024_0024BY07UPROPERTY_TO_PID_MAP_0040Subscription_0040Zune_0040Microsoft_0040_00402))))) != 0)
						{
							num2++;
							if ((uint)num2 < 8u)
							{
								continue;
							}
							goto IL_027e;
						}
						uint num3 = *(uint*)((ref *(_003F*)(num2 * 8)) + (ref Unsafe.As<_0024ArrayType_0024_0024_0024BY07UPROPERTY_TO_PID_MAP_0040Subscription_0040Zune_0040Microsoft_0040_0040, _003F>(ref Unsafe.AddByteOffset(ref _0024ArrayType_0024_0024_0024BY07UPROPERTY_TO_PID_MAP_0040Subscription_0040Zune_0040Microsoft_0040_00402, 4))));
						IMSMediaSchemaPropertySet* pEpisodePropertySet = m_pEpisodePropertySet;
						num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint, uint, tagPROPVARIANT*, int>)(int)(*(uint*)(*(int*)pEpisodePropertySet + 24)))((nint)pEpisodePropertySet, num3, 0u, (tagPROPVARIANT*)(&cComPropVariant));
					}
					if (num < 0)
					{
						goto IL_02a2;
					}
				}
				goto IL_027e;
				IL_02a2:
				result = ((DataProviderObject)this).Mappings[propertyName].DefaultValue;
				goto end_IL_00c6;
				IL_027e:
				if (global::_003CModule_003E.CComPropVariant_002EIsNullOrEmpty(&cComPropVariant))
				{
					goto IL_02a2;
				}
				result = SubscriptionDataProviderQueryResult.ConvertVariantToType(((DataProviderObject)this).Mappings[propertyName].PropertyTypeName, &cComPropVariant);
				end_IL_00c6:;
			}
		}
		catch
		{
			//try-fault
			global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<CComPropVariant*, void>)(&global::_003CModule_003E.CComPropVariant_002E_007Bdtor_007D), &cComPropVariant);
			throw;
		}
		global::_003CModule_003E.CComPropVariant_002EClear(&cComPropVariant);
		return result;
	}

	public unsafe override void SetProperty(string propertyName, object value)
	{
		if (m_nEpisodeId > 0 && propertyName == "DownloadErrorCode")
		{
			Unsafe.SkipInit(out tagVARIANT tagVARIANT2);
			*(short*)(&tagVARIANT2) = 3;
			Unsafe.As<tagVARIANT, int>(ref Unsafe.AddByteOffset(ref tagVARIANT2, 8)) = (int)value;
			Unsafe.SkipInit(out DBPropertySubmitStruct dBPropertySubmitStruct);
			*(int*)(&dBPropertySubmitStruct) = 144;
			Unsafe.As<DBPropertySubmitStruct, int>(ref Unsafe.AddByteOffset(ref dBPropertySubmitStruct, 4)) = (int)(&tagVARIANT2);
			int num = global::_003CModule_003E.ZuneLibraryExports_002ESetFieldValues(m_nEpisodeId, EListType.ePodcastEpisodeList, 1, &dBPropertySubmitStruct, null);
			if (0 == num)
			{
				((DataProviderObject)this).FirePropertyChanged(propertyName);
			}
		}
	}

	public unsafe virtual void SaveToLibrary()
	{
		IService* ptr = null;
		ISubscriptionManager* ptr2 = null;
		int num = 1;
		int nSeriesId = -1;
		int nEpisodeId = -1;
		int singleton = global::_003CModule_003E.GetSingleton((_GUID)global::_003CModule_003E._GUID_bb2d1edd_1bd5_4be1_8d38_36d4f0849911, (void**)(&ptr));
		if (singleton >= 0)
		{
			singleton = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, _GUID*, int*, int>)(int)(*(uint*)(*(int*)ptr + 192)))((nint)ptr, null, &num);
			if (singleton >= 0)
			{
				singleton = global::_003CModule_003E.GetSingleton((_GUID)global::_003CModule_003E._GUID_9dc7c984_41d5_4130_a5ac_46d0825cd29d, (void**)(&ptr2));
				if (singleton >= 0)
				{
					singleton = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, int, EMediaTypes, IMSMediaSchemaPropertySet*, int*, int*, int>)(int)(*(uint*)(*(int*)ptr2 + 68)))((nint)ptr2, num, EMediaTypes.eMediaTypePodcastSeries, m_pEpisodePropertySet, &nSeriesId, &nEpisodeId);
					if (singleton >= 0)
					{
						m_nSeriesId = nSeriesId;
						m_nEpisodeId = nEpisodeId;
						((DataProviderObject)this).FirePropertyChanged("DownloadState");
					}
				}
			}
		}
		if (null != ptr)
		{
			IService* intPtr = ptr;
			((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint>)(int)(*(uint*)(*(int*)intPtr + 8)))((nint)intPtr);
			ptr = null;
		}
		if (null != ptr2)
		{
			ISubscriptionManager* intPtr2 = ptr2;
			((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint>)(int)(*(uint*)(*(int*)intPtr2 + 8)))((nint)intPtr2);
		}
	}

	internal unsafe void OnDispose()
	{
		IMSMediaSchemaPropertySet* pEpisodePropertySet = m_pEpisodePropertySet;
		if (null != pEpisodePropertySet)
		{
			((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint>)(int)(*(uint*)(*(int*)pEpisodePropertySet + 8)))((nint)pEpisodePropertySet);
			m_pEpisodePropertySet = null;
		}
	}

	private unsafe int BindToLocalEpisode()
	{
		ISubscriptionManager* ptr = null;
		Unsafe.SkipInit(out CComPropVariant cComPropVariant);
		// IL initblk instruction
		Unsafe.InitBlock(ref cComPropVariant, 0, 16);
		int num2;
		try
		{
			Unsafe.SkipInit(out CComPropVariant cComPropVariant2);
			// IL initblk instruction
			Unsafe.InitBlock(ref cComPropVariant2, 0, 16);
			try
			{
				int num = -1;
				fixed (ushort* ptr2 = &Unsafe.As<char, ushort>(ref global::_003CModule_003E.PtrToStringChars(m_feedUrl)))
				{
					num2 = global::_003CModule_003E.GetSingleton((_GUID)global::_003CModule_003E._GUID_9dc7c984_41d5_4130_a5ac_46d0825cd29d, (void**)(&ptr));
					if (num2 >= 0)
					{
						IMSMediaSchemaPropertySet* pEpisodePropertySet = m_pEpisodePropertySet;
						num2 = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint, uint, tagPROPVARIANT*, int>)(int)(*(uint*)(*(int*)pEpisodePropertySet + 24)))((nint)pEpisodePropertySet, 16797697u, 0u, (tagPROPVARIANT*)(&cComPropVariant));
						if (num2 >= 0)
						{
							IMSMediaSchemaPropertySet* pEpisodePropertySet2 = m_pEpisodePropertySet;
							num2 = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint, uint, tagPROPVARIANT*, int>)(int)(*(uint*)(*(int*)pEpisodePropertySet2 + 24)))((nint)pEpisodePropertySet2, 134238215u, 0u, (tagPROPVARIANT*)(&cComPropVariant2));
							if (num2 >= 0)
							{
								int num3 = *(int*)ptr + 80;
								num2 = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, EMediaTypes, ushort*, ushort*, ushort*, int*, int>)(int)(*(uint*)num3))((nint)ptr, EMediaTypes.eMediaTypePodcastSeries, ptr2, (ushort*)(int)Unsafe.As<CComPropVariant, uint>(ref Unsafe.AddByteOffset(ref cComPropVariant, 8)), (ushort*)(int)Unsafe.As<CComPropVariant, uint>(ref Unsafe.AddByteOffset(ref cComPropVariant2, 8)), &num);
							}
						}
					}
					if (0 == num2)
					{
						Unsafe.SkipInit(out tagPROPVARIANT tagPROPVARIANT2);
						// IL initblk instruction
						Unsafe.InitBlock(ref tagPROPVARIANT2, 0, 16);
						num2 = GetDatabaseValue(num, 311u, &tagPROPVARIANT2);
						if (num2 >= 0)
						{
							m_nEpisodeId = num;
							m_nSeriesId = Unsafe.As<tagPROPVARIANT, int>(ref Unsafe.AddByteOffset(ref tagPROPVARIANT2, 8));
						}
					}
					if (null != ptr)
					{
						ISubscriptionManager* intPtr = ptr;
						((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint>)(int)(*(uint*)(*(int*)intPtr + 8)))((nint)intPtr);
						ptr = null;
					}
				}
			}
			catch
			{
				//try-fault
				global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<CComPropVariant*, void>)(&global::_003CModule_003E.CComPropVariant_002E_007Bdtor_007D), &cComPropVariant2);
				throw;
			}
			global::_003CModule_003E.CComPropVariant_002EClear(&cComPropVariant2);
		}
		catch
		{
			//try-fault
			global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<CComPropVariant*, void>)(&global::_003CModule_003E.CComPropVariant_002E_007Bdtor_007D), &cComPropVariant);
			throw;
		}
		global::_003CModule_003E.CComPropVariant_002EClear(&cComPropVariant);
		return num2;
	}

	private int InvalidateDownloadState()
	{
		((DataProviderObject)this).FirePropertyChanged("DownloadState");
		((DataProviderObject)this).FirePropertyChanged("DownloadType");
		((DataProviderObject)this).FirePropertyChanged("DownloadErrorCode");
		return 0;
	}

	private unsafe int GetDatabaseValue(int nMediaId, uint dwAtom, tagPROPVARIANT* pvarValue)
	{
		if (pvarValue == null)
		{
			global::_003CModule_003E._ZuneShipAssert(1001u, 633u);
			return -2147467261;
		}
		IService* ptr = null;
		int num = 1;
		int num2 = global::_003CModule_003E.GetSingleton((_GUID)global::_003CModule_003E._GUID_bb2d1edd_1bd5_4be1_8d38_36d4f0849911, (void**)(&ptr));
		if (num2 >= 0)
		{
			num2 = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, _GUID*, int*, int>)(int)(*(uint*)(*(int*)ptr + 192)))((nint)ptr, null, &num);
		}
		IQueryPropertyBag* ptr2 = null;
		Unsafe.SkipInit(out DBPropertyRequestStruct dBPropertyRequestStruct);
		global::_003CModule_003E.DBPropertyRequestStruct_002E_007Bctor_007D(&dBPropertyRequestStruct, dwAtom);
		try
		{
			if (num2 >= 0)
			{
				num2 = global::_003CModule_003E.ZuneLibraryExports_002ECreatePropertyBag(&ptr2);
				if (num2 >= 0)
				{
					num2 = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, EQueryPropertyBagProp, int, int>)(int)(*(uint*)(*(int*)ptr2 + 28)))((nint)ptr2, (EQueryPropertyBagProp)0, num);
					if (num2 >= 0)
					{
						num2 = global::_003CModule_003E.ZuneLibraryExports_002EGetFieldValues(nMediaId, EListType.ePodcastEpisodeList, 1, &dBPropertyRequestStruct, ptr2);
						if (num2 >= 0)
						{
							num2 = ((Unsafe.As<DBPropertyRequestStruct, int>(ref Unsafe.AddByteOffset(ref dBPropertyRequestStruct, 4)) < 0) ? Unsafe.As<DBPropertyRequestStruct, int>(ref Unsafe.AddByteOffset(ref dBPropertyRequestStruct, 4)) : global::_003CModule_003E.PropVariantCopy(pvarValue, (tagPROPVARIANT*)Unsafe.AsPointer(ref Unsafe.AddByteOffset(ref dBPropertyRequestStruct, 8))));
						}
					}
				}
			}
			if (null != ptr2)
			{
				IQueryPropertyBag* intPtr = ptr2;
				((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint>)(int)(*(uint*)(*(int*)intPtr + 8)))((nint)intPtr);
				ptr2 = null;
			}
			if (null != ptr)
			{
				IService* intPtr2 = ptr;
				((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint>)(int)(*(uint*)(*(int*)intPtr2 + 8)))((nint)intPtr2);
				ptr = null;
			}
		}
		catch
		{
			//try-fault
			global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<DBPropertyRequestStruct*, void>)(&global::_003CModule_003E.DBPropertyRequestStruct_002E_007Bdtor_007D), &dBPropertyRequestStruct);
			throw;
		}
		global::_003CModule_003E.DBPropertyRequestStruct_002E_007Bdtor_007D(&dBPropertyRequestStruct);
		return num2;
	}
}
