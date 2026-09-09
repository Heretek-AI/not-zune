using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

internal class CComPtrMgd_003CIFirmwareUpdater_003E : IDisposable
{
	public unsafe IFirmwareUpdater* p = null;

	private void _007ECComPtrMgd_003CIFirmwareUpdater_003E()
	{
		Release();
	}

	private void _0021CComPtrMgd_003CIFirmwareUpdater_003E()
	{
		Release();
	}

	public unsafe void Release()
	{
		IFirmwareUpdater* ptr = p;
		IFirmwareUpdater* ptr2 = ptr;
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

	~CComPtrMgd_003CIFirmwareUpdater_003E()
	{
		Dispose(false);
	}

	public unsafe implicit operator IFirmwareUpdater*()
	{
		return p;
	}

	[SpecialName]
	public unsafe IFirmwareUpdater* op_MemberSelection()
	{
		return p;
	}

	[SpecialName]
	public unsafe IFirmwareUpdater* op_Assign(IFirmwareUpdater* lp)
	{
		Release();
		p = lp;
		if (lp != null)
		{
			((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint>)(int)(*(uint*)(*(int*)lp + 4)))((nint)lp);
		}
		return p;
	}

	public unsafe int CopyTo(IFirmwareUpdater** ppT)
	{
		if (ppT == null)
		{
			global::_003CModule_003E._ZuneShipAssert(1001u, 168u);
			return -2147467261;
		}
		*(int*)ppT = (int)p;
		IFirmwareUpdater* ptr = p;
		if (ptr != null)
		{
			((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint>)(int)(*(uint*)(*(int*)ptr + 4)))((nint)ptr);
		}
		return 0;
	}

	public unsafe int QueryInterface_003CIFirmwareUpdater2_003E(IFirmwareUpdater2** pp)
	{
		IFirmwareUpdater* ptr = p;
		if (ptr == null)
		{
			global::_003CModule_003E._ZuneShipAssert(1001u, 186u);
			return -2147467261;
		}
		return ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, _GUID*, void**, int>)(int)(*(uint*)(int)(*(uint*)ptr)))((nint)ptr, (_GUID*)Unsafe.AsPointer(ref global::_003CModule_003E._GUID_f066fc29_e525_4ddc_abe6_5213d22c14d2), (void**)pp);
	}
}
