using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Microsoft.Zune.Util;

public class JumpListEntry : IDisposable
{
	private readonly CComPtrMgd_003CIJumpListEntry_003E m_spEntry;

	public unsafe int IconIndex
	{
		get
		{
			int result = 0;
			IJumpListEntry* p = m_spEntry.p;
			((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, int*, int>)(int)(*(uint*)(*(int*)p + 28)))((nint)p, &result);
			return result;
		}
		set
		{
			IJumpListEntry* p = m_spEntry.p;
			((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, int, int>)(int)(*(uint*)(*(int*)p + 32)))((nint)p, value);
		}
	}

	public unsafe string CommandLineArguments
	{
		get
		{
			ushort* ptr = null;
			object result = null;
			IJumpListEntry* p = m_spEntry.p;
			if (((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, ushort**, int>)(int)(*(uint*)(*(int*)p + 20)))((nint)p, &ptr) >= 0)
			{
				result = new string((char*)ptr);
			}
			global::_003CModule_003E.SysFreeString(ptr);
			return (string)result;
		}
		set
		{
			fixed (ushort* ptr = &Unsafe.As<char, ushort>(ref global::_003CModule_003E.PtrToStringChars(value)))
			{
				IJumpListEntry* p = m_spEntry.p;
				int num = *(int*)p + 24;
				((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, ushort*, int>)(int)(*(uint*)num))((nint)p, ptr);
			}
		}
	}

	public unsafe string Name
	{
		get
		{
			ushort* ptr = null;
			object result = null;
			IJumpListEntry* p = m_spEntry.p;
			if (((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, ushort**, int>)(int)(*(uint*)(*(int*)p + 12)))((nint)p, &ptr) >= 0)
			{
				result = new string((char*)ptr);
			}
			global::_003CModule_003E.SysFreeString(ptr);
			return (string)result;
		}
		set
		{
			fixed (ushort* ptr = &Unsafe.As<char, ushort>(ref global::_003CModule_003E.PtrToStringChars(value)))
			{
				IJumpListEntry* p = m_spEntry.p;
				int num = *(int*)p + 16;
				((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, ushort*, int>)(int)(*(uint*)num))((nint)p, ptr);
			}
		}
	}

	internal unsafe JumpListEntry(IJumpListEntry* pEntry)
	{
		CComPtrMgd_003CIJumpListEntry_003E spEntry = new CComPtrMgd_003CIJumpListEntry_003E();
		try
		{
			m_spEntry = spEntry;
			base._002Ector();
			m_spEntry.op_Assign(pEntry);
			return;
		}
		catch
		{
			//try-fault
			((IDisposable)m_spEntry).Dispose();
			throw;
		}
	}

	public void _007EJumpListEntry()
	{
	}

	protected virtual void Dispose([MarshalAs(UnmanagedType.U1)] bool P_0)
	{
		if (P_0)
		{
			try
			{
				return;
			}
			finally
			{
				((IDisposable)m_spEntry).Dispose();
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
