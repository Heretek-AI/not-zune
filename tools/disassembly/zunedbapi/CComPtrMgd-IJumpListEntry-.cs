using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

internal class CComPtrMgd_003CIJumpListEntry_003E : IDisposable
{
	public unsafe IJumpListEntry* p = null;

	private void _007ECComPtrMgd_003CIJumpListEntry_003E()
	{
		Release();
	}

	private void _0021CComPtrMgd_003CIJumpListEntry_003E()
	{
		Release();
	}

	public unsafe void Release()
	{
		IJumpListEntry* ptr = p;
		IJumpListEntry* ptr2 = ptr;
		if (ptr != null)
		{
			p = null;
			((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint>)(int)(*(uint*)(*(int*)ptr2 + 8)))((nint)ptr2);
		}
	}

	[SpecialName]
	public unsafe IJumpListEntry* op_MemberSelection()
	{
		return p;
	}

	[SpecialName]
	public unsafe IJumpListEntry* op_Assign(IJumpListEntry* lp)
	{
		Release();
		p = lp;
		if (lp != null)
		{
			((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint>)(int)(*(uint*)(*(int*)lp + 4)))((nint)lp);
		}
		return p;
	}

	protected virtual void Dispose([MarshalAs(UnmanagedType.U1)] bool P_0)
	{
		if (P_0)
		{
			Release();
			return;
		}
		try
		{
			Release();
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

	~CComPtrMgd_003CIJumpListEntry_003E()
	{
		Dispose(false);
	}
}
