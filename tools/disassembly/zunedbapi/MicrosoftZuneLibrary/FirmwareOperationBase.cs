using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Microsoft.Iris;
using Microsoft.Zune.Util;
using ZuneUI;

namespace MicrosoftZuneLibrary;

public abstract class FirmwareOperationBase : IDisposable
{
	internal readonly CComPtrMgd_003CIEndpointHost_003E m_spEndpointHost;

	internal object m_FirmwareLock;

	internal bool m_fFirmwareProcessSupported;

	internal bool m_Canceled;

	internal bool m_DeviceRebooting;

	internal readonly CComPtrMgd_003CMicrosoftZuneLibrary_003A_003AFirmwareUpdateMediator_003E m_spFirmwareMediator;

	internal FirmwareCompleteHandler m_OnCompleteHandler;

	internal DeferredInvokeHandler m_HandlerComplete;

	[return: MarshalAs(UnmanagedType.U1)]
	public bool EnterContinuousPowerMode()
	{
		return (global::_003CModule_003E.SetThreadExecutionState(2147483649u) != 0) ? true : false;
	}

	[return: MarshalAs(UnmanagedType.U1)]
	public bool LeaveContinuousPowerMode()
	{
		return (global::_003CModule_003E.SetThreadExecutionState(2147483648u) != 0) ? true : false;
	}

	[return: MarshalAs(UnmanagedType.U1)]
	public virtual bool IsValid()
	{
		return true;
	}

	public HRESULT CheckPowerRequirements(out bool fOnBatteryPower)
	{
		bool fOnBatteryPower2 = true;
		HRESULT hRESULT = PowerRequirements.CheckOnBatteryPower(out fOnBatteryPower2);
		HRESULT hRESULT2 = hRESULT;
		if (hRESULT2.hr >= 0)
		{
			fOnBatteryPower = fOnBatteryPower2;
		}
		return hRESULT;
	}

	internal FirmwareOperationBase()
	{
		CComPtrMgd_003CIEndpointHost_003E spEndpointHost = new CComPtrMgd_003CIEndpointHost_003E();
		try
		{
			m_spEndpointHost = spEndpointHost;
			CComPtrMgd_003CMicrosoftZuneLibrary_003A_003AFirmwareUpdateMediator_003E spFirmwareMediator = new CComPtrMgd_003CMicrosoftZuneLibrary_003A_003AFirmwareUpdateMediator_003E();
			try
			{
				m_spFirmwareMediator = spFirmwareMediator;
				base._002Ector();
				m_Canceled = false;
				m_FirmwareLock = new object();
				return;
			}
			catch
			{
				//try-fault
				((IDisposable)m_spFirmwareMediator).Dispose();
				throw;
			}
		}
		catch
		{
			//try-fault
			((IDisposable)m_spEndpointHost).Dispose();
			throw;
		}
	}

	private void _007EFirmwareOperationBase()
	{
		m_spEndpointHost.Release();
		m_spFirmwareMediator.Release();
	}

	[return: MarshalAs(UnmanagedType.U1)]
	internal unsafe bool IsDeviceRebooting()
	{
		if (global::_003CModule_003E.WPP_GLOBAL_Control != Unsafe.AsPointer(ref global::_003CModule_003E.WPP_GLOBAL_Control) && (((int*)global::_003CModule_003E.WPP_GLOBAL_Control)[15] & 4) != 0 && (uint)((byte*)global::_003CModule_003E.WPP_GLOBAL_Control)[57] >= 5u)
		{
			global::_003CModule_003E.WPP_SF_(((ulong*)global::_003CModule_003E.WPP_GLOBAL_Control)[6], 18, (_GUID*)Unsafe.AsPointer(ref global::_003CModule_003E._003FA0xff79125d_002EWPP_FirmwareUpdateAPI_cpp_Traceguids));
		}
		return m_DeviceRebooting;
	}

	internal unsafe int Reset([MarshalAs(UnmanagedType.U1)] bool deviceRebooting)
	{
		if (global::_003CModule_003E.WPP_GLOBAL_Control != Unsafe.AsPointer(ref global::_003CModule_003E.WPP_GLOBAL_Control) && (((int*)global::_003CModule_003E.WPP_GLOBAL_Control)[15] & 4) != 0 && (uint)((byte*)global::_003CModule_003E.WPP_GLOBAL_Control)[57] >= 5u)
		{
			global::_003CModule_003E.WPP_SF_(((ulong*)global::_003CModule_003E.WPP_GLOBAL_Control)[6], 19, (_GUID*)Unsafe.AsPointer(ref global::_003CModule_003E._003FA0xff79125d_002EWPP_FirmwareUpdateAPI_cpp_Traceguids));
		}
		int result = InternalReset();
		m_DeviceRebooting = deviceRebooting;
		return result;
	}

	internal unsafe void SendCompleteNotification(int hr, [MarshalAs(UnmanagedType.U1)] bool disconnectDeviceOnComplete)
	{
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		//IL_005c: Expected O, but got Unknown
		//IL_005c: Expected O, but got Unknown
		Unsafe.SkipInit(out CComPtrNtv_003CIFirmwareUpdateErrorInfo_003E cComPtrNtv_003CIFirmwareUpdateErrorInfo_003E);
		*(int*)(&cComPtrNtv_003CIFirmwareUpdateErrorInfo_003E) = 0;
		try
		{
			InternalErrorInfo* ptr = (InternalErrorInfo*)global::_003CModule_003E.@new(40u);
			InternalErrorInfo* lp;
			try
			{
				lp = ((ptr == null) ? null : global::_003CModule_003E.MicrosoftZuneLibrary_002EInternalErrorInfo_002E_007Bctor_007D(ptr, hr));
			}
			catch
			{
				//try-fault
				global::_003CModule_003E.delete(ptr);
				throw;
			}
			global::_003CModule_003E.CComPtrNtv_003CIFirmwareUpdateErrorInfo_003E_002E_003D(&cComPtrNtv_003CIFirmwareUpdateErrorInfo_003E, (IFirmwareUpdateErrorInfo*)lp);
			FirmwareProcessCompleteArgs firmwareProcessCompleteArgs = new FirmwareProcessCompleteArgs(new FirmwareUpdateErrorInfo((IFirmwareUpdateErrorInfo*)(int)(*(uint*)(&cComPtrNtv_003CIFirmwareUpdateErrorInfo_003E))), null, CompletionAction.Complete, disconnectDeviceOnComplete);
			Application.DeferredInvokeOnWorkerThread(new DeferredInvokeHandler(ProcessCompleteWorker), new DeferredInvokeHandler(ProcessCompleteWorkerComplete), (object)firmwareProcessCompleteArgs);
		}
		catch
		{
			//try-fault
			global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<CComPtrNtv_003CIFirmwareUpdateErrorInfo_003E*, void>)(&global::_003CModule_003E.CComPtrNtv_003CIFirmwareUpdateErrorInfo_003E_002E_007Bdtor_007D), &cComPtrNtv_003CIFirmwareUpdateErrorInfo_003E);
			throw;
		}
		global::_003CModule_003E.CComPtrNtv_003CIFirmwareUpdateErrorInfo_003E_002ERelease(&cComPtrNtv_003CIFirmwareUpdateErrorInfo_003E);
	}

	internal unsafe void OnFirmwareProcessCompleteWorker(object data)
	{
		FirmwareProcessCompleteArgs firmwareProcessCompleteArgs = (FirmwareProcessCompleteArgs)data;
		switch (firmwareProcessCompleteArgs.Action)
		{
		case CompletionAction.Complete:
			if (global::_003CModule_003E.WPP_GLOBAL_Control != Unsafe.AsPointer(ref global::_003CModule_003E.WPP_GLOBAL_Control) && (((int*)global::_003CModule_003E.WPP_GLOBAL_Control)[15] & 4) != 0 && (uint)((byte*)global::_003CModule_003E.WPP_GLOBAL_Control)[57] >= 6u)
			{
				HRESULT hrStatus4 = firmwareProcessCompleteArgs.ErrorInfo.HrStatus;
				global::_003CModule_003E.WPP_SF_d(((ulong*)global::_003CModule_003E.WPP_GLOBAL_Control)[6], 23, (_GUID*)Unsafe.AsPointer(ref global::_003CModule_003E._003FA0xff79125d_002EWPP_FirmwareUpdateAPI_cpp_Traceguids), hrStatus4.hr);
			}
			ProcessCompleteWorker(firmwareProcessCompleteArgs);
			break;
		case CompletionAction.Rollback:
		{
			if (global::_003CModule_003E.WPP_GLOBAL_Control != Unsafe.AsPointer(ref global::_003CModule_003E.WPP_GLOBAL_Control) && (((int*)global::_003CModule_003E.WPP_GLOBAL_Control)[15] & 4) != 0 && (uint)((byte*)global::_003CModule_003E.WPP_GLOBAL_Control)[57] >= 2u)
			{
				HRESULT hrStatus3 = firmwareProcessCompleteArgs.ErrorInfo.HrStatus;
				global::_003CModule_003E.WPP_SF_d(((ulong*)global::_003CModule_003E.WPP_GLOBAL_Control)[6], 22, (_GUID*)Unsafe.AsPointer(ref global::_003CModule_003E._003FA0xff79125d_002EWPP_FirmwareUpdateAPI_cpp_Traceguids), hrStatus3.hr);
			}
			DeferredInvokeHandler handlerComplete2 = m_HandlerComplete;
			if ((MulticastDelegate?)(object)handlerComplete2 != null)
			{
				handlerComplete2.Invoke((object)firmwareProcessCompleteArgs);
			}
			ContinueFirmwareProcess(null);
			break;
		}
		case CompletionAction.Reboot:
		{
			if (global::_003CModule_003E.WPP_GLOBAL_Control != Unsafe.AsPointer(ref global::_003CModule_003E.WPP_GLOBAL_Control) && (((int*)global::_003CModule_003E.WPP_GLOBAL_Control)[15] & 4) != 0 && (uint)((byte*)global::_003CModule_003E.WPP_GLOBAL_Control)[57] >= 6u)
			{
				HRESULT hrStatus2 = firmwareProcessCompleteArgs.ErrorInfo.HrStatus;
				global::_003CModule_003E.WPP_SF_d(((ulong*)global::_003CModule_003E.WPP_GLOBAL_Control)[6], 21, (_GUID*)Unsafe.AsPointer(ref global::_003CModule_003E._003FA0xff79125d_002EWPP_FirmwareUpdateAPI_cpp_Traceguids), hrStatus2.hr);
			}
			Reset(deviceRebooting: true);
			DeferredInvokeHandler handlerComplete = m_HandlerComplete;
			if ((MulticastDelegate?)(object)handlerComplete != null)
			{
				handlerComplete.Invoke((object)firmwareProcessCompleteArgs);
			}
			break;
		}
		case CompletionAction.Continue:
			if (global::_003CModule_003E.WPP_GLOBAL_Control != Unsafe.AsPointer(ref global::_003CModule_003E.WPP_GLOBAL_Control) && (((int*)global::_003CModule_003E.WPP_GLOBAL_Control)[15] & 4) != 0 && (uint)((byte*)global::_003CModule_003E.WPP_GLOBAL_Control)[57] >= 6u)
			{
				HRESULT hrStatus = firmwareProcessCompleteArgs.ErrorInfo.HrStatus;
				global::_003CModule_003E.WPP_SF_d(((ulong*)global::_003CModule_003E.WPP_GLOBAL_Control)[6], 20, (_GUID*)Unsafe.AsPointer(ref global::_003CModule_003E._003FA0xff79125d_002EWPP_FirmwareUpdateAPI_cpp_Traceguids), hrStatus.hr);
			}
			ContinueFirmwareProcess(null);
			break;
		}
	}

	internal unsafe void ProcessCompleteWorker(object data)
	{
		FirmwareProcessCompleteArgs firmwareProcessCompleteArgs = (FirmwareProcessCompleteArgs)data;
		if (global::_003CModule_003E.WPP_GLOBAL_Control != Unsafe.AsPointer(ref global::_003CModule_003E.WPP_GLOBAL_Control) && (((int*)global::_003CModule_003E.WPP_GLOBAL_Control)[15] & 4) != 0 && (uint)((byte*)global::_003CModule_003E.WPP_GLOBAL_Control)[57] >= 5u)
		{
			HRESULT hrStatus = firmwareProcessCompleteArgs.ErrorInfo.HrStatus;
			global::_003CModule_003E.WPP_SF_d(((ulong*)global::_003CModule_003E.WPP_GLOBAL_Control)[6], 24, (_GUID*)Unsafe.AsPointer(ref global::_003CModule_003E._003FA0xff79125d_002EWPP_FirmwareUpdateAPI_cpp_Traceguids), hrStatus.hr);
		}
		Reset(deviceRebooting: false);
		m_spFirmwareMediator.op_Assign(null);
		if (m_OnCompleteHandler != null)
		{
			HRESULT hrStatus2 = firmwareProcessCompleteArgs.ErrorInfo.HrStatus;
			m_OnCompleteHandler(hrStatus2.hr);
			m_OnCompleteHandler = null;
		}
		if ((MulticastDelegate?)(object)m_HandlerComplete != null)
		{
			m_HandlerComplete.Invoke((object)firmwareProcessCompleteArgs);
			m_HandlerComplete = null;
		}
		if (firmwareProcessCompleteArgs.DisconnectDeviceOnComplete)
		{
			IEndpointHost* p = m_spEndpointHost.p;
			DeviceList.Instance.DeviceDisconnected(p);
		}
		if (global::_003CModule_003E.WPP_GLOBAL_Control != Unsafe.AsPointer(ref global::_003CModule_003E.WPP_GLOBAL_Control) && (((int*)global::_003CModule_003E.WPP_GLOBAL_Control)[15] & 4) != 0 && (uint)((byte*)global::_003CModule_003E.WPP_GLOBAL_Control)[57] >= 5u)
		{
			global::_003CModule_003E.WPP_SF_(((ulong*)global::_003CModule_003E.WPP_GLOBAL_Control)[6], 25, (_GUID*)Unsafe.AsPointer(ref global::_003CModule_003E._003FA0xff79125d_002EWPP_FirmwareUpdateAPI_cpp_Traceguids));
		}
	}

	internal void ProcessCompleteWorkerComplete(object P_0)
	{
	}

	internal unsafe void DeferredCancel()
	{
		Unsafe.SkipInit(out CComPtrNtv_003CIFirmwareUpdateNotification_003E cComPtrNtv_003CIFirmwareUpdateNotification_003E);
		*(int*)(&cComPtrNtv_003CIFirmwareUpdateNotification_003E) = 0;
		try
		{
			if (global::_003CModule_003E.WPP_GLOBAL_Control != Unsafe.AsPointer(ref global::_003CModule_003E.WPP_GLOBAL_Control) && (((int*)global::_003CModule_003E.WPP_GLOBAL_Control)[15] & 4) != 0 && (uint)((byte*)global::_003CModule_003E.WPP_GLOBAL_Control)[57] >= 5u)
			{
				global::_003CModule_003E.WPP_SF_(((ulong*)global::_003CModule_003E.WPP_GLOBAL_Control)[6], 26, (_GUID*)Unsafe.AsPointer(ref global::_003CModule_003E._003FA0xff79125d_002EWPP_FirmwareUpdateAPI_cpp_Traceguids));
			}
			m_DeviceRebooting = false;
			int firmwareUpdateNotification = GetFirmwareUpdateNotification((IFirmwareUpdateNotification**)(&cComPtrNtv_003CIFirmwareUpdateNotification_003E));
			if (*(int*)(&cComPtrNtv_003CIFirmwareUpdateNotification_003E) != 0 && firmwareUpdateNotification >= 0)
			{
				Unsafe.SkipInit(out WBSTRString wBSTRString);
				global::_003CModule_003E.WBSTRString_002E_007Bctor_007D(&wBSTRString);
				try
				{
					IEndpointHost* p = m_spEndpointHost.p;
					firmwareUpdateNotification = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, EEndpointHostProperty, ushort**, int>)(int)(*(uint*)(*(int*)p + 60)))((nint)p, EEndpointHostProperty.eEndpointHostPropertyEndpointId, (ushort**)(&wBSTRString));
					if (firmwareUpdateNotification >= 0)
					{
						if (global::_003CModule_003E.WPP_GLOBAL_Control != Unsafe.AsPointer(ref global::_003CModule_003E.WPP_GLOBAL_Control) && (((int*)global::_003CModule_003E.WPP_GLOBAL_Control)[15] & 4) != 0 && (uint)((byte*)global::_003CModule_003E.WPP_GLOBAL_Control)[57] >= 5u)
						{
							global::_003CModule_003E.WPP_SF_S(((ulong*)global::_003CModule_003E.WPP_GLOBAL_Control)[6], 27, (_GUID*)Unsafe.AsPointer(ref global::_003CModule_003E._003FA0xff79125d_002EWPP_FirmwareUpdateAPI_cpp_Traceguids), (ushort*)(int)(*(uint*)(&wBSTRString)));
						}
						((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, ushort*, int>)(int)(*(uint*)(*(int*)(int)(*(uint*)(&cComPtrNtv_003CIFirmwareUpdateNotification_003E)) + 24)))((IntPtr)(*(int*)(&cComPtrNtv_003CIFirmwareUpdateNotification_003E)), (ushort*)(int)(*(uint*)(&wBSTRString)));
					}
				}
				catch
				{
					//try-fault
					global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<WBSTRString*, void>)(&global::_003CModule_003E.WBSTRString_002E_007Bdtor_007D), &wBSTRString);
					throw;
				}
				global::_003CModule_003E.WString_002E_007Bdtor_007D((WString*)(&wBSTRString));
			}
			SendCompleteNotification(-2147467260, disconnectDeviceOnComplete: true);
			if (global::_003CModule_003E.WPP_GLOBAL_Control != Unsafe.AsPointer(ref global::_003CModule_003E.WPP_GLOBAL_Control) && (((int*)global::_003CModule_003E.WPP_GLOBAL_Control)[15] & 4) != 0 && (uint)((byte*)global::_003CModule_003E.WPP_GLOBAL_Control)[57] >= 5u)
			{
				global::_003CModule_003E.WPP_SF_(((ulong*)global::_003CModule_003E.WPP_GLOBAL_Control)[6], 28, (_GUID*)Unsafe.AsPointer(ref global::_003CModule_003E._003FA0xff79125d_002EWPP_FirmwareUpdateAPI_cpp_Traceguids));
			}
		}
		catch
		{
			//try-fault
			global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<CComPtrNtv_003CIFirmwareUpdateNotification_003E*, void>)(&global::_003CModule_003E.CComPtrNtv_003CIFirmwareUpdateNotification_003E_002E_007Bdtor_007D), &cComPtrNtv_003CIFirmwareUpdateNotification_003E);
			throw;
		}
		global::_003CModule_003E.CComPtrNtv_003CIFirmwareUpdateNotification_003E_002ERelease(&cComPtrNtv_003CIFirmwareUpdateNotification_003E);
	}

	internal unsafe int GetFirmwareUpdateNotification(IFirmwareUpdateNotification** ppNotification)
	{
		Unsafe.SkipInit(out CComPtrNtv_003CIEndpointHostManager_003E cComPtrNtv_003CIEndpointHostManager_003E);
		*(int*)(&cComPtrNtv_003CIEndpointHostManager_003E) = 0;
		Unsafe.SkipInit(out CComPtrNtv_003CIEndpointManager_003E cComPtrNtv_003CIEndpointManager_003E);
		Unsafe.SkipInit(out CComPtrNtv_003CIFirmwareUpdateNotification_003E cComPtrNtv_003CIFirmwareUpdateNotification_003E);
		EEndpointClass eEndpointClass;
		try
		{
			*(int*)(&cComPtrNtv_003CIEndpointManager_003E) = 0;
			try
			{
				*(int*)(&cComPtrNtv_003CIFirmwareUpdateNotification_003E) = 0;
				try
				{
					eEndpointClass = EEndpointClass.eEndpointClassInvalid;
					if (global::_003CModule_003E.WPP_GLOBAL_Control != Unsafe.AsPointer(ref global::_003CModule_003E.WPP_GLOBAL_Control) && (((int*)global::_003CModule_003E.WPP_GLOBAL_Control)[15] & 4) != 0 && (uint)((byte*)global::_003CModule_003E.WPP_GLOBAL_Control)[57] >= 5u)
					{
						global::_003CModule_003E.WPP_SF_(((ulong*)global::_003CModule_003E.WPP_GLOBAL_Control)[6], 29, (_GUID*)Unsafe.AsPointer(ref global::_003CModule_003E._003FA0xff79125d_002EWPP_FirmwareUpdateAPI_cpp_Traceguids));
					}
					if (ppNotification == null)
					{
						global::_003CModule_003E._ZuneShipAssert(1001u, 1289u);
						goto IL_006c;
					}
				}
				catch
				{
					//try-fault
					global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<CComPtrNtv_003CIFirmwareUpdateNotification_003E*, void>)(&global::_003CModule_003E.CComPtrNtv_003CIFirmwareUpdateNotification_003E_002E_007Bdtor_007D), &cComPtrNtv_003CIFirmwareUpdateNotification_003E);
					throw;
				}
				goto end_IL_0008;
				IL_006c:
				global::_003CModule_003E.CComPtrNtv_003CIFirmwareUpdateNotification_003E_002ERelease(&cComPtrNtv_003CIFirmwareUpdateNotification_003E);
				goto IL_0083;
				end_IL_0008:;
			}
			catch
			{
				//try-fault
				global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<CComPtrNtv_003CIEndpointManager_003E*, void>)(&global::_003CModule_003E.CComPtrNtv_003CIEndpointManager_003E_002E_007Bdtor_007D), &cComPtrNtv_003CIEndpointManager_003E);
				throw;
			}
			goto end_IL_0004;
			IL_0083:
			global::_003CModule_003E.CComPtrNtv_003CIEndpointManager_003E_002ERelease(&cComPtrNtv_003CIEndpointManager_003E);
			goto IL_009a;
			end_IL_0004:;
		}
		catch
		{
			//try-fault
			global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<CComPtrNtv_003CIEndpointHostManager_003E*, void>)(&global::_003CModule_003E.CComPtrNtv_003CIEndpointHostManager_003E_002E_007Bdtor_007D), &cComPtrNtv_003CIEndpointHostManager_003E);
			throw;
		}
		CComPtrMgd_003CIEndpointHost_003E spEndpointHost;
		try
		{
			try
			{
				try
				{
					spEndpointHost = m_spEndpointHost;
					if (spEndpointHost.p == null)
					{
						global::_003CModule_003E._ZuneShipAssert(1002u, 1290u);
						goto IL_00d8;
					}
				}
				catch
				{
					//try-fault
					global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<CComPtrNtv_003CIFirmwareUpdateNotification_003E*, void>)(&global::_003CModule_003E.CComPtrNtv_003CIFirmwareUpdateNotification_003E_002E_007Bdtor_007D), &cComPtrNtv_003CIFirmwareUpdateNotification_003E);
					throw;
				}
				goto end_IL_00a8;
				IL_00d8:
				global::_003CModule_003E.CComPtrNtv_003CIFirmwareUpdateNotification_003E_002ERelease(&cComPtrNtv_003CIFirmwareUpdateNotification_003E);
				goto IL_00ef;
				end_IL_00a8:;
			}
			catch
			{
				//try-fault
				global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<CComPtrNtv_003CIEndpointManager_003E*, void>)(&global::_003CModule_003E.CComPtrNtv_003CIEndpointManager_003E_002E_007Bdtor_007D), &cComPtrNtv_003CIEndpointManager_003E);
				throw;
			}
			goto end_IL_00a8_2;
			IL_00ef:
			global::_003CModule_003E.CComPtrNtv_003CIEndpointManager_003E_002ERelease(&cComPtrNtv_003CIEndpointManager_003E);
			goto IL_0106;
			end_IL_00a8_2:;
		}
		catch
		{
			//try-fault
			global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<CComPtrNtv_003CIEndpointHostManager_003E*, void>)(&global::_003CModule_003E.CComPtrNtv_003CIEndpointHostManager_003E_002E_007Bdtor_007D), &cComPtrNtv_003CIEndpointHostManager_003E);
			throw;
		}
		int num;
		try
		{
			try
			{
				try
				{
					num = global::_003CModule_003E.GetEnumProperty_003Cstruct_0020IEndpointHost_002Cenum_0020EEndpointHostProperty_002Cenum_0020EEndpointClass_003E(spEndpointHost.p, EEndpointHostProperty.eEndpointHostPropertyClassId, &eEndpointClass);
					if (num >= 0)
					{
						num = global::_003CModule_003E.GetSingleton((_GUID)global::_003CModule_003E._GUID_0a3d3343_00d9_4c61_9a86_2d778793e05f, (void**)(&cComPtrNtv_003CIEndpointHostManager_003E));
						if (num >= 0)
						{
							num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, IEndpointManager**, int>)(int)(*(uint*)(*(int*)(int)(*(uint*)(&cComPtrNtv_003CIEndpointHostManager_003E)) + 44)))((IntPtr)(*(int*)(&cComPtrNtv_003CIEndpointHostManager_003E)), (IEndpointManager**)(&cComPtrNtv_003CIEndpointManager_003E));
							if (num >= 0)
							{
								num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, EEndpointClass, IFirmwareUpdateNotification**, int>)(int)(*(uint*)(*(int*)(int)(*(uint*)(&cComPtrNtv_003CIEndpointManager_003E)) + 44)))((IntPtr)(*(int*)(&cComPtrNtv_003CIEndpointManager_003E)), eEndpointClass, (IFirmwareUpdateNotification**)(&cComPtrNtv_003CIFirmwareUpdateNotification_003E));
								if (num >= 0)
								{
									IFirmwareUpdateNotification* ptr = (IFirmwareUpdateNotification*)(int)(*(uint*)(&cComPtrNtv_003CIFirmwareUpdateNotification_003E));
									*(int*)(&cComPtrNtv_003CIFirmwareUpdateNotification_003E) = 0;
									*(int*)ppNotification = (int)ptr;
								}
							}
						}
					}
					if (global::_003CModule_003E.WPP_GLOBAL_Control != Unsafe.AsPointer(ref global::_003CModule_003E.WPP_GLOBAL_Control) && (((int*)global::_003CModule_003E.WPP_GLOBAL_Control)[15] & 4) != 0 && (uint)((byte*)global::_003CModule_003E.WPP_GLOBAL_Control)[57] >= 5u)
					{
						global::_003CModule_003E.WPP_SF_d(((ulong*)global::_003CModule_003E.WPP_GLOBAL_Control)[6], 30, (_GUID*)Unsafe.AsPointer(ref global::_003CModule_003E._003FA0xff79125d_002EWPP_FirmwareUpdateAPI_cpp_Traceguids), num);
					}
				}
				catch
				{
					//try-fault
					global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<CComPtrNtv_003CIFirmwareUpdateNotification_003E*, void>)(&global::_003CModule_003E.CComPtrNtv_003CIFirmwareUpdateNotification_003E_002E_007Bdtor_007D), &cComPtrNtv_003CIFirmwareUpdateNotification_003E);
					throw;
				}
				global::_003CModule_003E.CComPtrNtv_003CIFirmwareUpdateNotification_003E_002ERelease(&cComPtrNtv_003CIFirmwareUpdateNotification_003E);
			}
			catch
			{
				//try-fault
				global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<CComPtrNtv_003CIEndpointManager_003E*, void>)(&global::_003CModule_003E.CComPtrNtv_003CIEndpointManager_003E_002E_007Bdtor_007D), &cComPtrNtv_003CIEndpointManager_003E);
				throw;
			}
			global::_003CModule_003E.CComPtrNtv_003CIEndpointManager_003E_002ERelease(&cComPtrNtv_003CIEndpointManager_003E);
		}
		catch
		{
			//try-fault
			global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<CComPtrNtv_003CIEndpointHostManager_003E*, void>)(&global::_003CModule_003E.CComPtrNtv_003CIEndpointHostManager_003E_002E_007Bdtor_007D), &cComPtrNtv_003CIEndpointHostManager_003E);
			throw;
		}
		global::_003CModule_003E.CComPtrNtv_003CIEndpointHostManager_003E_002ERelease(&cComPtrNtv_003CIEndpointHostManager_003E);
		return num;
		IL_009a:
		global::_003CModule_003E.CComPtrNtv_003CIEndpointHostManager_003E_002ERelease(&cComPtrNtv_003CIEndpointHostManager_003E);
		return -2147467261;
		IL_0106:
		global::_003CModule_003E.CComPtrNtv_003CIEndpointHostManager_003E_002ERelease(&cComPtrNtv_003CIEndpointHostManager_003E);
		return -2147418113;
	}

	internal abstract int ContinueFirmwareProcess(FirmwareCompleteHandler onCompleteHandler);

	internal abstract void ReleaseNativeObject();

	internal abstract int InternalReset();

	protected virtual void Dispose([MarshalAs(UnmanagedType.U1)] bool P_0)
	{
		if (P_0)
		{
			try
			{
				_007EFirmwareOperationBase();
				return;
			}
			finally
			{
				try
				{
					((IDisposable)m_spFirmwareMediator).Dispose();
				}
				finally
				{
					try
					{
						((IDisposable)m_spEndpointHost).Dispose();
					}
					finally
					{
					}
				}
			}
		}
		base.Finalize();
	}

	public virtual sealed void Dispose()
	{
		Dispose(true);
		GC.SuppressFinalize(this);
	}
}
