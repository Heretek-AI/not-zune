using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;

namespace MicrosoftZuneLibrary;

public class ZuneLibraryCDDeviceList : IDisposable
{
	private unsafe IWMPCDDeviceList* m_pDeviceList;

	private uint m_dwAdviseCookie;

	private bool m_fAdvised;

	private bool m_disposed;

	private OnMediaChangedHandler m_MediaChangedHandler;

	private int m_RefCount;

	public unsafe int Count
	{
		get
		{
			uint result = 0u;
			IWMPCDDeviceList* pDeviceList = m_pDeviceList;
			if (pDeviceList != null)
			{
				((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint*, int>)(int)(*(uint*)(*(int*)pDeviceList + 12)))((nint)pDeviceList, &result);
			}
			return (int)result;
		}
	}

	[SpecialName]
	public virtual event OnMediaChangedHandler MediaChangedHandler
	{
		add
		{
			m_MediaChangedHandler = (OnMediaChangedHandler)Delegate.Combine(m_MediaChangedHandler, value);
		}
		remove
		{
			m_MediaChangedHandler = (OnMediaChangedHandler)Delegate.Remove(m_MediaChangedHandler, value);
		}
	}

	public unsafe ZuneLibraryCDDeviceList(IWMPCDDeviceList* pDeviceList)
	{
		m_pDeviceList = pDeviceList;
		m_fAdvised = false;
		m_disposed = false;
		m_RefCount = 0;
		base._002Ector();
		IWMPCDDeviceList* pDeviceList2 = m_pDeviceList;
		if (pDeviceList2 == null)
		{
			return;
		}
		((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint>)(int)(*(uint*)(*(int*)pDeviceList2 + 4)))((nint)pDeviceList2);
		CDDeviceCallback* ptr = (CDDeviceCallback*)global::_003CModule_003E.@new(12u);
		CDDeviceCallback* ptr2;
		try
		{
			ptr2 = ((ptr == null) ? null : global::_003CModule_003E.MicrosoftZuneLibrary_002ECDDeviceCallback_002E_007Bctor_007D(ptr, this));
		}
		catch
		{
			//try-fault
			global::_003CModule_003E.delete(ptr);
			throw;
		}
		Unsafe.SkipInit(out IZuneCDDeviceCallback* ptr3);
		if (ptr2 == null || ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, _GUID*, void**, int>)(int)(*(uint*)(int)(*(uint*)ptr2)))((nint)ptr2, (_GUID*)Unsafe.AsPointer(ref global::_003CModule_003E.IID_IUnknown), (void**)(&ptr3)) < 0)
		{
			return;
		}
		fixed (uint* ptr4 = &m_dwAdviseCookie)
		{
			try
			{
				int num = *(int*)m_pDeviceList + 24;
				if (((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, IZuneCDDeviceCallback*, uint*, int>)(int)(*(uint*)num))((nint)m_pDeviceList, ptr3, ptr4) >= 0)
				{
					m_fAdvised = true;
				}
				IZuneCDDeviceCallback* intPtr = ptr3;
				((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint>)(int)(*(uint*)(*(int*)intPtr + 8)))((nint)intPtr);
			}
			catch
			{
				//try-fault
				ptr4 = null;
				throw;
			}
		}
	}

	private void _007EZuneLibraryCDDeviceList()
	{
		m_MediaChangedHandler = null;
		_0021ZuneLibraryCDDeviceList();
	}

	private unsafe void _0021ZuneLibraryCDDeviceList()
	{
		if (m_disposed)
		{
			return;
		}
		IWMPCDDeviceList* pDeviceList = m_pDeviceList;
		if (pDeviceList != null)
		{
			if (m_fAdvised)
			{
				((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint, int>)(int)(*(uint*)(*(int*)pDeviceList + 28)))((nint)pDeviceList, m_dwAdviseCookie);
				m_fAdvised = false;
			}
			pDeviceList = m_pDeviceList;
			IWMPCDDeviceList* intPtr = pDeviceList;
			((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint>)(int)(*(uint*)(*(int*)intPtr + 8)))((nint)intPtr);
			m_pDeviceList = null;
		}
		m_disposed = true;
	}

	public uint AddRef()
	{
		return (uint)Interlocked.Increment(ref m_RefCount);
	}

	public uint Release()
	{
		int num = Interlocked.Decrement(ref m_RefCount);
		if (0 == num)
		{
			_0021ZuneLibraryCDDeviceList();
		}
		return (uint)num;
	}

	public unsafe ZuneLibraryCDDevice GetItem(int idx)
	{
		if (m_pDeviceList == null)
		{
			return null;
		}
		if (idx >= Count)
		{
			return null;
		}
		IWMPCDDevice* pDevice = null;
		IWMPCDDeviceList* pDeviceList = m_pDeviceList;
		if (((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint, IWMPCDDevice**, int>)(int)(*(uint*)(*(int*)pDeviceList + 16)))((nint)pDeviceList, (uint)idx, &pDevice) < 0)
		{
			return null;
		}
		return new ZuneLibraryCDDevice(pDevice);
	}

	internal void OnMediaChanged(ushort driveLetter, int fMediaPresent)
	{
		OnMediaChangedHandler mediaChangedHandler = m_MediaChangedHandler;
		if (mediaChangedHandler != null)
		{
			bool fMediaArrived = ((fMediaPresent != 0) ? true : false);
			mediaChangedHandler((char)driveLetter, fMediaArrived);
		}
	}

	protected virtual void Dispose([MarshalAs(UnmanagedType.U1)] bool P_0)
	{
		if (P_0)
		{
			_007EZuneLibraryCDDeviceList();
			return;
		}
		try
		{
			_0021ZuneLibraryCDDeviceList();
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

	~ZuneLibraryCDDeviceList()
	{
		Dispose(false);
	}
}
