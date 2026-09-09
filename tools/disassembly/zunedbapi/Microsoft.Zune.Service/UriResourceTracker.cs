using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Microsoft.Zune.Service;

public class UriResourceTracker : IDisposable
{
	private static UriResourceTracker m_singletonInstance = null;

	private unsafe IUriResourceTracker* m_pUriResourceTracker = null;

	public static UriResourceTracker Instance
	{
		get
		{
			if (m_singletonInstance == null)
			{
				m_singletonInstance = new UriResourceTracker();
			}
			return m_singletonInstance;
		}
	}

	private unsafe UriResourceTracker()
	{
		IUriResourceTracker* pUriResourceTracker = null;
		if (global::_003CModule_003E.GetSingleton((_GUID)global::_003CModule_003E._GUID_ddbb9148_dea1_47dd_a0c1_1fdcf002c1e2, (void**)(&pUriResourceTracker)) >= 0)
		{
			m_pUriResourceTracker = pUriResourceTracker;
		}
	}

	private void _007EUriResourceTracker()
	{
		_0021UriResourceTracker();
	}

	private unsafe void _0021UriResourceTracker()
	{
		IUriResourceTracker* pUriResourceTracker = m_pUriResourceTracker;
		if (pUriResourceTracker != null)
		{
			((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint>)(int)(*(uint*)(*(int*)pUriResourceTracker + 8)))((nint)pUriResourceTracker);
			m_pUriResourceTracker = null;
		}
	}

	[return: MarshalAs(UnmanagedType.U1)]
	public unsafe bool SetResourceModified(string strUrlResource, [MarshalAs(UnmanagedType.U1)] bool fModified)
	{
		bool result = false;
		if (m_pUriResourceTracker != null)
		{
			fixed (ushort* ptr = &Unsafe.As<char, ushort>(ref global::_003CModule_003E.PtrToStringChars(strUrlResource)))
			{
				try
				{
					int num = *(int*)m_pUriResourceTracker + 12;
					result = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, ushort*, byte, int>)(int)(*(uint*)num))((nint)m_pUriResourceTracker, ptr, fModified ? ((byte)1) : ((byte)0)) >= 0;
				}
				catch
				{
					//try-fault
					ptr = null;
					throw;
				}
			}
		}
		return result;
	}

	[return: MarshalAs(UnmanagedType.U1)]
	public unsafe bool IsResourceModified(string strUrlResource)
	{
		bool result = false;
		if (m_pUriResourceTracker != null)
		{
			fixed (ushort* ptr = &Unsafe.As<char, ushort>(ref global::_003CModule_003E.PtrToStringChars(strUrlResource)))
			{
				try
				{
					int num = *(int*)m_pUriResourceTracker + 16;
					result = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, ushort*, byte>)(int)(*(uint*)num))((nint)m_pUriResourceTracker, ptr) != 0;
				}
				catch
				{
					//try-fault
					ptr = null;
					throw;
				}
			}
		}
		return result;
	}

	protected virtual void Dispose([MarshalAs(UnmanagedType.U1)] bool P_0)
	{
		if (P_0)
		{
			_0021UriResourceTracker();
			return;
		}
		try
		{
			_0021UriResourceTracker();
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

	~UriResourceTracker()
	{
		Dispose(false);
	}
}
