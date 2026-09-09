using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using Microsoft.Iris;
using ZuneUI;

namespace MicrosoftZuneLibrary;

public class FirmwareUpdater : FirmwareOperationBase
{
	private readonly CComPtrMgd_003CIFirmwareUpdater_003E m_spFirmwareUpdater;

	private FirmwareRestorer m_Restorer;

	public unsafe FirmwareRestorer Restorer
	{
		get
		{
			if (m_Restorer == null)
			{
				IEndpointHost* p = m_spEndpointHost.p;
				m_Restorer = new FirmwareRestorer(p, m_fFirmwareProcessSupported, this);
			}
			return m_Restorer;
		}
	}

	public unsafe UpdateAction IsUpdateInProgress()
	{
		if (global::_003CModule_003E.WPP_GLOBAL_Control != Unsafe.AsPointer(ref global::_003CModule_003E.WPP_GLOBAL_Control) && (((int*)global::_003CModule_003E.WPP_GLOBAL_Control)[15] & 4) != 0 && (uint)((byte*)global::_003CModule_003E.WPP_GLOBAL_Control)[57] >= 5u)
		{
			global::_003CModule_003E.WPP_SF_(((ulong*)global::_003CModule_003E.WPP_GLOBAL_Control)[6], 37, (_GUID*)Unsafe.AsPointer(ref global::_003CModule_003E._003FA0xff79125d_002EWPP_FirmwareUpdateAPI_cpp_Traceguids));
		}
		EUpdateAction eUpdateAction = ((m_spFirmwareMediator.p != null) ? ((EUpdateAction)1) : ((EUpdateAction)0));
		EUpdateAction eUpdateAction2 = eUpdateAction;
		if (!m_Canceled)
		{
			if (Application.IsApplicationThread)
			{
				global::_003CModule_003E._ZuneShipAssert(1004u, 1435u);
			}
			Monitor.Enter(m_FirmwareLock);
			try
			{
				if (m_fFirmwareProcessSupported && EnsureNativeObject() >= 0)
				{
					IFirmwareUpdater* p = m_spFirmwareUpdater.p;
					((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, EUpdateAction*, int>)(int)(*(uint*)(*(int*)p + 16)))((nint)p, &eUpdateAction2);
				}
			}
			finally
			{
				Monitor.Exit(m_FirmwareLock);
			}
		}
		if (global::_003CModule_003E.WPP_GLOBAL_Control != Unsafe.AsPointer(ref global::_003CModule_003E.WPP_GLOBAL_Control) && (((int*)global::_003CModule_003E.WPP_GLOBAL_Control)[15] & 4) != 0 && (uint)((byte*)global::_003CModule_003E.WPP_GLOBAL_Control)[57] >= 5u)
		{
			global::_003CModule_003E.WPP_SF_qd(((ulong*)global::_003CModule_003E.WPP_GLOBAL_Control)[6], 38, (_GUID*)Unsafe.AsPointer(ref global::_003CModule_003E._003FA0xff79125d_002EWPP_FirmwareUpdateAPI_cpp_Traceguids), m_spFirmwareUpdater.p, (int)eUpdateAction2);
		}
		return (UpdateAction)eUpdateAction2;
	}

	[return: MarshalAs(UnmanagedType.U1)]
	public unsafe bool RequiresSyncBeforeUpdate()
	{
		ManagedLock managedLock = null;
		bool flag = false;
		Unsafe.SkipInit(out CComPtrNtv_003CIFirmwareUpdater2_003E cComPtrNtv_003CIFirmwareUpdater2_003E);
		*(int*)(&cComPtrNtv_003CIFirmwareUpdater2_003E) = 0;
		try
		{
			if (global::_003CModule_003E.WPP_GLOBAL_Control != Unsafe.AsPointer(ref global::_003CModule_003E.WPP_GLOBAL_Control) && (((int*)global::_003CModule_003E.WPP_GLOBAL_Control)[15] & 4) != 0 && (uint)((byte*)global::_003CModule_003E.WPP_GLOBAL_Control)[57] >= 5u)
			{
				global::_003CModule_003E.WPP_SF_(((ulong*)global::_003CModule_003E.WPP_GLOBAL_Control)[6], 35, (_GUID*)Unsafe.AsPointer(ref global::_003CModule_003E._003FA0xff79125d_002EWPP_FirmwareUpdateAPI_cpp_Traceguids));
			}
			ManagedLock managedLock2 = new ManagedLock(m_FirmwareLock);
			try
			{
				managedLock = managedLock2;
				int num = EnsureNativeObject();
				if (num >= 0)
				{
					num = m_spFirmwareUpdater.QueryInterface_003CIFirmwareUpdater2_003E((IFirmwareUpdater2**)(&cComPtrNtv_003CIFirmwareUpdater2_003E));
					if (num >= 0)
					{
						int num2 = 0;
						num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, int*, int>)(int)(*(uint*)(*(int*)(int)(*(uint*)(&cComPtrNtv_003CIFirmwareUpdater2_003E)) + 36)))((IntPtr)(*(int*)(&cComPtrNtv_003CIFirmwareUpdater2_003E)), &num2);
						int num3 = ((num >= 0 && num2 != 0) ? 1 : 0);
						flag = (byte)num3 != 0;
					}
				}
				if (global::_003CModule_003E.WPP_GLOBAL_Control != Unsafe.AsPointer(ref global::_003CModule_003E.WPP_GLOBAL_Control) && (((int*)global::_003CModule_003E.WPP_GLOBAL_Control)[15] & 4) != 0 && (uint)((byte*)global::_003CModule_003E.WPP_GLOBAL_Control)[57] >= 5u)
				{
					global::_003CModule_003E.WPP_SF_dl(((ulong*)global::_003CModule_003E.WPP_GLOBAL_Control)[6], 36, (_GUID*)Unsafe.AsPointer(ref global::_003CModule_003E._003FA0xff79125d_002EWPP_FirmwareUpdateAPI_cpp_Traceguids), num, flag ? 1 : 0);
				}
			}
			catch
			{
				//try-fault
				((IDisposable)managedLock).Dispose();
				throw;
			}
			((IDisposable)managedLock).Dispose();
		}
		catch
		{
			//try-fault
			global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<CComPtrNtv_003CIFirmwareUpdater2_003E*, void>)(&global::_003CModule_003E.CComPtrNtv_003CIFirmwareUpdater2_003E_002E_007Bdtor_007D), &cComPtrNtv_003CIFirmwareUpdater2_003E);
			throw;
		}
		global::_003CModule_003E.CComPtrNtv_003CIFirmwareUpdater2_003E_002ERelease(&cComPtrNtv_003CIFirmwareUpdater2_003E);
		return flag;
	}

	public unsafe HRESULT StartCheckForDiskSpace(DeferredInvokeHandler checkForDiskSpaceComplete)
	{
		if (global::_003CModule_003E.WPP_GLOBAL_Control != Unsafe.AsPointer(ref global::_003CModule_003E.WPP_GLOBAL_Control) && (((int*)global::_003CModule_003E.WPP_GLOBAL_Control)[15] & 4) != 0 && (uint)((byte*)global::_003CModule_003E.WPP_GLOBAL_Control)[57] >= 5u)
		{
			global::_003CModule_003E.WPP_SF_(((ulong*)global::_003CModule_003E.WPP_GLOBAL_Control)[6], 47, (_GUID*)Unsafe.AsPointer(ref global::_003CModule_003E._003FA0xff79125d_002EWPP_FirmwareUpdateAPI_cpp_Traceguids));
		}
		int num = 0;
		if (Application.IsApplicationThread)
		{
			if (!ThreadPool.QueueUserWorkItem(StartCheckForDiskSpaceWorker, checkForDiskSpaceComplete))
			{
				global::_003CModule_003E._ZuneShipAssert(1004u, 1753u);
				num = -2147467259;
			}
		}
		else
		{
			StartCheckForDiskSpaceWorker(checkForDiskSpaceComplete);
		}
		if (global::_003CModule_003E.WPP_GLOBAL_Control != Unsafe.AsPointer(ref global::_003CModule_003E.WPP_GLOBAL_Control) && (((int*)global::_003CModule_003E.WPP_GLOBAL_Control)[15] & 4) != 0 && (uint)((byte*)global::_003CModule_003E.WPP_GLOBAL_Control)[57] >= 5u)
		{
			global::_003CModule_003E.WPP_SF_d(((ulong*)global::_003CModule_003E.WPP_GLOBAL_Control)[6], 48, (_GUID*)Unsafe.AsPointer(ref global::_003CModule_003E._003FA0xff79125d_002EWPP_FirmwareUpdateAPI_cpp_Traceguids), num);
		}
		return num;
	}

	public unsafe HRESULT StartCheckForUpdates([MarshalAs(UnmanagedType.U1)] bool fForceServerRequest, DeferredInvokeHandler checkForUpdatesComplete)
	{
		if (global::_003CModule_003E.WPP_GLOBAL_Control != Unsafe.AsPointer(ref global::_003CModule_003E.WPP_GLOBAL_Control) && (((int*)global::_003CModule_003E.WPP_GLOBAL_Control)[15] & 4) != 0 && (uint)((byte*)global::_003CModule_003E.WPP_GLOBAL_Control)[57] >= 5u)
		{
			global::_003CModule_003E.WPP_SF_l(((ulong*)global::_003CModule_003E.WPP_GLOBAL_Control)[6], 39, (_GUID*)Unsafe.AsPointer(ref global::_003CModule_003E._003FA0xff79125d_002EWPP_FirmwareUpdateAPI_cpp_Traceguids), fForceServerRequest ? 1 : 0);
		}
		int num = 0;
		if (m_fFirmwareProcessSupported)
		{
			if ((MulticastDelegate?)(object)checkForUpdatesComplete == null)
			{
				global::_003CModule_003E._ZuneShipAssert(1004u, 1479u);
				num = -2147467261;
			}
			else
			{
				m_Canceled = false;
				StartCheckForUpdatesArgs startCheckForUpdatesArgs = new StartCheckForUpdatesArgs(fForceServerRequest, checkForUpdatesComplete);
				if (Application.IsApplicationThread)
				{
					if (!ThreadPool.QueueUserWorkItem(StartCheckForUpdatesWorker, startCheckForUpdatesArgs))
					{
						global::_003CModule_003E._ZuneShipAssert(1004u, 1497u);
						num = -2147467259;
					}
				}
				else
				{
					StartCheckForUpdatesWorker(startCheckForUpdatesArgs);
					num = startCheckForUpdatesArgs.HrStatus;
				}
			}
		}
		else
		{
			num = -2147024846;
		}
		if (global::_003CModule_003E.WPP_GLOBAL_Control != Unsafe.AsPointer(ref global::_003CModule_003E.WPP_GLOBAL_Control) && (((int*)global::_003CModule_003E.WPP_GLOBAL_Control)[15] & 4) != 0 && (uint)((byte*)global::_003CModule_003E.WPP_GLOBAL_Control)[57] >= 5u)
		{
			global::_003CModule_003E.WPP_SF_d(((ulong*)global::_003CModule_003E.WPP_GLOBAL_Control)[6], 40, (_GUID*)Unsafe.AsPointer(ref global::_003CModule_003E._003FA0xff79125d_002EWPP_FirmwareUpdateAPI_cpp_Traceguids), num);
		}
		return num;
	}

	public unsafe HRESULT StartFirmwareUpdate(UpdatePackageCollection updates, DeferredInvokeHandler firmwareUpdateBegin, DeferredInvokeHandler firmwareUpdateProgress, DeferredInvokeHandler firmwareUpdateComplete, FirmwareUpdateOption updateOption)
	{
		if (global::_003CModule_003E.WPP_GLOBAL_Control != Unsafe.AsPointer(ref global::_003CModule_003E.WPP_GLOBAL_Control) && (((int*)global::_003CModule_003E.WPP_GLOBAL_Control)[15] & 4) != 0 && (uint)((byte*)global::_003CModule_003E.WPP_GLOBAL_Control)[57] >= 5u)
		{
			global::_003CModule_003E.WPP_SF_(((ulong*)global::_003CModule_003E.WPP_GLOBAL_Control)[6], 43, (_GUID*)Unsafe.AsPointer(ref global::_003CModule_003E._003FA0xff79125d_002EWPP_FirmwareUpdateAPI_cpp_Traceguids));
		}
		m_Canceled = false;
		int num = 0;
		if (updates == null)
		{
			num = -2147418113;
		}
		else
		{
			if (global::_003CModule_003E.WPP_GLOBAL_Control != Unsafe.AsPointer(ref global::_003CModule_003E.WPP_GLOBAL_Control) && (((int*)global::_003CModule_003E.WPP_GLOBAL_Control)[15] & 4) != 0 && (uint)((byte*)global::_003CModule_003E.WPP_GLOBAL_Control)[57] >= 6u)
			{
				global::_003CModule_003E.WPP_SF_d(((ulong*)global::_003CModule_003E.WPP_GLOBAL_Control)[6], 44, (_GUID*)Unsafe.AsPointer(ref global::_003CModule_003E._003FA0xff79125d_002EWPP_FirmwareUpdateAPI_cpp_Traceguids), updates.Count);
			}
			int num2 = 0;
			if (0 < updates.Count)
			{
				do
				{
					FirmwareUpdatePackage firmwareUpdatePackage = updates.get_Item(num2);
					fixed (ushort* a = &Unsafe.As<char, ushort>(ref global::_003CModule_003E.PtrToStringChars(firmwareUpdatePackage.Name)))
					{
						try
						{
							if (global::_003CModule_003E.WPP_GLOBAL_Control != Unsafe.AsPointer(ref global::_003CModule_003E.WPP_GLOBAL_Control) && (((int*)global::_003CModule_003E.WPP_GLOBAL_Control)[15] & 4) != 0 && (uint)((byte*)global::_003CModule_003E.WPP_GLOBAL_Control)[57] >= 6u)
							{
								global::_003CModule_003E.WPP_SF_dSl(((ulong*)global::_003CModule_003E.WPP_GLOBAL_Control)[6], 45, (_GUID*)Unsafe.AsPointer(ref global::_003CModule_003E._003FA0xff79125d_002EWPP_FirmwareUpdateAPI_cpp_Traceguids), num2, a, firmwareUpdatePackage.Selected ? 1 : 0);
							}
						}
						catch
						{
							//try-fault
							a = null;
							throw;
						}
					}
					num2++;
				}
				while (num2 < updates.Count);
			}
			StartFirmwareUpdateArgs startFirmwareUpdateArgs = new StartFirmwareUpdateArgs(updates, firmwareUpdateBegin, firmwareUpdateProgress, firmwareUpdateComplete, updateOption);
			if (Application.IsApplicationThread)
			{
				if (!ThreadPool.QueueUserWorkItem(StartFirmwareUpdateWorker, startFirmwareUpdateArgs))
				{
					global::_003CModule_003E._ZuneShipAssert(1004u, 1641u);
					num = -2147467259;
				}
			}
			else
			{
				StartFirmwareUpdateWorker(startFirmwareUpdateArgs);
				num = startFirmwareUpdateArgs.HrStatus;
			}
		}
		return num;
	}

	public unsafe HRESULT Cancel()
	{
		if (global::_003CModule_003E.WPP_GLOBAL_Control != Unsafe.AsPointer(ref global::_003CModule_003E.WPP_GLOBAL_Control) && (((int*)global::_003CModule_003E.WPP_GLOBAL_Control)[15] & 4) != 0 && (uint)((byte*)global::_003CModule_003E.WPP_GLOBAL_Control)[57] >= 5u)
		{
			global::_003CModule_003E.WPP_SF_(((ulong*)global::_003CModule_003E.WPP_GLOBAL_Control)[6], 56, (_GUID*)Unsafe.AsPointer(ref global::_003CModule_003E._003FA0xff79125d_002EWPP_FirmwareUpdateAPI_cpp_Traceguids));
		}
		int num = 0;
		if (m_fFirmwareProcessSupported)
		{
			m_Canceled = true;
			if (Application.IsApplicationThread)
			{
				if (!ThreadPool.QueueUserWorkItem(CancelWorker, null))
				{
					global::_003CModule_003E._ZuneShipAssert(1004u, 1945u);
				}
			}
			else
			{
				CancelWorker(null);
			}
		}
		else
		{
			global::_003CModule_003E._ZuneShipAssert(1004u, 1955u);
			num = -2147024846;
		}
		if (global::_003CModule_003E.WPP_GLOBAL_Control != Unsafe.AsPointer(ref global::_003CModule_003E.WPP_GLOBAL_Control) && (((int*)global::_003CModule_003E.WPP_GLOBAL_Control)[15] & 4) != 0 && (uint)((byte*)global::_003CModule_003E.WPP_GLOBAL_Control)[57] >= 5u)
		{
			global::_003CModule_003E.WPP_SF_(((ulong*)global::_003CModule_003E.WPP_GLOBAL_Control)[6], 57, (_GUID*)Unsafe.AsPointer(ref global::_003CModule_003E._003FA0xff79125d_002EWPP_FirmwareUpdateAPI_cpp_Traceguids));
		}
		return num;
	}

	internal unsafe FirmwareUpdater(IEndpointHost* pEndpointHost, [MarshalAs(UnmanagedType.U1)] bool fFirmwareProcessSupported)
	{
		CComPtrMgd_003CIFirmwareUpdater_003E spFirmwareUpdater = new CComPtrMgd_003CIFirmwareUpdater_003E();
		try
		{
			m_spFirmwareUpdater = spFirmwareUpdater;
			base._002Ector();
			try
			{
				if (global::_003CModule_003E.WPP_GLOBAL_Control != Unsafe.AsPointer(ref global::_003CModule_003E.WPP_GLOBAL_Control) && (((int*)global::_003CModule_003E.WPP_GLOBAL_Control)[15] & 4) != 0 && (uint)((byte*)global::_003CModule_003E.WPP_GLOBAL_Control)[57] >= 5u)
				{
					global::_003CModule_003E.WPP_SF_q(((ulong*)global::_003CModule_003E.WPP_GLOBAL_Control)[6], 31, (_GUID*)Unsafe.AsPointer(ref global::_003CModule_003E._003FA0xff79125d_002EWPP_FirmwareUpdateAPI_cpp_Traceguids), pEndpointHost);
				}
				m_spEndpointHost.op_Assign(pEndpointHost);
				m_fFirmwareProcessSupported = fFirmwareProcessSupported;
				if (global::_003CModule_003E.WPP_GLOBAL_Control != Unsafe.AsPointer(ref global::_003CModule_003E.WPP_GLOBAL_Control) && (((int*)global::_003CModule_003E.WPP_GLOBAL_Control)[15] & 4) != 0 && (uint)((byte*)global::_003CModule_003E.WPP_GLOBAL_Control)[57] >= 5u)
				{
					global::_003CModule_003E.WPP_SF_(((ulong*)global::_003CModule_003E.WPP_GLOBAL_Control)[6], 32, (_GUID*)Unsafe.AsPointer(ref global::_003CModule_003E._003FA0xff79125d_002EWPP_FirmwareUpdateAPI_cpp_Traceguids));
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
			((IDisposable)m_spFirmwareUpdater).Dispose();
			throw;
		}
	}

	private unsafe void _007EFirmwareUpdater()
	{
		if (global::_003CModule_003E.WPP_GLOBAL_Control != Unsafe.AsPointer(ref global::_003CModule_003E.WPP_GLOBAL_Control) && (((int*)global::_003CModule_003E.WPP_GLOBAL_Control)[15] & 4) != 0 && (uint)((byte*)global::_003CModule_003E.WPP_GLOBAL_Control)[57] >= 5u)
		{
			global::_003CModule_003E.WPP_SF_(((ulong*)global::_003CModule_003E.WPP_GLOBAL_Control)[6], 33, (_GUID*)Unsafe.AsPointer(ref global::_003CModule_003E._003FA0xff79125d_002EWPP_FirmwareUpdateAPI_cpp_Traceguids));
		}
		if (IsUpdateInProgress() != UpdateAction.None)
		{
			global::_003CModule_003E._ZuneShipAssert(1004u, 1355u);
		}
		m_spFirmwareUpdater.Release();
		FirmwareRestorer restorer = m_Restorer;
		if (restorer != null)
		{
			((IDisposable)restorer).Dispose();
			m_Restorer = null;
		}
		if (global::_003CModule_003E.WPP_GLOBAL_Control != Unsafe.AsPointer(ref global::_003CModule_003E.WPP_GLOBAL_Control) && (((int*)global::_003CModule_003E.WPP_GLOBAL_Control)[15] & 4) != 0 && (uint)((byte*)global::_003CModule_003E.WPP_GLOBAL_Control)[57] >= 5u)
		{
			global::_003CModule_003E.WPP_SF_(((ulong*)global::_003CModule_003E.WPP_GLOBAL_Control)[6], 34, (_GUID*)Unsafe.AsPointer(ref global::_003CModule_003E._003FA0xff79125d_002EWPP_FirmwareUpdateAPI_cpp_Traceguids));
		}
	}

	internal unsafe override int InternalReset()
	{
		if (global::_003CModule_003E.WPP_GLOBAL_Control != Unsafe.AsPointer(ref global::_003CModule_003E.WPP_GLOBAL_Control) && (((int*)global::_003CModule_003E.WPP_GLOBAL_Control)[15] & 4) != 0 && (uint)((byte*)global::_003CModule_003E.WPP_GLOBAL_Control)[57] >= 5u)
		{
			global::_003CModule_003E.WPP_SF_(((ulong*)global::_003CModule_003E.WPP_GLOBAL_Control)[6], 59, (_GUID*)Unsafe.AsPointer(ref global::_003CModule_003E._003FA0xff79125d_002EWPP_FirmwareUpdateAPI_cpp_Traceguids));
		}
		m_Restorer?.ReleaseNativeObject();
		ReleaseNativeObject();
		return 0;
	}

	internal override int ContinueFirmwareProcess(FirmwareCompleteHandler onCompleteHandler)
	{
		int result = 0;
		if (!ThreadPool.QueueUserWorkItem(state: new ContinueFirmwareUpdateArgs(onCompleteHandler), callBack: ContinueFirmwareUpdateWorker))
		{
			global::_003CModule_003E._ZuneShipAssert(1004u, 1838u);
			result = -2147467259;
			SendCompleteNotification(-2147467259, disconnectDeviceOnComplete: false);
		}
		return result;
	}

	internal unsafe override void ReleaseNativeObject()
	{
		if (global::_003CModule_003E.WPP_GLOBAL_Control != Unsafe.AsPointer(ref global::_003CModule_003E.WPP_GLOBAL_Control) && (((int*)global::_003CModule_003E.WPP_GLOBAL_Control)[15] & 4) != 0 && (uint)((byte*)global::_003CModule_003E.WPP_GLOBAL_Control)[57] >= 7u)
		{
			global::_003CModule_003E.WPP_SF_(((ulong*)global::_003CModule_003E.WPP_GLOBAL_Control)[6], 61, (_GUID*)Unsafe.AsPointer(ref global::_003CModule_003E._003FA0xff79125d_002EWPP_FirmwareUpdateAPI_cpp_Traceguids));
		}
		Monitor.Enter(m_FirmwareLock);
		try
		{
			m_spFirmwareUpdater.op_Assign(null);
		}
		finally
		{
			Monitor.Exit(m_FirmwareLock);
		}
	}

	internal unsafe int GetNativeObject(IFirmwareUpdater** ppUpdater)
	{
		int num = 0;
		if (global::_003CModule_003E.WPP_GLOBAL_Control != Unsafe.AsPointer(ref global::_003CModule_003E.WPP_GLOBAL_Control) && (((int*)global::_003CModule_003E.WPP_GLOBAL_Control)[15] & 4) != 0 && (uint)((byte*)global::_003CModule_003E.WPP_GLOBAL_Control)[57] >= 5u)
		{
			global::_003CModule_003E.WPP_SF_(((ulong*)global::_003CModule_003E.WPP_GLOBAL_Control)[6], 62, (_GUID*)Unsafe.AsPointer(ref global::_003CModule_003E._003FA0xff79125d_002EWPP_FirmwareUpdateAPI_cpp_Traceguids));
		}
		if (ppUpdater == null)
		{
			global::_003CModule_003E._ZuneShipAssert(1001u, 2089u);
			return -2147467261;
		}
		if (Application.IsApplicationThread)
		{
			global::_003CModule_003E._ZuneShipAssert(1004u, 2091u);
		}
		Monitor.Enter(m_FirmwareLock);
		try
		{
			num = EnsureNativeObject();
			if (num >= 0)
			{
				num = m_spFirmwareUpdater.CopyTo(ppUpdater);
			}
		}
		finally
		{
			Monitor.Exit(m_FirmwareLock);
		}
		if (global::_003CModule_003E.WPP_GLOBAL_Control != Unsafe.AsPointer(ref global::_003CModule_003E.WPP_GLOBAL_Control) && (((int*)global::_003CModule_003E.WPP_GLOBAL_Control)[15] & 4) != 0 && (uint)((byte*)global::_003CModule_003E.WPP_GLOBAL_Control)[57] >= 5u)
		{
			global::_003CModule_003E.WPP_SF_d(((ulong*)global::_003CModule_003E.WPP_GLOBAL_Control)[6], 63, (_GUID*)Unsafe.AsPointer(ref global::_003CModule_003E._003FA0xff79125d_002EWPP_FirmwareUpdateAPI_cpp_Traceguids), num);
		}
		return num;
	}

	private unsafe int EnsureNativeObject()
	{
		if (global::_003CModule_003E.WPP_GLOBAL_Control != Unsafe.AsPointer(ref global::_003CModule_003E.WPP_GLOBAL_Control) && (((int*)global::_003CModule_003E.WPP_GLOBAL_Control)[15] & 4) != 0 && (uint)((byte*)global::_003CModule_003E.WPP_GLOBAL_Control)[57] >= 7u)
		{
			global::_003CModule_003E.WPP_SF_(((ulong*)global::_003CModule_003E.WPP_GLOBAL_Control)[6], 60, (_GUID*)Unsafe.AsPointer(ref global::_003CModule_003E._003FA0xff79125d_002EWPP_FirmwareUpdateAPI_cpp_Traceguids));
		}
		int num = 0;
		if (Application.IsApplicationThread)
		{
			global::_003CModule_003E._ZuneShipAssert(1004u, 2028u);
		}
		Monitor.Enter(m_FirmwareLock);
		try
		{
			if (!m_fFirmwareProcessSupported)
			{
				int num2 = -2147024846;
				num = -2147024846;
			}
			else if (m_spFirmwareUpdater.p == null)
			{
				Unsafe.SkipInit(out CComPtrNtv_003CIFirmwareUpdater_003E cComPtrNtv_003CIFirmwareUpdater_003E);
				*(int*)(&cComPtrNtv_003CIFirmwareUpdater_003E) = 0;
				try
				{
					num = global::_003CModule_003E.GetInterfaceProperty_003Cstruct_0020IEndpointHost_002Cenum_0020EEndpointHostProperty_002Cstruct_0020IFirmwareUpdater_003E(m_spEndpointHost.p, EEndpointHostProperty.eEndpointHostPropertyFirmwareUpdater, (IFirmwareUpdater**)(&cComPtrNtv_003CIFirmwareUpdater_003E));
					if (num >= 0)
					{
						IFirmwareUpdater* ptr = (IFirmwareUpdater*)(int)(*(uint*)(&cComPtrNtv_003CIFirmwareUpdater_003E));
						m_spFirmwareUpdater.op_Assign((IFirmwareUpdater*)(int)(*(uint*)(&cComPtrNtv_003CIFirmwareUpdater_003E)));
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

	private unsafe void ContinueFirmwareUpdateWorker(object data)
	{
		if (global::_003CModule_003E.WPP_GLOBAL_Control != Unsafe.AsPointer(ref global::_003CModule_003E.WPP_GLOBAL_Control) && (((int*)global::_003CModule_003E.WPP_GLOBAL_Control)[15] & 4) != 0 && (uint)((byte*)global::_003CModule_003E.WPP_GLOBAL_Control)[57] >= 5u)
		{
			global::_003CModule_003E.WPP_SF_(((ulong*)global::_003CModule_003E.WPP_GLOBAL_Control)[6], 51, (_GUID*)Unsafe.AsPointer(ref global::_003CModule_003E._003FA0xff79125d_002EWPP_FirmwareUpdateAPI_cpp_Traceguids));
		}
		int num = 0;
		if (m_Canceled)
		{
			num = -2147467260;
			if (global::_003CModule_003E.WPP_GLOBAL_Control != Unsafe.AsPointer(ref global::_003CModule_003E.WPP_GLOBAL_Control) && (((int*)global::_003CModule_003E.WPP_GLOBAL_Control)[15] & 4) != 0 && (uint)((byte*)global::_003CModule_003E.WPP_GLOBAL_Control)[57] >= 5u)
			{
				global::_003CModule_003E.WPP_SF_(((ulong*)global::_003CModule_003E.WPP_GLOBAL_Control)[6], 52, (_GUID*)Unsafe.AsPointer(ref global::_003CModule_003E._003FA0xff79125d_002EWPP_FirmwareUpdateAPI_cpp_Traceguids));
			}
		}
		else if (m_spFirmwareMediator.p == null)
		{
			if (global::_003CModule_003E.WPP_GLOBAL_Control != Unsafe.AsPointer(ref global::_003CModule_003E.WPP_GLOBAL_Control) && (((int*)global::_003CModule_003E.WPP_GLOBAL_Control)[15] & 4) != 0 && (uint)((byte*)global::_003CModule_003E.WPP_GLOBAL_Control)[57] >= 2u)
			{
				global::_003CModule_003E.WPP_SF_(((ulong*)global::_003CModule_003E.WPP_GLOBAL_Control)[6], 53, (_GUID*)Unsafe.AsPointer(ref global::_003CModule_003E._003FA0xff79125d_002EWPP_FirmwareUpdateAPI_cpp_Traceguids));
			}
			num = -2147418113;
		}
		if (Application.IsApplicationThread)
		{
			global::_003CModule_003E._ZuneShipAssert(1004u, 1882u);
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
					IFirmwareUpdater* p = m_spFirmwareUpdater.p;
					FirmwareUpdateMediator* p2 = m_spFirmwareMediator.p;
					num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, IFirmwareUpdateCallback*, int>)(int)(*(uint*)(*(int*)p + 28)))((nint)p, (IFirmwareUpdateCallback*)p2);
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
				global::_003CModule_003E.WPP_SF_d(((ulong*)global::_003CModule_003E.WPP_GLOBAL_Control)[6], 54, (_GUID*)Unsafe.AsPointer(ref global::_003CModule_003E._003FA0xff79125d_002EWPP_FirmwareUpdateAPI_cpp_Traceguids), num);
			}
			SendCompleteNotification(num, disconnectDeviceOnComplete: false);
		}
		if (global::_003CModule_003E.WPP_GLOBAL_Control != Unsafe.AsPointer(ref global::_003CModule_003E.WPP_GLOBAL_Control) && (((int*)global::_003CModule_003E.WPP_GLOBAL_Control)[15] & 4) != 0 && (uint)((byte*)global::_003CModule_003E.WPP_GLOBAL_Control)[57] >= 5u)
		{
			global::_003CModule_003E.WPP_SF_d(((ulong*)global::_003CModule_003E.WPP_GLOBAL_Control)[6], 55, (_GUID*)Unsafe.AsPointer(ref global::_003CModule_003E._003FA0xff79125d_002EWPP_FirmwareUpdateAPI_cpp_Traceguids), num);
		}
	}

	private unsafe void StartCheckForUpdatesWorker(object data)
	{
		if (global::_003CModule_003E.WPP_GLOBAL_Control != Unsafe.AsPointer(ref global::_003CModule_003E.WPP_GLOBAL_Control) && (((int*)global::_003CModule_003E.WPP_GLOBAL_Control)[15] & 4) != 0 && (uint)((byte*)global::_003CModule_003E.WPP_GLOBAL_Control)[57] >= 5u)
		{
			global::_003CModule_003E.WPP_SF_(((ulong*)global::_003CModule_003E.WPP_GLOBAL_Control)[6], 41, (_GUID*)Unsafe.AsPointer(ref global::_003CModule_003E._003FA0xff79125d_002EWPP_FirmwareUpdateAPI_cpp_Traceguids));
		}
		StartCheckForUpdatesArgs startCheckForUpdatesArgs = (StartCheckForUpdatesArgs)data;
		int num = 0;
		Unsafe.SkipInit(out CComPtrNtv_003CIFirmwareUpdateCallback_003E cComPtrNtv_003CIFirmwareUpdateCallback_003E);
		*(int*)(&cComPtrNtv_003CIFirmwareUpdateCallback_003E) = 0;
		try
		{
			bool requiresSyncBeforeUpdate = false;
			if (Application.IsApplicationThread)
			{
				global::_003CModule_003E._ZuneShipAssert(1004u, 1533u);
			}
			Monitor.Enter(m_FirmwareLock);
			try
			{
				num = EnsureNativeObject();
				if (num >= 0)
				{
					requiresSyncBeforeUpdate = RequiresSyncBeforeUpdate();
				}
				CheckForUpdatesCallback* ptr = (CheckForUpdatesCallback*)global::_003CModule_003E.@new(20u);
				CheckForUpdatesCallback* ptr2;
				try
				{
					ptr2 = ((ptr == null) ? null : global::_003CModule_003E.MicrosoftZuneLibrary_002ECheckForUpdatesCallback_002E_007Bctor_007D(ptr, this, startCheckForUpdatesArgs.CheckForUpdatesComplete, requiresSyncBeforeUpdate));
					CheckForUpdatesCallback* ptr3 = ptr2;
				}
				catch
				{
					//try-fault
					global::_003CModule_003E.delete(ptr);
					throw;
				}
				CheckForUpdatesCallback* ptr4 = ptr2;
				if (ptr2 != null)
				{
					global::_003CModule_003E.CComPtrNtv_003CIFirmwareUpdateCallback_003E_002E_003D(&cComPtrNtv_003CIFirmwareUpdateCallback_003E, (IFirmwareUpdateCallback*)ptr2);
				}
				else
				{
					num = -2147024882;
				}
				if (num >= 0)
				{
					IFirmwareUpdater* p = m_spFirmwareUpdater.p;
					IFirmwareUpdateCallback* ptr5 = (IFirmwareUpdateCallback*)(int)(*(uint*)(&cComPtrNtv_003CIFirmwareUpdateCallback_003E));
					num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, int, IFirmwareUpdateCallback*, int>)(int)(*(uint*)(*(int*)p + 20)))((nint)p, startCheckForUpdatesArgs.ForceServerRequest ? 1 : 0, (IFirmwareUpdateCallback*)(int)(*(uint*)(&cComPtrNtv_003CIFirmwareUpdateCallback_003E)));
				}
			}
			finally
			{
				Monitor.Exit(m_FirmwareLock);
			}
			startCheckForUpdatesArgs.HrStatus = num;
			if (num < 0)
			{
				Unsafe.SkipInit(out CComPtrNtv_003CIFirmwareUpdateErrorInfo_003E cComPtrNtv_003CIFirmwareUpdateErrorInfo_003E);
				*(int*)(&cComPtrNtv_003CIFirmwareUpdateErrorInfo_003E) = 0;
				try
				{
					InternalErrorInfo* ptr6 = (InternalErrorInfo*)global::_003CModule_003E.@new(40u);
					InternalErrorInfo* lp;
					try
					{
						lp = ((ptr6 == null) ? null : global::_003CModule_003E.MicrosoftZuneLibrary_002EInternalErrorInfo_002E_007Bctor_007D(ptr6, num));
					}
					catch
					{
						//try-fault
						global::_003CModule_003E.delete(ptr6);
						throw;
					}
					global::_003CModule_003E.CComPtrNtv_003CIFirmwareUpdateErrorInfo_003E_002E_003D(&cComPtrNtv_003CIFirmwareUpdateErrorInfo_003E, (IFirmwareUpdateErrorInfo*)lp);
					startCheckForUpdatesArgs.CheckForUpdatesComplete.Invoke((object)new CheckForUpdatesArgs(new FirmwareUpdateErrorInfo((IFirmwareUpdateErrorInfo*)(int)(*(uint*)(&cComPtrNtv_003CIFirmwareUpdateErrorInfo_003E))), null, RequiresSyncBeforeUpdate()));
				}
				catch
				{
					//try-fault
					global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<CComPtrNtv_003CIFirmwareUpdateErrorInfo_003E*, void>)(&global::_003CModule_003E.CComPtrNtv_003CIFirmwareUpdateErrorInfo_003E_002E_007Bdtor_007D), &cComPtrNtv_003CIFirmwareUpdateErrorInfo_003E);
					throw;
				}
				global::_003CModule_003E.CComPtrNtv_003CIFirmwareUpdateErrorInfo_003E_002ERelease(&cComPtrNtv_003CIFirmwareUpdateErrorInfo_003E);
			}
			if (global::_003CModule_003E.WPP_GLOBAL_Control != Unsafe.AsPointer(ref global::_003CModule_003E.WPP_GLOBAL_Control) && (((int*)global::_003CModule_003E.WPP_GLOBAL_Control)[15] & 4) != 0 && (uint)((byte*)global::_003CModule_003E.WPP_GLOBAL_Control)[57] >= 5u)
			{
				global::_003CModule_003E.WPP_SF_d(((ulong*)global::_003CModule_003E.WPP_GLOBAL_Control)[6], 42, (_GUID*)Unsafe.AsPointer(ref global::_003CModule_003E._003FA0xff79125d_002EWPP_FirmwareUpdateAPI_cpp_Traceguids), num);
			}
		}
		catch
		{
			//try-fault
			global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<CComPtrNtv_003CIFirmwareUpdateCallback_003E*, void>)(&global::_003CModule_003E.CComPtrNtv_003CIFirmwareUpdateCallback_003E_002E_007Bdtor_007D), &cComPtrNtv_003CIFirmwareUpdateCallback_003E);
			throw;
		}
		global::_003CModule_003E.CComPtrNtv_003CIFirmwareUpdateCallback_003E_002ERelease(&cComPtrNtv_003CIFirmwareUpdateCallback_003E);
	}

	private unsafe void StartFirmwareUpdateWorker(object data)
	{
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_004b: Expected O, but got Unknown
		StartFirmwareUpdateArgs startFirmwareUpdateArgs = (StartFirmwareUpdateArgs)data;
		m_spFirmwareMediator.op_Assign(null);
		m_HandlerComplete = startFirmwareUpdateArgs.FirmwareUpdateComplete;
		int num = 0;
		FirmwareUpdateMediator* ptr = (FirmwareUpdateMediator*)global::_003CModule_003E.@new(32u);
		FirmwareUpdateMediator* ptr2;
		try
		{
			ptr2 = ((ptr == null) ? null : global::_003CModule_003E.MicrosoftZuneLibrary_002EFirmwareUpdateMediator_002E_007Bctor_007D(ptr, startFirmwareUpdateArgs.FirmwareUpdateBegin, startFirmwareUpdateArgs.FirmwareUpdateProgress, new DeferredInvokeHandler(base.OnFirmwareProcessCompleteWorker)));
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
			global::_003CModule_003E._ZuneShipAssert(1004u, 1684u);
		}
		Monitor.Enter(m_FirmwareLock);
		try
		{
			if (num >= 0)
			{
				num = EnsureNativeObject();
				if (num >= 0)
				{
					if (startFirmwareUpdateArgs.UpdateOption == FirmwareUpdateOption.None)
					{
						IFirmwareUpdater* p = m_spFirmwareUpdater.p;
						FirmwareUpdateMediator* p2 = m_spFirmwareMediator.p;
						num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, IFirmwareUpdateCollection*, IFirmwareUpdateCallback*, int>)(int)(*(uint*)(*(int*)p + 24)))((nint)p, startFirmwareUpdateArgs.Updates.NativeCollectionPtr, (IFirmwareUpdateCallback*)p2);
					}
					else
					{
						Unsafe.SkipInit(out CComPtrNtv_003CIFirmwareUpdater2_003E cComPtrNtv_003CIFirmwareUpdater2_003E);
						*(int*)(&cComPtrNtv_003CIFirmwareUpdater2_003E) = 0;
						try
						{
							num = m_spFirmwareUpdater.QueryInterface_003CIFirmwareUpdater2_003E((IFirmwareUpdater2**)(&cComPtrNtv_003CIFirmwareUpdater2_003E));
							if (num >= 0)
							{
								FirmwareUpdateMediator* p3 = m_spFirmwareMediator.p;
								num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, IFirmwareUpdateCollection*, IFirmwareUpdateCallback*, EFirmwareUpdateOption, int>)(int)(*(uint*)(*(int*)(int)(*(uint*)(&cComPtrNtv_003CIFirmwareUpdater2_003E)) + 40)))((IntPtr)(*(int*)(&cComPtrNtv_003CIFirmwareUpdater2_003E)), startFirmwareUpdateArgs.Updates.NativeCollectionPtr, (IFirmwareUpdateCallback*)p3, (EFirmwareUpdateOption)startFirmwareUpdateArgs.UpdateOption);
							}
						}
						catch
						{
							//try-fault
							global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<CComPtrNtv_003CIFirmwareUpdater2_003E*, void>)(&global::_003CModule_003E.CComPtrNtv_003CIFirmwareUpdater2_003E_002E_007Bdtor_007D), &cComPtrNtv_003CIFirmwareUpdater2_003E);
							throw;
						}
						global::_003CModule_003E.CComPtrNtv_003CIFirmwareUpdater2_003E_002ERelease(&cComPtrNtv_003CIFirmwareUpdater2_003E);
					}
				}
			}
		}
		finally
		{
			Monitor.Exit(m_FirmwareLock);
		}
		startFirmwareUpdateArgs.HrStatus = num;
		if (num < 0)
		{
			Reset(deviceRebooting: false);
			SendCompleteNotification(num, disconnectDeviceOnComplete: false);
		}
		if (global::_003CModule_003E.WPP_GLOBAL_Control != Unsafe.AsPointer(ref global::_003CModule_003E.WPP_GLOBAL_Control) && (((int*)global::_003CModule_003E.WPP_GLOBAL_Control)[15] & 4) != 0 && (uint)((byte*)global::_003CModule_003E.WPP_GLOBAL_Control)[57] >= 5u)
		{
			global::_003CModule_003E.WPP_SF_d(((ulong*)global::_003CModule_003E.WPP_GLOBAL_Control)[6], 46, (_GUID*)Unsafe.AsPointer(ref global::_003CModule_003E._003FA0xff79125d_002EWPP_FirmwareUpdateAPI_cpp_Traceguids), num);
		}
	}

	private unsafe void StartCheckForDiskSpaceWorker(object data)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0008: Expected O, but got Unknown
		DeferredInvokeHandler val = (DeferredInvokeHandler)data;
		int num = 0;
		ulong additionalSpaceRequiredInBytes = 0uL;
		ulong totalSpaceRequiredInBytes = 0uL;
		string driveLetter = null;
		if (Application.IsApplicationThread)
		{
			global::_003CModule_003E._ZuneShipAssert(1004u, 1779u);
		}
		Monitor.Enter(m_FirmwareLock);
		if (global::_003CModule_003E.WPP_GLOBAL_Control != Unsafe.AsPointer(ref global::_003CModule_003E.WPP_GLOBAL_Control) && (((int*)global::_003CModule_003E.WPP_GLOBAL_Control)[15] & 4) != 0 && (uint)((byte*)global::_003CModule_003E.WPP_GLOBAL_Control)[57] >= 5u)
		{
			global::_003CModule_003E.WPP_SF_(((ulong*)global::_003CModule_003E.WPP_GLOBAL_Control)[6], 49, (_GUID*)Unsafe.AsPointer(ref global::_003CModule_003E._003FA0xff79125d_002EWPP_FirmwareUpdateAPI_cpp_Traceguids));
		}
		try
		{
			Unsafe.SkipInit(out WBSTRString wBSTRString);
			global::_003CModule_003E.WBSTRString_002E_007Bctor_007D(&wBSTRString);
			try
			{
				Unsafe.SkipInit(out CComPtrNtv_003CIFirmwareUpdater2_003E cComPtrNtv_003CIFirmwareUpdater2_003E);
				*(int*)(&cComPtrNtv_003CIFirmwareUpdater2_003E) = 0;
				try
				{
					num = EnsureNativeObject();
					if (num >= 0)
					{
						num = m_spFirmwareUpdater.QueryInterface_003CIFirmwareUpdater2_003E((IFirmwareUpdater2**)(&cComPtrNtv_003CIFirmwareUpdater2_003E));
						if (num >= 0)
						{
							num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, ulong*, ulong*, ushort**, int>)(int)(*(uint*)(*(int*)(int)(*(uint*)(&cComPtrNtv_003CIFirmwareUpdater2_003E)) + 44)))((IntPtr)(*(int*)(&cComPtrNtv_003CIFirmwareUpdater2_003E)), &additionalSpaceRequiredInBytes, &totalSpaceRequiredInBytes, (ushort**)(&wBSTRString));
							if (num >= 0)
							{
								ushort* ptr = (ushort*)(int)(*(uint*)(&wBSTRString));
								driveLetter = new string((char*)(int)(*(uint*)(&wBSTRString)));
							}
						}
					}
				}
				catch
				{
					//try-fault
					global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<CComPtrNtv_003CIFirmwareUpdater2_003E*, void>)(&global::_003CModule_003E.CComPtrNtv_003CIFirmwareUpdater2_003E_002E_007Bdtor_007D), &cComPtrNtv_003CIFirmwareUpdater2_003E);
					throw;
				}
				global::_003CModule_003E.CComPtrNtv_003CIFirmwareUpdater2_003E_002ERelease(&cComPtrNtv_003CIFirmwareUpdater2_003E);
			}
			catch
			{
				//try-fault
				global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<WBSTRString*, void>)(&global::_003CModule_003E.WBSTRString_002E_007Bdtor_007D), &wBSTRString);
				throw;
			}
			global::_003CModule_003E.WBSTRString_002E_007Bdtor_007D(&wBSTRString);
		}
		finally
		{
			Monitor.Exit(m_FirmwareLock);
		}
		CheckDiskSpaceArgs checkDiskSpaceArgs = new CheckDiskSpaceArgs(num, additionalSpaceRequiredInBytes, totalSpaceRequiredInBytes, driveLetter);
		val.Invoke((object)checkDiskSpaceArgs);
		if (global::_003CModule_003E.WPP_GLOBAL_Control != Unsafe.AsPointer(ref global::_003CModule_003E.WPP_GLOBAL_Control) && (((int*)global::_003CModule_003E.WPP_GLOBAL_Control)[15] & 4) != 0 && (uint)((byte*)global::_003CModule_003E.WPP_GLOBAL_Control)[57] >= 5u)
		{
			global::_003CModule_003E.WPP_SF_d(((ulong*)global::_003CModule_003E.WPP_GLOBAL_Control)[6], 50, (_GUID*)Unsafe.AsPointer(ref global::_003CModule_003E._003FA0xff79125d_002EWPP_FirmwareUpdateAPI_cpp_Traceguids), num);
		}
	}

	private unsafe void CancelWorker(object data)
	{
		if (Application.IsApplicationThread)
		{
			global::_003CModule_003E._ZuneShipAssert(1004u, 1968u);
		}
		Monitor.Enter(m_FirmwareLock);
		try
		{
			CComPtrMgd_003CMicrosoftZuneLibrary_003A_003AFirmwareUpdateMediator_003E spFirmwareMediator = m_spFirmwareMediator;
			if (spFirmwareMediator.p != null)
			{
				global::_003CModule_003E.MicrosoftZuneLibrary_002EFirmwareUpdateMediator_002EFirmwareProcessCanceled(spFirmwareMediator.p);
			}
			CComPtrMgd_003CIFirmwareUpdater_003E spFirmwareUpdater = m_spFirmwareUpdater;
			if (spFirmwareUpdater.p != null)
			{
				IFirmwareUpdater* p = spFirmwareUpdater.p;
				if (((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, int>)(int)(*(uint*)(*(int*)p + 32)))((nint)p) < 0)
				{
					global::_003CModule_003E._ZuneShipAssert(1004u, 1983u);
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
			global::_003CModule_003E.WPP_SF_(((ulong*)global::_003CModule_003E.WPP_GLOBAL_Control)[6], 58, (_GUID*)Unsafe.AsPointer(ref global::_003CModule_003E._003FA0xff79125d_002EWPP_FirmwareUpdateAPI_cpp_Traceguids));
		}
	}

	protected override void Dispose([MarshalAs(UnmanagedType.U1)] bool P_0)
	{
		if (P_0)
		{
			try
			{
				_007EFirmwareUpdater();
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
					((IDisposable)m_spFirmwareUpdater).Dispose();
				}
			}
		}
		base.Dispose(false);
	}
}
