using System;
using System.Runtime.CompilerServices;
using _003CCppImplementationDetails_003E;
using Microsoft.Iris;

namespace Microsoft.Zune.Subscription;

public unsafe class SubscriptionSeriesInfo(DataProviderQuery owner, object typeCookie, string serviceId) : DataProviderObject(owner, typeCookie)
{
	private string m_serviceId = serviceId;

	private ESubscriptionState m_eSubscriptionState;

	private unsafe IMSMediaSchemaPropertySet* m_pSeriesPropertySet = null;

	public unsafe override object GetProperty(string propertyName)
	{
		Unsafe.SkipInit(out _0024ArrayType_0024_0024_0024BY09UPROPERTY_TO_PID_MAP_0040Subscription_0040Zune_0040Microsoft_0040_0040 _0024ArrayType_0024_0024_0024BY09UPROPERTY_TO_PID_MAP_0040Subscription_0040Zune_0040Microsoft_0040_00402);
		*(int*)(&_0024ArrayType_0024_0024_0024BY09UPROPERTY_TO_PID_MAP_0040Subscription_0040Zune_0040Microsoft_0040_00402) = (int)Unsafe.AsPointer(ref global::_003CModule_003E._003F_003F_C_0040_1BA_0040CEPEGMDB_0040_003F_0024AAF_003F_0024AAe_003F_0024AAe_003F_0024AAd_003F_0024AAU_003F_0024AAr_003F_0024AAl_003F_0024AA_003F_0024AA_0040);
		Unsafe.As<_0024ArrayType_0024_0024_0024BY09UPROPERTY_TO_PID_MAP_0040Subscription_0040Zune_0040Microsoft_0040_0040, int>(ref Unsafe.AddByteOffset(ref _0024ArrayType_0024_0024_0024BY09UPROPERTY_TO_PID_MAP_0040Subscription_0040Zune_0040Microsoft_0040_00402, 4)) = 134217987;
		Unsafe.As<_0024ArrayType_0024_0024_0024BY09UPROPERTY_TO_PID_MAP_0040Subscription_0040Zune_0040Microsoft_0040_0040, int>(ref Unsafe.AddByteOffset(ref _0024ArrayType_0024_0024_0024BY09UPROPERTY_TO_PID_MAP_0040Subscription_0040Zune_0040Microsoft_0040_00402, 8)) = (int)Unsafe.AsPointer(ref global::_003CModule_003E._003F_003F_C_0040_1BE_0040ILMBBNLH_0040_003F_0024AAE_003F_0024AAr_003F_0024AAr_003F_0024AAo_003F_0024AAr_003F_0024AAC_003F_0024AAo_003F_0024AAd_003F_0024AAe_003F_0024AA_003F_0024AA_0040);
		Unsafe.As<_0024ArrayType_0024_0024_0024BY09UPROPERTY_TO_PID_MAP_0040Subscription_0040Zune_0040Microsoft_0040_0040, int>(ref Unsafe.AddByteOffset(ref _0024ArrayType_0024_0024_0024BY09UPROPERTY_TO_PID_MAP_0040Subscription_0040Zune_0040Microsoft_0040_00402, 12)) = 100663562;
		Unsafe.As<_0024ArrayType_0024_0024_0024BY09UPROPERTY_TO_PID_MAP_0040Subscription_0040Zune_0040Microsoft_0040_0040, int>(ref Unsafe.AddByteOffset(ref _0024ArrayType_0024_0024_0024BY09UPROPERTY_TO_PID_MAP_0040Subscription_0040Zune_0040Microsoft_0040_00402, 16)) = (int)Unsafe.AsPointer(ref global::_003CModule_003E._003F_003F_C_0040_1M_0040MNHBCACD_0040_003F_0024AAT_003F_0024AAi_003F_0024AAt_003F_0024AAl_003F_0024AAe_003F_0024AA_003F_0024AA_0040);
		Unsafe.As<_0024ArrayType_0024_0024_0024BY09UPROPERTY_TO_PID_MAP_0040Subscription_0040Zune_0040Microsoft_0040_0040, int>(ref Unsafe.AddByteOffset(ref _0024ArrayType_0024_0024_0024BY09UPROPERTY_TO_PID_MAP_0040Subscription_0040Zune_0040Microsoft_0040_00402, 20)) = 16801793;
		Unsafe.As<_0024ArrayType_0024_0024_0024BY09UPROPERTY_TO_PID_MAP_0040Subscription_0040Zune_0040Microsoft_0040_0040, int>(ref Unsafe.AddByteOffset(ref _0024ArrayType_0024_0024_0024BY09UPROPERTY_TO_PID_MAP_0040Subscription_0040Zune_0040Microsoft_0040_00402, 24)) = (int)Unsafe.AsPointer(ref global::_003CModule_003E._003F_003F_C_0040_1BA_0040DPIPFNMB_0040_003F_0024AAH_003F_0024AAo_003F_0024AAm_003F_0024AAe_003F_0024AAU_003F_0024AAr_003F_0024AAl_003F_0024AA_003F_0024AA_0040);
		Unsafe.As<_0024ArrayType_0024_0024_0024BY09UPROPERTY_TO_PID_MAP_0040Subscription_0040Zune_0040Microsoft_0040_0040, int>(ref Unsafe.AddByteOffset(ref _0024ArrayType_0024_0024_0024BY09UPROPERTY_TO_PID_MAP_0040Subscription_0040Zune_0040Microsoft_0040_00402, 28)) = 134242316;
		Unsafe.As<_0024ArrayType_0024_0024_0024BY09UPROPERTY_TO_PID_MAP_0040Subscription_0040Zune_0040Microsoft_0040_0040, int>(ref Unsafe.AddByteOffset(ref _0024ArrayType_0024_0024_0024BY09UPROPERTY_TO_PID_MAP_0040Subscription_0040Zune_0040Microsoft_0040_00402, 32)) = (int)Unsafe.AsPointer(ref global::_003CModule_003E._003F_003F_C_0040_1O_0040OPPNLDOF_0040_003F_0024AAA_003F_0024AAr_003F_0024AAt_003F_0024AAU_003F_0024AAr_003F_0024AAl_003F_0024AA_003F_0024AA_0040);
		Unsafe.As<_0024ArrayType_0024_0024_0024BY09UPROPERTY_TO_PID_MAP_0040Subscription_0040Zune_0040Microsoft_0040_0040, int>(ref Unsafe.AddByteOffset(ref _0024ArrayType_0024_0024_0024BY09UPROPERTY_TO_PID_MAP_0040Subscription_0040Zune_0040Microsoft_0040_00402, 36)) = 134242317;
		Unsafe.As<_0024ArrayType_0024_0024_0024BY09UPROPERTY_TO_PID_MAP_0040Subscription_0040Zune_0040Microsoft_0040_0040, int>(ref Unsafe.AddByteOffset(ref _0024ArrayType_0024_0024_0024BY09UPROPERTY_TO_PID_MAP_0040Subscription_0040Zune_0040Microsoft_0040_00402, 40)) = (int)Unsafe.AsPointer(ref global::_003CModule_003E._003F_003F_C_0040_1BI_0040DLMANABL_0040_003F_0024AAD_003F_0024AAe_003F_0024AAs_003F_0024AAc_003F_0024AAr_003F_0024AAi_003F_0024AAp_003F_0024AAt_003F_0024AAi_003F_0024AAo_003F_0024AAn_003F_0024AA_003F_0024AA_0040);
		Unsafe.As<_0024ArrayType_0024_0024_0024BY09UPROPERTY_TO_PID_MAP_0040Subscription_0040Zune_0040Microsoft_0040_0040, int>(ref Unsafe.AddByteOffset(ref _0024ArrayType_0024_0024_0024BY09UPROPERTY_TO_PID_MAP_0040Subscription_0040Zune_0040Microsoft_0040_00402, 44)) = 134242306;
		Unsafe.As<_0024ArrayType_0024_0024_0024BY09UPROPERTY_TO_PID_MAP_0040Subscription_0040Zune_0040Microsoft_0040_0040, int>(ref Unsafe.AddByteOffset(ref _0024ArrayType_0024_0024_0024BY09UPROPERTY_TO_PID_MAP_0040Subscription_0040Zune_0040Microsoft_0040_00402, 48)) = (int)Unsafe.AsPointer(ref global::_003CModule_003E._003F_003F_C_0040_1BC_0040BFOBHOBE_0040_003F_0024AAE_003F_0024AAx_003F_0024AAp_003F_0024AAl_003F_0024AAi_003F_0024AAc_003F_0024AAi_003F_0024AAt_003F_0024AA_003F_0024AA_0040);
		Unsafe.As<_0024ArrayType_0024_0024_0024BY09UPROPERTY_TO_PID_MAP_0040Subscription_0040Zune_0040Microsoft_0040_0040, int>(ref Unsafe.AddByteOffset(ref _0024ArrayType_0024_0024_0024BY09UPROPERTY_TO_PID_MAP_0040Subscription_0040Zune_0040Microsoft_0040_00402, 52)) = 83910662;
		Unsafe.As<_0024ArrayType_0024_0024_0024BY09UPROPERTY_TO_PID_MAP_0040Subscription_0040Zune_0040Microsoft_0040_0040, int>(ref Unsafe.AddByteOffset(ref _0024ArrayType_0024_0024_0024BY09UPROPERTY_TO_PID_MAP_0040Subscription_0040Zune_0040Microsoft_0040_00402, 56)) = (int)Unsafe.AsPointer(ref global::_003CModule_003E._003F_003F_C_0040_1BE_0040NPMHDJAF_0040_003F_0024AAC_003F_0024AAo_003F_0024AAp_003F_0024AAy_003F_0024AAr_003F_0024AAi_003F_0024AAg_003F_0024AAh_003F_0024AAt_003F_0024AA_003F_0024AA_0040);
		Unsafe.As<_0024ArrayType_0024_0024_0024BY09UPROPERTY_TO_PID_MAP_0040Subscription_0040Zune_0040Microsoft_0040_0040, int>(ref Unsafe.AddByteOffset(ref _0024ArrayType_0024_0024_0024BY09UPROPERTY_TO_PID_MAP_0040Subscription_0040Zune_0040Microsoft_0040_00402, 60)) = 16801801;
		Unsafe.As<_0024ArrayType_0024_0024_0024BY09UPROPERTY_TO_PID_MAP_0040Subscription_0040Zune_0040Microsoft_0040_0040, int>(ref Unsafe.AddByteOffset(ref _0024ArrayType_0024_0024_0024BY09UPROPERTY_TO_PID_MAP_0040Subscription_0040Zune_0040Microsoft_0040_00402, 64)) = (int)Unsafe.AsPointer(ref global::_003CModule_003E._003F_003F_C_0040_1O_0040IKCDCNCP_0040_003F_0024AAA_003F_0024AAu_003F_0024AAt_003F_0024AAh_003F_0024AAo_003F_0024AAr_003F_0024AA_003F_0024AA_0040);
		Unsafe.As<_0024ArrayType_0024_0024_0024BY09UPROPERTY_TO_PID_MAP_0040Subscription_0040Zune_0040Microsoft_0040_0040, int>(ref Unsafe.AddByteOffset(ref _0024ArrayType_0024_0024_0024BY09UPROPERTY_TO_PID_MAP_0040Subscription_0040Zune_0040Microsoft_0040_00402, 68)) = 16801796;
		Unsafe.As<_0024ArrayType_0024_0024_0024BY09UPROPERTY_TO_PID_MAP_0040Subscription_0040Zune_0040Microsoft_0040_0040, int>(ref Unsafe.AddByteOffset(ref _0024ArrayType_0024_0024_0024BY09UPROPERTY_TO_PID_MAP_0040Subscription_0040Zune_0040Microsoft_0040_00402, 72)) = (int)Unsafe.AsPointer(ref global::_003CModule_003E._003F_003F_C_0040_1BE_0040GMGHCDOJ_0040_003F_0024AAO_003F_0024AAw_003F_0024AAn_003F_0024AAe_003F_0024AAr_003F_0024AAN_003F_0024AAa_003F_0024AAm_003F_0024AAe_003F_0024AA_003F_0024AA_0040);
		Unsafe.As<_0024ArrayType_0024_0024_0024BY09UPROPERTY_TO_PID_MAP_0040Subscription_0040Zune_0040Microsoft_0040_0040, int>(ref Unsafe.AddByteOffset(ref _0024ArrayType_0024_0024_0024BY09UPROPERTY_TO_PID_MAP_0040Subscription_0040Zune_0040Microsoft_0040_00402, 76)) = 16801799;
		Unsafe.SkipInit(out CComPropVariant cComPropVariant);
		// IL initblk instruction
		Unsafe.InitBlock(ref cComPropVariant, 0, 16);
		object result;
		try
		{
			fixed (ushort* ptr = &Unsafe.As<char, ushort>(ref global::_003CModule_003E.PtrToStringChars(propertyName)))
			{
				if (global::_003CModule_003E._wcsicmp(ptr, (ushort*)Unsafe.AsPointer(ref global::_003CModule_003E._003F_003F_C_0040_1BE_0040NCILDCLH_0040_003F_0024AAL_003F_0024AAi_003F_0024AAb_003F_0024AAr_003F_0024AAa_003F_0024AAr_003F_0024AAy_003F_0024AAI_003F_0024AAd_003F_0024AA_003F_0024AA_0040)) != 0 && global::_003CModule_003E._wcsicmp(ptr, (ushort*)Unsafe.AsPointer(ref global::_003CModule_003E._003F_003F_C_0040_1BI_0040KPFACBGA_0040_003F_0024AAS_003F_0024AAe_003F_0024AAr_003F_0024AAi_003F_0024AAe_003F_0024AAs_003F_0024AAS_003F_0024AAt_003F_0024AAa_003F_0024AAt_003F_0024AAe_003F_0024AA_003F_0024AA_0040)) != 0 && global::_003CModule_003E._wcsicmp(ptr, (ushort*)Unsafe.AsPointer(ref global::_003CModule_003E._003F_003F_C_0040_1CC_0040GKFOINAA_0040_003F_0024AAN_003F_0024AAu_003F_0024AAm_003F_0024AAb_003F_0024AAe_003F_0024AAr_003F_0024AAO_003F_0024AAf_003F_0024AAE_003F_0024AAp_003F_0024AAi_003F_0024AAs_003F_0024AAo_003F_0024AAd_003F_0024AAe_003F_0024AAs_003F_0024AA_003F_0024AA_0040)) != 0 && m_pSeriesPropertySet != null)
				{
					int num = 0;
					while (true)
					{
						if (global::_003CModule_003E._wcsicmp(ptr, (ushort*)(int)(*(uint*)((ref *(_003F*)(num * 8)) + (ref *(_003F*)(&_0024ArrayType_0024_0024_0024BY09UPROPERTY_TO_PID_MAP_0040Subscription_0040Zune_0040Microsoft_0040_00402))))) != 0)
						{
							num++;
							if ((uint)num >= 10u)
							{
								break;
							}
							continue;
						}
						uint num2 = *(uint*)((ref *(_003F*)(num * 8)) + (ref Unsafe.As<_0024ArrayType_0024_0024_0024BY09UPROPERTY_TO_PID_MAP_0040Subscription_0040Zune_0040Microsoft_0040_0040, _003F>(ref Unsafe.AddByteOffset(ref _0024ArrayType_0024_0024_0024BY09UPROPERTY_TO_PID_MAP_0040Subscription_0040Zune_0040Microsoft_0040_00402, 4))));
						IMSMediaSchemaPropertySet* pSeriesPropertySet = m_pSeriesPropertySet;
						if (((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint, uint, tagPROPVARIANT*, int>)(int)(*(uint*)(*(int*)pSeriesPropertySet + 24)))((nint)pSeriesPropertySet, num2, 0u, (tagPROPVARIANT*)(&cComPropVariant)) < 0 || *(ushort*)(&cComPropVariant) == 0)
						{
							break;
						}
						result = SubscriptionDataProviderQueryResult.ConvertVariantToType(((DataProviderObject)this).Mappings[propertyName].PropertyTypeName, &cComPropVariant);
						goto end_IL_00f0;
					}
				}
				result = ((DataProviderObject)this).Mappings[propertyName].DefaultValue;
				end_IL_00f0:;
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

	public override void SetProperty(string propertyName, object value)
	{
		throw new NotSupportedException();
	}

	internal unsafe void OnDispose()
	{
		IMSMediaSchemaPropertySet* pSeriesPropertySet = m_pSeriesPropertySet;
		if (null != pSeriesPropertySet)
		{
			((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint>)(int)(*(uint*)(*(int*)pSeriesPropertySet + 8)))((nint)pSeriesPropertySet);
			m_pSeriesPropertySet = null;
		}
	}

	internal unsafe void SetPropertySet(IMSMediaSchemaPropertySet* pSeriesPropertySet)
	{
		Unsafe.SkipInit(out CComPropVariant cComPropVariant);
		// IL initblk instruction
		Unsafe.InitBlock(ref cComPropVariant, 0, 16);
		try
		{
			if (pSeriesPropertySet != null && ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint, uint, tagPROPVARIANT*, int>)(int)(*(uint*)(*(int*)pSeriesPropertySet + 24)))((nint)pSeriesPropertySet, 16801793u, 0u, (tagPROPVARIANT*)(&cComPropVariant)) >= 0 && *(ushort*)(&cComPropVariant) != 0)
			{
				IMSMediaSchemaPropertySet* pSeriesPropertySet2 = m_pSeriesPropertySet;
				if (null != pSeriesPropertySet2)
				{
					((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint>)(int)(*(uint*)(*(int*)pSeriesPropertySet2 + 8)))((nint)pSeriesPropertySet2);
					m_pSeriesPropertySet = null;
				}
				m_pSeriesPropertySet = pSeriesPropertySet;
				((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint>)(int)(*(uint*)(*(int*)pSeriesPropertySet + 4)))((nint)pSeriesPropertySet);
				if (!string.IsNullOrEmpty(m_serviceId))
				{
					Guid guid = new Guid(m_serviceId);
					Unsafe.SkipInit(out _GUID gUID);
					*(int*)(&gUID) = 0;
					// IL initblk instruction
					Unsafe.InitBlock(ref Unsafe.AddByteOffset(ref gUID, 4), 0, 12);
					gUID = global::_003CModule_003E.GuidToGUID(guid);
					Unsafe.SkipInit(out tagPROPVARIANT tagPROPVARIANT2);
					*(short*)(&tagPROPVARIANT2) = 72;
					Unsafe.As<tagPROPVARIANT, int>(ref Unsafe.AddByteOffset(ref tagPROPVARIANT2, 8)) = (int)(&gUID);
					IMSMediaSchemaPropertySet* pSeriesPropertySet3 = m_pSeriesPropertySet;
					((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint, tagPROPVARIANT, int>)(int)(*(uint*)(*(int*)pSeriesPropertySet3 + 28)))((nint)pSeriesPropertySet3, 67133455u, tagPROPVARIANT2);
				}
			}
		}
		catch
		{
			//try-fault
			global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<CComPropVariant*, void>)(&global::_003CModule_003E.CComPropVariant_002E_007Bdtor_007D), &cComPropVariant);
			throw;
		}
		global::_003CModule_003E.CComPropVariant_002EClear(&cComPropVariant);
	}
}
