using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace MicrosoftZuneLibrary;

public class HMESettings : IDisposable
{
	private unsafe IHMESettings* m_pSettings;

	private unsafe INSSManager* m_pNSSManager;

	private unsafe INSSDevices* m_pDeviceList;

	private unsafe NSSMediator* m_pNSSMediator;

	private NSSDeviceListChangeHandler m_NSSDeviceListChange;

	public unsafe bool VelaSharingEnabled
	{
		[return: MarshalAs(UnmanagedType.U1)]
		get
		{
			IHMESettings* pSettings = m_pSettings;
			if (pSettings == null)
			{
				return false;
			}
			return (((delegate* unmanaged[Thiscall, Thiscall]<IntPtr, int>)(int)(*(uint*)(*(int*)pSettings + 84)))((nint)pSettings) != 0) ? true : false;
		}
	}

	public unsafe bool SharingEnableRequiresLoginAsAdmin
	{
		[return: MarshalAs(UnmanagedType.U1)]
		get
		{
			IHMESettings* pSettings = m_pSettings;
			if (pSettings == null)
			{
				return true;
			}
			return (((delegate* unmanaged[Thiscall, Thiscall]<IntPtr, int>)(int)(*(uint*)(*(int*)pSettings + 56)))((nint)pSettings) != 0) ? true : false;
		}
	}

	public unsafe bool SharingEnableRequiresElevation
	{
		[return: MarshalAs(UnmanagedType.U1)]
		get
		{
			IHMESettings* pSettings = m_pSettings;
			if (pSettings == null)
			{
				return true;
			}
			return (((delegate* unmanaged[Thiscall, Thiscall]<IntPtr, int>)(int)(*(uint*)(*(int*)pSettings + 52)))((nint)pSettings) != 0) ? true : false;
		}
	}

	public unsafe bool SharingEnabled
	{
		[return: MarshalAs(UnmanagedType.U1)]
		get
		{
			IHMESettings* pSettings = m_pSettings;
			if (pSettings == null)
			{
				return false;
			}
			return (((delegate* unmanaged[Thiscall, Thiscall]<IntPtr, int>)(int)(*(uint*)(*(int*)pSettings + 76)))((nint)pSettings) != 0) ? true : false;
		}
	}

	public bool SharingBroken
	{
		[return: MarshalAs(UnmanagedType.U1)]
		get
		{
			return false;
		}
	}

	[SpecialName]
	public virtual event NSSDeviceListChangeHandler NSSDeviceListChangeEvent
	{
		add
		{
			m_NSSDeviceListChange = (NSSDeviceListChangeHandler)Delegate.Combine(m_NSSDeviceListChange, value);
		}
		remove
		{
			m_NSSDeviceListChange = (NSSDeviceListChangeHandler)Delegate.Remove(m_NSSDeviceListChange, value);
		}
	}

	public unsafe HMESettings()
	{
		m_pSettings = null;
		m_pNSSManager = null;
		m_pDeviceList = null;
	}

	private void _007EHMESettings()
	{
		_0021HMESettings();
	}

	private unsafe void _0021HMESettings()
	{
		IHMESettings* pSettings = m_pSettings;
		if (null != pSettings)
		{
			((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint>)(int)(*(uint*)(*(int*)pSettings + 8)))((nint)pSettings);
			m_pSettings = null;
		}
		INSSManager* pNSSManager = m_pNSSManager;
		if (null != pNSSManager)
		{
			((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint>)(int)(*(uint*)(*(int*)pNSSManager + 8)))((nint)pNSSManager);
			m_pNSSManager = null;
		}
		INSSDevices* pDeviceList = m_pDeviceList;
		if (null != pDeviceList)
		{
			((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint>)(int)(*(uint*)(*(int*)pDeviceList + 8)))((nint)pDeviceList);
			m_pDeviceList = null;
		}
		NSSMediator* pNSSMediator = m_pNSSMediator;
		if (pNSSMediator != null)
		{
			global::_003CModule_003E.NSSMediator_002EShutdown(pNSSMediator);
		}
		NSSMediator* pNSSMediator2 = m_pNSSMediator;
		if (null != pNSSMediator2)
		{
			((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint>)(int)(*(uint*)(*(int*)pNSSMediator2 + 8)))((nint)pNSSMediator2);
			m_pNSSMediator = null;
		}
	}

	public unsafe int Init()
	{
		fixed (IHMESettings** pSettings = &m_pSettings)
		{
			int num = global::_003CModule_003E.CreateHMESettings(pSettings);
			fixed (INSSManager** pNSSManager = &m_pNSSManager)
			{
				if (num >= 0)
				{
					num = global::_003CModule_003E.CreateNSSManager(pNSSManager);
				}
				NSSMediator* pNSSMediator = m_pNSSMediator;
				if (null != pNSSMediator)
				{
					((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint>)(int)(*(uint*)(*(int*)pNSSMediator + 8)))((nint)pNSSMediator);
					m_pNSSMediator = null;
				}
				NSSMediator* ptr = (NSSMediator*)global::_003CModule_003E.@new(20u);
				NSSMediator* ptr2;
				try
				{
					ptr2 = ((ptr == null) ? null : global::_003CModule_003E.NSSMediator_002E_007Bctor_007D(ptr, m_pNSSManager, this));
				}
				catch
				{
					//try-fault
					global::_003CModule_003E.delete(ptr);
					throw;
				}
				m_pNSSMediator = ptr2;
				global::_003CModule_003E.SafeAddRef_003Cclass_0020NSSMediator_003E(ptr2);
				return num;
			}
		}
	}

	public unsafe int RepairSharing()
	{
		IHMESettings* pSettings = m_pSettings;
		if (pSettings == null)
		{
			global::_003CModule_003E._ZuneShipAssert(1002u, 76u);
			return -2147418113;
		}
		return ((delegate* unmanaged[Thiscall, Thiscall]<IntPtr, int>)(int)(*(uint*)(*(int*)pSettings + 68)))((nint)pSettings);
	}

	public unsafe int EnableSharingForUser()
	{
		IHMESettings* pSettings = m_pSettings;
		if (pSettings == null)
		{
			global::_003CModule_003E._ZuneShipAssert(1002u, 119u);
			return -2147418113;
		}
		IHMESettings* intPtr = pSettings;
		int num;
		if (((delegate* unmanaged[Thiscall, Thiscall]<IntPtr, int>)(int)(*(uint*)(*(int*)intPtr + 48)))((nint)intPtr) == 0)
		{
			pSettings = m_pSettings;
			num = ((delegate* unmanaged[Thiscall, Thiscall]<IntPtr, int, int>)(int)(*(uint*)(*(int*)pSettings + 60)))((nint)pSettings, 1);
			if (num < 0)
			{
				goto IL_0071;
			}
		}
		pSettings = m_pSettings;
		num = ((delegate* unmanaged[Thiscall, Thiscall]<IntPtr, int, int>)(int)(*(uint*)(*(int*)pSettings + 80)))((nint)pSettings, 1);
		if (num >= 0)
		{
			pSettings = m_pSettings;
			num = ((delegate* unmanaged[Thiscall, Thiscall]<IntPtr, byte, int>)(int)(*(uint*)(*(int*)pSettings + 12)))((nint)pSettings, 0);
		}
		goto IL_0071;
		IL_0071:
		return num;
	}

	public unsafe int DisableSharingForUser()
	{
		IHMESettings* pSettings = m_pSettings;
		if (pSettings == null)
		{
			global::_003CModule_003E._ZuneShipAssert(1002u, 144u);
			return -2147418113;
		}
		return ((delegate* unmanaged[Thiscall, Thiscall]<IntPtr, int, int>)(int)(*(uint*)(*(int*)pSettings + 80)))((nint)pSettings, 0);
	}

	public unsafe int DisableSharingForMachine()
	{
		IHMESettings* pSettings = m_pSettings;
		if (pSettings == null)
		{
			global::_003CModule_003E._ZuneShipAssert(1002u, 153u);
			return -2147418113;
		}
		return ((delegate* unmanaged[Thiscall, Thiscall]<IntPtr, int, int>)(int)(*(uint*)(*(int*)pSettings + 60)))((nint)pSettings, 0);
	}

	public unsafe int SetSharedFoldersList([MarshalAs(UnmanagedType.U1)] bool fForce)
	{
		IHMESettings* pSettings = m_pSettings;
		if (pSettings == null)
		{
			global::_003CModule_003E._ZuneShipAssert(1002u, 162u);
			return -2147418113;
		}
		return ((delegate* unmanaged[Thiscall, Thiscall]<IntPtr, byte, int>)(int)(*(uint*)(*(int*)pSettings + 12)))((nint)pSettings, fForce ? ((byte)1) : ((byte)0));
	}

	public unsafe int GetDisplayName(ref string strName)
	{
		IHMESettings* pSettings = m_pSettings;
		if (pSettings == null)
		{
			global::_003CModule_003E._ZuneShipAssert(1002u, 181u);
			return -2147418113;
		}
		Unsafe.SkipInit(out ushort* ptr);
		int num = ((delegate* unmanaged[Thiscall, Thiscall]<IntPtr, ushort**, int>)(int)(*(uint*)(*(int*)pSettings + 112)))((nint)pSettings, &ptr);
		if (num >= 0)
		{
			strName = new string((char*)ptr);
			global::_003CModule_003E.SysFreeString(ptr);
		}
		return num;
	}

	public unsafe int SetDisplayName(string strName)
	{
		if (m_pSettings == null)
		{
			global::_003CModule_003E._ZuneShipAssert(1002u, 197u);
			return -2147418113;
		}
		fixed (ushort* ptr = &Unsafe.As<char, ushort>(ref global::_003CModule_003E.PtrToStringChars(strName)))
		{
			int result;
			if (ptr != null)
			{
				int num = *(int*)m_pSettings + 116;
				result = ((delegate* unmanaged[Thiscall, Thiscall]<IntPtr, ushort*, int>)(int)(*(uint*)num))((nint)m_pSettings, ptr);
			}
			else
			{
				result = -2147418113;
			}
			return result;
		}
	}

	[return: MarshalAs(UnmanagedType.U1)]
	public unsafe bool GetSharingEnabledForMediaType(EMediaTypes mediaType)
	{
		IHMESettings* pSettings = m_pSettings;
		if (pSettings == null)
		{
			return false;
		}
		int num;
		if (mediaType != EMediaTypes.eMediaTypeAudio)
		{
			if (mediaType != EMediaTypes.eMediaTypeVideo)
			{
				if (mediaType != EMediaTypes.eMediaTypeImage)
				{
					goto IL_004a;
				}
				num = ((delegate* unmanaged[Thiscall, Thiscall]<IntPtr, int>)(int)(*(uint*)(*(int*)pSettings + 96)))((nint)pSettings);
			}
			else
			{
				num = ((delegate* unmanaged[Thiscall, Thiscall]<IntPtr, int>)(int)(*(uint*)(*(int*)pSettings + 104)))((nint)pSettings);
			}
		}
		else
		{
			num = ((delegate* unmanaged[Thiscall, Thiscall]<IntPtr, int>)(int)(*(uint*)(*(int*)pSettings + 88)))((nint)pSettings);
		}
		if (num != 0)
		{
			return true;
		}
		goto IL_004a;
		IL_004a:
		return false;
	}

	public unsafe int SetSharingEnabledForMediaType(EMediaTypes mediaType, [MarshalAs(UnmanagedType.U1)] bool bEnabled)
	{
		IHMESettings* pSettings = m_pSettings;
		if (pSettings == null)
		{
			global::_003CModule_003E._ZuneShipAssert(1002u, 248u);
			return -2147418113;
		}
		int num = (bEnabled ? 1 : 0);
		return mediaType switch
		{
			EMediaTypes.eMediaTypeImage => ((delegate* unmanaged[Thiscall, Thiscall]<IntPtr, int, int>)(int)(*(uint*)(*(int*)pSettings + 100)))((nint)pSettings, num), 
			EMediaTypes.eMediaTypeVideo => ((delegate* unmanaged[Thiscall, Thiscall]<IntPtr, int, int>)(int)(*(uint*)(*(int*)pSettings + 108)))((nint)pSettings, num), 
			EMediaTypes.eMediaTypeAudio => ((delegate* unmanaged[Thiscall, Thiscall]<IntPtr, int, int>)(int)(*(uint*)(*(int*)pSettings + 92)))((nint)pSettings, num), 
			_ => -2147024809, 
		};
	}

	[return: MarshalAs(UnmanagedType.U1)]
	public unsafe bool GetAllDevicesEnabled()
	{
		INSSManager* pNSSManager = m_pNSSManager;
		if (pNSSManager == null)
		{
			return false;
		}
		Unsafe.SkipInit(out short num);
		if (((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, short*, int>)(int)(*(uint*)(*(int*)pNSSManager + 28)))((nint)pNSSManager, &num) >= 0)
		{
			return num == -1;
		}
		return false;
	}

	public unsafe int SetAllDevicesEnabled([MarshalAs(UnmanagedType.U1)] bool bEnabled)
	{
		INSSManager* pNSSManager = m_pNSSManager;
		if (pNSSManager == null)
		{
			global::_003CModule_003E._ZuneShipAssert(1002u, 297u);
			return -2147418113;
		}
		int num = -1;
		if (!bEnabled)
		{
			num = ~num;
		}
		short num2 = (short)num;
		return ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, short, int>)(int)(*(uint*)(*(int*)pNSSManager + 32)))((nint)pNSSManager, num2);
	}

	public unsafe uint GetDeviceCount()
	{
		if (m_pNSSManager == null)
		{
			return 0u;
		}
		uint result = 0u;
		if (m_pDeviceList == null)
		{
			fixed (INSSDevices** ptr = &m_pDeviceList)
			{
				try
				{
					int num = *(int*)m_pNSSManager + 44;
					((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, INSSDevices**, int>)(int)(*(uint*)num))((nint)m_pNSSManager, ptr);
				}
				catch
				{
					//try-fault
					ptr = null;
					throw;
				}
			}
		}
		INSSDevices* pDeviceList = m_pDeviceList;
		Unsafe.SkipInit(out int num2);
		if (pDeviceList != null && ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, int*, int>)(int)(*(uint*)(*(int*)pDeviceList + 32)))((nint)pDeviceList, &num2) >= 0)
		{
			result = (uint)num2;
		}
		return result;
	}

	public unsafe int GetDeviceProps(uint dwIndex, ref string strName, ref string strMAC, ref string strSerialNumber)
	{
		if (m_pDeviceList == null)
		{
			global::_003CModule_003E._ZuneShipAssert(1002u, 338u);
			return -2147418113;
		}
		INSSDevice* ptr = null;
		INSSProperties* ptr2 = null;
		INSSProperty* ptr3 = null;
		ushort* ptr4 = null;
		Unsafe.SkipInit(out tagVARIANT tagVARIANT2);
		fixed (tagVARIANT* ptr5 = &Unsafe.AsRef<tagVARIANT>(&tagVARIANT2))
		{
			global::_003CModule_003E.VariantInit(ptr5);
			fixed (INSSDevice** ptr6 = &Unsafe.AsRef<INSSDevice*>(&ptr))
			{
				int num = *(int*)m_pDeviceList + 28;
				int num2 = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, int, INSSDevice**, int>)(int)(*(uint*)num))((nint)m_pDeviceList, (int)dwIndex, ptr6);
				if (num2 >= 0)
				{
					fixed (ushort** ptr7 = &Unsafe.AsRef<ushort*>(&ptr4))
					{
						try
						{
							int num3 = *(int*)ptr + 28;
							num2 = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, ushort**, int>)(int)(*(uint*)num3))((nint)ptr, ptr7);
						}
						catch
						{
							//try-fault
							ptr7 = null;
							throw;
						}
					}
					if (num2 >= 0)
					{
						strMAC = new string((char*)ptr4);
						global::_003CModule_003E.SysFreeString(ptr4);
						ptr4 = null;
						fixed (INSSProperties** ptr8 = &Unsafe.AsRef<INSSProperties*>(&ptr2))
						{
							try
							{
								int num4 = *(int*)ptr + 48;
								num2 = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, INSSProperties**, int>)(int)(*(uint*)num4))((nint)ptr, ptr8);
							}
							catch
							{
								//try-fault
								ptr8 = null;
								throw;
							}
						}
						if (num2 >= 0)
						{
							fixed (INSSProperty** ptr9 = &Unsafe.AsRef<INSSProperty*>(&ptr3))
							{
								try
								{
									int num5 = *(int*)ptr2 + 36;
									num2 = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, ushort*, INSSProperty**, int>)(int)(*(uint*)num5))((nint)ptr2, (ushort*)Unsafe.AsPointer(ref global::_003CModule_003E._003F_003F_C_0040_1BK_0040BFIEKNFP_0040_003F_0024AAF_003F_0024AAr_003F_0024AAi_003F_0024AAe_003F_0024AAn_003F_0024AAd_003F_0024AAl_003F_0024AAy_003F_0024AAN_003F_0024AAa_003F_0024AAm_003F_0024AAe_003F_0024AA_003F_0024AA_0040), ptr9);
									if (num2 < 0)
									{
										ptr3 = null;
										num2 = 0;
									}
								}
								catch
								{
									//try-fault
									ptr9 = null;
									throw;
								}
							}
							if (num2 >= 0)
							{
								if (ptr3 != null)
								{
									int num6 = *(int*)ptr3 + 32;
									num2 = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, tagVARIANT*, int>)(int)(*(uint*)num6))((nint)ptr3, ptr5);
								}
								if (num2 >= 0)
								{
									if (ptr3 != null)
									{
										if (*(ushort*)(&tagVARIANT2) == 8)
										{
											strName = new string((char*)(int)Unsafe.As<tagVARIANT, uint>(ref Unsafe.AddByteOffset(ref tagVARIANT2, 8)));
										}
										global::_003CModule_003E.VariantClear(ptr5);
										if (null != ptr3)
										{
											INSSProperty* intPtr = ptr3;
											((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint>)(int)(*(uint*)(*(int*)intPtr + 8)))((nint)intPtr);
											ptr3 = null;
										}
									}
									fixed (INSSProperty** ptr10 = &Unsafe.AsRef<INSSProperty*>(&ptr3))
									{
										try
										{
											int num7 = *(int*)ptr2 + 36;
											num2 = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, ushort*, INSSProperty**, int>)(int)(*(uint*)num7))((nint)ptr2, (ushort*)Unsafe.AsPointer(ref global::_003CModule_003E._003F_003F_C_0040_1BK_0040GBMCOKGG_0040_003F_0024AAS_003F_0024AAe_003F_0024AAr_003F_0024AAi_003F_0024AAa_003F_0024AAl_003F_0024AAN_003F_0024AAu_003F_0024AAm_003F_0024AAb_003F_0024AAe_003F_0024AAr_003F_0024AA_003F_0024AA_0040), ptr10);
											if (num2 < 0)
											{
												ptr3 = null;
												num2 = 0;
											}
										}
										catch
										{
											//try-fault
											ptr10 = null;
											throw;
										}
									}
									if (num2 >= 0)
									{
										if (ptr3 != null)
										{
											int num8 = *(int*)ptr3 + 32;
											num2 = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, tagVARIANT*, int>)(int)(*(uint*)num8))((nint)ptr3, ptr5);
										}
										if (num2 >= 0 && ptr3 != null)
										{
											if (*(ushort*)(&tagVARIANT2) == 8)
											{
												strSerialNumber = new string((char*)(int)Unsafe.As<tagVARIANT, uint>(ref Unsafe.AddByteOffset(ref tagVARIANT2, 8)));
											}
											global::_003CModule_003E.VariantClear(ptr5);
											if (null != ptr3)
											{
												INSSProperty* intPtr2 = ptr3;
												((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint>)(int)(*(uint*)(*(int*)intPtr2 + 8)))((nint)intPtr2);
												ptr3 = null;
											}
										}
									}
								}
							}
						}
					}
				}
				if (ptr4 != null)
				{
					global::_003CModule_003E.SysFreeString(ptr4);
				}
				if (null != ptr)
				{
					INSSDevice* intPtr3 = ptr;
					((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint>)(int)(*(uint*)(*(int*)intPtr3 + 8)))((nint)intPtr3);
					ptr = null;
				}
				if (null != ptr2)
				{
					INSSProperties* intPtr4 = ptr2;
					((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint>)(int)(*(uint*)(*(int*)intPtr4 + 8)))((nint)intPtr4);
					ptr2 = null;
				}
				if (null != ptr3)
				{
					INSSProperty* intPtr5 = ptr3;
					((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint>)(int)(*(uint*)(*(int*)intPtr5 + 8)))((nint)intPtr5);
					ptr3 = null;
				}
				global::_003CModule_003E.VariantClear(ptr5);
				return num2;
			}
		}
	}

	[return: MarshalAs(UnmanagedType.U1)]
	public unsafe bool GetDeviceEnabled(uint dwIndex)
	{
		if (m_pDeviceList == null)
		{
			return false;
		}
		bool result = false;
		INSSDevice* ptr = null;
		AuthorizationStatus authorizationStatus = (AuthorizationStatus)0;
		fixed (INSSDevice** ptr2 = &Unsafe.AsRef<INSSDevice*>(&ptr))
		{
			int num = *(int*)m_pDeviceList + 28;
			int num2 = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, int, INSSDevice**, int>)(int)(*(uint*)num))((nint)m_pDeviceList, (int)dwIndex, ptr2);
			if (num2 >= 0)
			{
				fixed (AuthorizationStatus* ptr3 = &Unsafe.AsRef<AuthorizationStatus>(&authorizationStatus))
				{
					try
					{
						int num3 = *(int*)ptr + 32;
						num2 = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, AuthorizationStatus*, int>)(int)(*(uint*)num3))((nint)ptr, ptr3);
					}
					catch
					{
						//try-fault
						ptr3 = null;
						throw;
					}
				}
				if (num2 >= 0 && authorizationStatus == (AuthorizationStatus)2)
				{
					result = true;
				}
			}
			if (null != ptr)
			{
				INSSDevice* intPtr = ptr;
				((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint>)(int)(*(uint*)(*(int*)intPtr + 8)))((nint)intPtr);
			}
			return result;
		}
	}

	public unsafe int EnableDevice(uint dwIndex, [MarshalAs(UnmanagedType.U1)] bool bEnabled)
	{
		if (m_pDeviceList == null)
		{
			global::_003CModule_003E._ZuneShipAssert(1002u, 470u);
			return -2147418113;
		}
		INSSDevice* ptr = null;
		fixed (INSSDevice** ptr2 = &Unsafe.AsRef<INSSDevice*>(&ptr))
		{
			int num = *(int*)m_pDeviceList + 28;
			int num2 = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, int, INSSDevice**, int>)(int)(*(uint*)num))((nint)m_pDeviceList, (int)dwIndex, ptr2);
			if (num2 >= 0)
			{
				AuthorizationStatus authorizationStatus = (bEnabled ? ((AuthorizationStatus)2) : ((AuthorizationStatus)3));
				num2 = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, AuthorizationStatus, int>)(int)(*(uint*)(*(int*)ptr + 36)))((nint)ptr, authorizationStatus);
			}
			if (null != ptr)
			{
				INSSDevice* intPtr = ptr;
				((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint>)(int)(*(uint*)(*(int*)intPtr + 8)))((nint)intPtr);
			}
			return num2;
		}
	}

	internal void NSSDeviceListChange()
	{
		if (m_NSSDeviceListChange != null)
		{
			m_NSSDeviceListChange();
		}
	}

	protected virtual void Dispose([MarshalAs(UnmanagedType.U1)] bool P_0)
	{
		if (P_0)
		{
			_0021HMESettings();
			return;
		}
		try
		{
			_0021HMESettings();
		}
		finally
		{
			base.Finalize();
		}
	}

	public virtual sealed void Dispose()
	{
		Dispose(true);
		GC.SuppressFinalize(this);
	}

	~HMESettings()
	{
		Dispose(false);
	}
}
