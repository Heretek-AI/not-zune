using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using _003CCppImplementationDetails_003E;
using Microsoft.Iris;
using Microsoft.Zune.Util;
using ZuneUI;

namespace Microsoft.Zune.Service;

public class Service : IDisposable
{
	private static Service m_singletonInstance = null;

	private unsafe IService* m_pService = null;

	public static Service Instance
	{
		get
		{
			if (m_singletonInstance == null)
			{
				m_singletonInstance = new Service();
			}
			return m_singletonInstance;
		}
	}

	private unsafe Service()
	{
		IService* pService = null;
		if (global::_003CModule_003E.GetSingleton((_GUID)global::_003CModule_003E._GUID_bb2d1edd_1bd5_4be1_8d38_36d4f0849911, (void**)(&pService)) >= 0)
		{
			m_pService = pService;
		}
	}

	private unsafe static int PaymentTypeToBillingPaymentType(PaymentType ePaymentType, EBillingPaymentType* pePaymentType)
	{
		if (pePaymentType == null)
		{
			global::_003CModule_003E._ZuneShipAssert(1001u, 501u);
			return -2147467261;
		}
		int result = 0;
		switch (ePaymentType)
		{
		default:
			global::_003CModule_003E._ZuneShipAssert(1003u, 532u);
			result = -2147024809;
			break;
		case PaymentType.Token:
			*pePaymentType = (EBillingPaymentType)4;
			break;
		case PaymentType.Wholesale:
			*pePaymentType = (EBillingPaymentType)3;
			break;
		case PaymentType.DirectDebit:
			*pePaymentType = (EBillingPaymentType)2;
			break;
		case PaymentType.CreditCard:
			*pePaymentType = (EBillingPaymentType)1;
			break;
		case PaymentType.None:
			*pePaymentType = (EBillingPaymentType)0;
			break;
		case PaymentType.Unknown:
			*pePaymentType = (EBillingPaymentType)(-1);
			break;
		}
		return result;
	}

	private unsafe static int PaymentTypeToMediaPaymentType(PaymentType ePaymentType, EMediaPaymentType* pePaymentType)
	{
		if (pePaymentType == null)
		{
			global::_003CModule_003E._ZuneShipAssert(1001u, 545u);
			return -2147467261;
		}
		int result = 0;
		switch (ePaymentType)
		{
		default:
			global::_003CModule_003E._ZuneShipAssert(1003u, 568u);
			result = -2147024809;
			break;
		case PaymentType.Points:
			*pePaymentType = (EMediaPaymentType)3;
			break;
		case PaymentType.Token:
			*pePaymentType = (EMediaPaymentType)4;
			break;
		case PaymentType.CreditCard:
			*pePaymentType = (EMediaPaymentType)1;
			break;
		case PaymentType.Unknown:
			*pePaymentType = (EMediaPaymentType)(-1);
			break;
		}
		return result;
	}

	public unsafe static string GetEndPointUri(EServiceEndpointId eServiceEndpointId)
	{
		object result = null;
		ushort* ptr = null;
		if (global::_003CModule_003E.GetServiceEndPointUri((global::EServiceEndpointId)eServiceEndpointId, &ptr) >= 0)
		{
			result = Marshal.PtrToStringBSTR((IntPtr)ptr);
		}
		if (ptr != null)
		{
			global::_003CModule_003E.SysFreeString(ptr);
		}
		return (string)result;
	}

	public unsafe int Phase3Initialize()
	{
		int result = -2147467259;
		IService* pService = m_pService;
		if (pService != null)
		{
			result = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, int>)(int)(*(uint*)(*(int*)pService + 68)))((nint)pService);
		}
		return result;
	}

	public unsafe int InitializeWMISEndpointCollection()
	{
		int result = -2147467259;
		IService* pService = m_pService;
		if (pService != null)
		{
			result = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, int>)(int)(*(uint*)(*(int*)pService + 444)))((nint)pService);
		}
		return result;
	}

	public unsafe string GetWMISEndPointUri(string strEndPointName)
	{
		string result = null;
		fixed (ushort* ptr = &Unsafe.As<char, ushort>(ref global::_003CModule_003E.PtrToStringChars(strEndPointName)))
		{
			IService* pService = m_pService;
			if (pService != null)
			{
				ushort* ptr2 = null;
				int num = *(int*)pService + 448;
				if (((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, ushort*, ushort**, int>)(int)(*(uint*)num))((nint)m_pService, ptr, &ptr2) >= 0)
				{
					if (ptr2 == null)
					{
						goto IL_004b;
					}
					result = Marshal.PtrToStringBSTR((IntPtr)ptr2);
				}
				if (ptr2 != null)
				{
					global::_003CModule_003E.SysFreeString(ptr2);
				}
			}
			goto IL_004b;
			IL_004b:
			return result;
		}
	}

	private void _007EService()
	{
		_0021Service();
	}

	private unsafe void _0021Service()
	{
		IService* pService = m_pService;
		if (pService != null)
		{
			((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint>)(int)(*(uint*)(*(int*)pService + 8)))((nint)pService);
			m_pService = null;
		}
	}

	public unsafe void SignIn(string strUsername, string strPassword, [MarshalAs(UnmanagedType.U1)] bool fRememberUsername, [MarshalAs(UnmanagedType.U1)] bool fRememberPassword, [MarshalAs(UnmanagedType.U1)] bool fAutomaticallySignInAtStartup, AsyncCompleteHandler eventHandler)
	{
		if (m_pService == null)
		{
			return;
		}
		int num = 0;
		AsyncCallbackWrapper* ptr = (AsyncCallbackWrapper*)global::_003CModule_003E.@new(12u);
		AsyncCallbackWrapper* ptr2;
		try
		{
			ptr2 = ((ptr == null) ? null : global::_003CModule_003E.Microsoft_002EZune_002EUtil_002EAsyncCallbackWrapper_002E_007Bctor_007D(ptr, eventHandler));
		}
		catch
		{
			//try-fault
			global::_003CModule_003E.delete(ptr);
			throw;
		}
		if (ptr2 == null)
		{
			num = -2147024882;
		}
		fixed (ushort* ptr3 = &Unsafe.As<char, ushort>(ref global::_003CModule_003E.PtrToStringChars(strUsername)))
		{
			try
			{
				fixed (ushort* ptr4 = &Unsafe.As<char, ushort>(ref global::_003CModule_003E.PtrToStringChars(strPassword)))
				{
					try
					{
						if (num >= 0)
						{
							int num2 = *(int*)m_pService + 144;
							((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, ushort*, ushort*, int, int, int, IAsyncCallback*, int>)(int)(*(uint*)num2))((nint)m_pService, ptr3, ptr4, fRememberUsername ? 1 : 0, fRememberPassword ? 1 : 0, fAutomaticallySignInAtStartup ? 1 : 0, (IAsyncCallback*)ptr2);
						}
						if (ptr2 != null)
						{
							((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint>)(int)(*(uint*)(*(int*)ptr2 + 8)))((nint)ptr2);
						}
					}
					catch
					{
						//try-fault
						ptr4 = null;
						throw;
					}
				}
			}
			catch
			{
				//try-fault
				ptr3 = null;
				throw;
			}
		}
	}

	public unsafe void RefreshAccount(AsyncCompleteHandler eventHandler)
	{
		if (m_pService != null)
		{
			AsyncCallbackWrapper* ptr = (AsyncCallbackWrapper*)global::_003CModule_003E.@new(12u);
			AsyncCallbackWrapper* ptr2;
			try
			{
				ptr2 = ((ptr == null) ? null : global::_003CModule_003E.Microsoft_002EZune_002EUtil_002EAsyncCallbackWrapper_002E_007Bctor_007D(ptr, eventHandler));
			}
			catch
			{
				//try-fault
				global::_003CModule_003E.delete(ptr);
				throw;
			}
			if (ptr2 != null)
			{
				IService* pService = m_pService;
				((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, int, IAsyncCallback*, int>)(int)(*(uint*)(*(int*)pService + 152)))((nint)pService, 1, (IAsyncCallback*)ptr2);
				((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint>)(int)(*(uint*)(*(int*)ptr2 + 8)))((nint)ptr2);
			}
		}
	}

	[return: MarshalAs(UnmanagedType.U1)]
	public unsafe bool IsSigningIn()
	{
		bool result = false;
		IService* pService = m_pService;
		if (pService != null)
		{
			bool flag = ((((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, int>)(int)(*(uint*)(*(int*)pService + 104)))((nint)pService) != 0) ? true : false);
			result = flag;
		}
		return result;
	}

	[return: MarshalAs(UnmanagedType.U1)]
	public unsafe bool IsSignedIn()
	{
		bool result = false;
		IService* pService = m_pService;
		if (pService != null)
		{
			bool flag = ((((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, int>)(int)(*(uint*)(*(int*)pService + 100)))((nint)pService) != 0) ? true : false);
			result = flag;
		}
		return result;
	}

	[return: MarshalAs(UnmanagedType.U1)]
	public unsafe bool IsSignedInWithSubscription()
	{
		bool result = false;
		IService* pService = m_pService;
		if (pService != null)
		{
			bool flag = ((((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, int>)(int)(*(uint*)(*(int*)pService + 108)))((nint)pService) != 0) ? true : false);
			result = flag;
		}
		return result;
	}

	[return: MarshalAs(UnmanagedType.U1)]
	public unsafe bool BlockExplicitContent()
	{
		bool result = false;
		IService* pService = m_pService;
		if (pService != null)
		{
			bool flag = ((((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, int>)(int)(*(uint*)(*(int*)pService + 124)))((nint)pService) != 0) ? true : false);
			result = flag;
		}
		return result;
	}

	[return: MarshalAs(UnmanagedType.U1)]
	public unsafe bool BlockRatedContent(string system, string rating)
	{
		bool result = false;
		if (m_pService != null)
		{
			fixed (ushort* ptr = &Unsafe.As<char, ushort>(ref global::_003CModule_003E.PtrToStringChars(system)))
			{
				try
				{
					fixed (ushort* ptr2 = &Unsafe.As<char, ushort>(ref global::_003CModule_003E.PtrToStringChars(rating)))
					{
						try
						{
							int num = *(int*)m_pService + 128;
							bool flag = ((((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, ushort*, ushort*, int>)(int)(*(uint*)num))((nint)m_pService, ptr, ptr2) != 0) ? true : false);
							result = flag;
						}
						catch
						{
							//try-fault
							ptr2 = null;
							throw;
						}
					}
				}
				catch
				{
					//try-fault
					ptr = null;
					throw;
				}
			}
		}
		return result;
	}

	[return: MarshalAs(UnmanagedType.U1)]
	public unsafe bool CanSignedInUserPostUsageData()
	{
		bool result = false;
		IService* pService = m_pService;
		if (pService != null)
		{
			bool flag = ((((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, int>)(int)(*(uint*)(*(int*)pService + 132)))((nint)pService) != 0) ? true : false);
			result = flag;
		}
		return result;
	}

	public unsafe string GetSignedInUsername()
	{
		string result = null;
		IService* pService = m_pService;
		if (pService != null)
		{
			ushort* ptr = null;
			if (((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, ushort**, int>)(int)(*(uint*)(*(int*)pService + 136)))((nint)pService, &ptr) >= 0)
			{
				if (ptr == null)
				{
					goto IL_003a;
				}
				result = Marshal.PtrToStringBSTR((IntPtr)ptr);
			}
			if (ptr != null)
			{
				global::_003CModule_003E.SysFreeString(ptr);
			}
		}
		goto IL_003a;
		IL_003a:
		return result;
	}

	public unsafe uint GetSignedInGeoId()
	{
		int result = 0;
		IService* pService = m_pService;
		if (pService != null)
		{
			result = (int)((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint>)(int)(*(uint*)(*(int*)pService + 140)))((nint)pService);
		}
		return (uint)result;
	}

	public unsafe void CancelSignIn()
	{
		IService* pService = m_pService;
		if (pService != null)
		{
			((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, int>)(int)(*(uint*)(*(int*)pService + 164)))((nint)pService);
		}
	}

	public unsafe void SignOut()
	{
		IService* pService = m_pService;
		if (pService != null)
		{
			((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, int>)(int)(*(uint*)(*(int*)pService + 168)))((nint)pService);
		}
	}

	public unsafe IList GetPersistedUsernames()
	{
		IList list = null;
		if (m_pService != null)
		{
			Unsafe.SkipInit(out DynamicArray_003Cunsigned_0020short_0020_002A_003E obj);
			global::_003CModule_003E.DynamicArray_003Cunsigned_0020short_0020_002A_003E_002E_007Bctor_007D(&obj);
			try
			{
				IService* pService = m_pService;
				if (((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, DynamicArray_003Cunsigned_0020short_0020_002A_003E*, int>)(int)(*(uint*)(*(int*)pService + 196)))((nint)pService, &obj) >= 0)
				{
					int num = Unsafe.As<DynamicArray_003Cunsigned_0020short_0020_002A_003E, int>(ref Unsafe.AddByteOffset(ref obj, 8));
					list = new ArrayList(Unsafe.As<DynamicArray_003Cunsigned_0020short_0020_002A_003E, int>(ref Unsafe.AddByteOffset(ref obj, 8)));
					int num2 = 0;
					if (0 < num)
					{
						do
						{
							ushort* ptr = (ushort*)(int)(*(uint*)global::_003CModule_003E.DynamicArray_003Cunsigned_0020short_0020_002A_003E_002E_005B_005D(&obj, num2));
							list.Add(new string((char*)ptr));
							global::_003CModule_003E.SysFreeString(ptr);
							num2++;
						}
						while (num2 < num);
					}
					global::_003CModule_003E.DynamicArray_003Cunsigned_0020short_0020_002A_003E_002ERemoveAllElements(&obj, true);
				}
			}
			catch
			{
				//try-fault
				global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<DynamicArray_003Cunsigned_0020short_0020_002A_003E*, void>)(&global::_003CModule_003E.DynamicArray_003Cunsigned_0020short_0020_002A_003E_002E_007Bdtor_007D), &obj);
				throw;
			}
			global::_003CModule_003E.DynamicArray_003Cunsigned_0020short_0020_002A_003E_002E_007Bdtor_007D(&obj);
		}
		return list;
	}

	public unsafe void RemovePersistedUsername(string strUsername)
	{
		if (m_pService == null)
		{
			return;
		}
		fixed (ushort* ptr = &Unsafe.As<char, ushort>(ref global::_003CModule_003E.PtrToStringChars(strUsername)))
		{
			try
			{
				int num = *(int*)m_pService + 200;
				((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, ushort*, int>)(int)(*(uint*)num))((nint)m_pService, ptr);
			}
			catch
			{
				//try-fault
				ptr = null;
				throw;
			}
		}
	}

	[return: MarshalAs(UnmanagedType.U1)]
	public unsafe bool SignInPasswordRequired(string strUsername)
	{
		bool result = false;
		if (m_pService != null)
		{
			fixed (ushort* ptr = &Unsafe.As<char, ushort>(ref global::_003CModule_003E.PtrToStringChars(strUsername)))
			{
				try
				{
					int num = 0;
					int num2 = *(int*)m_pService + 204;
					if (((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, ushort*, int*, int>)(int)(*(uint*)num2))((nint)m_pService, ptr, &num) >= 0)
					{
						bool flag = ((num != 0) ? true : false);
						result = flag;
					}
				}
				catch
				{
					//try-fault
					ptr = null;
					throw;
				}
			}
		}
		return result;
	}

	[return: MarshalAs(UnmanagedType.U1)]
	public unsafe bool SignInAtStartup(string strUsername)
	{
		bool result = false;
		if (m_pService != null)
		{
			fixed (ushort* ptr = &Unsafe.As<char, ushort>(ref global::_003CModule_003E.PtrToStringChars(strUsername)))
			{
				try
				{
					int num = 0;
					int num2 = *(int*)m_pService + 208;
					if (((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, ushort*, int*, int>)(int)(*(uint*)num2))((nint)m_pService, ptr, &num) >= 0)
					{
						bool flag = ((num != 0) ? true : false);
						result = flag;
					}
				}
				catch
				{
					//try-fault
					ptr = null;
					throw;
				}
			}
		}
		return result;
	}

	public unsafe string GetSignInAtStartupUsername()
	{
		string result = null;
		IService* pService = m_pService;
		if (pService != null)
		{
			ushort* ptr = null;
			if (((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, ushort**, int>)(int)(*(uint*)(*(int*)pService + 212)))((nint)pService, &ptr) >= 0)
			{
				if (ptr == null)
				{
					goto IL_003a;
				}
				result = Marshal.PtrToStringBSTR((IntPtr)ptr);
			}
			if (ptr != null)
			{
				global::_003CModule_003E.SysFreeString(ptr);
			}
		}
		goto IL_003a;
		IL_003a:
		return result;
	}

	[return: MarshalAs(UnmanagedType.U1)]
	public unsafe bool CanDownloadSubscriptionContent()
	{
		bool result = false;
		if (IsSignedIn())
		{
			Unsafe.SkipInit(out CComPtrNtv_003CISignInState_003E cComPtrNtv_003CISignInState_003E);
			*(int*)(&cComPtrNtv_003CISignInState_003E) = 0;
			try
			{
				IService* pService = m_pService;
				if (((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, ISignInState**, int>)(int)(*(uint*)(*(int*)pService + 172)))((nint)pService, (ISignInState**)(&cComPtrNtv_003CISignInState_003E)) >= 0)
				{
					bool flag = ((((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, int>)(int)(*(uint*)(*(int*)(int)(*(uint*)(&cComPtrNtv_003CISignInState_003E)) + 92)))((IntPtr)(*(int*)(&cComPtrNtv_003CISignInState_003E))) != 0) ? true : false);
					result = flag;
				}
			}
			catch
			{
				//try-fault
				global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<CComPtrNtv_003CISignInState_003E*, void>)(&global::_003CModule_003E.CComPtrNtv_003CISignInState_003E_002E_007Bdtor_007D), &cComPtrNtv_003CISignInState_003E);
				throw;
			}
			global::_003CModule_003E.CComPtrNtv_003CISignInState_003E_002ERelease(&cComPtrNtv_003CISignInState_003E);
		}
		return result;
	}

	public unsafe void GetLastSignedInUserSubscriptionState(out bool activeSubscription, out ulong subscriptionId)
	{
		activeSubscription = false;
		subscriptionId = 0uL;
		IService* pService = m_pService;
		if (pService != null)
		{
			int num = 0;
			ulong num2 = 0uL;
			if (((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, int*, ulong*, int>)(int)(*(uint*)(*(int*)pService + 188)))((nint)pService, &num, &num2) >= 0)
			{
				bool flag = ((num != 0) ? true : false);
				activeSubscription = flag;
				subscriptionId = num2;
			}
		}
	}

	[return: MarshalAs(UnmanagedType.U1)]
	public unsafe bool GetLastSignedInUserGuid(out int iUserId, out Guid guidUserGuid)
	{
		bool result = false;
		_GUID gUID_NULL = global::_003CModule_003E.GUID_NULL;
		int num = 0;
		IService* pService = m_pService;
		if (pService != null && ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, _GUID*, int*, int>)(int)(*(uint*)(*(int*)pService + 192)))((nint)pService, &gUID_NULL, &num) >= 0)
		{
			result = true;
		}
		iUserId = num;
		Guid guid = global::_003CModule_003E.GUIDToGuid(gUID_NULL);
		guidUserGuid = guid;
		return result;
	}

	[return: MarshalAs(UnmanagedType.U1)]
	public unsafe bool ClearLastSignedInUser()
	{
		bool result = false;
		IService* pService = m_pService;
		if (pService != null)
		{
			if (((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, int>)(int)(*(uint*)(*(int*)pService + 180)))((nint)pService) >= 0)
			{
				result = true;
			}
		}
		return result;
	}

	[return: MarshalAs(UnmanagedType.U1)]
	public unsafe bool SetLastSignedInUserGuid(ref Guid guidUserGuid, out int iUserId)
	{
		bool result = false;
		_GUID gUID = global::_003CModule_003E.GuidToGUID(guidUserGuid);
		int num = 0;
		IService* pService = m_pService;
		if (pService != null && ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, ulong, _GUID*, ushort*, ushort*, int, int*, int>)(int)(*(uint*)(*(int*)pService + 176)))((nint)pService, 0uL, &gUID, null, null, 0, &num) >= 0)
		{
			result = true;
		}
		iUserId = num;
		return result;
	}

	public unsafe string GetXboxPuid()
	{
		string result = null;
		IService* pService = m_pService;
		if (pService != null)
		{
			ulong num = 0uL;
			int num2 = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, ulong*, int>)(int)(*(uint*)(*(int*)pService + 472)))((nint)pService, &num);
			Unsafe.SkipInit(out _0024ArrayType_0024_0024_0024BY0BO_0040G _0024ArrayType_0024_0024_0024BY0BO_0040G2);
			*(short*)(&_0024ArrayType_0024_0024_0024BY0BO_0040G2) = 0;
			// IL initblk instruction
			Unsafe.InitBlock(ref Unsafe.AddByteOffset(ref _0024ArrayType_0024_0024_0024BY0BO_0040G2, 2), 0, 58);
			if (num2 >= 0)
			{
				if (global::_003CModule_003E._i64tow_s((long)num, (ushort*)(&_0024ArrayType_0024_0024_0024BY0BO_0040G2), 30u, 10) != 0)
				{
					num2 = -2147467259;
				}
				if (num2 >= 0)
				{
					result = new string((char*)(&_0024ArrayType_0024_0024_0024BY0BO_0040G2));
				}
			}
		}
		return result;
	}

	public unsafe void RegisterForDownloadNotification(DownloadEventHandler eventHandler, DownloadEventProgressHandler progressHandler, EventHandler allPendingHandler)
	{
		DownloadCallbackWrapper* ptr = (DownloadCallbackWrapper*)global::_003CModule_003E.@new(20u);
		DownloadCallbackWrapper* ptr2;
		try
		{
			ptr2 = ((ptr == null) ? null : global::_003CModule_003E.Microsoft_002EZune_002EService_002EDownloadCallbackWrapper_002E_007Bctor_007D(ptr, eventHandler, progressHandler, allPendingHandler));
		}
		catch
		{
			//try-fault
			global::_003CModule_003E.delete(ptr);
			throw;
		}
		if (ptr2 != null)
		{
			IService* pService = m_pService;
			((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, IDownloadCallback*, int>)(int)(*(uint*)(*(int*)pService + 300)))((nint)pService, (IDownloadCallback*)ptr2);
			((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint>)(int)(*(uint*)(*(int*)ptr2 + 8)))((nint)ptr2);
		}
	}

	public unsafe void Download(IList items, EDownloadFlags eDownloadFlags, string deviceEndpointId, EDownloadContextEvent clientContextEvent, string clientContextEventData, DownloadEventHandler eventHandler, DownloadEventProgressHandler progressHandler, EventHandler allPendingHandler)
	{
		//IL_03b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ba: Expected O, but got Unknown
		//IL_0345: Unknown result type (might be due to invalid IL or missing references)
		//IL_034c: Expected O, but got Unknown
		//The blocks IL_05d5, IL_063a are reachable both inside and outside the pinned region starting at IL_05d3. ILSpy has duplicated these blocks in order to place them both within and outside the `fixed` statement.
		//The blocks IL_05b4, IL_05b8, IL_05ba, IL_0647 are reachable both inside and outside the pinned region starting at IL_05b2. ILSpy has duplicated these blocks in order to place them both within and outside the `fixed` statement.
		//The blocks IL_059f, IL_05a2, IL_0650 are reachable both inside and outside the pinned region starting at IL_059d. ILSpy has duplicated these blocks in order to place them both within and outside the `fixed` statement.
		//The blocks IL_058b, IL_0594, IL_0659 are reachable both inside and outside the pinned region starting at IL_0589. ILSpy has duplicated these blocks in order to place them both within and outside the `fixed` statement.
		//The blocks IL_0577, IL_0580, IL_0662 are reachable both inside and outside the pinned region starting at IL_0575. ILSpy has duplicated these blocks in order to place them both within and outside the `fixed` statement.
		//The blocks IL_0099, IL_009f, IL_00a7, IL_00b3, IL_00db, IL_0100, IL_0110, IL_0120, IL_0130, IL_0140, IL_0150, IL_0160, IL_0170, IL_0180, IL_0190, IL_01a0, IL_01b0, IL_01bd, IL_0211, IL_0221, IL_0231, IL_0241, IL_0251, IL_0261, IL_026e, IL_02b0, IL_02bd, IL_02cd, IL_030f, IL_0353, IL_036b, IL_03c1, IL_03d9, IL_0409, IL_0411, IL_044d, IL_0455, IL_0488, IL_0490, IL_04ba, IL_04cf, IL_04e8, IL_04f2, IL_0508, IL_0519, IL_0543, IL_054b, IL_055b, IL_056a, IL_056c, IL_066b, IL_0673, IL_0678, IL_069f, IL_06a3, IL_06b0, IL_06b6 are reachable both inside and outside the pinned region starting at IL_0097. ILSpy has duplicated these blocks in order to place them both within and outside the `fixed` statement.
		string text = null;
		if (m_pService == null)
		{
			return;
		}
		/*pinned*/ref ushort reference = ref *(ushort*)null;
		try
		{
			int num = 0;
			DownloadCallbackWrapper* ptr = (DownloadCallbackWrapper*)global::_003CModule_003E.@new(20u);
			DownloadCallbackWrapper* ptr2;
			try
			{
				ptr2 = ((ptr == null) ? null : global::_003CModule_003E.Microsoft_002EZune_002EService_002EDownloadCallbackWrapper_002E_007Bctor_007D(ptr, eventHandler, progressHandler, allPendingHandler));
			}
			catch
			{
				//try-fault
				global::_003CModule_003E.delete(ptr);
				throw;
			}
			DownloadCallbackWrapper* ptr3 = ptr2;
			if (ptr2 == null)
			{
				num = -2147024882;
			}
			IMediaCollection* ptr4 = null;
			if (num < 0)
			{
				goto IL_06b0;
			}
			IService* pService = m_pService;
			num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, IMediaCollection**, int>)(int)(*(uint*)(*(int*)pService + 16)))((nint)pService, &ptr4);
			if (num < 0)
			{
				goto IL_069f;
			}
			_GUID gUID;
			ref ushort reference2;
			ref ushort reference3;
			Unsafe.SkipInit(out CComPtrNtv_003CIContextData_003E cComPtrNtv_003CIContextData_003E);
			if (!(deviceEndpointId != null) || deviceEndpointId.Equals(""))
			{
				if (items != null)
				{
					IEnumerator enumerator = items.GetEnumerator();
					while (enumerator.MoveNext())
					{
						object current = enumerator.Current;
						Guid guid = Guid.Empty;
						string s = null;
						string s2 = null;
						string s3 = null;
						text = null;
						global::EContentType eContentType = (global::EContentType)(-1);
						if (current is DataProviderObject)
						{
							DataProviderObject val = (DataProviderObject)((current is DataProviderObject) ? current : null);
							string typeName = val.TypeName;
							try
							{
								text = (string)val.GetProperty("ReferrerContext");
							}
							catch (KeyNotFoundException ex)
							{
							}
							if (!typeName.Equals("Album") && !typeName.Equals("AlbumData") && !typeName.Equals("RecommendedAlbum"))
							{
								if (!typeName.Equals("Track") && !typeName.Equals("ChannelTrack") && !typeName.Equals("ProfileTrack") && !typeName.Equals("PlaylistTrack") && !typeName.Equals("ZplTrack") && !typeName.Equals("RecommendedTrack") && !typeName.Equals("TrackPurchaseHistory") && !typeName.Equals("TrackDownloadHistory"))
								{
									if (typeName.Equals("PlaylistContentItem"))
									{
										eContentType = (global::EContentType)0;
										guid = (Guid)val.GetProperty("ZuneMediaId");
										s = (string)val.GetProperty("Title");
										s2 = (string)val.GetProperty("AlbumName");
										s3 = (string)val.GetProperty("ArtistName");
									}
									else if (!typeName.Equals("MusicVideo") && !typeName.Equals("MusicVideoHistory") && !typeName.Equals("Episode") && !typeName.Equals("Short") && !typeName.Equals("VideoHistory"))
									{
										if (typeName.Equals("Video"))
										{
											eContentType = (global::EContentType)3;
											guid = (Guid)val.GetProperty("ZuneMediaId");
											s = (string)val.GetProperty("Title");
											s3 = (string)val.GetProperty("Title");
										}
										else if (typeName.Equals("AppData") || typeName.Equals("ZuneHDAppData"))
										{
											eContentType = (global::EContentType)7;
											guid = (Guid)val.GetProperty("Id");
											s = (string)val.GetProperty("Title");
											s3 = (string)val.GetProperty("Author");
										}
									}
									else
									{
										eContentType = (global::EContentType)3;
										guid = (Guid)val.GetProperty("Id");
										s = (string)val.GetProperty("Title");
										DataProviderObject val2 = (DataProviderObject)val.GetProperty("PrimaryArtist");
										if (val2 != null)
										{
											s3 = (string)val2.GetProperty("Title");
										}
									}
								}
								else
								{
									eContentType = (global::EContentType)0;
									guid = (Guid)val.GetProperty("Id");
									s = (string)val.GetProperty("Title");
									s2 = (string)val.GetProperty("AlbumTitle");
									DataProviderObject val3 = (DataProviderObject)val.GetProperty("PrimaryArtist");
									if (val3 != null)
									{
										s3 = (string)val3.GetProperty("Title");
									}
								}
							}
							else
							{
								eContentType = (global::EContentType)1;
								guid = (Guid)val.GetProperty("Id");
								s = (string)val.GetProperty("Title");
							}
						}
						else if (current is TrackOffer)
						{
							eContentType = (global::EContentType)0;
							TrackOffer trackOffer = current as TrackOffer;
							guid = trackOffer.Id;
							s = trackOffer.Title;
							s2 = trackOffer.Album;
							s3 = trackOffer.Artist;
							text = trackOffer.ServiceContext;
						}
						else if (current is VideoOffer)
						{
							eContentType = (global::EContentType)3;
							VideoOffer videoOffer = current as VideoOffer;
							guid = videoOffer.Id;
							s = videoOffer.Title;
							s2 = videoOffer.SeriesTitle;
							s3 = videoOffer.Artist;
						}
						else if (current is AlbumOffer)
						{
							eContentType = (global::EContentType)1;
							AlbumOffer albumOffer = current as AlbumOffer;
							guid = albumOffer.Id;
							s = albumOffer.Title;
							text = albumOffer.ServiceContext;
						}
						else if ((object)current.GetType() == typeof(DownloadTask))
						{
							DownloadTask downloadTask = (DownloadTask)current;
							string property = ((DownloadTask)current).GetProperty("Type");
							if (!string.IsNullOrEmpty(property))
							{
								eContentType = (global::EContentType)GetContentType(property);
							}
							string property2 = ((DownloadTask)current).GetProperty("ServiceId");
							if (!string.IsNullOrEmpty(property2))
							{
								try
								{
									Guid guid2 = new Guid(property2);
									guid = guid2;
								}
								catch (FormatException ex2)
								{
								}
							}
							s = downloadTask.GetProperty("Title");
							s2 = downloadTask.GetProperty("Album");
							s3 = downloadTask.GetProperty("Artist");
						}
						if (num < 0 || !(guid != Guid.Empty))
						{
							continue;
						}
						gUID = global::_003CModule_003E.GuidToGUID(guid);
						fixed (ushort* ptr5 = &Unsafe.As<char, ushort>(ref global::_003CModule_003E.PtrToStringChars(s)))
						{
							try
							{
								if (ptr5 == null)
								{
									fixed (ushort* ptr5 = &Unsafe.As<_0024ArrayType_0024_0024_0024BY00_0024_0024CBG, ushort>(ref global::_003CModule_003E._003F_003F_C_0040_11LOCGONAA_0040_003F_0024AA_003F_0024AA_0040))
									{
										fixed (ushort* ptr6 = &Unsafe.As<char, ushort>(ref global::_003CModule_003E.PtrToStringChars(s2)))
										{
											try
											{
												if (ptr6 == null)
												{
													fixed (ushort* ptr6 = &Unsafe.As<_0024ArrayType_0024_0024_0024BY00_0024_0024CBG, ushort>(ref global::_003CModule_003E._003F_003F_C_0040_11LOCGONAA_0040_003F_0024AA_003F_0024AA_0040))
													{
														fixed (ushort* ptr7 = &Unsafe.As<char, ushort>(ref global::_003CModule_003E.PtrToStringChars(s3)))
														{
															try
															{
																if (ptr7 == null)
																{
																	fixed (ushort* ptr7 = &Unsafe.As<_0024ArrayType_0024_0024_0024BY00_0024_0024CBG, ushort>(ref global::_003CModule_003E._003F_003F_C_0040_11LOCGONAA_0040_003F_0024AA_003F_0024AA_0040))
																	{
																		reference2 = ref *(ushort*)null;
																		try
																		{
																			if (!string.IsNullOrEmpty(text))
																			{
																				fixed (ushort* ptr8 = &Unsafe.As<char, ushort>(ref global::_003CModule_003E.PtrToStringChars(text)))
																				{
																					global::EDownloadContextEvent eDownloadContextEvent = (global::EDownloadContextEvent)(-1);
																					reference3 = ref *(ushort*)null;
																					try
																					{
																						if (clientContextEvent != EDownloadContextEvent.Unknown && !string.IsNullOrEmpty(clientContextEventData))
																						{
																							eDownloadContextEvent = (global::EDownloadContextEvent)clientContextEvent;
																							fixed (ushort* ptr9 = &Unsafe.As<char, ushort>(ref global::_003CModule_003E.PtrToStringChars(clientContextEventData)))
																							{
																								global::_003CModule_003E.CComPtrNtv_003CIContextData_003E_002E_007Bctor_007D(&cComPtrNtv_003CIContextData_003E);
																								try
																								{
																									int num2 = *(int*)m_pService + 60;
																									((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, ushort*, global::EDownloadContextEvent, ushort*, IContextData**, int>)(int)(*(uint*)num2))((nint)m_pService, ptr8, eDownloadContextEvent, ptr9, global::_003CModule_003E.CComPtrNtv_003CIContextData_003E_002E_0026(&cComPtrNtv_003CIContextData_003E));
																									int num3 = *(int*)ptr4 + 20;
																									num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, _GUID*, global::EContentType, ushort*, ushort*, ushort*, IContextData*, int>)(int)(*(uint*)num3))((nint)ptr4, &gUID, eContentType, ptr5, ptr6, ptr7, global::_003CModule_003E.CComPtrNtv_003CIContextData_003E_002E_002EPAUIContextData_0040_0040(&cComPtrNtv_003CIContextData_003E));
																								}
																								catch
																								{
																									//try-fault
																									global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<CComPtrNtv_003CIContextData_003E*, void>)(&global::_003CModule_003E.CComPtrNtv_003CIContextData_003E_002E_007Bdtor_007D), &cComPtrNtv_003CIContextData_003E);
																									throw;
																								}
																								global::_003CModule_003E.CComPtrNtv_003CIContextData_003E_002E_007Bdtor_007D(&cComPtrNtv_003CIContextData_003E);
																							}
																						}
																						else
																						{
																							global::_003CModule_003E.CComPtrNtv_003CIContextData_003E_002E_007Bctor_007D(&cComPtrNtv_003CIContextData_003E);
																							try
																							{
																								int num2 = *(int*)m_pService + 60;
																								((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, ushort*, global::EDownloadContextEvent, ushort*, IContextData**, int>)(int)(*(uint*)num2))((nint)m_pService, ptr8, eDownloadContextEvent, (ushort*)Unsafe.AsPointer(ref reference3), global::_003CModule_003E.CComPtrNtv_003CIContextData_003E_002E_0026(&cComPtrNtv_003CIContextData_003E));
																								int num3 = *(int*)ptr4 + 20;
																								num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, _GUID*, global::EContentType, ushort*, ushort*, ushort*, IContextData*, int>)(int)(*(uint*)num3))((nint)ptr4, &gUID, eContentType, ptr5, ptr6, ptr7, global::_003CModule_003E.CComPtrNtv_003CIContextData_003E_002E_002EPAUIContextData_0040_0040(&cComPtrNtv_003CIContextData_003E));
																							}
																							catch
																							{
																								//try-fault
																								global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<CComPtrNtv_003CIContextData_003E*, void>)(&global::_003CModule_003E.CComPtrNtv_003CIContextData_003E_002E_007Bdtor_007D), &cComPtrNtv_003CIContextData_003E);
																								throw;
																							}
																							global::_003CModule_003E.CComPtrNtv_003CIContextData_003E_002E_007Bdtor_007D(&cComPtrNtv_003CIContextData_003E);
																						}
																					}
																					catch
																					{
																						//try-fault
																						reference3 = ref *(ushort*)null;
																						throw;
																					}
																					reference3 = ref *(ushort*)null;
																				}
																			}
																			else
																			{
																				global::EDownloadContextEvent eDownloadContextEvent = (global::EDownloadContextEvent)(-1);
																				reference3 = ref *(ushort*)null;
																				try
																				{
																					if (clientContextEvent != EDownloadContextEvent.Unknown && !string.IsNullOrEmpty(clientContextEventData))
																					{
																						eDownloadContextEvent = (global::EDownloadContextEvent)clientContextEvent;
																						fixed (ushort* ptr9 = &Unsafe.As<char, ushort>(ref global::_003CModule_003E.PtrToStringChars(clientContextEventData)))
																						{
																							global::_003CModule_003E.CComPtrNtv_003CIContextData_003E_002E_007Bctor_007D(&cComPtrNtv_003CIContextData_003E);
																							try
																							{
																								int num2 = *(int*)m_pService + 60;
																								((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, ushort*, global::EDownloadContextEvent, ushort*, IContextData**, int>)(int)(*(uint*)num2))((nint)m_pService, (ushort*)Unsafe.AsPointer(ref reference2), eDownloadContextEvent, ptr9, global::_003CModule_003E.CComPtrNtv_003CIContextData_003E_002E_0026(&cComPtrNtv_003CIContextData_003E));
																								int num3 = *(int*)ptr4 + 20;
																								num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, _GUID*, global::EContentType, ushort*, ushort*, ushort*, IContextData*, int>)(int)(*(uint*)num3))((nint)ptr4, &gUID, eContentType, ptr5, ptr6, ptr7, global::_003CModule_003E.CComPtrNtv_003CIContextData_003E_002E_002EPAUIContextData_0040_0040(&cComPtrNtv_003CIContextData_003E));
																							}
																							catch
																							{
																								//try-fault
																								global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<CComPtrNtv_003CIContextData_003E*, void>)(&global::_003CModule_003E.CComPtrNtv_003CIContextData_003E_002E_007Bdtor_007D), &cComPtrNtv_003CIContextData_003E);
																								throw;
																							}
																							global::_003CModule_003E.CComPtrNtv_003CIContextData_003E_002E_007Bdtor_007D(&cComPtrNtv_003CIContextData_003E);
																						}
																					}
																					else
																					{
																						global::_003CModule_003E.CComPtrNtv_003CIContextData_003E_002E_007Bctor_007D(&cComPtrNtv_003CIContextData_003E);
																						try
																						{
																							int num2 = *(int*)m_pService + 60;
																							((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, ushort*, global::EDownloadContextEvent, ushort*, IContextData**, int>)(int)(*(uint*)num2))((nint)m_pService, (ushort*)Unsafe.AsPointer(ref reference2), eDownloadContextEvent, (ushort*)Unsafe.AsPointer(ref reference3), global::_003CModule_003E.CComPtrNtv_003CIContextData_003E_002E_0026(&cComPtrNtv_003CIContextData_003E));
																							int num3 = *(int*)ptr4 + 20;
																							num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, _GUID*, global::EContentType, ushort*, ushort*, ushort*, IContextData*, int>)(int)(*(uint*)num3))((nint)ptr4, &gUID, eContentType, ptr5, ptr6, ptr7, global::_003CModule_003E.CComPtrNtv_003CIContextData_003E_002E_002EPAUIContextData_0040_0040(&cComPtrNtv_003CIContextData_003E));
																						}
																						catch
																						{
																							//try-fault
																							global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<CComPtrNtv_003CIContextData_003E*, void>)(&global::_003CModule_003E.CComPtrNtv_003CIContextData_003E_002E_007Bdtor_007D), &cComPtrNtv_003CIContextData_003E);
																							throw;
																						}
																						global::_003CModule_003E.CComPtrNtv_003CIContextData_003E_002E_007Bdtor_007D(&cComPtrNtv_003CIContextData_003E);
																					}
																				}
																				catch
																				{
																					//try-fault
																					reference3 = ref *(ushort*)null;
																					throw;
																				}
																				reference3 = ref *(ushort*)null;
																			}
																		}
																		catch
																		{
																			//try-fault
																			reference2 = ref *(ushort*)null;
																			throw;
																		}
																		reference2 = ref *(ushort*)null;
																	}
																	continue;
																}
																reference2 = ref *(ushort*)null;
																try
																{
																	if (!string.IsNullOrEmpty(text))
																	{
																		fixed (ushort* ptr8 = &Unsafe.As<char, ushort>(ref global::_003CModule_003E.PtrToStringChars(text)))
																		{
																			global::EDownloadContextEvent eDownloadContextEvent = (global::EDownloadContextEvent)(-1);
																			reference3 = ref *(ushort*)null;
																			try
																			{
																				if (clientContextEvent != EDownloadContextEvent.Unknown && !string.IsNullOrEmpty(clientContextEventData))
																				{
																					eDownloadContextEvent = (global::EDownloadContextEvent)clientContextEvent;
																					fixed (ushort* ptr9 = &Unsafe.As<char, ushort>(ref global::_003CModule_003E.PtrToStringChars(clientContextEventData)))
																					{
																						global::_003CModule_003E.CComPtrNtv_003CIContextData_003E_002E_007Bctor_007D(&cComPtrNtv_003CIContextData_003E);
																						try
																						{
																							int num2 = *(int*)m_pService + 60;
																							((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, ushort*, global::EDownloadContextEvent, ushort*, IContextData**, int>)(int)(*(uint*)num2))((nint)m_pService, ptr8, eDownloadContextEvent, ptr9, global::_003CModule_003E.CComPtrNtv_003CIContextData_003E_002E_0026(&cComPtrNtv_003CIContextData_003E));
																							int num3 = *(int*)ptr4 + 20;
																							num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, _GUID*, global::EContentType, ushort*, ushort*, ushort*, IContextData*, int>)(int)(*(uint*)num3))((nint)ptr4, &gUID, eContentType, ptr5, ptr6, ptr7, global::_003CModule_003E.CComPtrNtv_003CIContextData_003E_002E_002EPAUIContextData_0040_0040(&cComPtrNtv_003CIContextData_003E));
																						}
																						catch
																						{
																							//try-fault
																							global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<CComPtrNtv_003CIContextData_003E*, void>)(&global::_003CModule_003E.CComPtrNtv_003CIContextData_003E_002E_007Bdtor_007D), &cComPtrNtv_003CIContextData_003E);
																							throw;
																						}
																						global::_003CModule_003E.CComPtrNtv_003CIContextData_003E_002E_007Bdtor_007D(&cComPtrNtv_003CIContextData_003E);
																					}
																				}
																				else
																				{
																					global::_003CModule_003E.CComPtrNtv_003CIContextData_003E_002E_007Bctor_007D(&cComPtrNtv_003CIContextData_003E);
																					try
																					{
																						int num2 = *(int*)m_pService + 60;
																						((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, ushort*, global::EDownloadContextEvent, ushort*, IContextData**, int>)(int)(*(uint*)num2))((nint)m_pService, ptr8, eDownloadContextEvent, (ushort*)Unsafe.AsPointer(ref reference3), global::_003CModule_003E.CComPtrNtv_003CIContextData_003E_002E_0026(&cComPtrNtv_003CIContextData_003E));
																						int num3 = *(int*)ptr4 + 20;
																						num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, _GUID*, global::EContentType, ushort*, ushort*, ushort*, IContextData*, int>)(int)(*(uint*)num3))((nint)ptr4, &gUID, eContentType, ptr5, ptr6, ptr7, global::_003CModule_003E.CComPtrNtv_003CIContextData_003E_002E_002EPAUIContextData_0040_0040(&cComPtrNtv_003CIContextData_003E));
																					}
																					catch
																					{
																						//try-fault
																						global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<CComPtrNtv_003CIContextData_003E*, void>)(&global::_003CModule_003E.CComPtrNtv_003CIContextData_003E_002E_007Bdtor_007D), &cComPtrNtv_003CIContextData_003E);
																						throw;
																					}
																					global::_003CModule_003E.CComPtrNtv_003CIContextData_003E_002E_007Bdtor_007D(&cComPtrNtv_003CIContextData_003E);
																				}
																			}
																			catch
																			{
																				//try-fault
																				reference3 = ref *(ushort*)null;
																				throw;
																			}
																			reference3 = ref *(ushort*)null;
																		}
																	}
																	else
																	{
																		global::EDownloadContextEvent eDownloadContextEvent = (global::EDownloadContextEvent)(-1);
																		reference3 = ref *(ushort*)null;
																		try
																		{
																			if (clientContextEvent != EDownloadContextEvent.Unknown && !string.IsNullOrEmpty(clientContextEventData))
																			{
																				eDownloadContextEvent = (global::EDownloadContextEvent)clientContextEvent;
																				fixed (ushort* ptr9 = &Unsafe.As<char, ushort>(ref global::_003CModule_003E.PtrToStringChars(clientContextEventData)))
																				{
																					global::_003CModule_003E.CComPtrNtv_003CIContextData_003E_002E_007Bctor_007D(&cComPtrNtv_003CIContextData_003E);
																					try
																					{
																						int num2 = *(int*)m_pService + 60;
																						((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, ushort*, global::EDownloadContextEvent, ushort*, IContextData**, int>)(int)(*(uint*)num2))((nint)m_pService, (ushort*)Unsafe.AsPointer(ref reference2), eDownloadContextEvent, ptr9, global::_003CModule_003E.CComPtrNtv_003CIContextData_003E_002E_0026(&cComPtrNtv_003CIContextData_003E));
																						int num3 = *(int*)ptr4 + 20;
																						num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, _GUID*, global::EContentType, ushort*, ushort*, ushort*, IContextData*, int>)(int)(*(uint*)num3))((nint)ptr4, &gUID, eContentType, ptr5, ptr6, ptr7, global::_003CModule_003E.CComPtrNtv_003CIContextData_003E_002E_002EPAUIContextData_0040_0040(&cComPtrNtv_003CIContextData_003E));
																					}
																					catch
																					{
																						//try-fault
																						global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<CComPtrNtv_003CIContextData_003E*, void>)(&global::_003CModule_003E.CComPtrNtv_003CIContextData_003E_002E_007Bdtor_007D), &cComPtrNtv_003CIContextData_003E);
																						throw;
																					}
																					global::_003CModule_003E.CComPtrNtv_003CIContextData_003E_002E_007Bdtor_007D(&cComPtrNtv_003CIContextData_003E);
																				}
																			}
																			else
																			{
																				global::_003CModule_003E.CComPtrNtv_003CIContextData_003E_002E_007Bctor_007D(&cComPtrNtv_003CIContextData_003E);
																				try
																				{
																					int num2 = *(int*)m_pService + 60;
																					((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, ushort*, global::EDownloadContextEvent, ushort*, IContextData**, int>)(int)(*(uint*)num2))((nint)m_pService, (ushort*)Unsafe.AsPointer(ref reference2), eDownloadContextEvent, (ushort*)Unsafe.AsPointer(ref reference3), global::_003CModule_003E.CComPtrNtv_003CIContextData_003E_002E_0026(&cComPtrNtv_003CIContextData_003E));
																					int num3 = *(int*)ptr4 + 20;
																					num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, _GUID*, global::EContentType, ushort*, ushort*, ushort*, IContextData*, int>)(int)(*(uint*)num3))((nint)ptr4, &gUID, eContentType, ptr5, ptr6, ptr7, global::_003CModule_003E.CComPtrNtv_003CIContextData_003E_002E_002EPAUIContextData_0040_0040(&cComPtrNtv_003CIContextData_003E));
																				}
																				catch
																				{
																					//try-fault
																					global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<CComPtrNtv_003CIContextData_003E*, void>)(&global::_003CModule_003E.CComPtrNtv_003CIContextData_003E_002E_007Bdtor_007D), &cComPtrNtv_003CIContextData_003E);
																					throw;
																				}
																				global::_003CModule_003E.CComPtrNtv_003CIContextData_003E_002E_007Bdtor_007D(&cComPtrNtv_003CIContextData_003E);
																			}
																		}
																		catch
																		{
																			//try-fault
																			reference3 = ref *(ushort*)null;
																			throw;
																		}
																		reference3 = ref *(ushort*)null;
																	}
																}
																catch
																{
																	//try-fault
																	reference2 = ref *(ushort*)null;
																	throw;
																}
																reference2 = ref *(ushort*)null;
															}
															catch
															{
																//try-fault
																ptr7 = null;
																throw;
															}
														}
													}
													continue;
												}
												fixed (ushort* ptr7 = &Unsafe.As<char, ushort>(ref global::_003CModule_003E.PtrToStringChars(s3)))
												{
													try
													{
														if (ptr7 == null)
														{
															fixed (ushort* ptr7 = &Unsafe.As<_0024ArrayType_0024_0024_0024BY00_0024_0024CBG, ushort>(ref global::_003CModule_003E._003F_003F_C_0040_11LOCGONAA_0040_003F_0024AA_003F_0024AA_0040))
															{
																reference2 = ref *(ushort*)null;
																try
																{
																	if (!string.IsNullOrEmpty(text))
																	{
																		fixed (ushort* ptr8 = &Unsafe.As<char, ushort>(ref global::_003CModule_003E.PtrToStringChars(text)))
																		{
																			global::EDownloadContextEvent eDownloadContextEvent = (global::EDownloadContextEvent)(-1);
																			reference3 = ref *(ushort*)null;
																			try
																			{
																				if (clientContextEvent != EDownloadContextEvent.Unknown && !string.IsNullOrEmpty(clientContextEventData))
																				{
																					eDownloadContextEvent = (global::EDownloadContextEvent)clientContextEvent;
																					fixed (ushort* ptr9 = &Unsafe.As<char, ushort>(ref global::_003CModule_003E.PtrToStringChars(clientContextEventData)))
																					{
																						global::_003CModule_003E.CComPtrNtv_003CIContextData_003E_002E_007Bctor_007D(&cComPtrNtv_003CIContextData_003E);
																						try
																						{
																							int num2 = *(int*)m_pService + 60;
																							((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, ushort*, global::EDownloadContextEvent, ushort*, IContextData**, int>)(int)(*(uint*)num2))((nint)m_pService, ptr8, eDownloadContextEvent, ptr9, global::_003CModule_003E.CComPtrNtv_003CIContextData_003E_002E_0026(&cComPtrNtv_003CIContextData_003E));
																							int num3 = *(int*)ptr4 + 20;
																							num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, _GUID*, global::EContentType, ushort*, ushort*, ushort*, IContextData*, int>)(int)(*(uint*)num3))((nint)ptr4, &gUID, eContentType, ptr5, ptr6, ptr7, global::_003CModule_003E.CComPtrNtv_003CIContextData_003E_002E_002EPAUIContextData_0040_0040(&cComPtrNtv_003CIContextData_003E));
																						}
																						catch
																						{
																							//try-fault
																							global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<CComPtrNtv_003CIContextData_003E*, void>)(&global::_003CModule_003E.CComPtrNtv_003CIContextData_003E_002E_007Bdtor_007D), &cComPtrNtv_003CIContextData_003E);
																							throw;
																						}
																						global::_003CModule_003E.CComPtrNtv_003CIContextData_003E_002E_007Bdtor_007D(&cComPtrNtv_003CIContextData_003E);
																					}
																				}
																				else
																				{
																					global::_003CModule_003E.CComPtrNtv_003CIContextData_003E_002E_007Bctor_007D(&cComPtrNtv_003CIContextData_003E);
																					try
																					{
																						int num2 = *(int*)m_pService + 60;
																						((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, ushort*, global::EDownloadContextEvent, ushort*, IContextData**, int>)(int)(*(uint*)num2))((nint)m_pService, ptr8, eDownloadContextEvent, (ushort*)Unsafe.AsPointer(ref reference3), global::_003CModule_003E.CComPtrNtv_003CIContextData_003E_002E_0026(&cComPtrNtv_003CIContextData_003E));
																						int num3 = *(int*)ptr4 + 20;
																						num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, _GUID*, global::EContentType, ushort*, ushort*, ushort*, IContextData*, int>)(int)(*(uint*)num3))((nint)ptr4, &gUID, eContentType, ptr5, ptr6, ptr7, global::_003CModule_003E.CComPtrNtv_003CIContextData_003E_002E_002EPAUIContextData_0040_0040(&cComPtrNtv_003CIContextData_003E));
																					}
																					catch
																					{
																						//try-fault
																						global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<CComPtrNtv_003CIContextData_003E*, void>)(&global::_003CModule_003E.CComPtrNtv_003CIContextData_003E_002E_007Bdtor_007D), &cComPtrNtv_003CIContextData_003E);
																						throw;
																					}
																					global::_003CModule_003E.CComPtrNtv_003CIContextData_003E_002E_007Bdtor_007D(&cComPtrNtv_003CIContextData_003E);
																				}
																			}
																			catch
																			{
																				//try-fault
																				reference3 = ref *(ushort*)null;
																				throw;
																			}
																			reference3 = ref *(ushort*)null;
																		}
																	}
																	else
																	{
																		global::EDownloadContextEvent eDownloadContextEvent = (global::EDownloadContextEvent)(-1);
																		reference3 = ref *(ushort*)null;
																		try
																		{
																			if (clientContextEvent != EDownloadContextEvent.Unknown && !string.IsNullOrEmpty(clientContextEventData))
																			{
																				eDownloadContextEvent = (global::EDownloadContextEvent)clientContextEvent;
																				fixed (ushort* ptr9 = &Unsafe.As<char, ushort>(ref global::_003CModule_003E.PtrToStringChars(clientContextEventData)))
																				{
																					global::_003CModule_003E.CComPtrNtv_003CIContextData_003E_002E_007Bctor_007D(&cComPtrNtv_003CIContextData_003E);
																					try
																					{
																						int num2 = *(int*)m_pService + 60;
																						((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, ushort*, global::EDownloadContextEvent, ushort*, IContextData**, int>)(int)(*(uint*)num2))((nint)m_pService, (ushort*)Unsafe.AsPointer(ref reference2), eDownloadContextEvent, ptr9, global::_003CModule_003E.CComPtrNtv_003CIContextData_003E_002E_0026(&cComPtrNtv_003CIContextData_003E));
																						int num3 = *(int*)ptr4 + 20;
																						num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, _GUID*, global::EContentType, ushort*, ushort*, ushort*, IContextData*, int>)(int)(*(uint*)num3))((nint)ptr4, &gUID, eContentType, ptr5, ptr6, ptr7, global::_003CModule_003E.CComPtrNtv_003CIContextData_003E_002E_002EPAUIContextData_0040_0040(&cComPtrNtv_003CIContextData_003E));
																					}
																					catch
																					{
																						//try-fault
																						global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<CComPtrNtv_003CIContextData_003E*, void>)(&global::_003CModule_003E.CComPtrNtv_003CIContextData_003E_002E_007Bdtor_007D), &cComPtrNtv_003CIContextData_003E);
																						throw;
																					}
																					global::_003CModule_003E.CComPtrNtv_003CIContextData_003E_002E_007Bdtor_007D(&cComPtrNtv_003CIContextData_003E);
																				}
																			}
																			else
																			{
																				global::_003CModule_003E.CComPtrNtv_003CIContextData_003E_002E_007Bctor_007D(&cComPtrNtv_003CIContextData_003E);
																				try
																				{
																					int num2 = *(int*)m_pService + 60;
																					((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, ushort*, global::EDownloadContextEvent, ushort*, IContextData**, int>)(int)(*(uint*)num2))((nint)m_pService, (ushort*)Unsafe.AsPointer(ref reference2), eDownloadContextEvent, (ushort*)Unsafe.AsPointer(ref reference3), global::_003CModule_003E.CComPtrNtv_003CIContextData_003E_002E_0026(&cComPtrNtv_003CIContextData_003E));
																					int num3 = *(int*)ptr4 + 20;
																					num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, _GUID*, global::EContentType, ushort*, ushort*, ushort*, IContextData*, int>)(int)(*(uint*)num3))((nint)ptr4, &gUID, eContentType, ptr5, ptr6, ptr7, global::_003CModule_003E.CComPtrNtv_003CIContextData_003E_002E_002EPAUIContextData_0040_0040(&cComPtrNtv_003CIContextData_003E));
																				}
																				catch
																				{
																					//try-fault
																					global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<CComPtrNtv_003CIContextData_003E*, void>)(&global::_003CModule_003E.CComPtrNtv_003CIContextData_003E_002E_007Bdtor_007D), &cComPtrNtv_003CIContextData_003E);
																					throw;
																				}
																				global::_003CModule_003E.CComPtrNtv_003CIContextData_003E_002E_007Bdtor_007D(&cComPtrNtv_003CIContextData_003E);
																			}
																		}
																		catch
																		{
																			//try-fault
																			reference3 = ref *(ushort*)null;
																			throw;
																		}
																		reference3 = ref *(ushort*)null;
																	}
																}
																catch
																{
																	//try-fault
																	reference2 = ref *(ushort*)null;
																	throw;
																}
																reference2 = ref *(ushort*)null;
															}
															continue;
														}
														reference2 = ref *(ushort*)null;
														try
														{
															if (!string.IsNullOrEmpty(text))
															{
																fixed (ushort* ptr8 = &Unsafe.As<char, ushort>(ref global::_003CModule_003E.PtrToStringChars(text)))
																{
																	global::EDownloadContextEvent eDownloadContextEvent = (global::EDownloadContextEvent)(-1);
																	reference3 = ref *(ushort*)null;
																	try
																	{
																		if (clientContextEvent != EDownloadContextEvent.Unknown && !string.IsNullOrEmpty(clientContextEventData))
																		{
																			eDownloadContextEvent = (global::EDownloadContextEvent)clientContextEvent;
																			fixed (ushort* ptr9 = &Unsafe.As<char, ushort>(ref global::_003CModule_003E.PtrToStringChars(clientContextEventData)))
																			{
																				global::_003CModule_003E.CComPtrNtv_003CIContextData_003E_002E_007Bctor_007D(&cComPtrNtv_003CIContextData_003E);
																				try
																				{
																					int num2 = *(int*)m_pService + 60;
																					((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, ushort*, global::EDownloadContextEvent, ushort*, IContextData**, int>)(int)(*(uint*)num2))((nint)m_pService, ptr8, eDownloadContextEvent, ptr9, global::_003CModule_003E.CComPtrNtv_003CIContextData_003E_002E_0026(&cComPtrNtv_003CIContextData_003E));
																					int num3 = *(int*)ptr4 + 20;
																					num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, _GUID*, global::EContentType, ushort*, ushort*, ushort*, IContextData*, int>)(int)(*(uint*)num3))((nint)ptr4, &gUID, eContentType, ptr5, ptr6, ptr7, global::_003CModule_003E.CComPtrNtv_003CIContextData_003E_002E_002EPAUIContextData_0040_0040(&cComPtrNtv_003CIContextData_003E));
																				}
																				catch
																				{
																					//try-fault
																					global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<CComPtrNtv_003CIContextData_003E*, void>)(&global::_003CModule_003E.CComPtrNtv_003CIContextData_003E_002E_007Bdtor_007D), &cComPtrNtv_003CIContextData_003E);
																					throw;
																				}
																				global::_003CModule_003E.CComPtrNtv_003CIContextData_003E_002E_007Bdtor_007D(&cComPtrNtv_003CIContextData_003E);
																			}
																		}
																		else
																		{
																			global::_003CModule_003E.CComPtrNtv_003CIContextData_003E_002E_007Bctor_007D(&cComPtrNtv_003CIContextData_003E);
																			try
																			{
																				int num2 = *(int*)m_pService + 60;
																				((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, ushort*, global::EDownloadContextEvent, ushort*, IContextData**, int>)(int)(*(uint*)num2))((nint)m_pService, ptr8, eDownloadContextEvent, (ushort*)Unsafe.AsPointer(ref reference3), global::_003CModule_003E.CComPtrNtv_003CIContextData_003E_002E_0026(&cComPtrNtv_003CIContextData_003E));
																				int num3 = *(int*)ptr4 + 20;
																				num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, _GUID*, global::EContentType, ushort*, ushort*, ushort*, IContextData*, int>)(int)(*(uint*)num3))((nint)ptr4, &gUID, eContentType, ptr5, ptr6, ptr7, global::_003CModule_003E.CComPtrNtv_003CIContextData_003E_002E_002EPAUIContextData_0040_0040(&cComPtrNtv_003CIContextData_003E));
																			}
																			catch
																			{
																				//try-fault
																				global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<CComPtrNtv_003CIContextData_003E*, void>)(&global::_003CModule_003E.CComPtrNtv_003CIContextData_003E_002E_007Bdtor_007D), &cComPtrNtv_003CIContextData_003E);
																				throw;
																			}
																			global::_003CModule_003E.CComPtrNtv_003CIContextData_003E_002E_007Bdtor_007D(&cComPtrNtv_003CIContextData_003E);
																		}
																	}
																	catch
																	{
																		//try-fault
																		reference3 = ref *(ushort*)null;
																		throw;
																	}
																	reference3 = ref *(ushort*)null;
																}
															}
															else
															{
																global::EDownloadContextEvent eDownloadContextEvent = (global::EDownloadContextEvent)(-1);
																reference3 = ref *(ushort*)null;
																try
																{
																	if (clientContextEvent != EDownloadContextEvent.Unknown && !string.IsNullOrEmpty(clientContextEventData))
																	{
																		eDownloadContextEvent = (global::EDownloadContextEvent)clientContextEvent;
																		fixed (ushort* ptr9 = &Unsafe.As<char, ushort>(ref global::_003CModule_003E.PtrToStringChars(clientContextEventData)))
																		{
																			global::_003CModule_003E.CComPtrNtv_003CIContextData_003E_002E_007Bctor_007D(&cComPtrNtv_003CIContextData_003E);
																			try
																			{
																				int num2 = *(int*)m_pService + 60;
																				((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, ushort*, global::EDownloadContextEvent, ushort*, IContextData**, int>)(int)(*(uint*)num2))((nint)m_pService, (ushort*)Unsafe.AsPointer(ref reference2), eDownloadContextEvent, ptr9, global::_003CModule_003E.CComPtrNtv_003CIContextData_003E_002E_0026(&cComPtrNtv_003CIContextData_003E));
																				int num3 = *(int*)ptr4 + 20;
																				num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, _GUID*, global::EContentType, ushort*, ushort*, ushort*, IContextData*, int>)(int)(*(uint*)num3))((nint)ptr4, &gUID, eContentType, ptr5, ptr6, ptr7, global::_003CModule_003E.CComPtrNtv_003CIContextData_003E_002E_002EPAUIContextData_0040_0040(&cComPtrNtv_003CIContextData_003E));
																			}
																			catch
																			{
																				//try-fault
																				global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<CComPtrNtv_003CIContextData_003E*, void>)(&global::_003CModule_003E.CComPtrNtv_003CIContextData_003E_002E_007Bdtor_007D), &cComPtrNtv_003CIContextData_003E);
																				throw;
																			}
																			global::_003CModule_003E.CComPtrNtv_003CIContextData_003E_002E_007Bdtor_007D(&cComPtrNtv_003CIContextData_003E);
																		}
																	}
																	else
																	{
																		global::_003CModule_003E.CComPtrNtv_003CIContextData_003E_002E_007Bctor_007D(&cComPtrNtv_003CIContextData_003E);
																		try
																		{
																			int num2 = *(int*)m_pService + 60;
																			((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, ushort*, global::EDownloadContextEvent, ushort*, IContextData**, int>)(int)(*(uint*)num2))((nint)m_pService, (ushort*)Unsafe.AsPointer(ref reference2), eDownloadContextEvent, (ushort*)Unsafe.AsPointer(ref reference3), global::_003CModule_003E.CComPtrNtv_003CIContextData_003E_002E_0026(&cComPtrNtv_003CIContextData_003E));
																			int num3 = *(int*)ptr4 + 20;
																			num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, _GUID*, global::EContentType, ushort*, ushort*, ushort*, IContextData*, int>)(int)(*(uint*)num3))((nint)ptr4, &gUID, eContentType, ptr5, ptr6, ptr7, global::_003CModule_003E.CComPtrNtv_003CIContextData_003E_002E_002EPAUIContextData_0040_0040(&cComPtrNtv_003CIContextData_003E));
																		}
																		catch
																		{
																			//try-fault
																			global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<CComPtrNtv_003CIContextData_003E*, void>)(&global::_003CModule_003E.CComPtrNtv_003CIContextData_003E_002E_007Bdtor_007D), &cComPtrNtv_003CIContextData_003E);
																			throw;
																		}
																		global::_003CModule_003E.CComPtrNtv_003CIContextData_003E_002E_007Bdtor_007D(&cComPtrNtv_003CIContextData_003E);
																	}
																}
																catch
																{
																	//try-fault
																	reference3 = ref *(ushort*)null;
																	throw;
																}
																reference3 = ref *(ushort*)null;
															}
														}
														catch
														{
															//try-fault
															reference2 = ref *(ushort*)null;
															throw;
														}
														reference2 = ref *(ushort*)null;
													}
													catch
													{
														//try-fault
														ptr7 = null;
														throw;
													}
												}
											}
											catch
											{
												//try-fault
												ptr6 = null;
												throw;
											}
										}
									}
									continue;
								}
								fixed (ushort* ptr6 = &Unsafe.As<char, ushort>(ref global::_003CModule_003E.PtrToStringChars(s2)))
								{
									try
									{
										if (ptr6 == null)
										{
											fixed (ushort* ptr6 = &Unsafe.As<_0024ArrayType_0024_0024_0024BY00_0024_0024CBG, ushort>(ref global::_003CModule_003E._003F_003F_C_0040_11LOCGONAA_0040_003F_0024AA_003F_0024AA_0040))
											{
												fixed (ushort* ptr7 = &Unsafe.As<char, ushort>(ref global::_003CModule_003E.PtrToStringChars(s3)))
												{
													try
													{
														if (ptr7 == null)
														{
															fixed (ushort* ptr7 = &Unsafe.As<_0024ArrayType_0024_0024_0024BY00_0024_0024CBG, ushort>(ref global::_003CModule_003E._003F_003F_C_0040_11LOCGONAA_0040_003F_0024AA_003F_0024AA_0040))
															{
																reference2 = ref *(ushort*)null;
																try
																{
																	if (!string.IsNullOrEmpty(text))
																	{
																		fixed (ushort* ptr8 = &Unsafe.As<char, ushort>(ref global::_003CModule_003E.PtrToStringChars(text)))
																		{
																			global::EDownloadContextEvent eDownloadContextEvent = (global::EDownloadContextEvent)(-1);
																			reference3 = ref *(ushort*)null;
																			try
																			{
																				if (clientContextEvent != EDownloadContextEvent.Unknown && !string.IsNullOrEmpty(clientContextEventData))
																				{
																					eDownloadContextEvent = (global::EDownloadContextEvent)clientContextEvent;
																					fixed (ushort* ptr9 = &Unsafe.As<char, ushort>(ref global::_003CModule_003E.PtrToStringChars(clientContextEventData)))
																					{
																						global::_003CModule_003E.CComPtrNtv_003CIContextData_003E_002E_007Bctor_007D(&cComPtrNtv_003CIContextData_003E);
																						try
																						{
																							int num2 = *(int*)m_pService + 60;
																							((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, ushort*, global::EDownloadContextEvent, ushort*, IContextData**, int>)(int)(*(uint*)num2))((nint)m_pService, ptr8, eDownloadContextEvent, ptr9, global::_003CModule_003E.CComPtrNtv_003CIContextData_003E_002E_0026(&cComPtrNtv_003CIContextData_003E));
																							int num3 = *(int*)ptr4 + 20;
																							num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, _GUID*, global::EContentType, ushort*, ushort*, ushort*, IContextData*, int>)(int)(*(uint*)num3))((nint)ptr4, &gUID, eContentType, ptr5, ptr6, ptr7, global::_003CModule_003E.CComPtrNtv_003CIContextData_003E_002E_002EPAUIContextData_0040_0040(&cComPtrNtv_003CIContextData_003E));
																						}
																						catch
																						{
																							//try-fault
																							global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<CComPtrNtv_003CIContextData_003E*, void>)(&global::_003CModule_003E.CComPtrNtv_003CIContextData_003E_002E_007Bdtor_007D), &cComPtrNtv_003CIContextData_003E);
																							throw;
																						}
																						global::_003CModule_003E.CComPtrNtv_003CIContextData_003E_002E_007Bdtor_007D(&cComPtrNtv_003CIContextData_003E);
																					}
																				}
																				else
																				{
																					global::_003CModule_003E.CComPtrNtv_003CIContextData_003E_002E_007Bctor_007D(&cComPtrNtv_003CIContextData_003E);
																					try
																					{
																						int num2 = *(int*)m_pService + 60;
																						((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, ushort*, global::EDownloadContextEvent, ushort*, IContextData**, int>)(int)(*(uint*)num2))((nint)m_pService, ptr8, eDownloadContextEvent, (ushort*)Unsafe.AsPointer(ref reference3), global::_003CModule_003E.CComPtrNtv_003CIContextData_003E_002E_0026(&cComPtrNtv_003CIContextData_003E));
																						int num3 = *(int*)ptr4 + 20;
																						num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, _GUID*, global::EContentType, ushort*, ushort*, ushort*, IContextData*, int>)(int)(*(uint*)num3))((nint)ptr4, &gUID, eContentType, ptr5, ptr6, ptr7, global::_003CModule_003E.CComPtrNtv_003CIContextData_003E_002E_002EPAUIContextData_0040_0040(&cComPtrNtv_003CIContextData_003E));
																					}
																					catch
																					{
																						//try-fault
																						global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<CComPtrNtv_003CIContextData_003E*, void>)(&global::_003CModule_003E.CComPtrNtv_003CIContextData_003E_002E_007Bdtor_007D), &cComPtrNtv_003CIContextData_003E);
																						throw;
																					}
																					global::_003CModule_003E.CComPtrNtv_003CIContextData_003E_002E_007Bdtor_007D(&cComPtrNtv_003CIContextData_003E);
																				}
																			}
																			catch
																			{
																				//try-fault
																				reference3 = ref *(ushort*)null;
																				throw;
																			}
																			reference3 = ref *(ushort*)null;
																		}
																	}
																	else
																	{
																		global::EDownloadContextEvent eDownloadContextEvent = (global::EDownloadContextEvent)(-1);
																		reference3 = ref *(ushort*)null;
																		try
																		{
																			if (clientContextEvent != EDownloadContextEvent.Unknown && !string.IsNullOrEmpty(clientContextEventData))
																			{
																				eDownloadContextEvent = (global::EDownloadContextEvent)clientContextEvent;
																				fixed (ushort* ptr9 = &Unsafe.As<char, ushort>(ref global::_003CModule_003E.PtrToStringChars(clientContextEventData)))
																				{
																					global::_003CModule_003E.CComPtrNtv_003CIContextData_003E_002E_007Bctor_007D(&cComPtrNtv_003CIContextData_003E);
																					try
																					{
																						int num2 = *(int*)m_pService + 60;
																						((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, ushort*, global::EDownloadContextEvent, ushort*, IContextData**, int>)(int)(*(uint*)num2))((nint)m_pService, (ushort*)Unsafe.AsPointer(ref reference2), eDownloadContextEvent, ptr9, global::_003CModule_003E.CComPtrNtv_003CIContextData_003E_002E_0026(&cComPtrNtv_003CIContextData_003E));
																						int num3 = *(int*)ptr4 + 20;
																						num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, _GUID*, global::EContentType, ushort*, ushort*, ushort*, IContextData*, int>)(int)(*(uint*)num3))((nint)ptr4, &gUID, eContentType, ptr5, ptr6, ptr7, global::_003CModule_003E.CComPtrNtv_003CIContextData_003E_002E_002EPAUIContextData_0040_0040(&cComPtrNtv_003CIContextData_003E));
																					}
																					catch
																					{
																						//try-fault
																						global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<CComPtrNtv_003CIContextData_003E*, void>)(&global::_003CModule_003E.CComPtrNtv_003CIContextData_003E_002E_007Bdtor_007D), &cComPtrNtv_003CIContextData_003E);
																						throw;
																					}
																					global::_003CModule_003E.CComPtrNtv_003CIContextData_003E_002E_007Bdtor_007D(&cComPtrNtv_003CIContextData_003E);
																				}
																			}
																			else
																			{
																				global::_003CModule_003E.CComPtrNtv_003CIContextData_003E_002E_007Bctor_007D(&cComPtrNtv_003CIContextData_003E);
																				try
																				{
																					int num2 = *(int*)m_pService + 60;
																					((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, ushort*, global::EDownloadContextEvent, ushort*, IContextData**, int>)(int)(*(uint*)num2))((nint)m_pService, (ushort*)Unsafe.AsPointer(ref reference2), eDownloadContextEvent, (ushort*)Unsafe.AsPointer(ref reference3), global::_003CModule_003E.CComPtrNtv_003CIContextData_003E_002E_0026(&cComPtrNtv_003CIContextData_003E));
																					int num3 = *(int*)ptr4 + 20;
																					num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, _GUID*, global::EContentType, ushort*, ushort*, ushort*, IContextData*, int>)(int)(*(uint*)num3))((nint)ptr4, &gUID, eContentType, ptr5, ptr6, ptr7, global::_003CModule_003E.CComPtrNtv_003CIContextData_003E_002E_002EPAUIContextData_0040_0040(&cComPtrNtv_003CIContextData_003E));
																				}
																				catch
																				{
																					//try-fault
																					global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<CComPtrNtv_003CIContextData_003E*, void>)(&global::_003CModule_003E.CComPtrNtv_003CIContextData_003E_002E_007Bdtor_007D), &cComPtrNtv_003CIContextData_003E);
																					throw;
																				}
																				global::_003CModule_003E.CComPtrNtv_003CIContextData_003E_002E_007Bdtor_007D(&cComPtrNtv_003CIContextData_003E);
																			}
																		}
																		catch
																		{
																			//try-fault
																			reference3 = ref *(ushort*)null;
																			throw;
																		}
																		reference3 = ref *(ushort*)null;
																	}
																}
																catch
																{
																	//try-fault
																	reference2 = ref *(ushort*)null;
																	throw;
																}
																reference2 = ref *(ushort*)null;
															}
															continue;
														}
														reference2 = ref *(ushort*)null;
														try
														{
															if (!string.IsNullOrEmpty(text))
															{
																fixed (ushort* ptr8 = &Unsafe.As<char, ushort>(ref global::_003CModule_003E.PtrToStringChars(text)))
																{
																	global::EDownloadContextEvent eDownloadContextEvent = (global::EDownloadContextEvent)(-1);
																	reference3 = ref *(ushort*)null;
																	try
																	{
																		if (clientContextEvent != EDownloadContextEvent.Unknown && !string.IsNullOrEmpty(clientContextEventData))
																		{
																			eDownloadContextEvent = (global::EDownloadContextEvent)clientContextEvent;
																			fixed (ushort* ptr9 = &Unsafe.As<char, ushort>(ref global::_003CModule_003E.PtrToStringChars(clientContextEventData)))
																			{
																				global::_003CModule_003E.CComPtrNtv_003CIContextData_003E_002E_007Bctor_007D(&cComPtrNtv_003CIContextData_003E);
																				try
																				{
																					int num2 = *(int*)m_pService + 60;
																					((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, ushort*, global::EDownloadContextEvent, ushort*, IContextData**, int>)(int)(*(uint*)num2))((nint)m_pService, ptr8, eDownloadContextEvent, ptr9, global::_003CModule_003E.CComPtrNtv_003CIContextData_003E_002E_0026(&cComPtrNtv_003CIContextData_003E));
																					int num3 = *(int*)ptr4 + 20;
																					num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, _GUID*, global::EContentType, ushort*, ushort*, ushort*, IContextData*, int>)(int)(*(uint*)num3))((nint)ptr4, &gUID, eContentType, ptr5, ptr6, ptr7, global::_003CModule_003E.CComPtrNtv_003CIContextData_003E_002E_002EPAUIContextData_0040_0040(&cComPtrNtv_003CIContextData_003E));
																				}
																				catch
																				{
																					//try-fault
																					global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<CComPtrNtv_003CIContextData_003E*, void>)(&global::_003CModule_003E.CComPtrNtv_003CIContextData_003E_002E_007Bdtor_007D), &cComPtrNtv_003CIContextData_003E);
																					throw;
																				}
																				global::_003CModule_003E.CComPtrNtv_003CIContextData_003E_002E_007Bdtor_007D(&cComPtrNtv_003CIContextData_003E);
																			}
																		}
																		else
																		{
																			global::_003CModule_003E.CComPtrNtv_003CIContextData_003E_002E_007Bctor_007D(&cComPtrNtv_003CIContextData_003E);
																			try
																			{
																				int num2 = *(int*)m_pService + 60;
																				((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, ushort*, global::EDownloadContextEvent, ushort*, IContextData**, int>)(int)(*(uint*)num2))((nint)m_pService, ptr8, eDownloadContextEvent, (ushort*)Unsafe.AsPointer(ref reference3), global::_003CModule_003E.CComPtrNtv_003CIContextData_003E_002E_0026(&cComPtrNtv_003CIContextData_003E));
																				int num3 = *(int*)ptr4 + 20;
																				num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, _GUID*, global::EContentType, ushort*, ushort*, ushort*, IContextData*, int>)(int)(*(uint*)num3))((nint)ptr4, &gUID, eContentType, ptr5, ptr6, ptr7, global::_003CModule_003E.CComPtrNtv_003CIContextData_003E_002E_002EPAUIContextData_0040_0040(&cComPtrNtv_003CIContextData_003E));
																			}
																			catch
																			{
																				//try-fault
																				global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<CComPtrNtv_003CIContextData_003E*, void>)(&global::_003CModule_003E.CComPtrNtv_003CIContextData_003E_002E_007Bdtor_007D), &cComPtrNtv_003CIContextData_003E);
																				throw;
																			}
																			global::_003CModule_003E.CComPtrNtv_003CIContextData_003E_002E_007Bdtor_007D(&cComPtrNtv_003CIContextData_003E);
																		}
																	}
																	catch
																	{
																		//try-fault
																		reference3 = ref *(ushort*)null;
																		throw;
																	}
																	reference3 = ref *(ushort*)null;
																}
															}
															else
															{
																global::EDownloadContextEvent eDownloadContextEvent = (global::EDownloadContextEvent)(-1);
																reference3 = ref *(ushort*)null;
																try
																{
																	if (clientContextEvent != EDownloadContextEvent.Unknown && !string.IsNullOrEmpty(clientContextEventData))
																	{
																		eDownloadContextEvent = (global::EDownloadContextEvent)clientContextEvent;
																		fixed (ushort* ptr9 = &Unsafe.As<char, ushort>(ref global::_003CModule_003E.PtrToStringChars(clientContextEventData)))
																		{
																			global::_003CModule_003E.CComPtrNtv_003CIContextData_003E_002E_007Bctor_007D(&cComPtrNtv_003CIContextData_003E);
																			try
																			{
																				int num2 = *(int*)m_pService + 60;
																				((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, ushort*, global::EDownloadContextEvent, ushort*, IContextData**, int>)(int)(*(uint*)num2))((nint)m_pService, (ushort*)Unsafe.AsPointer(ref reference2), eDownloadContextEvent, ptr9, global::_003CModule_003E.CComPtrNtv_003CIContextData_003E_002E_0026(&cComPtrNtv_003CIContextData_003E));
																				int num3 = *(int*)ptr4 + 20;
																				num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, _GUID*, global::EContentType, ushort*, ushort*, ushort*, IContextData*, int>)(int)(*(uint*)num3))((nint)ptr4, &gUID, eContentType, ptr5, ptr6, ptr7, global::_003CModule_003E.CComPtrNtv_003CIContextData_003E_002E_002EPAUIContextData_0040_0040(&cComPtrNtv_003CIContextData_003E));
																			}
																			catch
																			{
																				//try-fault
																				global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<CComPtrNtv_003CIContextData_003E*, void>)(&global::_003CModule_003E.CComPtrNtv_003CIContextData_003E_002E_007Bdtor_007D), &cComPtrNtv_003CIContextData_003E);
																				throw;
																			}
																			global::_003CModule_003E.CComPtrNtv_003CIContextData_003E_002E_007Bdtor_007D(&cComPtrNtv_003CIContextData_003E);
																		}
																	}
																	else
																	{
																		global::_003CModule_003E.CComPtrNtv_003CIContextData_003E_002E_007Bctor_007D(&cComPtrNtv_003CIContextData_003E);
																		try
																		{
																			int num2 = *(int*)m_pService + 60;
																			((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, ushort*, global::EDownloadContextEvent, ushort*, IContextData**, int>)(int)(*(uint*)num2))((nint)m_pService, (ushort*)Unsafe.AsPointer(ref reference2), eDownloadContextEvent, (ushort*)Unsafe.AsPointer(ref reference3), global::_003CModule_003E.CComPtrNtv_003CIContextData_003E_002E_0026(&cComPtrNtv_003CIContextData_003E));
																			int num3 = *(int*)ptr4 + 20;
																			num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, _GUID*, global::EContentType, ushort*, ushort*, ushort*, IContextData*, int>)(int)(*(uint*)num3))((nint)ptr4, &gUID, eContentType, ptr5, ptr6, ptr7, global::_003CModule_003E.CComPtrNtv_003CIContextData_003E_002E_002EPAUIContextData_0040_0040(&cComPtrNtv_003CIContextData_003E));
																		}
																		catch
																		{
																			//try-fault
																			global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<CComPtrNtv_003CIContextData_003E*, void>)(&global::_003CModule_003E.CComPtrNtv_003CIContextData_003E_002E_007Bdtor_007D), &cComPtrNtv_003CIContextData_003E);
																			throw;
																		}
																		global::_003CModule_003E.CComPtrNtv_003CIContextData_003E_002E_007Bdtor_007D(&cComPtrNtv_003CIContextData_003E);
																	}
																}
																catch
																{
																	//try-fault
																	reference3 = ref *(ushort*)null;
																	throw;
																}
																reference3 = ref *(ushort*)null;
															}
														}
														catch
														{
															//try-fault
															reference2 = ref *(ushort*)null;
															throw;
														}
														reference2 = ref *(ushort*)null;
													}
													catch
													{
														//try-fault
														ptr7 = null;
														throw;
													}
												}
											}
											continue;
										}
										fixed (ushort* ptr7 = &Unsafe.As<char, ushort>(ref global::_003CModule_003E.PtrToStringChars(s3)))
										{
											try
											{
												if (ptr7 == null)
												{
													fixed (ushort* ptr7 = &Unsafe.As<_0024ArrayType_0024_0024_0024BY00_0024_0024CBG, ushort>(ref global::_003CModule_003E._003F_003F_C_0040_11LOCGONAA_0040_003F_0024AA_003F_0024AA_0040))
													{
														reference2 = ref *(ushort*)null;
														try
														{
															if (!string.IsNullOrEmpty(text))
															{
																fixed (ushort* ptr8 = &Unsafe.As<char, ushort>(ref global::_003CModule_003E.PtrToStringChars(text)))
																{
																	global::EDownloadContextEvent eDownloadContextEvent = (global::EDownloadContextEvent)(-1);
																	reference3 = ref *(ushort*)null;
																	try
																	{
																		if (clientContextEvent != EDownloadContextEvent.Unknown && !string.IsNullOrEmpty(clientContextEventData))
																		{
																			eDownloadContextEvent = (global::EDownloadContextEvent)clientContextEvent;
																			fixed (ushort* ptr9 = &Unsafe.As<char, ushort>(ref global::_003CModule_003E.PtrToStringChars(clientContextEventData)))
																			{
																				global::_003CModule_003E.CComPtrNtv_003CIContextData_003E_002E_007Bctor_007D(&cComPtrNtv_003CIContextData_003E);
																				try
																				{
																					int num2 = *(int*)m_pService + 60;
																					((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, ushort*, global::EDownloadContextEvent, ushort*, IContextData**, int>)(int)(*(uint*)num2))((nint)m_pService, ptr8, eDownloadContextEvent, ptr9, global::_003CModule_003E.CComPtrNtv_003CIContextData_003E_002E_0026(&cComPtrNtv_003CIContextData_003E));
																					int num3 = *(int*)ptr4 + 20;
																					num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, _GUID*, global::EContentType, ushort*, ushort*, ushort*, IContextData*, int>)(int)(*(uint*)num3))((nint)ptr4, &gUID, eContentType, ptr5, ptr6, ptr7, global::_003CModule_003E.CComPtrNtv_003CIContextData_003E_002E_002EPAUIContextData_0040_0040(&cComPtrNtv_003CIContextData_003E));
																				}
																				catch
																				{
																					//try-fault
																					global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<CComPtrNtv_003CIContextData_003E*, void>)(&global::_003CModule_003E.CComPtrNtv_003CIContextData_003E_002E_007Bdtor_007D), &cComPtrNtv_003CIContextData_003E);
																					throw;
																				}
																				global::_003CModule_003E.CComPtrNtv_003CIContextData_003E_002E_007Bdtor_007D(&cComPtrNtv_003CIContextData_003E);
																			}
																		}
																		else
																		{
																			global::_003CModule_003E.CComPtrNtv_003CIContextData_003E_002E_007Bctor_007D(&cComPtrNtv_003CIContextData_003E);
																			try
																			{
																				int num2 = *(int*)m_pService + 60;
																				((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, ushort*, global::EDownloadContextEvent, ushort*, IContextData**, int>)(int)(*(uint*)num2))((nint)m_pService, ptr8, eDownloadContextEvent, (ushort*)Unsafe.AsPointer(ref reference3), global::_003CModule_003E.CComPtrNtv_003CIContextData_003E_002E_0026(&cComPtrNtv_003CIContextData_003E));
																				int num3 = *(int*)ptr4 + 20;
																				num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, _GUID*, global::EContentType, ushort*, ushort*, ushort*, IContextData*, int>)(int)(*(uint*)num3))((nint)ptr4, &gUID, eContentType, ptr5, ptr6, ptr7, global::_003CModule_003E.CComPtrNtv_003CIContextData_003E_002E_002EPAUIContextData_0040_0040(&cComPtrNtv_003CIContextData_003E));
																			}
																			catch
																			{
																				//try-fault
																				global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<CComPtrNtv_003CIContextData_003E*, void>)(&global::_003CModule_003E.CComPtrNtv_003CIContextData_003E_002E_007Bdtor_007D), &cComPtrNtv_003CIContextData_003E);
																				throw;
																			}
																			global::_003CModule_003E.CComPtrNtv_003CIContextData_003E_002E_007Bdtor_007D(&cComPtrNtv_003CIContextData_003E);
																		}
																	}
																	catch
																	{
																		//try-fault
																		reference3 = ref *(ushort*)null;
																		throw;
																	}
																	reference3 = ref *(ushort*)null;
																}
															}
															else
															{
																global::EDownloadContextEvent eDownloadContextEvent = (global::EDownloadContextEvent)(-1);
																reference3 = ref *(ushort*)null;
																try
																{
																	if (clientContextEvent != EDownloadContextEvent.Unknown && !string.IsNullOrEmpty(clientContextEventData))
																	{
																		eDownloadContextEvent = (global::EDownloadContextEvent)clientContextEvent;
																		fixed (ushort* ptr9 = &Unsafe.As<char, ushort>(ref global::_003CModule_003E.PtrToStringChars(clientContextEventData)))
																		{
																			global::_003CModule_003E.CComPtrNtv_003CIContextData_003E_002E_007Bctor_007D(&cComPtrNtv_003CIContextData_003E);
																			try
																			{
																				int num2 = *(int*)m_pService + 60;
																				((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, ushort*, global::EDownloadContextEvent, ushort*, IContextData**, int>)(int)(*(uint*)num2))((nint)m_pService, (ushort*)Unsafe.AsPointer(ref reference2), eDownloadContextEvent, ptr9, global::_003CModule_003E.CComPtrNtv_003CIContextData_003E_002E_0026(&cComPtrNtv_003CIContextData_003E));
																				int num3 = *(int*)ptr4 + 20;
																				num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, _GUID*, global::EContentType, ushort*, ushort*, ushort*, IContextData*, int>)(int)(*(uint*)num3))((nint)ptr4, &gUID, eContentType, ptr5, ptr6, ptr7, global::_003CModule_003E.CComPtrNtv_003CIContextData_003E_002E_002EPAUIContextData_0040_0040(&cComPtrNtv_003CIContextData_003E));
																			}
																			catch
																			{
																				//try-fault
																				global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<CComPtrNtv_003CIContextData_003E*, void>)(&global::_003CModule_003E.CComPtrNtv_003CIContextData_003E_002E_007Bdtor_007D), &cComPtrNtv_003CIContextData_003E);
																				throw;
																			}
																			global::_003CModule_003E.CComPtrNtv_003CIContextData_003E_002E_007Bdtor_007D(&cComPtrNtv_003CIContextData_003E);
																		}
																	}
																	else
																	{
																		global::_003CModule_003E.CComPtrNtv_003CIContextData_003E_002E_007Bctor_007D(&cComPtrNtv_003CIContextData_003E);
																		try
																		{
																			int num2 = *(int*)m_pService + 60;
																			((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, ushort*, global::EDownloadContextEvent, ushort*, IContextData**, int>)(int)(*(uint*)num2))((nint)m_pService, (ushort*)Unsafe.AsPointer(ref reference2), eDownloadContextEvent, (ushort*)Unsafe.AsPointer(ref reference3), global::_003CModule_003E.CComPtrNtv_003CIContextData_003E_002E_0026(&cComPtrNtv_003CIContextData_003E));
																			int num3 = *(int*)ptr4 + 20;
																			num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, _GUID*, global::EContentType, ushort*, ushort*, ushort*, IContextData*, int>)(int)(*(uint*)num3))((nint)ptr4, &gUID, eContentType, ptr5, ptr6, ptr7, global::_003CModule_003E.CComPtrNtv_003CIContextData_003E_002E_002EPAUIContextData_0040_0040(&cComPtrNtv_003CIContextData_003E));
																		}
																		catch
																		{
																			//try-fault
																			global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<CComPtrNtv_003CIContextData_003E*, void>)(&global::_003CModule_003E.CComPtrNtv_003CIContextData_003E_002E_007Bdtor_007D), &cComPtrNtv_003CIContextData_003E);
																			throw;
																		}
																		global::_003CModule_003E.CComPtrNtv_003CIContextData_003E_002E_007Bdtor_007D(&cComPtrNtv_003CIContextData_003E);
																	}
																}
																catch
																{
																	//try-fault
																	reference3 = ref *(ushort*)null;
																	throw;
																}
																reference3 = ref *(ushort*)null;
															}
														}
														catch
														{
															//try-fault
															reference2 = ref *(ushort*)null;
															throw;
														}
														reference2 = ref *(ushort*)null;
													}
													continue;
												}
												reference2 = ref *(ushort*)null;
												try
												{
													if (!string.IsNullOrEmpty(text))
													{
														fixed (ushort* ptr8 = &Unsafe.As<char, ushort>(ref global::_003CModule_003E.PtrToStringChars(text)))
														{
															global::EDownloadContextEvent eDownloadContextEvent = (global::EDownloadContextEvent)(-1);
															reference3 = ref *(ushort*)null;
															try
															{
																if (clientContextEvent != EDownloadContextEvent.Unknown && !string.IsNullOrEmpty(clientContextEventData))
																{
																	eDownloadContextEvent = (global::EDownloadContextEvent)clientContextEvent;
																	fixed (ushort* ptr9 = &Unsafe.As<char, ushort>(ref global::_003CModule_003E.PtrToStringChars(clientContextEventData)))
																	{
																		global::_003CModule_003E.CComPtrNtv_003CIContextData_003E_002E_007Bctor_007D(&cComPtrNtv_003CIContextData_003E);
																		try
																		{
																			int num2 = *(int*)m_pService + 60;
																			((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, ushort*, global::EDownloadContextEvent, ushort*, IContextData**, int>)(int)(*(uint*)num2))((nint)m_pService, ptr8, eDownloadContextEvent, ptr9, global::_003CModule_003E.CComPtrNtv_003CIContextData_003E_002E_0026(&cComPtrNtv_003CIContextData_003E));
																			int num3 = *(int*)ptr4 + 20;
																			num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, _GUID*, global::EContentType, ushort*, ushort*, ushort*, IContextData*, int>)(int)(*(uint*)num3))((nint)ptr4, &gUID, eContentType, ptr5, ptr6, ptr7, global::_003CModule_003E.CComPtrNtv_003CIContextData_003E_002E_002EPAUIContextData_0040_0040(&cComPtrNtv_003CIContextData_003E));
																		}
																		catch
																		{
																			//try-fault
																			global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<CComPtrNtv_003CIContextData_003E*, void>)(&global::_003CModule_003E.CComPtrNtv_003CIContextData_003E_002E_007Bdtor_007D), &cComPtrNtv_003CIContextData_003E);
																			throw;
																		}
																		global::_003CModule_003E.CComPtrNtv_003CIContextData_003E_002E_007Bdtor_007D(&cComPtrNtv_003CIContextData_003E);
																	}
																}
																else
																{
																	global::_003CModule_003E.CComPtrNtv_003CIContextData_003E_002E_007Bctor_007D(&cComPtrNtv_003CIContextData_003E);
																	try
																	{
																		int num2 = *(int*)m_pService + 60;
																		((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, ushort*, global::EDownloadContextEvent, ushort*, IContextData**, int>)(int)(*(uint*)num2))((nint)m_pService, ptr8, eDownloadContextEvent, (ushort*)Unsafe.AsPointer(ref reference3), global::_003CModule_003E.CComPtrNtv_003CIContextData_003E_002E_0026(&cComPtrNtv_003CIContextData_003E));
																		int num3 = *(int*)ptr4 + 20;
																		num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, _GUID*, global::EContentType, ushort*, ushort*, ushort*, IContextData*, int>)(int)(*(uint*)num3))((nint)ptr4, &gUID, eContentType, ptr5, ptr6, ptr7, global::_003CModule_003E.CComPtrNtv_003CIContextData_003E_002E_002EPAUIContextData_0040_0040(&cComPtrNtv_003CIContextData_003E));
																	}
																	catch
																	{
																		//try-fault
																		global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<CComPtrNtv_003CIContextData_003E*, void>)(&global::_003CModule_003E.CComPtrNtv_003CIContextData_003E_002E_007Bdtor_007D), &cComPtrNtv_003CIContextData_003E);
																		throw;
																	}
																	global::_003CModule_003E.CComPtrNtv_003CIContextData_003E_002E_007Bdtor_007D(&cComPtrNtv_003CIContextData_003E);
																}
															}
															catch
															{
																//try-fault
																reference3 = ref *(ushort*)null;
																throw;
															}
															reference3 = ref *(ushort*)null;
														}
													}
													else
													{
														global::EDownloadContextEvent eDownloadContextEvent = (global::EDownloadContextEvent)(-1);
														reference3 = ref *(ushort*)null;
														try
														{
															if (clientContextEvent != EDownloadContextEvent.Unknown && !string.IsNullOrEmpty(clientContextEventData))
															{
																eDownloadContextEvent = (global::EDownloadContextEvent)clientContextEvent;
																fixed (ushort* ptr9 = &Unsafe.As<char, ushort>(ref global::_003CModule_003E.PtrToStringChars(clientContextEventData)))
																{
																	global::_003CModule_003E.CComPtrNtv_003CIContextData_003E_002E_007Bctor_007D(&cComPtrNtv_003CIContextData_003E);
																	try
																	{
																		int num2 = *(int*)m_pService + 60;
																		((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, ushort*, global::EDownloadContextEvent, ushort*, IContextData**, int>)(int)(*(uint*)num2))((nint)m_pService, (ushort*)Unsafe.AsPointer(ref reference2), eDownloadContextEvent, ptr9, global::_003CModule_003E.CComPtrNtv_003CIContextData_003E_002E_0026(&cComPtrNtv_003CIContextData_003E));
																		int num3 = *(int*)ptr4 + 20;
																		num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, _GUID*, global::EContentType, ushort*, ushort*, ushort*, IContextData*, int>)(int)(*(uint*)num3))((nint)ptr4, &gUID, eContentType, ptr5, ptr6, ptr7, global::_003CModule_003E.CComPtrNtv_003CIContextData_003E_002E_002EPAUIContextData_0040_0040(&cComPtrNtv_003CIContextData_003E));
																	}
																	catch
																	{
																		//try-fault
																		global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<CComPtrNtv_003CIContextData_003E*, void>)(&global::_003CModule_003E.CComPtrNtv_003CIContextData_003E_002E_007Bdtor_007D), &cComPtrNtv_003CIContextData_003E);
																		throw;
																	}
																	global::_003CModule_003E.CComPtrNtv_003CIContextData_003E_002E_007Bdtor_007D(&cComPtrNtv_003CIContextData_003E);
																}
															}
															else
															{
																global::_003CModule_003E.CComPtrNtv_003CIContextData_003E_002E_007Bctor_007D(&cComPtrNtv_003CIContextData_003E);
																try
																{
																	int num2 = *(int*)m_pService + 60;
																	((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, ushort*, global::EDownloadContextEvent, ushort*, IContextData**, int>)(int)(*(uint*)num2))((nint)m_pService, (ushort*)Unsafe.AsPointer(ref reference2), eDownloadContextEvent, (ushort*)Unsafe.AsPointer(ref reference3), global::_003CModule_003E.CComPtrNtv_003CIContextData_003E_002E_0026(&cComPtrNtv_003CIContextData_003E));
																	int num3 = *(int*)ptr4 + 20;
																	num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, _GUID*, global::EContentType, ushort*, ushort*, ushort*, IContextData*, int>)(int)(*(uint*)num3))((nint)ptr4, &gUID, eContentType, ptr5, ptr6, ptr7, global::_003CModule_003E.CComPtrNtv_003CIContextData_003E_002E_002EPAUIContextData_0040_0040(&cComPtrNtv_003CIContextData_003E));
																}
																catch
																{
																	//try-fault
																	global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<CComPtrNtv_003CIContextData_003E*, void>)(&global::_003CModule_003E.CComPtrNtv_003CIContextData_003E_002E_007Bdtor_007D), &cComPtrNtv_003CIContextData_003E);
																	throw;
																}
																global::_003CModule_003E.CComPtrNtv_003CIContextData_003E_002E_007Bdtor_007D(&cComPtrNtv_003CIContextData_003E);
															}
														}
														catch
														{
															//try-fault
															reference3 = ref *(ushort*)null;
															throw;
														}
														reference3 = ref *(ushort*)null;
													}
												}
												catch
												{
													//try-fault
													reference2 = ref *(ushort*)null;
													throw;
												}
												reference2 = ref *(ushort*)null;
											}
											catch
											{
												//try-fault
												ptr7 = null;
												throw;
											}
										}
									}
									catch
									{
										//try-fault
										ptr6 = null;
										throw;
									}
								}
							}
							catch
							{
								//try-fault
								ptr5 = null;
								throw;
							}
						}
					}
				}
				if (num >= 0)
				{
					int num4 = *(int*)m_pService + 304;
					((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, IMediaCollection*, global::EDownloadFlags, int, ushort*, int, IDownloadCallback*, int>)(int)(*(uint*)num4))((nint)m_pService, ptr4, (global::EDownloadFlags)eDownloadFlags, -1, (ushort*)Unsafe.AsPointer(ref reference), 1, (IDownloadCallback*)ptr3);
				}
				goto IL_069f;
			}
			IMediaCollection* intPtr;
			DownloadCallbackWrapper* intPtr2;
			fixed (ushort* ptr10 = &Unsafe.As<char, ushort>(ref global::_003CModule_003E.PtrToStringChars(deviceEndpointId)))
			{
				if (items != null)
				{
					IEnumerator enumerator = items.GetEnumerator();
					while (enumerator.MoveNext())
					{
						object current = enumerator.Current;
						Guid guid = Guid.Empty;
						string s = null;
						string s2 = null;
						string s3 = null;
						text = null;
						global::EContentType eContentType = (global::EContentType)(-1);
						if (current is DataProviderObject)
						{
							DataProviderObject val = (DataProviderObject)((current is DataProviderObject) ? current : null);
							string typeName = val.TypeName;
							try
							{
								text = (string)val.GetProperty("ReferrerContext");
							}
							catch (KeyNotFoundException ex)
							{
							}
							if (!typeName.Equals("Album") && !typeName.Equals("AlbumData") && !typeName.Equals("RecommendedAlbum"))
							{
								if (!typeName.Equals("Track") && !typeName.Equals("ChannelTrack") && !typeName.Equals("ProfileTrack") && !typeName.Equals("PlaylistTrack") && !typeName.Equals("ZplTrack") && !typeName.Equals("RecommendedTrack") && !typeName.Equals("TrackPurchaseHistory") && !typeName.Equals("TrackDownloadHistory"))
								{
									if (typeName.Equals("PlaylistContentItem"))
									{
										eContentType = (global::EContentType)0;
										guid = (Guid)val.GetProperty("ZuneMediaId");
										s = (string)val.GetProperty("Title");
										s2 = (string)val.GetProperty("AlbumName");
										s3 = (string)val.GetProperty("ArtistName");
									}
									else if (!typeName.Equals("MusicVideo") && !typeName.Equals("MusicVideoHistory") && !typeName.Equals("Episode") && !typeName.Equals("Short") && !typeName.Equals("VideoHistory"))
									{
										if (typeName.Equals("Video"))
										{
											eContentType = (global::EContentType)3;
											guid = (Guid)val.GetProperty("ZuneMediaId");
											s = (string)val.GetProperty("Title");
											s3 = (string)val.GetProperty("Title");
										}
										else if (typeName.Equals("AppData") || typeName.Equals("ZuneHDAppData"))
										{
											eContentType = (global::EContentType)7;
											guid = (Guid)val.GetProperty("Id");
											s = (string)val.GetProperty("Title");
											s3 = (string)val.GetProperty("Author");
										}
									}
									else
									{
										eContentType = (global::EContentType)3;
										guid = (Guid)val.GetProperty("Id");
										s = (string)val.GetProperty("Title");
										DataProviderObject val2 = (DataProviderObject)val.GetProperty("PrimaryArtist");
										if (val2 != null)
										{
											s3 = (string)val2.GetProperty("Title");
										}
									}
								}
								else
								{
									eContentType = (global::EContentType)0;
									guid = (Guid)val.GetProperty("Id");
									s = (string)val.GetProperty("Title");
									s2 = (string)val.GetProperty("AlbumTitle");
									DataProviderObject val3 = (DataProviderObject)val.GetProperty("PrimaryArtist");
									if (val3 != null)
									{
										s3 = (string)val3.GetProperty("Title");
									}
								}
							}
							else
							{
								eContentType = (global::EContentType)1;
								guid = (Guid)val.GetProperty("Id");
								s = (string)val.GetProperty("Title");
							}
						}
						else if (current is TrackOffer)
						{
							eContentType = (global::EContentType)0;
							TrackOffer trackOffer = current as TrackOffer;
							guid = trackOffer.Id;
							s = trackOffer.Title;
							s2 = trackOffer.Album;
							s3 = trackOffer.Artist;
							text = trackOffer.ServiceContext;
						}
						else if (current is VideoOffer)
						{
							eContentType = (global::EContentType)3;
							VideoOffer videoOffer = current as VideoOffer;
							guid = videoOffer.Id;
							s = videoOffer.Title;
							s2 = videoOffer.SeriesTitle;
							s3 = videoOffer.Artist;
						}
						else if (current is AlbumOffer)
						{
							eContentType = (global::EContentType)1;
							AlbumOffer albumOffer = current as AlbumOffer;
							guid = albumOffer.Id;
							s = albumOffer.Title;
							text = albumOffer.ServiceContext;
						}
						else if ((object)current.GetType() == typeof(DownloadTask))
						{
							DownloadTask downloadTask = (DownloadTask)current;
							string property = ((DownloadTask)current).GetProperty("Type");
							if (!string.IsNullOrEmpty(property))
							{
								eContentType = (global::EContentType)GetContentType(property);
							}
							string property2 = ((DownloadTask)current).GetProperty("ServiceId");
							if (!string.IsNullOrEmpty(property2))
							{
								try
								{
									Guid guid2 = new Guid(property2);
									guid = guid2;
								}
								catch (FormatException ex2)
								{
								}
							}
							s = downloadTask.GetProperty("Title");
							s2 = downloadTask.GetProperty("Album");
							s3 = downloadTask.GetProperty("Artist");
						}
						if (num < 0 || !(guid != Guid.Empty))
						{
							continue;
						}
						gUID = global::_003CModule_003E.GuidToGUID(guid);
						fixed (ushort* ptr5 = &Unsafe.As<char, ushort>(ref global::_003CModule_003E.PtrToStringChars(s)))
						{
							try
							{
								if (ptr5 == null)
								{
									fixed (ushort* ptr5 = &Unsafe.As<_0024ArrayType_0024_0024_0024BY00_0024_0024CBG, ushort>(ref global::_003CModule_003E._003F_003F_C_0040_11LOCGONAA_0040_003F_0024AA_003F_0024AA_0040))
									{
										fixed (ushort* ptr6 = &Unsafe.As<char, ushort>(ref global::_003CModule_003E.PtrToStringChars(s2)))
										{
											try
											{
												if (ptr6 == null)
												{
													fixed (ushort* ptr6 = &Unsafe.As<_0024ArrayType_0024_0024_0024BY00_0024_0024CBG, ushort>(ref global::_003CModule_003E._003F_003F_C_0040_11LOCGONAA_0040_003F_0024AA_003F_0024AA_0040))
													{
														fixed (ushort* ptr7 = &Unsafe.As<char, ushort>(ref global::_003CModule_003E.PtrToStringChars(s3)))
														{
															try
															{
																if (ptr7 == null)
																{
																	fixed (ushort* ptr7 = &Unsafe.As<_0024ArrayType_0024_0024_0024BY00_0024_0024CBG, ushort>(ref global::_003CModule_003E._003F_003F_C_0040_11LOCGONAA_0040_003F_0024AA_003F_0024AA_0040))
																	{
																		reference2 = ref *(ushort*)null;
																		try
																		{
																			if (!string.IsNullOrEmpty(text))
																			{
																				fixed (ushort* ptr8 = &Unsafe.As<char, ushort>(ref global::_003CModule_003E.PtrToStringChars(text)))
																				{
																					global::EDownloadContextEvent eDownloadContextEvent = (global::EDownloadContextEvent)(-1);
																					reference3 = ref *(ushort*)null;
																					try
																					{
																						if (clientContextEvent != EDownloadContextEvent.Unknown && !string.IsNullOrEmpty(clientContextEventData))
																						{
																							eDownloadContextEvent = (global::EDownloadContextEvent)clientContextEvent;
																							fixed (ushort* ptr9 = &Unsafe.As<char, ushort>(ref global::_003CModule_003E.PtrToStringChars(clientContextEventData)))
																							{
																								global::_003CModule_003E.CComPtrNtv_003CIContextData_003E_002E_007Bctor_007D(&cComPtrNtv_003CIContextData_003E);
																								try
																								{
																									int num2 = *(int*)m_pService + 60;
																									((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, ushort*, global::EDownloadContextEvent, ushort*, IContextData**, int>)(int)(*(uint*)num2))((nint)m_pService, ptr8, eDownloadContextEvent, ptr9, global::_003CModule_003E.CComPtrNtv_003CIContextData_003E_002E_0026(&cComPtrNtv_003CIContextData_003E));
																									int num3 = *(int*)ptr4 + 20;
																									num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, _GUID*, global::EContentType, ushort*, ushort*, ushort*, IContextData*, int>)(int)(*(uint*)num3))((nint)ptr4, &gUID, eContentType, ptr5, ptr6, ptr7, global::_003CModule_003E.CComPtrNtv_003CIContextData_003E_002E_002EPAUIContextData_0040_0040(&cComPtrNtv_003CIContextData_003E));
																								}
																								catch
																								{
																									//try-fault
																									global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<CComPtrNtv_003CIContextData_003E*, void>)(&global::_003CModule_003E.CComPtrNtv_003CIContextData_003E_002E_007Bdtor_007D), &cComPtrNtv_003CIContextData_003E);
																									throw;
																								}
																								global::_003CModule_003E.CComPtrNtv_003CIContextData_003E_002E_007Bdtor_007D(&cComPtrNtv_003CIContextData_003E);
																							}
																						}
																						else
																						{
																							global::_003CModule_003E.CComPtrNtv_003CIContextData_003E_002E_007Bctor_007D(&cComPtrNtv_003CIContextData_003E);
																							try
																							{
																								int num2 = *(int*)m_pService + 60;
																								((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, ushort*, global::EDownloadContextEvent, ushort*, IContextData**, int>)(int)(*(uint*)num2))((nint)m_pService, ptr8, eDownloadContextEvent, (ushort*)Unsafe.AsPointer(ref reference3), global::_003CModule_003E.CComPtrNtv_003CIContextData_003E_002E_0026(&cComPtrNtv_003CIContextData_003E));
																								int num3 = *(int*)ptr4 + 20;
																								num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, _GUID*, global::EContentType, ushort*, ushort*, ushort*, IContextData*, int>)(int)(*(uint*)num3))((nint)ptr4, &gUID, eContentType, ptr5, ptr6, ptr7, global::_003CModule_003E.CComPtrNtv_003CIContextData_003E_002E_002EPAUIContextData_0040_0040(&cComPtrNtv_003CIContextData_003E));
																							}
																							catch
																							{
																								//try-fault
																								global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<CComPtrNtv_003CIContextData_003E*, void>)(&global::_003CModule_003E.CComPtrNtv_003CIContextData_003E_002E_007Bdtor_007D), &cComPtrNtv_003CIContextData_003E);
																								throw;
																							}
																							global::_003CModule_003E.CComPtrNtv_003CIContextData_003E_002E_007Bdtor_007D(&cComPtrNtv_003CIContextData_003E);
																						}
																					}
																					catch
																					{
																						//try-fault
																						reference3 = ref *(ushort*)null;
																						throw;
																					}
																					reference3 = ref *(ushort*)null;
																				}
																			}
																			else
																			{
																				global::EDownloadContextEvent eDownloadContextEvent = (global::EDownloadContextEvent)(-1);
																				reference3 = ref *(ushort*)null;
																				try
																				{
																					if (clientContextEvent != EDownloadContextEvent.Unknown && !string.IsNullOrEmpty(clientContextEventData))
																					{
																						eDownloadContextEvent = (global::EDownloadContextEvent)clientContextEvent;
																						fixed (ushort* ptr9 = &Unsafe.As<char, ushort>(ref global::_003CModule_003E.PtrToStringChars(clientContextEventData)))
																						{
																							global::_003CModule_003E.CComPtrNtv_003CIContextData_003E_002E_007Bctor_007D(&cComPtrNtv_003CIContextData_003E);
																							try
																							{
																								int num2 = *(int*)m_pService + 60;
																								((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, ushort*, global::EDownloadContextEvent, ushort*, IContextData**, int>)(int)(*(uint*)num2))((nint)m_pService, (ushort*)Unsafe.AsPointer(ref reference2), eDownloadContextEvent, ptr9, global::_003CModule_003E.CComPtrNtv_003CIContextData_003E_002E_0026(&cComPtrNtv_003CIContextData_003E));
																								int num3 = *(int*)ptr4 + 20;
																								num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, _GUID*, global::EContentType, ushort*, ushort*, ushort*, IContextData*, int>)(int)(*(uint*)num3))((nint)ptr4, &gUID, eContentType, ptr5, ptr6, ptr7, global::_003CModule_003E.CComPtrNtv_003CIContextData_003E_002E_002EPAUIContextData_0040_0040(&cComPtrNtv_003CIContextData_003E));
																							}
																							catch
																							{
																								//try-fault
																								global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<CComPtrNtv_003CIContextData_003E*, void>)(&global::_003CModule_003E.CComPtrNtv_003CIContextData_003E_002E_007Bdtor_007D), &cComPtrNtv_003CIContextData_003E);
																								throw;
																							}
																							global::_003CModule_003E.CComPtrNtv_003CIContextData_003E_002E_007Bdtor_007D(&cComPtrNtv_003CIContextData_003E);
																						}
																					}
																					else
																					{
																						global::_003CModule_003E.CComPtrNtv_003CIContextData_003E_002E_007Bctor_007D(&cComPtrNtv_003CIContextData_003E);
																						try
																						{
																							int num2 = *(int*)m_pService + 60;
																							((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, ushort*, global::EDownloadContextEvent, ushort*, IContextData**, int>)(int)(*(uint*)num2))((nint)m_pService, (ushort*)Unsafe.AsPointer(ref reference2), eDownloadContextEvent, (ushort*)Unsafe.AsPointer(ref reference3), global::_003CModule_003E.CComPtrNtv_003CIContextData_003E_002E_0026(&cComPtrNtv_003CIContextData_003E));
																							int num3 = *(int*)ptr4 + 20;
																							num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, _GUID*, global::EContentType, ushort*, ushort*, ushort*, IContextData*, int>)(int)(*(uint*)num3))((nint)ptr4, &gUID, eContentType, ptr5, ptr6, ptr7, global::_003CModule_003E.CComPtrNtv_003CIContextData_003E_002E_002EPAUIContextData_0040_0040(&cComPtrNtv_003CIContextData_003E));
																						}
																						catch
																						{
																							//try-fault
																							global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<CComPtrNtv_003CIContextData_003E*, void>)(&global::_003CModule_003E.CComPtrNtv_003CIContextData_003E_002E_007Bdtor_007D), &cComPtrNtv_003CIContextData_003E);
																							throw;
																						}
																						global::_003CModule_003E.CComPtrNtv_003CIContextData_003E_002E_007Bdtor_007D(&cComPtrNtv_003CIContextData_003E);
																					}
																				}
																				catch
																				{
																					//try-fault
																					reference3 = ref *(ushort*)null;
																					throw;
																				}
																				reference3 = ref *(ushort*)null;
																			}
																		}
																		catch
																		{
																			//try-fault
																			reference2 = ref *(ushort*)null;
																			throw;
																		}
																		reference2 = ref *(ushort*)null;
																	}
																	continue;
																}
																reference2 = ref *(ushort*)null;
																try
																{
																	if (!string.IsNullOrEmpty(text))
																	{
																		fixed (ushort* ptr8 = &Unsafe.As<char, ushort>(ref global::_003CModule_003E.PtrToStringChars(text)))
																		{
																			global::EDownloadContextEvent eDownloadContextEvent = (global::EDownloadContextEvent)(-1);
																			reference3 = ref *(ushort*)null;
																			try
																			{
																				if (clientContextEvent != EDownloadContextEvent.Unknown && !string.IsNullOrEmpty(clientContextEventData))
																				{
																					eDownloadContextEvent = (global::EDownloadContextEvent)clientContextEvent;
																					fixed (ushort* ptr9 = &Unsafe.As<char, ushort>(ref global::_003CModule_003E.PtrToStringChars(clientContextEventData)))
																					{
																						global::_003CModule_003E.CComPtrNtv_003CIContextData_003E_002E_007Bctor_007D(&cComPtrNtv_003CIContextData_003E);
																						try
																						{
																							int num2 = *(int*)m_pService + 60;
																							((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, ushort*, global::EDownloadContextEvent, ushort*, IContextData**, int>)(int)(*(uint*)num2))((nint)m_pService, ptr8, eDownloadContextEvent, ptr9, global::_003CModule_003E.CComPtrNtv_003CIContextData_003E_002E_0026(&cComPtrNtv_003CIContextData_003E));
																							int num3 = *(int*)ptr4 + 20;
																							num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, _GUID*, global::EContentType, ushort*, ushort*, ushort*, IContextData*, int>)(int)(*(uint*)num3))((nint)ptr4, &gUID, eContentType, ptr5, ptr6, ptr7, global::_003CModule_003E.CComPtrNtv_003CIContextData_003E_002E_002EPAUIContextData_0040_0040(&cComPtrNtv_003CIContextData_003E));
																						}
																						catch
																						{
																							//try-fault
																							global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<CComPtrNtv_003CIContextData_003E*, void>)(&global::_003CModule_003E.CComPtrNtv_003CIContextData_003E_002E_007Bdtor_007D), &cComPtrNtv_003CIContextData_003E);
																							throw;
																						}
																						global::_003CModule_003E.CComPtrNtv_003CIContextData_003E_002E_007Bdtor_007D(&cComPtrNtv_003CIContextData_003E);
																					}
																				}
																				else
																				{
																					global::_003CModule_003E.CComPtrNtv_003CIContextData_003E_002E_007Bctor_007D(&cComPtrNtv_003CIContextData_003E);
																					try
																					{
																						int num2 = *(int*)m_pService + 60;
																						((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, ushort*, global::EDownloadContextEvent, ushort*, IContextData**, int>)(int)(*(uint*)num2))((nint)m_pService, ptr8, eDownloadContextEvent, (ushort*)Unsafe.AsPointer(ref reference3), global::_003CModule_003E.CComPtrNtv_003CIContextData_003E_002E_0026(&cComPtrNtv_003CIContextData_003E));
																						int num3 = *(int*)ptr4 + 20;
																						num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, _GUID*, global::EContentType, ushort*, ushort*, ushort*, IContextData*, int>)(int)(*(uint*)num3))((nint)ptr4, &gUID, eContentType, ptr5, ptr6, ptr7, global::_003CModule_003E.CComPtrNtv_003CIContextData_003E_002E_002EPAUIContextData_0040_0040(&cComPtrNtv_003CIContextData_003E));
																					}
																					catch
																					{
																						//try-fault
																						global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<CComPtrNtv_003CIContextData_003E*, void>)(&global::_003CModule_003E.CComPtrNtv_003CIContextData_003E_002E_007Bdtor_007D), &cComPtrNtv_003CIContextData_003E);
																						throw;
																					}
																					global::_003CModule_003E.CComPtrNtv_003CIContextData_003E_002E_007Bdtor_007D(&cComPtrNtv_003CIContextData_003E);
																				}
																			}
																			catch
																			{
																				//try-fault
																				reference3 = ref *(ushort*)null;
																				throw;
																			}
																			reference3 = ref *(ushort*)null;
																		}
																	}
																	else
																	{
																		global::EDownloadContextEvent eDownloadContextEvent = (global::EDownloadContextEvent)(-1);
																		reference3 = ref *(ushort*)null;
																		try
																		{
																			if (clientContextEvent != EDownloadContextEvent.Unknown && !string.IsNullOrEmpty(clientContextEventData))
																			{
																				eDownloadContextEvent = (global::EDownloadContextEvent)clientContextEvent;
																				fixed (ushort* ptr9 = &Unsafe.As<char, ushort>(ref global::_003CModule_003E.PtrToStringChars(clientContextEventData)))
																				{
																					global::_003CModule_003E.CComPtrNtv_003CIContextData_003E_002E_007Bctor_007D(&cComPtrNtv_003CIContextData_003E);
																					try
																					{
																						int num2 = *(int*)m_pService + 60;
																						((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, ushort*, global::EDownloadContextEvent, ushort*, IContextData**, int>)(int)(*(uint*)num2))((nint)m_pService, (ushort*)Unsafe.AsPointer(ref reference2), eDownloadContextEvent, ptr9, global::_003CModule_003E.CComPtrNtv_003CIContextData_003E_002E_0026(&cComPtrNtv_003CIContextData_003E));
																						int num3 = *(int*)ptr4 + 20;
																						num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, _GUID*, global::EContentType, ushort*, ushort*, ushort*, IContextData*, int>)(int)(*(uint*)num3))((nint)ptr4, &gUID, eContentType, ptr5, ptr6, ptr7, global::_003CModule_003E.CComPtrNtv_003CIContextData_003E_002E_002EPAUIContextData_0040_0040(&cComPtrNtv_003CIContextData_003E));
																					}
																					catch
																					{
																						//try-fault
																						global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<CComPtrNtv_003CIContextData_003E*, void>)(&global::_003CModule_003E.CComPtrNtv_003CIContextData_003E_002E_007Bdtor_007D), &cComPtrNtv_003CIContextData_003E);
																						throw;
																					}
																					global::_003CModule_003E.CComPtrNtv_003CIContextData_003E_002E_007Bdtor_007D(&cComPtrNtv_003CIContextData_003E);
																				}
																			}
																			else
																			{
																				global::_003CModule_003E.CComPtrNtv_003CIContextData_003E_002E_007Bctor_007D(&cComPtrNtv_003CIContextData_003E);
																				try
																				{
																					int num2 = *(int*)m_pService + 60;
																					((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, ushort*, global::EDownloadContextEvent, ushort*, IContextData**, int>)(int)(*(uint*)num2))((nint)m_pService, (ushort*)Unsafe.AsPointer(ref reference2), eDownloadContextEvent, (ushort*)Unsafe.AsPointer(ref reference3), global::_003CModule_003E.CComPtrNtv_003CIContextData_003E_002E_0026(&cComPtrNtv_003CIContextData_003E));
																					int num3 = *(int*)ptr4 + 20;
																					num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, _GUID*, global::EContentType, ushort*, ushort*, ushort*, IContextData*, int>)(int)(*(uint*)num3))((nint)ptr4, &gUID, eContentType, ptr5, ptr6, ptr7, global::_003CModule_003E.CComPtrNtv_003CIContextData_003E_002E_002EPAUIContextData_0040_0040(&cComPtrNtv_003CIContextData_003E));
																				}
																				catch
																				{
																					//try-fault
																					global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<CComPtrNtv_003CIContextData_003E*, void>)(&global::_003CModule_003E.CComPtrNtv_003CIContextData_003E_002E_007Bdtor_007D), &cComPtrNtv_003CIContextData_003E);
																					throw;
																				}
																				global::_003CModule_003E.CComPtrNtv_003CIContextData_003E_002E_007Bdtor_007D(&cComPtrNtv_003CIContextData_003E);
																			}
																		}
																		catch
																		{
																			//try-fault
																			reference3 = ref *(ushort*)null;
																			throw;
																		}
																		reference3 = ref *(ushort*)null;
																	}
																}
																catch
																{
																	//try-fault
																	reference2 = ref *(ushort*)null;
																	throw;
																}
																reference2 = ref *(ushort*)null;
															}
															catch
															{
																//try-fault
																ptr7 = null;
																throw;
															}
														}
													}
													continue;
												}
												fixed (ushort* ptr7 = &Unsafe.As<char, ushort>(ref global::_003CModule_003E.PtrToStringChars(s3)))
												{
													try
													{
														if (ptr7 == null)
														{
															fixed (ushort* ptr7 = &Unsafe.As<_0024ArrayType_0024_0024_0024BY00_0024_0024CBG, ushort>(ref global::_003CModule_003E._003F_003F_C_0040_11LOCGONAA_0040_003F_0024AA_003F_0024AA_0040))
															{
																reference2 = ref *(ushort*)null;
																try
																{
																	if (!string.IsNullOrEmpty(text))
																	{
																		fixed (ushort* ptr8 = &Unsafe.As<char, ushort>(ref global::_003CModule_003E.PtrToStringChars(text)))
																		{
																			global::EDownloadContextEvent eDownloadContextEvent = (global::EDownloadContextEvent)(-1);
																			reference3 = ref *(ushort*)null;
																			try
																			{
																				if (clientContextEvent != EDownloadContextEvent.Unknown && !string.IsNullOrEmpty(clientContextEventData))
																				{
																					eDownloadContextEvent = (global::EDownloadContextEvent)clientContextEvent;
																					fixed (ushort* ptr9 = &Unsafe.As<char, ushort>(ref global::_003CModule_003E.PtrToStringChars(clientContextEventData)))
																					{
																						global::_003CModule_003E.CComPtrNtv_003CIContextData_003E_002E_007Bctor_007D(&cComPtrNtv_003CIContextData_003E);
																						try
																						{
																							int num2 = *(int*)m_pService + 60;
																							((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, ushort*, global::EDownloadContextEvent, ushort*, IContextData**, int>)(int)(*(uint*)num2))((nint)m_pService, ptr8, eDownloadContextEvent, ptr9, global::_003CModule_003E.CComPtrNtv_003CIContextData_003E_002E_0026(&cComPtrNtv_003CIContextData_003E));
																							int num3 = *(int*)ptr4 + 20;
																							num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, _GUID*, global::EContentType, ushort*, ushort*, ushort*, IContextData*, int>)(int)(*(uint*)num3))((nint)ptr4, &gUID, eContentType, ptr5, ptr6, ptr7, global::_003CModule_003E.CComPtrNtv_003CIContextData_003E_002E_002EPAUIContextData_0040_0040(&cComPtrNtv_003CIContextData_003E));
																						}
																						catch
																						{
																							//try-fault
																							global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<CComPtrNtv_003CIContextData_003E*, void>)(&global::_003CModule_003E.CComPtrNtv_003CIContextData_003E_002E_007Bdtor_007D), &cComPtrNtv_003CIContextData_003E);
																							throw;
																						}
																						global::_003CModule_003E.CComPtrNtv_003CIContextData_003E_002E_007Bdtor_007D(&cComPtrNtv_003CIContextData_003E);
																					}
																				}
																				else
																				{
																					global::_003CModule_003E.CComPtrNtv_003CIContextData_003E_002E_007Bctor_007D(&cComPtrNtv_003CIContextData_003E);
																					try
																					{
																						int num2 = *(int*)m_pService + 60;
																						((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, ushort*, global::EDownloadContextEvent, ushort*, IContextData**, int>)(int)(*(uint*)num2))((nint)m_pService, ptr8, eDownloadContextEvent, (ushort*)Unsafe.AsPointer(ref reference3), global::_003CModule_003E.CComPtrNtv_003CIContextData_003E_002E_0026(&cComPtrNtv_003CIContextData_003E));
																						int num3 = *(int*)ptr4 + 20;
																						num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, _GUID*, global::EContentType, ushort*, ushort*, ushort*, IContextData*, int>)(int)(*(uint*)num3))((nint)ptr4, &gUID, eContentType, ptr5, ptr6, ptr7, global::_003CModule_003E.CComPtrNtv_003CIContextData_003E_002E_002EPAUIContextData_0040_0040(&cComPtrNtv_003CIContextData_003E));
																					}
																					catch
																					{
																						//try-fault
																						global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<CComPtrNtv_003CIContextData_003E*, void>)(&global::_003CModule_003E.CComPtrNtv_003CIContextData_003E_002E_007Bdtor_007D), &cComPtrNtv_003CIContextData_003E);
																						throw;
																					}
																					global::_003CModule_003E.CComPtrNtv_003CIContextData_003E_002E_007Bdtor_007D(&cComPtrNtv_003CIContextData_003E);
																				}
																			}
																			catch
																			{
																				//try-fault
																				reference3 = ref *(ushort*)null;
																				throw;
																			}
																			reference3 = ref *(ushort*)null;
																		}
																	}
																	else
																	{
																		global::EDownloadContextEvent eDownloadContextEvent = (global::EDownloadContextEvent)(-1);
																		reference3 = ref *(ushort*)null;
																		try
																		{
																			if (clientContextEvent != EDownloadContextEvent.Unknown && !string.IsNullOrEmpty(clientContextEventData))
																			{
																				eDownloadContextEvent = (global::EDownloadContextEvent)clientContextEvent;
																				fixed (ushort* ptr9 = &Unsafe.As<char, ushort>(ref global::_003CModule_003E.PtrToStringChars(clientContextEventData)))
																				{
																					global::_003CModule_003E.CComPtrNtv_003CIContextData_003E_002E_007Bctor_007D(&cComPtrNtv_003CIContextData_003E);
																					try
																					{
																						int num2 = *(int*)m_pService + 60;
																						((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, ushort*, global::EDownloadContextEvent, ushort*, IContextData**, int>)(int)(*(uint*)num2))((nint)m_pService, (ushort*)Unsafe.AsPointer(ref reference2), eDownloadContextEvent, ptr9, global::_003CModule_003E.CComPtrNtv_003CIContextData_003E_002E_0026(&cComPtrNtv_003CIContextData_003E));
																						int num3 = *(int*)ptr4 + 20;
																						num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, _GUID*, global::EContentType, ushort*, ushort*, ushort*, IContextData*, int>)(int)(*(uint*)num3))((nint)ptr4, &gUID, eContentType, ptr5, ptr6, ptr7, global::_003CModule_003E.CComPtrNtv_003CIContextData_003E_002E_002EPAUIContextData_0040_0040(&cComPtrNtv_003CIContextData_003E));
																					}
																					catch
																					{
																						//try-fault
																						global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<CComPtrNtv_003CIContextData_003E*, void>)(&global::_003CModule_003E.CComPtrNtv_003CIContextData_003E_002E_007Bdtor_007D), &cComPtrNtv_003CIContextData_003E);
																						throw;
																					}
																					global::_003CModule_003E.CComPtrNtv_003CIContextData_003E_002E_007Bdtor_007D(&cComPtrNtv_003CIContextData_003E);
																				}
																			}
																			else
																			{
																				global::_003CModule_003E.CComPtrNtv_003CIContextData_003E_002E_007Bctor_007D(&cComPtrNtv_003CIContextData_003E);
																				try
																				{
																					int num2 = *(int*)m_pService + 60;
																					((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, ushort*, global::EDownloadContextEvent, ushort*, IContextData**, int>)(int)(*(uint*)num2))((nint)m_pService, (ushort*)Unsafe.AsPointer(ref reference2), eDownloadContextEvent, (ushort*)Unsafe.AsPointer(ref reference3), global::_003CModule_003E.CComPtrNtv_003CIContextData_003E_002E_0026(&cComPtrNtv_003CIContextData_003E));
																					int num3 = *(int*)ptr4 + 20;
																					num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, _GUID*, global::EContentType, ushort*, ushort*, ushort*, IContextData*, int>)(int)(*(uint*)num3))((nint)ptr4, &gUID, eContentType, ptr5, ptr6, ptr7, global::_003CModule_003E.CComPtrNtv_003CIContextData_003E_002E_002EPAUIContextData_0040_0040(&cComPtrNtv_003CIContextData_003E));
																				}
																				catch
																				{
																					//try-fault
																					global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<CComPtrNtv_003CIContextData_003E*, void>)(&global::_003CModule_003E.CComPtrNtv_003CIContextData_003E_002E_007Bdtor_007D), &cComPtrNtv_003CIContextData_003E);
																					throw;
																				}
																				global::_003CModule_003E.CComPtrNtv_003CIContextData_003E_002E_007Bdtor_007D(&cComPtrNtv_003CIContextData_003E);
																			}
																		}
																		catch
																		{
																			//try-fault
																			reference3 = ref *(ushort*)null;
																			throw;
																		}
																		reference3 = ref *(ushort*)null;
																	}
																}
																catch
																{
																	//try-fault
																	reference2 = ref *(ushort*)null;
																	throw;
																}
																reference2 = ref *(ushort*)null;
															}
															continue;
														}
														reference2 = ref *(ushort*)null;
														try
														{
															if (!string.IsNullOrEmpty(text))
															{
																fixed (ushort* ptr8 = &Unsafe.As<char, ushort>(ref global::_003CModule_003E.PtrToStringChars(text)))
																{
																	global::EDownloadContextEvent eDownloadContextEvent = (global::EDownloadContextEvent)(-1);
																	reference3 = ref *(ushort*)null;
																	try
																	{
																		if (clientContextEvent != EDownloadContextEvent.Unknown && !string.IsNullOrEmpty(clientContextEventData))
																		{
																			eDownloadContextEvent = (global::EDownloadContextEvent)clientContextEvent;
																			fixed (ushort* ptr9 = &Unsafe.As<char, ushort>(ref global::_003CModule_003E.PtrToStringChars(clientContextEventData)))
																			{
																				global::_003CModule_003E.CComPtrNtv_003CIContextData_003E_002E_007Bctor_007D(&cComPtrNtv_003CIContextData_003E);
																				try
																				{
																					int num2 = *(int*)m_pService + 60;
																					((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, ushort*, global::EDownloadContextEvent, ushort*, IContextData**, int>)(int)(*(uint*)num2))((nint)m_pService, ptr8, eDownloadContextEvent, ptr9, global::_003CModule_003E.CComPtrNtv_003CIContextData_003E_002E_0026(&cComPtrNtv_003CIContextData_003E));
																					int num3 = *(int*)ptr4 + 20;
																					num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, _GUID*, global::EContentType, ushort*, ushort*, ushort*, IContextData*, int>)(int)(*(uint*)num3))((nint)ptr4, &gUID, eContentType, ptr5, ptr6, ptr7, global::_003CModule_003E.CComPtrNtv_003CIContextData_003E_002E_002EPAUIContextData_0040_0040(&cComPtrNtv_003CIContextData_003E));
																				}
																				catch
																				{
																					//try-fault
																					global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<CComPtrNtv_003CIContextData_003E*, void>)(&global::_003CModule_003E.CComPtrNtv_003CIContextData_003E_002E_007Bdtor_007D), &cComPtrNtv_003CIContextData_003E);
																					throw;
																				}
																				global::_003CModule_003E.CComPtrNtv_003CIContextData_003E_002E_007Bdtor_007D(&cComPtrNtv_003CIContextData_003E);
																			}
																		}
																		else
																		{
																			global::_003CModule_003E.CComPtrNtv_003CIContextData_003E_002E_007Bctor_007D(&cComPtrNtv_003CIContextData_003E);
																			try
																			{
																				int num2 = *(int*)m_pService + 60;
																				((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, ushort*, global::EDownloadContextEvent, ushort*, IContextData**, int>)(int)(*(uint*)num2))((nint)m_pService, ptr8, eDownloadContextEvent, (ushort*)Unsafe.AsPointer(ref reference3), global::_003CModule_003E.CComPtrNtv_003CIContextData_003E_002E_0026(&cComPtrNtv_003CIContextData_003E));
																				int num3 = *(int*)ptr4 + 20;
																				num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, _GUID*, global::EContentType, ushort*, ushort*, ushort*, IContextData*, int>)(int)(*(uint*)num3))((nint)ptr4, &gUID, eContentType, ptr5, ptr6, ptr7, global::_003CModule_003E.CComPtrNtv_003CIContextData_003E_002E_002EPAUIContextData_0040_0040(&cComPtrNtv_003CIContextData_003E));
																			}
																			catch
																			{
																				//try-fault
																				global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<CComPtrNtv_003CIContextData_003E*, void>)(&global::_003CModule_003E.CComPtrNtv_003CIContextData_003E_002E_007Bdtor_007D), &cComPtrNtv_003CIContextData_003E);
																				throw;
																			}
																			global::_003CModule_003E.CComPtrNtv_003CIContextData_003E_002E_007Bdtor_007D(&cComPtrNtv_003CIContextData_003E);
																		}
																	}
																	catch
																	{
																		//try-fault
																		reference3 = ref *(ushort*)null;
																		throw;
																	}
																	reference3 = ref *(ushort*)null;
																}
															}
															else
															{
																global::EDownloadContextEvent eDownloadContextEvent = (global::EDownloadContextEvent)(-1);
																reference3 = ref *(ushort*)null;
																try
																{
																	if (clientContextEvent != EDownloadContextEvent.Unknown && !string.IsNullOrEmpty(clientContextEventData))
																	{
																		eDownloadContextEvent = (global::EDownloadContextEvent)clientContextEvent;
																		fixed (ushort* ptr9 = &Unsafe.As<char, ushort>(ref global::_003CModule_003E.PtrToStringChars(clientContextEventData)))
																		{
																			global::_003CModule_003E.CComPtrNtv_003CIContextData_003E_002E_007Bctor_007D(&cComPtrNtv_003CIContextData_003E);
																			try
																			{
																				int num2 = *(int*)m_pService + 60;
																				((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, ushort*, global::EDownloadContextEvent, ushort*, IContextData**, int>)(int)(*(uint*)num2))((nint)m_pService, (ushort*)Unsafe.AsPointer(ref reference2), eDownloadContextEvent, ptr9, global::_003CModule_003E.CComPtrNtv_003CIContextData_003E_002E_0026(&cComPtrNtv_003CIContextData_003E));
																				int num3 = *(int*)ptr4 + 20;
																				num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, _GUID*, global::EContentType, ushort*, ushort*, ushort*, IContextData*, int>)(int)(*(uint*)num3))((nint)ptr4, &gUID, eContentType, ptr5, ptr6, ptr7, global::_003CModule_003E.CComPtrNtv_003CIContextData_003E_002E_002EPAUIContextData_0040_0040(&cComPtrNtv_003CIContextData_003E));
																			}
																			catch
																			{
																				//try-fault
																				global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<CComPtrNtv_003CIContextData_003E*, void>)(&global::_003CModule_003E.CComPtrNtv_003CIContextData_003E_002E_007Bdtor_007D), &cComPtrNtv_003CIContextData_003E);
																				throw;
																			}
																			global::_003CModule_003E.CComPtrNtv_003CIContextData_003E_002E_007Bdtor_007D(&cComPtrNtv_003CIContextData_003E);
																		}
																	}
																	else
																	{
																		global::_003CModule_003E.CComPtrNtv_003CIContextData_003E_002E_007Bctor_007D(&cComPtrNtv_003CIContextData_003E);
																		try
																		{
																			int num2 = *(int*)m_pService + 60;
																			((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, ushort*, global::EDownloadContextEvent, ushort*, IContextData**, int>)(int)(*(uint*)num2))((nint)m_pService, (ushort*)Unsafe.AsPointer(ref reference2), eDownloadContextEvent, (ushort*)Unsafe.AsPointer(ref reference3), global::_003CModule_003E.CComPtrNtv_003CIContextData_003E_002E_0026(&cComPtrNtv_003CIContextData_003E));
																			int num3 = *(int*)ptr4 + 20;
																			num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, _GUID*, global::EContentType, ushort*, ushort*, ushort*, IContextData*, int>)(int)(*(uint*)num3))((nint)ptr4, &gUID, eContentType, ptr5, ptr6, ptr7, global::_003CModule_003E.CComPtrNtv_003CIContextData_003E_002E_002EPAUIContextData_0040_0040(&cComPtrNtv_003CIContextData_003E));
																		}
																		catch
																		{
																			//try-fault
																			global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<CComPtrNtv_003CIContextData_003E*, void>)(&global::_003CModule_003E.CComPtrNtv_003CIContextData_003E_002E_007Bdtor_007D), &cComPtrNtv_003CIContextData_003E);
																			throw;
																		}
																		global::_003CModule_003E.CComPtrNtv_003CIContextData_003E_002E_007Bdtor_007D(&cComPtrNtv_003CIContextData_003E);
																	}
																}
																catch
																{
																	//try-fault
																	reference3 = ref *(ushort*)null;
																	throw;
																}
																reference3 = ref *(ushort*)null;
															}
														}
														catch
														{
															//try-fault
															reference2 = ref *(ushort*)null;
															throw;
														}
														reference2 = ref *(ushort*)null;
													}
													catch
													{
														//try-fault
														ptr7 = null;
														throw;
													}
												}
											}
											catch
											{
												//try-fault
												ptr6 = null;
												throw;
											}
										}
									}
									continue;
								}
								fixed (ushort* ptr6 = &Unsafe.As<char, ushort>(ref global::_003CModule_003E.PtrToStringChars(s2)))
								{
									try
									{
										if (ptr6 == null)
										{
											fixed (ushort* ptr6 = &Unsafe.As<_0024ArrayType_0024_0024_0024BY00_0024_0024CBG, ushort>(ref global::_003CModule_003E._003F_003F_C_0040_11LOCGONAA_0040_003F_0024AA_003F_0024AA_0040))
											{
												fixed (ushort* ptr7 = &Unsafe.As<char, ushort>(ref global::_003CModule_003E.PtrToStringChars(s3)))
												{
													try
													{
														if (ptr7 == null)
														{
															fixed (ushort* ptr7 = &Unsafe.As<_0024ArrayType_0024_0024_0024BY00_0024_0024CBG, ushort>(ref global::_003CModule_003E._003F_003F_C_0040_11LOCGONAA_0040_003F_0024AA_003F_0024AA_0040))
															{
																reference2 = ref *(ushort*)null;
																try
																{
																	if (!string.IsNullOrEmpty(text))
																	{
																		fixed (ushort* ptr8 = &Unsafe.As<char, ushort>(ref global::_003CModule_003E.PtrToStringChars(text)))
																		{
																			global::EDownloadContextEvent eDownloadContextEvent = (global::EDownloadContextEvent)(-1);
																			reference3 = ref *(ushort*)null;
																			try
																			{
																				if (clientContextEvent != EDownloadContextEvent.Unknown && !string.IsNullOrEmpty(clientContextEventData))
																				{
																					eDownloadContextEvent = (global::EDownloadContextEvent)clientContextEvent;
																					fixed (ushort* ptr9 = &Unsafe.As<char, ushort>(ref global::_003CModule_003E.PtrToStringChars(clientContextEventData)))
																					{
																						global::_003CModule_003E.CComPtrNtv_003CIContextData_003E_002E_007Bctor_007D(&cComPtrNtv_003CIContextData_003E);
																						try
																						{
																							int num2 = *(int*)m_pService + 60;
																							((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, ushort*, global::EDownloadContextEvent, ushort*, IContextData**, int>)(int)(*(uint*)num2))((nint)m_pService, ptr8, eDownloadContextEvent, ptr9, global::_003CModule_003E.CComPtrNtv_003CIContextData_003E_002E_0026(&cComPtrNtv_003CIContextData_003E));
																							int num3 = *(int*)ptr4 + 20;
																							num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, _GUID*, global::EContentType, ushort*, ushort*, ushort*, IContextData*, int>)(int)(*(uint*)num3))((nint)ptr4, &gUID, eContentType, ptr5, ptr6, ptr7, global::_003CModule_003E.CComPtrNtv_003CIContextData_003E_002E_002EPAUIContextData_0040_0040(&cComPtrNtv_003CIContextData_003E));
																						}
																						catch
																						{
																							//try-fault
																							global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<CComPtrNtv_003CIContextData_003E*, void>)(&global::_003CModule_003E.CComPtrNtv_003CIContextData_003E_002E_007Bdtor_007D), &cComPtrNtv_003CIContextData_003E);
																							throw;
																						}
																						global::_003CModule_003E.CComPtrNtv_003CIContextData_003E_002E_007Bdtor_007D(&cComPtrNtv_003CIContextData_003E);
																					}
																				}
																				else
																				{
																					global::_003CModule_003E.CComPtrNtv_003CIContextData_003E_002E_007Bctor_007D(&cComPtrNtv_003CIContextData_003E);
																					try
																					{
																						int num2 = *(int*)m_pService + 60;
																						((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, ushort*, global::EDownloadContextEvent, ushort*, IContextData**, int>)(int)(*(uint*)num2))((nint)m_pService, ptr8, eDownloadContextEvent, (ushort*)Unsafe.AsPointer(ref reference3), global::_003CModule_003E.CComPtrNtv_003CIContextData_003E_002E_0026(&cComPtrNtv_003CIContextData_003E));
																						int num3 = *(int*)ptr4 + 20;
																						num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, _GUID*, global::EContentType, ushort*, ushort*, ushort*, IContextData*, int>)(int)(*(uint*)num3))((nint)ptr4, &gUID, eContentType, ptr5, ptr6, ptr7, global::_003CModule_003E.CComPtrNtv_003CIContextData_003E_002E_002EPAUIContextData_0040_0040(&cComPtrNtv_003CIContextData_003E));
																					}
																					catch
																					{
																						//try-fault
																						global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<CComPtrNtv_003CIContextData_003E*, void>)(&global::_003CModule_003E.CComPtrNtv_003CIContextData_003E_002E_007Bdtor_007D), &cComPtrNtv_003CIContextData_003E);
																						throw;
																					}
																					global::_003CModule_003E.CComPtrNtv_003CIContextData_003E_002E_007Bdtor_007D(&cComPtrNtv_003CIContextData_003E);
																				}
																			}
																			catch
																			{
																				//try-fault
																				reference3 = ref *(ushort*)null;
																				throw;
																			}
																			reference3 = ref *(ushort*)null;
																		}
																	}
																	else
																	{
																		global::EDownloadContextEvent eDownloadContextEvent = (global::EDownloadContextEvent)(-1);
																		reference3 = ref *(ushort*)null;
																		try
																		{
																			if (clientContextEvent != EDownloadContextEvent.Unknown && !string.IsNullOrEmpty(clientContextEventData))
																			{
																				eDownloadContextEvent = (global::EDownloadContextEvent)clientContextEvent;
																				fixed (ushort* ptr9 = &Unsafe.As<char, ushort>(ref global::_003CModule_003E.PtrToStringChars(clientContextEventData)))
																				{
																					global::_003CModule_003E.CComPtrNtv_003CIContextData_003E_002E_007Bctor_007D(&cComPtrNtv_003CIContextData_003E);
																					try
																					{
																						int num2 = *(int*)m_pService + 60;
																						((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, ushort*, global::EDownloadContextEvent, ushort*, IContextData**, int>)(int)(*(uint*)num2))((nint)m_pService, (ushort*)Unsafe.AsPointer(ref reference2), eDownloadContextEvent, ptr9, global::_003CModule_003E.CComPtrNtv_003CIContextData_003E_002E_0026(&cComPtrNtv_003CIContextData_003E));
																						int num3 = *(int*)ptr4 + 20;
																						num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, _GUID*, global::EContentType, ushort*, ushort*, ushort*, IContextData*, int>)(int)(*(uint*)num3))((nint)ptr4, &gUID, eContentType, ptr5, ptr6, ptr7, global::_003CModule_003E.CComPtrNtv_003CIContextData_003E_002E_002EPAUIContextData_0040_0040(&cComPtrNtv_003CIContextData_003E));
																					}
																					catch
																					{
																						//try-fault
																						global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<CComPtrNtv_003CIContextData_003E*, void>)(&global::_003CModule_003E.CComPtrNtv_003CIContextData_003E_002E_007Bdtor_007D), &cComPtrNtv_003CIContextData_003E);
																						throw;
																					}
																					global::_003CModule_003E.CComPtrNtv_003CIContextData_003E_002E_007Bdtor_007D(&cComPtrNtv_003CIContextData_003E);
																				}
																			}
																			else
																			{
																				global::_003CModule_003E.CComPtrNtv_003CIContextData_003E_002E_007Bctor_007D(&cComPtrNtv_003CIContextData_003E);
																				try
																				{
																					int num2 = *(int*)m_pService + 60;
																					((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, ushort*, global::EDownloadContextEvent, ushort*, IContextData**, int>)(int)(*(uint*)num2))((nint)m_pService, (ushort*)Unsafe.AsPointer(ref reference2), eDownloadContextEvent, (ushort*)Unsafe.AsPointer(ref reference3), global::_003CModule_003E.CComPtrNtv_003CIContextData_003E_002E_0026(&cComPtrNtv_003CIContextData_003E));
																					int num3 = *(int*)ptr4 + 20;
																					num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, _GUID*, global::EContentType, ushort*, ushort*, ushort*, IContextData*, int>)(int)(*(uint*)num3))((nint)ptr4, &gUID, eContentType, ptr5, ptr6, ptr7, global::_003CModule_003E.CComPtrNtv_003CIContextData_003E_002E_002EPAUIContextData_0040_0040(&cComPtrNtv_003CIContextData_003E));
																				}
																				catch
																				{
																					//try-fault
																					global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<CComPtrNtv_003CIContextData_003E*, void>)(&global::_003CModule_003E.CComPtrNtv_003CIContextData_003E_002E_007Bdtor_007D), &cComPtrNtv_003CIContextData_003E);
																					throw;
																				}
																				global::_003CModule_003E.CComPtrNtv_003CIContextData_003E_002E_007Bdtor_007D(&cComPtrNtv_003CIContextData_003E);
																			}
																		}
																		catch
																		{
																			//try-fault
																			reference3 = ref *(ushort*)null;
																			throw;
																		}
																		reference3 = ref *(ushort*)null;
																	}
																}
																catch
																{
																	//try-fault
																	reference2 = ref *(ushort*)null;
																	throw;
																}
																reference2 = ref *(ushort*)null;
															}
															continue;
														}
														reference2 = ref *(ushort*)null;
														try
														{
															if (!string.IsNullOrEmpty(text))
															{
																fixed (ushort* ptr8 = &Unsafe.As<char, ushort>(ref global::_003CModule_003E.PtrToStringChars(text)))
																{
																	global::EDownloadContextEvent eDownloadContextEvent = (global::EDownloadContextEvent)(-1);
																	reference3 = ref *(ushort*)null;
																	try
																	{
																		if (clientContextEvent != EDownloadContextEvent.Unknown && !string.IsNullOrEmpty(clientContextEventData))
																		{
																			eDownloadContextEvent = (global::EDownloadContextEvent)clientContextEvent;
																			fixed (ushort* ptr9 = &Unsafe.As<char, ushort>(ref global::_003CModule_003E.PtrToStringChars(clientContextEventData)))
																			{
																				global::_003CModule_003E.CComPtrNtv_003CIContextData_003E_002E_007Bctor_007D(&cComPtrNtv_003CIContextData_003E);
																				try
																				{
																					int num2 = *(int*)m_pService + 60;
																					((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, ushort*, global::EDownloadContextEvent, ushort*, IContextData**, int>)(int)(*(uint*)num2))((nint)m_pService, ptr8, eDownloadContextEvent, ptr9, global::_003CModule_003E.CComPtrNtv_003CIContextData_003E_002E_0026(&cComPtrNtv_003CIContextData_003E));
																					int num3 = *(int*)ptr4 + 20;
																					num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, _GUID*, global::EContentType, ushort*, ushort*, ushort*, IContextData*, int>)(int)(*(uint*)num3))((nint)ptr4, &gUID, eContentType, ptr5, ptr6, ptr7, global::_003CModule_003E.CComPtrNtv_003CIContextData_003E_002E_002EPAUIContextData_0040_0040(&cComPtrNtv_003CIContextData_003E));
																				}
																				catch
																				{
																					//try-fault
																					global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<CComPtrNtv_003CIContextData_003E*, void>)(&global::_003CModule_003E.CComPtrNtv_003CIContextData_003E_002E_007Bdtor_007D), &cComPtrNtv_003CIContextData_003E);
																					throw;
																				}
																				global::_003CModule_003E.CComPtrNtv_003CIContextData_003E_002E_007Bdtor_007D(&cComPtrNtv_003CIContextData_003E);
																			}
																		}
																		else
																		{
																			global::_003CModule_003E.CComPtrNtv_003CIContextData_003E_002E_007Bctor_007D(&cComPtrNtv_003CIContextData_003E);
																			try
																			{
																				int num2 = *(int*)m_pService + 60;
																				((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, ushort*, global::EDownloadContextEvent, ushort*, IContextData**, int>)(int)(*(uint*)num2))((nint)m_pService, ptr8, eDownloadContextEvent, (ushort*)Unsafe.AsPointer(ref reference3), global::_003CModule_003E.CComPtrNtv_003CIContextData_003E_002E_0026(&cComPtrNtv_003CIContextData_003E));
																				int num3 = *(int*)ptr4 + 20;
																				num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, _GUID*, global::EContentType, ushort*, ushort*, ushort*, IContextData*, int>)(int)(*(uint*)num3))((nint)ptr4, &gUID, eContentType, ptr5, ptr6, ptr7, global::_003CModule_003E.CComPtrNtv_003CIContextData_003E_002E_002EPAUIContextData_0040_0040(&cComPtrNtv_003CIContextData_003E));
																			}
																			catch
																			{
																				//try-fault
																				global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<CComPtrNtv_003CIContextData_003E*, void>)(&global::_003CModule_003E.CComPtrNtv_003CIContextData_003E_002E_007Bdtor_007D), &cComPtrNtv_003CIContextData_003E);
																				throw;
																			}
																			global::_003CModule_003E.CComPtrNtv_003CIContextData_003E_002E_007Bdtor_007D(&cComPtrNtv_003CIContextData_003E);
																		}
																	}
																	catch
																	{
																		//try-fault
																		reference3 = ref *(ushort*)null;
																		throw;
																	}
																	reference3 = ref *(ushort*)null;
																}
															}
															else
															{
																global::EDownloadContextEvent eDownloadContextEvent = (global::EDownloadContextEvent)(-1);
																reference3 = ref *(ushort*)null;
																try
																{
																	if (clientContextEvent != EDownloadContextEvent.Unknown && !string.IsNullOrEmpty(clientContextEventData))
																	{
																		eDownloadContextEvent = (global::EDownloadContextEvent)clientContextEvent;
																		fixed (ushort* ptr9 = &Unsafe.As<char, ushort>(ref global::_003CModule_003E.PtrToStringChars(clientContextEventData)))
																		{
																			global::_003CModule_003E.CComPtrNtv_003CIContextData_003E_002E_007Bctor_007D(&cComPtrNtv_003CIContextData_003E);
																			try
																			{
																				int num2 = *(int*)m_pService + 60;
																				((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, ushort*, global::EDownloadContextEvent, ushort*, IContextData**, int>)(int)(*(uint*)num2))((nint)m_pService, (ushort*)Unsafe.AsPointer(ref reference2), eDownloadContextEvent, ptr9, global::_003CModule_003E.CComPtrNtv_003CIContextData_003E_002E_0026(&cComPtrNtv_003CIContextData_003E));
																				int num3 = *(int*)ptr4 + 20;
																				num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, _GUID*, global::EContentType, ushort*, ushort*, ushort*, IContextData*, int>)(int)(*(uint*)num3))((nint)ptr4, &gUID, eContentType, ptr5, ptr6, ptr7, global::_003CModule_003E.CComPtrNtv_003CIContextData_003E_002E_002EPAUIContextData_0040_0040(&cComPtrNtv_003CIContextData_003E));
																			}
																			catch
																			{
																				//try-fault
																				global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<CComPtrNtv_003CIContextData_003E*, void>)(&global::_003CModule_003E.CComPtrNtv_003CIContextData_003E_002E_007Bdtor_007D), &cComPtrNtv_003CIContextData_003E);
																				throw;
																			}
																			global::_003CModule_003E.CComPtrNtv_003CIContextData_003E_002E_007Bdtor_007D(&cComPtrNtv_003CIContextData_003E);
																		}
																	}
																	else
																	{
																		global::_003CModule_003E.CComPtrNtv_003CIContextData_003E_002E_007Bctor_007D(&cComPtrNtv_003CIContextData_003E);
																		try
																		{
																			int num2 = *(int*)m_pService + 60;
																			((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, ushort*, global::EDownloadContextEvent, ushort*, IContextData**, int>)(int)(*(uint*)num2))((nint)m_pService, (ushort*)Unsafe.AsPointer(ref reference2), eDownloadContextEvent, (ushort*)Unsafe.AsPointer(ref reference3), global::_003CModule_003E.CComPtrNtv_003CIContextData_003E_002E_0026(&cComPtrNtv_003CIContextData_003E));
																			int num3 = *(int*)ptr4 + 20;
																			num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, _GUID*, global::EContentType, ushort*, ushort*, ushort*, IContextData*, int>)(int)(*(uint*)num3))((nint)ptr4, &gUID, eContentType, ptr5, ptr6, ptr7, global::_003CModule_003E.CComPtrNtv_003CIContextData_003E_002E_002EPAUIContextData_0040_0040(&cComPtrNtv_003CIContextData_003E));
																		}
																		catch
																		{
																			//try-fault
																			global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<CComPtrNtv_003CIContextData_003E*, void>)(&global::_003CModule_003E.CComPtrNtv_003CIContextData_003E_002E_007Bdtor_007D), &cComPtrNtv_003CIContextData_003E);
																			throw;
																		}
																		global::_003CModule_003E.CComPtrNtv_003CIContextData_003E_002E_007Bdtor_007D(&cComPtrNtv_003CIContextData_003E);
																	}
																}
																catch
																{
																	//try-fault
																	reference3 = ref *(ushort*)null;
																	throw;
																}
																reference3 = ref *(ushort*)null;
															}
														}
														catch
														{
															//try-fault
															reference2 = ref *(ushort*)null;
															throw;
														}
														reference2 = ref *(ushort*)null;
													}
													catch
													{
														//try-fault
														ptr7 = null;
														throw;
													}
												}
											}
											continue;
										}
										fixed (ushort* ptr7 = &Unsafe.As<char, ushort>(ref global::_003CModule_003E.PtrToStringChars(s3)))
										{
											try
											{
												if (ptr7 == null)
												{
													fixed (ushort* ptr7 = &Unsafe.As<_0024ArrayType_0024_0024_0024BY00_0024_0024CBG, ushort>(ref global::_003CModule_003E._003F_003F_C_0040_11LOCGONAA_0040_003F_0024AA_003F_0024AA_0040))
													{
														reference2 = ref *(ushort*)null;
														try
														{
															if (!string.IsNullOrEmpty(text))
															{
																fixed (ushort* ptr8 = &Unsafe.As<char, ushort>(ref global::_003CModule_003E.PtrToStringChars(text)))
																{
																	global::EDownloadContextEvent eDownloadContextEvent = (global::EDownloadContextEvent)(-1);
																	reference3 = ref *(ushort*)null;
																	try
																	{
																		if (clientContextEvent != EDownloadContextEvent.Unknown && !string.IsNullOrEmpty(clientContextEventData))
																		{
																			eDownloadContextEvent = (global::EDownloadContextEvent)clientContextEvent;
																			fixed (ushort* ptr9 = &Unsafe.As<char, ushort>(ref global::_003CModule_003E.PtrToStringChars(clientContextEventData)))
																			{
																				global::_003CModule_003E.CComPtrNtv_003CIContextData_003E_002E_007Bctor_007D(&cComPtrNtv_003CIContextData_003E);
																				try
																				{
																					int num2 = *(int*)m_pService + 60;
																					((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, ushort*, global::EDownloadContextEvent, ushort*, IContextData**, int>)(int)(*(uint*)num2))((nint)m_pService, ptr8, eDownloadContextEvent, ptr9, global::_003CModule_003E.CComPtrNtv_003CIContextData_003E_002E_0026(&cComPtrNtv_003CIContextData_003E));
																					int num3 = *(int*)ptr4 + 20;
																					num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, _GUID*, global::EContentType, ushort*, ushort*, ushort*, IContextData*, int>)(int)(*(uint*)num3))((nint)ptr4, &gUID, eContentType, ptr5, ptr6, ptr7, global::_003CModule_003E.CComPtrNtv_003CIContextData_003E_002E_002EPAUIContextData_0040_0040(&cComPtrNtv_003CIContextData_003E));
																				}
																				catch
																				{
																					//try-fault
																					global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<CComPtrNtv_003CIContextData_003E*, void>)(&global::_003CModule_003E.CComPtrNtv_003CIContextData_003E_002E_007Bdtor_007D), &cComPtrNtv_003CIContextData_003E);
																					throw;
																				}
																				global::_003CModule_003E.CComPtrNtv_003CIContextData_003E_002E_007Bdtor_007D(&cComPtrNtv_003CIContextData_003E);
																			}
																		}
																		else
																		{
																			global::_003CModule_003E.CComPtrNtv_003CIContextData_003E_002E_007Bctor_007D(&cComPtrNtv_003CIContextData_003E);
																			try
																			{
																				int num2 = *(int*)m_pService + 60;
																				((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, ushort*, global::EDownloadContextEvent, ushort*, IContextData**, int>)(int)(*(uint*)num2))((nint)m_pService, ptr8, eDownloadContextEvent, (ushort*)Unsafe.AsPointer(ref reference3), global::_003CModule_003E.CComPtrNtv_003CIContextData_003E_002E_0026(&cComPtrNtv_003CIContextData_003E));
																				int num3 = *(int*)ptr4 + 20;
																				num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, _GUID*, global::EContentType, ushort*, ushort*, ushort*, IContextData*, int>)(int)(*(uint*)num3))((nint)ptr4, &gUID, eContentType, ptr5, ptr6, ptr7, global::_003CModule_003E.CComPtrNtv_003CIContextData_003E_002E_002EPAUIContextData_0040_0040(&cComPtrNtv_003CIContextData_003E));
																			}
																			catch
																			{
																				//try-fault
																				global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<CComPtrNtv_003CIContextData_003E*, void>)(&global::_003CModule_003E.CComPtrNtv_003CIContextData_003E_002E_007Bdtor_007D), &cComPtrNtv_003CIContextData_003E);
																				throw;
																			}
																			global::_003CModule_003E.CComPtrNtv_003CIContextData_003E_002E_007Bdtor_007D(&cComPtrNtv_003CIContextData_003E);
																		}
																	}
																	catch
																	{
																		//try-fault
																		reference3 = ref *(ushort*)null;
																		throw;
																	}
																	reference3 = ref *(ushort*)null;
																}
															}
															else
															{
																global::EDownloadContextEvent eDownloadContextEvent = (global::EDownloadContextEvent)(-1);
																reference3 = ref *(ushort*)null;
																try
																{
																	if (clientContextEvent != EDownloadContextEvent.Unknown && !string.IsNullOrEmpty(clientContextEventData))
																	{
																		eDownloadContextEvent = (global::EDownloadContextEvent)clientContextEvent;
																		fixed (ushort* ptr9 = &Unsafe.As<char, ushort>(ref global::_003CModule_003E.PtrToStringChars(clientContextEventData)))
																		{
																			global::_003CModule_003E.CComPtrNtv_003CIContextData_003E_002E_007Bctor_007D(&cComPtrNtv_003CIContextData_003E);
																			try
																			{
																				int num2 = *(int*)m_pService + 60;
																				((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, ushort*, global::EDownloadContextEvent, ushort*, IContextData**, int>)(int)(*(uint*)num2))((nint)m_pService, (ushort*)Unsafe.AsPointer(ref reference2), eDownloadContextEvent, ptr9, global::_003CModule_003E.CComPtrNtv_003CIContextData_003E_002E_0026(&cComPtrNtv_003CIContextData_003E));
																				int num3 = *(int*)ptr4 + 20;
																				num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, _GUID*, global::EContentType, ushort*, ushort*, ushort*, IContextData*, int>)(int)(*(uint*)num3))((nint)ptr4, &gUID, eContentType, ptr5, ptr6, ptr7, global::_003CModule_003E.CComPtrNtv_003CIContextData_003E_002E_002EPAUIContextData_0040_0040(&cComPtrNtv_003CIContextData_003E));
																			}
																			catch
																			{
																				//try-fault
																				global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<CComPtrNtv_003CIContextData_003E*, void>)(&global::_003CModule_003E.CComPtrNtv_003CIContextData_003E_002E_007Bdtor_007D), &cComPtrNtv_003CIContextData_003E);
																				throw;
																			}
																			global::_003CModule_003E.CComPtrNtv_003CIContextData_003E_002E_007Bdtor_007D(&cComPtrNtv_003CIContextData_003E);
																		}
																	}
																	else
																	{
																		global::_003CModule_003E.CComPtrNtv_003CIContextData_003E_002E_007Bctor_007D(&cComPtrNtv_003CIContextData_003E);
																		try
																		{
																			int num2 = *(int*)m_pService + 60;
																			((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, ushort*, global::EDownloadContextEvent, ushort*, IContextData**, int>)(int)(*(uint*)num2))((nint)m_pService, (ushort*)Unsafe.AsPointer(ref reference2), eDownloadContextEvent, (ushort*)Unsafe.AsPointer(ref reference3), global::_003CModule_003E.CComPtrNtv_003CIContextData_003E_002E_0026(&cComPtrNtv_003CIContextData_003E));
																			int num3 = *(int*)ptr4 + 20;
																			num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, _GUID*, global::EContentType, ushort*, ushort*, ushort*, IContextData*, int>)(int)(*(uint*)num3))((nint)ptr4, &gUID, eContentType, ptr5, ptr6, ptr7, global::_003CModule_003E.CComPtrNtv_003CIContextData_003E_002E_002EPAUIContextData_0040_0040(&cComPtrNtv_003CIContextData_003E));
																		}
																		catch
																		{
																			//try-fault
																			global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<CComPtrNtv_003CIContextData_003E*, void>)(&global::_003CModule_003E.CComPtrNtv_003CIContextData_003E_002E_007Bdtor_007D), &cComPtrNtv_003CIContextData_003E);
																			throw;
																		}
																		global::_003CModule_003E.CComPtrNtv_003CIContextData_003E_002E_007Bdtor_007D(&cComPtrNtv_003CIContextData_003E);
																	}
																}
																catch
																{
																	//try-fault
																	reference3 = ref *(ushort*)null;
																	throw;
																}
																reference3 = ref *(ushort*)null;
															}
														}
														catch
														{
															//try-fault
															reference2 = ref *(ushort*)null;
															throw;
														}
														reference2 = ref *(ushort*)null;
													}
													continue;
												}
												reference2 = ref *(ushort*)null;
												try
												{
													if (!string.IsNullOrEmpty(text))
													{
														fixed (ushort* ptr8 = &Unsafe.As<char, ushort>(ref global::_003CModule_003E.PtrToStringChars(text)))
														{
															global::EDownloadContextEvent eDownloadContextEvent = (global::EDownloadContextEvent)(-1);
															reference3 = ref *(ushort*)null;
															try
															{
																if (clientContextEvent != EDownloadContextEvent.Unknown && !string.IsNullOrEmpty(clientContextEventData))
																{
																	eDownloadContextEvent = (global::EDownloadContextEvent)clientContextEvent;
																	fixed (ushort* ptr9 = &Unsafe.As<char, ushort>(ref global::_003CModule_003E.PtrToStringChars(clientContextEventData)))
																	{
																		global::_003CModule_003E.CComPtrNtv_003CIContextData_003E_002E_007Bctor_007D(&cComPtrNtv_003CIContextData_003E);
																		try
																		{
																			int num2 = *(int*)m_pService + 60;
																			((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, ushort*, global::EDownloadContextEvent, ushort*, IContextData**, int>)(int)(*(uint*)num2))((nint)m_pService, ptr8, eDownloadContextEvent, ptr9, global::_003CModule_003E.CComPtrNtv_003CIContextData_003E_002E_0026(&cComPtrNtv_003CIContextData_003E));
																			int num3 = *(int*)ptr4 + 20;
																			num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, _GUID*, global::EContentType, ushort*, ushort*, ushort*, IContextData*, int>)(int)(*(uint*)num3))((nint)ptr4, &gUID, eContentType, ptr5, ptr6, ptr7, global::_003CModule_003E.CComPtrNtv_003CIContextData_003E_002E_002EPAUIContextData_0040_0040(&cComPtrNtv_003CIContextData_003E));
																		}
																		catch
																		{
																			//try-fault
																			global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<CComPtrNtv_003CIContextData_003E*, void>)(&global::_003CModule_003E.CComPtrNtv_003CIContextData_003E_002E_007Bdtor_007D), &cComPtrNtv_003CIContextData_003E);
																			throw;
																		}
																		global::_003CModule_003E.CComPtrNtv_003CIContextData_003E_002E_007Bdtor_007D(&cComPtrNtv_003CIContextData_003E);
																	}
																}
																else
																{
																	global::_003CModule_003E.CComPtrNtv_003CIContextData_003E_002E_007Bctor_007D(&cComPtrNtv_003CIContextData_003E);
																	try
																	{
																		int num2 = *(int*)m_pService + 60;
																		((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, ushort*, global::EDownloadContextEvent, ushort*, IContextData**, int>)(int)(*(uint*)num2))((nint)m_pService, ptr8, eDownloadContextEvent, (ushort*)Unsafe.AsPointer(ref reference3), global::_003CModule_003E.CComPtrNtv_003CIContextData_003E_002E_0026(&cComPtrNtv_003CIContextData_003E));
																		int num3 = *(int*)ptr4 + 20;
																		num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, _GUID*, global::EContentType, ushort*, ushort*, ushort*, IContextData*, int>)(int)(*(uint*)num3))((nint)ptr4, &gUID, eContentType, ptr5, ptr6, ptr7, global::_003CModule_003E.CComPtrNtv_003CIContextData_003E_002E_002EPAUIContextData_0040_0040(&cComPtrNtv_003CIContextData_003E));
																	}
																	catch
																	{
																		//try-fault
																		global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<CComPtrNtv_003CIContextData_003E*, void>)(&global::_003CModule_003E.CComPtrNtv_003CIContextData_003E_002E_007Bdtor_007D), &cComPtrNtv_003CIContextData_003E);
																		throw;
																	}
																	global::_003CModule_003E.CComPtrNtv_003CIContextData_003E_002E_007Bdtor_007D(&cComPtrNtv_003CIContextData_003E);
																}
															}
															catch
															{
																//try-fault
																reference3 = ref *(ushort*)null;
																throw;
															}
															reference3 = ref *(ushort*)null;
														}
													}
													else
													{
														global::EDownloadContextEvent eDownloadContextEvent = (global::EDownloadContextEvent)(-1);
														reference3 = ref *(ushort*)null;
														try
														{
															if (clientContextEvent != EDownloadContextEvent.Unknown && !string.IsNullOrEmpty(clientContextEventData))
															{
																eDownloadContextEvent = (global::EDownloadContextEvent)clientContextEvent;
																fixed (ushort* ptr9 = &Unsafe.As<char, ushort>(ref global::_003CModule_003E.PtrToStringChars(clientContextEventData)))
																{
																	global::_003CModule_003E.CComPtrNtv_003CIContextData_003E_002E_007Bctor_007D(&cComPtrNtv_003CIContextData_003E);
																	try
																	{
																		int num2 = *(int*)m_pService + 60;
																		((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, ushort*, global::EDownloadContextEvent, ushort*, IContextData**, int>)(int)(*(uint*)num2))((nint)m_pService, (ushort*)Unsafe.AsPointer(ref reference2), eDownloadContextEvent, ptr9, global::_003CModule_003E.CComPtrNtv_003CIContextData_003E_002E_0026(&cComPtrNtv_003CIContextData_003E));
																		int num3 = *(int*)ptr4 + 20;
																		num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, _GUID*, global::EContentType, ushort*, ushort*, ushort*, IContextData*, int>)(int)(*(uint*)num3))((nint)ptr4, &gUID, eContentType, ptr5, ptr6, ptr7, global::_003CModule_003E.CComPtrNtv_003CIContextData_003E_002E_002EPAUIContextData_0040_0040(&cComPtrNtv_003CIContextData_003E));
																	}
																	catch
																	{
																		//try-fault
																		global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<CComPtrNtv_003CIContextData_003E*, void>)(&global::_003CModule_003E.CComPtrNtv_003CIContextData_003E_002E_007Bdtor_007D), &cComPtrNtv_003CIContextData_003E);
																		throw;
																	}
																	global::_003CModule_003E.CComPtrNtv_003CIContextData_003E_002E_007Bdtor_007D(&cComPtrNtv_003CIContextData_003E);
																}
															}
															else
															{
																global::_003CModule_003E.CComPtrNtv_003CIContextData_003E_002E_007Bctor_007D(&cComPtrNtv_003CIContextData_003E);
																try
																{
																	int num2 = *(int*)m_pService + 60;
																	((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, ushort*, global::EDownloadContextEvent, ushort*, IContextData**, int>)(int)(*(uint*)num2))((nint)m_pService, (ushort*)Unsafe.AsPointer(ref reference2), eDownloadContextEvent, (ushort*)Unsafe.AsPointer(ref reference3), global::_003CModule_003E.CComPtrNtv_003CIContextData_003E_002E_0026(&cComPtrNtv_003CIContextData_003E));
																	int num3 = *(int*)ptr4 + 20;
																	num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, _GUID*, global::EContentType, ushort*, ushort*, ushort*, IContextData*, int>)(int)(*(uint*)num3))((nint)ptr4, &gUID, eContentType, ptr5, ptr6, ptr7, global::_003CModule_003E.CComPtrNtv_003CIContextData_003E_002E_002EPAUIContextData_0040_0040(&cComPtrNtv_003CIContextData_003E));
																}
																catch
																{
																	//try-fault
																	global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<CComPtrNtv_003CIContextData_003E*, void>)(&global::_003CModule_003E.CComPtrNtv_003CIContextData_003E_002E_007Bdtor_007D), &cComPtrNtv_003CIContextData_003E);
																	throw;
																}
																global::_003CModule_003E.CComPtrNtv_003CIContextData_003E_002E_007Bdtor_007D(&cComPtrNtv_003CIContextData_003E);
															}
														}
														catch
														{
															//try-fault
															reference3 = ref *(ushort*)null;
															throw;
														}
														reference3 = ref *(ushort*)null;
													}
												}
												catch
												{
													//try-fault
													reference2 = ref *(ushort*)null;
													throw;
												}
												reference2 = ref *(ushort*)null;
											}
											catch
											{
												//try-fault
												ptr7 = null;
												throw;
											}
										}
									}
									catch
									{
										//try-fault
										ptr6 = null;
										throw;
									}
								}
							}
							catch
							{
								//try-fault
								ptr5 = null;
								throw;
							}
						}
					}
				}
				if (num >= 0)
				{
					int num4 = *(int*)m_pService + 304;
					((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, IMediaCollection*, global::EDownloadFlags, int, ushort*, int, IDownloadCallback*, int>)(int)(*(uint*)num4))((nint)m_pService, ptr4, (global::EDownloadFlags)eDownloadFlags, -1, ptr10, 1, (IDownloadCallback*)ptr3);
				}
				if (ptr4 != null)
				{
					intPtr = ptr4;
					((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint>)(int)(*(uint*)(*(int*)intPtr + 8)))((nint)intPtr);
				}
				if (ptr3 != null)
				{
					intPtr2 = ptr3;
					((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint>)(int)(*(uint*)(*(int*)intPtr2 + 8)))((nint)intPtr2);
				}
			}
			goto end_IL_0011;
			IL_06b0:
			if (ptr3 != null)
			{
				intPtr2 = ptr3;
				((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint>)(int)(*(uint*)(*(int*)intPtr2 + 8)))((nint)intPtr2);
			}
			goto end_IL_0011;
			IL_069f:
			if (ptr4 != null)
			{
				intPtr = ptr4;
				((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint>)(int)(*(uint*)(*(int*)intPtr + 8)))((nint)intPtr);
			}
			goto IL_06b0;
			end_IL_0011:;
		}
		catch
		{
			//try-fault
			reference = ref *(ushort*)null;
			throw;
		}
		reference = ref *(ushort*)null;
	}

	[return: MarshalAs(UnmanagedType.U1)]
	public unsafe bool IsDownloading(Guid guidMediaId, EContentType eContentType, out bool fIsDownloadPending, out bool fIsHidden)
	{
		bool result = false;
		if (m_pService != null)
		{
			int num = 0;
			int num2 = 0;
			int num3 = 0;
			_GUID gUID = global::_003CModule_003E.GuidToGUID(guidMediaId);
			IService* pService = m_pService;
			if (((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, _GUID, global::EContentType, int*, int*, int*, int>)(int)(*(uint*)(*(int*)pService + 308)))((nint)pService, gUID, (global::EContentType)eContentType, &num, &num2, &num3) >= 0)
			{
				bool flag = ((num != 0 || num2 != 0) ? true : false);
				result = flag;
				bool flag2 = ((num2 != 0) ? true : false);
				fIsDownloadPending = flag2;
				bool flag3 = ((num3 != 0) ? true : false);
				fIsHidden = flag3;
			}
		}
		return result;
	}

	public unsafe void CancelDownload(Guid guidMediaId, EContentType eContentType)
	{
		if (m_pService != null)
		{
			_GUID gUID = global::_003CModule_003E.GuidToGUID(guidMediaId);
			IService* pService = m_pService;
			((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, _GUID, global::EContentType, int>)(int)(*(uint*)(*(int*)pService + 312)))((nint)pService, gUID, (global::EContentType)eContentType);
		}
	}

	public unsafe HRESULT GetContentUri(Guid guidMediaId, EContentType eContentType, EContentUriFlags eContentUriFlags, EMediaFormat eMediaFormat, EMediaRights eMediaRights, out string uriOut, out Guid mediaInstanceIdOut)
	{
		int num = 0;
		string text = null;
		_GUID gUID_NULL = global::_003CModule_003E.GUID_NULL;
		if (m_pService != null)
		{
			ushort* ptr = null;
			_GUID gUID = global::_003CModule_003E.GuidToGUID(guidMediaId);
			IService* pService = m_pService;
			num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, _GUID, global::EContentType, global::EContentUriFlags, EMediaFormat, EMediaRights, ushort**, _GUID*, int>)(int)(*(uint*)(*(int*)pService + 292)))((nint)pService, gUID, (global::EContentType)eContentType, (global::EContentUriFlags)eContentUriFlags, eMediaFormat, eMediaRights, &ptr, &gUID_NULL);
			if (num >= 0)
			{
				if (ptr == null)
				{
					goto IL_005c;
				}
				text = Marshal.PtrToStringBSTR((IntPtr)ptr);
			}
			if (ptr != null)
			{
				global::_003CModule_003E.SysFreeString(ptr);
			}
		}
		goto IL_005c;
		IL_005c:
		uriOut = text;
		Guid guid = global::_003CModule_003E.GUIDToGuid(gUID_NULL);
		mediaInstanceIdOut = guid;
		return num;
	}

	public HRESULT GetContentUri(Guid guidMediaId, EContentType eContentType, EContentUriFlags eContentUriFlags, [MarshalAs(UnmanagedType.U1)] bool fIsHD, [MarshalAs(UnmanagedType.U1)] bool fIsRental, out string uriOut)
	{
		EMediaFormat eMediaFormat = (fIsHD ? ((EMediaFormat)2) : ((EMediaFormat)3));
		EMediaRights eMediaRights = (fIsRental ? ((EMediaRights)9) : ((EMediaRights)8));
		Guid mediaInstanceIdOut = default(Guid);
		return GetContentUri(guidMediaId, eContentType, eContentUriFlags, eMediaFormat, eMediaRights, out uriOut, out mediaInstanceIdOut);
	}

	public HRESULT GetContentUri(Guid guidMediaId, EContentType eContentType, EContentUriFlags eContentUriFlags, out string uriOut, out Guid mediaInstanceIdOut)
	{
		return GetContentUri(guidMediaId, eContentType, eContentUriFlags, (EMediaFormat)(-1), (EMediaRights)(-1), out uriOut, out mediaInstanceIdOut);
	}

	[return: MarshalAs(UnmanagedType.U1)]
	public bool InCompleteCollection(Guid guidMediaId, EContentType eContentType)
	{
		int dbMediaId;
		bool fHidden;
		return InCompleteCollection(guidMediaId, eContentType, null, out dbMediaId, out fHidden);
	}

	[return: MarshalAs(UnmanagedType.U1)]
	public bool InCompleteCollection(Guid guidMediaId, EContentType eContentType, string strDeviceEndpointId)
	{
		int dbMediaId;
		bool fHidden;
		return InCompleteCollection(guidMediaId, eContentType, strDeviceEndpointId, out dbMediaId, out fHidden);
	}

	[return: MarshalAs(UnmanagedType.U1)]
	public bool InCompleteCollection(Guid guidMediaId, EContentType eContentType, out int dbMediaId, out bool fHidden)
	{
		return InCompleteCollection(guidMediaId, eContentType, null, out dbMediaId, out fHidden);
	}

	[return: MarshalAs(UnmanagedType.U1)]
	public unsafe bool InCompleteCollection(Guid guidMediaId, EContentType eContentType, string strDeviceEndpointId, out int dbMediaId, out bool fHidden)
	{
		bool result = false;
		int num = -1;
		if (m_pService != null)
		{
			int num2 = 0;
			int num3 = 0;
			fixed (ushort* ptr = &Unsafe.As<char, ushort>(ref global::_003CModule_003E.PtrToStringChars(strDeviceEndpointId)))
			{
				try
				{
					_GUID gUID = global::_003CModule_003E.GuidToGUID(guidMediaId);
					int num4 = *(int*)m_pService + 320;
					if (((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, _GUID, global::EContentType, ushort*, int*, int*, int*, int>)(int)(*(uint*)num4))((nint)m_pService, gUID, (global::EContentType)eContentType, ptr, &num2, &num, &num3) >= 0)
					{
						bool flag = ((num2 != 0) ? true : false);
						result = flag;
						bool flag2 = ((num3 != 0) ? true : false);
						fHidden = flag2;
					}
				}
				catch
				{
					//try-fault
					ptr = null;
					throw;
				}
			}
		}
		dbMediaId = num;
		return result;
	}

	[return: MarshalAs(UnmanagedType.U1)]
	public bool InVisibleCollection(Guid guidMediaId, EContentType eContentType)
	{
		if (InCompleteCollection(guidMediaId, eContentType, null, out var _, out var fHidden))
		{
			return !fHidden;
		}
		return false;
	}

	[return: MarshalAs(UnmanagedType.U1)]
	public bool InVisibleCollection(Guid guidMediaId, EContentType eContentType, out int dbMediaId)
	{
		if (InCompleteCollection(guidMediaId, eContentType, null, out dbMediaId, out var fHidden))
		{
			return !fHidden;
		}
		return false;
	}

	[return: MarshalAs(UnmanagedType.U1)]
	public bool InHiddenCollection(Guid guidMediaId, EContentType eContentType)
	{
		int dbMediaId;
		bool fHidden;
		return InCompleteCollection(guidMediaId, eContentType, null, out dbMediaId, out fHidden) && fHidden;
	}

	[return: MarshalAs(UnmanagedType.U1)]
	public unsafe bool SetUserTrackRating(int iUserId, int iRating, Guid guidTrackMediaId, Guid guidAlbumMediaId, int iTrackNumber, string strTitle, int msDuration, string strAlbum, string strArtist, string strGenre, string strServiceContext)
	{
		bool result = false;
		if (m_pService != null)
		{
			fixed (ushort* ptr = &Unsafe.As<char, ushort>(ref global::_003CModule_003E.PtrToStringChars(strTitle)))
			{
				try
				{
					fixed (ushort* ptr2 = &Unsafe.As<char, ushort>(ref global::_003CModule_003E.PtrToStringChars(strAlbum)))
					{
						try
						{
							fixed (ushort* ptr3 = &Unsafe.As<char, ushort>(ref global::_003CModule_003E.PtrToStringChars(strArtist)))
							{
								try
								{
									fixed (ushort* ptr4 = &Unsafe.As<char, ushort>(ref global::_003CModule_003E.PtrToStringChars(strGenre)))
									{
										try
										{
											fixed (ushort* ptr5 = &Unsafe.As<char, ushort>(ref global::_003CModule_003E.PtrToStringChars(strServiceContext)))
											{
												try
												{
													Unsafe.SkipInit(out CComPtrNtv_003CIContextData_003E cComPtrNtv_003CIContextData_003E);
													*(int*)(&cComPtrNtv_003CIContextData_003E) = 0;
													try
													{
														int num = *(int*)m_pService + 60;
														((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, ushort*, global::EDownloadContextEvent, ushort*, IContextData**, int>)(int)(*(uint*)num))((nint)m_pService, ptr5, (global::EDownloadContextEvent)(-1), null, (IContextData**)(&cComPtrNtv_003CIContextData_003E));
														_GUID gUID = global::_003CModule_003E.GuidToGUID(guidAlbumMediaId);
														_GUID gUID2 = global::_003CModule_003E.GuidToGUID(guidTrackMediaId);
														int num2 = *(int*)m_pService + 452;
														result = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, int, int, _GUID*, _GUID*, int, ushort*, int, ushort*, ushort*, ushort*, IContextData*, int>)(int)(*(uint*)num2))((nint)m_pService, iUserId, iRating, &gUID2, &gUID, iTrackNumber, ptr, msDuration, ptr2, ptr3, ptr4, (IContextData*)(int)(*(uint*)(&cComPtrNtv_003CIContextData_003E))) >= 0;
													}
													catch
													{
														//try-fault
														global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<CComPtrNtv_003CIContextData_003E*, void>)(&global::_003CModule_003E.CComPtrNtv_003CIContextData_003E_002E_007Bdtor_007D), &cComPtrNtv_003CIContextData_003E);
														throw;
													}
													global::_003CModule_003E.CComPtrNtv_003CIContextData_003E_002ERelease(&cComPtrNtv_003CIContextData_003E);
												}
												catch
												{
													//try-fault
													ptr5 = null;
													throw;
												}
											}
										}
										catch
										{
											//try-fault
											ptr4 = null;
											throw;
										}
									}
								}
								catch
								{
									//try-fault
									ptr3 = null;
									throw;
								}
							}
						}
						catch
						{
							//try-fault
							ptr2 = null;
							throw;
						}
					}
				}
				catch
				{
					//try-fault
					ptr = null;
					throw;
				}
			}
		}
		return result;
	}

	[return: MarshalAs(UnmanagedType.U1)]
	public unsafe bool SetUserArtistRating(int iUserId, int iRating, Guid guidArtistMediaId, string strTitle)
	{
		bool result = false;
		if (m_pService != null)
		{
			fixed (ushort* ptr = &Unsafe.As<char, ushort>(ref global::_003CModule_003E.PtrToStringChars(strTitle)))
			{
				try
				{
					_GUID gUID = global::_003CModule_003E.GuidToGUID(guidArtistMediaId);
					int num = *(int*)m_pService + 456;
					result = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, int, int, _GUID*, ushort*, int>)(int)(*(uint*)num))((nint)m_pService, iUserId, iRating, &gUID, ptr) >= 0;
				}
				catch
				{
					//try-fault
					ptr = null;
					throw;
				}
			}
		}
		return result;
	}

	[return: MarshalAs(UnmanagedType.U1)]
	public unsafe bool GetUserRating(int iUserId, Guid guidMediaId, EContentType eContentType, [In][Out] ref int piRating)
	{
		bool flag = false;
		if (m_pService != null)
		{
			_GUID gUID = global::_003CModule_003E.GuidToGUID(guidMediaId);
			IService* pService = m_pService;
			Unsafe.SkipInit(out int num);
			flag = 0 == ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, int, _GUID*, global::EContentType, int*, int>)(int)(*(uint*)(*(int*)pService + 460)))((nint)pService, iUserId, &gUID, (global::EContentType)eContentType, &num);
			if (flag)
			{
				piRating = num;
			}
		}
		return flag;
	}

	[return: MarshalAs(UnmanagedType.U1)]
	public unsafe bool DeleteSubscriptionDownloads(AsyncCompleteHandler eventHandler)
	{
		bool result = false;
		if (m_pService != null)
		{
			AsyncCallbackWrapper* ptr = (AsyncCallbackWrapper*)global::_003CModule_003E.@new(12u);
			AsyncCallbackWrapper* ptr2;
			try
			{
				ptr2 = ((ptr == null) ? null : global::_003CModule_003E.Microsoft_002EZune_002EUtil_002EAsyncCallbackWrapper_002E_007Bctor_007D(ptr, eventHandler));
			}
			catch
			{
				//try-fault
				global::_003CModule_003E.delete(ptr);
				throw;
			}
			if (ptr2 != null)
			{
				IService* pService = m_pService;
				int num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, IAsyncCallback*, int>)(int)(*(uint*)(*(int*)pService + 324)))((nint)pService, (IAsyncCallback*)ptr2);
				((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint>)(int)(*(uint*)(*(int*)ptr2 + 8)))((nint)ptr2);
				if (num >= 0)
				{
					result = true;
				}
			}
		}
		return result;
	}

	public unsafe string GetSubscriptionDirectory()
	{
		string result = null;
		IService* pService = m_pService;
		if (pService != null)
		{
			ushort* ptr = null;
			if (((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, ushort**, int>)(int)(*(uint*)(*(int*)pService + 328)))((nint)pService, &ptr) >= 0)
			{
				result = Marshal.PtrToStringBSTR((IntPtr)ptr);
			}
			if (ptr != null)
			{
				global::_003CModule_003E.SysFreeString(ptr);
			}
		}
		return result;
	}

	public static EListType ContentTypeToListType(EContentType contentType)
	{
		int result;
		switch (contentType)
		{
		case EContentType.MusicTrack:
			return EListType.eTrackList;
		case EContentType.MusicAlbum:
			return EListType.eAlbumList;
		case EContentType.Video:
			return EListType.eVideoList;
		case EContentType.PodcastEpisode:
			return EListType.ePodcastEpisodeList;
		case EContentType.PodcastSeries:
			return EListType.ePodcastList;
		case EContentType.Artist:
			return EListType.eArtistList;
		default:
			result = 23;
			break;
		case EContentType.App:
			result = 20;
			break;
		}
		return (EListType)result;
	}

	public unsafe EMediaStatus GetMediaStatus(Guid guidMediaId, EContentType eContentType)
	{
		int dbMediaId = -1;
		bool fIsDownloadPending = false;
		EMediaStatus result = EMediaStatus.StatusNotAvailable;
		bool fIsHidden;
		if (InVisibleCollection(guidMediaId, eContentType, out dbMediaId))
		{
			EMediaTypes eMediaTypes = EMediaTypes.eMediaTypeInvalid;
			if (eContentType == EContentType.Video)
			{
				Unsafe.SkipInit(out _0024ArrayType_0024_0024_0024BY00UDBPropertyRequestStruct_0040_0040 _0024ArrayType_0024_0024_0024BY00UDBPropertyRequestStruct_0040_00402);
				global::_003CModule_003E.DBPropertyRequestStruct_002E_007Bctor_007D((DBPropertyRequestStruct*)(&_0024ArrayType_0024_0024_0024BY00UDBPropertyRequestStruct_0040_00402), 177u);
				try
				{
					if (global::_003CModule_003E.ZuneLibraryExports_002EGetFieldValues(dbMediaId, ContentTypeToListType(EContentType.Video), 1, (DBPropertyRequestStruct*)(&_0024ArrayType_0024_0024_0024BY00UDBPropertyRequestStruct_0040_00402), null) >= 0 && Unsafe.As<_0024ArrayType_0024_0024_0024BY00UDBPropertyRequestStruct_0040_0040, int>(ref Unsafe.AddByteOffset(ref _0024ArrayType_0024_0024_0024BY00UDBPropertyRequestStruct_0040_00402, 4)) >= 0 && Unsafe.As<_0024ArrayType_0024_0024_0024BY00UDBPropertyRequestStruct_0040_0040, ushort>(ref Unsafe.AddByteOffset(ref _0024ArrayType_0024_0024_0024BY00UDBPropertyRequestStruct_0040_00402, 8)) == 3)
					{
						eMediaTypes = Unsafe.As<_0024ArrayType_0024_0024_0024BY00UDBPropertyRequestStruct_0040_0040, EMediaTypes>(ref Unsafe.AddByteOffset(ref _0024ArrayType_0024_0024_0024BY00UDBPropertyRequestStruct_0040_00402, 16));
					}
				}
				catch
				{
					//try-fault
					global::_003CModule_003E.___CxxCallUnwindVecDtor((delegate*<void*, uint, int, delegate*<void*, void>, void>)(&global::_003CModule_003E.__ehvec_dtor), (void*)(&_0024ArrayType_0024_0024_0024BY00UDBPropertyRequestStruct_0040_00402), 24u, 1, (delegate*<void*, void>)(delegate*<DBPropertyRequestStruct*, void>)(&global::_003CModule_003E.DBPropertyRequestStruct_002E_007Bdtor_007D));
					throw;
				}
				global::_003CModule_003E.__ehvec_dtor(&_0024ArrayType_0024_0024_0024BY00UDBPropertyRequestStruct_0040_00402, 24u, 1, (delegate*<void*, void>)(delegate*<DBPropertyRequestStruct*, void>)(&global::_003CModule_003E.DBPropertyRequestStruct_002E_007Bdtor_007D));
				if (eMediaTypes == EMediaTypes.eMediaTypeVideoMBR)
				{
					if (IsDownloading(guidMediaId, EContentType.Video, out fIsDownloadPending, out fIsHidden))
					{
						EMediaStatus eMediaStatus = (fIsDownloadPending ? EMediaStatus.StatusDownloadPending : EMediaStatus.StatusDownloading);
						result = eMediaStatus;
					}
					else
					{
						result = EMediaStatus.StatusInCollectionShortcut;
					}
					goto IL_0168;
				}
			}
			Unsafe.SkipInit(out _0024ArrayType_0024_0024_0024BY00UDBPropertyRequestStruct_0040_0040 _0024ArrayType_0024_0024_0024BY00UDBPropertyRequestStruct_0040_00403);
			global::_003CModule_003E.DBPropertyRequestStruct_002E_007Bctor_007D((DBPropertyRequestStruct*)(&_0024ArrayType_0024_0024_0024BY00UDBPropertyRequestStruct_0040_00403), 149u);
			try
			{
				if (global::_003CModule_003E.ZuneLibraryExports_002EGetFieldValues(dbMediaId, ContentTypeToListType(eContentType), 1, (DBPropertyRequestStruct*)(&_0024ArrayType_0024_0024_0024BY00UDBPropertyRequestStruct_0040_00403), null) < 0 || Unsafe.As<_0024ArrayType_0024_0024_0024BY00UDBPropertyRequestStruct_0040_0040, int>(ref Unsafe.AddByteOffset(ref _0024ArrayType_0024_0024_0024BY00UDBPropertyRequestStruct_0040_00403, 4)) < 0 || Unsafe.As<_0024ArrayType_0024_0024_0024BY00UDBPropertyRequestStruct_0040_0040, ushort>(ref Unsafe.AddByteOffset(ref _0024ArrayType_0024_0024_0024BY00UDBPropertyRequestStruct_0040_00403, 8)) != 3)
				{
					goto IL_0113;
				}
				if (Unsafe.As<_0024ArrayType_0024_0024_0024BY00UDBPropertyRequestStruct_0040_0040, int>(ref Unsafe.AddByteOffset(ref _0024ArrayType_0024_0024_0024BY00UDBPropertyRequestStruct_0040_00403, 16)) != 10)
				{
					if (Unsafe.As<_0024ArrayType_0024_0024_0024BY00UDBPropertyRequestStruct_0040_0040, int>(ref Unsafe.AddByteOffset(ref _0024ArrayType_0024_0024_0024BY00UDBPropertyRequestStruct_0040_00403, 16)) != 20 && Unsafe.As<_0024ArrayType_0024_0024_0024BY00UDBPropertyRequestStruct_0040_0040, int>(ref Unsafe.AddByteOffset(ref _0024ArrayType_0024_0024_0024BY00UDBPropertyRequestStruct_0040_00403, 16)) != 26)
					{
						if (Unsafe.As<_0024ArrayType_0024_0024_0024BY00UDBPropertyRequestStruct_0040_0040, int>(ref Unsafe.AddByteOffset(ref _0024ArrayType_0024_0024_0024BY00UDBPropertyRequestStruct_0040_00403, 16)) != 30 && Unsafe.As<_0024ArrayType_0024_0024_0024BY00UDBPropertyRequestStruct_0040_0040, int>(ref Unsafe.AddByteOffset(ref _0024ArrayType_0024_0024_0024BY00UDBPropertyRequestStruct_0040_00403, 16)) != 40)
						{
							goto IL_0113;
						}
						result = EMediaStatus.StatusInCollectionOwned;
					}
					else
					{
						result = EMediaStatus.StatusInCollectionExpiring;
					}
				}
				else
				{
					result = EMediaStatus.StatusInCollectionNoLicense;
				}
				goto end_IL_00be;
				IL_0113:
				result = EMediaStatus.StatusInCollectionUnknown;
				end_IL_00be:;
			}
			catch
			{
				//try-fault
				global::_003CModule_003E.___CxxCallUnwindVecDtor((delegate*<void*, uint, int, delegate*<void*, void>, void>)(&global::_003CModule_003E.__ehvec_dtor), (void*)(&_0024ArrayType_0024_0024_0024BY00UDBPropertyRequestStruct_0040_00403), 24u, 1, (delegate*<void*, void>)(delegate*<DBPropertyRequestStruct*, void>)(&global::_003CModule_003E.DBPropertyRequestStruct_002E_007Bdtor_007D));
				throw;
			}
			global::_003CModule_003E.__ehvec_dtor(&_0024ArrayType_0024_0024_0024BY00UDBPropertyRequestStruct_0040_00403, 24u, 1, (delegate*<void*, void>)(delegate*<DBPropertyRequestStruct*, void>)(&global::_003CModule_003E.DBPropertyRequestStruct_002E_007Bdtor_007D));
		}
		else if (IsDownloading(guidMediaId, eContentType, out fIsDownloadPending, out fIsHidden))
		{
			EMediaStatus eMediaStatus2 = (fIsDownloadPending ? EMediaStatus.StatusDownloadPending : EMediaStatus.StatusDownloading);
			result = eMediaStatus2;
		}
		goto IL_0168;
		IL_0168:
		return result;
	}

	public unsafe string GetZuneTag()
	{
		string result = null;
		if (m_pService != null && IsSignedIn())
		{
			Unsafe.SkipInit(out CComPtrNtv_003CISignInState_003E cComPtrNtv_003CISignInState_003E);
			*(int*)(&cComPtrNtv_003CISignInState_003E) = 0;
			try
			{
				IService* pService = m_pService;
				int num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, ISignInState**, int>)(int)(*(uint*)(*(int*)pService + 172)))((nint)pService, (ISignInState**)(&cComPtrNtv_003CISignInState_003E));
				ushort* ptr = null;
				if (num >= 0)
				{
					num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, ushort**, int>)(int)(*(uint*)(*(int*)(int)(*(uint*)(&cComPtrNtv_003CISignInState_003E)) + 48)))((IntPtr)(*(int*)(&cComPtrNtv_003CISignInState_003E)), &ptr);
					if (num < 0)
					{
						goto IL_0062;
					}
					if (ptr != null)
					{
						result = Marshal.PtrToStringBSTR((IntPtr)ptr);
						goto IL_0062;
					}
				}
				goto end_IL_0019;
				IL_0062:
				if (ptr != null)
				{
					global::_003CModule_003E.SysFreeString(ptr);
				}
				end_IL_0019:;
			}
			catch
			{
				//try-fault
				global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<CComPtrNtv_003CISignInState_003E*, void>)(&global::_003CModule_003E.CComPtrNtv_003CISignInState_003E_002E_007Bdtor_007D), &cComPtrNtv_003CISignInState_003E);
				throw;
			}
			global::_003CModule_003E.CComPtrNtv_003CISignInState_003E_002ERelease(&cComPtrNtv_003CISignInState_003E);
		}
		return result;
	}

	public unsafe string GetPassportTicket(EPassportPolicyId ePassportPolicy)
	{
		string result = null;
		if (m_pService != null && IsSignedIn())
		{
			Unsafe.SkipInit(out CComPtrNtv_003CISignInState_003E cComPtrNtv_003CISignInState_003E);
			*(int*)(&cComPtrNtv_003CISignInState_003E) = 0;
			try
			{
				IService* pService = m_pService;
				int num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, ISignInState**, int>)(int)(*(uint*)(*(int*)pService + 172)))((nint)pService, (ISignInState**)(&cComPtrNtv_003CISignInState_003E));
				ushort* ptr = null;
				if (num >= 0)
				{
					num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, EPassportPolicy, ushort**, int>)(int)(*(uint*)(*(int*)(int)(*(uint*)(&cComPtrNtv_003CISignInState_003E)) + 20)))((IntPtr)(*(int*)(&cComPtrNtv_003CISignInState_003E)), (EPassportPolicy)ePassportPolicy, &ptr);
					if (num < 0)
					{
						goto IL_0063;
					}
					if (ptr != null)
					{
						result = Marshal.PtrToStringBSTR((IntPtr)ptr);
						goto IL_0063;
					}
				}
				goto end_IL_0019;
				IL_0063:
				if (ptr != null)
				{
					global::_003CModule_003E.SysFreeString(ptr);
				}
				end_IL_0019:;
			}
			catch
			{
				//try-fault
				global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<CComPtrNtv_003CISignInState_003E*, void>)(&global::_003CModule_003E.CComPtrNtv_003CISignInState_003E_002E_007Bdtor_007D), &cComPtrNtv_003CISignInState_003E);
				throw;
			}
			global::_003CModule_003E.CComPtrNtv_003CISignInState_003E_002ERelease(&cComPtrNtv_003CISignInState_003E);
		}
		return result;
	}

	public unsafe HRESULT AuthenticatePassport(string username, string password, EPassportPolicyId ePassportPolicyId, out PassportIdentity passportIdentity)
	{
		int num = -2147467259;
		passportIdentity = null;
		if (m_pService != null)
		{
			fixed (ushort* ptr = &Unsafe.As<char, ushort>(ref global::_003CModule_003E.PtrToStringChars(username)))
			{
				try
				{
					fixed (ushort* ptr2 = &Unsafe.As<char, ushort>(ref global::_003CModule_003E.PtrToStringChars(password)))
					{
						try
						{
							Unsafe.SkipInit(out CComPtrNtv_003CIPassportIdentity_003E cComPtrNtv_003CIPassportIdentity_003E);
							*(int*)(&cComPtrNtv_003CIPassportIdentity_003E) = 0;
							try
							{
								int num2 = *(int*)m_pService + 220;
								num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, ushort*, ushort*, EPassportPolicy, IPassportIdentity**, int>)(int)(*(uint*)num2))((nint)m_pService, ptr, ptr2, (EPassportPolicy)ePassportPolicyId, (IPassportIdentity**)(&cComPtrNtv_003CIPassportIdentity_003E));
								if (num >= 0)
								{
									passportIdentity = new PassportIdentity((IPassportIdentity*)(int)(*(uint*)(&cComPtrNtv_003CIPassportIdentity_003E)));
								}
							}
							catch
							{
								//try-fault
								global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<CComPtrNtv_003CIPassportIdentity_003E*, void>)(&global::_003CModule_003E.CComPtrNtv_003CIPassportIdentity_003E_002E_007Bdtor_007D), &cComPtrNtv_003CIPassportIdentity_003E);
								throw;
							}
							global::_003CModule_003E.CComPtrNtv_003CIPassportIdentity_003E_002ERelease(&cComPtrNtv_003CIPassportIdentity_003E);
						}
						catch
						{
							//try-fault
							ptr2 = null;
							throw;
						}
					}
				}
				catch
				{
					//try-fault
					ptr = null;
					throw;
				}
			}
		}
		return new HRESULT(num);
	}

	public unsafe string GetXboxTicket()
	{
		string result = null;
		if (m_pService != null && IsSignedIn())
		{
			Unsafe.SkipInit(out CComPtrNtv_003CISignInState_003E cComPtrNtv_003CISignInState_003E);
			*(int*)(&cComPtrNtv_003CISignInState_003E) = 0;
			try
			{
				IService* pService = m_pService;
				int num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, ISignInState**, int>)(int)(*(uint*)(*(int*)pService + 172)))((nint)pService, (ISignInState**)(&cComPtrNtv_003CISignInState_003E));
				ushort* ptr = null;
				if (num >= 0)
				{
					num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, ushort**, int>)(int)(*(uint*)(*(int*)(int)(*(uint*)(&cComPtrNtv_003CISignInState_003E)) + 16)))((IntPtr)(*(int*)(&cComPtrNtv_003CISignInState_003E)), &ptr);
					if (num < 0)
					{
						goto IL_0062;
					}
					if (ptr != null)
					{
						result = Marshal.PtrToStringBSTR((IntPtr)ptr);
						goto IL_0062;
					}
				}
				goto end_IL_0019;
				IL_0062:
				if (ptr != null)
				{
					global::_003CModule_003E.SysFreeString(ptr);
				}
				end_IL_0019:;
			}
			catch
			{
				//try-fault
				global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<CComPtrNtv_003CISignInState_003E*, void>)(&global::_003CModule_003E.CComPtrNtv_003CISignInState_003E_002E_007Bdtor_007D), &cComPtrNtv_003CISignInState_003E);
				throw;
			}
			global::_003CModule_003E.CComPtrNtv_003CISignInState_003E_002ERelease(&cComPtrNtv_003CISignInState_003E);
		}
		return result;
	}

	public unsafe ulong GetPassportPuid()
	{
		ulong result = 0uL;
		if (m_pService != null && IsSignedIn())
		{
			Unsafe.SkipInit(out CComPtrNtv_003CISignInState_003E cComPtrNtv_003CISignInState_003E);
			*(int*)(&cComPtrNtv_003CISignInState_003E) = 0;
			try
			{
				IService* pService = m_pService;
				if (((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, ISignInState**, int>)(int)(*(uint*)(*(int*)pService + 172)))((nint)pService, (ISignInState**)(&cComPtrNtv_003CISignInState_003E)) >= 0)
				{
					result = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, ulong>)(int)(*(uint*)(*(int*)(int)(*(uint*)(&cComPtrNtv_003CISignInState_003E)) + 40)))((IntPtr)(*(int*)(&cComPtrNtv_003CISignInState_003E)));
				}
			}
			catch
			{
				//try-fault
				global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<CComPtrNtv_003CISignInState_003E*, void>)(&global::_003CModule_003E.CComPtrNtv_003CISignInState_003E_002E_007Bdtor_007D), &cComPtrNtv_003CISignInState_003E);
				throw;
			}
			global::_003CModule_003E.CComPtrNtv_003CISignInState_003E_002ERelease(&cComPtrNtv_003CISignInState_003E);
		}
		return result;
	}

	public unsafe ValueType GetUserGuid()
	{
		_GUID gUID_NULL = global::_003CModule_003E.GUID_NULL;
		if (m_pService != null && IsSignedIn())
		{
			Unsafe.SkipInit(out CComPtrNtv_003CISignInState_003E cComPtrNtv_003CISignInState_003E);
			*(int*)(&cComPtrNtv_003CISignInState_003E) = 0;
			try
			{
				IService* pService = m_pService;
				int num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, ISignInState**, int>)(int)(*(uint*)(*(int*)pService + 172)))((nint)pService, (ISignInState**)(&cComPtrNtv_003CISignInState_003E));
				if (num >= 0)
				{
					num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, _GUID*, int>)(int)(*(uint*)(*(int*)(int)(*(uint*)(&cComPtrNtv_003CISignInState_003E)) + 32)))((IntPtr)(*(int*)(&cComPtrNtv_003CISignInState_003E)), &gUID_NULL);
					if (num < 0)
					{
						gUID_NULL = global::_003CModule_003E.GUID_NULL;
					}
				}
			}
			catch
			{
				//try-fault
				global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<CComPtrNtv_003CISignInState_003E*, void>)(&global::_003CModule_003E.CComPtrNtv_003CISignInState_003E_002E_007Bdtor_007D), &cComPtrNtv_003CISignInState_003E);
				throw;
			}
			global::_003CModule_003E.CComPtrNtv_003CISignInState_003E_002ERelease(&cComPtrNtv_003CISignInState_003E);
		}
		return global::_003CModule_003E.GUIDToGuid(gUID_NULL);
	}

	public unsafe string GetLocale()
	{
		string result = null;
		if (m_pService != null && IsSignedIn())
		{
			Unsafe.SkipInit(out CComPtrNtv_003CISignInState_003E cComPtrNtv_003CISignInState_003E);
			*(int*)(&cComPtrNtv_003CISignInState_003E) = 0;
			try
			{
				IService* pService = m_pService;
				int num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, ISignInState**, int>)(int)(*(uint*)(*(int*)pService + 172)))((nint)pService, (ISignInState**)(&cComPtrNtv_003CISignInState_003E));
				ushort* ptr = null;
				if (num >= 0)
				{
					num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, ushort**, int>)(int)(*(uint*)(*(int*)(int)(*(uint*)(&cComPtrNtv_003CISignInState_003E)) + 52)))((IntPtr)(*(int*)(&cComPtrNtv_003CISignInState_003E)), &ptr);
					if (num < 0)
					{
						goto IL_0062;
					}
					if (ptr != null)
					{
						result = Marshal.PtrToStringBSTR((IntPtr)ptr);
						goto IL_0062;
					}
				}
				goto end_IL_0019;
				IL_0062:
				if (ptr != null)
				{
					global::_003CModule_003E.SysFreeString(ptr);
				}
				end_IL_0019:;
			}
			catch
			{
				//try-fault
				global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<CComPtrNtv_003CISignInState_003E*, void>)(&global::_003CModule_003E.CComPtrNtv_003CISignInState_003E_002E_007Bdtor_007D), &cComPtrNtv_003CISignInState_003E);
				throw;
			}
			global::_003CModule_003E.CComPtrNtv_003CISignInState_003E_002ERelease(&cComPtrNtv_003CISignInState_003E);
		}
		return result;
	}

	[return: MarshalAs(UnmanagedType.U1)]
	public unsafe bool HasSignInLabelTakedown()
	{
		bool result = false;
		if (m_pService != null && IsSignedIn())
		{
			Unsafe.SkipInit(out CComPtrNtv_003CISignInState_003E cComPtrNtv_003CISignInState_003E);
			*(int*)(&cComPtrNtv_003CISignInState_003E) = 0;
			try
			{
				IService* pService = m_pService;
				if (((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, ISignInState**, int>)(int)(*(uint*)(*(int*)pService + 172)))((nint)pService, (ISignInState**)(&cComPtrNtv_003CISignInState_003E)) >= 0)
				{
					bool flag = ((((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, int>)(int)(*(uint*)(*(int*)(int)(*(uint*)(&cComPtrNtv_003CISignInState_003E)) + 124)))((IntPtr)(*(int*)(&cComPtrNtv_003CISignInState_003E))) != 0) ? true : false);
					result = flag;
				}
			}
			catch
			{
				//try-fault
				global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<CComPtrNtv_003CISignInState_003E*, void>)(&global::_003CModule_003E.CComPtrNtv_003CISignInState_003E_002E_007Bdtor_007D), &cComPtrNtv_003CISignInState_003E);
				throw;
			}
			global::_003CModule_003E.CComPtrNtv_003CISignInState_003E_002ERelease(&cComPtrNtv_003CISignInState_003E);
		}
		return result;
	}

	[return: MarshalAs(UnmanagedType.U1)]
	public unsafe bool HasSignInBillingViolation()
	{
		bool result = false;
		if (m_pService != null && IsSignedIn())
		{
			Unsafe.SkipInit(out CComPtrNtv_003CISignInState_003E cComPtrNtv_003CISignInState_003E);
			*(int*)(&cComPtrNtv_003CISignInState_003E) = 0;
			try
			{
				IService* pService = m_pService;
				if (((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, ISignInState**, int>)(int)(*(uint*)(*(int*)pService + 172)))((nint)pService, (ISignInState**)(&cComPtrNtv_003CISignInState_003E)) >= 0)
				{
					bool flag = ((((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, int>)(int)(*(uint*)(*(int*)(int)(*(uint*)(&cComPtrNtv_003CISignInState_003E)) + 100)))((IntPtr)(*(int*)(&cComPtrNtv_003CISignInState_003E))) != 0) ? true : false);
					result = flag;
				}
			}
			catch
			{
				//try-fault
				global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<CComPtrNtv_003CISignInState_003E*, void>)(&global::_003CModule_003E.CComPtrNtv_003CISignInState_003E_002E_007Bdtor_007D), &cComPtrNtv_003CISignInState_003E);
				throw;
			}
			global::_003CModule_003E.CComPtrNtv_003CISignInState_003E_002ERelease(&cComPtrNtv_003CISignInState_003E);
		}
		return result;
	}

	public unsafe int GetPointsBalance()
	{
		int result = 0;
		if (m_pService != null && IsSignedIn())
		{
			Unsafe.SkipInit(out CComPtrNtv_003CISignInState_003E cComPtrNtv_003CISignInState_003E);
			*(int*)(&cComPtrNtv_003CISignInState_003E) = 0;
			try
			{
				IService* pService = m_pService;
				if (((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, ISignInState**, int>)(int)(*(uint*)(*(int*)pService + 172)))((nint)pService, (ISignInState**)(&cComPtrNtv_003CISignInState_003E)) >= 0)
				{
					result = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, int>)(int)(*(uint*)(*(int*)(int)(*(uint*)(&cComPtrNtv_003CISignInState_003E)) + 72)))((IntPtr)(*(int*)(&cComPtrNtv_003CISignInState_003E)));
				}
			}
			catch
			{
				//try-fault
				global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<CComPtrNtv_003CISignInState_003E*, void>)(&global::_003CModule_003E.CComPtrNtv_003CISignInState_003E_002E_007Bdtor_007D), &cComPtrNtv_003CISignInState_003E);
				throw;
			}
			global::_003CModule_003E.CComPtrNtv_003CISignInState_003E_002ERelease(&cComPtrNtv_003CISignInState_003E);
		}
		return result;
	}

	public unsafe void GetBalances(GetBalancesCompleteCallback completeCallback, GetBalancesErrorCallback errorCallback)
	{
		if (m_pService != null)
		{
			GetBalancesCallbackWrapper* ptr = (GetBalancesCallbackWrapper*)global::_003CModule_003E.@new(16u);
			GetBalancesCallbackWrapper* ptr2;
			try
			{
				ptr2 = ((ptr == null) ? null : global::_003CModule_003E.Microsoft_002EZune_002EService_002EGetBalancesCallbackWrapper_002E_007Bctor_007D(ptr, completeCallback, errorCallback));
			}
			catch
			{
				//try-fault
				global::_003CModule_003E.delete(ptr);
				throw;
			}
			if (ptr2 != null)
			{
				IService* pService = m_pService;
				((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, IGetBalancesCallback*, int>)(int)(*(uint*)(*(int*)pService + 336)))((nint)pService, (IGetBalancesCallback*)ptr2);
				((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint>)(int)(*(uint*)(*(int*)ptr2 + 8)))((nint)ptr2);
			}
		}
	}

	public unsafe int GetSubscriptionFreeTrackBalance()
	{
		int result = 0;
		if (m_pService != null && IsSignedInWithSubscription())
		{
			Unsafe.SkipInit(out CComPtrNtv_003CISignInState_003E cComPtrNtv_003CISignInState_003E);
			*(int*)(&cComPtrNtv_003CISignInState_003E) = 0;
			try
			{
				IService* pService = m_pService;
				if (((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, ISignInState**, int>)(int)(*(uint*)(*(int*)pService + 172)))((nint)pService, (ISignInState**)(&cComPtrNtv_003CISignInState_003E)) >= 0)
				{
					result = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, int>)(int)(*(uint*)(*(int*)(int)(*(uint*)(&cComPtrNtv_003CISignInState_003E)) + 76)))((IntPtr)(*(int*)(&cComPtrNtv_003CISignInState_003E)));
				}
			}
			catch
			{
				//try-fault
				global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<CComPtrNtv_003CISignInState_003E*, void>)(&global::_003CModule_003E.CComPtrNtv_003CISignInState_003E_002E_007Bdtor_007D), &cComPtrNtv_003CISignInState_003E);
				throw;
			}
			global::_003CModule_003E.CComPtrNtv_003CISignInState_003E_002ERelease(&cComPtrNtv_003CISignInState_003E);
		}
		return result;
	}

	public unsafe HRESULT GetOfferDetails(Guid offerId, GetOfferDetailsCompleteCallback completeCallback, GetOfferDetailsErrorCallback errorCallback, object state)
	{
		int num = 0;
		if (m_pService == null)
		{
			num = -2147467259;
		}
		_GUID offerId2 = global::_003CModule_003E.GuidToGUID(offerId);
		Unsafe.SkipInit(out CComPtrNtv_003CIGetOfferDetailsCallback_003E cComPtrNtv_003CIGetOfferDetailsCallback_003E);
		*(int*)(&cComPtrNtv_003CIGetOfferDetailsCallback_003E) = 0;
		HRESULT result;
		try
		{
			if (num >= 0)
			{
				GetOfferDetailsCallbackWrapper* ptr = (GetOfferDetailsCallbackWrapper*)global::_003CModule_003E.@new(40u);
				GetOfferDetailsCallbackWrapper* p;
				try
				{
					p = ((ptr == null) ? null : global::_003CModule_003E.Microsoft_002EZune_002EService_002EGetOfferDetailsCallbackWrapper_002E_007Bctor_007D(ptr, offerId2, state, m_pService, completeCallback, errorCallback));
				}
				catch
				{
					//try-fault
					global::_003CModule_003E.delete(ptr);
					throw;
				}
				global::_003CModule_003E.CComPtrNtv_003CIGetOfferDetailsCallback_003E_002EAttach(&cComPtrNtv_003CIGetOfferDetailsCallback_003E, (IGetOfferDetailsCallback*)p);
				if (*(int*)(&cComPtrNtv_003CIGetOfferDetailsCallback_003E) == 0)
				{
					num = -2147024882;
				}
				if (num >= 0)
				{
					IService* pService = m_pService;
					num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, int, _GUID*, IGetOfferDetailsCallback*, int>)(int)(*(uint*)(*(int*)pService + 344)))((nint)pService, 1, &offerId2, (IGetOfferDetailsCallback*)(int)(*(uint*)(&cComPtrNtv_003CIGetOfferDetailsCallback_003E)));
				}
			}
			result = new HRESULT(num);
		}
		catch
		{
			//try-fault
			global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<CComPtrNtv_003CIGetOfferDetailsCallback_003E*, void>)(&global::_003CModule_003E.CComPtrNtv_003CIGetOfferDetailsCallback_003E_002E_007Bdtor_007D), &cComPtrNtv_003CIGetOfferDetailsCallback_003E);
			throw;
		}
		global::_003CModule_003E.CComPtrNtv_003CIGetOfferDetailsCallback_003E_002ERelease(&cComPtrNtv_003CIGetOfferDetailsCallback_003E);
		return result;
	}

	public unsafe void GetOffers(IList albumGuids, IList trackGuids, IList videoGuids, IList appGuids, IDictionary mapIdToContext, EGetOffersFlags eGetOffersFlags, string deviceEndpointId, GetOffersCompleteCallback completeCallback, GetOffersErrorCallback errorCallback)
	{
		//The blocks IL_00e0, IL_013d are reachable both inside and outside the pinned region starting at IL_00de. ILSpy has duplicated these blocks in order to place them both within and outside the `fixed` statement.
		//The blocks IL_01cc, IL_021f are reachable both inside and outside the pinned region starting at IL_01cb. ILSpy has duplicated these blocks in order to place them both within and outside the `fixed` statement.
		//The blocks IL_02eb, IL_030f, IL_0312, IL_031e, IL_0323 are reachable both inside and outside the pinned region starting at IL_02e9. ILSpy has duplicated these blocks in order to place them both within and outside the `fixed` statement.
		if (m_pService == null)
		{
			return;
		}
		/*pinned*/ref ushort reference = ref *(ushort*)null;
		try
		{
			int num = 0;
			GetOffersCallbackWrapper* ptr = (GetOffersCallbackWrapper*)global::_003CModule_003E.@new(20u);
			GetOffersCallbackWrapper* ptr2;
			try
			{
				ptr2 = ((ptr == null) ? null : global::_003CModule_003E.Microsoft_002EZune_002EService_002EGetOffersCallbackWrapper_002E_007Bctor_007D(ptr, completeCallback, errorCallback, mapIdToContext));
			}
			catch
			{
				//try-fault
				global::_003CModule_003E.delete(ptr);
				throw;
			}
			if (ptr2 == null)
			{
				num = -2147024882;
			}
			IMediaCollection* ptr3 = null;
			if (num < 0)
			{
				goto IL_031e;
			}
			IService* pService = m_pService;
			num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, IMediaCollection**, int>)(int)(*(uint*)(*(int*)pService + 16)))((nint)pService, &ptr3);
			if (num < 0)
			{
				goto IL_030f;
			}
			if (albumGuids != null)
			{
				IEnumerator enumerator = albumGuids.GetEnumerator();
				if (enumerator.MoveNext())
				{
					Unsafe.SkipInit(out CComPtrNtv_003CIContextData_003E cComPtrNtv_003CIContextData_003E);
					do
					{
						if (num < 0)
						{
							continue;
						}
						Guid guid = (Guid)enumerator.Current;
						string text = null;
						if (mapIdToContext != null && mapIdToContext.Contains(guid))
						{
							text = (string)mapIdToContext[guid];
						}
						/*pinned*/ref ushort reference2 = ref *(ushort*)null;
						try
						{
							_GUID gUID;
							if (!string.IsNullOrEmpty(text))
							{
								fixed (ushort* ptr4 = &Unsafe.As<char, ushort>(ref global::_003CModule_003E.PtrToStringChars(text)))
								{
									global::_003CModule_003E.CComPtrNtv_003CIContextData_003E_002E_007Bctor_007D(&cComPtrNtv_003CIContextData_003E);
									try
									{
										int num2 = *(int*)m_pService + 60;
										((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, ushort*, global::EDownloadContextEvent, ushort*, IContextData**, int>)(int)(*(uint*)num2))((nint)m_pService, ptr4, (global::EDownloadContextEvent)(-1), null, global::_003CModule_003E.CComPtrNtv_003CIContextData_003E_002E_0026(&cComPtrNtv_003CIContextData_003E));
										gUID = global::_003CModule_003E.GuidToGUID(guid);
										num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, _GUID*, global::EContentType, ushort*, ushort*, ushort*, IContextData*, int>)(int)(*(uint*)(*(int*)ptr3 + 20)))((nint)ptr3, &gUID, (global::EContentType)1, null, null, null, (IContextData*)(int)(*(uint*)(&cComPtrNtv_003CIContextData_003E)));
									}
									catch
									{
										//try-fault
										global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<CComPtrNtv_003CIContextData_003E*, void>)(&global::_003CModule_003E.CComPtrNtv_003CIContextData_003E_002E_007Bdtor_007D), &cComPtrNtv_003CIContextData_003E);
										throw;
									}
									global::_003CModule_003E.CComPtrNtv_003CIContextData_003E_002E_007Bdtor_007D(&cComPtrNtv_003CIContextData_003E);
								}
							}
							else
							{
								global::_003CModule_003E.CComPtrNtv_003CIContextData_003E_002E_007Bctor_007D(&cComPtrNtv_003CIContextData_003E);
								try
								{
									int num2 = *(int*)m_pService + 60;
									((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, ushort*, global::EDownloadContextEvent, ushort*, IContextData**, int>)(int)(*(uint*)num2))((nint)m_pService, (ushort*)Unsafe.AsPointer(ref reference2), (global::EDownloadContextEvent)(-1), null, global::_003CModule_003E.CComPtrNtv_003CIContextData_003E_002E_0026(&cComPtrNtv_003CIContextData_003E));
									gUID = global::_003CModule_003E.GuidToGUID(guid);
									num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, _GUID*, global::EContentType, ushort*, ushort*, ushort*, IContextData*, int>)(int)(*(uint*)(*(int*)ptr3 + 20)))((nint)ptr3, &gUID, (global::EContentType)1, null, null, null, (IContextData*)(int)(*(uint*)(&cComPtrNtv_003CIContextData_003E)));
								}
								catch
								{
									//try-fault
									global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<CComPtrNtv_003CIContextData_003E*, void>)(&global::_003CModule_003E.CComPtrNtv_003CIContextData_003E_002E_007Bdtor_007D), &cComPtrNtv_003CIContextData_003E);
									throw;
								}
								global::_003CModule_003E.CComPtrNtv_003CIContextData_003E_002E_007Bdtor_007D(&cComPtrNtv_003CIContextData_003E);
							}
						}
						catch
						{
							//try-fault
							reference2 = ref *(ushort*)null;
							throw;
						}
						reference2 = ref *(ushort*)null;
					}
					while (enumerator.MoveNext());
				}
			}
			if (trackGuids != null)
			{
				IEnumerator enumerator2 = trackGuids.GetEnumerator();
				if (enumerator2.MoveNext())
				{
					Unsafe.SkipInit(out CComPtrNtv_003CIContextData_003E cComPtrNtv_003CIContextData_003E2);
					do
					{
						if (num < 0)
						{
							continue;
						}
						Guid guid2 = (Guid)enumerator2.Current;
						string text2 = null;
						if (mapIdToContext != null && mapIdToContext.Contains(guid2))
						{
							text2 = (string)mapIdToContext[guid2];
						}
						/*pinned*/ref ushort reference3 = ref *(ushort*)null;
						try
						{
							_GUID gUID2;
							if (!string.IsNullOrEmpty(text2))
							{
								fixed (ushort* ptr5 = &Unsafe.As<char, ushort>(ref global::_003CModule_003E.PtrToStringChars(text2)))
								{
									*(int*)(&cComPtrNtv_003CIContextData_003E2) = 0;
									try
									{
										int num3 = *(int*)m_pService + 60;
										((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, ushort*, global::EDownloadContextEvent, ushort*, IContextData**, int>)(int)(*(uint*)num3))((nint)m_pService, ptr5, (global::EDownloadContextEvent)(-1), null, (IContextData**)(&cComPtrNtv_003CIContextData_003E2));
										gUID2 = global::_003CModule_003E.GuidToGUID(guid2);
										num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, _GUID*, global::EContentType, ushort*, ushort*, ushort*, IContextData*, int>)(int)(*(uint*)(*(int*)ptr3 + 20)))((nint)ptr3, &gUID2, (global::EContentType)0, null, null, null, (IContextData*)(int)(*(uint*)(&cComPtrNtv_003CIContextData_003E2)));
									}
									catch
									{
										//try-fault
										global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<CComPtrNtv_003CIContextData_003E*, void>)(&global::_003CModule_003E.CComPtrNtv_003CIContextData_003E_002E_007Bdtor_007D), &cComPtrNtv_003CIContextData_003E2);
										throw;
									}
									global::_003CModule_003E.CComPtrNtv_003CIContextData_003E_002E_007Bdtor_007D(&cComPtrNtv_003CIContextData_003E2);
								}
							}
							else
							{
								*(int*)(&cComPtrNtv_003CIContextData_003E2) = 0;
								try
								{
									int num3 = *(int*)m_pService + 60;
									((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, ushort*, global::EDownloadContextEvent, ushort*, IContextData**, int>)(int)(*(uint*)num3))((nint)m_pService, (ushort*)Unsafe.AsPointer(ref reference3), (global::EDownloadContextEvent)(-1), null, (IContextData**)(&cComPtrNtv_003CIContextData_003E2));
									gUID2 = global::_003CModule_003E.GuidToGUID(guid2);
									num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, _GUID*, global::EContentType, ushort*, ushort*, ushort*, IContextData*, int>)(int)(*(uint*)(*(int*)ptr3 + 20)))((nint)ptr3, &gUID2, (global::EContentType)0, null, null, null, (IContextData*)(int)(*(uint*)(&cComPtrNtv_003CIContextData_003E2)));
								}
								catch
								{
									//try-fault
									global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<CComPtrNtv_003CIContextData_003E*, void>)(&global::_003CModule_003E.CComPtrNtv_003CIContextData_003E_002E_007Bdtor_007D), &cComPtrNtv_003CIContextData_003E2);
									throw;
								}
								global::_003CModule_003E.CComPtrNtv_003CIContextData_003E_002E_007Bdtor_007D(&cComPtrNtv_003CIContextData_003E2);
							}
						}
						catch
						{
							//try-fault
							reference3 = ref *(ushort*)null;
							throw;
						}
						reference3 = ref *(ushort*)null;
					}
					while (enumerator2.MoveNext());
				}
			}
			if (videoGuids != null)
			{
				IEnumerator enumerator3 = videoGuids.GetEnumerator();
				if (enumerator3.MoveNext())
				{
					do
					{
						if (num >= 0)
						{
							_GUID gUID3 = global::_003CModule_003E.GuidToGUID((Guid)enumerator3.Current);
							num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, _GUID*, global::EContentType, ushort*, ushort*, ushort*, IContextData*, int>)(int)(*(uint*)(*(int*)ptr3 + 20)))((nint)ptr3, &gUID3, (global::EContentType)3, null, null, null, null);
						}
					}
					while (enumerator3.MoveNext());
				}
			}
			if (appGuids != null)
			{
				IEnumerator enumerator4 = appGuids.GetEnumerator();
				if (enumerator4.MoveNext())
				{
					do
					{
						if (num >= 0)
						{
							_GUID gUID4 = global::_003CModule_003E.GuidToGUID((Guid)enumerator4.Current);
							num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, _GUID*, global::EContentType, ushort*, ushort*, ushort*, IContextData*, int>)(int)(*(uint*)(*(int*)ptr3 + 20)))((nint)ptr3, &gUID4, (global::EContentType)7, null, null, null, null);
						}
					}
					while (enumerator4.MoveNext());
				}
			}
			if (num < 0)
			{
				goto IL_030f;
			}
			if (string.IsNullOrEmpty(deviceEndpointId))
			{
				int num4 = *(int*)m_pService + 340;
				((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, IMediaCollection*, IGetOffersCallback*, global::EGetOffersFlags, ushort*, int>)(int)(*(uint*)num4))((nint)m_pService, ptr3, (IGetOffersCallback*)ptr2, (global::EGetOffersFlags)eGetOffersFlags, (ushort*)Unsafe.AsPointer(ref reference));
				goto IL_030f;
			}
			IMediaCollection* intPtr;
			GetOffersCallbackWrapper* intPtr2;
			fixed (ushort* ptr6 = &Unsafe.As<char, ushort>(ref global::_003CModule_003E.PtrToStringChars(deviceEndpointId)))
			{
				int num4 = *(int*)m_pService + 340;
				((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, IMediaCollection*, IGetOffersCallback*, global::EGetOffersFlags, ushort*, int>)(int)(*(uint*)num4))((nint)m_pService, ptr3, (IGetOffersCallback*)ptr2, (global::EGetOffersFlags)eGetOffersFlags, ptr6);
				if (ptr3 != null)
				{
					intPtr = ptr3;
					((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint>)(int)(*(uint*)(*(int*)intPtr + 8)))((nint)intPtr);
				}
				if (ptr2 != null)
				{
					intPtr2 = ptr2;
					((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint>)(int)(*(uint*)(*(int*)intPtr2 + 8)))((nint)intPtr2);
				}
			}
			goto end_IL_000e;
			IL_031e:
			if (ptr2 != null)
			{
				intPtr2 = ptr2;
				((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint>)(int)(*(uint*)(*(int*)intPtr2 + 8)))((nint)intPtr2);
			}
			goto end_IL_000e;
			IL_030f:
			if (ptr3 != null)
			{
				intPtr = ptr3;
				((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint>)(int)(*(uint*)(*(int*)intPtr + 8)))((nint)intPtr);
			}
			goto IL_031e;
			end_IL_000e:;
		}
		catch
		{
			//try-fault
			reference = ref *(ushort*)null;
			throw;
		}
		reference = ref *(ushort*)null;
	}

	public unsafe void PurchaseOffers(PaymentInstrument payment, AlbumOfferCollection albumOffers, TrackOfferCollection trackOffers, VideoOfferCollection videoOffers, AppOfferCollection appOffers, EPurchaseOffersFlags ePurchaseOffersFlags, PurchaseOffersCompleteHandler purchaseOffersHandler)
	{
		if (null == payment)
		{
			throw new ArgumentNullException("payment");
		}
		if (m_pService == null)
		{
			return;
		}
		int num = 0;
		PurchaseOffersCallbackWrapper* ptr = (PurchaseOffersCallbackWrapper*)global::_003CModule_003E.@new(12u);
		PurchaseOffersCallbackWrapper* ptr2;
		try
		{
			ptr2 = ((ptr == null) ? null : global::_003CModule_003E.Microsoft_002EZune_002EUtil_002EPurchaseOffersCallbackWrapper_002E_007Bctor_007D(ptr, purchaseOffersHandler));
		}
		catch
		{
			//try-fault
			global::_003CModule_003E.delete(ptr);
			throw;
		}
		if (ptr2 == null)
		{
			num = -2147024882;
		}
		IMusicAlbumCollection* ptr3 = null;
		if (num >= 0 && albumOffers != null)
		{
			ptr3 = albumOffers.GetCollection();
		}
		IMusicTrackCollection* ptr4 = null;
		if (num >= 0 && trackOffers != null)
		{
			ptr4 = trackOffers.GetCollection();
		}
		IVideoCollection* ptr5 = null;
		if (num >= 0 && videoOffers != null)
		{
			ptr5 = videoOffers.GetCollection();
		}
		IAppCollection* ptr6 = null;
		if (num >= 0 && appOffers != null)
		{
			ptr6 = appOffers.GetCollection();
		}
		EMediaPaymentType eMediaPaymentType = (EMediaPaymentType)(-1);
		if (num >= 0)
		{
			num = PaymentTypeToMediaPaymentType(payment.Type, &eMediaPaymentType);
			if (num >= 0)
			{
				fixed (ushort* ptr7 = &Unsafe.As<char, ushort>(ref global::_003CModule_003E.PtrToStringChars(payment.Id)))
				{
					try
					{
						int num2 = *(int*)m_pService + 356;
						((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, EMediaPaymentType, ushort*, IMusicAlbumCollection*, IMusicTrackCollection*, IVideoCollection*, IAppCollection*, global::EPurchaseOffersFlags, IPurchaseOffersCallback*, int>)(int)(*(uint*)num2))((nint)m_pService, eMediaPaymentType, ptr7, ptr3, ptr4, ptr5, ptr6, (global::EPurchaseOffersFlags)ePurchaseOffersFlags, (IPurchaseOffersCallback*)ptr2);
					}
					catch
					{
						//try-fault
						ptr7 = null;
						throw;
					}
				}
			}
		}
		if (ptr3 != null)
		{
			IMusicAlbumCollection* intPtr = ptr3;
			((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint>)(int)(*(uint*)(*(int*)intPtr + 8)))((nint)intPtr);
		}
		if (ptr4 != null)
		{
			IMusicTrackCollection* intPtr2 = ptr4;
			((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint>)(int)(*(uint*)(*(int*)intPtr2 + 8)))((nint)intPtr2);
		}
		if (ptr5 != null)
		{
			IVideoCollection* intPtr3 = ptr5;
			((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint>)(int)(*(uint*)(*(int*)intPtr3 + 8)))((nint)intPtr3);
		}
		if (ptr6 != null)
		{
			IAppCollection* intPtr4 = ptr6;
			((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint>)(int)(*(uint*)(*(int*)intPtr4 + 8)))((nint)intPtr4);
		}
		if (ptr2 != null)
		{
			((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint>)(int)(*(uint*)(*(int*)ptr2 + 8)))((nint)ptr2);
		}
	}

	[return: MarshalAs(UnmanagedType.U1)]
	public unsafe bool ReportFavouriteArtists(Guid userId, IList artists, AsyncCompleteHandler callback)
	{
		int num;
		if (m_pService != null)
		{
			AsyncCallbackWrapper* ptr = (AsyncCallbackWrapper*)global::_003CModule_003E.@new(12u);
			AsyncCallbackWrapper* ptr2;
			try
			{
				ptr2 = ((ptr == null) ? null : global::_003CModule_003E.Microsoft_002EZune_002EUtil_002EAsyncCallbackWrapper_002E_007Bctor_007D(ptr, callback));
			}
			catch
			{
				//try-fault
				global::_003CModule_003E.delete(ptr);
				throw;
			}
			if (ptr2 == null)
			{
				num = -2147024882;
				goto IL_00ec;
			}
			_GUID gUID = global::_003CModule_003E.GuidToGUID(userId);
			uint count = (uint)artists.Count;
			_GUID* ptr3 = (_GUID*)global::_003CModule_003E.new_005B_005D((count > 268435455) ? uint.MaxValue : (count * 16));
			int num2 = 0;
			if (0 < artists.Count)
			{
				_GUID* ptr4 = ptr3;
				do
				{
					_GUID gUID2 = global::_003CModule_003E.GuidToGUID((Guid)artists[num2]);
					// IL cpblk instruction
					Unsafe.CopyBlock(ptr4, ref gUID2, 16);
					num2++;
					ptr4 = (_GUID*)((byte*)ptr4 + 16);
				}
				while (num2 < artists.Count);
			}
			int num3 = *(int*)m_pService + 264;
			num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, _GUID, int, _GUID*, IAsyncCallback*, int>)(int)(*(uint*)num3))((nint)m_pService, gUID, artists.Count, ptr3, (IAsyncCallback*)ptr2);
			global::_003CModule_003E.delete_005B_005D(ptr3);
			((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint>)(int)(*(uint*)(*(int*)ptr2 + 8)))((nint)ptr2);
		}
		else
		{
			num = -2147467259;
		}
		if (num >= 0)
		{
			return true;
		}
		goto IL_00ec;
		IL_00ec:
		if (callback != null)
		{
			HRESULT hr = num;
			callback(hr);
		}
		return false;
	}

	public unsafe int VerifyToken(string token, out TokenDetails tokenDetails)
	{
		int num = 0;
		tokenDetails = null;
		if (m_pService != null)
		{
			fixed (ushort* ptr = &Unsafe.As<char, ushort>(ref global::_003CModule_003E.PtrToStringChars(token)))
			{
				try
				{
					Unsafe.SkipInit(out CComPtrNtv_003CITokenDetails_003E cComPtrNtv_003CITokenDetails_003E);
					*(int*)(&cComPtrNtv_003CITokenDetails_003E) = 0;
					try
					{
						int num2 = *(int*)m_pService + 268;
						num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, ushort*, ITokenDetails**, int>)(int)(*(uint*)num2))((nint)m_pService, ptr, (ITokenDetails**)(&cComPtrNtv_003CITokenDetails_003E));
						if (num >= 0)
						{
							tokenDetails = new TokenDetails((ITokenDetails*)(int)(*(uint*)(&cComPtrNtv_003CITokenDetails_003E)));
						}
					}
					catch
					{
						//try-fault
						global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<CComPtrNtv_003CITokenDetails_003E*, void>)(&global::_003CModule_003E.CComPtrNtv_003CITokenDetails_003E_002E_007Bdtor_007D), &cComPtrNtv_003CITokenDetails_003E);
						throw;
					}
					global::_003CModule_003E.CComPtrNtv_003CITokenDetails_003E_002ERelease(&cComPtrNtv_003CITokenDetails_003E);
				}
				catch
				{
					//try-fault
					ptr = null;
					throw;
				}
			}
		}
		return num;
	}

	[return: MarshalAs(UnmanagedType.U1)]
	public unsafe bool ReportAConcern(EConcernType concernType, EContentType contentType, Guid mediaId, string message, AsyncCompleteHandler callback)
	{
		int num;
		if (m_pService != null)
		{
			AsyncCallbackWrapper* ptr = (AsyncCallbackWrapper*)global::_003CModule_003E.@new(12u);
			AsyncCallbackWrapper* ptr2;
			try
			{
				ptr2 = ((ptr == null) ? null : global::_003CModule_003E.Microsoft_002EZune_002EUtil_002EAsyncCallbackWrapper_002E_007Bctor_007D(ptr, callback));
			}
			catch
			{
				//try-fault
				global::_003CModule_003E.delete(ptr);
				throw;
			}
			if (ptr2 == null)
			{
				num = -2147024882;
				goto IL_008a;
			}
			_GUID gUID = global::_003CModule_003E.GuidToGUID(mediaId);
			fixed (ushort* ptr3 = &Unsafe.As<char, ushort>(ref global::_003CModule_003E.PtrToStringChars(message)))
			{
				try
				{
					int num2 = *(int*)m_pService + 260;
					num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, global::EConcernType, global::EContentType, _GUID, ushort*, IAsyncCallback*, int>)(int)(*(uint*)num2))((nint)m_pService, (global::EConcernType)concernType, (global::EContentType)contentType, gUID, ptr3, (IAsyncCallback*)ptr2);
				}
				catch
				{
					//try-fault
					ptr3 = null;
					throw;
				}
			}
			((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint>)(int)(*(uint*)(*(int*)ptr2 + 8)))((nint)ptr2);
		}
		else
		{
			num = -2147467259;
		}
		if (num >= 0)
		{
			return true;
		}
		goto IL_008a;
		IL_008a:
		if (callback != null)
		{
			HRESULT hr = num;
			callback(hr);
		}
		return false;
	}

	[return: MarshalAs(UnmanagedType.U1)]
	public unsafe bool PostAppReview(Guid mediaId, string title, string comment, int rating, AsyncCompleteHandler callback)
	{
		int num;
		if (m_pService != null)
		{
			AsyncCallbackWrapper* ptr = (AsyncCallbackWrapper*)global::_003CModule_003E.@new(12u);
			AsyncCallbackWrapper* ptr2;
			try
			{
				ptr2 = ((ptr == null) ? null : global::_003CModule_003E.Microsoft_002EZune_002EUtil_002EAsyncCallbackWrapper_002E_007Bctor_007D(ptr, callback));
			}
			catch
			{
				//try-fault
				global::_003CModule_003E.delete(ptr);
				throw;
			}
			if (ptr2 == null)
			{
				num = -2147024882;
				goto IL_009c;
			}
			_GUID gUID = global::_003CModule_003E.GuidToGUID(mediaId);
			fixed (ushort* ptr3 = &Unsafe.As<char, ushort>(ref global::_003CModule_003E.PtrToStringChars(title)))
			{
				try
				{
					fixed (ushort* ptr4 = &Unsafe.As<char, ushort>(ref global::_003CModule_003E.PtrToStringChars(comment)))
					{
						try
						{
							int num2 = *(int*)m_pService + 272;
							num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, _GUID*, ushort*, ushort*, int, IAsyncCallback*, int>)(int)(*(uint*)num2))((nint)m_pService, &gUID, ptr3, ptr4, rating, (IAsyncCallback*)ptr2);
						}
						catch
						{
							//try-fault
							ptr4 = null;
							throw;
						}
					}
				}
				catch
				{
					//try-fault
					ptr3 = null;
					throw;
				}
			}
			((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint>)(int)(*(uint*)(*(int*)ptr2 + 8)))((nint)ptr2);
		}
		else
		{
			num = -2147467259;
		}
		if (num >= 0)
		{
			return true;
		}
		goto IL_009c;
		IL_009c:
		if (callback != null)
		{
			HRESULT hr = num;
			callback(hr);
		}
		return false;
	}

	[return: MarshalAs(UnmanagedType.U1)]
	public unsafe bool LaunchBrowserForExternalUrl(string strUrl, EPassportPolicyId ePassportPolicy)
	{
		bool result = false;
		if (m_pService != null)
		{
			fixed (ushort* ptr = &Unsafe.As<char, ushort>(ref global::_003CModule_003E.PtrToStringChars(strUrl)))
			{
				try
				{
					int num = *(int*)m_pService + 408;
					result = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, ushort*, EPassportPolicy, IAsyncCallback*, int>)(int)(*(uint*)num))((nint)m_pService, ptr, (EPassportPolicy)ePassportPolicy, null) >= 0;
				}
				catch
				{
					//try-fault
					ptr = null;
					throw;
				}
			}
		}
		return result;
	}

	[return: MarshalAs(UnmanagedType.U1)]
	public unsafe bool GetAlbumIdFromCompId(string compId, out Guid guidAlbum)
	{
		bool result = false;
		if (m_pService != null)
		{
			Unsafe.SkipInit(out MusicAlbumMetadata musicAlbumMetadata);
			global::_003CModule_003E.MusicAlbumMetadata_002E_007Bctor_007D(&musicAlbumMetadata);
			try
			{
				fixed (ushort* ptr = &Unsafe.As<char, ushort>(ref global::_003CModule_003E.PtrToStringChars(compId)))
				{
					try
					{
						int num = *(int*)m_pService + 416;
						if (((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, ushort*, MusicAlbumMetadata*, int>)(int)(*(uint*)num))((nint)m_pService, ptr, &musicAlbumMetadata) >= 0)
						{
							Guid guid = global::_003CModule_003E.GUIDToGuid(Unsafe.As<MusicAlbumMetadata, _GUID>(ref Unsafe.AddByteOffset(ref musicAlbumMetadata, 4)));
							guidAlbum = guid;
							result = true;
						}
					}
					catch
					{
						//try-fault
						ptr = null;
						throw;
					}
				}
			}
			catch
			{
				//try-fault
				global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<MusicAlbumMetadata*, void>)(&global::_003CModule_003E.MusicAlbumMetadata_002E_007Bdtor_007D), &musicAlbumMetadata);
				throw;
			}
			*(int*)(&musicAlbumMetadata) = (int)Unsafe.AsPointer(ref global::_003CModule_003E._003F_003F_7MusicAlbumMetadata_0040_00406B_0040);
			global::_003CModule_003E.MusicAlbumMetadata_002EClear(&musicAlbumMetadata);
		}
		return result;
	}

	[return: MarshalAs(UnmanagedType.U1)]
	public unsafe bool GetMusicVideoIdFromCompId(string compId, out Guid guidMusicVideo)
	{
		bool result = false;
		if (m_pService != null)
		{
			fixed (ushort* ptr = &Unsafe.As<char, ushort>(ref global::_003CModule_003E.PtrToStringChars(compId)))
			{
				try
				{
					int num = *(int*)m_pService + 420;
					Unsafe.SkipInit(out _GUID guid);
					if (((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, ushort*, _GUID*, int>)(int)(*(uint*)num))((nint)m_pService, ptr, &guid) >= 0)
					{
						Guid guid2 = global::_003CModule_003E.GUIDToGuid(guid);
						guidMusicVideo = guid2;
						result = true;
					}
				}
				catch
				{
					//try-fault
					ptr = null;
					throw;
				}
			}
		}
		return result;
	}

	public unsafe DRMInfo GetFileDRMInfo(string filePath)
	{
		DRMInfo result = null;
		if (m_pService != null)
		{
			fixed (ushort* ptr = &Unsafe.As<char, ushort>(ref global::_003CModule_003E.PtrToStringChars(filePath)))
			{
				try
				{
					int num = *(int*)m_pService + 284;
					Unsafe.SkipInit(out DRMQueryState eCanPlay);
					Unsafe.SkipInit(out _FILETIME fILETIME);
					Unsafe.SkipInit(out int num2);
					Unsafe.SkipInit(out int num3);
					if (((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, ushort*, DRMQueryState*, _FILETIME*, int*, int*, int>)(int)(*(uint*)num))((nint)m_pService, ptr, &eCanPlay, &fILETIME, &num2, &num3) >= 0)
					{
						long num4 = (long)(uint)Unsafe.As<_FILETIME, int>(ref Unsafe.AddByteOffset(ref fILETIME, 4)) * 4294967296L + (uint)(*(int*)(&fILETIME));
						DateTime expiryDate = DateTime.MaxValue;
						if (num4 > 0)
						{
							try
							{
								expiryDate = DateTime.FromFileTime(num4);
							}
							catch (ArgumentOutOfRangeException)
							{
							}
						}
						bool canBurn = ((num3 != 0) ? true : false);
						bool canSync = ((num2 != 0) ? true : false);
						result = new DRMInfo(eCanPlay, expiryDate, canSync, canBurn);
					}
				}
				catch
				{
					//try-fault
					ptr = null;
					throw;
				}
			}
		}
		return result;
	}

	public unsafe DRMInfo GetMediaDRMInfo(Guid mediaId, EContentType eContentType)
	{
		DRMInfo result = null;
		if (m_pService != null)
		{
			_GUID gUID = global::_003CModule_003E.GuidToGUID(mediaId);
			IService* pService = m_pService;
			Unsafe.SkipInit(out DRMQueryState eCanPlay);
			Unsafe.SkipInit(out _FILETIME fILETIME);
			Unsafe.SkipInit(out int num);
			Unsafe.SkipInit(out int num2);
			if (((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, _GUID, global::EContentType, DRMQueryState*, _FILETIME*, int*, int*, int>)(int)(*(uint*)(*(int*)pService + 288)))((nint)pService, gUID, (global::EContentType)eContentType, &eCanPlay, &fILETIME, &num, &num2) >= 0)
			{
				long num3 = (long)(uint)Unsafe.As<_FILETIME, int>(ref Unsafe.AddByteOffset(ref fILETIME, 4)) * 4294967296L + (uint)(*(int*)(&fILETIME));
				DateTime expiryDate = DateTime.MaxValue;
				if (num3 > 0)
				{
					try
					{
						expiryDate = DateTime.FromFileTime(num3);
					}
					catch (ArgumentOutOfRangeException)
					{
					}
				}
				bool canBurn = ((num2 != 0) ? true : false);
				bool canSync = ((num != 0) ? true : false);
				result = new DRMInfo(eCanPlay, expiryDate, canSync, canBurn);
			}
		}
		return result;
	}

	public unsafe ulong GetSubscriptionOfferId()
	{
		ulong result = 0uL;
		if (IsSignedIn())
		{
			Unsafe.SkipInit(out CComPtrNtv_003CISignInState_003E cComPtrNtv_003CISignInState_003E);
			*(int*)(&cComPtrNtv_003CISignInState_003E) = 0;
			try
			{
				IService* pService = m_pService;
				if (((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, ISignInState**, int>)(int)(*(uint*)(*(int*)pService + 172)))((nint)pService, (ISignInState**)(&cComPtrNtv_003CISignInState_003E)) >= 0)
				{
					result = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, ulong>)(int)(*(uint*)(*(int*)(int)(*(uint*)(&cComPtrNtv_003CISignInState_003E)) + 104)))((IntPtr)(*(int*)(&cComPtrNtv_003CISignInState_003E)));
				}
			}
			catch
			{
				//try-fault
				global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<CComPtrNtv_003CISignInState_003E*, void>)(&global::_003CModule_003E.CComPtrNtv_003CISignInState_003E_002E_007Bdtor_007D), &cComPtrNtv_003CISignInState_003E);
				throw;
			}
			global::_003CModule_003E.CComPtrNtv_003CISignInState_003E_002ERelease(&cComPtrNtv_003CISignInState_003E);
		}
		return result;
	}

	public unsafe ulong GetSubscriptionRenewalOfferId()
	{
		ulong result = 0uL;
		if (IsSignedIn())
		{
			Unsafe.SkipInit(out CComPtrNtv_003CISignInState_003E cComPtrNtv_003CISignInState_003E);
			*(int*)(&cComPtrNtv_003CISignInState_003E) = 0;
			try
			{
				IService* pService = m_pService;
				if (((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, ISignInState**, int>)(int)(*(uint*)(*(int*)pService + 172)))((nint)pService, (ISignInState**)(&cComPtrNtv_003CISignInState_003E)) >= 0)
				{
					result = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, ulong>)(int)(*(uint*)(*(int*)(int)(*(uint*)(&cComPtrNtv_003CISignInState_003E)) + 108)))((IntPtr)(*(int*)(&cComPtrNtv_003CISignInState_003E)));
				}
			}
			catch
			{
				//try-fault
				global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<CComPtrNtv_003CISignInState_003E*, void>)(&global::_003CModule_003E.CComPtrNtv_003CISignInState_003E_002E_007Bdtor_007D), &cComPtrNtv_003CISignInState_003E);
				throw;
			}
			global::_003CModule_003E.CComPtrNtv_003CISignInState_003E_002ERelease(&cComPtrNtv_003CISignInState_003E);
		}
		return result;
	}

	public unsafe DateTime GetSubscriptionEndDate()
	{
		DateTime result = DateTime.MaxValue;
		if (IsSignedIn())
		{
			Unsafe.SkipInit(out CComPtrNtv_003CISignInState_003E cComPtrNtv_003CISignInState_003E);
			*(int*)(&cComPtrNtv_003CISignInState_003E) = 0;
			try
			{
				IService* pService = m_pService;
				int num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, ISignInState**, int>)(int)(*(uint*)(*(int*)pService + 172)))((nint)pService, (ISignInState**)(&cComPtrNtv_003CISignInState_003E));
				ushort* ptr = null;
				string text = null;
				if (num >= 0)
				{
					num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, ushort**, int>)(int)(*(uint*)(*(int*)(int)(*(uint*)(&cComPtrNtv_003CISignInState_003E)) + 112)))((IntPtr)(*(int*)(&cComPtrNtv_003CISignInState_003E)), &ptr);
					if (num >= 0)
					{
						if (ptr == null)
						{
							goto IL_0066;
						}
						text = Marshal.PtrToStringBSTR((IntPtr)ptr);
					}
					if (ptr != null)
					{
						global::_003CModule_003E.SysFreeString(ptr);
					}
				}
				goto IL_0066;
				IL_0066:
				if (text != null && text.Length > 0)
				{
					DateTime.TryParse(text, out result);
				}
			}
			catch
			{
				//try-fault
				global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<CComPtrNtv_003CISignInState_003E*, void>)(&global::_003CModule_003E.CComPtrNtv_003CISignInState_003E_002E_007Bdtor_007D), &cComPtrNtv_003CISignInState_003E);
				throw;
			}
			global::_003CModule_003E.CComPtrNtv_003CISignInState_003E_002ERelease(&cComPtrNtv_003CISignInState_003E);
		}
		return result;
	}

	public unsafe DateTime GetSubscriptionFreeTrackExpiration()
	{
		DateTime result = DateTime.MaxValue;
		if (IsSignedIn())
		{
			Unsafe.SkipInit(out CComPtrNtv_003CISignInState_003E cComPtrNtv_003CISignInState_003E);
			*(int*)(&cComPtrNtv_003CISignInState_003E) = 0;
			try
			{
				IService* pService = m_pService;
				int num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, ISignInState**, int>)(int)(*(uint*)(*(int*)pService + 172)))((nint)pService, (ISignInState**)(&cComPtrNtv_003CISignInState_003E));
				ushort* ptr = null;
				string text = null;
				if (num >= 0)
				{
					num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, ushort**, int>)(int)(*(uint*)(*(int*)(int)(*(uint*)(&cComPtrNtv_003CISignInState_003E)) + 116)))((IntPtr)(*(int*)(&cComPtrNtv_003CISignInState_003E)), &ptr);
					if (num >= 0)
					{
						if (ptr == null)
						{
							goto IL_0066;
						}
						text = Marshal.PtrToStringBSTR((IntPtr)ptr);
					}
					if (ptr != null)
					{
						global::_003CModule_003E.SysFreeString(ptr);
					}
				}
				goto IL_0066;
				IL_0066:
				if (text != null && text.Length > 0)
				{
					DateTime.TryParse(text, null, DateTimeStyles.AdjustToUniversal | DateTimeStyles.AssumeUniversal, out result);
				}
			}
			catch
			{
				//try-fault
				global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<CComPtrNtv_003CISignInState_003E*, void>)(&global::_003CModule_003E.CComPtrNtv_003CISignInState_003E_002E_007Bdtor_007D), &cComPtrNtv_003CISignInState_003E);
				throw;
			}
			global::_003CModule_003E.CComPtrNtv_003CISignInState_003E_002ERelease(&cComPtrNtv_003CISignInState_003E);
		}
		return result;
	}

	[return: MarshalAs(UnmanagedType.U1)]
	public unsafe bool SubscriptionPendingCancel()
	{
		bool result = false;
		if (IsSignedIn())
		{
			Unsafe.SkipInit(out CComPtrNtv_003CISignInState_003E cComPtrNtv_003CISignInState_003E);
			*(int*)(&cComPtrNtv_003CISignInState_003E) = 0;
			try
			{
				IService* pService = m_pService;
				if (((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, ISignInState**, int>)(int)(*(uint*)(*(int*)pService + 172)))((nint)pService, (ISignInState**)(&cComPtrNtv_003CISignInState_003E)) >= 0 && ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, int>)(int)(*(uint*)(*(int*)(int)(*(uint*)(&cComPtrNtv_003CISignInState_003E)) + 120)))((IntPtr)(*(int*)(&cComPtrNtv_003CISignInState_003E))) != 0)
				{
					result = true;
				}
			}
			catch
			{
				//try-fault
				global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<CComPtrNtv_003CISignInState_003E*, void>)(&global::_003CModule_003E.CComPtrNtv_003CISignInState_003E_002E_007Bdtor_007D), &cComPtrNtv_003CISignInState_003E);
				throw;
			}
			global::_003CModule_003E.CComPtrNtv_003CISignInState_003E_002ERelease(&cComPtrNtv_003CISignInState_003E);
		}
		return result;
	}

	[return: MarshalAs(UnmanagedType.U1)]
	public unsafe bool IsParentallyControlled()
	{
		bool result = false;
		if (IsSignedIn())
		{
			Unsafe.SkipInit(out CComPtrNtv_003CISignInState_003E cComPtrNtv_003CISignInState_003E);
			*(int*)(&cComPtrNtv_003CISignInState_003E) = 0;
			try
			{
				IService* pService = m_pService;
				if (((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, ISignInState**, int>)(int)(*(uint*)(*(int*)pService + 172)))((nint)pService, (ISignInState**)(&cComPtrNtv_003CISignInState_003E)) >= 0)
				{
					bool flag = ((((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, int>)(int)(*(uint*)(*(int*)(int)(*(uint*)(&cComPtrNtv_003CISignInState_003E)) + 60)))((IntPtr)(*(int*)(&cComPtrNtv_003CISignInState_003E))) != 0) ? true : false);
					result = flag;
				}
			}
			catch
			{
				//try-fault
				global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<CComPtrNtv_003CISignInState_003E*, void>)(&global::_003CModule_003E.CComPtrNtv_003CISignInState_003E_002E_007Bdtor_007D), &cComPtrNtv_003CISignInState_003E);
				throw;
			}
			global::_003CModule_003E.CComPtrNtv_003CISignInState_003E_002ERelease(&cComPtrNtv_003CISignInState_003E);
		}
		return result;
	}

	[return: MarshalAs(UnmanagedType.U1)]
	public unsafe bool IsLightWeight()
	{
		bool result = false;
		if (IsSignedIn())
		{
			Unsafe.SkipInit(out CComPtrNtv_003CISignInState_003E cComPtrNtv_003CISignInState_003E);
			*(int*)(&cComPtrNtv_003CISignInState_003E) = 0;
			try
			{
				IService* pService = m_pService;
				if (((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, ISignInState**, int>)(int)(*(uint*)(*(int*)pService + 172)))((nint)pService, (ISignInState**)(&cComPtrNtv_003CISignInState_003E)) >= 0)
				{
					bool flag = ((((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, int>)(int)(*(uint*)(*(int*)(int)(*(uint*)(&cComPtrNtv_003CISignInState_003E)) + 64)))((IntPtr)(*(int*)(&cComPtrNtv_003CISignInState_003E))) != 0) ? true : false);
					result = flag;
				}
			}
			catch
			{
				//try-fault
				global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<CComPtrNtv_003CISignInState_003E*, void>)(&global::_003CModule_003E.CComPtrNtv_003CISignInState_003E_002E_007Bdtor_007D), &cComPtrNtv_003CISignInState_003E);
				throw;
			}
			global::_003CModule_003E.CComPtrNtv_003CISignInState_003E_002ERelease(&cComPtrNtv_003CISignInState_003E);
		}
		return result;
	}

	public unsafe void GetPaymentInstruments(GetPaymentInstrumentsCompleteCallback completeCallback, GetPaymentInstrumentsErrorCallback errorCallback)
	{
		if (m_pService != null)
		{
			GetPaymentInstrumentsCallbackWrapper* ptr = (GetPaymentInstrumentsCallbackWrapper*)global::_003CModule_003E.@new(16u);
			GetPaymentInstrumentsCallbackWrapper* ptr2;
			try
			{
				ptr2 = ((ptr == null) ? null : global::_003CModule_003E.Microsoft_002EZune_002EService_002EGetPaymentInstrumentsCallbackWrapper_002E_007Bctor_007D(ptr, completeCallback, errorCallback));
			}
			catch
			{
				//try-fault
				global::_003CModule_003E.delete(ptr);
				throw;
			}
			if (ptr2 != null)
			{
				IService* pService = m_pService;
				((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, IGetPaymentInstrumentsCallback*, int>)(int)(*(uint*)(*(int*)pService + 396)))((nint)pService, (IGetPaymentInstrumentsCallback*)ptr2);
				((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint>)(int)(*(uint*)(*(int*)ptr2 + 8)))((nint)ptr2);
			}
		}
	}

	public unsafe int AddPaymentInstrument(PaymentInstrument paymentInstrument, AddPaymentInstrumentCompleteCallback completeCallback, AddPaymentInstrumentErrorCallback errorCallback)
	{
		CreditCard creditCard = paymentInstrument as CreditCard;
		int result;
		if (m_pService != null && creditCard == null)
		{
			result = -2147467259;
		}
		else
		{
			AddPaymentInstrumentCallbackWrapper* ptr = (AddPaymentInstrumentCallbackWrapper*)global::_003CModule_003E.@new(20u);
			AddPaymentInstrumentCallbackWrapper* ptr2;
			try
			{
				ptr2 = ((ptr == null) ? null : global::_003CModule_003E.Microsoft_002EZune_002EService_002EAddPaymentInstrumentCallbackWrapper_002E_007Bctor_007D(ptr, creditCard, completeCallback, errorCallback));
			}
			catch
			{
				//try-fault
				global::_003CModule_003E.delete(ptr);
				throw;
			}
			if (ptr2 == null)
			{
				result = -2147024882;
			}
			else
			{
				_SYSTEMTIME sYSTEMTIME = global::_003CModule_003E.DateTimeToSystemTime(creditCard.ExpirationDate);
				fixed (ushort* ptr3 = &Unsafe.As<char, ushort>(ref global::_003CModule_003E.PtrToStringChars(creditCard.Address.Street1)))
				{
					try
					{
						fixed (ushort* ptr4 = &Unsafe.As<char, ushort>(ref global::_003CModule_003E.PtrToStringChars(creditCard.Address.Street2)))
						{
							try
							{
								fixed (ushort* ptr5 = &Unsafe.As<char, ushort>(ref global::_003CModule_003E.PtrToStringChars(creditCard.Address.City)))
								{
									try
									{
										fixed (ushort* ptr6 = &Unsafe.As<char, ushort>(ref global::_003CModule_003E.PtrToStringChars(creditCard.Address.District)))
										{
											try
											{
												fixed (ushort* ptr7 = &Unsafe.As<char, ushort>(ref global::_003CModule_003E.PtrToStringChars(creditCard.Address.State)))
												{
													try
													{
														fixed (ushort* ptr8 = &Unsafe.As<char, ushort>(ref global::_003CModule_003E.PtrToStringChars(creditCard.Address.PostalCode)))
														{
															try
															{
																fixed (ushort* ptr9 = &Unsafe.As<char, ushort>(ref global::_003CModule_003E.PtrToStringChars(creditCard.PhonePrefix)))
																{
																	try
																	{
																		fixed (ushort* ptr10 = &Unsafe.As<char, ushort>(ref global::_003CModule_003E.PtrToStringChars(creditCard.PhoneNumber)))
																		{
																			try
																			{
																				fixed (ushort* ptr11 = &Unsafe.As<char, ushort>(ref global::_003CModule_003E.PtrToStringChars(creditCard.PhoneExtension)))
																				{
																					try
																					{
																						fixed (ushort* ptr12 = &Unsafe.As<char, ushort>(ref global::_003CModule_003E.PtrToStringChars(creditCard.AccountHolderName)))
																						{
																							try
																							{
																								fixed (ushort* ptr13 = &Unsafe.As<char, ushort>(ref global::_003CModule_003E.PtrToStringChars(creditCard.AccountNumber)))
																								{
																									try
																									{
																										fixed (ushort* ptr14 = &Unsafe.As<char, ushort>(ref global::_003CModule_003E.PtrToStringChars(creditCard.CCVNumber)))
																										{
																											try
																											{
																												int num = *(int*)m_pService + 404;
																												result = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, ushort*, ushort*, ushort*, ushort*, ushort*, ushort*, ushort*, ushort*, ushort*, ECreditCardType, ushort*, ushort*, ushort*, _SYSTEMTIME, IAddPaymentInstrumentCallback*, int>)(int)(*(uint*)num))((nint)m_pService, ptr3, ptr4, ptr5, ptr6, ptr7, ptr8, ptr9, ptr10, ptr11, (ECreditCardType)creditCard.CreditCardType, ptr12, ptr13, ptr14, sYSTEMTIME, (IAddPaymentInstrumentCallback*)ptr2);
																											}
																											catch
																											{
																												//try-fault
																												ptr14 = null;
																												throw;
																											}
																										}
																									}
																									catch
																									{
																										//try-fault
																										ptr13 = null;
																										throw;
																									}
																								}
																							}
																							catch
																							{
																								//try-fault
																								ptr12 = null;
																								throw;
																							}
																						}
																					}
																					catch
																					{
																						//try-fault
																						ptr11 = null;
																						throw;
																					}
																				}
																			}
																			catch
																			{
																				//try-fault
																				ptr10 = null;
																				throw;
																			}
																		}
																	}
																	catch
																	{
																		//try-fault
																		ptr9 = null;
																		throw;
																	}
																}
															}
															catch
															{
																//try-fault
																ptr8 = null;
																throw;
															}
														}
													}
													catch
													{
														//try-fault
														ptr7 = null;
														throw;
													}
												}
											}
											catch
											{
												//try-fault
												ptr6 = null;
												throw;
											}
										}
									}
									catch
									{
										//try-fault
										ptr5 = null;
										throw;
									}
								}
							}
							catch
							{
								//try-fault
								ptr4 = null;
								throw;
							}
						}
					}
					catch
					{
						//try-fault
						ptr3 = null;
						throw;
					}
				}
				if (ptr2 != null)
				{
					((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint>)(int)(*(uint*)(*(int*)ptr2 + 8)))((nint)ptr2);
				}
			}
		}
		return result;
	}

	public unsafe int AddPaymentInstrument(PaymentInstrument paymentInstrument, out string paymentId, out ServiceError serviceError)
	{
		CreditCard creditCard = paymentInstrument as CreditCard;
		int num = 0;
		if (m_pService == null || creditCard == null)
		{
			num = -2147467259;
		}
		Unsafe.SkipInit(out WBSTRString wBSTRString);
		global::_003CModule_003E.WBSTRString_002E_007Bctor_007D(&wBSTRString);
		try
		{
			Unsafe.SkipInit(out CComPtrNtv_003CIServiceError_003E cComPtrNtv_003CIServiceError_003E);
			*(int*)(&cComPtrNtv_003CIServiceError_003E) = 0;
			try
			{
				if (num >= 0)
				{
					_SYSTEMTIME sYSTEMTIME = global::_003CModule_003E.DateTimeToSystemTime(creditCard.ExpirationDate);
					fixed (ushort* ptr = &Unsafe.As<char, ushort>(ref global::_003CModule_003E.PtrToStringChars(creditCard.Address.Street1)))
					{
						try
						{
							fixed (ushort* ptr2 = &Unsafe.As<char, ushort>(ref global::_003CModule_003E.PtrToStringChars(creditCard.Address.Street2)))
							{
								try
								{
									fixed (ushort* ptr3 = &Unsafe.As<char, ushort>(ref global::_003CModule_003E.PtrToStringChars(creditCard.Address.City)))
									{
										try
										{
											fixed (ushort* ptr4 = &Unsafe.As<char, ushort>(ref global::_003CModule_003E.PtrToStringChars(creditCard.Address.District)))
											{
												try
												{
													fixed (ushort* ptr5 = &Unsafe.As<char, ushort>(ref global::_003CModule_003E.PtrToStringChars(creditCard.Address.State)))
													{
														try
														{
															fixed (ushort* ptr6 = &Unsafe.As<char, ushort>(ref global::_003CModule_003E.PtrToStringChars(creditCard.Address.PostalCode)))
															{
																try
																{
																	fixed (ushort* ptr7 = &Unsafe.As<char, ushort>(ref global::_003CModule_003E.PtrToStringChars(creditCard.PhonePrefix)))
																	{
																		try
																		{
																			fixed (ushort* ptr8 = &Unsafe.As<char, ushort>(ref global::_003CModule_003E.PtrToStringChars(creditCard.PhoneNumber)))
																			{
																				try
																				{
																					fixed (ushort* ptr9 = &Unsafe.As<char, ushort>(ref global::_003CModule_003E.PtrToStringChars(creditCard.PhoneExtension)))
																					{
																						try
																						{
																							fixed (ushort* ptr10 = &Unsafe.As<char, ushort>(ref global::_003CModule_003E.PtrToStringChars(creditCard.AccountHolderName)))
																							{
																								try
																								{
																									fixed (ushort* ptr11 = &Unsafe.As<char, ushort>(ref global::_003CModule_003E.PtrToStringChars(creditCard.AccountNumber)))
																									{
																										try
																										{
																											fixed (ushort* ptr12 = &Unsafe.As<char, ushort>(ref global::_003CModule_003E.PtrToStringChars(creditCard.CCVNumber)))
																											{
																												try
																												{
																													int num2 = *(int*)m_pService + 400;
																													num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, ushort*, ushort*, ushort*, ushort*, ushort*, ushort*, ushort*, ushort*, ushort*, ECreditCardType, ushort*, ushort*, ushort*, _SYSTEMTIME, ushort**, IServiceError**, int>)(int)(*(uint*)num2))((nint)m_pService, ptr, ptr2, ptr3, ptr4, ptr5, ptr6, ptr7, ptr8, ptr9, (ECreditCardType)creditCard.CreditCardType, ptr10, ptr11, ptr12, sYSTEMTIME, (ushort**)(&wBSTRString), global::_003CModule_003E.CComPtrNtv_003CIServiceError_003E_002E_0026(&cComPtrNtv_003CIServiceError_003E));
																												}
																												catch
																												{
																													//try-fault
																													ptr12 = null;
																													throw;
																												}
																											}
																										}
																										catch
																										{
																											//try-fault
																											ptr11 = null;
																											throw;
																										}
																									}
																								}
																								catch
																								{
																									//try-fault
																									ptr10 = null;
																									throw;
																								}
																							}
																						}
																						catch
																						{
																							//try-fault
																							ptr9 = null;
																							throw;
																						}
																					}
																				}
																				catch
																				{
																					//try-fault
																					ptr8 = null;
																					throw;
																				}
																			}
																		}
																		catch
																		{
																			//try-fault
																			ptr7 = null;
																			throw;
																		}
																	}
																}
																catch
																{
																	//try-fault
																	ptr6 = null;
																	throw;
																}
															}
														}
														catch
														{
															//try-fault
															ptr5 = null;
															throw;
														}
													}
												}
												catch
												{
													//try-fault
													ptr4 = null;
													throw;
												}
											}
										}
										catch
										{
											//try-fault
											ptr3 = null;
											throw;
										}
									}
								}
								catch
								{
									//try-fault
									ptr2 = null;
									throw;
								}
							}
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
						paymentId = new string((char*)(int)(*(uint*)(&wBSTRString)));
					}
				}
				if (*(int*)(&cComPtrNtv_003CIServiceError_003E) != 0)
				{
					serviceError = new ServiceError((IServiceError*)(int)(*(uint*)(&cComPtrNtv_003CIServiceError_003E)));
				}
				else
				{
					serviceError = null;
				}
			}
			catch
			{
				//try-fault
				global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<CComPtrNtv_003CIServiceError_003E*, void>)(&global::_003CModule_003E.CComPtrNtv_003CIServiceError_003E_002E_007Bdtor_007D), &cComPtrNtv_003CIServiceError_003E);
				throw;
			}
			global::_003CModule_003E.CComPtrNtv_003CIServiceError_003E_002ERelease(&cComPtrNtv_003CIServiceError_003E);
		}
		catch
		{
			//try-fault
			global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<WBSTRString*, void>)(&global::_003CModule_003E.WBSTRString_002E_007Bdtor_007D), &wBSTRString);
			throw;
		}
		global::_003CModule_003E.WString_002E_007Bdtor_007D((WString*)(&wBSTRString));
		return num;
	}

	public unsafe void GetSubscriptionOffers(GetBillingOffersCompleteCallback completeCallback, GetBillingOffersErrorCallback errorCallback)
	{
		if (m_pService != null)
		{
			GetBillingOffersCallbackWrapper* ptr = (GetBillingOffersCallbackWrapper*)global::_003CModule_003E.@new(16u);
			GetBillingOffersCallbackWrapper* ptr2;
			try
			{
				ptr2 = ((ptr == null) ? null : global::_003CModule_003E.Microsoft_002EZune_002EService_002EGetBillingOffersCallbackWrapper_002E_007Bctor_007D(ptr, completeCallback, errorCallback));
			}
			catch
			{
				//try-fault
				global::_003CModule_003E.delete(ptr);
				throw;
			}
			if (ptr2 != null)
			{
				IService* pService = m_pService;
				((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, IGetBillingOffersCallback*, int>)(int)(*(uint*)(*(int*)pService + 368)))((nint)pService, (IGetBillingOffersCallback*)ptr2);
				((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint>)(int)(*(uint*)(*(int*)ptr2 + 8)))((nint)ptr2);
			}
		}
	}

	public unsafe void GetSubscriptionDetails(ulong offerId, GetBillingOffersCompleteCallback completeCallback, GetBillingOffersErrorCallback errorCallback)
	{
		if (m_pService != null)
		{
			GetBillingOffersCallbackWrapper* ptr = (GetBillingOffersCallbackWrapper*)global::_003CModule_003E.@new(16u);
			GetBillingOffersCallbackWrapper* ptr2;
			try
			{
				ptr2 = ((ptr == null) ? null : global::_003CModule_003E.Microsoft_002EZune_002EService_002EGetBillingOffersCallbackWrapper_002E_007Bctor_007D(ptr, completeCallback, errorCallback));
			}
			catch
			{
				//try-fault
				global::_003CModule_003E.delete(ptr);
				throw;
			}
			if (ptr2 != null)
			{
				IService* pService = m_pService;
				((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, ulong, IGetBillingOffersCallback*, int>)(int)(*(uint*)(*(int*)pService + 372)))((nint)pService, offerId, (IGetBillingOffersCallback*)ptr2);
				((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint>)(int)(*(uint*)(*(int*)ptr2 + 8)))((nint)ptr2);
			}
		}
	}

	public unsafe void GetPointsOffers(GetBillingOffersCompleteCallback completeCallback, GetBillingOffersErrorCallback errorCallback)
	{
		if (m_pService != null)
		{
			GetBillingOffersCallbackWrapper* ptr = (GetBillingOffersCallbackWrapper*)global::_003CModule_003E.@new(16u);
			GetBillingOffersCallbackWrapper* ptr2;
			try
			{
				ptr2 = ((ptr == null) ? null : global::_003CModule_003E.Microsoft_002EZune_002EService_002EGetBillingOffersCallbackWrapper_002E_007Bctor_007D(ptr, completeCallback, errorCallback));
			}
			catch
			{
				//try-fault
				global::_003CModule_003E.delete(ptr);
				throw;
			}
			if (ptr2 != null)
			{
				IService* pService = m_pService;
				((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, IGetBillingOffersCallback*, int>)(int)(*(uint*)(*(int*)pService + 376)))((nint)pService, (IGetBillingOffersCallback*)ptr2);
				((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint>)(int)(*(uint*)(*(int*)ptr2 + 8)))((nint)ptr2);
			}
		}
	}

	public unsafe int PurchaseBillingOffer(BillingOffer offer, PaymentInstrument paymentInstrument, AsyncCompleteHandler callback)
	{
		int num = 0;
		if (m_pService == null || offer == null || paymentInstrument == null)
		{
			num = -2147467259;
		}
		AsyncCallbackWrapper* ptr = null;
		if (num >= 0)
		{
			AsyncCallbackWrapper* ptr2 = (AsyncCallbackWrapper*)global::_003CModule_003E.@new(12u);
			AsyncCallbackWrapper* ptr3;
			try
			{
				ptr3 = ((ptr2 == null) ? null : global::_003CModule_003E.Microsoft_002EZune_002EUtil_002EAsyncCallbackWrapper_002E_007Bctor_007D(ptr2, callback));
			}
			catch
			{
				//try-fault
				global::_003CModule_003E.delete(ptr2);
				throw;
			}
			ptr = ptr3;
			if (ptr3 == null)
			{
				num = -2147024882;
			}
		}
		EBillingPaymentType eBillingPaymentType = (EBillingPaymentType)(-1);
		if (num >= 0)
		{
			num = PaymentTypeToBillingPaymentType(paymentInstrument.Type, &eBillingPaymentType);
			if (num >= 0)
			{
				fixed (ushort* ptr4 = &Unsafe.As<char, ushort>(ref global::_003CModule_003E.PtrToStringChars(paymentInstrument.Id)))
				{
					try
					{
						int num2 = *(int*)m_pService + 384;
						num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, ulong, global::EBillingOfferType, ushort*, EBillingPaymentType, int, IAsyncCallback*, int>)(int)(*(uint*)num2))((nint)m_pService, offer.Id, (global::EBillingOfferType)offer.OfferType, ptr4, eBillingPaymentType, (int)offer.Points, (IAsyncCallback*)ptr);
					}
					catch
					{
						//try-fault
						ptr4 = null;
						throw;
					}
				}
			}
		}
		if (ptr != null)
		{
			AsyncCallbackWrapper* intPtr = ptr;
			((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint>)(int)(*(uint*)(*(int*)intPtr + 8)))((nint)intPtr);
		}
		return num;
	}

	public unsafe int PurchaseBillingOffer(BillingOffer offer, PaymentInstrument paymentInstrument)
	{
		int num = 0;
		if (m_pService == null || offer == null || paymentInstrument == null)
		{
			num = -2147467259;
		}
		EBillingPaymentType eBillingPaymentType = (EBillingPaymentType)(-1);
		if (num >= 0)
		{
			num = PaymentTypeToBillingPaymentType(paymentInstrument.Type, &eBillingPaymentType);
			if (num >= 0)
			{
				fixed (ushort* ptr = &Unsafe.As<char, ushort>(ref global::_003CModule_003E.PtrToStringChars(paymentInstrument.Id)))
				{
					try
					{
						int num2 = *(int*)m_pService + 380;
						num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, ulong, global::EBillingOfferType, ushort*, EBillingPaymentType, int, int>)(int)(*(uint*)num2))((nint)m_pService, offer.Id, (global::EBillingOfferType)offer.OfferType, ptr, eBillingPaymentType, (int)offer.Points);
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
		return num;
	}

	public unsafe string GetMachineId()
	{
		string result = null;
		IService* pService = m_pService;
		if (pService != null)
		{
			ushort* ptr = null;
			if (((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, ushort**, int>)(int)(*(uint*)(*(int*)pService + 464)))((nint)pService, &ptr) >= 0)
			{
				result = new string((char*)ptr);
				global::_003CModule_003E.SysFreeString(ptr);
			}
		}
		return result;
	}

	public unsafe int ResumePurchase(string resumeHandle, string authorizationToken, AsyncCompleteHandler callback)
	{
		AsyncCallbackWrapper* ptr = (AsyncCallbackWrapper*)global::_003CModule_003E.@new(12u);
		AsyncCallbackWrapper* ptr2;
		try
		{
			ptr2 = ((ptr == null) ? null : global::_003CModule_003E.Microsoft_002EZune_002EUtil_002EAsyncCallbackWrapper_002E_007Bctor_007D(ptr, callback));
		}
		catch
		{
			//try-fault
			global::_003CModule_003E.delete(ptr);
			throw;
		}
		fixed (ushort* ptr3 = &Unsafe.As<char, ushort>(ref global::_003CModule_003E.PtrToStringChars(resumeHandle)))
		{
			fixed (ushort* ptr4 = &Unsafe.As<char, ushort>(ref global::_003CModule_003E.PtrToStringChars(authorizationToken)))
			{
				int num = *(int*)m_pService + 364;
				return ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, ushort*, ushort*, IAsyncCallback*, int>)(int)(*(uint*)num))((nint)m_pService, ptr3, ptr4, (IAsyncCallback*)ptr2);
			}
		}
	}

	public unsafe CountryBaseDetails[] GetCountryDetails()
	{
		CountryBaseDetails[] array = null;
		if (m_pService != null)
		{
			Unsafe.SkipInit(out CComPtrNtv_003CITunerConfig_003E cComPtrNtv_003CITunerConfig_003E);
			*(int*)(&cComPtrNtv_003CITunerConfig_003E) = 0;
			try
			{
				IService* pService = m_pService;
				int num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, ITunerConfig**, int>)(int)(*(uint*)(*(int*)pService + 88)))((nint)pService, (ITunerConfig**)(&cComPtrNtv_003CITunerConfig_003E));
				int num2 = 0;
				if (num >= 0)
				{
					num2 = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, int>)(int)(*(uint*)(*(int*)(int)(*(uint*)(&cComPtrNtv_003CITunerConfig_003E)) + 56)))((IntPtr)(*(int*)(&cComPtrNtv_003CITunerConfig_003E)));
					array = new CountryBaseDetails[num2];
				}
				int num3 = 0;
				if (0 < num2)
				{
					Unsafe.SkipInit(out WBSTRString wBSTRString);
					do
					{
						int num4 = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, int, int>)(int)(*(uint*)(*(int*)(int)(*(uint*)(&cComPtrNtv_003CITunerConfig_003E)) + 60)))((IntPtr)(*(int*)(&cComPtrNtv_003CITunerConfig_003E)), num3);
						ushort** ptr = null;
						if (num4 != 0)
						{
							ptr = (ushort**)global::_003CModule_003E.new_005B_005D(((uint)num4 > 1073741823u) ? uint.MaxValue : ((uint)(num4 << 2)));
							if (ptr == null)
							{
								num = -2147024882;
							}
						}
						global::_003CModule_003E.WBSTRString_002E_007Bctor_007D(&wBSTRString);
						try
						{
							int teenagerAge = 0;
							int adultAge = 0;
							int num5 = 0;
							int num6 = 0;
							int num7 = 0;
							if (num >= 0)
							{
								num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, int, int, ushort**, ushort**, int*, int*, int*, int*, int*, int>)(int)(*(uint*)(*(int*)(int)(*(uint*)(&cComPtrNtv_003CITunerConfig_003E)) + 64)))((IntPtr)(*(int*)(&cComPtrNtv_003CITunerConfig_003E)), num3, num4, (ushort**)(&wBSTRString), ptr, &teenagerAge, &adultAge, &num5, &num6, &num7);
								if (num >= 0)
								{
									CountryFieldValidator[] array2 = new CountryFieldValidator[num7];
									int num8 = 0;
									if (0 < num7)
									{
										do
										{
											ushort* value = null;
											ushort* value2 = null;
											ushort* value3 = null;
											ushort* value4 = null;
											num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, int, int, ushort**, ushort**, ushort**, ushort**, int>)(int)(*(uint*)(*(int*)(int)(*(uint*)(&cComPtrNtv_003CITunerConfig_003E)) + 68)))((IntPtr)(*(int*)(&cComPtrNtv_003CITunerConfig_003E)), num3, num8, &value, &value2, &value3, &value4);
											if (num >= 0)
											{
												CountryFieldValidator countryFieldValidator = new CountryFieldValidator(new string((char*)value), new string((char*)value2), new string((char*)value3), new string((char*)value4));
												array2[num8] = countryFieldValidator;
											}
											global::_003CModule_003E.SafeSysFreeString(&value);
											global::_003CModule_003E.SafeSysFreeString(&value2);
											global::_003CModule_003E.SafeSysFreeString(&value3);
											global::_003CModule_003E.SafeSysFreeString(&value4);
											num8++;
										}
										while (num8 < num7);
									}
									if (num >= 0)
									{
										string abbreviation = new string((char*)(int)(*(uint*)(&wBSTRString)));
										string[] array3 = new string[num4];
										int num9 = 0;
										if (0 < num4)
										{
											do
											{
												int num10 = num9;
												array3[num10] = new string((char*)(int)(*(uint*)(num10 * 4 + (byte*)ptr)));
												num9++;
											}
											while (num9 < num4);
										}
										bool usageCollection = ((num6 != 0) ? true : false);
										bool showNewsletterOptions = ((num5 != 0) ? true : false);
										array[num3] = new CountryBaseDetails(abbreviation, array3, teenagerAge, adultAge, showNewsletterOptions, usageCollection, array2);
									}
								}
							}
							if (ptr != null)
							{
								if (0 < num4)
								{
									ushort** ptr2 = ptr;
									int num11 = num4;
									do
									{
										global::_003CModule_003E.SafeSysFreeString(ptr2);
										ptr2 = (ushort**)((byte*)ptr2 + 4);
										num11--;
									}
									while (num11 != 0);
								}
								global::_003CModule_003E.delete_005B_005D(ptr);
							}
						}
						catch
						{
							//try-fault
							global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<WBSTRString*, void>)(&global::_003CModule_003E.WBSTRString_002E_007Bdtor_007D), &wBSTRString);
							throw;
						}
						global::_003CModule_003E.WBSTRString_002E_007Bdtor_007D(&wBSTRString);
						num3++;
					}
					while (num3 < num2);
				}
			}
			catch
			{
				//try-fault
				global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<CComPtrNtv_003CITunerConfig_003E*, void>)(&global::_003CModule_003E.CComPtrNtv_003CITunerConfig_003E_002E_007Bdtor_007D), &cComPtrNtv_003CITunerConfig_003E);
				throw;
			}
			global::_003CModule_003E.CComPtrNtv_003CITunerConfig_003E_002ERelease(&cComPtrNtv_003CITunerConfig_003E);
		}
		return array;
	}

	public unsafe RatingSystemBase[] GetRatingSystems()
	{
		RatingSystemBase[] array = null;
		if (m_pService != null)
		{
			Unsafe.SkipInit(out CComPtrNtv_003CITunerConfig_003E cComPtrNtv_003CITunerConfig_003E);
			*(int*)(&cComPtrNtv_003CITunerConfig_003E) = 0;
			try
			{
				IService* pService = m_pService;
				int num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, ITunerConfig**, int>)(int)(*(uint*)(*(int*)pService + 88)))((nint)pService, (ITunerConfig**)(&cComPtrNtv_003CITunerConfig_003E));
				int num2 = 0;
				if (num >= 0)
				{
					num2 = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, int>)(int)(*(uint*)(*(int*)(int)(*(uint*)(&cComPtrNtv_003CITunerConfig_003E)) + 72)))((IntPtr)(*(int*)(&cComPtrNtv_003CITunerConfig_003E)));
					array = new RatingSystemBase[num2];
				}
				int num3 = 0;
				if (0 < num2)
				{
					Unsafe.SkipInit(out WBSTRString wBSTRString);
					Unsafe.SkipInit(out WBSTRString wBSTRString2);
					Unsafe.SkipInit(out WBSTRString wBSTRString3);
					Unsafe.SkipInit(out WBSTRString wBSTRString4);
					Unsafe.SkipInit(out WBSTRString wBSTRString5);
					Unsafe.SkipInit(out WBSTRString wBSTRString6);
					Unsafe.SkipInit(out WBSTRString wBSTRString7);
					Unsafe.SkipInit(out WBSTRString wBSTRString8);
					Unsafe.SkipInit(out WBSTRString wBSTRString9);
					Unsafe.SkipInit(out WBSTRString wBSTRString10);
					Unsafe.SkipInit(out WBSTRString wBSTRString11);
					Unsafe.SkipInit(out int order);
					Unsafe.SkipInit(out int num15);
					do
					{
						int num4 = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, int, int>)(int)(*(uint*)(*(int*)(int)(*(uint*)(&cComPtrNtv_003CITunerConfig_003E)) + 84)))((IntPtr)(*(int*)(&cComPtrNtv_003CITunerConfig_003E)), num3);
						int num5 = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, int, int>)(int)(*(uint*)(*(int*)(int)(*(uint*)(&cComPtrNtv_003CITunerConfig_003E)) + 80)))((IntPtr)(*(int*)(&cComPtrNtv_003CITunerConfig_003E)), num3);
						int num6 = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, int, int>)(int)(*(uint*)(*(int*)(int)(*(uint*)(&cComPtrNtv_003CITunerConfig_003E)) + 76)))((IntPtr)(*(int*)(&cComPtrNtv_003CITunerConfig_003E)), num3);
						Dictionary<string, Dictionary<string, string>> dictionary = new Dictionary<string, Dictionary<string, string>>();
						global::_003CModule_003E.WBSTRString_002E_007Bctor_007D(&wBSTRString);
						try
						{
							global::_003CModule_003E.WBSTRString_002E_007Bctor_007D(&wBSTRString2);
							try
							{
								global::_003CModule_003E.WBSTRString_002E_007Bctor_007D(&wBSTRString3);
								try
								{
									global::_003CModule_003E.WBSTRString_002E_007Bctor_007D(&wBSTRString4);
									try
									{
										global::_003CModule_003E.WBSTRString_002E_007Bctor_007D(&wBSTRString5);
										try
										{
											int num7 = 0;
											int num8 = 0;
											int num9 = 0;
											if (0 < num5)
											{
												do
												{
													ITunerConfig* ptr = global::_003CModule_003E.CComPtrNtv_003CITunerConfig_003E_002E_002D_003E(&cComPtrNtv_003CITunerConfig_003E);
													int num10 = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, int, int, int>)(int)(*(uint*)(*(int*)ptr + 88)))((nint)ptr, num3, num9);
													ushort** ptr2 = null;
													if (num >= 0)
													{
														ptr2 = (ushort**)global::_003CModule_003E.new_005B_005D(((uint)num10 > 1073741823u) ? uint.MaxValue : ((uint)(num10 << 2)));
														if (ptr2 == null)
														{
															num = -2147024882;
														}
													}
													ushort** ptr3 = null;
													if (num >= 0)
													{
														ptr3 = (ushort**)global::_003CModule_003E.new_005B_005D(((uint)num10 > 1073741823u) ? uint.MaxValue : ((uint)(num10 << 2)));
													}
													global::_003CModule_003E.WString_002EDeleteString((WString*)(&wBSTRString));
													global::_003CModule_003E.WString_002EDeleteString((WString*)(&wBSTRString2));
													global::_003CModule_003E.WString_002EDeleteString((WString*)(&wBSTRString3));
													global::_003CModule_003E.WString_002EDeleteString((WString*)(&wBSTRString4));
													global::_003CModule_003E.WBSTRString_002E_007Bctor_007D(&wBSTRString6);
													try
													{
														ITunerConfig* ptr4 = global::_003CModule_003E.CComPtrNtv_003CITunerConfig_003E_002E_002D_003E(&cComPtrNtv_003CITunerConfig_003E);
														num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, int, int, int, ushort**, ushort**, ushort**, ushort**, ushort**, int*, int*, ushort**, ushort**, int>)(int)(*(uint*)(*(int*)ptr4 + 92)))((nint)ptr4, num3, num9, num10, (ushort**)(&wBSTRString), (ushort**)(&wBSTRString2), (ushort**)(&wBSTRString3), (ushort**)(&wBSTRString4), (ushort**)(&wBSTRString6), &num7, &num8, ptr2, ptr3);
														if (num >= 0)
														{
															string key = new string((char*)(int)(*(uint*)(&wBSTRString6)));
															Dictionary<string, string> dictionary2 = new Dictionary<string, string>();
															if (num9 == num6)
															{
																global::_003CModule_003E.WString_002EDeleteString((WString*)(&wBSTRString5));
																global::_003CModule_003E.WString_002EInit((WString*)(&wBSTRString5), (ushort*)(int)(*(uint*)(&wBSTRString6)), uint.MaxValue);
															}
															if (0 < num10)
															{
																ushort** ptr5 = ptr2;
																ushort** ptr6 = (ushort**)((byte*)ptr3 - (nuint)ptr2);
																int num11 = num10;
																do
																{
																	dictionary2.Add(new string((char*)(int)(*(uint*)ptr5)), new string((char*)(int)(*(uint*)((byte*)ptr6 + (nuint)ptr5))));
																	ptr5 = (ushort**)((byte*)ptr5 + 4);
																	num11--;
																}
																while (num11 != 0);
															}
															dictionary.Add(key, dictionary2);
															if (ptr2 != null)
															{
																if (0 < num10)
																{
																	ushort** ptr7 = ptr2;
																	int num12 = num10;
																	do
																	{
																		global::_003CModule_003E.SafeSysFreeString(ptr7);
																		ptr7 = (ushort**)((byte*)ptr7 + 4);
																		num12--;
																	}
																	while (num12 != 0);
																}
																global::_003CModule_003E.delete_005B_005D(ptr2);
															}
															if (ptr3 != null)
															{
																if (0 < num10)
																{
																	ushort** ptr8 = ptr3;
																	int num13 = num10;
																	do
																	{
																		global::_003CModule_003E.SafeSysFreeString(ptr8);
																		ptr8 = (ushort**)((byte*)ptr8 + 4);
																		num13--;
																	}
																	while (num13 != 0);
																}
																global::_003CModule_003E.delete_005B_005D(ptr3);
															}
														}
													}
													catch
													{
														//try-fault
														global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<WBSTRString*, void>)(&global::_003CModule_003E.WBSTRString_002E_007Bdtor_007D), &wBSTRString6);
														throw;
													}
													global::_003CModule_003E.WBSTRString_002E_007Bdtor_007D(&wBSTRString6);
													num9++;
												}
												while (num9 < num5);
											}
											if (num >= 0)
											{
												RatingValue[] array2 = new RatingValue[num4];
												int num14 = 0;
												if (0 < num4)
												{
													do
													{
														global::_003CModule_003E.WBSTRString_002E_007Bctor_007D(&wBSTRString7);
														try
														{
															global::_003CModule_003E.WBSTRString_002E_007Bctor_007D(&wBSTRString8);
															try
															{
																global::_003CModule_003E.WBSTRString_002E_007Bctor_007D(&wBSTRString9);
																try
																{
																	global::_003CModule_003E.WBSTRString_002E_007Bctor_007D(&wBSTRString10);
																	try
																	{
																		global::_003CModule_003E.WBSTRString_002E_007Bctor_007D(&wBSTRString11);
																		try
																		{
																			num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, int, int, ushort**, int*, ushort**, ushort**, ushort**, ushort**, int*, int>)(int)(*(uint*)(*(int*)(int)(*(uint*)(&cComPtrNtv_003CITunerConfig_003E)) + 96)))((IntPtr)(*(int*)(&cComPtrNtv_003CITunerConfig_003E)), num3, num14, (ushort**)(&wBSTRString7), &order, (ushort**)(&wBSTRString10), (ushort**)(&wBSTRString8), (ushort**)(&wBSTRString9), (ushort**)(&wBSTRString11), &num15);
																			if (num >= 0)
																			{
																				bool treatAsUnrated = ((num15 != 0) ? true : false);
																				ushort* value = (ushort*)(int)(*(uint*)(&wBSTRString9));
																				ushort* value2 = (ushort*)(int)(*(uint*)(&wBSTRString10));
																				ushort* value3 = (ushort*)(int)(*(uint*)(&wBSTRString8));
																				RatingValue ratingValue = new RatingValue(new string((char*)(int)(*(uint*)(&wBSTRString7))), order, new string((char*)value3), new string((char*)value2), new string((char*)value), new string((char*)(int)(*(uint*)(&wBSTRString11))), treatAsUnrated);
																				array2[num14] = ratingValue;
																			}
																		}
																		catch
																		{
																			//try-fault
																			global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<WBSTRString*, void>)(&global::_003CModule_003E.WBSTRString_002E_007Bdtor_007D), &wBSTRString11);
																			throw;
																		}
																		global::_003CModule_003E.WBSTRString_002E_007Bdtor_007D(&wBSTRString11);
																	}
																	catch
																	{
																		//try-fault
																		global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<WBSTRString*, void>)(&global::_003CModule_003E.WBSTRString_002E_007Bdtor_007D), &wBSTRString10);
																		throw;
																	}
																	global::_003CModule_003E.WBSTRString_002E_007Bdtor_007D(&wBSTRString10);
																}
																catch
																{
																	//try-fault
																	global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<WBSTRString*, void>)(&global::_003CModule_003E.WBSTRString_002E_007Bdtor_007D), &wBSTRString9);
																	throw;
																}
																global::_003CModule_003E.WBSTRString_002E_007Bdtor_007D(&wBSTRString9);
															}
															catch
															{
																//try-fault
																global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<WBSTRString*, void>)(&global::_003CModule_003E.WBSTRString_002E_007Bdtor_007D), &wBSTRString8);
																throw;
															}
															global::_003CModule_003E.WBSTRString_002E_007Bdtor_007D(&wBSTRString8);
														}
														catch
														{
															//try-fault
															global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<WBSTRString*, void>)(&global::_003CModule_003E.WBSTRString_002E_007Bdtor_007D), &wBSTRString7);
															throw;
														}
														global::_003CModule_003E.WBSTRString_002E_007Bdtor_007D(&wBSTRString7);
														num14++;
													}
													while (num14 < num4);
												}
												if (num >= 0)
												{
													bool showBlockUnrated = ((num8 != 0) ? true : false);
													bool useImages = ((num7 != 0) ? true : false);
													ushort* value4 = (ushort*)(int)(*(uint*)(&wBSTRString4));
													ushort* value5 = (ushort*)(int)(*(uint*)(&wBSTRString3));
													ushort* value6 = (ushort*)(int)(*(uint*)(&wBSTRString2));
													RatingSystemBase ratingSystemBase = new RatingSystemBase(new string((char*)(int)(*(uint*)(&wBSTRString))), new string((char*)value6), new string((char*)value5), new string((char*)value4), useImages, showBlockUnrated, new string((char*)(int)(*(uint*)(&wBSTRString5))), dictionary, array2);
													array[num3] = ratingSystemBase;
												}
											}
										}
										catch
										{
											//try-fault
											global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<WBSTRString*, void>)(&global::_003CModule_003E.WBSTRString_002E_007Bdtor_007D), &wBSTRString5);
											throw;
										}
										global::_003CModule_003E.WBSTRString_002E_007Bdtor_007D(&wBSTRString5);
									}
									catch
									{
										//try-fault
										global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<WBSTRString*, void>)(&global::_003CModule_003E.WBSTRString_002E_007Bdtor_007D), &wBSTRString4);
										throw;
									}
									global::_003CModule_003E.WBSTRString_002E_007Bdtor_007D(&wBSTRString4);
								}
								catch
								{
									//try-fault
									global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<WBSTRString*, void>)(&global::_003CModule_003E.WBSTRString_002E_007Bdtor_007D), &wBSTRString3);
									throw;
								}
								global::_003CModule_003E.WBSTRString_002E_007Bdtor_007D(&wBSTRString3);
							}
							catch
							{
								//try-fault
								global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<WBSTRString*, void>)(&global::_003CModule_003E.WBSTRString_002E_007Bdtor_007D), &wBSTRString2);
								throw;
							}
							global::_003CModule_003E.WBSTRString_002E_007Bdtor_007D(&wBSTRString2);
						}
						catch
						{
							//try-fault
							global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<WBSTRString*, void>)(&global::_003CModule_003E.WBSTRString_002E_007Bdtor_007D), &wBSTRString);
							throw;
						}
						global::_003CModule_003E.WBSTRString_002E_007Bdtor_007D(&wBSTRString);
						num3++;
					}
					while (num3 < num2);
				}
			}
			catch
			{
				//try-fault
				global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<CComPtrNtv_003CITunerConfig_003E*, void>)(&global::_003CModule_003E.CComPtrNtv_003CITunerConfig_003E_002E_007Bdtor_007D), &cComPtrNtv_003CITunerConfig_003E);
				throw;
			}
			global::_003CModule_003E.CComPtrNtv_003CITunerConfig_003E_002E_007Bdtor_007D(&cComPtrNtv_003CITunerConfig_003E);
		}
		return array;
	}

	public unsafe int GetRentalTermDays(string strStudio)
	{
		int result = 14;
		fixed (ushort* ptr = &Unsafe.As<char, ushort>(ref global::_003CModule_003E.PtrToStringChars(strStudio)))
		{
			if (m_pService != null)
			{
				Unsafe.SkipInit(out CComPtrNtv_003CITunerConfig_003E cComPtrNtv_003CITunerConfig_003E);
				*(int*)(&cComPtrNtv_003CITunerConfig_003E) = 0;
				try
				{
					IService* pService = m_pService;
					if (((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, ITunerConfig**, int>)(int)(*(uint*)(*(int*)pService + 88)))((nint)pService, (ITunerConfig**)(&cComPtrNtv_003CITunerConfig_003E)) >= 0)
					{
						int num = *(int*)(int)(*(uint*)(&cComPtrNtv_003CITunerConfig_003E)) + 104;
						((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, ushort*, int*, int>)(int)(*(uint*)num))((IntPtr)(*(int*)(&cComPtrNtv_003CITunerConfig_003E)), ptr, &result);
					}
				}
				catch
				{
					//try-fault
					global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<CComPtrNtv_003CITunerConfig_003E*, void>)(&global::_003CModule_003E.CComPtrNtv_003CITunerConfig_003E_002E_007Bdtor_007D), &cComPtrNtv_003CITunerConfig_003E);
					throw;
				}
				global::_003CModule_003E.CComPtrNtv_003CITunerConfig_003E_002ERelease(&cComPtrNtv_003CITunerConfig_003E);
			}
			return result;
		}
	}

	public unsafe int GetRentalTermHours(string strStudio)
	{
		int result = 24;
		fixed (ushort* ptr = &Unsafe.As<char, ushort>(ref global::_003CModule_003E.PtrToStringChars(strStudio)))
		{
			if (m_pService != null)
			{
				Unsafe.SkipInit(out CComPtrNtv_003CITunerConfig_003E cComPtrNtv_003CITunerConfig_003E);
				*(int*)(&cComPtrNtv_003CITunerConfig_003E) = 0;
				try
				{
					IService* pService = m_pService;
					if (((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, ITunerConfig**, int>)(int)(*(uint*)(*(int*)pService + 88)))((nint)pService, (ITunerConfig**)(&cComPtrNtv_003CITunerConfig_003E)) >= 0)
					{
						int num = *(int*)(int)(*(uint*)(&cComPtrNtv_003CITunerConfig_003E)) + 108;
						((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, ushort*, int*, int>)(int)(*(uint*)num))((IntPtr)(*(int*)(&cComPtrNtv_003CITunerConfig_003E)), ptr, &result);
					}
				}
				catch
				{
					//try-fault
					global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<CComPtrNtv_003CITunerConfig_003E*, void>)(&global::_003CModule_003E.CComPtrNtv_003CITunerConfig_003E_002E_007Bdtor_007D), &cComPtrNtv_003CITunerConfig_003E);
					throw;
				}
				global::_003CModule_003E.CComPtrNtv_003CITunerConfig_003E_002ERelease(&cComPtrNtv_003CITunerConfig_003E);
			}
			return result;
		}
	}

	public unsafe string GetPhoneClientType(string strPhoneOsVersion)
	{
		string result = null;
		ushort* ptr = null;
		fixed (ushort* ptr2 = &Unsafe.As<char, ushort>(ref global::_003CModule_003E.PtrToStringChars(strPhoneOsVersion)))
		{
			if (m_pService != null)
			{
				Unsafe.SkipInit(out CComPtrNtv_003CITunerConfig_003E cComPtrNtv_003CITunerConfig_003E);
				*(int*)(&cComPtrNtv_003CITunerConfig_003E) = 0;
				try
				{
					IService* pService = m_pService;
					int num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, ITunerConfig**, int>)(int)(*(uint*)(*(int*)pService + 88)))((nint)pService, (ITunerConfig**)(&cComPtrNtv_003CITunerConfig_003E));
					if (num >= 0)
					{
						int num2 = *(int*)(int)(*(uint*)(&cComPtrNtv_003CITunerConfig_003E)) + 112;
						num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, ushort*, ushort**, int>)(int)(*(uint*)num2))((IntPtr)(*(int*)(&cComPtrNtv_003CITunerConfig_003E)), ptr2, &ptr);
						if (num >= 0)
						{
							result = Marshal.PtrToStringBSTR((IntPtr)ptr);
						}
					}
				}
				catch
				{
					//try-fault
					global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<CComPtrNtv_003CITunerConfig_003E*, void>)(&global::_003CModule_003E.CComPtrNtv_003CITunerConfig_003E_002E_007Bdtor_007D), &cComPtrNtv_003CITunerConfig_003E);
					throw;
				}
				global::_003CModule_003E.CComPtrNtv_003CITunerConfig_003E_002ERelease(&cComPtrNtv_003CITunerConfig_003E);
			}
			return result;
		}
	}

	public unsafe int GetSubscriptionTrialDuration()
	{
		int result = 0;
		if (m_pService != null)
		{
			Unsafe.SkipInit(out CComPtrNtv_003CITunerConfig_003E cComPtrNtv_003CITunerConfig_003E);
			*(int*)(&cComPtrNtv_003CITunerConfig_003E) = 0;
			try
			{
				IService* pService = m_pService;
				if (((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, ITunerConfig**, int>)(int)(*(uint*)(*(int*)pService + 88)))((nint)pService, (ITunerConfig**)(&cComPtrNtv_003CITunerConfig_003E)) >= 0)
				{
					((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, int*, int>)(int)(*(uint*)(*(int*)(int)(*(uint*)(&cComPtrNtv_003CITunerConfig_003E)) + 116)))((IntPtr)(*(int*)(&cComPtrNtv_003CITunerConfig_003E)), &result);
				}
			}
			catch
			{
				//try-fault
				global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<CComPtrNtv_003CITunerConfig_003E*, void>)(&global::_003CModule_003E.CComPtrNtv_003CITunerConfig_003E_002E_007Bdtor_007D), &cComPtrNtv_003CITunerConfig_003E);
				throw;
			}
			global::_003CModule_003E.CComPtrNtv_003CITunerConfig_003E_002ERelease(&cComPtrNtv_003CITunerConfig_003E);
		}
		return result;
	}

	public unsafe AppOfferCollection CreateEmptyAppCollection()
	{
		AppOfferCollection appOfferCollection = null;
		if (m_pService != null)
		{
			Unsafe.SkipInit(out CComPtrNtv_003CIAppCollection_003E cComPtrNtv_003CIAppCollection_003E);
			*(int*)(&cComPtrNtv_003CIAppCollection_003E) = 0;
			try
			{
				IService* pService = m_pService;
				if (((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, IAppCollection**, int>)(int)(*(uint*)(*(int*)pService + 36)))((nint)pService, (IAppCollection**)(&cComPtrNtv_003CIAppCollection_003E)) >= 0)
				{
					appOfferCollection = new AppOfferCollection();
					appOfferCollection.Init((IAppCollection*)(int)(*(uint*)(&cComPtrNtv_003CIAppCollection_003E)));
				}
			}
			catch
			{
				//try-fault
				global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<CComPtrNtv_003CIAppCollection_003E*, void>)(&global::_003CModule_003E.CComPtrNtv_003CIAppCollection_003E_002E_007Bdtor_007D), &cComPtrNtv_003CIAppCollection_003E);
				throw;
			}
			global::_003CModule_003E.CComPtrNtv_003CIAppCollection_003E_002ERelease(&cComPtrNtv_003CIAppCollection_003E);
		}
		return appOfferCollection;
	}

	public EContentType GetContentType(string contentTypeStr)
	{
		EContentType result = EContentType.Unknown;
		switch (contentTypeStr)
		{
		case "type:musictrack":
			result = EContentType.MusicTrack;
			break;
		case "type:musicvideo":
			result = EContentType.Video;
			break;
		case "type:podcast":
			result = EContentType.PodcastEpisode;
			break;
		case "type:app":
			result = EContentType.App;
			break;
		}
		return result;
	}

	public unsafe void ReportStreamingAction(EStreamingActionType eStreamingActionType, Guid guidMediaInstanceId, AsyncCompleteHandler eventHandler)
	{
		if (m_pService == null)
		{
			return;
		}
		AsyncCallbackWrapper* ptr = null;
		if (eventHandler != null)
		{
			AsyncCallbackWrapper* ptr2 = (AsyncCallbackWrapper*)global::_003CModule_003E.@new(12u);
			AsyncCallbackWrapper* ptr3;
			try
			{
				ptr3 = ((ptr2 == null) ? null : global::_003CModule_003E.Microsoft_002EZune_002EUtil_002EAsyncCallbackWrapper_002E_007Bctor_007D(ptr2, eventHandler));
			}
			catch
			{
				//try-fault
				global::_003CModule_003E.delete(ptr2);
				throw;
			}
			ptr = ptr3;
			if (ptr3 == null)
			{
				goto IL_0055;
			}
		}
		_GUID gUID = global::_003CModule_003E.GuidToGUID(guidMediaInstanceId);
		IService* pService = m_pService;
		((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, global::EStreamingActionType, _GUID, IAsyncCallback*, int>)(int)(*(uint*)(*(int*)pService + 492)))((nint)pService, (global::EStreamingActionType)eStreamingActionType, gUID, (IAsyncCallback*)ptr);
		goto IL_0055;
		IL_0055:
		if (ptr != null)
		{
			AsyncCallbackWrapper* intPtr = ptr;
			((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint>)(int)(*(uint*)(*(int*)intPtr + 8)))((nint)intPtr);
		}
	}

	protected virtual void Dispose([MarshalAs(UnmanagedType.U1)] bool P_0)
	{
		if (P_0)
		{
			_0021Service();
			return;
		}
		try
		{
			_0021Service();
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

	~Service()
	{
		Dispose(false);
	}
}
