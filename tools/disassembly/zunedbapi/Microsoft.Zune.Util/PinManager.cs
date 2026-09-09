using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using ZuneUI;

namespace Microsoft.Zune.Util;

public class PinManager : IDisposable
{
	private unsafe IPinProvider* m_pPinProvider = null;

	private static PinManager sm_PinManager = null;

	private static object sm_lock = new object();

	public unsafe static PinManager Instance
	{
		get
		{
			if (sm_PinManager == null)
			{
				try
				{
					Monitor.Enter(sm_lock);
					if (sm_PinManager == null)
					{
						PinManager pinManager = new PinManager();
						Unsafe.SkipInit(out IMetadataManager* ptr);
						int singleton = global::_003CModule_003E.GetSingleton((_GUID)global::_003CModule_003E._GUID_6dd7146d_7a19_4fbb_9235_9e6c382fcc71, (void**)(&ptr));
						if (singleton < 0)
						{
							throw new ApplicationException(global::_003CModule_003E.GetErrorDescription(singleton));
						}
						Unsafe.SkipInit(out IPinProvider* pPinProvider);
						int num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, _GUID, void**, int>)(int)(*(uint*)(*(int*)ptr + 12)))((nint)ptr, (_GUID)global::_003CModule_003E._GUID_b396c324_6ab3_4e8e_a5cd_aafb3e01bedc, (void**)(&pPinProvider));
						if (num < 0)
						{
							throw new ApplicationException(global::_003CModule_003E.GetErrorDescription(num));
						}
						Thread.MemoryBarrier();
						pinManager.m_pPinProvider = pPinProvider;
						if (null != ptr)
						{
							IMetadataManager* intPtr = ptr;
							((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint>)(int)(*(uint*)(*(int*)intPtr + 8)))((nint)intPtr);
							ptr = null;
						}
						sm_PinManager = pinManager;
					}
				}
				finally
				{
					Monitor.Exit(sm_lock);
				}
			}
			return sm_PinManager;
		}
	}

	private void _007EPinManager()
	{
		_0021PinManager();
	}

	private unsafe void _0021PinManager()
	{
		IPinProvider* pPinProvider = m_pPinProvider;
		if (null != pPinProvider)
		{
			((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint>)(int)(*(uint*)(*(int*)pPinProvider + 8)))((nint)pPinProvider);
			m_pPinProvider = null;
		}
	}

	public unsafe HRESULT AddPin(EPinType ePinType, string szPinServiceRef, string szDescription, EServiceMediaType ePinServiceTypeId, int nUserId, int nOrdinal, out int nPinId)
	{
		//IL_001a->IL001c: Incompatible stack types: I4 vs Ref
		fixed (ushort* ptr = &Unsafe.As<char, ushort>(ref global::_003CModule_003E.PtrToStringChars(szPinServiceRef)))
		{
			fixed (ushort* ptr2 = &Unsafe.As<char, ushort>(ref global::_003CModule_003E.PtrToStringChars(szDescription)))
			{
				int num = -1;
				int* ptr3 = (int*)Unsafe.AsPointer(ref EPinType.ePinTypeQuickMix == ePinType ? ref *(_003F*)(&nOrdinal) : ref *(_003F*)null);
				int num2 = *(int*)m_pPinProvider + 12;
				int hr = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, EPinType, ushort*, ushort*, EServiceMediaType, int, int*, int*, int>)(int)(*(uint*)num2))((nint)m_pPinProvider, ePinType, ptr, ptr2, ePinServiceTypeId, nUserId, ptr3, &num);
				nPinId = num;
				return new HRESULT(hr);
			}
		}
	}

	public unsafe HRESULT AddPin(EPinType ePinType, int nPinMediaId, EMediaTypes ePinTypeId, int nUserId, int nOrdinal, out int pinId)
	{
		//IL_000a->IL000c: Incompatible stack types: I4 vs Ref
		int num = -1;
		int* ptr = (int*)Unsafe.AsPointer(ref EPinType.ePinTypeQuickMix == ePinType ? ref *(_003F*)(&nOrdinal) : ref *(_003F*)null);
		IPinProvider* pPinProvider = m_pPinProvider;
		int hr = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, EPinType, int, EMediaTypes, int, int*, int*, int>)(int)(*(uint*)(*(int*)pPinProvider + 16)))((nint)pPinProvider, ePinType, nPinMediaId, ePinTypeId, nUserId, ptr, &num);
		pinId = num;
		return new HRESULT(hr);
	}

	public unsafe HRESULT FindPin(EPinType ePinType, string szPinServiceRef, EServiceMediaType ePinServiceTypeId, int nUserId, int nMaxAge, out int nPinId)
	{
		fixed (ushort* ptr = &Unsafe.As<char, ushort>(ref global::_003CModule_003E.PtrToStringChars(szPinServiceRef)))
		{
			int num = -1;
			int num2 = *(int*)m_pPinProvider + 20;
			int hr = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, EPinType, ushort*, EServiceMediaType, int, int, int*, int>)(int)(*(uint*)num2))((nint)m_pPinProvider, ePinType, ptr, ePinServiceTypeId, nUserId, nMaxAge, &num);
			nPinId = num;
			return new HRESULT(hr);
		}
	}

	public unsafe HRESULT FindPin(EPinType ePinType, int nPinMediaId, EMediaTypes ePinTypeId, int nUserId, int nMaxAge, out int nPinId)
	{
		int num = -1;
		IPinProvider* pPinProvider = m_pPinProvider;
		int hr = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, EPinType, int, EMediaTypes, int, int, int*, int>)(int)(*(uint*)(*(int*)pPinProvider + 24)))((nint)pPinProvider, ePinType, nPinMediaId, ePinTypeId, nUserId, nMaxAge, &num);
		nPinId = num;
		return new HRESULT(hr);
	}

	public unsafe HRESULT DeletePin(int nPinId)
	{
		IPinProvider* pPinProvider = m_pPinProvider;
		return new HRESULT(((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, int, int>)(int)(*(uint*)(*(int*)pPinProvider + 32)))((nint)pPinProvider, nPinId));
	}

	private unsafe PinManager()
	{
	}

	protected virtual void Dispose([MarshalAs(UnmanagedType.U1)] bool P_0)
	{
		if (P_0)
		{
			_0021PinManager();
			return;
		}
		try
		{
			_0021PinManager();
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

	~PinManager()
	{
		Dispose(false);
	}
}
