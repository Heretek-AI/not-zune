using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;

namespace Microsoft.Zune.Util;

public class UpdateManager : IDisposable
{
	private readonly CComPtrMgd_003CIUpdateManager_003E m_spUpdateManager;

	private static UpdateManager sm_updateManager = null;

	private static object sm_lock = new object();

	public static UpdateManager Instance
	{
		get
		{
			if (sm_updateManager == null)
			{
				try
				{
					Monitor.Enter(sm_lock);
					if (sm_updateManager == null)
					{
						UpdateManager updateManager = new UpdateManager();
						Thread.MemoryBarrier();
						sm_updateManager = updateManager;
					}
				}
				finally
				{
					Monitor.Exit(sm_lock);
				}
			}
			return sm_updateManager;
		}
	}

	public unsafe void BeginUpdateCheck(UpdateProgressHandler updateProgressHandler)
	{
		IUpdateManager* ptr = null;
		UpdateProxy* ptr2 = (UpdateProxy*)global::_003CModule_003E.@new(12u);
		UpdateProxy* ptr3;
		try
		{
			ptr3 = ((ptr2 == null) ? null : global::_003CModule_003E.Microsoft_002EZune_002EUtil_002EUpdateProxy_002E_007Bctor_007D(ptr2, updateProgressHandler));
		}
		catch
		{
			//try-fault
			global::_003CModule_003E.delete(ptr2);
			throw;
		}
		UpdateProxy* ptr4 = ptr3;
		try
		{
			Monitor.Enter(sm_lock);
			if (global::_003CModule_003E.GetSingleton((_GUID)global::_003CModule_003E._GUID_9d21716a_ca61_4e24_a1ba_47b9e70e1e2c, (void**)(&ptr)) >= 0)
			{
				m_spUpdateManager.op_Assign(ptr);
				IUpdateManager* p = m_spUpdateManager.p;
				((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, IUpdateProgress*, int>)(int)(*(uint*)(*(int*)p + 12)))((nint)p, (IUpdateProgress*)ptr3);
			}
		}
		finally
		{
			if (null != ptr4)
			{
				((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint>)(int)(*(uint*)(*(int*)ptr4 + 8)))((nint)ptr4);
			}
			if (null != ptr)
			{
				IUpdateManager* intPtr = ptr;
				((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint>)(int)(*(uint*)(*(int*)intPtr + 8)))((nint)intPtr);
				ptr = null;
			}
			Monitor.Exit(sm_lock);
		}
	}

	public unsafe void CancelUpdateCheck()
	{
		try
		{
			Monitor.Enter(sm_lock);
			CComPtrMgd_003CIUpdateManager_003E spUpdateManager = m_spUpdateManager;
			IUpdateManager* p = spUpdateManager.p;
			if (null != p)
			{
				IUpdateManager* p2 = spUpdateManager.p;
				((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, int>)(int)(*(uint*)(*(int*)p2 + 16)))((nint)p2);
			}
		}
		finally
		{
			m_spUpdateManager.Release();
			Monitor.Exit(sm_lock);
		}
	}

	public unsafe void InstallUpdate(UpdateProgressHandler updateProgressHandler)
	{
		UpdateProxy* ptr = (UpdateProxy*)global::_003CModule_003E.@new(12u);
		UpdateProxy* ptr2;
		try
		{
			ptr2 = ((ptr == null) ? null : global::_003CModule_003E.Microsoft_002EZune_002EUtil_002EUpdateProxy_002E_007Bctor_007D(ptr, updateProgressHandler));
		}
		catch
		{
			//try-fault
			global::_003CModule_003E.delete(ptr);
			throw;
		}
		UpdateProxy* ptr3 = ptr2;
		try
		{
			Monitor.Enter(sm_lock);
			IUpdateManager* p = m_spUpdateManager.p;
			if (null == p)
			{
				Unsafe.SkipInit(out CComPtrNtv_003CIUpdateManager_003E cComPtrNtv_003CIUpdateManager_003E);
				*(int*)(&cComPtrNtv_003CIUpdateManager_003E) = 0;
				try
				{
					if (global::_003CModule_003E.GetSingleton((_GUID)global::_003CModule_003E._GUID_9d21716a_ca61_4e24_a1ba_47b9e70e1e2c, (void**)(&cComPtrNtv_003CIUpdateManager_003E)) >= 0)
					{
						IUpdateManager* ptr4 = (IUpdateManager*)(int)(*(uint*)(&cComPtrNtv_003CIUpdateManager_003E));
						m_spUpdateManager.op_Assign((IUpdateManager*)(int)(*(uint*)(&cComPtrNtv_003CIUpdateManager_003E)));
					}
				}
				catch
				{
					//try-fault
					global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<CComPtrNtv_003CIUpdateManager_003E*, void>)(&global::_003CModule_003E.CComPtrNtv_003CIUpdateManager_003E_002E_007Bdtor_007D), &cComPtrNtv_003CIUpdateManager_003E);
					throw;
				}
				global::_003CModule_003E.CComPtrNtv_003CIUpdateManager_003E_002ERelease(&cComPtrNtv_003CIUpdateManager_003E);
			}
			CComPtrMgd_003CIUpdateManager_003E spUpdateManager = m_spUpdateManager;
			IUpdateManager* p2 = spUpdateManager.p;
			if (null != p2)
			{
				IUpdateManager* p3 = spUpdateManager.p;
				((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, IUpdateProgress*, int>)(int)(*(uint*)(*(int*)p3 + 20)))((nint)p3, (IUpdateProgress*)ptr2);
			}
		}
		finally
		{
			global::_003CModule_003E.SafeRelease_003Cclass_0020Microsoft_003A_003AZune_003A_003AUtil_003A_003AUpdateProxy_003E(&ptr3);
			m_spUpdateManager.Release();
			Monitor.Exit(sm_lock);
		}
	}

	private UpdateManager()
	{
		CComPtrMgd_003CIUpdateManager_003E spUpdateManager = new CComPtrMgd_003CIUpdateManager_003E();
		try
		{
			m_spUpdateManager = spUpdateManager;
			base._002Ector();
			return;
		}
		catch
		{
			//try-fault
			((IDisposable)m_spUpdateManager).Dispose();
			throw;
		}
	}

	private void _007EUpdateManager()
	{
		m_spUpdateManager.Release();
	}

	protected virtual void Dispose([MarshalAs(UnmanagedType.U1)] bool P_0)
	{
		if (P_0)
		{
			try
			{
				_007EUpdateManager();
				return;
			}
			finally
			{
				((IDisposable)m_spUpdateManager).Dispose();
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
