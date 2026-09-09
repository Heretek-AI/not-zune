using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

internal class CComPtrMgd_003CIAsyncCallback_003E : IDisposable
{
	public unsafe IAsyncCallback* p = null;

	private void _007ECComPtrMgd_003CIAsyncCallback_003E()
	{
		Release();
	}

	private void _0021CComPtrMgd_003CIAsyncCallback_003E()
	{
		Release();
	}

	public unsafe void Release()
	{
		IAsyncCallback* ptr = p;
		IAsyncCallback* ptr2 = ptr;
		if (ptr != null)
		{
			p = null;
			((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint>)(int)(*(uint*)(*(int*)ptr2 + 8)))((nint)ptr2);
		}
	}

	[SpecialName]
	public unsafe IAsyncCallback* op_Assign(IAsyncCallback* lp)
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

	~CComPtrMgd_003CIAsyncCallback_003E()
	{
		Dispose(false);
	}
}
