using System;
using System.Runtime.InteropServices;
using ZuneUI;

namespace Microsoft.Zune.Util;

public class ContentRefreshTask : IDisposable
{
	private unsafe IContentRefreshTask* m_pContentRefreshTask = null;

	private static ContentRefreshTask sm_ContentRefreshTask = null;

	public static bool HasInstance
	{
		[return: MarshalAs(UnmanagedType.U1)]
		get
		{
			return sm_ContentRefreshTask != null;
		}
	}

	public static ContentRefreshTask Instance
	{
		get
		{
			if (sm_ContentRefreshTask == null)
			{
				sm_ContentRefreshTask = new ContentRefreshTask();
			}
			return sm_ContentRefreshTask;
		}
	}

	private void _007EContentRefreshTask()
	{
		_0021ContentRefreshTask();
	}

	private unsafe void _0021ContentRefreshTask()
	{
		IContentRefreshTask* pContentRefreshTask = m_pContentRefreshTask;
		if (pContentRefreshTask != null)
		{
			IContentRefreshTask* intPtr = pContentRefreshTask;
			((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, int>)(int)(*(uint*)(*(int*)intPtr + 16)))((nint)intPtr);
			pContentRefreshTask = m_pContentRefreshTask;
			if (null != pContentRefreshTask)
			{
				IContentRefreshTask* intPtr2 = pContentRefreshTask;
				((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint>)(int)(*(uint*)(*(int*)intPtr2 + 8)))((nint)intPtr2);
				m_pContentRefreshTask = null;
			}
		}
	}

	public unsafe HRESULT StartContentRefresh(AsyncCompleteHandler completeHandler)
	{
		int num = 0;
		AsyncCallbackWrapper* ptr = (AsyncCallbackWrapper*)global::_003CModule_003E.@new(12u);
		AsyncCallbackWrapper* ptr2;
		try
		{
			ptr2 = ((ptr == null) ? null : global::_003CModule_003E.Microsoft_002EZune_002EUtil_002EAsyncCallbackWrapper_002E_007Bctor_007D(ptr, completeHandler));
		}
		catch
		{
			//try-fault
			global::_003CModule_003E.delete(ptr);
			throw;
		}
		if (ptr2 == null)
		{
			num = -2147418113;
		}
		IContentRefreshTask* ptr3 = null;
		if (num >= 0)
		{
			num = global::_003CModule_003E.ZuneLibraryExports_002ECreateContentRefreshTask((IAsyncCallback*)ptr2, &ptr3);
			if (num >= 0)
			{
				if (null != ptr3)
				{
					m_pContentRefreshTask = ptr3;
				}
			}
			else if (null != ptr3)
			{
				IContentRefreshTask* intPtr = ptr3;
				((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint>)(int)(*(uint*)(*(int*)intPtr + 8)))((nint)intPtr);
				ptr3 = null;
			}
		}
		if (null != ptr2)
		{
			((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint>)(int)(*(uint*)(*(int*)ptr2 + 8)))((nint)ptr2);
		}
		return new HRESULT(num);
	}

	private unsafe ContentRefreshTask()
	{
	}

	protected virtual void Dispose([MarshalAs(UnmanagedType.U1)] bool P_0)
	{
		if (P_0)
		{
			_0021ContentRefreshTask();
			return;
		}
		try
		{
			_0021ContentRefreshTask();
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

	~ContentRefreshTask()
	{
		Dispose(false);
	}
}
