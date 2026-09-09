using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;

namespace Microsoft.Zune.Util;

public class RadioStationManager : IDisposable
{
	private unsafe IRadioStationManager* m_pRadioStationManager = null;

	private static RadioStationManager sm_radioStationManager = null;

	private static object sm_lock = new object();

	public static RadioStationManager Instance
	{
		get
		{
			if (sm_radioStationManager == null)
			{
				try
				{
					Monitor.Enter(sm_lock);
					if (sm_radioStationManager == null)
					{
						RadioStationManager radioStationManager = new RadioStationManager();
						Thread.MemoryBarrier();
						sm_radioStationManager = radioStationManager;
					}
				}
				finally
				{
					Monitor.Exit(sm_lock);
				}
			}
			return sm_radioStationManager;
		}
	}

	private void _007ERadioStationManager()
	{
		_0021RadioStationManager();
	}

	private unsafe void _0021RadioStationManager()
	{
		IRadioStationManager* pRadioStationManager = m_pRadioStationManager;
		if (null != pRadioStationManager)
		{
			((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint>)(int)(*(uint*)(*(int*)pRadioStationManager + 8)))((nint)pRadioStationManager);
			m_pRadioStationManager = null;
		}
	}

	public unsafe RadioPlaylist GetRadioPlaylist(string uri)
	{
		IRadioStationManager* ptr = null;
		IRadioPlaylist* ptr2 = null;
		RadioPlaylist result = null;
		fixed (ushort* ptr3 = &Unsafe.As<char, ushort>(ref global::_003CModule_003E.PtrToStringChars(uri)))
		{
			int singleton = global::_003CModule_003E.GetSingleton((_GUID)global::_003CModule_003E._GUID_e1c20902_172d_4c40_bc82_5164f64ab783, (void**)(&ptr));
			if (singleton >= 0)
			{
				int num = *(int*)ptr + 12;
				singleton = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, ushort*, IRadioPlaylist**, int>)(int)(*(uint*)num))((nint)ptr, ptr3, &ptr2);
				if (singleton >= 0)
				{
					result = new RadioPlaylist(ptr2);
				}
			}
			if (null != ptr)
			{
				IRadioStationManager* intPtr = ptr;
				((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint>)(int)(*(uint*)(*(int*)intPtr + 8)))((nint)intPtr);
				ptr = null;
			}
			if (null != ptr2)
			{
				IRadioPlaylist* intPtr2 = ptr2;
				((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint>)(int)(*(uint*)(*(int*)intPtr2 + 8)))((nint)intPtr2);
			}
			return result;
		}
	}

	public unsafe void AddStation(string title, string sourceUrl, string imageUrl, RadioStationProgressHandler radioStationProgressHandler)
	{
		RadioStationProxy* ptr = (RadioStationProxy*)global::_003CModule_003E.@new(12u);
		RadioStationProxy* ptr2;
		try
		{
			ptr2 = ((ptr == null) ? null : global::_003CModule_003E.Microsoft_002EZune_002EUtil_002ERadioStationProxy_002E_007Bctor_007D(ptr, radioStationProgressHandler));
		}
		catch
		{
			//try-fault
			global::_003CModule_003E.delete(ptr);
			throw;
		}
		fixed (ushort* ptr3 = &Unsafe.As<char, ushort>(ref global::_003CModule_003E.PtrToStringChars(title)))
		{
			fixed (ushort* ptr4 = &Unsafe.As<char, ushort>(ref global::_003CModule_003E.PtrToStringChars(sourceUrl)))
			{
				fixed (ushort* ptr5 = &Unsafe.As<char, ushort>(ref global::_003CModule_003E.PtrToStringChars(imageUrl)))
				{
					Unsafe.SkipInit(out IRadioStationManager* ptr6);
					if (global::_003CModule_003E.GetSingleton((_GUID)global::_003CModule_003E._GUID_e1c20902_172d_4c40_bc82_5164f64ab783, (void**)(&ptr6)) >= 0)
					{
						int num = *(int*)ptr6 + 16;
						((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, ushort*, ushort*, ushort*, IAsyncCallback*, int>)(int)(*(uint*)num))((nint)ptr6, ptr3, ptr4, ptr5, (IAsyncCallback*)ptr2);
					}
					if (null != ptr2)
					{
						((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint>)(int)(*(uint*)(*(int*)ptr2 + 8)))((nint)ptr2);
					}
					if (null != ptr6)
					{
						IRadioStationManager* intPtr = ptr6;
						((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint>)(int)(*(uint*)(*(int*)intPtr + 8)))((nint)intPtr);
					}
				}
			}
		}
	}

	public unsafe void DeleteStation(string title, RadioStationProgressHandler radioStationProgressHandler)
	{
		RadioStationProxy* ptr = (RadioStationProxy*)global::_003CModule_003E.@new(12u);
		RadioStationProxy* ptr2;
		try
		{
			ptr2 = ((ptr == null) ? null : global::_003CModule_003E.Microsoft_002EZune_002EUtil_002ERadioStationProxy_002E_007Bctor_007D(ptr, radioStationProgressHandler));
		}
		catch
		{
			//try-fault
			global::_003CModule_003E.delete(ptr);
			throw;
		}
		fixed (ushort* ptr3 = &Unsafe.As<char, ushort>(ref global::_003CModule_003E.PtrToStringChars(title)))
		{
			Unsafe.SkipInit(out IRadioStationManager* ptr4);
			if (global::_003CModule_003E.GetSingleton((_GUID)global::_003CModule_003E._GUID_e1c20902_172d_4c40_bc82_5164f64ab783, (void**)(&ptr4)) >= 0)
			{
				int num = *(int*)ptr4 + 20;
				((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, ushort*, IAsyncCallback*, int>)(int)(*(uint*)num))((nint)ptr4, ptr3, (IAsyncCallback*)ptr2);
			}
			if (null != ptr2)
			{
				((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint>)(int)(*(uint*)(*(int*)ptr2 + 8)))((nint)ptr2);
			}
			if (null != ptr4)
			{
				IRadioStationManager* intPtr = ptr4;
				((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint>)(int)(*(uint*)(*(int*)intPtr + 8)))((nint)intPtr);
			}
		}
	}

	private unsafe RadioStationManager()
	{
	}

	protected virtual void Dispose([MarshalAs(UnmanagedType.U1)] bool P_0)
	{
		if (P_0)
		{
			_0021RadioStationManager();
			return;
		}
		try
		{
			_0021RadioStationManager();
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

	~RadioStationManager()
	{
		Dispose(false);
	}
}
