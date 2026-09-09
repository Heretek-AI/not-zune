using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using ZuneUI;

namespace Microsoft.Zune.QuickMix;

public class QuickMix
{
	private static object sm_lock = new object();

	private static QuickMix sm_instance;

	private QuickMixProgressHandler m_onProgressHandler;

	public unsafe bool IsReady
	{
		[return: MarshalAs(UnmanagedType.U1)]
		get
		{
			bool result = false;
			Unsafe.SkipInit(out CComPtrNtv_003CIQuickMixManager_003E cComPtrNtv_003CIQuickMixManager_003E);
			*(int*)(&cComPtrNtv_003CIQuickMixManager_003E) = 0;
			try
			{
				Unsafe.SkipInit(out QUICK_MIX_STATUS_INFO qUICK_MIX_STATUS_INFO);
				*(sbyte*)(&qUICK_MIX_STATUS_INFO) = 0;
				// IL initblk instruction
				Unsafe.InitBlock(ref Unsafe.AddByteOffset(ref qUICK_MIX_STATUS_INFO, 1), 0, 15);
				int singleton = global::_003CModule_003E.GetSingleton((_GUID)global::_003CModule_003E._GUID_d69e22ae_7e21_4959_be6e_14462eb96f64, (void**)(&cComPtrNtv_003CIQuickMixManager_003E));
				if (singleton >= 0)
				{
					singleton = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, QUICK_MIX_STATUS_INFO*, int>)(int)(*(uint*)(*(int*)(int)(*(uint*)(&cComPtrNtv_003CIQuickMixManager_003E)) + 28)))((IntPtr)(*(int*)(&cComPtrNtv_003CIQuickMixManager_003E)), &qUICK_MIX_STATUS_INFO);
					if (singleton >= 0)
					{
						result = *(bool*)(&qUICK_MIX_STATUS_INFO);
					}
				}
			}
			catch
			{
				//try-fault
				global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<CComPtrNtv_003CIQuickMixManager_003E*, void>)(&global::_003CModule_003E.CComPtrNtv_003CIQuickMixManager_003E_002E_007Bdtor_007D), &cComPtrNtv_003CIQuickMixManager_003E);
				throw;
			}
			global::_003CModule_003E.CComPtrNtv_003CIQuickMixManager_003E_002ERelease(&cComPtrNtv_003CIQuickMixManager_003E);
			return result;
		}
	}

	public static QuickMix Instance
	{
		get
		{
			if (sm_instance == null)
			{
				try
				{
					Monitor.Enter(sm_lock);
					if (sm_instance == null)
					{
						QuickMix quickMix = new QuickMix();
						int num = quickMix.Initialize();
						Thread.MemoryBarrier();
						if (num >= 0)
						{
							sm_instance = quickMix;
						}
					}
				}
				finally
				{
					Monitor.Exit(sm_lock);
				}
			}
			return sm_instance;
		}
	}

	[SpecialName]
	public unsafe event QuickMixProgressHandler OnProgress
	{
		add
		{
			m_onProgressHandler = (QuickMixProgressHandler)Delegate.Combine(m_onProgressHandler, value);
			Unsafe.SkipInit(out CComPtrNtv_003CIQuickMixManager_003E cComPtrNtv_003CIQuickMixManager_003E);
			*(int*)(&cComPtrNtv_003CIQuickMixManager_003E) = 0;
			try
			{
				Unsafe.SkipInit(out QUICK_MIX_STATUS_INFO qUICK_MIX_STATUS_INFO);
				*(sbyte*)(&qUICK_MIX_STATUS_INFO) = 0;
				// IL initblk instruction
				Unsafe.InitBlock(ref Unsafe.AddByteOffset(ref qUICK_MIX_STATUS_INFO, 1), 0, 15);
				if (global::_003CModule_003E.GetSingleton((_GUID)global::_003CModule_003E._GUID_d69e22ae_7e21_4959_be6e_14462eb96f64, (void**)(&cComPtrNtv_003CIQuickMixManager_003E)) >= 0)
				{
					((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, QUICK_MIX_STATUS_INFO*, int>)(int)(*(uint*)(*(int*)(int)(*(uint*)(&cComPtrNtv_003CIQuickMixManager_003E)) + 28)))((IntPtr)(*(int*)(&cComPtrNtv_003CIQuickMixManager_003E)), &qUICK_MIX_STATUS_INFO);
				}
				value(Unsafe.As<QUICK_MIX_STATUS_INFO, float>(ref Unsafe.AddByteOffset(ref qUICK_MIX_STATUS_INFO, 4)), Unsafe.As<QUICK_MIX_STATUS_INFO, int>(ref Unsafe.AddByteOffset(ref qUICK_MIX_STATUS_INFO, 12)));
			}
			catch
			{
				//try-fault
				global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<CComPtrNtv_003CIQuickMixManager_003E*, void>)(&global::_003CModule_003E.CComPtrNtv_003CIQuickMixManager_003E_002E_007Bdtor_007D), &cComPtrNtv_003CIQuickMixManager_003E);
				throw;
			}
			global::_003CModule_003E.CComPtrNtv_003CIQuickMixManager_003E_002ERelease(&cComPtrNtv_003CIQuickMixManager_003E);
		}
		remove
		{
			m_onProgressHandler = (QuickMixProgressHandler)Delegate.Remove(m_onProgressHandler, value);
		}
	}

	public unsafe HRESULT CreateSession(EQuickMixMode eQuickMixMode, Guid serviceMediaId, EMediaTypes eMediaType, string mediaTitle, out QuickMixSession quickMixSession)
	{
		Unsafe.SkipInit(out CComPtrNtv_003CIQuickMixManager_003E cComPtrNtv_003CIQuickMixManager_003E);
		*(int*)(&cComPtrNtv_003CIQuickMixManager_003E) = 0;
		HRESULT result;
		try
		{
			Unsafe.SkipInit(out CComPtrNtv_003CIQuickMixSession_003E cComPtrNtv_003CIQuickMixSession_003E);
			*(int*)(&cComPtrNtv_003CIQuickMixSession_003E) = 0;
			try
			{
				int num = global::_003CModule_003E.GetSingleton((_GUID)global::_003CModule_003E._GUID_d69e22ae_7e21_4959_be6e_14462eb96f64, (void**)(&cComPtrNtv_003CIQuickMixManager_003E));
				if (num >= 0)
				{
					_GUID gUID = global::_003CModule_003E.GuidToGUID(serviceMediaId);
					fixed (ushort* ptr = &Unsafe.As<char, ushort>(ref global::_003CModule_003E.PtrToStringChars(mediaTitle)))
					{
						try
						{
							int num2 = *(int*)(int)(*(uint*)(&cComPtrNtv_003CIQuickMixManager_003E)) + 12;
							num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, EQuickMixMode, _GUID*, EMediaTypes, ushort*, IQuickMixSession**, int>)(int)(*(uint*)num2))((IntPtr)(*(int*)(&cComPtrNtv_003CIQuickMixManager_003E)), eQuickMixMode, &gUID, eMediaType, ptr, (IQuickMixSession**)(&cComPtrNtv_003CIQuickMixSession_003E));
						}
						catch
						{
							//try-fault
							ptr = null;
							throw;
						}
					}
					if (num >= 0)
					{
						quickMixSession = new QuickMixSession((IQuickMixSession*)(int)(*(uint*)(&cComPtrNtv_003CIQuickMixSession_003E)));
					}
				}
				result = num;
			}
			catch
			{
				//try-fault
				global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<CComPtrNtv_003CIQuickMixSession_003E*, void>)(&global::_003CModule_003E.CComPtrNtv_003CIQuickMixSession_003E_002E_007Bdtor_007D), &cComPtrNtv_003CIQuickMixSession_003E);
				throw;
			}
			global::_003CModule_003E.CComPtrNtv_003CIQuickMixSession_003E_002ERelease(&cComPtrNtv_003CIQuickMixSession_003E);
		}
		catch
		{
			//try-fault
			global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<CComPtrNtv_003CIQuickMixManager_003E*, void>)(&global::_003CModule_003E.CComPtrNtv_003CIQuickMixManager_003E_002E_007Bdtor_007D), &cComPtrNtv_003CIQuickMixManager_003E);
			throw;
		}
		global::_003CModule_003E.CComPtrNtv_003CIQuickMixManager_003E_002ERelease(&cComPtrNtv_003CIQuickMixManager_003E);
		return result;
	}

	public unsafe HRESULT CreateSession(EQuickMixMode eQuickMixMode, int[] seedMediaIds, EMediaTypes eMediaType, out QuickMixSession quickMixSession)
	{
		Unsafe.SkipInit(out CComPtrNtv_003CIQuickMixManager_003E cComPtrNtv_003CIQuickMixManager_003E);
		*(int*)(&cComPtrNtv_003CIQuickMixManager_003E) = 0;
		HRESULT result;
		try
		{
			Unsafe.SkipInit(out CComPtrNtv_003CIQuickMixSession_003E cComPtrNtv_003CIQuickMixSession_003E);
			*(int*)(&cComPtrNtv_003CIQuickMixSession_003E) = 0;
			try
			{
				int num = global::_003CModule_003E.GetSingleton((_GUID)global::_003CModule_003E._GUID_d69e22ae_7e21_4959_be6e_14462eb96f64, (void**)(&cComPtrNtv_003CIQuickMixManager_003E));
				if (num >= 0)
				{
					fixed (int* ptr = &seedMediaIds[0])
					{
						try
						{
							int num2 = *(int*)(int)(*(uint*)(&cComPtrNtv_003CIQuickMixManager_003E)) + 16;
							num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, EQuickMixMode, int, int*, EMediaTypes, IQuickMixSession**, int>)(int)(*(uint*)num2))((IntPtr)(*(int*)(&cComPtrNtv_003CIQuickMixManager_003E)), eQuickMixMode, seedMediaIds.Length, ptr, eMediaType, (IQuickMixSession**)(&cComPtrNtv_003CIQuickMixSession_003E));
						}
						catch
						{
							//try-fault
							ptr = null;
							throw;
						}
					}
					if (num >= 0)
					{
						quickMixSession = new QuickMixSession((IQuickMixSession*)(int)(*(uint*)(&cComPtrNtv_003CIQuickMixSession_003E)));
					}
				}
				result = num;
			}
			catch
			{
				//try-fault
				global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<CComPtrNtv_003CIQuickMixSession_003E*, void>)(&global::_003CModule_003E.CComPtrNtv_003CIQuickMixSession_003E_002E_007Bdtor_007D), &cComPtrNtv_003CIQuickMixSession_003E);
				throw;
			}
			global::_003CModule_003E.CComPtrNtv_003CIQuickMixSession_003E_002ERelease(&cComPtrNtv_003CIQuickMixSession_003E);
		}
		catch
		{
			//try-fault
			global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<CComPtrNtv_003CIQuickMixManager_003E*, void>)(&global::_003CModule_003E.CComPtrNtv_003CIQuickMixManager_003E_002E_007Bdtor_007D), &cComPtrNtv_003CIQuickMixManager_003E);
			throw;
		}
		global::_003CModule_003E.CComPtrNtv_003CIQuickMixManager_003E_002ERelease(&cComPtrNtv_003CIQuickMixManager_003E);
		return result;
	}

	private QuickMix()
	{
	}

	private unsafe int Initialize()
	{
		Unsafe.SkipInit(out CComPtrNtv_003CIQuickMixManager_003E cComPtrNtv_003CIQuickMixManager_003E);
		*(int*)(&cComPtrNtv_003CIQuickMixManager_003E) = 0;
		int num2;
		try
		{
			QuickMixProgressHandler progressHandler = QuickMixProgressHandlerInternal;
			QuickMixCallbackProxy* ptr = (QuickMixCallbackProxy*)global::_003CModule_003E.@new(24u);
			QuickMixCallbackProxy* ptr2;
			try
			{
				ptr2 = ((ptr == null) ? null : global::_003CModule_003E.Microsoft_002EZune_002EQuickMix_002EQuickMixCallbackProxy_002E_007Bctor_007D(ptr, progressHandler));
			}
			catch
			{
				//try-fault
				global::_003CModule_003E.delete(ptr);
				throw;
			}
			int num = ((ptr2 == null) ? (-2147024882) : 0);
			num2 = num;
			if (num >= 0)
			{
				num2 = global::_003CModule_003E.GetSingleton((_GUID)global::_003CModule_003E._GUID_d69e22ae_7e21_4959_be6e_14462eb96f64, (void**)(&cComPtrNtv_003CIQuickMixManager_003E));
				if (num2 >= 0)
				{
					QuickMixCallbackProxy* ptr3 = (QuickMixCallbackProxy*)((ptr2 == null) ? null : ((byte*)ptr2 + 4));
					num2 = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, IQuickMixStatusCallback*, int>)(int)(*(uint*)(*(int*)(int)(*(uint*)(&cComPtrNtv_003CIQuickMixManager_003E)) + 36)))((IntPtr)(*(int*)(&cComPtrNtv_003CIQuickMixManager_003E)), (IQuickMixStatusCallback*)ptr3);
				}
			}
			if (ptr2 != null)
			{
				((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint>)(int)(*(uint*)(*(int*)ptr2 + 8)))((nint)ptr2);
			}
		}
		catch
		{
			//try-fault
			global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<CComPtrNtv_003CIQuickMixManager_003E*, void>)(&global::_003CModule_003E.CComPtrNtv_003CIQuickMixManager_003E_002E_007Bdtor_007D), &cComPtrNtv_003CIQuickMixManager_003E);
			throw;
		}
		global::_003CModule_003E.CComPtrNtv_003CIQuickMixManager_003E_002ERelease(&cComPtrNtv_003CIQuickMixManager_003E);
		return num2;
	}

	private void QuickMixProgressHandlerInternal(float progress, int secondsRemaining)
	{
		m_onProgressHandler?.Invoke(progress, secondsRemaining);
	}
}
