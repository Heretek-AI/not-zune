using System;
using System.Runtime.InteropServices;
using Microsoft.Iris;
using Microsoft.Zune.Util;
using ZuneUI;

namespace MicrosoftZuneLibrary;

public class AppInitializationSequencer
{
	public delegate void Phase3CompleteCallback(int hr);

	private CorePhase2ReadyCallback m_GcCorePhase2ReadyCallback;

	public AppInitializationSequencer(CorePhase2ReadyCallback corePhase2ReadyCallback)
	{
		m_GcCorePhase2ReadyCallback = corePhase2ReadyCallback;
	}

	[return: MarshalAs(UnmanagedType.U1)]
	public unsafe bool UIReady()
	{
		AsyncCallbackWrapper* ptr = (AsyncCallbackWrapper*)global::_003CModule_003E.@new(12u);
		AsyncCallbackWrapper* ptr2;
		try
		{
			ptr2 = ((ptr == null) ? null : global::_003CModule_003E.Microsoft_002EZune_002EUtil_002EAsyncCallbackWrapper_002E_007Bctor_007D(ptr, CorePhase2Ready));
		}
		catch
		{
			//try-fault
			global::_003CModule_003E.delete(ptr);
			throw;
		}
		int num2;
		if (ptr2 != null)
		{
			int num = global::_003CModule_003E.ZuneLibraryExports_002EPhase3Initialization((IAsyncCallback*)ptr2);
			((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint>)(int)(*(uint*)(*(int*)ptr2 + 8)))((nint)ptr2);
			if (num >= 0)
			{
				num2 = 1;
				goto IL_0049;
			}
		}
		num2 = 0;
		goto IL_0049;
		IL_0049:
		return (byte)num2 != 0;
	}

	public void CorePhase2Ready(HRESULT hr)
	{
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Expected O, but got Unknown
		object[] array = new object[2] { hr.hr, null };
		byte b = ((hr.hr >= 0) ? ((byte)1) : ((byte)0));
		array[1] = b != 0;
		Application.DeferredInvoke(new DeferredInvokeHandler(CorePhase2ReadyMarshalled), (object)array, (DeferredInvokePriority)0);
	}

	private void CorePhase2ReadyMarshalled(object args)
	{
		object[] array = (object[])args;
		m_GcCorePhase2ReadyCallback((int)array[0], (bool)array[1]);
	}
}
