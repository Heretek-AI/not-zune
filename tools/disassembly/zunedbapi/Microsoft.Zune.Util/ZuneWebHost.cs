using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;

namespace Microsoft.Zune.Util;

public class ZuneWebHost
{
	private static object s_lock = new object();

	private static ZuneWebHost s_zuneWebHost = null;

	private unsafe ZuneWebHostEventSink* m_zuneWebHostEventSink;

	private NavigationCompleteHandler m_navCompleteHandler;

	private NavigationErrorHandler m_navErrorHandler;

	public unsafe static ZuneWebHost Instance
	{
		get
		{
			if (global::_003CModule_003E.WPP_GLOBAL_Control != Unsafe.AsPointer(ref global::_003CModule_003E.WPP_GLOBAL_Control) && (((int*)global::_003CModule_003E.WPP_GLOBAL_Control)[7] & 0x8000) != 0 && (uint)((byte*)global::_003CModule_003E.WPP_GLOBAL_Control)[25] >= 5u)
			{
				global::_003CModule_003E.WPP_SF_(((ulong*)global::_003CModule_003E.WPP_GLOBAL_Control)[2], 10, (_GUID*)Unsafe.AsPointer(ref global::_003CModule_003E._003FA0x2c637d09_002EWPP_ZuneWebHostInterop_cpp_Traceguids));
			}
			if (s_zuneWebHost == null)
			{
				try
				{
					Monitor.Enter(s_lock);
					if (s_zuneWebHost == null)
					{
						ZuneWebHost zuneWebHost = new ZuneWebHost();
						Thread.MemoryBarrier();
						s_zuneWebHost = zuneWebHost;
					}
				}
				finally
				{
					Monitor.Exit(s_lock);
				}
			}
			if (global::_003CModule_003E.WPP_GLOBAL_Control != Unsafe.AsPointer(ref global::_003CModule_003E.WPP_GLOBAL_Control) && (((int*)global::_003CModule_003E.WPP_GLOBAL_Control)[7] & 0x8000) != 0 && (uint)((byte*)global::_003CModule_003E.WPP_GLOBAL_Control)[25] >= 5u)
			{
				global::_003CModule_003E.WPP_SF_(((ulong*)global::_003CModule_003E.WPP_GLOBAL_Control)[2], 11, (_GUID*)Unsafe.AsPointer(ref global::_003CModule_003E._003FA0x2c637d09_002EWPP_ZuneWebHostInterop_cpp_Traceguids));
			}
			return s_zuneWebHost;
		}
	}

	public unsafe long Initialize(string navUrl, long hWndHost, int width, int height)
	{
		if (global::_003CModule_003E.WPP_GLOBAL_Control != Unsafe.AsPointer(ref global::_003CModule_003E.WPP_GLOBAL_Control) && (((int*)global::_003CModule_003E.WPP_GLOBAL_Control)[7] & 0x8000) != 0 && (uint)((byte*)global::_003CModule_003E.WPP_GLOBAL_Control)[25] >= 5u)
		{
			global::_003CModule_003E.WPP_SF_(((ulong*)global::_003CModule_003E.WPP_GLOBAL_Control)[2], 12, (_GUID*)Unsafe.AsPointer(ref global::_003CModule_003E._003FA0x2c637d09_002EWPP_ZuneWebHostInterop_cpp_Traceguids));
		}
		HWND__* ptr = null;
		Unsafe.SkipInit(out CComPtrNtv_003CIZuneWebHost_003E cComPtrNtv_003CIZuneWebHost_003E);
		*(int*)(&cComPtrNtv_003CIZuneWebHost_003E) = 0;
		long result;
		try
		{
			global::_003CModule_003E.GetSingleton((_GUID)global::_003CModule_003E._GUID_51005f8f_675e_45e1_ae94_8edef996a02e, (void**)(&cComPtrNtv_003CIZuneWebHost_003E));
			fixed (ushort* ptr2 = &Unsafe.As<char, ushort>(ref global::_003CModule_003E.PtrToStringChars(navUrl)))
			{
				ZuneWebHostEventSink* ptr3 = (ZuneWebHostEventSink*)global::_003CModule_003E.@new(12u);
				ZuneWebHostEventSink* zuneWebHostEventSink;
				try
				{
					zuneWebHostEventSink = ((ptr3 == null) ? null : global::_003CModule_003E.Microsoft_002EZune_002EUtil_002EZuneWebHostEventSink_002E_007Bctor_007D(ptr3, this, (IZuneWebHost*)(int)(*(uint*)(&cComPtrNtv_003CIZuneWebHost_003E))));
				}
				catch
				{
					//try-fault
					global::_003CModule_003E.delete(ptr3);
					throw;
				}
				m_zuneWebHostEventSink = zuneWebHostEventSink;
				int num = *(int*)(int)(*(uint*)(&cComPtrNtv_003CIZuneWebHost_003E)) + 12;
				int num2 = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, ushort*, HWND__*, int, int, HWND__**, int>)(int)(*(uint*)num))((IntPtr)(*(int*)(&cComPtrNtv_003CIZuneWebHost_003E)), ptr2, (HWND__*)(int)hWndHost, width, height, &ptr);
				if (global::_003CModule_003E.WPP_GLOBAL_Control != Unsafe.AsPointer(ref global::_003CModule_003E.WPP_GLOBAL_Control) && (((int*)global::_003CModule_003E.WPP_GLOBAL_Control)[7] & 0x8000) != 0 && (uint)((byte*)global::_003CModule_003E.WPP_GLOBAL_Control)[25] >= 5u)
				{
					sbyte* a = (sbyte*)((num2 >= 0) ? Unsafe.AsPointer(ref global::_003CModule_003E._003F_003F_C_0040_00CNPNBAHC_0040_003F_0024AA_0040) : Unsafe.AsPointer(ref global::_003CModule_003E._003F_003F_C_0040_07PPOLEBIF_0040_003F5ERROR_003F3_003F_0024AA_0040));
					global::_003CModule_003E.WPP_SF_sD(((ulong*)global::_003CModule_003E.WPP_GLOBAL_Control)[2], 13, (_GUID*)Unsafe.AsPointer(ref global::_003CModule_003E._003FA0x2c637d09_002EWPP_ZuneWebHostInterop_cpp_Traceguids), a, (uint)num2);
				}
				result = (nint)ptr;
			}
		}
		catch
		{
			//try-fault
			global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<CComPtrNtv_003CIZuneWebHost_003E*, void>)(&global::_003CModule_003E.CComPtrNtv_003CIZuneWebHost_003E_002E_007Bdtor_007D), &cComPtrNtv_003CIZuneWebHost_003E);
			throw;
		}
		global::_003CModule_003E.CComPtrNtv_003CIZuneWebHost_003E_002ERelease(&cComPtrNtv_003CIZuneWebHost_003E);
		return result;
	}

	[return: MarshalAs(UnmanagedType.U1)]
	public unsafe bool SetSize(long hWndHost, int width, int height)
	{
		bool result = false;
		if (global::_003CModule_003E.WPP_GLOBAL_Control != Unsafe.AsPointer(ref global::_003CModule_003E.WPP_GLOBAL_Control) && (((int*)global::_003CModule_003E.WPP_GLOBAL_Control)[7] & 0x8000) != 0 && (uint)((byte*)global::_003CModule_003E.WPP_GLOBAL_Control)[25] >= 5u)
		{
			global::_003CModule_003E.WPP_SF_(((ulong*)global::_003CModule_003E.WPP_GLOBAL_Control)[2], 14, (_GUID*)Unsafe.AsPointer(ref global::_003CModule_003E._003FA0x2c637d09_002EWPP_ZuneWebHostInterop_cpp_Traceguids));
		}
		if (hWndHost != 0)
		{
			HWND__* window = global::_003CModule_003E.GetWindow((HWND__*)(int)hWndHost, 5u);
			if (window != null)
			{
				global::_003CModule_003E.MoveWindow(window, 0, 0, width, height, 0);
				result = true;
			}
		}
		if (global::_003CModule_003E.WPP_GLOBAL_Control != Unsafe.AsPointer(ref global::_003CModule_003E.WPP_GLOBAL_Control) && (((int*)global::_003CModule_003E.WPP_GLOBAL_Control)[7] & 0x8000) != 0 && (uint)((byte*)global::_003CModule_003E.WPP_GLOBAL_Control)[25] >= 5u)
		{
			global::_003CModule_003E.WPP_SF_(((ulong*)global::_003CModule_003E.WPP_GLOBAL_Control)[2], 15, (_GUID*)Unsafe.AsPointer(ref global::_003CModule_003E._003FA0x2c637d09_002EWPP_ZuneWebHostInterop_cpp_Traceguids));
		}
		return result;
	}

	public unsafe void SetNavigationCompleteHandler(NavigationCompleteHandler navigationCompleteHandler)
	{
		if (global::_003CModule_003E.WPP_GLOBAL_Control != Unsafe.AsPointer(ref global::_003CModule_003E.WPP_GLOBAL_Control) && (((int*)global::_003CModule_003E.WPP_GLOBAL_Control)[7] & 0x8000) != 0 && (uint)((byte*)global::_003CModule_003E.WPP_GLOBAL_Control)[25] >= 5u)
		{
			global::_003CModule_003E.WPP_SF_(((ulong*)global::_003CModule_003E.WPP_GLOBAL_Control)[2], 16, (_GUID*)Unsafe.AsPointer(ref global::_003CModule_003E._003FA0x2c637d09_002EWPP_ZuneWebHostInterop_cpp_Traceguids));
		}
		m_navCompleteHandler = navigationCompleteHandler;
	}

	public unsafe void SetNavigationErrorHandler(NavigationErrorHandler navigationErrorHandler)
	{
		if (global::_003CModule_003E.WPP_GLOBAL_Control != Unsafe.AsPointer(ref global::_003CModule_003E.WPP_GLOBAL_Control) && (((int*)global::_003CModule_003E.WPP_GLOBAL_Control)[7] & 0x8000) != 0 && (uint)((byte*)global::_003CModule_003E.WPP_GLOBAL_Control)[25] >= 5u)
		{
			global::_003CModule_003E.WPP_SF_(((ulong*)global::_003CModule_003E.WPP_GLOBAL_Control)[2], 17, (_GUID*)Unsafe.AsPointer(ref global::_003CModule_003E._003FA0x2c637d09_002EWPP_ZuneWebHostInterop_cpp_Traceguids));
		}
		m_navErrorHandler = navigationErrorHandler;
	}

	public unsafe void OnNavigationComplete(string data)
	{
		if (global::_003CModule_003E.WPP_GLOBAL_Control != Unsafe.AsPointer(ref global::_003CModule_003E.WPP_GLOBAL_Control) && (((int*)global::_003CModule_003E.WPP_GLOBAL_Control)[7] & 0x8000) != 0 && (uint)((byte*)global::_003CModule_003E.WPP_GLOBAL_Control)[25] >= 5u)
		{
			global::_003CModule_003E.WPP_SF_(((ulong*)global::_003CModule_003E.WPP_GLOBAL_Control)[2], 18, (_GUID*)Unsafe.AsPointer(ref global::_003CModule_003E._003FA0x2c637d09_002EWPP_ZuneWebHostInterop_cpp_Traceguids));
		}
		if (m_navCompleteHandler != null)
		{
			m_navCompleteHandler(data);
		}
		if (global::_003CModule_003E.WPP_GLOBAL_Control != Unsafe.AsPointer(ref global::_003CModule_003E.WPP_GLOBAL_Control) && (((int*)global::_003CModule_003E.WPP_GLOBAL_Control)[7] & 0x8000) != 0 && (uint)((byte*)global::_003CModule_003E.WPP_GLOBAL_Control)[25] >= 5u)
		{
			global::_003CModule_003E.WPP_SF_(((ulong*)global::_003CModule_003E.WPP_GLOBAL_Control)[2], 19, (_GUID*)Unsafe.AsPointer(ref global::_003CModule_003E._003FA0x2c637d09_002EWPP_ZuneWebHostInterop_cpp_Traceguids));
		}
	}

	public unsafe void OnNavigationError(string navUrl, int errorCode)
	{
		if (global::_003CModule_003E.WPP_GLOBAL_Control != Unsafe.AsPointer(ref global::_003CModule_003E.WPP_GLOBAL_Control) && (((int*)global::_003CModule_003E.WPP_GLOBAL_Control)[7] & 0x8000) != 0 && (uint)((byte*)global::_003CModule_003E.WPP_GLOBAL_Control)[25] >= 5u)
		{
			global::_003CModule_003E.WPP_SF_(((ulong*)global::_003CModule_003E.WPP_GLOBAL_Control)[2], 20, (_GUID*)Unsafe.AsPointer(ref global::_003CModule_003E._003FA0x2c637d09_002EWPP_ZuneWebHostInterop_cpp_Traceguids));
		}
		if (m_navErrorHandler != null)
		{
			m_navErrorHandler(navUrl, errorCode);
		}
		if (global::_003CModule_003E.WPP_GLOBAL_Control != Unsafe.AsPointer(ref global::_003CModule_003E.WPP_GLOBAL_Control) && (((int*)global::_003CModule_003E.WPP_GLOBAL_Control)[7] & 0x8000) != 0 && (uint)((byte*)global::_003CModule_003E.WPP_GLOBAL_Control)[25] >= 5u)
		{
			global::_003CModule_003E.WPP_SF_(((ulong*)global::_003CModule_003E.WPP_GLOBAL_Control)[2], 21, (_GUID*)Unsafe.AsPointer(ref global::_003CModule_003E._003FA0x2c637d09_002EWPP_ZuneWebHostInterop_cpp_Traceguids));
		}
	}
}
