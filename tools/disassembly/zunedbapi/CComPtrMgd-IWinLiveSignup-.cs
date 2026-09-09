using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

internal class CComPtrMgd_003CIWinLiveSignup_003E : IDisposable
{
	public unsafe IWinLiveSignup* p = null;

	private void _007ECComPtrMgd_003CIWinLiveSignup_003E()
	{
		Release();
	}

	private void _0021CComPtrMgd_003CIWinLiveSignup_003E()
	{
		Release();
	}

	public unsafe void Release()
	{
		IWinLiveSignup* ptr = p;
		IWinLiveSignup* ptr2 = ptr;
		if (ptr != null)
		{
			p = null;
			((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint>)(int)(*(uint*)(*(int*)ptr2 + 8)))((nint)ptr2);
		}
	}

	[SpecialName]
	public unsafe IWinLiveSignup* op_MemberSelection()
	{
		return p;
	}

	[SpecialName]
	public unsafe IWinLiveSignup* op_Assign(IWinLiveSignup* lp)
	{
		Release();
		p = lp;
		if (lp != null)
		{
			((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint>)(int)(*(uint*)(*(int*)lp + 4)))((nint)lp);
		}
		return p;
	}

	[return: MarshalAs(UnmanagedType.U1)]
	public unsafe bool operator !()
	{
		return p == null;
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

	~CComPtrMgd_003CIWinLiveSignup_003E()
	{
		Dispose(false);
	}
}
