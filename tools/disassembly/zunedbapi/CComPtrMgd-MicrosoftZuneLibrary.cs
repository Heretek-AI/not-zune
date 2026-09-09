using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using MicrosoftZuneLibrary;

internal class CComPtrMgd_003CMicrosoftZuneLibrary_003A_003AFirmwareUpdateMediator_003E : IDisposable
{
	public unsafe FirmwareUpdateMediator* p = null;

	private void _007ECComPtrMgd_003CMicrosoftZuneLibrary_003A_003AFirmwareUpdateMediator_003E()
	{
		Release();
	}

	private void _0021CComPtrMgd_003CMicrosoftZuneLibrary_003A_003AFirmwareUpdateMediator_003E()
	{
		Release();
	}

	public unsafe void Release()
	{
		FirmwareUpdateMediator* ptr = p;
		FirmwareUpdateMediator* ptr2 = ptr;
		if (ptr != null)
		{
			p = null;
			((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint>)(int)(*(uint*)(*(int*)ptr2 + 8)))((nint)ptr2);
		}
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

	~CComPtrMgd_003CMicrosoftZuneLibrary_003A_003AFirmwareUpdateMediator_003E()
	{
		Dispose(false);
	}

	public unsafe implicit operator FirmwareUpdateMediator*()
	{
		return p;
	}

	[SpecialName]
	public unsafe FirmwareUpdateMediator* op_MemberSelection()
	{
		return p;
	}

	[SpecialName]
	public unsafe FirmwareUpdateMediator* op_Assign(FirmwareUpdateMediator* lp)
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
}
