using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Microsoft.Zune.Util;

public class JumpListCategory : IDisposable
{
	private readonly CComPtrMgd_003CIJumpListCategory_003E m_spCategory;

	public unsafe string Name
	{
		get
		{
			ushort* ptr = null;
			object result = null;
			IJumpListCategory* p = m_spCategory.p;
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
				IJumpListCategory* p = m_spCategory.p;
				int num = *(int*)p + 16;
				((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, ushort*, int>)(int)(*(uint*)num))((nint)p, ptr);
			}
		}
	}

	public unsafe int CreateDestination(out JumpListEntry destination)
	{
		Unsafe.SkipInit(out CComPtrNtv_003CIJumpListEntry_003E cComPtrNtv_003CIJumpListEntry_003E);
		*(int*)(&cComPtrNtv_003CIJumpListEntry_003E) = 0;
		try
		{
			IJumpListCategory* p = m_spCategory.p;
			((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, IJumpListEntry**, int>)(int)(*(uint*)(*(int*)p + 20)))((nint)p, (IJumpListEntry**)(&cComPtrNtv_003CIJumpListEntry_003E));
			destination = new JumpListEntry((IJumpListEntry*)(int)(*(uint*)(&cComPtrNtv_003CIJumpListEntry_003E)));
		}
		catch
		{
			//try-fault
			global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<CComPtrNtv_003CIJumpListEntry_003E*, void>)(&global::_003CModule_003E.CComPtrNtv_003CIJumpListEntry_003E_002E_007Bdtor_007D), &cComPtrNtv_003CIJumpListEntry_003E);
			throw;
		}
		global::_003CModule_003E.CComPtrNtv_003CIJumpListEntry_003E_002ERelease(&cComPtrNtv_003CIJumpListEntry_003E);
		return 0;
	}

	internal unsafe JumpListCategory(IJumpListCategory* pCategory)
	{
		CComPtrMgd_003CIJumpListCategory_003E spCategory = new CComPtrMgd_003CIJumpListCategory_003E();
		try
		{
			m_spCategory = spCategory;
			base._002Ector();
			m_spCategory.op_Assign(pCategory);
			return;
		}
		catch
		{
			//try-fault
			((IDisposable)m_spCategory).Dispose();
			throw;
		}
	}

	public void _007EJumpListCategory()
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
				((IDisposable)m_spCategory).Dispose();
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
