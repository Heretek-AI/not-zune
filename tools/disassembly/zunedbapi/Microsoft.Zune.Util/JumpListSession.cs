using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Microsoft.Zune.Util;

public class JumpListSession : IDisposable
{
	private readonly CComPtrMgd_003CIJumpList_003E m_spJumpList;

	public unsafe bool IsAlive
	{
		[return: MarshalAs(UnmanagedType.U1)]
		get
		{
			IJumpList* p = m_spJumpList.p;
			Unsafe.SkipInit(out int num);
			((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, int*, int>)(int)(*(uint*)(*(int*)p + 36)))((nint)p, &num);
			return num == 1;
		}
	}

	public unsafe int GetDisallowedDestinations(out List<JumpListEntry> disallowedDestinationList)
	{
		if (m_spJumpList.p == null)
		{
			global::_003CModule_003E._ZuneShipAssert(1002u, 214u);
			return -2147418113;
		}
		disallowedDestinationList = null;
		uint num = 0u;
		IJumpList* p = m_spJumpList.p;
		int num2 = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint*, int>)(int)(*(uint*)(*(int*)p + 12)))((nint)p, &num);
		if (num2 >= 0)
		{
			IJumpListEntry** ptr = (IJumpListEntry**)global::_003CModule_003E.new_005B_005D((num > 1073741823) ? uint.MaxValue : (num << 2));
			if (ptr == null)
			{
				num2 = -2147024882;
			}
			if (num2 >= 0)
			{
				p = m_spJumpList.p;
				num2 = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint, IJumpListEntry**, int>)(int)(*(uint*)(*(int*)p + 16)))((nint)p, num, ptr);
				if (num2 >= 0)
				{
					disallowedDestinationList = new List<JumpListEntry>((int)num);
					uint num3 = 0u;
					if (0 < num)
					{
						do
						{
							disallowedDestinationList.Add(new JumpListEntry((IJumpListEntry*)(int)(*(uint*)((int)(num3 * 4) + (byte*)ptr))));
							int num4 = *(int*)((int)(num3 * 4) + (byte*)ptr);
							if (0 != num4)
							{
								((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint>)(int)(*(uint*)(*(int*)num4 + 8)))((IntPtr)num4);
								*(int*)((int)(num3 * 4) + (byte*)ptr) = 0;
							}
							num3++;
						}
						while (num3 < num);
					}
				}
			}
			if (null != ptr)
			{
				global::_003CModule_003E.delete_005B_005D(ptr);
			}
		}
		return num2;
	}

	public unsafe int CreateTask(out JumpListEntry task)
	{
		Unsafe.SkipInit(out CComPtrNtv_003CIJumpListEntry_003E cComPtrNtv_003CIJumpListEntry_003E);
		*(int*)(&cComPtrNtv_003CIJumpListEntry_003E) = 0;
		try
		{
			IJumpList* p = m_spJumpList.p;
			((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, IJumpListEntry**, int>)(int)(*(uint*)(*(int*)p + 20)))((nint)p, (IJumpListEntry**)(&cComPtrNtv_003CIJumpListEntry_003E));
			task = new JumpListEntry((IJumpListEntry*)(int)(*(uint*)(&cComPtrNtv_003CIJumpListEntry_003E)));
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

	public unsafe int CreateCategory(out JumpListCategory category)
	{
		Unsafe.SkipInit(out CComPtrNtv_003CIJumpListCategory_003E cComPtrNtv_003CIJumpListCategory_003E);
		*(int*)(&cComPtrNtv_003CIJumpListCategory_003E) = 0;
		try
		{
			IJumpList* p = m_spJumpList.p;
			((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, IJumpListCategory**, int>)(int)(*(uint*)(*(int*)p + 24)))((nint)p, (IJumpListCategory**)(&cComPtrNtv_003CIJumpListCategory_003E));
			category = new JumpListCategory((IJumpListCategory*)(int)(*(uint*)(&cComPtrNtv_003CIJumpListCategory_003E)));
		}
		catch
		{
			//try-fault
			global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<CComPtrNtv_003CIJumpListCategory_003E*, void>)(&global::_003CModule_003E.CComPtrNtv_003CIJumpListCategory_003E_002E_007Bdtor_007D), &cComPtrNtv_003CIJumpListCategory_003E);
			throw;
		}
		global::_003CModule_003E.CComPtrNtv_003CIJumpListCategory_003E_002ERelease(&cComPtrNtv_003CIJumpListCategory_003E);
		return 0;
	}

	public unsafe int Commit()
	{
		IJumpList* p = m_spJumpList.p;
		if (p == null)
		{
			global::_003CModule_003E._ZuneShipAssert(1002u, 303u);
			return -2147418113;
		}
		return ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, int>)(int)(*(uint*)(*(int*)p + 28)))((nint)p);
	}

	public unsafe int Cancel()
	{
		IJumpList* p = m_spJumpList.p;
		if (p == null)
		{
			global::_003CModule_003E._ZuneShipAssert(1002u, 319u);
			return -2147418113;
		}
		return ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, int>)(int)(*(uint*)(*(int*)p + 32)))((nint)p);
	}

	internal unsafe JumpListSession(IJumpList* pJumpList)
	{
		CComPtrMgd_003CIJumpList_003E spJumpList = new CComPtrMgd_003CIJumpList_003E();
		try
		{
			m_spJumpList = spJumpList;
			base._002Ector();
			m_spJumpList.op_Assign(pJumpList);
			return;
		}
		catch
		{
			//try-fault
			((IDisposable)m_spJumpList).Dispose();
			throw;
		}
	}

	public void _007EJumpListSession()
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
				((IDisposable)m_spJumpList).Dispose();
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
