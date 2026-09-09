using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Microsoft.Zune.Util;

public class FeatureEnablement
{
	[return: MarshalAs(UnmanagedType.U1)]
	public unsafe static bool IsFeatureEnabled(Features eFeature)
	{
		IFeatureEnablementManager* ptr = null;
		bool result = false;
		if (global::_003CModule_003E.GetSingleton((_GUID)global::_003CModule_003E._GUID_9581b41a_b5cf_4ebf_9d1a_975477e081ca, (void**)(&ptr)) >= 0)
		{
			((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, EFeatures, bool*, int>)(int)(*(uint*)(*(int*)ptr + 12)))((nint)ptr, (EFeatures)eFeature, &result);
		}
		if (null != ptr)
		{
			IFeatureEnablementManager* intPtr = ptr;
			((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint>)(int)(*(uint*)(*(int*)intPtr + 8)))((nint)intPtr);
		}
		return result;
	}

	public unsafe static void ForceFeatureOn(Features eFeature)
	{
		IFeatureEnablementManager* ptr = null;
		if (global::_003CModule_003E.GetSingleton((_GUID)global::_003CModule_003E._GUID_9581b41a_b5cf_4ebf_9d1a_975477e081ca, (void**)(&ptr)) >= 0)
		{
			((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, EFeatures, int>)(int)(*(uint*)(*(int*)ptr + 16)))((nint)ptr, (EFeatures)eFeature);
		}
		if (null != ptr)
		{
			IFeatureEnablementManager* intPtr = ptr;
			((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint>)(int)(*(uint*)(*(int*)intPtr + 8)))((nint)intPtr);
		}
	}

	public unsafe static string GetRegion()
	{
		IFeatureEnablementManager* ptr = null;
		string result = null;
		ushort* ptr2 = null;
		if (global::_003CModule_003E.GetSingleton((_GUID)global::_003CModule_003E._GUID_9581b41a_b5cf_4ebf_9d1a_975477e081ca, (void**)(&ptr)) >= 0 && ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, ushort**, int>)(int)(*(uint*)(*(int*)ptr + 20)))((nint)ptr, &ptr2) >= 0)
		{
			result = new string((char*)ptr2);
			global::_003CModule_003E.SysFreeString(ptr2);
		}
		if (null != ptr)
		{
			IFeatureEnablementManager* intPtr = ptr;
			((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint>)(int)(*(uint*)(*(int*)intPtr + 8)))((nint)intPtr);
		}
		return result;
	}

	public unsafe static uint GetGeoId()
	{
		IFeatureEnablementManager* ptr = null;
		uint result = uint.MaxValue;
		uint num = uint.MaxValue;
		if (global::_003CModule_003E.GetSingleton((_GUID)global::_003CModule_003E._GUID_9581b41a_b5cf_4ebf_9d1a_975477e081ca, (void**)(&ptr)) >= 0 && ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint*, int>)(int)(*(uint*)(*(int*)ptr + 28)))((nint)ptr, &num) >= 0)
		{
			result = num;
		}
		if (null != ptr)
		{
			IFeatureEnablementManager* intPtr = ptr;
			((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint>)(int)(*(uint*)(*(int*)intPtr + 8)))((nint)intPtr);
		}
		return result;
	}

	public unsafe static uint GetInvalidGeoId()
	{
		IFeatureEnablementManager* ptr = null;
		int result = -1;
		if (global::_003CModule_003E.GetSingleton((_GUID)global::_003CModule_003E._GUID_9581b41a_b5cf_4ebf_9d1a_975477e081ca, (void**)(&ptr)) >= 0)
		{
			IFeatureEnablementManager* intPtr = ptr;
			result = (int)((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint>)(int)(*(uint*)(*(int*)intPtr + 32)))((nint)intPtr);
		}
		if (null != ptr)
		{
			IFeatureEnablementManager* intPtr2 = ptr;
			((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint>)(int)(*(uint*)(*(int*)intPtr2 + 8)))((nint)intPtr2);
		}
		return (uint)result;
	}

	[return: MarshalAs(UnmanagedType.U1)]
	public unsafe static bool HasValidRegionAndLanguage()
	{
		IFeatureEnablementManager* ptr = null;
		bool result = false;
		if (global::_003CModule_003E.GetSingleton((_GUID)global::_003CModule_003E._GUID_9581b41a_b5cf_4ebf_9d1a_975477e081ca, (void**)(&ptr)) >= 0)
		{
			IFeatureEnablementManager* intPtr = ptr;
			bool flag = ((((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, int>)(int)(*(uint*)(*(int*)intPtr + 36)))((nint)intPtr) != 0) ? true : false);
			result = flag;
		}
		if (null != ptr)
		{
			IFeatureEnablementManager* intPtr2 = ptr;
			((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint>)(int)(*(uint*)(*(int*)intPtr2 + 8)))((nint)intPtr2);
		}
		return result;
	}

	public unsafe static string GetMarketplaceCulture()
	{
		Unsafe.SkipInit(out CComPtrNtv_003CIService_003E cComPtrNtv_003CIService_003E);
		*(int*)(&cComPtrNtv_003CIService_003E) = 0;
		string result;
		try
		{
			int singleton = global::_003CModule_003E.GetSingleton((_GUID)global::_003CModule_003E._GUID_bb2d1edd_1bd5_4be1_8d38_36d4f0849911, (void**)(&cComPtrNtv_003CIService_003E));
			result = null;
			if (singleton >= 0)
			{
				Unsafe.SkipInit(out WBSTRString wBSTRString);
				global::_003CModule_003E.WBSTRString_002E_007Bctor_007D(&wBSTRString);
				try
				{
					Unsafe.SkipInit(out CComPtrNtv_003CITunerConfig_003E cComPtrNtv_003CITunerConfig_003E);
					*(int*)(&cComPtrNtv_003CITunerConfig_003E) = 0;
					try
					{
						if (((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, int>)(int)(*(uint*)(*(int*)(int)(*(uint*)(&cComPtrNtv_003CIService_003E)) + 84)))((IntPtr)(*(int*)(&cComPtrNtv_003CIService_003E))) != 0 && ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, ITunerConfig**, int>)(int)(*(uint*)(*(int*)(int)(*(uint*)(&cComPtrNtv_003CIService_003E)) + 88)))((IntPtr)(*(int*)(&cComPtrNtv_003CIService_003E)), (ITunerConfig**)(&cComPtrNtv_003CITunerConfig_003E)) >= 0)
						{
							global::_003CModule_003E.WString_002EAttachBSTR((WString*)(&wBSTRString), ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, ushort*>)(int)(*(uint*)(*(int*)(int)(*(uint*)(&cComPtrNtv_003CITunerConfig_003E)) + 36)))((IntPtr)(*(int*)(&cComPtrNtv_003CITunerConfig_003E))));
						}
						result = new string((char*)(int)(*(uint*)(&wBSTRString)));
					}
					catch
					{
						//try-fault
						global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<CComPtrNtv_003CITunerConfig_003E*, void>)(&global::_003CModule_003E.CComPtrNtv_003CITunerConfig_003E_002E_007Bdtor_007D), &cComPtrNtv_003CITunerConfig_003E);
						throw;
					}
					global::_003CModule_003E.CComPtrNtv_003CITunerConfig_003E_002ERelease(&cComPtrNtv_003CITunerConfig_003E);
				}
				catch
				{
					//try-fault
					global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<WBSTRString*, void>)(&global::_003CModule_003E.WBSTRString_002E_007Bdtor_007D), &wBSTRString);
					throw;
				}
				global::_003CModule_003E.WString_002E_007Bdtor_007D((WString*)(&wBSTRString));
			}
		}
		catch
		{
			//try-fault
			global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<CComPtrNtv_003CIService_003E*, void>)(&global::_003CModule_003E.CComPtrNtv_003CIService_003E_002E_007Bdtor_007D), &cComPtrNtv_003CIService_003E);
			throw;
		}
		global::_003CModule_003E.CComPtrNtv_003CIService_003E_002ERelease(&cComPtrNtv_003CIService_003E);
		return result;
	}

	public unsafe static string GetLynxCulture()
	{
		Unsafe.SkipInit(out CComPtrNtv_003CIService_003E cComPtrNtv_003CIService_003E);
		*(int*)(&cComPtrNtv_003CIService_003E) = 0;
		string result;
		try
		{
			if (global::_003CModule_003E.GetSingleton((_GUID)global::_003CModule_003E._GUID_bb2d1edd_1bd5_4be1_8d38_36d4f0849911, (void**)(&cComPtrNtv_003CIService_003E)) >= 0)
			{
				Unsafe.SkipInit(out WBSTRString wBSTRString);
				global::_003CModule_003E.WBSTRString_002E_007Bctor_007D(&wBSTRString);
				try
				{
					Unsafe.SkipInit(out CComPtrNtv_003CITunerConfig_003E cComPtrNtv_003CITunerConfig_003E);
					*(int*)(&cComPtrNtv_003CITunerConfig_003E) = 0;
					try
					{
						if (((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, int>)(int)(*(uint*)(*(int*)(int)(*(uint*)(&cComPtrNtv_003CIService_003E)) + 84)))((IntPtr)(*(int*)(&cComPtrNtv_003CIService_003E))) != 0 && ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, ITunerConfig**, int>)(int)(*(uint*)(*(int*)(int)(*(uint*)(&cComPtrNtv_003CIService_003E)) + 88)))((IntPtr)(*(int*)(&cComPtrNtv_003CIService_003E)), (ITunerConfig**)(&cComPtrNtv_003CITunerConfig_003E)) >= 0)
						{
							global::_003CModule_003E.WString_002EAttachBSTR((WString*)(&wBSTRString), ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, ushort*>)(int)(*(uint*)(*(int*)(int)(*(uint*)(&cComPtrNtv_003CITunerConfig_003E)) + 40)))((IntPtr)(*(int*)(&cComPtrNtv_003CITunerConfig_003E))));
						}
						result = new string((char*)(int)(*(uint*)(&wBSTRString)));
					}
					catch
					{
						//try-fault
						global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<CComPtrNtv_003CITunerConfig_003E*, void>)(&global::_003CModule_003E.CComPtrNtv_003CITunerConfig_003E_002E_007Bdtor_007D), &cComPtrNtv_003CITunerConfig_003E);
						throw;
					}
					global::_003CModule_003E.CComPtrNtv_003CITunerConfig_003E_002ERelease(&cComPtrNtv_003CITunerConfig_003E);
				}
				catch
				{
					//try-fault
					global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<WBSTRString*, void>)(&global::_003CModule_003E.WBSTRString_002E_007Bdtor_007D), &wBSTRString);
					throw;
				}
				global::_003CModule_003E.WString_002E_007Bdtor_007D((WString*)(&wBSTRString));
				goto IL_00a9;
			}
		}
		catch
		{
			//try-fault
			global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<CComPtrNtv_003CIService_003E*, void>)(&global::_003CModule_003E.CComPtrNtv_003CIService_003E_002E_007Bdtor_007D), &cComPtrNtv_003CIService_003E);
			throw;
		}
		string result2;
		try
		{
			result2 = null;
		}
		catch
		{
			//try-fault
			global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<CComPtrNtv_003CIService_003E*, void>)(&global::_003CModule_003E.CComPtrNtv_003CIService_003E_002E_007Bdtor_007D), &cComPtrNtv_003CIService_003E);
			throw;
		}
		global::_003CModule_003E.CComPtrNtv_003CIService_003E_002ERelease(&cComPtrNtv_003CIService_003E);
		return result2;
		IL_00a9:
		global::_003CModule_003E.CComPtrNtv_003CIService_003E_002ERelease(&cComPtrNtv_003CIService_003E);
		return result;
	}

	public unsafe static string GetTaxString()
	{
		Unsafe.SkipInit(out CComPtrNtv_003CIService_003E cComPtrNtv_003CIService_003E);
		*(int*)(&cComPtrNtv_003CIService_003E) = 0;
		string result;
		try
		{
			int singleton = global::_003CModule_003E.GetSingleton((_GUID)global::_003CModule_003E._GUID_bb2d1edd_1bd5_4be1_8d38_36d4f0849911, (void**)(&cComPtrNtv_003CIService_003E));
			result = null;
			if (singleton >= 0)
			{
				Unsafe.SkipInit(out WBSTRString wBSTRString);
				global::_003CModule_003E.WBSTRString_002E_007Bctor_007D(&wBSTRString);
				try
				{
					Unsafe.SkipInit(out CComPtrNtv_003CITunerConfig_003E cComPtrNtv_003CITunerConfig_003E);
					*(int*)(&cComPtrNtv_003CITunerConfig_003E) = 0;
					try
					{
						if (((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, int>)(int)(*(uint*)(*(int*)(int)(*(uint*)(&cComPtrNtv_003CIService_003E)) + 84)))((IntPtr)(*(int*)(&cComPtrNtv_003CIService_003E))) != 0 && ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, ITunerConfig**, int>)(int)(*(uint*)(*(int*)(int)(*(uint*)(&cComPtrNtv_003CIService_003E)) + 88)))((IntPtr)(*(int*)(&cComPtrNtv_003CIService_003E)), (ITunerConfig**)(&cComPtrNtv_003CITunerConfig_003E)) >= 0)
						{
							global::_003CModule_003E.WString_002EAttachBSTR((WString*)(&wBSTRString), ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, ushort*>)(int)(*(uint*)(*(int*)(int)(*(uint*)(&cComPtrNtv_003CITunerConfig_003E)) + 44)))((IntPtr)(*(int*)(&cComPtrNtv_003CITunerConfig_003E))));
						}
						result = new string((char*)(int)(*(uint*)(&wBSTRString)));
					}
					catch
					{
						//try-fault
						global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<CComPtrNtv_003CITunerConfig_003E*, void>)(&global::_003CModule_003E.CComPtrNtv_003CITunerConfig_003E_002E_007Bdtor_007D), &cComPtrNtv_003CITunerConfig_003E);
						throw;
					}
					global::_003CModule_003E.CComPtrNtv_003CITunerConfig_003E_002ERelease(&cComPtrNtv_003CITunerConfig_003E);
				}
				catch
				{
					//try-fault
					global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<WBSTRString*, void>)(&global::_003CModule_003E.WBSTRString_002E_007Bdtor_007D), &wBSTRString);
					throw;
				}
				global::_003CModule_003E.WString_002E_007Bdtor_007D((WString*)(&wBSTRString));
			}
		}
		catch
		{
			//try-fault
			global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<CComPtrNtv_003CIService_003E*, void>)(&global::_003CModule_003E.CComPtrNtv_003CIService_003E_002E_007Bdtor_007D), &cComPtrNtv_003CIService_003E);
			throw;
		}
		global::_003CModule_003E.CComPtrNtv_003CIService_003E_002ERelease(&cComPtrNtv_003CIService_003E);
		return result;
	}

	public unsafe static string GetCreditCardValidationString()
	{
		Unsafe.SkipInit(out CComPtrNtv_003CIService_003E cComPtrNtv_003CIService_003E);
		*(int*)(&cComPtrNtv_003CIService_003E) = 0;
		string result;
		try
		{
			int singleton = global::_003CModule_003E.GetSingleton((_GUID)global::_003CModule_003E._GUID_bb2d1edd_1bd5_4be1_8d38_36d4f0849911, (void**)(&cComPtrNtv_003CIService_003E));
			result = null;
			if (singleton >= 0)
			{
				Unsafe.SkipInit(out WBSTRString wBSTRString);
				global::_003CModule_003E.WBSTRString_002E_007Bctor_007D(&wBSTRString);
				try
				{
					Unsafe.SkipInit(out CComPtrNtv_003CITunerConfig_003E cComPtrNtv_003CITunerConfig_003E);
					*(int*)(&cComPtrNtv_003CITunerConfig_003E) = 0;
					try
					{
						if (((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, int>)(int)(*(uint*)(*(int*)(int)(*(uint*)(&cComPtrNtv_003CIService_003E)) + 84)))((IntPtr)(*(int*)(&cComPtrNtv_003CIService_003E))) != 0 && ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, ITunerConfig**, int>)(int)(*(uint*)(*(int*)(int)(*(uint*)(&cComPtrNtv_003CIService_003E)) + 88)))((IntPtr)(*(int*)(&cComPtrNtv_003CIService_003E)), (ITunerConfig**)(&cComPtrNtv_003CITunerConfig_003E)) >= 0)
						{
							global::_003CModule_003E.WString_002EAttachBSTR((WString*)(&wBSTRString), ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, ushort*>)(int)(*(uint*)(*(int*)(int)(*(uint*)(&cComPtrNtv_003CITunerConfig_003E)) + 48)))((IntPtr)(*(int*)(&cComPtrNtv_003CITunerConfig_003E))));
						}
						result = new string((char*)(int)(*(uint*)(&wBSTRString)));
					}
					catch
					{
						//try-fault
						global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<CComPtrNtv_003CITunerConfig_003E*, void>)(&global::_003CModule_003E.CComPtrNtv_003CITunerConfig_003E_002E_007Bdtor_007D), &cComPtrNtv_003CITunerConfig_003E);
						throw;
					}
					global::_003CModule_003E.CComPtrNtv_003CITunerConfig_003E_002ERelease(&cComPtrNtv_003CITunerConfig_003E);
				}
				catch
				{
					//try-fault
					global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<WBSTRString*, void>)(&global::_003CModule_003E.WBSTRString_002E_007Bdtor_007D), &wBSTRString);
					throw;
				}
				global::_003CModule_003E.WString_002E_007Bdtor_007D((WString*)(&wBSTRString));
			}
		}
		catch
		{
			//try-fault
			global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<CComPtrNtv_003CIService_003E*, void>)(&global::_003CModule_003E.CComPtrNtv_003CIService_003E_002E_007Bdtor_007D), &cComPtrNtv_003CIService_003E);
			throw;
		}
		global::_003CModule_003E.CComPtrNtv_003CIService_003E_002ERelease(&cComPtrNtv_003CIService_003E);
		return result;
	}

	private FeatureEnablement()
	{
	}
}
