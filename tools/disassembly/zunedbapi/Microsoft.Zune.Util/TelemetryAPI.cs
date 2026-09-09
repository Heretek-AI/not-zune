using System;
using System.Collections;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Microsoft.Zune.Util;

public class TelemetryAPI
{
	public unsafe static void SendDatapoint(string command, IDictionary dictionary)
	{
		fixed (ushort* ptr = &Unsafe.As<char, ushort>(ref global::_003CModule_003E.PtrToStringChars(command)))
		{
			int count = dictionary.Keys.Count;
			tagSAFEARRAY* ptr2 = global::_003CModule_003E.SafeArrayCreateVector(12, 0, (uint)count);
			tagSAFEARRAY* ptr3 = global::_003CModule_003E.SafeArrayCreateVector(12, 0, (uint)count);
			int num = 0;
			Unsafe.SkipInit(out tagVARIANT tagVARIANT2);
			Unsafe.SkipInit(out tagVARIANT tagVARIANT3);
			foreach (DictionaryEntry item in dictionary)
			{
				int num2 = 0;
				if ((object)((DictionaryEntry)item).Key.GetType() == typeof(string))
				{
					IntPtr pDstNativeVariant = (IntPtr)(&tagVARIANT2);
					Marshal.GetNativeVariantForObject(((DictionaryEntry)item).Key, pDstNativeVariant);
					if ((object)((DictionaryEntry)item).Value.GetType() == typeof(Guid))
					{
						string? obj = ((DictionaryEntry)item).Value.ToString();
						IntPtr pDstNativeVariant2 = (IntPtr)(&tagVARIANT3);
						Marshal.GetNativeVariantForObject((object?)obj, pDstNativeVariant2);
					}
					else
					{
						if ((object)((DictionaryEntry)item).Value.GetType() != typeof(string) && (object)((DictionaryEntry)item).Value.GetType() != typeof(int) && (object)((DictionaryEntry)item).Value.GetType() != typeof(long))
						{
							num2 = -2147024809;
							continue;
						}
						IntPtr pDstNativeVariant3 = (IntPtr)(&tagVARIANT3);
						Marshal.GetNativeVariantForObject(((DictionaryEntry)item).Value, pDstNativeVariant3);
					}
					global::_003CModule_003E.SafeArrayPutElement(ptr2, &num, &tagVARIANT2);
					global::_003CModule_003E.SafeArrayPutElement(ptr3, &num, &tagVARIANT3);
					num++;
				}
				else
				{
					num2 = -2147024809;
				}
			}
			Unsafe.SkipInit(out CComPtrNtv_003CITelemetryManager_003E cComPtrNtv_003CITelemetryManager_003E);
			*(int*)(&cComPtrNtv_003CITelemetryManager_003E) = 0;
			try
			{
				if (global::_003CModule_003E.GetSingleton((_GUID)global::_003CModule_003E._GUID_ab28333b_a55c_4312_a7a3_2dd60d4a7154, (void**)(&cComPtrNtv_003CITelemetryManager_003E)) >= 0)
				{
					int num3 = *(int*)(int)(*(uint*)(&cComPtrNtv_003CITelemetryManager_003E)) + 24;
					((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, ushort*, tagSAFEARRAY*, tagSAFEARRAY*, int>)(int)(*(uint*)num3))((IntPtr)(*(int*)(&cComPtrNtv_003CITelemetryManager_003E)), ptr, ptr2, ptr3);
				}
				if (ptr2 != null)
				{
					global::_003CModule_003E.SafeArrayDestroy(ptr2);
				}
				if (ptr3 != null)
				{
					global::_003CModule_003E.SafeArrayDestroy(ptr3);
				}
			}
			catch
			{
				//try-fault
				global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<CComPtrNtv_003CITelemetryManager_003E*, void>)(&global::_003CModule_003E.CComPtrNtv_003CITelemetryManager_003E_002E_007Bdtor_007D), &cComPtrNtv_003CITelemetryManager_003E);
				throw;
			}
			global::_003CModule_003E.CComPtrNtv_003CITelemetryManager_003E_002ERelease(&cComPtrNtv_003CITelemetryManager_003E);
		}
	}

	public unsafe static void AddToSessionEvent(ETelemetryEvent evt, string key, int value)
	{
		fixed (ushort* ptr = &Unsafe.As<char, ushort>(ref global::_003CModule_003E.PtrToStringChars(key)))
		{
			Unsafe.SkipInit(out CComPtrNtv_003CITelemetryManager_003E cComPtrNtv_003CITelemetryManager_003E);
			*(int*)(&cComPtrNtv_003CITelemetryManager_003E) = 0;
			try
			{
				if (global::_003CModule_003E.GetSingleton((_GUID)global::_003CModule_003E._GUID_ab28333b_a55c_4312_a7a3_2dd60d4a7154, (void**)(&cComPtrNtv_003CITelemetryManager_003E)) >= 0)
				{
					int num = *(int*)(int)(*(uint*)(&cComPtrNtv_003CITelemetryManager_003E)) + 32;
					((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, ETelemetryEvent, ushort*, int, int>)(int)(*(uint*)num))((IntPtr)(*(int*)(&cComPtrNtv_003CITelemetryManager_003E)), evt, ptr, value);
				}
			}
			catch
			{
				//try-fault
				global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<CComPtrNtv_003CITelemetryManager_003E*, void>)(&global::_003CModule_003E.CComPtrNtv_003CITelemetryManager_003E_002E_007Bdtor_007D), &cComPtrNtv_003CITelemetryManager_003E);
				throw;
			}
			global::_003CModule_003E.CComPtrNtv_003CITelemetryManager_003E_002ERelease(&cComPtrNtv_003CITelemetryManager_003E);
		}
	}

	public unsafe static void SendEvent(ETelemetryEvent eEvent, string eventParameter)
	{
		fixed (ushort* ptr = &Unsafe.As<char, ushort>(ref global::_003CModule_003E.PtrToStringChars(eventParameter)))
		{
			Unsafe.SkipInit(out CComPtrNtv_003CITelemetryManager_003E cComPtrNtv_003CITelemetryManager_003E);
			*(int*)(&cComPtrNtv_003CITelemetryManager_003E) = 0;
			try
			{
				if (global::_003CModule_003E.GetSingleton((_GUID)global::_003CModule_003E._GUID_ab28333b_a55c_4312_a7a3_2dd60d4a7154, (void**)(&cComPtrNtv_003CITelemetryManager_003E)) >= 0)
				{
					int num = *(int*)(int)(*(uint*)(&cComPtrNtv_003CITelemetryManager_003E)) + 28;
					((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, ETelemetryEvent, ushort*, int>)(int)(*(uint*)num))((IntPtr)(*(int*)(&cComPtrNtv_003CITelemetryManager_003E)), eEvent, ptr);
				}
			}
			catch
			{
				//try-fault
				global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<CComPtrNtv_003CITelemetryManager_003E*, void>)(&global::_003CModule_003E.CComPtrNtv_003CITelemetryManager_003E_002E_007Bdtor_007D), &cComPtrNtv_003CITelemetryManager_003E);
				throw;
			}
			global::_003CModule_003E.CComPtrNtv_003CITelemetryManager_003E_002ERelease(&cComPtrNtv_003CITelemetryManager_003E);
		}
	}
}
