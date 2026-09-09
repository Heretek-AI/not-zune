using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace MicrosoftZuneLibrary;

public class FirmwareRestorePoint : IDisposable
{
	private readonly CComPtrMgd_003CIFirmwareRestorePoint_003E m_spRestorePoint;

	internal unsafe IFirmwareRestorePoint* NativeRestorePointPtr => m_spRestorePoint.p;

	public unsafe TimeSpan EstimatedRestoreTime
	{
		get
		{
			TimeSpan result = TimeSpan.Zero;
			uint num = 0u;
			IFirmwareRestorePoint* p = m_spRestorePoint.p;
			if (p != null && ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint*, int>)(int)(*(uint*)(*(int*)p + 20)))((nint)p, &num) >= 0)
			{
				result = TimeSpan.FromMilliseconds((double)num);
			}
			return result;
		}
	}

	public unsafe DateTime CreationDate
	{
		get
		{
			DateTime result = default(DateTime);
			Unsafe.SkipInit(out _SYSTEMTIME stValue);
			*(short*)(&stValue) = 0;
			// IL initblk instruction
			Unsafe.InitBlock(ref Unsafe.AddByteOffset(ref stValue, 2), 0, 14);
			IFirmwareRestorePoint* p = m_spRestorePoint.p;
			if (p != null && ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, _SYSTEMTIME*, int>)(int)(*(uint*)(*(int*)p + 16)))((nint)p, &stValue) >= 0)
			{
				return global::_003CModule_003E.SystemTimeToDateTime(stValue);
			}
			return result;
		}
	}

	public unsafe string OSVersion
	{
		get
		{
			string result = null;
			ushort* ptr = null;
			IFirmwareRestorePoint* p = m_spRestorePoint.p;
			if (p != null && ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, ushort**, int>)(int)(*(uint*)(*(int*)p + 12)))((nint)p, &ptr) >= 0)
			{
				result = new string((char*)ptr);
				global::_003CModule_003E.SysFreeString(ptr);
			}
			return result;
		}
	}

	internal unsafe FirmwareRestorePoint(IFirmwareRestorePoint* pRestorePoint)
	{
		CComPtrMgd_003CIFirmwareRestorePoint_003E spRestorePoint = new CComPtrMgd_003CIFirmwareRestorePoint_003E();
		try
		{
			m_spRestorePoint = spRestorePoint;
			base._002Ector();
			if (global::_003CModule_003E.WPP_GLOBAL_Control != Unsafe.AsPointer(ref global::_003CModule_003E.WPP_GLOBAL_Control) && (((int*)global::_003CModule_003E.WPP_GLOBAL_Control)[15] & 4) != 0 && (uint)((byte*)global::_003CModule_003E.WPP_GLOBAL_Control)[57] >= 5u)
			{
				global::_003CModule_003E.WPP_SF_q(((ulong*)global::_003CModule_003E.WPP_GLOBAL_Control)[6], 64, (_GUID*)Unsafe.AsPointer(ref global::_003CModule_003E._003FA0xff79125d_002EWPP_FirmwareUpdateAPI_cpp_Traceguids), pRestorePoint);
			}
			m_spRestorePoint.op_Assign(pRestorePoint);
			if (global::_003CModule_003E.WPP_GLOBAL_Control != Unsafe.AsPointer(ref global::_003CModule_003E.WPP_GLOBAL_Control) && (((int*)global::_003CModule_003E.WPP_GLOBAL_Control)[15] & 4) != 0 && (uint)((byte*)global::_003CModule_003E.WPP_GLOBAL_Control)[57] >= 5u)
			{
				global::_003CModule_003E.WPP_SF_(((ulong*)global::_003CModule_003E.WPP_GLOBAL_Control)[6], 65, (_GUID*)Unsafe.AsPointer(ref global::_003CModule_003E._003FA0xff79125d_002EWPP_FirmwareUpdateAPI_cpp_Traceguids));
			}
			return;
		}
		catch
		{
			//try-fault
			((IDisposable)m_spRestorePoint).Dispose();
			throw;
		}
	}

	private unsafe void _007EFirmwareRestorePoint()
	{
		if (global::_003CModule_003E.WPP_GLOBAL_Control != Unsafe.AsPointer(ref global::_003CModule_003E.WPP_GLOBAL_Control) && (((int*)global::_003CModule_003E.WPP_GLOBAL_Control)[15] & 4) != 0 && (uint)((byte*)global::_003CModule_003E.WPP_GLOBAL_Control)[57] >= 5u)
		{
			global::_003CModule_003E.WPP_SF_(((ulong*)global::_003CModule_003E.WPP_GLOBAL_Control)[6], 66, (_GUID*)Unsafe.AsPointer(ref global::_003CModule_003E._003FA0xff79125d_002EWPP_FirmwareUpdateAPI_cpp_Traceguids));
		}
		m_spRestorePoint.Release();
		if (global::_003CModule_003E.WPP_GLOBAL_Control != Unsafe.AsPointer(ref global::_003CModule_003E.WPP_GLOBAL_Control) && (((int*)global::_003CModule_003E.WPP_GLOBAL_Control)[15] & 4) != 0 && (uint)((byte*)global::_003CModule_003E.WPP_GLOBAL_Control)[57] >= 5u)
		{
			global::_003CModule_003E.WPP_SF_(((ulong*)global::_003CModule_003E.WPP_GLOBAL_Control)[6], 67, (_GUID*)Unsafe.AsPointer(ref global::_003CModule_003E._003FA0xff79125d_002EWPP_FirmwareUpdateAPI_cpp_Traceguids));
		}
	}

	protected virtual void Dispose([MarshalAs(UnmanagedType.U1)] bool P_0)
	{
		if (P_0)
		{
			try
			{
				_007EFirmwareRestorePoint();
				return;
			}
			finally
			{
				((IDisposable)m_spRestorePoint).Dispose();
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
