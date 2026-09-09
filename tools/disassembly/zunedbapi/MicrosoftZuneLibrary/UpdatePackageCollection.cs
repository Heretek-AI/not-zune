using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace MicrosoftZuneLibrary;

public class UpdatePackageCollection : IDisposable
{
	private readonly CComPtrMgd_003CIFirmwareUpdateCollection_003E m_spUpdatePackageCollection;

	private List<FirmwareUpdatePackage> m_packages;

	private FirmwareUpdatePackage m_FirmwarePackage;

	private FirmwareUpdatePackage m_GamesPackage;

	private List<FirmwareUpdatePackage> Packages
	{
		get
		{
			CachePackages();
			return m_packages;
		}
	}

	internal unsafe IFirmwareUpdateCollection* NativeCollectionPtr => m_spUpdatePackageCollection.p;

	public FirmwareUpdatePackage GamesPackage
	{
		get
		{
			CachePackages();
			return m_GamesPackage;
		}
	}

	public FirmwareUpdatePackage FirmwarePackage
	{
		get
		{
			CachePackages();
			return m_FirmwarePackage;
		}
	}

	public FirmwareUpdatePackage Item => Packages[index];

	public unsafe bool Selected
	{
		[return: MarshalAs(UnmanagedType.U1)]
		get
		{
			return Packages[index].Selected;
		}
		[param: MarshalAs(UnmanagedType.U1)]
		set
		{
			uint num = (uint)index;
			IFirmwareUpdateCollection* p = m_spUpdatePackageCollection.p;
			int num2 = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint, uint*, int, int>)(int)(*(uint*)(*(int*)p + 20)))((nint)p, 1u, &num, value ? 1 : 0);
			if (num2 >= 0)
			{
				Packages[index].Selected = value;
			}
			if (global::_003CModule_003E.WPP_GLOBAL_Control != Unsafe.AsPointer(ref global::_003CModule_003E.WPP_GLOBAL_Control) && (((int*)global::_003CModule_003E.WPP_GLOBAL_Control)[15] & 4) != 0 && (uint)((byte*)global::_003CModule_003E.WPP_GLOBAL_Control)[57] >= 6u)
			{
				global::_003CModule_003E.WPP_SF_dld(((ulong*)global::_003CModule_003E.WPP_GLOBAL_Control)[6], 10, (_GUID*)Unsafe.AsPointer(ref global::_003CModule_003E._003FA0xff79125d_002EWPP_FirmwareUpdateAPI_cpp_Traceguids), index, value ? 1 : 0, num2);
			}
		}
	}

	public unsafe int Count
	{
		get
		{
			uint result = 0u;
			IFirmwareUpdateCollection* p = m_spUpdatePackageCollection.p;
			((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint*, int>)(int)(*(uint*)(*(int*)p + 12)))((nint)p, &result);
			return (int)result;
		}
	}

	internal unsafe UpdatePackageCollection(IFirmwareUpdateCollection* pCollection)
	{
		CComPtrMgd_003CIFirmwareUpdateCollection_003E spUpdatePackageCollection = new CComPtrMgd_003CIFirmwareUpdateCollection_003E();
		try
		{
			m_spUpdatePackageCollection = spUpdatePackageCollection;
			base._002Ector();
			m_spUpdatePackageCollection.op_Assign(pCollection);
			return;
		}
		catch
		{
			//try-fault
			((IDisposable)m_spUpdatePackageCollection).Dispose();
			throw;
		}
	}

	private void _007EUpdatePackageCollection()
	{
		m_spUpdatePackageCollection.Release();
		m_FirmwarePackage = null;
		m_GamesPackage = null;
		DisposePackages();
	}

	private unsafe void CachePackages()
	{
		if (m_packages != null)
		{
			return;
		}
		m_packages = new List<FirmwareUpdatePackage>();
		uint num = 0u;
		IFirmwareUpdateCollection* p = m_spUpdatePackageCollection.p;
		int num2 = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint*, int>)(int)(*(uint*)(*(int*)p + 12)))((nint)p, &num);
		if (global::_003CModule_003E.WPP_GLOBAL_Control != Unsafe.AsPointer(ref global::_003CModule_003E.WPP_GLOBAL_Control) && (((int*)global::_003CModule_003E.WPP_GLOBAL_Control)[15] & 4) != 0 && (uint)((byte*)global::_003CModule_003E.WPP_GLOBAL_Control)[57] >= 6u)
		{
			global::_003CModule_003E.WPP_SF_d(((ulong*)global::_003CModule_003E.WPP_GLOBAL_Control)[6], 11, (_GUID*)Unsafe.AsPointer(ref global::_003CModule_003E._003FA0xff79125d_002EWPP_FirmwareUpdateAPI_cpp_Traceguids), (int)num);
		}
		uint num3 = 0u;
		if (num2 >= 0)
		{
			Unsafe.SkipInit(out CComPtrNtv_003CIFirmwareMetadata_003E cComPtrNtv_003CIFirmwareMetadata_003E);
			while (num3 < num)
			{
				FirmwareUpdatePackage firmwareUpdatePackage = null;
				*(int*)(&cComPtrNtv_003CIFirmwareMetadata_003E) = 0;
				try
				{
					IFirmwareUpdateCollection* p2 = m_spUpdatePackageCollection.p;
					num2 = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint, IFirmwareMetadata**, int>)(int)(*(uint*)(*(int*)p2 + 16)))((nint)p2, num3, (IFirmwareMetadata**)(&cComPtrNtv_003CIFirmwareMetadata_003E));
					if (num2 >= 0)
					{
						firmwareUpdatePackage = new FirmwareUpdatePackage((IFirmwareMetadata*)(int)(*(uint*)(&cComPtrNtv_003CIFirmwareMetadata_003E)));
						m_packages.Add(firmwareUpdatePackage);
						IFirmwareUpdateCollection* p3 = m_spUpdatePackageCollection.p;
						num2 = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint, uint*, int, int>)(int)(*(uint*)(*(int*)p3 + 20)))((nint)p3, 1u, &num3, firmwareUpdatePackage.Selected ? 1 : 0);
					}
					EFirmwareUpdateType eFirmwareUpdateType = (EFirmwareUpdateType)(-1);
					if (num2 >= 0)
					{
						num2 = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, EFirmwareUpdateType*, int>)(int)(*(uint*)(*(int*)(int)(*(uint*)(&cComPtrNtv_003CIFirmwareMetadata_003E)) + 24)))((IntPtr)(*(int*)(&cComPtrNtv_003CIFirmwareMetadata_003E)), &eFirmwareUpdateType);
						if (num2 >= 0)
						{
							if (eFirmwareUpdateType == (EFirmwareUpdateType)0)
							{
								m_GamesPackage = firmwareUpdatePackage;
							}
							else
							{
								m_FirmwarePackage = firmwareUpdatePackage;
							}
						}
					}
					fixed (ushort* a = &Unsafe.As<char, ushort>(ref global::_003CModule_003E.PtrToStringChars(firmwareUpdatePackage.Name)))
					{
						try
						{
							if (global::_003CModule_003E.WPP_GLOBAL_Control != Unsafe.AsPointer(ref global::_003CModule_003E.WPP_GLOBAL_Control) && (((int*)global::_003CModule_003E.WPP_GLOBAL_Control)[15] & 4) != 0 && (uint)((byte*)global::_003CModule_003E.WPP_GLOBAL_Control)[57] >= 6u)
							{
								global::_003CModule_003E.WPP_SF_dSdl(((ulong*)global::_003CModule_003E.WPP_GLOBAL_Control)[6], 12, (_GUID*)Unsafe.AsPointer(ref global::_003CModule_003E._003FA0xff79125d_002EWPP_FirmwareUpdateAPI_cpp_Traceguids), (int)num3, a, (int)eFirmwareUpdateType, firmwareUpdatePackage.Selected ? 1 : 0);
							}
						}
						catch
						{
							//try-fault
							a = null;
							throw;
						}
					}
				}
				catch
				{
					//try-fault
					global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<CComPtrNtv_003CIFirmwareMetadata_003E*, void>)(&global::_003CModule_003E.CComPtrNtv_003CIFirmwareMetadata_003E_002E_007Bdtor_007D), &cComPtrNtv_003CIFirmwareMetadata_003E);
					throw;
				}
				global::_003CModule_003E.CComPtrNtv_003CIFirmwareMetadata_003E_002ERelease(&cComPtrNtv_003CIFirmwareMetadata_003E);
				num3++;
				if (num2 < 0)
				{
					break;
				}
			}
			if (num2 >= 0)
			{
				return;
			}
		}
		DisposePackages();
	}

	private unsafe void DisposePackages()
	{
		if (m_packages == null)
		{
			return;
		}
		if (global::_003CModule_003E.WPP_GLOBAL_Control != Unsafe.AsPointer(ref global::_003CModule_003E.WPP_GLOBAL_Control) && (((int*)global::_003CModule_003E.WPP_GLOBAL_Control)[15] & 4) != 0 && (uint)((byte*)global::_003CModule_003E.WPP_GLOBAL_Control)[57] >= 6u)
		{
			global::_003CModule_003E.WPP_SF_(((ulong*)global::_003CModule_003E.WPP_GLOBAL_Control)[6], 13, (_GUID*)Unsafe.AsPointer(ref global::_003CModule_003E._003FA0xff79125d_002EWPP_FirmwareUpdateAPI_cpp_Traceguids));
		}
		int num = m_packages.Count - 1;
		if (num >= 0)
		{
			do
			{
				((IDisposable)m_packages[num])?.Dispose();
				num--;
			}
			while (num >= 0);
		}
		m_packages = null;
	}

	protected virtual void Dispose([MarshalAs(UnmanagedType.U1)] bool P_0)
	{
		if (P_0)
		{
			try
			{
				_007EUpdatePackageCollection();
				return;
			}
			finally
			{
				((IDisposable)m_spUpdatePackageCollection).Dispose();
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
