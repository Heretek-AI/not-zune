using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Microsoft.Zune.Util;

public class DRMCanDoQuery : IDisposable
{
	private unsafe IDRMQuery* _pDRMQuery;

	public unsafe DRMCanDoQuery()
	{
		Unsafe.SkipInit(out IDRMQuery* pDRMQuery);
		int num = global::_003CModule_003E.ZuneLibraryExports_002ECreateDRMQuery(&pDRMQuery);
		if (num < 0)
		{
			throw new ApplicationException(global::_003CModule_003E.GetErrorDescription(num));
		}
		_pDRMQuery = pDRMQuery;
	}

	private void _007EDRMCanDoQuery()
	{
		_0021DRMCanDoQuery();
	}

	private unsafe void _0021DRMCanDoQuery()
	{
		IDRMQuery* pDRMQuery = _pDRMQuery;
		if (pDRMQuery != null)
		{
			((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint>)(int)(*(uint*)(*(int*)pDRMQuery + 8)))((nint)pDRMQuery);
			_pDRMQuery = null;
		}
	}

	public unsafe void SetDeviceInfo([MarshalAs(UnmanagedType.U1)] bool fHasSerialNumber, string deviceCert)
	{
		fixed (ushort* ptr = &Unsafe.As<char, ushort>(ref global::_003CModule_003E.PtrToStringChars(deviceCert)))
		{
			int num = *(int*)_pDRMQuery + 12;
			int num2 = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, byte, ushort*, int>)(int)(*(uint*)num))((nint)_pDRMQuery, fHasSerialNumber ? ((byte)1) : ((byte)0), ptr);
			if (num2 < 0)
			{
				throw new ApplicationException(global::_003CModule_003E.GetErrorDescription(num2));
			}
		}
	}

	[return: MarshalAs(UnmanagedType.U1)]
	public unsafe bool CanBurnFile(string path)
	{
		fixed (ushort* ptr = &Unsafe.As<char, ushort>(ref global::_003CModule_003E.PtrToStringChars(path)))
		{
			int num = *(int*)_pDRMQuery + 28;
			Unsafe.SkipInit(out bool flag);
			int num2 = ((((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, ushort*, bool*, int>)(int)(*(uint*)num))((nint)_pDRMQuery, ptr, &flag) >= 0 && flag) ? 1 : 0);
			return (byte)num2 != 0;
		}
	}

	[return: MarshalAs(UnmanagedType.U1)]
	public unsafe bool CanBurnKID(string DRMKeyID)
	{
		fixed (ushort* ptr = &Unsafe.As<char, ushort>(ref global::_003CModule_003E.PtrToStringChars(DRMKeyID)))
		{
			int num = *(int*)_pDRMQuery + 32;
			Unsafe.SkipInit(out bool result);
			int num2 = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, ushort*, bool*, int>)(int)(*(uint*)num))((nint)_pDRMQuery, ptr, &result);
			if (num2 < 0)
			{
				throw new ApplicationException(global::_003CModule_003E.GetErrorDescription(num2));
			}
			return result;
		}
	}

	[return: MarshalAs(UnmanagedType.U1)]
	public unsafe bool CanSyncFile(string path)
	{
		fixed (ushort* ptr = &Unsafe.As<char, ushort>(ref global::_003CModule_003E.PtrToStringChars(path)))
		{
			int num = *(int*)_pDRMQuery + 36;
			Unsafe.SkipInit(out bool result);
			int num2 = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, ushort*, bool*, int>)(int)(*(uint*)num))((nint)_pDRMQuery, ptr, &result);
			if (num2 < 0)
			{
				throw new ApplicationException(global::_003CModule_003E.GetErrorDescription(num2));
			}
			return result;
		}
	}

	[return: MarshalAs(UnmanagedType.U1)]
	public unsafe bool CanSyncKID(string DRMKeyID)
	{
		fixed (ushort* ptr = &Unsafe.As<char, ushort>(ref global::_003CModule_003E.PtrToStringChars(DRMKeyID)))
		{
			int num = *(int*)_pDRMQuery + 40;
			Unsafe.SkipInit(out bool result);
			int num2 = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, ushort*, bool*, int>)(int)(*(uint*)num))((nint)_pDRMQuery, ptr, &result);
			if (num2 < 0)
			{
				throw new ApplicationException(global::_003CModule_003E.GetErrorDescription(num2));
			}
			return result;
		}
	}

	protected virtual void Dispose([MarshalAs(UnmanagedType.U1)] bool P_0)
	{
		if (P_0)
		{
			_0021DRMCanDoQuery();
			return;
		}
		try
		{
			_0021DRMCanDoQuery();
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

	~DRMCanDoQuery()
	{
		Dispose(false);
	}
}
