using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using Microsoft.Iris;
using ZuneUI;

namespace MicrosoftZuneLibrary;

public class FirmwareRestorer : FirmwareOperationBase
{
	private readonly CComPtrMgd_003CIFirmwareRestorer_003E m_spFirmwareRestorer;

	private FirmwareUpdater m_Updater;

	[return: MarshalAs(UnmanagedType.U1)]
	public unsafe bool IsRestoreInProgress()
	{
		if (global::_003CModule_003E.WPP_GLOBAL_Control != Unsafe.AsPointer(ref global::_003CModule_003E.WPP_GLOBAL_Control) && (((int*)global::_003CModule_003E.WPP_GLOBAL_Control)[15] & 4) != 0 && (uint)((byte*)global::_003CModule_003E.WPP_GLOBAL_Control)[57] >= 5u)
		{
			global::_003CModule_003E.WPP_SF_(((ulong*)global::_003CModule_003E.WPP_GLOBAL_Control)[6], 76, (_GUID*)Unsafe.AsPointer(ref global::_003CModule_003E._003FA0xff79125d_002EWPP_FirmwareUpdateAPI_cpp_Traceguids));
		}
		int num = ((m_spFirmwareMediator.p != null) ? 1 : 0);
		if (!m_Canceled)
		{
			if (Application.IsApplicationThread)
			{
				global::_003CModule_003E._ZuneShipAssert(1004u, 2313u);
			}
			Monitor.Enter(m_FirmwareLock);
			try
			{
				if (m_fFirmwareProcessSupported && EnsureNativeObject() >= 0)
				{
					IFirmwareRestorer* p = m_spFirmwareRestorer.p;
					((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, int*, int>)(int)(*(uint*)(*(int*)p + 16)))((nint)p, &num);
				}
			}
			finally
			{
				Monitor.Exit(m_FirmwareLock);
			}
		}
		if (global::_003CModule_003E.WPP_GLOBAL_Control != Unsafe.AsPointer(ref global::_003CModule_003E.WPP_GLOBAL_Control) && (((int*)global::_003CModule_003E.WPP_GLOBAL_Control)[15] & 4) != 0 && (uint)((byte*)global::_003CModule_003E.WPP_GLOBAL_Control)[57] >= 5u)
		{
			global::_003CModule_003E.WPP_SF_ql(((ulong*)global::_003CModule_003E.WPP_GLOBAL_Control)[6], 77, (_GUID*)Unsafe.AsPointer(ref global::_003CModule_003E._003FA0xff79125d_002EWPP_FirmwareUpdateAPI_cpp_Traceguids), m_spFirmwareRestorer.p, num);
		}
		return (byte)((num != 0) ? 1u : 0u) != 0;
	}

	public unsafe HRESULT StartGetRestorePointCollection(DeferredInvokeHandler getCollectionComplete)
	{
		if (global::_003CModule_003E.WPP_GLOBAL_Control != Unsafe.AsPointer(ref global::_003CModule_003E.WPP_GLOBAL_Control) && (((int*)global::_003CModule_003E.WPP_GLOBAL_Control)[15] & 4) != 0 && (uint)((byte*)global::_003CModule_003E.WPP_GLOBAL_Control)[57] >= 5u)
		{
			global::_003CModule_003E.WPP_SF_(((ulong*)global::_003CModule_003E.WPP_GLOBAL_Control)[6], 88, (_GUID*)Unsafe.AsPointer(ref global::_003CModule_003E._003FA0xff79125d_002EWPP_FirmwareUpdateAPI_cpp_Traceguids));
		}
		int num = 0;
		if (m_fFirmwareProcessSupported)
		{
			if ((MulticastDelegate?)(object)getCollectionComplete == null)
			{
				num = -2147418113;
			}
			else if (!ThreadPool.QueueUserWorkItem(StartGetRestorePointCollectionWorker, getCollectionComplete))
			{
				global::_003CModule_003E._ZuneShipAssert(1004u, 2660u);
				num = -2147467259;
			}
		}
		else
		{
			global::_003CModule_003E._ZuneShipAssert(1004u, 2667u);
			num = -2147024846;
		}
		if (global::_003CModule_003E.WPP_GLOBAL_Control != Unsafe.AsPointer(ref global::_003CModule_003E.WPP_GLOBAL_Control) && (((int*)global::_003CModule_003E.WPP_GLOBAL_Control)[15] & 4) != 0 && (uint)((byte*)global::_003CModule_003E.WPP_GLOBAL_Control)[57] >= 5u)
		{
			global::_003CModule_003E.WPP_SF_d(((ulong*)global::_003CModule_003E.WPP_GLOBAL_Control)[6], 89, (_GUID*)Unsafe.AsPointer(ref global::_003CModule_003E._003FA0xff79125d_002EWPP_FirmwareUpdateAPI_cpp_Traceguids), num);
		}
		return num;
	}

	public unsafe HRESULT StartFirmwareRestore(FirmwareRestorePoint restorePoint, DeferredInvokeHandler restoreBegin, DeferredInvokeHandler restoreProgress, DeferredInvokeHandler restoreComplete)
	{
		if (global::_003CModule_003E.WPP_GLOBAL_Control != Unsafe.AsPointer(ref global::_003CModule_003E.WPP_GLOBAL_Control) && (((int*)global::_003CModule_003E.WPP_GLOBAL_Control)[15] & 4) != 0 && (uint)((byte*)global::_003CModule_003E.WPP_GLOBAL_Control)[57] >= 5u)
		{
			global::_003CModule_003E.WPP_SF_(((ulong*)global::_003CModule_003E.WPP_GLOBAL_Control)[6], 78, (_GUID*)Unsafe.AsPointer(ref global::_003CModule_003E._003FA0xff79125d_002EWPP_FirmwareUpdateAPI_cpp_Traceguids));
		}
		m_Canceled = false;
		int num = 0;
		if (m_fFirmwareProcessSupported)
		{
			if (restorePoint == null)
			{
				num = -2147418113;
			}
			else
			{
				StartFirmwareRestoreArgs startFirmwareRestoreArgs = new StartFirmwareRestoreArgs(restorePoint, restoreBegin, restoreProgress, restoreComplete);
				if (Application.IsApplicationThread)
				{
					if (!ThreadPool.QueueUserWorkItem(StartFirmwareRestoreWorker, startFirmwareRestoreArgs))
					{
						global::_003CModule_003E._ZuneShipAssert(1004u, 2375u);
						num = -2147467259;
					}
				}
				else
				{
					StartFirmwareRestoreWorker(startFirmwareRestoreArgs);
					num = startFirmwareRestoreArgs.HrStatus;
				}
			}
		}
		else
		{
			global::_003CModule_003E._ZuneShipAssert(1004u, 2388u);
			num = -2147024846;
		}
		return num;
	}

	public unsafe HRESULT Cancel()
	{
		if (global::_003CModule_003E.WPP_GLOBAL_Control != Unsafe.AsPointer(ref global::_003CModule_003E.WPP_GLOBAL_Control) && (((int*)global::_003CModule_003E.WPP_GLOBAL_Control)[15] & 4) != 0 && (uint)((byte*)global::_003CModule_003E.WPP_GLOBAL_Control)[57] >= 5u)
		{
			global::_003CModule_003E.WPP_SF_(((ulong*)global::_003CModule_003E.WPP_GLOBAL_Control)[6], 92, (_GUID*)Unsafe.AsPointer(ref global::_003CModule_003E._003FA0xff79125d_002EWPP_FirmwareUpdateAPI_cpp_Traceguids));
		}
		int num = 0;
		if (m_fFirmwareProcessSupported)
		{
			m_Canceled = true;
			if (Application.IsApplicationThread)
			{
				if (!ThreadPool.QueueUserWorkItem(CancelWorker, null))
				{
					global::_003CModule_003E._ZuneShipAssert(1004u, 2735u);
				}
			}
			else
			{
				CancelWorker(null);
			}
		}
		else
		{
			global::_003CModule_003E._ZuneShipAssert(1004u, 2745u);
			num = -2147024846;
		}
		if (global::_003CModule_003E.WPP_GLOBAL_Control != Unsafe.AsPointer(ref global::_003CModule_003E.WPP_GLOBAL_Control) && (((int*)global::_003CModule_003E.WPP_GLOBAL_Control)[15] & 4) != 0 && (uint)((byte*)global::_003CModule_003E.WPP_GLOBAL_Control)[57] >= 5u)
		{
			global::_003CModule_003E.WPP_SF_(((ulong*)global::_003CModule_003E.WPP_GLOBAL_Control)[6], 93, (_GUID*)Unsafe.AsPointer(ref global::_003CModule_003E._003FA0xff79125d_002EWPP_FirmwareUpdateAPI_cpp_Traceguids));
		}
		return num;
	}

	internal unsafe FirmwareRestorer(IEndpointHost* pEndpointHost, [MarshalAs(UnmanagedType.U1)] bool fFirmwareProcessSupported, FirmwareUpdater updater)
	{
		CComPtrMgd_003CIFirmwareRestorer_003E spFirmwareRestorer = new CComPtrMgd_003CIFirmwareRestorer_003E();
		try
		{
			m_spFirmwareRestorer = spFirmwareRestorer;
			base._002Ector();
			try
			{
				if (global::_003CModule_003E.WPP_GLOBAL_Control != Unsafe.AsPointer(ref global::_003CModule_003E.WPP_GLOBAL_Control) && (((int*)global::_003CModule_003E.WPP_GLOBAL_Control)[15] & 4) != 0 && (uint)((byte*)global::_003CModule_003E.WPP_GLOBAL_Control)[57] >= 5u)
				{
					global::_003CModule_003E.WPP_SF_q(((ulong*)global::_003CModule_003E.WPP_GLOBAL_Control)[6], 72, (_GUID*)Unsafe.AsPointer(ref global::_003CModule_003E._003FA0xff79125d_002EWPP_FirmwareUpdateAPI_cpp_Traceguids), pEndpointHost);
				}
				m_spEndpointHost.op_Assign(pEndpointHost);
				m_fFirmwareProcessSupported = fFirmwareProcessSupported;
				m_Updater = updater;
				if (global::_003CModule_003E.WPP_GLOBAL_Control != Unsafe.AsPointer(ref global::_003CModule_003E.WPP_GLOBAL_Control) && (((int*)global::_003CModule_003E.WPP_GLOBAL_Control)[15] & 4) != 0 && (uint)((byte*)global::_003CModule_003E.WPP_GLOBAL_Control)[57] >= 5u)
				{
					global::_003CModule_003E.WPP_SF_(((ulong*)global::_003CModule_003E.WPP_GLOBAL_Control)[6], 73, (_GUID*)Unsafe.AsPointer(ref global::_003CModule_003E._003FA0xff79125d_002EWPP_FirmwareUpdateAPI_cpp_Traceguids));
				}
				return;
			}
			catch
			{
				//try-fault
				base.Dispose(true);
				throw;
			}
		}
		catch
		{
			//try-fault
			((IDisposable)m_spFirmwareRestorer).Dispose();
			throw;
		}
	}

	private unsafe void _007EFirmwareRestorer()
	{
		if (global::_003CModule_003E.WPP_GLOBAL_Control != Unsafe.AsPointer(ref global::_003CModule_003E.WPP_GLOBAL_Control) && (((int*)global::_003CModule_003E.WPP_GLOBAL_Control)[15] & 4) != 0 && (uint)((byte*)global::_003CModule_003E.WPP_GLOBAL_Control)[57] >= 5u)
		{
			global::_003CModule_003E.WPP_SF_(((ulong*)global::_003CModule_003E.WPP_GLOBAL_Control)[6], 74, (_GUID*)Unsafe.AsPointer(ref global::_003CModule_003E._003FA0xff79125d_002EWPP_FirmwareUpdateAPI_cpp_Traceguids));
		}
		if (IsRestoreInProgress())
		{
			global::_003CModule_003E._ZuneShipAssert(1004u, 2290u);
		}
		m_spFirmwareRestorer.Release();
		m_Updater = null;
		if (global::_003CModule_003E.WPP_GLOBAL_Control != Unsafe.AsPointer(ref global::_003CModule_003E.WPP_GLOBAL_Control) && (((int*)global::_003CModule_003E.WPP_GLOBAL_Control)[15] & 4) != 0 && (uint)((byte*)global::_003CModule_003E.WPP_GLOBAL_Control)[57] >= 5u)
		{
			global::_003CModule_003E.WPP_SF_(((ulong*)global::_003CModule_003E.WPP_GLOBAL_Control)[6], 75, (_GUID*)Unsafe.AsPointer(ref global::_003CModule_003E._003FA0xff79125d_002EWPP_FirmwareUpdateAPI_cpp_Traceguids));
		}
	}

	internal unsafe override int InternalReset()
	{
		if (global::_003CModule_003E.WPP_GLOBAL_Control != Unsafe.AsPointer(ref global::_003CModule_003E.WPP_GLOBAL_Control) && (((int*)global::_003CModule_003E.WPP_GLOBAL_Control)[15] & 4) != 0 && (uint)((byte*)global::_003CModule_003E.WPP_GLOBAL_Control)[57] >= 5u)
		{
			global::_003CModule_003E.WPP_SF_(((ulong*)global::_003CModule_003E.WPP_GLOBAL_Control)[6], 85, (_GUID*)Unsafe.AsPointer(ref global::_003CModule_003E._003FA0xff79125d_002EWPP_FirmwareUpdateAPI_cpp_Traceguids));
		}
		m_Updater?.InternalReset();
		return 0;
	}

	internal override int ContinueFirmwareProcess(FirmwareCompleteHandler onCompleteHandler)
	{
		int result = 0;
		if (!ThreadPool.QueueUserWorkItem(state: new ContinueFirmwareUpdateArgs(onCompleteHandler), callBack: ContinueFirmwareRestoreWorker))
		{
			global::_003CModule_003E._ZuneShipAssert(1004u, 2470u);
			result = -2147467259;
			SendCompleteNotification(-2147467259, disconnectDeviceOnComplete: false);
		}
		return result;
	}

	internal unsafe override void ReleaseNativeObject()
	{
		if (global::_003CModule_003E.WPP_GLOBAL_Control != Unsafe.AsPointer(ref global::_003CModule_003E.WPP_GLOBAL_Control) && (((int*)global::_003CModule_003E.WPP_GLOBAL_Control)[15] & 4) != 0 && (uint)((byte*)global::_003CModule_003E.WPP_GLOBAL_Control)[57] >= 7u)
		{
			global::_003CModule_003E.WPP_SF_(((ulong*)global::_003CModule_003E.WPP_GLOBAL_Control)[6], 87, (_GUID*)Unsafe.AsPointer(ref global::_003CModule_003E._003FA0xff79125d_002EWPP_FirmwareUpdateAPI_cpp_Traceguids));
		}
		Monitor.Enter(m_FirmwareLock);
		try
		{
			m_spFirmwareRestorer.op_Assign(null);
		}
		finally
		{
			Monitor.Exit(m_FirmwareLock);
		}
	}

	private unsafe int EnsureNativeObject()
	{
		if (global::_003CModule_003E.WPP_GLOBAL_Control != Unsafe.AsPointer(ref global::_003CModule_003E.WPP_GLOBAL_Control) && (((int*)global::_003CModule_003E.WPP_GLOBAL_Control)[15] & 4) != 0 && (uint)((byte*)global::_003CModule_003E.WPP_GLOBAL_Control)[57] >= 7u)
		{
			global::_003CModule_003E.WPP_SF_(((ulong*)global::_003CModule_003E.WPP_GLOBAL_Control)[6], 86, (_GUID*)Unsafe.AsPointer(ref global::_003CModule_003E._003FA0xff79125d_002EWPP_FirmwareUpdateAPI_cpp_Traceguids));
		}
		int num = 0;
		if (Application.IsApplicationThread)
		{
			global::_003CModule_003E._ZuneShipAssert(1004u, 2582u);
		}
		Monitor.Enter(m_FirmwareLock);
		try
		{
			if (!m_fFirmwareProcessSupported)
			{
				int num2 = -2147024846;
				num = -2147024846;
			}
			else if (m_spFirmwareRestorer.p == null)
			{
				Unsafe.SkipInit(out CComPtrNtv_003CIFirmwareUpdater_003E cComPtrNtv_003CIFirmwareUpdater_003E);
				*(int*)(&cComPtrNtv_003CIFirmwareUpdater_003E) = 0;
				try
				{
					num = m_Updater.GetNativeObject((IFirmwareUpdater**)(&cComPtrNtv_003CIFirmwareUpdater_003E));
					if (num >= 0 && *(int*)(&cComPtrNtv_003CIFirmwareUpdater_003E) != 0)
					{
						Unsafe.SkipInit(out CComPtrNtv_003CIFirmwareRestorer_003E cComPtrNtv_003CIFirmwareRestorer_003E);
						*(int*)(&cComPtrNtv_003CIFirmwareRestorer_003E) = 0;
						try
						{
							IFirmwareUpdater* ptr = (IFirmwareUpdater*)(int)(*(uint*)(&cComPtrNtv_003CIFirmwareUpdater_003E));
							num = global::_003CModule_003E.IUnknown_002EQueryInterface_003Cstruct_0020IFirmwareRestorer_003E((IUnknown*)(int)(*(uint*)(&cComPtrNtv_003CIFirmwareUpdater_003E)), (IFirmwareRestorer**)(&cComPtrNtv_003CIFirmwareRestorer_003E));
							if (num >= 0)
							{
								IFirmwareRestorer* ptr2 = (IFirmwareRestorer*)(int)(*(uint*)(&cComPtrNtv_003CIFirmwareRestorer_003E));
								m_spFirmwareRestorer.op_Assign((IFirmwareRestorer*)(int)(*(uint*)(&cComPtrNtv_003CIFirmwareRestorer_003E)));
							}
						}
						catch
						{
							//try-fault
							global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<CComPtrNtv_003CIFirmwareRestorer_003E*, void>)(&global::_003CModule_003E.CComPtrNtv_003CIFirmwareRestorer_003E_002E_007Bdtor_007D), &cComPtrNtv_003CIFirmwareRestorer_003E);
							throw;
						}
						global::_003CModule_003E.CComPtrNtv_003CIFirmwareRestorer_003E_002ERelease(&cComPtrNtv_003CIFirmwareRestorer_003E);
					}
				}
				catch
				{
					//try-fault
					global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<CComPtrNtv_003CIFirmwareUpdater_003E*, void>)(&global::_003CModule_003E.CComPtrNtv_003CIFirmwareUpdater_003E_002E_007Bdtor_007D), &cComPtrNtv_003CIFirmwareUpdater_003E);
					throw;
				}
				global::_003CModule_003E.CComPtrNtv_003CIFirmwareUpdater_003E_002ERelease(&cComPtrNtv_003CIFirmwareUpdater_003E);
			}
		}
		finally
		{
			Monitor.Exit(m_FirmwareLock);
		}
		return num;
	}

	private unsafe void ContinueFirmwareRestoreWorker(object data)
	{
		if (global::_003CModule_003E.WPP_GLOBAL_Control != Unsafe.AsPointer(ref global::_003CModule_003E.WPP_GLOBAL_Control) && (((int*)global::_003CModule_003E.WPP_GLOBAL_Control)[15] & 4) != 0 && (uint)((byte*)global::_003CModule_003E.WPP_GLOBAL_Control)[57] >= 5u)
		{
			global::_003CModule_003E.WPP_SF_(((ulong*)global::_003CModule_003E.WPP_GLOBAL_Control)[6], 80, (_GUID*)Unsafe.AsPointer(ref global::_003CModule_003E._003FA0xff79125d_002EWPP_FirmwareUpdateAPI_cpp_Traceguids));
		}
		int num = 0;
		if (m_Canceled)
		{
			num = -2147467260;
			if (global::_003CModule_003E.WPP_GLOBAL_Control != Unsafe.AsPointer(ref global::_003CModule_003E.WPP_GLOBAL_Control) && (((int*)global::_003CModule_003E.WPP_GLOBAL_Control)[15] & 4) != 0 && (uint)((byte*)global::_003CModule_003E.WPP_GLOBAL_Control)[57] >= 5u)
			{
				global::_003CModule_003E.WPP_SF_(((ulong*)global::_003CModule_003E.WPP_GLOBAL_Control)[6], 81, (_GUID*)Unsafe.AsPointer(ref global::_003CModule_003E._003FA0xff79125d_002EWPP_FirmwareUpdateAPI_cpp_Traceguids));
			}
		}
		else if (m_spFirmwareMediator.p == null)
		{
			if (global::_003CModule_003E.WPP_GLOBAL_Control != Unsafe.AsPointer(ref global::_003CModule_003E.WPP_GLOBAL_Control) && (((int*)global::_003CModule_003E.WPP_GLOBAL_Control)[15] & 4) != 0 && (uint)((byte*)global::_003CModule_003E.WPP_GLOBAL_Control)[57] >= 2u)
			{
				global::_003CModule_003E.WPP_SF_(((ulong*)global::_003CModule_003E.WPP_GLOBAL_Control)[6], 82, (_GUID*)Unsafe.AsPointer(ref global::_003CModule_003E._003FA0xff79125d_002EWPP_FirmwareUpdateAPI_cpp_Traceguids));
			}
			num = -2147418113;
		}
		if (Application.IsApplicationThread)
		{
			global::_003CModule_003E._ZuneShipAssert(1004u, 2514u);
		}
		Monitor.Enter(m_FirmwareLock);
		try
		{
			if (num >= 0)
			{
				num = EnsureNativeObject();
				if (num >= 0)
				{
					ContinueFirmwareUpdateArgs continueFirmwareUpdateArgs = (ContinueFirmwareUpdateArgs)data;
					if (continueFirmwareUpdateArgs.OnCompleteHandler != null)
					{
						m_OnCompleteHandler = continueFirmwareUpdateArgs.OnCompleteHandler;
					}
					IFirmwareRestorer* p = m_spFirmwareRestorer.p;
					FirmwareUpdateMediator* p2 = m_spFirmwareMediator.p;
					num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, IFirmwareUpdateCallback*, int>)(int)(*(uint*)(*(int*)p + 24)))((nint)p, (IFirmwareUpdateCallback*)p2);
				}
			}
		}
		finally
		{
			Monitor.Exit(m_FirmwareLock);
		}
		if (num < 0)
		{
			if (global::_003CModule_003E.WPP_GLOBAL_Control != Unsafe.AsPointer(ref global::_003CModule_003E.WPP_GLOBAL_Control) && (((int*)global::_003CModule_003E.WPP_GLOBAL_Control)[15] & 4) != 0 && (uint)((byte*)global::_003CModule_003E.WPP_GLOBAL_Control)[57] >= 2u)
			{
				global::_003CModule_003E.WPP_SF_d(((ulong*)global::_003CModule_003E.WPP_GLOBAL_Control)[6], 83, (_GUID*)Unsafe.AsPointer(ref global::_003CModule_003E._003FA0xff79125d_002EWPP_FirmwareUpdateAPI_cpp_Traceguids), num);
			}
			SendCompleteNotification(num, disconnectDeviceOnComplete: false);
		}
		if (global::_003CModule_003E.WPP_GLOBAL_Control != Unsafe.AsPointer(ref global::_003CModule_003E.WPP_GLOBAL_Control) && (((int*)global::_003CModule_003E.WPP_GLOBAL_Control)[15] & 4) != 0 && (uint)((byte*)global::_003CModule_003E.WPP_GLOBAL_Control)[57] >= 5u)
		{
			global::_003CModule_003E.WPP_SF_d(((ulong*)global::_003CModule_003E.WPP_GLOBAL_Control)[6], 84, (_GUID*)Unsafe.AsPointer(ref global::_003CModule_003E._003FA0xff79125d_002EWPP_FirmwareUpdateAPI_cpp_Traceguids), num);
		}
	}

	private unsafe void StartFirmwareRestoreWorker(object data)
	{
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_004b: Expected O, but got Unknown
		StartFirmwareRestoreArgs startFirmwareRestoreArgs = (StartFirmwareRestoreArgs)data;
		m_spFirmwareMediator.op_Assign(null);
		m_HandlerComplete = startFirmwareRestoreArgs.FirmwareUpdateComplete;
		int num = 0;
		FirmwareUpdateMediator* ptr = (FirmwareUpdateMediator*)global::_003CModule_003E.@new(32u);
		FirmwareUpdateMediator* ptr2;
		try
		{
			ptr2 = ((ptr == null) ? null : global::_003CModule_003E.MicrosoftZuneLibrary_002EFirmwareUpdateMediator_002E_007Bctor_007D(ptr, startFirmwareRestoreArgs.FirmwareUpdateBegin, startFirmwareRestoreArgs.FirmwareUpdateProgress, new DeferredInvokeHandler(base.OnFirmwareProcessCompleteWorker)));
		}
		catch
		{
			//try-fault
			global::_003CModule_003E.delete(ptr);
			throw;
		}
		if (ptr2 != null)
		{
			m_spFirmwareMediator.op_Assign(ptr2);
		}
		else
		{
			num = -2147024882;
		}
		if (Application.IsApplicationThread)
		{
			global::_003CModule_003E._ZuneShipAssert(1004u, 2424u);
		}
		Monitor.Enter(m_FirmwareLock);
		try
		{
			if (num >= 0)
			{
				num = EnsureNativeObject();
				if (num >= 0)
				{
					IFirmwareRestorer* p = m_spFirmwareRestorer.p;
					FirmwareUpdateMediator* p2 = m_spFirmwareMediator.p;
					num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, IFirmwareRestorePoint*, IFirmwareUpdateCallback*, int>)(int)(*(uint*)(*(int*)p + 20)))((nint)p, startFirmwareRestoreArgs.RestorePoint.NativeRestorePointPtr, (IFirmwareUpdateCallback*)p2);
				}
			}
		}
		finally
		{
			Monitor.Exit(m_FirmwareLock);
		}
		startFirmwareRestoreArgs.HrStatus = num;
		if (num < 0)
		{
			Reset(deviceRebooting: false);
			SendCompleteNotification(num, disconnectDeviceOnComplete: false);
		}
		if (global::_003CModule_003E.WPP_GLOBAL_Control != Unsafe.AsPointer(ref global::_003CModule_003E.WPP_GLOBAL_Control) && (((int*)global::_003CModule_003E.WPP_GLOBAL_Control)[15] & 4) != 0 && (uint)((byte*)global::_003CModule_003E.WPP_GLOBAL_Control)[57] >= 5u)
		{
			global::_003CModule_003E.WPP_SF_d(((ulong*)global::_003CModule_003E.WPP_GLOBAL_Control)[6], 79, (_GUID*)Unsafe.AsPointer(ref global::_003CModule_003E._003FA0xff79125d_002EWPP_FirmwareUpdateAPI_cpp_Traceguids), num);
		}
	}

	private unsafe void StartGetRestorePointCollectionWorker(object data)
	{
		//IL_0005: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Expected O, but got Unknown
		Unsafe.SkipInit(out CComPtrNtv_003CIFirmwareRestorePointCollection_003E cComPtrNtv_003CIFirmwareRestorePointCollection_003E);
		*(int*)(&cComPtrNtv_003CIFirmwareRestorePointCollection_003E) = 0;
		try
		{
			DeferredInvokeHandler val = (DeferredInvokeHandler)data;
			int num = 0;
			if (global::_003CModule_003E.WPP_GLOBAL_Control != Unsafe.AsPointer(ref global::_003CModule_003E.WPP_GLOBAL_Control) && (((int*)global::_003CModule_003E.WPP_GLOBAL_Control)[15] & 4) != 0 && (uint)((byte*)global::_003CModule_003E.WPP_GLOBAL_Control)[57] >= 5u)
			{
				global::_003CModule_003E.WPP_SF_(((ulong*)global::_003CModule_003E.WPP_GLOBAL_Control)[6], 90, (_GUID*)Unsafe.AsPointer(ref global::_003CModule_003E._003FA0xff79125d_002EWPP_FirmwareUpdateAPI_cpp_Traceguids));
			}
			if (Application.IsApplicationThread)
			{
				global::_003CModule_003E._ZuneShipAssert(1004u, 2687u);
			}
			Monitor.Enter(m_FirmwareLock);
			try
			{
				num = EnsureNativeObject();
				if (num >= 0)
				{
					IFirmwareRestorer* p = m_spFirmwareRestorer.p;
					num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, IFirmwareRestorePointCollection**, int>)(int)(*(uint*)(*(int*)p + 12)))((nint)p, (IFirmwareRestorePointCollection**)(&cComPtrNtv_003CIFirmwareRestorePointCollection_003E));
				}
				if ((MulticastDelegate?)(object)val != null)
				{
					FirmwareRestorePointCollection firmwareRestorePointCollection;
					if (num >= 0)
					{
						IFirmwareRestorePointCollection* ptr = (IFirmwareRestorePointCollection*)(int)(*(uint*)(&cComPtrNtv_003CIFirmwareRestorePointCollection_003E));
						firmwareRestorePointCollection = new FirmwareRestorePointCollection((IFirmwareRestorePointCollection*)(int)(*(uint*)(&cComPtrNtv_003CIFirmwareRestorePointCollection_003E)));
					}
					else
					{
						firmwareRestorePointCollection = null;
					}
					val.Invoke((object)firmwareRestorePointCollection);
				}
			}
			finally
			{
				Monitor.Exit(m_FirmwareLock);
			}
			if (global::_003CModule_003E.WPP_GLOBAL_Control != Unsafe.AsPointer(ref global::_003CModule_003E.WPP_GLOBAL_Control) && (((int*)global::_003CModule_003E.WPP_GLOBAL_Control)[15] & 4) != 0 && (uint)((byte*)global::_003CModule_003E.WPP_GLOBAL_Control)[57] >= 5u)
			{
				global::_003CModule_003E.WPP_SF_d(((ulong*)global::_003CModule_003E.WPP_GLOBAL_Control)[6], 91, (_GUID*)Unsafe.AsPointer(ref global::_003CModule_003E._003FA0xff79125d_002EWPP_FirmwareUpdateAPI_cpp_Traceguids), num);
			}
		}
		catch
		{
			//try-fault
			global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<CComPtrNtv_003CIFirmwareRestorePointCollection_003E*, void>)(&global::_003CModule_003E.CComPtrNtv_003CIFirmwareRestorePointCollection_003E_002E_007Bdtor_007D), &cComPtrNtv_003CIFirmwareRestorePointCollection_003E);
			throw;
		}
		global::_003CModule_003E.CComPtrNtv_003CIFirmwareRestorePointCollection_003E_002ERelease(&cComPtrNtv_003CIFirmwareRestorePointCollection_003E);
	}

	private unsafe void CancelWorker(object data)
	{
		if (Application.IsApplicationThread)
		{
			global::_003CModule_003E._ZuneShipAssert(1004u, 2758u);
		}
		Monitor.Enter(m_FirmwareLock);
		try
		{
			CComPtrMgd_003CMicrosoftZuneLibrary_003A_003AFirmwareUpdateMediator_003E spFirmwareMediator = m_spFirmwareMediator;
			if (spFirmwareMediator.p != null)
			{
				global::_003CModule_003E.MicrosoftZuneLibrary_002EFirmwareUpdateMediator_002EFirmwareProcessCanceled(spFirmwareMediator.p);
			}
			CComPtrMgd_003CIFirmwareRestorer_003E spFirmwareRestorer = m_spFirmwareRestorer;
			if (spFirmwareRestorer.p != null)
			{
				IFirmwareRestorer* p = spFirmwareRestorer.p;
				if (((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, int>)(int)(*(uint*)(*(int*)p + 28)))((nint)p) < 0)
				{
					global::_003CModule_003E._ZuneShipAssert(1004u, 2773u);
				}
				Reset(deviceRebooting: false);
			}
			else if (IsDeviceRebooting())
			{
				DeferredCancel();
			}
		}
		finally
		{
			Monitor.Exit(m_FirmwareLock);
		}
		if (global::_003CModule_003E.WPP_GLOBAL_Control != Unsafe.AsPointer(ref global::_003CModule_003E.WPP_GLOBAL_Control) && (((int*)global::_003CModule_003E.WPP_GLOBAL_Control)[15] & 4) != 0 && (uint)((byte*)global::_003CModule_003E.WPP_GLOBAL_Control)[57] >= 5u)
		{
			global::_003CModule_003E.WPP_SF_(((ulong*)global::_003CModule_003E.WPP_GLOBAL_Control)[6], 94, (_GUID*)Unsafe.AsPointer(ref global::_003CModule_003E._003FA0xff79125d_002EWPP_FirmwareUpdateAPI_cpp_Traceguids));
		}
	}

	protected override void Dispose([MarshalAs(UnmanagedType.U1)] bool P_0)
	{
		if (P_0)
		{
			try
			{
				_007EFirmwareRestorer();
				return;
			}
			finally
			{
				try
				{
					base.Dispose(true);
				}
				finally
				{
					((IDisposable)m_spFirmwareRestorer).Dispose();
				}
			}
		}
		base.Dispose(false);
	}
}
