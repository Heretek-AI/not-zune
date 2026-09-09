using System;
using System.Runtime.InteropServices;

namespace MicrosoftZuneLibrary;

public class FirmwareUpdatePackage : IDisposable
{
	private readonly CComPtrMgd_003CIFirmwareMetadata_003E m_spFirmwareMetadata;

	private bool m_fSelected;

	internal bool Selected
	{
		[return: MarshalAs(UnmanagedType.U1)]
		get
		{
			return m_fSelected;
		}
		[param: MarshalAs(UnmanagedType.U1)]
		set
		{
			m_fSelected = value;
		}
	}

	public unsafe TimeSpan UpdateEstimatedTime
	{
		get
		{
			TimeSpan result = default(TimeSpan);
			CComPtrMgd_003CIFirmwareMetadata_003E spFirmwareMetadata = m_spFirmwareMetadata;
			if (spFirmwareMetadata.p != null)
			{
				uint num = 0u;
				IFirmwareMetadata* p = spFirmwareMetadata.p;
				if (((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint*, int>)(int)(*(uint*)(*(int*)p + 36)))((nint)p, &num) >= 0)
				{
					return TimeSpan.FromMilliseconds((double)num);
				}
			}
			return result;
		}
	}

	public unsafe string MoreInfoURL
	{
		get
		{
			string result = null;
			ushort* ptr = null;
			IFirmwareMetadata* p = m_spFirmwareMetadata.p;
			if (p != null && ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, ushort**, int>)(int)(*(uint*)(*(int*)p + 32)))((nint)p, &ptr) >= 0)
			{
				result = new string((char*)ptr);
				global::_003CModule_003E.SysFreeString(ptr);
			}
			return result;
		}
	}

	public unsafe string EULAContent
	{
		get
		{
			string result = null;
			ushort* ptr = null;
			IFirmwareMetadata* p = m_spFirmwareMetadata.p;
			if (p != null && ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, ushort**, int>)(int)(*(uint*)(*(int*)p + 28)))((nint)p, &ptr) >= 0)
			{
				result = new string((char*)ptr);
				global::_003CModule_003E.SysFreeString(ptr);
			}
			return result;
		}
	}

	public unsafe string Version
	{
		get
		{
			string result = null;
			ushort* ptr = null;
			IFirmwareMetadata* p = m_spFirmwareMetadata.p;
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
			IFirmwareMetadata* p = m_spFirmwareMetadata.p;
			if (p != null && ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, ushort**, int>)(int)(*(uint*)(*(int*)p + 16)))((nint)p, &ptr) >= 0)
			{
				result = new string((char*)ptr);
				global::_003CModule_003E.SysFreeString(ptr);
			}
			return result;
		}
	}

	public unsafe string Name
	{
		get
		{
			string result = null;
			ushort* ptr = null;
			IFirmwareMetadata* p = m_spFirmwareMetadata.p;
			if (p != null && ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, ushort**, int>)(int)(*(uint*)(*(int*)p + 12)))((nint)p, &ptr) >= 0)
			{
				result = new string((char*)ptr);
				global::_003CModule_003E.SysFreeString(ptr);
			}
			return result;
		}
	}

	public unsafe FirmwareUpdateType Type
	{
		get
		{
			EFirmwareUpdateType result = (EFirmwareUpdateType)(-1);
			IFirmwareMetadata* p = m_spFirmwareMetadata.p;
			if (p != null)
			{
				((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, EFirmwareUpdateType*, int>)(int)(*(uint*)(*(int*)p + 24)))((nint)p, &result);
			}
			return (FirmwareUpdateType)result;
		}
	}

	internal unsafe FirmwareUpdatePackage(IFirmwareMetadata* pFirmwareMetadata)
	{
		CComPtrMgd_003CIFirmwareMetadata_003E spFirmwareMetadata = new CComPtrMgd_003CIFirmwareMetadata_003E();
		try
		{
			m_spFirmwareMetadata = spFirmwareMetadata;
			base._002Ector();
			m_spFirmwareMetadata.op_Assign(pFirmwareMetadata);
			m_fSelected = false;
			return;
		}
		catch
		{
			//try-fault
			((IDisposable)m_spFirmwareMetadata).Dispose();
			throw;
		}
	}

	private void _007EFirmwareUpdatePackage()
	{
		m_spFirmwareMetadata.Release();
	}

	protected virtual void Dispose([MarshalAs(UnmanagedType.U1)] bool P_0)
	{
		if (P_0)
		{
			try
			{
				_007EFirmwareUpdatePackage();
				return;
			}
			finally
			{
				((IDisposable)m_spFirmwareMetadata).Dispose();
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
