using System;
using System.Runtime.InteropServices;
using ZuneUI;

namespace MicrosoftZuneLibrary;

public class FirmwareUpdateErrorInfo : IDisposable
{
	private readonly CComPtrMgd_003CIFirmwareUpdateErrorInfo_003E m_spErrorInfo;

	public unsafe string Url
	{
		get
		{
			string result = null;
			ushort* ptr = null;
			IFirmwareUpdateErrorInfo* p = m_spErrorInfo.p;
			if (p != null && ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, ushort**, int>)(int)(*(uint*)(*(int*)p + 20)))((nint)p, &ptr) >= 0)
			{
				result = new string((char*)ptr);
				global::_003CModule_003E.SysFreeString(ptr);
			}
			return result;
		}
	}

	public unsafe string Description
	{
		get
		{
			string result = null;
			ushort* ptr = null;
			IFirmwareUpdateErrorInfo* p = m_spErrorInfo.p;
			if (p != null && ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, ushort**, int>)(int)(*(uint*)(*(int*)p + 16)))((nint)p, &ptr) >= 0)
			{
				result = new string((char*)ptr);
				global::_003CModule_003E.SysFreeString(ptr);
			}
			return result;
		}
	}

	public unsafe HRESULT HrStatus
	{
		get
		{
			int num = -2147467259;
			IFirmwareUpdateErrorInfo* p = m_spErrorInfo.p;
			if (p != null)
			{
				((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, int*, int>)(int)(*(uint*)(*(int*)p + 12)))((nint)p, &num);
			}
			return num;
		}
	}

	internal unsafe FirmwareUpdateErrorInfo(IFirmwareUpdateErrorInfo* pErrorInfo)
	{
		CComPtrMgd_003CIFirmwareUpdateErrorInfo_003E spErrorInfo = new CComPtrMgd_003CIFirmwareUpdateErrorInfo_003E();
		try
		{
			m_spErrorInfo = spErrorInfo;
			base._002Ector();
			m_spErrorInfo.op_Assign(pErrorInfo);
			return;
		}
		catch
		{
			//try-fault
			((IDisposable)m_spErrorInfo).Dispose();
			throw;
		}
	}

	private void _007EFirmwareUpdateErrorInfo()
	{
		m_spErrorInfo.Release();
	}

	protected virtual void Dispose([MarshalAs(UnmanagedType.U1)] bool P_0)
	{
		if (P_0)
		{
			try
			{
				_007EFirmwareUpdateErrorInfo();
				return;
			}
			finally
			{
				((IDisposable)m_spErrorInfo).Dispose();
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
