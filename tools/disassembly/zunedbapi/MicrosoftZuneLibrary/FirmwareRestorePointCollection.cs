using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace MicrosoftZuneLibrary;

public class FirmwareRestorePointCollection : IDisposable
{
	private readonly CComPtrMgd_003CIFirmwareRestorePointCollection_003E m_spRestorePointCollection;

	public unsafe int Count
	{
		get
		{
			int result = 0;
			uint num = 0u;
			IFirmwareRestorePointCollection* p = m_spRestorePointCollection.p;
			if (p != null && ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint*, int>)(int)(*(uint*)(*(int*)p + 12)))((nint)p, &num) >= 0)
			{
				result = (int)num;
			}
			return result;
		}
	}

	public unsafe FirmwareRestorePoint GetRestorePoint(int index)
	{
		FirmwareRestorePoint result = null;
		Unsafe.SkipInit(out CComPtrNtv_003CIFirmwareRestorePoint_003E cComPtrNtv_003CIFirmwareRestorePoint_003E);
		*(int*)(&cComPtrNtv_003CIFirmwareRestorePoint_003E) = 0;
		try
		{
			CComPtrMgd_003CIFirmwareRestorePointCollection_003E spRestorePointCollection = m_spRestorePointCollection;
			if (spRestorePointCollection.p != null)
			{
				IFirmwareRestorePointCollection* p = spRestorePointCollection.p;
				if (((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint, IFirmwareRestorePoint**, int>)(int)(*(uint*)(*(int*)p + 16)))((nint)p, (uint)index, (IFirmwareRestorePoint**)(&cComPtrNtv_003CIFirmwareRestorePoint_003E)) >= 0)
				{
					result = new FirmwareRestorePoint((IFirmwareRestorePoint*)(int)(*(uint*)(&cComPtrNtv_003CIFirmwareRestorePoint_003E)));
				}
			}
		}
		catch
		{
			//try-fault
			global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<CComPtrNtv_003CIFirmwareRestorePoint_003E*, void>)(&global::_003CModule_003E.CComPtrNtv_003CIFirmwareRestorePoint_003E_002E_007Bdtor_007D), &cComPtrNtv_003CIFirmwareRestorePoint_003E);
			throw;
		}
		global::_003CModule_003E.CComPtrNtv_003CIFirmwareRestorePoint_003E_002ERelease(&cComPtrNtv_003CIFirmwareRestorePoint_003E);
		return result;
	}

	internal unsafe FirmwareRestorePointCollection(IFirmwareRestorePointCollection* pRestorePointCollection)
	{
		CComPtrMgd_003CIFirmwareRestorePointCollection_003E spRestorePointCollection = new CComPtrMgd_003CIFirmwareRestorePointCollection_003E();
		try
		{
			m_spRestorePointCollection = spRestorePointCollection;
			base._002Ector();
			if (global::_003CModule_003E.WPP_GLOBAL_Control != Unsafe.AsPointer(ref global::_003CModule_003E.WPP_GLOBAL_Control) && (((int*)global::_003CModule_003E.WPP_GLOBAL_Control)[15] & 4) != 0 && (uint)((byte*)global::_003CModule_003E.WPP_GLOBAL_Control)[57] >= 5u)
			{
				global::_003CModule_003E.WPP_SF_q(((ulong*)global::_003CModule_003E.WPP_GLOBAL_Control)[6], 68, (_GUID*)Unsafe.AsPointer(ref global::_003CModule_003E._003FA0xff79125d_002EWPP_FirmwareUpdateAPI_cpp_Traceguids), pRestorePointCollection);
			}
			m_spRestorePointCollection.op_Assign(pRestorePointCollection);
			if (global::_003CModule_003E.WPP_GLOBAL_Control != Unsafe.AsPointer(ref global::_003CModule_003E.WPP_GLOBAL_Control) && (((int*)global::_003CModule_003E.WPP_GLOBAL_Control)[15] & 4) != 0 && (uint)((byte*)global::_003CModule_003E.WPP_GLOBAL_Control)[57] >= 5u)
			{
				global::_003CModule_003E.WPP_SF_(((ulong*)global::_003CModule_003E.WPP_GLOBAL_Control)[6], 69, (_GUID*)Unsafe.AsPointer(ref global::_003CModule_003E._003FA0xff79125d_002EWPP_FirmwareUpdateAPI_cpp_Traceguids));
			}
			return;
		}
		catch
		{
			//try-fault
			((IDisposable)m_spRestorePointCollection).Dispose();
			throw;
		}
	}

	private unsafe void _007EFirmwareRestorePointCollection()
	{
		if (global::_003CModule_003E.WPP_GLOBAL_Control != Unsafe.AsPointer(ref global::_003CModule_003E.WPP_GLOBAL_Control) && (((int*)global::_003CModule_003E.WPP_GLOBAL_Control)[15] & 4) != 0 && (uint)((byte*)global::_003CModule_003E.WPP_GLOBAL_Control)[57] >= 5u)
		{
			global::_003CModule_003E.WPP_SF_(((ulong*)global::_003CModule_003E.WPP_GLOBAL_Control)[6], 70, (_GUID*)Unsafe.AsPointer(ref global::_003CModule_003E._003FA0xff79125d_002EWPP_FirmwareUpdateAPI_cpp_Traceguids));
		}
		m_spRestorePointCollection.Release();
		if (global::_003CModule_003E.WPP_GLOBAL_Control != Unsafe.AsPointer(ref global::_003CModule_003E.WPP_GLOBAL_Control) && (((int*)global::_003CModule_003E.WPP_GLOBAL_Control)[15] & 4) != 0 && (uint)((byte*)global::_003CModule_003E.WPP_GLOBAL_Control)[57] >= 5u)
		{
			global::_003CModule_003E.WPP_SF_(((ulong*)global::_003CModule_003E.WPP_GLOBAL_Control)[6], 71, (_GUID*)Unsafe.AsPointer(ref global::_003CModule_003E._003FA0xff79125d_002EWPP_FirmwareUpdateAPI_cpp_Traceguids));
		}
	}

	protected virtual void Dispose([MarshalAs(UnmanagedType.U1)] bool P_0)
	{
		if (P_0)
		{
			try
			{
				_007EFirmwareRestorePointCollection();
				return;
			}
			finally
			{
				((IDisposable)m_spRestorePointCollection).Dispose();
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
