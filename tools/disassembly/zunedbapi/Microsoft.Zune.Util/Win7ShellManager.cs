using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;

namespace Microsoft.Zune.Util;

public class Win7ShellManager
{
	private WindowPositionKeyPressHandler _003Cbacking_store_003EOnWindowPositionKeyPress;

	private ThumbBarButtonPressHandler _003Cbacking_store_003EOnThumbBarButtonPress;

	private MonitorChangeHandler _003Cbacking_store_003EOnMonitorChange;

	private static Win7ShellManager sm_win7ShellManager = null;

	private static object sm_lock = new object();

	public static Win7ShellManager Instance
	{
		get
		{
			if (sm_win7ShellManager == null)
			{
				try
				{
					Monitor.Enter(sm_lock);
					if (sm_win7ShellManager == null)
					{
						Win7ShellManager win7ShellManager = new Win7ShellManager();
						Thread.MemoryBarrier();
						sm_win7ShellManager = win7ShellManager;
					}
				}
				finally
				{
					Monitor.Exit(sm_lock);
				}
			}
			return sm_win7ShellManager;
		}
	}

	[SpecialName]
	public event MonitorChangeHandler OnMonitorChange
	{
		[MethodImpl(MethodImplOptions.Synchronized)]
		add
		{
			_003Cbacking_store_003EOnMonitorChange = (MonitorChangeHandler)Delegate.Combine(_003Cbacking_store_003EOnMonitorChange, value);
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		remove
		{
			_003Cbacking_store_003EOnMonitorChange = (MonitorChangeHandler)Delegate.Remove(_003Cbacking_store_003EOnMonitorChange, value);
		}
	}

	[SpecialName]
	public event ThumbBarButtonPressHandler OnThumbBarButtonPress
	{
		[MethodImpl(MethodImplOptions.Synchronized)]
		add
		{
			_003Cbacking_store_003EOnThumbBarButtonPress = (ThumbBarButtonPressHandler)Delegate.Combine(_003Cbacking_store_003EOnThumbBarButtonPress, value);
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		remove
		{
			_003Cbacking_store_003EOnThumbBarButtonPress = (ThumbBarButtonPressHandler)Delegate.Remove(_003Cbacking_store_003EOnThumbBarButtonPress, value);
		}
	}

	[SpecialName]
	public event WindowPositionKeyPressHandler OnWindowPositionKeyPress
	{
		[MethodImpl(MethodImplOptions.Synchronized)]
		add
		{
			_003Cbacking_store_003EOnWindowPositionKeyPress = (WindowPositionKeyPressHandler)Delegate.Combine(_003Cbacking_store_003EOnWindowPositionKeyPress, value);
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		remove
		{
			_003Cbacking_store_003EOnWindowPositionKeyPress = (WindowPositionKeyPressHandler)Delegate.Remove(_003Cbacking_store_003EOnWindowPositionKeyPress, value);
		}
	}

	public unsafe int AddLocationToLibrary(EWin7LibraryKind libraryKind, [MarshalAs(UnmanagedType.U1)] bool defaultSaveFolder, string path)
	{
		Unsafe.SkipInit(out CComPtrNtv_003CIWin7ShellManager_003E cComPtrNtv_003CIWin7ShellManager_003E);
		*(int*)(&cComPtrNtv_003CIWin7ShellManager_003E) = 0;
		int num;
		try
		{
			num = global::_003CModule_003E.GetSingleton((_GUID)global::_003CModule_003E._GUID_a89c52eb_97a9_417b_9872_46c040f1b76f, (void**)(&cComPtrNtv_003CIWin7ShellManager_003E));
			if (num >= 0)
			{
				fixed (ushort* ptr = &Unsafe.As<char, ushort>(ref global::_003CModule_003E.PtrToStringChars(path)))
				{
					try
					{
						int num2 = *(int*)(int)(*(uint*)(&cComPtrNtv_003CIWin7ShellManager_003E)) + 48;
						num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, EWin7LibraryKind, int, ushort*, int>)(int)(*(uint*)num2))((IntPtr)(*(int*)(&cComPtrNtv_003CIWin7ShellManager_003E)), libraryKind, defaultSaveFolder ? 1 : 0, ptr);
					}
					catch
					{
						//try-fault
						ptr = null;
						throw;
					}
				}
			}
		}
		catch
		{
			//try-fault
			global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<CComPtrNtv_003CIWin7ShellManager_003E*, void>)(&global::_003CModule_003E.CComPtrNtv_003CIWin7ShellManager_003E_002E_007Bdtor_007D), &cComPtrNtv_003CIWin7ShellManager_003E);
			throw;
		}
		global::_003CModule_003E.CComPtrNtv_003CIWin7ShellManager_003E_002ERelease(&cComPtrNtv_003CIWin7ShellManager_003E);
		return num;
	}

	public unsafe int RemoveLocationFromLibrary(EWin7LibraryKind libraryKind, out bool defaultSaveFolder, string path)
	{
		Unsafe.SkipInit(out CComPtrNtv_003CIWin7ShellManager_003E cComPtrNtv_003CIWin7ShellManager_003E);
		*(int*)(&cComPtrNtv_003CIWin7ShellManager_003E) = 0;
		int num;
		try
		{
			num = global::_003CModule_003E.GetSingleton((_GUID)global::_003CModule_003E._GUID_a89c52eb_97a9_417b_9872_46c040f1b76f, (void**)(&cComPtrNtv_003CIWin7ShellManager_003E));
			int num2 = 0;
			int num4;
			if (num >= 0)
			{
				fixed (ushort* ptr = &Unsafe.As<char, ushort>(ref global::_003CModule_003E.PtrToStringChars(path)))
				{
					try
					{
						int num3 = *(int*)(int)(*(uint*)(&cComPtrNtv_003CIWin7ShellManager_003E)) + 52;
						num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, EWin7LibraryKind, int*, ushort*, int>)(int)(*(uint*)num3))((IntPtr)(*(int*)(&cComPtrNtv_003CIWin7ShellManager_003E)), libraryKind, &num2, ptr);
					}
					catch
					{
						//try-fault
						ptr = null;
						throw;
					}
				}
				if (num2 == 1)
				{
					num4 = 1;
					goto IL_0048;
				}
			}
			num4 = 0;
			goto IL_0048;
			IL_0048:
			defaultSaveFolder = (byte)num4 != 0;
		}
		catch
		{
			//try-fault
			global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<CComPtrNtv_003CIWin7ShellManager_003E*, void>)(&global::_003CModule_003E.CComPtrNtv_003CIWin7ShellManager_003E_002E_007Bdtor_007D), &cComPtrNtv_003CIWin7ShellManager_003E);
			throw;
		}
		global::_003CModule_003E.CComPtrNtv_003CIWin7ShellManager_003E_002ERelease(&cComPtrNtv_003CIWin7ShellManager_003E);
		return num;
	}

	public unsafe int BeginJumpListSession(out JumpListSession session)
	{
		IWin7ShellManager* ptr = null;
		IJumpList* ptr2 = null;
		int num = global::_003CModule_003E.GetSingleton((_GUID)global::_003CModule_003E._GUID_a89c52eb_97a9_417b_9872_46c040f1b76f, (void**)(&ptr));
		if (num >= 0)
		{
			num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, IJumpList**, int>)(int)(*(uint*)(*(int*)ptr + 12)))((nint)ptr, &ptr2);
			if (num >= 0)
			{
				session = new JumpListSession(ptr2);
			}
		}
		if (null != ptr2)
		{
			IJumpList* intPtr = ptr2;
			((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint>)(int)(*(uint*)(*(int*)intPtr + 8)))((nint)intPtr);
			ptr2 = null;
		}
		if (null != ptr)
		{
			IWin7ShellManager* intPtr2 = ptr;
			((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint>)(int)(*(uint*)(*(int*)intPtr2 + 8)))((nint)intPtr2);
		}
		return num;
	}

	public unsafe int BeginThumbBarSession(IntPtr hWnd, out ThumbBar thumbBar)
	{
		IWin7ShellManager* ptr = null;
		IThumbBar* ptr2 = null;
		int num = global::_003CModule_003E.GetSingleton((_GUID)global::_003CModule_003E._GUID_a89c52eb_97a9_417b_9872_46c040f1b76f, (void**)(&ptr));
		if (num >= 0)
		{
			int num2 = *(int*)ptr + 16;
			num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, HWND__*, IThumbBar**, int>)(int)(*(uint*)num2))((nint)ptr, (HWND__*)hWnd.ToPointer(), &ptr2);
			if (num >= 0)
			{
				thumbBar = new ThumbBar(ptr2);
			}
		}
		if (null != ptr2)
		{
			IThumbBar* intPtr = ptr2;
			((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint>)(int)(*(uint*)(*(int*)intPtr + 8)))((nint)intPtr);
			ptr2 = null;
		}
		if (null != ptr)
		{
			IWin7ShellManager* intPtr2 = ptr;
			((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint>)(int)(*(uint*)(*(int*)intPtr2 + 8)))((nint)intPtr2);
		}
		return num;
	}

	public unsafe int SubprocWindow(IntPtr hWnd)
	{
		IWin7ShellManager* ptr = null;
		int num = global::_003CModule_003E.GetSingleton((_GUID)global::_003CModule_003E._GUID_a89c52eb_97a9_417b_9872_46c040f1b76f, (void**)(&ptr));
		if (num >= 0)
		{
			int num2 = *(int*)ptr + 20;
			num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, HWND__*, int>)(int)(*(uint*)num2))((nint)ptr, (HWND__*)hWnd.ToPointer());
		}
		if (null != ptr)
		{
			IWin7ShellManager* intPtr = ptr;
			((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint>)(int)(*(uint*)(*(int*)intPtr + 8)))((nint)intPtr);
		}
		return num;
	}

	public unsafe int ShowLibraryDialog(EWin7LibraryKind libraryKind, IntPtr hWnd, string title, string helpText)
	{
		fixed (ushort* ptr = &Unsafe.As<char, ushort>(ref global::_003CModule_003E.PtrToStringChars(title)))
		{
			fixed (ushort* ptr2 = &Unsafe.As<char, ushort>(ref global::_003CModule_003E.PtrToStringChars(helpText)))
			{
				IWin7ShellManager* ptr3 = null;
				int num = global::_003CModule_003E.GetSingleton((_GUID)global::_003CModule_003E._GUID_a89c52eb_97a9_417b_9872_46c040f1b76f, (void**)(&ptr3));
				if (num >= 0)
				{
					int num2 = *(int*)ptr3 + 40;
					num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, EWin7LibraryKind, HWND__*, ushort*, ushort*, int>)(int)(*(uint*)num2))((nint)ptr3, libraryKind, (HWND__*)hWnd.ToPointer(), ptr, ptr2);
				}
				if (null != ptr3)
				{
					IWin7ShellManager* intPtr = ptr3;
					((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint>)(int)(*(uint*)(*(int*)intPtr + 8)))((nint)intPtr);
				}
				return num;
			}
		}
	}

	public unsafe int SyncLibraryFolders()
	{
		IWin7ShellManager* ptr = null;
		int num = global::_003CModule_003E.GetSingleton((_GUID)global::_003CModule_003E._GUID_a89c52eb_97a9_417b_9872_46c040f1b76f, (void**)(&ptr));
		if (num >= 0)
		{
			IWin7ShellManager* intPtr = ptr;
			num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, int>)(int)(*(uint*)(*(int*)intPtr + 44)))((nint)intPtr);
		}
		if (null != ptr)
		{
			IWin7ShellManager* intPtr2 = ptr;
			((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint>)(int)(*(uint*)(*(int*)intPtr2 + 8)))((nint)intPtr2);
		}
		return num;
	}

	public unsafe int CreatePodcastLibraryTemplate()
	{
		IWin7Libraries* ptr = null;
		int num = global::_003CModule_003E.GetSingleton((_GUID)global::_003CModule_003E._GUID_e24c5c6a_85a5_440e_93e1_bb51e32033ac, (void**)(&ptr));
		if (num >= 0)
		{
			IWin7Libraries* intPtr = ptr;
			num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, int>)(int)(*(uint*)(*(int*)intPtr + 28)))((nint)intPtr);
		}
		if (null != ptr)
		{
			IWin7Libraries* intPtr2 = ptr;
			((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint>)(int)(*(uint*)(*(int*)intPtr2 + 8)))((nint)intPtr2);
		}
		return num;
	}

	[SpecialName]
	protected void raise_OnWindowPositionKeyPress(WindowPositionKeys value0)
	{
		_003Cbacking_store_003EOnWindowPositionKeyPress?.Invoke(value0);
	}

	[SpecialName]
	protected void raise_OnThumbBarButtonPress(uint value0)
	{
		_003Cbacking_store_003EOnThumbBarButtonPress?.Invoke(value0);
	}

	[SpecialName]
	protected void raise_OnMonitorChange()
	{
		_003Cbacking_store_003EOnMonitorChange?.Invoke();
	}

	internal int WindowPositionKeyPressDetected(WindowPositionKeys key)
	{
		raise_OnWindowPositionKeyPress(key);
		return 0;
	}

	internal int ThumbBarButtonPressed(uint iUniqueID)
	{
		raise_OnThumbBarButtonPress(iUniqueID);
		return 0;
	}

	internal int MonitorChanged()
	{
		raise_OnMonitorChange();
		return 0;
	}

	private unsafe Win7ShellManager()
	{
		IWin7ShellManager* ptr = null;
		if (global::_003CModule_003E.GetSingleton((_GUID)global::_003CModule_003E._GUID_a89c52eb_97a9_417b_9872_46c040f1b76f, (void**)(&ptr)) >= 0)
		{
			Win7ShellManagerMediator* ptr2 = (Win7ShellManagerMediator*)global::_003CModule_003E.@new(12u);
			Win7ShellManagerMediator* ptr3;
			try
			{
				ptr3 = ((ptr2 == null) ? null : global::_003CModule_003E.Microsoft_002EZune_002EUtil_002EWin7ShellManagerMediator_002E_007Bctor_007D(ptr2, this));
			}
			catch
			{
				//try-fault
				global::_003CModule_003E.delete(ptr2);
				throw;
			}
			((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, IWin7ShellManagerMediator*, int>)(int)(*(uint*)(*(int*)ptr + 24)))((nint)ptr, (IWin7ShellManagerMediator*)ptr3);
		}
		if (null != ptr)
		{
			IWin7ShellManager* intPtr = ptr;
			((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint>)(int)(*(uint*)(*(int*)intPtr + 8)))((nint)intPtr);
		}
	}
}
