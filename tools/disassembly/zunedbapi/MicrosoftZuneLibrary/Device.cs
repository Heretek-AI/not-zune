using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;
using System.Threading;
using _003CCppImplementationDetails_003E;
using DataStructs;

namespace MicrosoftZuneLibrary;

public class Device : IDisposable
{
	private bool m_fInitializationCompleted;

	private object m_Lock;

	private readonly CComPtrMgd_003CIEndpointHost_003E m_spEndpointHost;

	private readonly CComPtrMgd_003CISyncEngine_003E m_spSyncEngine;

	private readonly CComPtrMgd_003CIWlanProvider_003E m_spWlanProvider;

	private readonly CComPtrMgd_003CDeviceMediator_003E m_spDeviceMediator;

	private SyncBeganHandler m_SyncBegan;

	private SyncProgressedHandler m_SyncProgressed;

	private SyncCompletedHandler m_SyncCompleted;

	private FriendlyNameChangedHandler m_FriendlyNameChanged;

	private DeviceStatusChangedHandler m_DeviceStatusChanged;

	private FormatCompleteHandler m_FormatComplete;

	private GetWlanProfilesCompleteHandler m_GetWlanProfilesComplete;

	private GetDeviceWlanNetworksCompleteHandler m_GetDeviceWlanNetworksComplete;

	private GetDeviceWlanProfilesCompleteHandler m_GetDeviceWlanProfilesComplete;

	private SetDeviceWlanProfilesCompleteHandler m_SetDeviceWlanProfilesComplete;

	private AssociateWlanDeviceCompleteHandler m_AssociateWlanDeviceComplete;

	private UnassociateWlanDeviceCompleteHandler m_UnassociateWlanDeviceComplete;

	private TestDeviceWlanCompleteHandler m_TestDeviceWlanComplete;

	private SyncRules m_syncRules;

	private FirmwareUpdater m_firmwareUpdater;

	private GasGauge m_predictedGasGauge;

	private GasGauge m_actualGasGauge;

	private bool m_fClientUpdateRequired;

	private bool m_fFirmwareUpdateRequired;

	private int m_hrEnumeration;

	private DeviceAssetSet m_DeviceAssetSet;

	private int m_iDeviceID;

	private uint m_dwModelID;

	private string m_strIconPath;

	private string m_strEndpointId;

	private string m_strCanonicalName;

	private bool m_fFirmwareUpdateSupported;

	private EEndpointStatus m_eLastDeviceStatus;

	public unsafe string MyPhoneDeviceID
	{
		get
		{
			string result = null;
			CComPtrMgd_003CIEndpointHost_003E spEndpointHost = m_spEndpointHost;
			if (spEndpointHost.p != null)
			{
				ushort* ptr = null;
				IEndpointHost* p = spEndpointHost.p;
				if (((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, EEndpointHostProperty, ushort**, int>)(int)(*(uint*)(*(int*)p + 60)))((nint)p, EEndpointHostProperty.eEndpointHostPropertyMyPhoneDeviceId, &ptr) >= 0 && ptr != null)
				{
					result = new string((char*)ptr);
					global::_003CModule_003E.SysFreeString(ptr);
				}
			}
			return result;
		}
	}

	public unsafe bool InStandardMode
	{
		[return: MarshalAs(UnmanagedType.U1)]
		get
		{
			bool result = false;
			IEndpointHost* p = m_spEndpointHost.p;
			if (p != null)
			{
				((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, EEndpointHostProperty, bool*, int>)(int)(*(uint*)(*(int*)p + 76)))((nint)p, EEndpointHostProperty.eEndpointHostPropertyIsInStandardMode, &result);
			}
			return result;
		}
	}

	public DeviceAssetSet DeviceAssetSet => m_DeviceAssetSet;

	public unsafe int LastFirmwareUpdateError
	{
		get
		{
			int result = 0;
			IEndpointHost* p = m_spEndpointHost.p;
			if (p == null)
			{
				global::_003CModule_003E._ZuneShipAssert(1002u, 3517u);
				return -2147418113;
			}
			((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, EEndpointHostProperty, int*, int>)(int)(*(uint*)(*(int*)p + 68)))((nint)p, EEndpointHostProperty.eEndpointHostPropertyLastFirmwareUpdateError, &result);
			return result;
		}
	}

	public FirmwareUpdater FirmwareUpdater => m_firmwareUpdater;

	public unsafe string PicturesVideosViewUrl
	{
		get
		{
			string result = null;
			CComPtrMgd_003CIEndpointHost_003E spEndpointHost = m_spEndpointHost;
			if (spEndpointHost.p != null)
			{
				ushort* ptr = null;
				IEndpointHost* p = spEndpointHost.p;
				if (((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, EEndpointHostProperty, ushort**, int>)(int)(*(uint*)(*(int*)p + 60)))((nint)p, EEndpointHostProperty.eEndpointHostPropertyPicturesVideosViewUrl, &ptr) >= 0 && ptr != null)
				{
					result = new string((char*)ptr);
					global::_003CModule_003E.SysFreeString(ptr);
				}
			}
			return result;
		}
	}

	public unsafe string PicturesVideosViewText
	{
		get
		{
			string result = null;
			CComPtrMgd_003CIEndpointHost_003E spEndpointHost = m_spEndpointHost;
			if (spEndpointHost.p != null)
			{
				ushort* ptr = null;
				IEndpointHost* p = spEndpointHost.p;
				if (((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, EEndpointHostProperty, ushort**, int>)(int)(*(uint*)(*(int*)p + 60)))((nint)p, EEndpointHostProperty.eEndpointHostPropertyPicturesVideosViewText, &ptr) >= 0 && ptr != null)
				{
					result = new string((char*)ptr);
					global::_003CModule_003E.SysFreeString(ptr);
				}
			}
			return result;
		}
	}

	public bool SyncSetupRequired
	{
		[return: MarshalAs(UnmanagedType.U1)]
		get
		{
			ESyncRelationship relationship = ESyncRelationship.srNone;
			if (GetSyncRelationship(ref relationship) >= 0 && (ESyncRelationship.srNone == relationship || ESyncRelationship.srSyncWithOtherMachine == relationship))
			{
				return true;
			}
			return false;
		}
	}

	public bool ClientUpdateRequired
	{
		[return: MarshalAs(UnmanagedType.U1)]
		get
		{
			return m_fClientUpdateRequired;
		}
	}

	public bool FirmwareUpdateRequired
	{
		[return: MarshalAs(UnmanagedType.U1)]
		get
		{
			return m_fFirmwareUpdateRequired;
		}
	}

	public GasGauge ActualGasGauge => m_actualGasGauge;

	public GasGauge PredictedGasGauge => m_predictedGasGauge;

	public SyncRules Rules => m_syncRules;

	public unsafe DateTime LastConnectTime
	{
		get
		{
			DateTime result = DateTime.Now;
			CComPtrMgd_003CIEndpointHost_003E spEndpointHost = m_spEndpointHost;
			if (spEndpointHost.p != null)
			{
				IEndpointHost* p = spEndpointHost.p;
				Unsafe.SkipInit(out _FILETIME fILETIME);
				if (((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, EEndpointHostProperty, _FILETIME*, int>)(int)(*(uint*)(*(int*)p + 44)))((nint)p, EEndpointHostProperty.eEndpointHostPropertyLastConnectTime, &fILETIME) >= 0)
				{
					long fileTime = (long)(uint)Unsafe.As<_FILETIME, int>(ref Unsafe.AddByteOffset(ref fILETIME, 4)) * 4294967296L + (uint)(*(int*)(&fILETIME));
					try
					{
						result = DateTime.FromFileTime(fileTime);
					}
					catch (ArgumentOutOfRangeException)
					{
					}
				}
			}
			return result;
		}
	}

	public unsafe DateTime LastSyncTime
	{
		get
		{
			DateTime result = DateTime.Now;
			CComPtrMgd_003CIEndpointHost_003E spEndpointHost = m_spEndpointHost;
			if (spEndpointHost.p != null)
			{
				IEndpointHost* p = spEndpointHost.p;
				Unsafe.SkipInit(out _FILETIME fILETIME);
				if (((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, EEndpointHostProperty, _FILETIME*, int>)(int)(*(uint*)(*(int*)p + 44)))((nint)p, EEndpointHostProperty.eEndpointHostPropertyLastSyncTime, &fILETIME) >= 0)
				{
					long fileTime = (long)(uint)Unsafe.As<_FILETIME, int>(ref Unsafe.AddByteOffset(ref fILETIME, 4)) * 4294967296L + (uint)(*(int*)(&fILETIME));
					try
					{
						result = DateTime.FromFileTime(fileTime);
					}
					catch (ArgumentOutOfRangeException)
					{
					}
				}
			}
			return result;
		}
	}

	public unsafe bool IsFormatting
	{
		[return: MarshalAs(UnmanagedType.U1)]
		get
		{
			bool result = false;
			IEndpointHost* p = m_spEndpointHost.p;
			if (p != null)
			{
				((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, EEndpointHostProperty, bool*, int>)(int)(*(uint*)(*(int*)p + 76)))((nint)p, EEndpointHostProperty.eEndpointHostPropertyIsFormatting, &result);
			}
			return result;
		}
	}

	public unsafe bool IsConnectedWirelessly
	{
		[return: MarshalAs(UnmanagedType.U1)]
		get
		{
			bool result = false;
			IEndpointHost* p = m_spEndpointHost.p;
			if (p != null)
			{
				((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, EEndpointHostProperty, bool*, int>)(int)(*(uint*)(*(int*)p + 76)))((nint)p, EEndpointHostProperty.eEndpointHostPropertyIsConnectedWirelessly, &result);
			}
			return result;
		}
	}

	public unsafe bool IsConnected
	{
		[return: MarshalAs(UnmanagedType.U1)]
		get
		{
			bool result = false;
			IEndpointHost* p = m_spEndpointHost.p;
			if (p != null)
			{
				((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, EEndpointHostProperty, bool*, int>)(int)(*(uint*)(*(int*)p + 76)))((nint)p, EEndpointHostProperty.eEndpointHostPropertyIsConnected, &result);
			}
			return result;
		}
	}

	public unsafe bool IsAvailable
	{
		[return: MarshalAs(UnmanagedType.U1)]
		get
		{
			bool result = false;
			IEndpointHost* p = m_spEndpointHost.p;
			if (p != null)
			{
				((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, EEndpointHostProperty, bool*, int>)(int)(*(uint*)(*(int*)p + 76)))((nint)p, EEndpointHostProperty.eEndpointHostPropertyIsAvailable, &result);
			}
			return result;
		}
	}

	public unsafe EEndpointStatus DeviceStatus
	{
		get
		{
			int result = 1;
			if (m_spEndpointHost.p != null)
			{
				result = (int)m_eLastDeviceStatus;
			}
			return (EEndpointStatus)result;
		}
	}

	public unsafe string OwnerApplicationName
	{
		get
		{
			string result = string.Empty;
			CComPtrMgd_003CIEndpointHost_003E spEndpointHost = m_spEndpointHost;
			if (spEndpointHost.p != null)
			{
				ushort* ptr = null;
				IEndpointHost* p = spEndpointHost.p;
				if (((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, EEndpointHostProperty, ushort**, int>)(int)(*(uint*)(*(int*)p + 60)))((nint)p, EEndpointHostProperty.eEndpointHostPropertyOwnerApplicationName, &ptr) >= 0 && ptr != null)
				{
					result = new string((char*)ptr);
					global::_003CModule_003E.SysFreeString(ptr);
				}
			}
			return result;
		}
	}

	public unsafe bool IsReady
	{
		[return: MarshalAs(UnmanagedType.U1)]
		get
		{
			bool flag = false;
			IEndpointHost* p = m_spEndpointHost.p;
			if (p != null)
			{
				int num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, EEndpointHostProperty, bool*, int>)(int)(*(uint*)(*(int*)p + 76)))((nint)p, EEndpointHostProperty.eEndpointHostPropertyIsReady, &flag);
				int num2 = ((flag && num >= 0 && m_hrEnumeration >= 0) ? 1 : 0);
				flag = (byte)num2 != 0;
			}
			return flag;
		}
	}

	public unsafe bool IsSyncSuspended
	{
		[return: MarshalAs(UnmanagedType.U1)]
		get
		{
			ISyncEngine* p = m_spSyncEngine.p;
			if (p != null)
			{
				return (((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, int>)(int)(*(uint*)(*(int*)p + 40)))((nint)p) != 0) ? true : false;
			}
			return false;
		}
	}

	public unsafe bool IsSyncRunning
	{
		[return: MarshalAs(UnmanagedType.U1)]
		get
		{
			bool result = false;
			IEndpointHost* p = m_spEndpointHost.p;
			if (p != null)
			{
				((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, EEndpointHostProperty, bool*, int>)(int)(*(uint*)(*(int*)p + 76)))((nint)p, EEndpointHostProperty.eEndpointHostPropertySyncing, &result);
			}
			return result;
		}
	}

	public unsafe ulong DeviceCapacity
	{
		get
		{
			ulong result = 0uL;
			IEndpointHost* p = m_spEndpointHost.p;
			((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, EEndpointHostProperty, ulong*, int>)(int)(*(uint*)(*(int*)p + 64)))((nint)p, EEndpointHostProperty.eEndpointHostPropertyTotalSpace, &result);
			return result;
		}
	}

	public string CanonicalName => m_strCanonicalName;

	public string EndpointId => m_strEndpointId;

	public unsafe uint StatedCapacity
	{
		get
		{
			uint result = 0u;
			IEndpointHost* p = m_spEndpointHost.p;
			if (p != null)
			{
				((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, EEndpointHostProperty, uint*, int>)(int)(*(uint*)(*(int*)p + 72)))((nint)p, EEndpointHostProperty.eEndpointHostPropertyStatedCapacity, &result);
			}
			return result;
		}
	}

	public unsafe uint BackgroundID
	{
		get
		{
			uint result = 0u;
			IEndpointHost* p = m_spEndpointHost.p;
			if (p != null)
			{
				((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, EEndpointHostProperty, uint*, int>)(int)(*(uint*)(*(int*)p + 72)))((nint)p, EEndpointHostProperty.eEndpointHostPropertyBackgroundId, &result);
			}
			return result;
		}
	}

	public unsafe uint TattooID
	{
		get
		{
			uint result = 0u;
			IEndpointHost* p = m_spEndpointHost.p;
			if (p != null)
			{
				((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, EEndpointHostProperty, uint*, int>)(int)(*(uint*)(*(int*)p + 72)))((nint)p, EEndpointHostProperty.eEndpointHostPropertyTattooId, &result);
			}
			return result;
		}
	}

	public unsafe ulong ColorID
	{
		get
		{
			ulong result = 0uL;
			IEndpointHost* p = m_spEndpointHost.p;
			if (p != null)
			{
				((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, EEndpointHostProperty, ulong*, int>)(int)(*(uint*)(*(int*)p + 64)))((nint)p, EEndpointHostProperty.eEndpointHostPropertyColorId, &result);
			}
			return result;
		}
	}

	public unsafe uint PrimaryColorID
	{
		get
		{
			uint result = 0u;
			IEndpointHost* p = m_spEndpointHost.p;
			if (p != null)
			{
				((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, EEndpointHostProperty, uint*, int>)(int)(*(uint*)(*(int*)p + 72)))((nint)p, EEndpointHostProperty.eEndpointHostPropertyPrimaryColorId, &result);
			}
			return result;
		}
	}

	public unsafe uint FamilyID
	{
		get
		{
			uint result = 0u;
			IEndpointHost* p = m_spEndpointHost.p;
			if (p != null)
			{
				((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, EEndpointHostProperty, uint*, int>)(int)(*(uint*)(*(int*)p + 72)))((nint)p, EEndpointHostProperty.eEndpointHostPropertyFamilyId, &result);
			}
			return result;
		}
	}

	public unsafe int ClassID
	{
		get
		{
			int result = 0;
			IEndpointHost* p = m_spEndpointHost.p;
			if (p != null)
			{
				((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, EEndpointHostProperty, int*, int>)(int)(*(uint*)(*(int*)p + 68)))((nint)p, EEndpointHostProperty.eEndpointHostPropertyClassId, &result);
			}
			return result;
		}
	}

	public int DeviceID => m_iDeviceID;

	[SpecialName]
	public virtual event TestDeviceWlanCompleteHandler TestDeviceWlanCompleteEvent
	{
		add
		{
			m_TestDeviceWlanComplete = (TestDeviceWlanCompleteHandler)Delegate.Combine(m_TestDeviceWlanComplete, value);
		}
		remove
		{
			m_TestDeviceWlanComplete = (TestDeviceWlanCompleteHandler)Delegate.Remove(m_TestDeviceWlanComplete, value);
		}
	}

	[SpecialName]
	public virtual event UnassociateWlanDeviceCompleteHandler UnassociateWlanDeviceCompleteEvent
	{
		add
		{
			m_UnassociateWlanDeviceComplete = (UnassociateWlanDeviceCompleteHandler)Delegate.Combine(m_UnassociateWlanDeviceComplete, value);
		}
		remove
		{
			m_UnassociateWlanDeviceComplete = (UnassociateWlanDeviceCompleteHandler)Delegate.Remove(m_UnassociateWlanDeviceComplete, value);
		}
	}

	[SpecialName]
	public virtual event AssociateWlanDeviceCompleteHandler AssociateWlanDeviceCompleteEvent
	{
		add
		{
			m_AssociateWlanDeviceComplete = (AssociateWlanDeviceCompleteHandler)Delegate.Combine(m_AssociateWlanDeviceComplete, value);
		}
		remove
		{
			m_AssociateWlanDeviceComplete = (AssociateWlanDeviceCompleteHandler)Delegate.Remove(m_AssociateWlanDeviceComplete, value);
		}
	}

	[SpecialName]
	public virtual event SetDeviceWlanProfilesCompleteHandler SetDeviceWlanProfilesCompleteEvent
	{
		add
		{
			m_SetDeviceWlanProfilesComplete = (SetDeviceWlanProfilesCompleteHandler)Delegate.Combine(m_SetDeviceWlanProfilesComplete, value);
		}
		remove
		{
			m_SetDeviceWlanProfilesComplete = (SetDeviceWlanProfilesCompleteHandler)Delegate.Remove(m_SetDeviceWlanProfilesComplete, value);
		}
	}

	[SpecialName]
	public virtual event GetDeviceWlanProfilesCompleteHandler GetDeviceWlanProfilesCompleteEvent
	{
		add
		{
			m_GetDeviceWlanProfilesComplete = (GetDeviceWlanProfilesCompleteHandler)Delegate.Combine(m_GetDeviceWlanProfilesComplete, value);
		}
		remove
		{
			m_GetDeviceWlanProfilesComplete = (GetDeviceWlanProfilesCompleteHandler)Delegate.Remove(m_GetDeviceWlanProfilesComplete, value);
		}
	}

	[SpecialName]
	public virtual event GetDeviceWlanNetworksCompleteHandler GetDeviceWlanNetworksCompleteEvent
	{
		add
		{
			m_GetDeviceWlanNetworksComplete = (GetDeviceWlanNetworksCompleteHandler)Delegate.Combine(m_GetDeviceWlanNetworksComplete, value);
		}
		remove
		{
			m_GetDeviceWlanNetworksComplete = (GetDeviceWlanNetworksCompleteHandler)Delegate.Remove(m_GetDeviceWlanNetworksComplete, value);
		}
	}

	[SpecialName]
	public virtual event GetWlanProfilesCompleteHandler GetWlanProfilesCompleteEvent
	{
		add
		{
			m_GetWlanProfilesComplete = (GetWlanProfilesCompleteHandler)Delegate.Combine(m_GetWlanProfilesComplete, value);
		}
		remove
		{
			m_GetWlanProfilesComplete = (GetWlanProfilesCompleteHandler)Delegate.Remove(m_GetWlanProfilesComplete, value);
		}
	}

	[SpecialName]
	public virtual event FormatCompleteHandler FormatCompleteEvent
	{
		add
		{
			m_FormatComplete = (FormatCompleteHandler)Delegate.Combine(m_FormatComplete, value);
		}
		remove
		{
			m_FormatComplete = (FormatCompleteHandler)Delegate.Remove(m_FormatComplete, value);
		}
	}

	[SpecialName]
	public virtual event DeviceStatusChangedHandler DeviceStatusChangedEvent
	{
		add
		{
			m_DeviceStatusChanged = (DeviceStatusChangedHandler)Delegate.Combine(m_DeviceStatusChanged, value);
		}
		remove
		{
			m_DeviceStatusChanged = (DeviceStatusChangedHandler)Delegate.Remove(m_DeviceStatusChanged, value);
		}
	}

	[SpecialName]
	public virtual event FriendlyNameChangedHandler FriendlyNameChangedEvent
	{
		add
		{
			m_FriendlyNameChanged = (FriendlyNameChangedHandler)Delegate.Combine(m_FriendlyNameChanged, value);
		}
		remove
		{
			m_FriendlyNameChanged = (FriendlyNameChangedHandler)Delegate.Remove(m_FriendlyNameChanged, value);
		}
	}

	[SpecialName]
	public virtual event SyncCompletedHandler SyncCompleted
	{
		add
		{
			m_SyncCompleted = (SyncCompletedHandler)Delegate.Combine(m_SyncCompleted, value);
		}
		remove
		{
			m_SyncCompleted = (SyncCompletedHandler)Delegate.Remove(m_SyncCompleted, value);
		}
	}

	[SpecialName]
	public virtual event SyncProgressedHandler SyncProgressed
	{
		add
		{
			m_SyncProgressed = (SyncProgressedHandler)Delegate.Combine(m_SyncProgressed, value);
		}
		remove
		{
			m_SyncProgressed = (SyncProgressedHandler)Delegate.Remove(m_SyncProgressed, value);
		}
	}

	[SpecialName]
	public virtual event SyncBeganHandler SyncBegan
	{
		add
		{
			m_SyncBegan = (SyncBeganHandler)Delegate.Combine(m_SyncBegan, value);
		}
		remove
		{
			m_SyncBegan = (SyncBeganHandler)Delegate.Remove(m_SyncBegan, value);
		}
	}

	private unsafe void _007EDevice()
	{
		if (global::_003CModule_003E.WPP_GLOBAL_Control != Unsafe.AsPointer(ref global::_003CModule_003E.WPP_GLOBAL_Control) && (((int*)global::_003CModule_003E.WPP_GLOBAL_Control)[15] & 2) != 0 && (uint)((byte*)global::_003CModule_003E.WPP_GLOBAL_Control)[57] >= 5u)
		{
			global::_003CModule_003E.WPP_SF_d(((ulong*)global::_003CModule_003E.WPP_GLOBAL_Control)[6], 12, (_GUID*)Unsafe.AsPointer(ref global::_003CModule_003E._003FA0xbf2368f8_002EWPP_DeviceAPI_cpp_Traceguids), m_iDeviceID);
		}
		DeviceMediator* p = m_spDeviceMediator.p;
		if (p != null)
		{
			global::_003CModule_003E.DeviceMediator_002EShutdown(p);
		}
		m_spEndpointHost.Release();
		m_spSyncEngine.Release();
		m_spDeviceMediator.Release();
		m_spWlanProvider.Release();
		SyncRules syncRules = m_syncRules;
		if (syncRules != null)
		{
			((IDisposable)syncRules).Dispose();
			m_syncRules = null;
		}
		FirmwareUpdater firmwareUpdater = m_firmwareUpdater;
		if (firmwareUpdater != null)
		{
			((IDisposable)firmwareUpdater).Dispose();
			m_firmwareUpdater = null;
		}
		GasGauge actualGasGauge = m_actualGasGauge;
		if (actualGasGauge != null)
		{
			((IDisposable)actualGasGauge).Dispose();
			m_actualGasGauge = null;
		}
		GasGauge predictedGasGauge = m_predictedGasGauge;
		if (predictedGasGauge != null)
		{
			((IDisposable)predictedGasGauge).Dispose();
			m_predictedGasGauge = null;
		}
		if (global::_003CModule_003E.WPP_GLOBAL_Control != Unsafe.AsPointer(ref global::_003CModule_003E.WPP_GLOBAL_Control) && (((int*)global::_003CModule_003E.WPP_GLOBAL_Control)[15] & 2) != 0 && (uint)((byte*)global::_003CModule_003E.WPP_GLOBAL_Control)[57] >= 5u)
		{
			global::_003CModule_003E.WPP_SF_(((ulong*)global::_003CModule_003E.WPP_GLOBAL_Control)[6], 13, (_GUID*)Unsafe.AsPointer(ref global::_003CModule_003E._003FA0xbf2368f8_002EWPP_DeviceAPI_cpp_Traceguids));
		}
	}

	public unsafe int StartSync()
	{
		IEndpointHost* p = m_spEndpointHost.p;
		if (p == null)
		{
			global::_003CModule_003E._ZuneShipAssert(1002u, 723u);
			return -2147418113;
		}
		return ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, EEndpointHostAction, int>)(int)(*(uint*)(*(int*)p + 148)))((nint)p, EEndpointHostAction.eEndpointHostActionStartSync);
	}

	public unsafe int StartSyncNextNotify()
	{
		IEndpointHost* p = m_spEndpointHost.p;
		if (p == null)
		{
			global::_003CModule_003E._ZuneShipAssert(1002u, 732u);
			return -2147418113;
		}
		return ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, EEndpointHostAction, int>)(int)(*(uint*)(*(int*)p + 148)))((nint)p, EEndpointHostAction.eEndpointHostActionStartSyncNextNotify);
	}

	public unsafe int StopSync()
	{
		IEndpointHost* p = m_spEndpointHost.p;
		if (p == null)
		{
			global::_003CModule_003E._ZuneShipAssert(1002u, 741u);
			return -2147418113;
		}
		((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, EEndpointHostAction, int>)(int)(*(uint*)(*(int*)p + 148)))((nint)p, EEndpointHostAction.eEndpointHostActionCancelSync);
		return 0;
	}

	public unsafe int StartEnumeration()
	{
		IEndpointHost* p = m_spEndpointHost.p;
		if (p == null)
		{
			global::_003CModule_003E._ZuneShipAssert(1002u, 751u);
			return -2147418113;
		}
		IEndpointHost* ptr = p;
		return ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, EEndpointHostAction, int>)(int)(*(uint*)(*(int*)ptr + 148)))((nint)ptr, EEndpointHostAction.eEndpointHostActionIngestMetadataAsync);
	}

	public unsafe int Format()
	{
		IEndpointHost* p = m_spEndpointHost.p;
		if (p == null)
		{
			global::_003CModule_003E._ZuneShipAssert(1002u, 764u);
			return -2147418113;
		}
		DeviceMediator* p2 = m_spDeviceMediator.p;
		DeviceMediator* ptr = (DeviceMediator*)((p2 == null) ? null : ((byte*)p2 + 12));
		IEndpointHost* ptr2 = p;
		return ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, EEndpointHostAction, IUnknown*, int>)(int)(*(uint*)(*(int*)ptr2 + 152)))((nint)ptr2, EEndpointHostAction.eEndpointHostActionFormat, (IUnknown*)ptr);
	}

	public unsafe int GetFriendlyName(ref string strName)
	{
		CComPtrMgd_003CIEndpointHost_003E spEndpointHost = m_spEndpointHost;
		if (spEndpointHost.p == null)
		{
			global::_003CModule_003E._ZuneShipAssert(1002u, 777u);
			return -2147418113;
		}
		ushort* ptr = null;
		IEndpointHost* p = spEndpointHost.p;
		int num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, EEndpointHostProperty, ushort**, int>)(int)(*(uint*)(*(int*)p + 60)))((nint)p, EEndpointHostProperty.eEndpointHostPropertyName, &ptr);
		if (num >= 0 && ptr != null)
		{
			strName = new string((char*)ptr);
			global::_003CModule_003E.SysFreeString(ptr);
		}
		return num;
	}

	public unsafe int SetFriendlyName(string strName)
	{
		if (m_spEndpointHost.p == null)
		{
			global::_003CModule_003E._ZuneShipAssert(1002u, 798u);
			return -2147418113;
		}
		fixed (ushort* ptr = &Unsafe.As<char, ushort>(ref global::_003CModule_003E.PtrToStringChars(strName)))
		{
			int result;
			if (ptr != null)
			{
				IEndpointHost* p = m_spEndpointHost.p;
				int num = *(int*)p + 92;
				result = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, EEndpointHostProperty, ushort*, int>)(int)(*(uint*)num))((nint)p, EEndpointHostProperty.eEndpointHostPropertyName, ptr);
			}
			else
			{
				result = -2147418113;
			}
			return result;
		}
	}

	public unsafe int GetManufacturer(ref string strManufacturer)
	{
		CComPtrMgd_003CIEndpointHost_003E spEndpointHost = m_spEndpointHost;
		if (spEndpointHost.p == null)
		{
			global::_003CModule_003E._ZuneShipAssert(1002u, 821u);
			return -2147418113;
		}
		ushort* ptr = null;
		IEndpointHost* p = spEndpointHost.p;
		int num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, EEndpointHostProperty, ushort**, int>)(int)(*(uint*)(*(int*)p + 60)))((nint)p, EEndpointHostProperty.eEndpointHostPropertyManufacturer, &ptr);
		if (num >= 0 && ptr != null)
		{
			strManufacturer = new string((char*)ptr);
			global::_003CModule_003E.SysFreeString(ptr);
		}
		return num;
	}

	public unsafe int GetModelName(ref string strModelName)
	{
		CComPtrMgd_003CIEndpointHost_003E spEndpointHost = m_spEndpointHost;
		if (spEndpointHost.p == null)
		{
			global::_003CModule_003E._ZuneShipAssert(1002u, 842u);
			return -2147418113;
		}
		ushort* ptr = null;
		IEndpointHost* p = spEndpointHost.p;
		int num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, EEndpointHostProperty, ushort**, int>)(int)(*(uint*)(*(int*)p + 60)))((nint)p, EEndpointHostProperty.eEndpointHostPropertyModelName, &ptr);
		if (num >= 0 && ptr != null)
		{
			strModelName = new string((char*)ptr);
			global::_003CModule_003E.SysFreeString(ptr);
		}
		return num;
	}

	public unsafe int GetUserGuid(ref Guid guidUserGuid)
	{
		CComPtrMgd_003CIEndpointHost_003E spEndpointHost = m_spEndpointHost;
		if (spEndpointHost.p == null)
		{
			global::_003CModule_003E._ZuneShipAssert(1002u, 863u);
			return -2147418113;
		}
		_GUID gUID_NULL = global::_003CModule_003E.GUID_NULL;
		IEndpointHost* p = spEndpointHost.p;
		int num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, EEndpointHostProperty, _GUID*, int>)(int)(*(uint*)(*(int*)p + 56)))((nint)p, EEndpointHostProperty.eEndpointHostPropertyUserGuid, &gUID_NULL);
		if (num >= 0)
		{
			Guid guid = global::_003CModule_003E.GUIDToGuid(gUID_NULL);
			guidUserGuid = guid;
		}
		return num;
	}

	public unsafe int GetUserId(ref int userId)
	{
		IEndpointHost* p = m_spEndpointHost.p;
		if (p == null)
		{
			global::_003CModule_003E._ZuneShipAssert(1002u, 884u);
			return -2147418113;
		}
		Unsafe.SkipInit(out int num2);
		int num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, EEndpointHostProperty, int*, int>)(int)(*(uint*)(*(int*)p + 68)))((nint)p, EEndpointHostProperty.eEndpointHostPropertyAssociatedUserId, &num2);
		if (num >= 0)
		{
			userId = num2;
		}
		else
		{
			userId = 0;
		}
		return num;
	}

	public unsafe int GetZuneTag(ref string strZuneTag)
	{
		CComPtrMgd_003CIEndpointHost_003E spEndpointHost = m_spEndpointHost;
		if (spEndpointHost.p == null)
		{
			global::_003CModule_003E._ZuneShipAssert(1002u, 1029u);
			return -2147418113;
		}
		ushort* ptr = null;
		IEndpointHost* p = spEndpointHost.p;
		int num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, EEndpointHostProperty, ushort**, int>)(int)(*(uint*)(*(int*)p + 60)))((nint)p, EEndpointHostProperty.eEndpointHostPropertyZuneTag, &ptr);
		if (num >= 0 && ptr != null)
		{
			strZuneTag = new string((char*)ptr);
			global::_003CModule_003E.SysFreeString(ptr);
		}
		return num;
	}

	public unsafe int GetLiveId(ref string strLiveId)
	{
		CComPtrMgd_003CIEndpointHost_003E spEndpointHost = m_spEndpointHost;
		if (spEndpointHost.p == null)
		{
			global::_003CModule_003E._ZuneShipAssert(1002u, 1051u);
			return -2147418113;
		}
		ushort* ptr = null;
		IEndpointHost* p = spEndpointHost.p;
		int num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, EEndpointHostProperty, ushort**, int>)(int)(*(uint*)(*(int*)p + 60)))((nint)p, EEndpointHostProperty.eEndpointHostPropertyLiveId, &ptr);
		if (num >= 0 && ptr != null)
		{
			strLiveId = new string((char*)ptr);
			global::_003CModule_003E.SysFreeString(ptr);
		}
		return num;
	}

	public unsafe int GetIsTvOutSupported(ref bool fIsTvOutSupported)
	{
		CComPtrMgd_003CIEndpointHost_003E spEndpointHost = m_spEndpointHost;
		if (spEndpointHost.p == null)
		{
			global::_003CModule_003E._ZuneShipAssert(1002u, 1073u);
			return -2147418113;
		}
		bool flag = false;
		IEndpointHost* p = spEndpointHost.p;
		int num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, EEndpointCapability, bool*, int>)(int)(*(uint*)(*(int*)p + 40)))((nint)p, EEndpointCapability.eEndpointCapabilityTvOut, &flag);
		if (num >= 0)
		{
			fIsTvOutSupported = flag;
		}
		return num;
	}

	public unsafe int GetIsRestorePointSupported(ref bool fIsRestorePointSupported)
	{
		CComPtrMgd_003CIEndpointHost_003E spEndpointHost = m_spEndpointHost;
		if (spEndpointHost.p == null)
		{
			global::_003CModule_003E._ZuneShipAssert(1002u, 1095u);
			return -2147418113;
		}
		bool flag = false;
		IEndpointHost* p = spEndpointHost.p;
		int num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, EEndpointCapability, bool*, int>)(int)(*(uint*)(*(int*)p + 40)))((nint)p, EEndpointCapability.eEndpointCapabilityRestorePoint, &flag);
		if (num >= 0)
		{
			fIsRestorePointSupported = flag;
		}
		return num;
	}

	public unsafe int GetCapability(EEndpointCapability capabilityId, ref bool fHasCapability)
	{
		CComPtrMgd_003CIEndpointHost_003E spEndpointHost = m_spEndpointHost;
		if (spEndpointHost.p == null)
		{
			global::_003CModule_003E._ZuneShipAssert(1002u, 1117u);
			return -2147418113;
		}
		bool flag = false;
		IEndpointHost* p = spEndpointHost.p;
		int num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, EEndpointCapability, bool*, int>)(int)(*(uint*)(*(int*)p + 40)))((nint)p, capabilityId, &flag);
		if (num >= 0)
		{
			fHasCapability = flag;
		}
		return num;
	}

	public unsafe int GetGeoId(ref uint dwGeoId)
	{
		IEndpointHost* p = m_spEndpointHost.p;
		if (p == null)
		{
			global::_003CModule_003E._ZuneShipAssert(1002u, 909u);
			return -2147418113;
		}
		Unsafe.SkipInit(out uint num2);
		int num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, EEndpointHostProperty, uint*, int>)(int)(*(uint*)(*(int*)p + 72)))((nint)p, EEndpointHostProperty.eEndpointHostPropertyGeoId, &num2);
		if (num >= 0)
		{
			dwGeoId = num2;
		}
		else
		{
			dwGeoId = 0u;
		}
		return num;
	}

	public unsafe int SetGeoId(uint dwGeoId)
	{
		IEndpointHost* p = m_spEndpointHost.p;
		if (p == null)
		{
			global::_003CModule_003E._ZuneShipAssert(1002u, 934u);
			return -2147418113;
		}
		IEndpointHost* ptr = p;
		return ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, EEndpointHostProperty, uint, int>)(int)(*(uint*)(*(int*)ptr + 104)))((nint)ptr, EEndpointHostProperty.eEndpointHostPropertyGeoId, dwGeoId);
	}

	public unsafe int GetTimeZoneBias(ref int lTimeZoneBias)
	{
		IEndpointHost* p = m_spEndpointHost.p;
		if (p == null)
		{
			global::_003CModule_003E._ZuneShipAssert(1002u, 949u);
			return -2147418113;
		}
		Unsafe.SkipInit(out int num2);
		int num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, EEndpointHostProperty, int*, int>)(int)(*(uint*)(*(int*)p + 68)))((nint)p, EEndpointHostProperty.eEndpointHostPropertyTimeZoneBias, &num2);
		if (num >= 0)
		{
			lTimeZoneBias = num2;
		}
		else
		{
			lTimeZoneBias = 0;
		}
		return num;
	}

	public unsafe int SetTimeZoneBias(int lTimeZoneBias)
	{
		IEndpointHost* p = m_spEndpointHost.p;
		if (p == null)
		{
			global::_003CModule_003E._ZuneShipAssert(1002u, 974u);
			return -2147418113;
		}
		IEndpointHost* ptr = p;
		return ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, EEndpointHostProperty, int, int>)(int)(*(uint*)(*(int*)ptr + 100)))((nint)ptr, EEndpointHostProperty.eEndpointHostPropertyTimeZoneBias, lTimeZoneBias);
	}

	public unsafe int GetWatsonSetting(ref uint dwWatsonSetting)
	{
		IEndpointHost* p = m_spEndpointHost.p;
		if (p == null)
		{
			global::_003CModule_003E._ZuneShipAssert(1002u, 989u);
			return -2147418113;
		}
		Unsafe.SkipInit(out uint num2);
		int num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, EEndpointHostProperty, uint*, int>)(int)(*(uint*)(*(int*)p + 72)))((nint)p, EEndpointHostProperty.eEndpointHostPropertyWatsonSetting, &num2);
		if (num >= 0)
		{
			dwWatsonSetting = num2;
		}
		else
		{
			dwWatsonSetting = 0u;
		}
		return num;
	}

	public unsafe int SetWatsonSetting(uint dwWatsonSetting)
	{
		IEndpointHost* p = m_spEndpointHost.p;
		if (p == null)
		{
			global::_003CModule_003E._ZuneShipAssert(1002u, 1014u);
			return -2147418113;
		}
		IEndpointHost* ptr = p;
		return ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, EEndpointHostProperty, uint, int>)(int)(*(uint*)(*(int*)ptr + 104)))((nint)ptr, EEndpointHostProperty.eEndpointHostPropertyWatsonSetting, dwWatsonSetting);
	}

	public unsafe int SetUserGuidandZuneTag(Guid guidUserGuid, string strZuneTag)
	{
		if (m_spEndpointHost.p == null)
		{
			global::_003CModule_003E._ZuneShipAssert(1002u, 1139u);
			return -2147418113;
		}
		_GUID gUID = global::_003CModule_003E.GuidToGUID(guidUserGuid);
		IEndpointHost* p = m_spEndpointHost.p;
		int num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, EEndpointHostProperty, _GUID*, int>)(int)(*(uint*)(*(int*)p + 88)))((nint)p, EEndpointHostProperty.eEndpointHostPropertyUserGuid, &gUID);
		if (num >= 0)
		{
			fixed (ushort* ptr = &Unsafe.As<char, ushort>(ref global::_003CModule_003E.PtrToStringChars(strZuneTag)))
			{
				try
				{
					if (ptr != null)
					{
						IEndpointHost* p2 = m_spEndpointHost.p;
						int num2 = *(int*)p2 + 92;
						num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, EEndpointHostProperty, ushort*, int>)(int)(*(uint*)num2))((nint)p2, EEndpointHostProperty.eEndpointHostPropertyZuneTag, ptr);
					}
					else
					{
						num = -2147418113;
					}
					if (num < 0)
					{
						IEndpointHost* p3 = m_spEndpointHost.p;
						num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, EEndpointHostProperty, _GUID*, int>)(int)(*(uint*)(*(int*)p3 + 88)))((nint)p3, EEndpointHostProperty.eEndpointHostPropertyUserGuid, (_GUID*)Unsafe.AsPointer(ref global::_003CModule_003E.GUID_NULL));
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
		return num;
	}

	public int ClearUserGuidandZuneTag()
	{
		return SetUserGuidandZuneTag(default(Guid), "");
	}

	public unsafe int SetMarketplaceCredentials(SecureString strUsername, SecureString strPassword)
	{
		if (m_spEndpointHost.p == null)
		{
			global::_003CModule_003E._ZuneShipAssert(1002u, 1191u);
			return -2147418113;
		}
		IntPtr hglobal = Marshal.SecureStringToGlobalAllocUnicode(strUsername);
		IntPtr hglobal2 = Marshal.SecureStringToGlobalAllocUnicode(strPassword);
		IEndpointHost* p = m_spEndpointHost.p;
		int num = *(int*)p + 80;
		int result = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, EEndpointHostProperty, ushort*, ushort*, int>)(int)(*(uint*)num))((nint)p, EEndpointHostProperty.eEndpointHostPropertyMarketplaceCredentials, (ushort*)hglobal.ToPointer(), (ushort*)hglobal2.ToPointer());
		Marshal.FreeHGlobal(hglobal);
		Marshal.FreeHGlobal(hglobal2);
		return result;
	}

	public unsafe int GetOOBECompleted(ref bool oobeCompleted)
	{
		CComPtrMgd_003CIEndpointHost_003E spEndpointHost = m_spEndpointHost;
		if (spEndpointHost.p == null)
		{
			global::_003CModule_003E._ZuneShipAssert(1002u, 1241u);
			return -2147418113;
		}
		bool flag = false;
		IEndpointHost* p = spEndpointHost.p;
		int num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, EEndpointHostProperty, bool*, int>)(int)(*(uint*)(*(int*)p + 76)))((nint)p, EEndpointHostProperty.eEndpointHostPropertyOOBECompleted, &flag);
		if (num >= 0)
		{
			oobeCompleted = flag;
		}
		return num;
	}

	public unsafe int GetPurchaseEnabled(ref bool purchaseEnabled)
	{
		CComPtrMgd_003CIEndpointHost_003E spEndpointHost = m_spEndpointHost;
		if (spEndpointHost.p == null)
		{
			global::_003CModule_003E._ZuneShipAssert(1002u, 1220u);
			return -2147418113;
		}
		bool flag = false;
		IEndpointHost* p = spEndpointHost.p;
		int result = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, EEndpointHostProperty, bool*, int>)(int)(*(uint*)(*(int*)p + 76)))((nint)p, EEndpointHostProperty.eEndpointHostPropertyPurchaseEnabled, &flag);
		purchaseEnabled = flag;
		return result;
	}

	public unsafe int SetPurchaseEnabled([MarshalAs(UnmanagedType.U1)] bool purchaseEnabled)
	{
		IEndpointHost* p = m_spEndpointHost.p;
		if (p == null)
		{
			global::_003CModule_003E._ZuneShipAssert(1002u, 1265u);
			return -2147418113;
		}
		return ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, EEndpointHostProperty, byte, int>)(int)(*(uint*)(*(int*)p + 108)))((nint)p, EEndpointHostProperty.eEndpointHostPropertyPurchaseEnabled, purchaseEnabled ? ((byte)1) : ((byte)0));
	}

	public unsafe int GetAndResetLastLoginError(ref int hrLogin)
	{
		CComPtrMgd_003CIEndpointHost_003E spEndpointHost = m_spEndpointHost;
		if (spEndpointHost.p == null)
		{
			global::_003CModule_003E._ZuneShipAssert(1002u, 1279u);
			return -2147418113;
		}
		int num = 0;
		IEndpointHost* p = spEndpointHost.p;
		int num2 = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, EEndpointHostProperty, int*, int>)(int)(*(uint*)(*(int*)p + 68)))((nint)p, EEndpointHostProperty.eEndpointHostPropertyLastLoginError, &num);
		int num3 = ((num2 >= 0) ? num : 0);
		hrLogin = num3;
		return num2;
	}

	public unsafe int GetAndResetLastDownloadError(ref int hrDownload)
	{
		CComPtrMgd_003CIEndpointHost_003E spEndpointHost = m_spEndpointHost;
		if (spEndpointHost.p == null)
		{
			global::_003CModule_003E._ZuneShipAssert(1002u, 1301u);
			return -2147418113;
		}
		int num = 0;
		IEndpointHost* p = spEndpointHost.p;
		int num2 = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, EEndpointHostProperty, int*, int>)(int)(*(uint*)(*(int*)p + 68)))((nint)p, EEndpointHostProperty.eEndpointHostPropertyLastDownloadError, &num);
		int num3 = ((num2 >= 0) ? num : 0);
		hrDownload = num3;
		return num2;
	}

	public unsafe int LoadWlanProvider()
	{
		int num = 0;
		if (m_spWlanProvider.p == null)
		{
			if (m_spEndpointHost.p == null)
			{
				global::_003CModule_003E._ZuneShipAssert(1001u, 1325u);
				return -2147467261;
			}
			Unsafe.SkipInit(out CComPtrNtv_003CIWlanProvider_003E cComPtrNtv_003CIWlanProvider_003E);
			*(int*)(&cComPtrNtv_003CIWlanProvider_003E) = 0;
			try
			{
				num = global::_003CModule_003E.GetInterfaceProperty_003Cstruct_0020IEndpointHost_002Cenum_0020EEndpointHostProperty_002Cstruct_0020IWlanProvider_003E(m_spEndpointHost.p, EEndpointHostProperty.eEndpointHostPropertyWlanProvider, (IWlanProvider**)(&cComPtrNtv_003CIWlanProvider_003E));
				if (num >= 0)
				{
					m_spWlanProvider.op_Assign((IWlanProvider*)(int)(*(uint*)(&cComPtrNtv_003CIWlanProvider_003E)));
					DeviceMediator* p = m_spDeviceMediator.p;
					DeviceMediator* ptr = (DeviceMediator*)((p == null) ? null : ((byte*)p + 8));
					IWlanProvider* p2 = m_spWlanProvider.p;
					((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, IZuneWlanCallback*, int>)(int)(*(uint*)(*(int*)p2 + 12)))((nint)p2, (IZuneWlanCallback*)ptr);
				}
			}
			catch
			{
				//try-fault
				global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<CComPtrNtv_003CIWlanProvider_003E*, void>)(&global::_003CModule_003E.CComPtrNtv_003CIWlanProvider_003E_002E_007Bdtor_007D), &cComPtrNtv_003CIWlanProvider_003E);
				throw;
			}
			global::_003CModule_003E.CComPtrNtv_003CIWlanProvider_003E_002ERelease(&cComPtrNtv_003CIWlanProvider_003E);
		}
		return num;
	}

	public unsafe int SetWlanProfileList(WlanProfileList profileList)
	{
		if (m_spEndpointHost.p == null)
		{
			global::_003CModule_003E._ZuneShipAssert(1002u, 1356u);
			return -2147418113;
		}
		IWlanProfileList* ptr = null;
		if (global::_003CModule_003E.WPP_GLOBAL_Control != Unsafe.AsPointer(ref global::_003CModule_003E.WPP_GLOBAL_Control) && (((int*)global::_003CModule_003E.WPP_GLOBAL_Control)[15] & 2) != 0 && (uint)((byte*)global::_003CModule_003E.WPP_GLOBAL_Control)[57] >= 5u)
		{
			global::_003CModule_003E.WPP_SF_d(((ulong*)global::_003CModule_003E.WPP_GLOBAL_Control)[6], 29, (_GUID*)Unsafe.AsPointer(ref global::_003CModule_003E._003FA0xbf2368f8_002EWPP_DeviceAPI_cpp_Traceguids), m_iDeviceID);
		}
		int num = LoadWlanProvider();
		if (num >= 0)
		{
			IWlanProvider* p = m_spWlanProvider.p;
			num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, IWlanProfileList**, int>)(int)(*(uint*)(*(int*)p + 20)))((nint)p, &ptr);
			if (num >= 0)
			{
				int num2 = 0;
				Unsafe.SkipInit(out _0024ArrayType_0024_0024_0024BY0BAA_0040E _0024ArrayType_0024_0024_0024BY0BAA_0040E2);
				while (num2 < profileList.Count)
				{
					IWlanProfile* ptr2 = null;
					WlanProfile wlanProfile = profileList[num2];
					fixed (ushort* ptr3 = &Unsafe.As<char, ushort>(ref global::_003CModule_003E.PtrToStringChars(wlanProfile.SSID)))
					{
						try
						{
							if (ptr3 == null)
							{
								goto end_IL_00bf;
							}
							ushort* ptr4 = global::_003CModule_003E.SysAllocString(ptr3);
							ushort* ptr5 = null;
							if ((wlanProfile.Encrypted || !(null == wlanProfile.Key)) && (!wlanProfile.Encrypted || null != wlanProfile.EncryptedKey))
							{
								if (!(null != wlanProfile.Key))
								{
									goto IL_0139;
								}
								fixed (ushort* ptr6 = &Unsafe.As<char, ushort>(ref global::_003CModule_003E.PtrToStringChars(wlanProfile.Key)))
								{
									try
									{
										if (ptr6 != null)
										{
											ptr5 = global::_003CModule_003E.SysAllocString(ptr6);
											goto IL_0139;
										}
									}
									catch
									{
										//try-fault
										ptr6 = null;
										throw;
									}
								}
								goto IL_032c;
							}
							goto end_IL_00bf_2;
							IL_0139:
							IWlanProvider* p2 = m_spWlanProvider.p;
							num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, IWlanProfile**, int>)(int)(*(uint*)(*(int*)p2 + 16)))((nint)p2, &ptr2);
							if (num >= 0)
							{
								num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, ushort*, int>)(int)(*(uint*)(*(int*)ptr2 + 16)))((nint)ptr2, ptr4);
								if (num >= 0)
								{
									num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, _DOT11_AUTH_ALGORITHM, int>)(int)(*(uint*)(*(int*)ptr2 + 24)))((nint)ptr2, (_DOT11_AUTH_ALGORITHM)wlanProfile.Auth);
									if (num >= 0)
									{
										num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, _DOT11_CIPHER_ALGORITHM, int>)(int)(*(uint*)(*(int*)ptr2 + 32)))((nint)ptr2, (_DOT11_CIPHER_ALGORITHM)wlanProfile.Cipher);
										if (num >= 0)
										{
											int num3 = (wlanProfile.PassPhrase ? 1 : 0);
											num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, int, int>)(int)(*(uint*)(*(int*)ptr2 + 40)))((nint)ptr2, num3);
											if (num >= 0)
											{
												if (wlanProfile.Encrypted)
												{
													if (256 < (nuint)wlanProfile.EncryptedKey.LongLength)
													{
														num = -2147024774;
													}
													if (num < 0)
													{
														goto IL_02d0;
													}
													int num4 = 0;
													if (0 < (nint)wlanProfile.EncryptedKey.LongLength)
													{
														do
														{
															*(byte*)((ref *(_003F*)num4) + (ref *(_003F*)(&_0024ArrayType_0024_0024_0024BY0BAA_0040E2))) = wlanProfile.EncryptedKey[num4];
															num4++;
														}
														while (num4 < (nint)wlanProfile.EncryptedKey.LongLength);
													}
													num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, byte*, uint, int, int>)(int)(*(uint*)(*(int*)ptr2 + 48)))((nint)ptr2, (byte*)(&_0024ArrayType_0024_0024_0024BY0BAA_0040E2), (uint)wlanProfile.EncryptedKey.Length, 1);
												}
												else if (WirelessCiphers.None == wlanProfile.Cipher && 0 == global::_003CModule_003E.SysStringLen(ptr5))
												{
													num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, byte*, uint, int, int>)(int)(*(uint*)(*(int*)ptr2 + 48)))((nint)ptr2, null, 0u, 1);
												}
												else
												{
													int num5 = *(int*)ptr2 + 48;
													IWlanProfile* intPtr = ptr2;
													ushort* intPtr2 = ptr5;
													num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, byte*, uint, int, int>)(int)(*(uint*)num5))((nint)intPtr, (byte*)intPtr2, global::_003CModule_003E.SysStringLen(intPtr2) << 1, 0);
												}
												if (num >= 0)
												{
													num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint, int>)(int)(*(uint*)(*(int*)ptr2 + 56)))((nint)ptr2, wlanProfile.KeyIndex);
													if (num >= 0)
													{
														num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint, int>)(int)(*(uint*)(*(int*)ptr2 + 72)))((nint)ptr2, wlanProfile.SignalQuality);
														if (num >= 0)
														{
															num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, int, int>)(int)(*(uint*)(*(int*)ptr2 + 80)))((nint)ptr2, wlanProfile.Connected ? 1 : 0);
															if (num >= 0)
															{
																num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, IWlanProfile*, int>)(int)(*(uint*)(*(int*)ptr + 20)))((nint)ptr, ptr2);
															}
														}
													}
												}
											}
										}
									}
								}
							}
							goto IL_02d0;
							IL_02d0:
							global::_003CModule_003E.SysFreeString(ptr4);
							global::_003CModule_003E.SysFreeString(ptr5);
							if (null != ptr2)
							{
								IWlanProfile* intPtr3 = ptr2;
								((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint>)(int)(*(uint*)(*(int*)intPtr3 + 8)))((nint)intPtr3);
							}
							goto IL_02fb;
							end_IL_00bf:;
						}
						catch
						{
							//try-fault
							ptr3 = null;
							throw;
						}
						try
						{
							global::_003CModule_003E._ZuneShipAssert(1002u, 1382u);
						}
						catch
						{
							//try-fault
							ptr3 = null;
							throw;
						}
						goto IL_0322;
						IL_032c:
						try
						{
							try
							{
								global::_003CModule_003E._ZuneShipAssert(1002u, 1398u);
							}
							catch
							{
								//try-fault
								throw;
							}
						}
						catch
						{
							//try-fault
							ptr3 = null;
							throw;
						}
						goto IL_0350;
						end_IL_00bf_2:;
					}
					return -2147024809;
					IL_0350:
					return -2147418113;
					IL_02fb:
					num2++;
					if (num < 0)
					{
						break;
					}
					continue;
					IL_0322:
					return -2147418113;
				}
				IWlanProvider* p3 = m_spWlanProvider.p;
				num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, IWlanProfileList*, int>)(int)(*(uint*)(*(int*)p3 + 24)))((nint)p3, ptr);
				IWlanProfileList* intPtr4 = ptr;
				((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint>)(int)(*(uint*)(*(int*)intPtr4 + 8)))((nint)intPtr4);
			}
		}
		if (global::_003CModule_003E.WPP_GLOBAL_Control != Unsafe.AsPointer(ref global::_003CModule_003E.WPP_GLOBAL_Control) && (((int*)global::_003CModule_003E.WPP_GLOBAL_Control)[15] & 2) != 0 && (uint)((byte*)global::_003CModule_003E.WPP_GLOBAL_Control)[57] >= 5u)
		{
			global::_003CModule_003E.WPP_SF_d(((ulong*)global::_003CModule_003E.WPP_GLOBAL_Control)[6], 30, (_GUID*)Unsafe.AsPointer(ref global::_003CModule_003E._003FA0xbf2368f8_002EWPP_DeviceAPI_cpp_Traceguids), num);
		}
		return num;
	}

	public unsafe int GetWlanProfileList(ref WlanProfileList profileList)
	{
		if (m_spEndpointHost.p == null)
		{
			global::_003CModule_003E._ZuneShipAssert(1002u, 1503u);
			return -2147418113;
		}
		IWlanProfileList* ptr = null;
		if (global::_003CModule_003E.WPP_GLOBAL_Control != Unsafe.AsPointer(ref global::_003CModule_003E.WPP_GLOBAL_Control) && (((int*)global::_003CModule_003E.WPP_GLOBAL_Control)[15] & 2) != 0 && (uint)((byte*)global::_003CModule_003E.WPP_GLOBAL_Control)[57] >= 5u)
		{
			global::_003CModule_003E.WPP_SF_d(((ulong*)global::_003CModule_003E.WPP_GLOBAL_Control)[6], 31, (_GUID*)Unsafe.AsPointer(ref global::_003CModule_003E._003FA0xbf2368f8_002EWPP_DeviceAPI_cpp_Traceguids), m_iDeviceID);
		}
		int num = LoadWlanProvider();
		if (num >= 0)
		{
			IWlanProvider* p = m_spWlanProvider.p;
			num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, IWlanProfileList**, int>)(int)(*(uint*)(*(int*)p + 28)))((nint)p, &ptr);
			if (num >= 0)
			{
				int num2 = 0;
				num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, int*, int>)(int)(*(uint*)(*(int*)ptr + 16)))((nint)ptr, &num2);
				int num3 = 0;
				if (num >= 0)
				{
					Unsafe.SkipInit(out _0024ArrayType_0024_0024_0024BY0BAA_0040E _0024ArrayType_0024_0024_0024BY0BAA_0040E2);
					while (num3 < num2)
					{
						IWlanProfile* ptr2 = null;
						num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, int, IWlanProfile**, int>)(int)(*(uint*)(*(int*)ptr + 24)))((nint)ptr, num3, &ptr2);
						if (num >= 0)
						{
							ushort* ptr3 = null;
							_DOT11_AUTH_ALGORITHM auth = (_DOT11_AUTH_ALGORITHM)1;
							_DOT11_CIPHER_ALGORITHM cipher = (_DOT11_CIPHER_ALGORITHM)0;
							int num4 = 0;
							uint num5 = 256u;
							int num6 = 0;
							uint keyIndex = 0u;
							uint signalQuality = 0u;
							int num7 = 0;
							num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, ushort**, int>)(int)(*(uint*)(*(int*)ptr2 + 12)))((nint)ptr2, &ptr3);
							if (num >= 0)
							{
								num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, _DOT11_AUTH_ALGORITHM*, int>)(int)(*(uint*)(*(int*)ptr2 + 20)))((nint)ptr2, &auth);
								if (num >= 0)
								{
									num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, _DOT11_CIPHER_ALGORITHM*, int>)(int)(*(uint*)(*(int*)ptr2 + 28)))((nint)ptr2, &cipher);
									if (num >= 0)
									{
										num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, int*, int>)(int)(*(uint*)(*(int*)ptr2 + 36)))((nint)ptr2, &num4);
										if (num >= 0)
										{
											num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, byte*, uint*, int*, int>)(int)(*(uint*)(*(int*)ptr2 + 44)))((nint)ptr2, (byte*)(&_0024ArrayType_0024_0024_0024BY0BAA_0040E2), &num5, &num6);
											if (num >= 0)
											{
												num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint*, int>)(int)(*(uint*)(*(int*)ptr2 + 52)))((nint)ptr2, &keyIndex);
												if (num >= 0)
												{
													num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint*, int>)(int)(*(uint*)(*(int*)ptr2 + 68)))((nint)ptr2, &signalQuality);
													if (num >= 0)
													{
														num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, int*, int>)(int)(*(uint*)(*(int*)ptr2 + 76)))((nint)ptr2, &num7);
														if (num >= 0)
														{
															WlanProfile wlanProfile = new WlanProfile();
															wlanProfile.SSID = new string((char*)ptr3);
															wlanProfile.Auth = (WirelessAuthenticationTypes)auth;
															wlanProfile.Cipher = (WirelessCiphers)cipher;
															byte passPhrase = (byte)((num4 != 0) ? 1 : 0);
															wlanProfile.PassPhrase = passPhrase != 0;
															if (0 == num5)
															{
																wlanProfile.EncryptedKey = new byte[0];
															}
															else if (num6 != 0)
															{
																wlanProfile.EncryptedKey = new byte[num5];
																uint num8 = 0u;
																if (0 < num5)
																{
																	do
																	{
																		byte[] encryptedKey = wlanProfile.EncryptedKey;
																		uint num9 = num8;
																		encryptedKey[num9] = *(byte*)((ref *(_003F*)num9) + (ref *(_003F*)(&_0024ArrayType_0024_0024_0024BY0BAA_0040E2)));
																		num8++;
																	}
																	while (num8 < num5);
																}
															}
															else
															{
																wlanProfile.Key = new string((char*)(&_0024ArrayType_0024_0024_0024BY0BAA_0040E2));
															}
															wlanProfile.KeyIndex = keyIndex;
															wlanProfile.SignalQuality = signalQuality;
															byte connected = ((num7 == 1) ? ((byte)1) : ((byte)0));
															wlanProfile.Connected = connected != 0;
															profileList.Add(wlanProfile);
														}
													}
												}
											}
										}
									}
								}
							}
							global::_003CModule_003E.SysFreeString(ptr3);
							if (null != ptr2)
							{
								IWlanProfile* intPtr = ptr2;
								((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint>)(int)(*(uint*)(*(int*)intPtr + 8)))((nint)intPtr);
							}
						}
						num3++;
						if (num < 0)
						{
							break;
						}
					}
				}
			}
		}
		IWlanProfileList* intPtr2 = ptr;
		((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint>)(int)(*(uint*)(*(int*)intPtr2 + 8)))((nint)intPtr2);
		if (global::_003CModule_003E.WPP_GLOBAL_Control != Unsafe.AsPointer(ref global::_003CModule_003E.WPP_GLOBAL_Control) && (((int*)global::_003CModule_003E.WPP_GLOBAL_Control)[15] & 2) != 0 && (uint)((byte*)global::_003CModule_003E.WPP_GLOBAL_Control)[57] >= 5u)
		{
			global::_003CModule_003E.WPP_SF_d(((ulong*)global::_003CModule_003E.WPP_GLOBAL_Control)[6], 32, (_GUID*)Unsafe.AsPointer(ref global::_003CModule_003E._003FA0xbf2368f8_002EWPP_DeviceAPI_cpp_Traceguids), num);
		}
		return num;
	}

	public unsafe int GetWlanProfiles()
	{
		if (m_spEndpointHost.p == null)
		{
			global::_003CModule_003E._ZuneShipAssert(1002u, 1636u);
			return -2147418113;
		}
		int num = LoadWlanProvider();
		if (num >= 0)
		{
			IWlanProvider* p = m_spWlanProvider.p;
			num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, int>)(int)(*(uint*)(*(int*)p + 32)))((nint)p);
		}
		return num;
	}

	public unsafe int IsWlanFirewallEnabled(ref bool bEnabled)
	{
		if (m_spEndpointHost.p == null)
		{
			global::_003CModule_003E._ZuneShipAssert(1002u, 1664u);
			return -2147418113;
		}
		int num = 0;
		int num2 = LoadWlanProvider();
		if (num2 >= 0)
		{
			IWlanProvider* p = m_spWlanProvider.p;
			num2 = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, int*, int>)(int)(*(uint*)(*(int*)p + 36)))((nint)p, &num);
		}
		bool flag = ((num != 0) ? true : false);
		bEnabled = flag;
		return num2;
	}

	public void GetWlanProfilesComplete(int hr)
	{
		if (m_GetWlanProfilesComplete != null)
		{
			m_GetWlanProfilesComplete(this, hr);
		}
	}

	public unsafe int GetDeviceWlanNetworks()
	{
		if (m_spEndpointHost.p == null)
		{
			global::_003CModule_003E._ZuneShipAssert(1002u, 1686u);
			return -2147418113;
		}
		int num = LoadWlanProvider();
		if (num >= 0)
		{
			IWlanProvider* p = m_spWlanProvider.p;
			num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, int>)(int)(*(uint*)(*(int*)p + 40)))((nint)p);
		}
		return num;
	}

	public void GetDeviceWlanNetworksComplete(int hr)
	{
		if (m_GetDeviceWlanNetworksComplete != null)
		{
			m_GetDeviceWlanNetworksComplete(this, hr);
		}
	}

	public unsafe int GetDeviceWlanProfiles()
	{
		if (m_spEndpointHost.p == null)
		{
			global::_003CModule_003E._ZuneShipAssert(1002u, 1714u);
			return -2147418113;
		}
		int num = LoadWlanProvider();
		if (num >= 0)
		{
			IWlanProvider* p = m_spWlanProvider.p;
			num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, int>)(int)(*(uint*)(*(int*)p + 44)))((nint)p);
		}
		return num;
	}

	public void GetDeviceWlanProfilesComplete(int hr)
	{
		if (m_GetDeviceWlanProfilesComplete != null)
		{
			m_GetDeviceWlanProfilesComplete(this, hr);
		}
	}

	public unsafe int SetDeviceWlanProfiles()
	{
		if (m_spEndpointHost.p == null)
		{
			global::_003CModule_003E._ZuneShipAssert(1002u, 1742u);
			return -2147418113;
		}
		int num = LoadWlanProvider();
		if (num >= 0)
		{
			IWlanProvider* p = m_spWlanProvider.p;
			num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, int>)(int)(*(uint*)(*(int*)p + 48)))((nint)p);
		}
		return num;
	}

	public void SetDeviceWlanProfilesComplete(int hr)
	{
		if (m_SetDeviceWlanProfilesComplete != null)
		{
			m_SetDeviceWlanProfilesComplete(this, hr);
		}
	}

	public unsafe int GetWlanDeviceAuthCipherPairList(ref WlanAuthCipherPairList authCipherPairList)
	{
		if (m_spEndpointHost.p == null)
		{
			global::_003CModule_003E._ZuneShipAssert(1002u, 1770u);
			return -2147418113;
		}
		IWlanAuthCipherPairList* ptr = null;
		if (global::_003CModule_003E.WPP_GLOBAL_Control != Unsafe.AsPointer(ref global::_003CModule_003E.WPP_GLOBAL_Control) && (((int*)global::_003CModule_003E.WPP_GLOBAL_Control)[15] & 2) != 0 && (uint)((byte*)global::_003CModule_003E.WPP_GLOBAL_Control)[57] >= 5u)
		{
			global::_003CModule_003E.WPP_SF_d(((ulong*)global::_003CModule_003E.WPP_GLOBAL_Control)[6], 33, (_GUID*)Unsafe.AsPointer(ref global::_003CModule_003E._003FA0xbf2368f8_002EWPP_DeviceAPI_cpp_Traceguids), m_iDeviceID);
		}
		int num = LoadWlanProvider();
		if (num >= 0)
		{
			IWlanProvider* p = m_spWlanProvider.p;
			num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, IWlanAuthCipherPairList**, int>)(int)(*(uint*)(*(int*)p + 52)))((nint)p, &ptr);
			if (num >= 0)
			{
				int num2 = 0;
				num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, int*, int>)(int)(*(uint*)(*(int*)ptr + 16)))((nint)ptr, &num2);
				int num3 = 0;
				if (num >= 0)
				{
					Unsafe.SkipInit(out DOT11_AUTH_CIPHER_PAIR dOT11_AUTH_CIPHER_PAIR);
					while (num3 < num2)
					{
						num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, int, DOT11_AUTH_CIPHER_PAIR*, int>)(int)(*(uint*)(*(int*)ptr + 24)))((nint)ptr, num3, &dOT11_AUTH_CIPHER_PAIR);
						if (num >= 0)
						{
							WlanAuthCipherPair wlanAuthCipherPair = new WlanAuthCipherPair();
							wlanAuthCipherPair.Auth = *(WirelessAuthenticationTypes*)(&dOT11_AUTH_CIPHER_PAIR);
							wlanAuthCipherPair.Cipher = Unsafe.As<DOT11_AUTH_CIPHER_PAIR, WirelessCiphers>(ref Unsafe.AddByteOffset(ref dOT11_AUTH_CIPHER_PAIR, 4));
							authCipherPairList.Add(wlanAuthCipherPair);
						}
						num3++;
						if (num < 0)
						{
							break;
						}
					}
				}
				IWlanAuthCipherPairList* intPtr = ptr;
				((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint>)(int)(*(uint*)(*(int*)intPtr + 8)))((nint)intPtr);
			}
		}
		if (global::_003CModule_003E.WPP_GLOBAL_Control != Unsafe.AsPointer(ref global::_003CModule_003E.WPP_GLOBAL_Control) && (((int*)global::_003CModule_003E.WPP_GLOBAL_Control)[15] & 2) != 0 && (uint)((byte*)global::_003CModule_003E.WPP_GLOBAL_Control)[57] >= 5u)
		{
			global::_003CModule_003E.WPP_SF_d(((ulong*)global::_003CModule_003E.WPP_GLOBAL_Control)[6], 34, (_GUID*)Unsafe.AsPointer(ref global::_003CModule_003E._003FA0xbf2368f8_002EWPP_DeviceAPI_cpp_Traceguids), num);
		}
		return num;
	}

	public unsafe int GetDisconnectedWlanDeviceUuid(ref string strDeviceUuid)
	{
		if (m_spEndpointHost.p == null)
		{
			global::_003CModule_003E._ZuneShipAssert(1002u, 1818u);
			return -2147418113;
		}
		ushort* ptr = null;
		int num = LoadWlanProvider();
		if (num >= 0)
		{
			IWlanProvider* p = m_spWlanProvider.p;
			num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, ushort**, int>)(int)(*(uint*)(*(int*)p + 56)))((nint)p, &ptr);
			if (num >= 0)
			{
				strDeviceUuid = new string((char*)ptr);
				global::_003CModule_003E.SysFreeString(ptr);
			}
		}
		return num;
	}

	public unsafe int GetAssociatedWlanDeviceUuidList(ref List<string> deviceUuidList)
	{
		if (m_spEndpointHost.p == null)
		{
			global::_003CModule_003E._ZuneShipAssert(1002u, 1845u);
			return -2147418113;
		}
		if (global::_003CModule_003E.WPP_GLOBAL_Control != Unsafe.AsPointer(ref global::_003CModule_003E.WPP_GLOBAL_Control) && (((int*)global::_003CModule_003E.WPP_GLOBAL_Control)[15] & 2) != 0 && (uint)((byte*)global::_003CModule_003E.WPP_GLOBAL_Control)[57] >= 5u)
		{
			global::_003CModule_003E.WPP_SF_d(((ulong*)global::_003CModule_003E.WPP_GLOBAL_Control)[6], 35, (_GUID*)Unsafe.AsPointer(ref global::_003CModule_003E._003FA0xbf2368f8_002EWPP_DeviceAPI_cpp_Traceguids), m_iDeviceID);
		}
		int num = LoadWlanProvider();
		uint num2 = 0u;
		do
		{
			ushort* ptr = null;
			if (num >= 0)
			{
				IWlanProvider* p = m_spWlanProvider.p;
				num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint, ushort**, int>)(int)(*(uint*)(*(int*)p + 60)))((nint)p, num2, &ptr);
				if (num >= 0)
				{
					string item = new string((char*)ptr);
					deviceUuidList.Add(item);
					global::_003CModule_003E.SysFreeString(ptr);
				}
			}
			num2++;
		}
		while (num >= 0);
		if (-2147024894 == num || -2147024637 == num)
		{
			num = 0;
		}
		if (global::_003CModule_003E.WPP_GLOBAL_Control != Unsafe.AsPointer(ref global::_003CModule_003E.WPP_GLOBAL_Control) && (((int*)global::_003CModule_003E.WPP_GLOBAL_Control)[15] & 2) != 0 && (uint)((byte*)global::_003CModule_003E.WPP_GLOBAL_Control)[57] >= 5u)
		{
			global::_003CModule_003E.WPP_SF_d(((ulong*)global::_003CModule_003E.WPP_GLOBAL_Control)[6], 36, (_GUID*)Unsafe.AsPointer(ref global::_003CModule_003E._003FA0xbf2368f8_002EWPP_DeviceAPI_cpp_Traceguids), num);
		}
		return num;
	}

	public unsafe int AssociateWlanDevice()
	{
		if (m_spEndpointHost.p == null)
		{
			global::_003CModule_003E._ZuneShipAssert(1002u, 1893u);
			return -2147418113;
		}
		int num = LoadWlanProvider();
		if (num >= 0)
		{
			IWlanProvider* p = m_spWlanProvider.p;
			num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, int>)(int)(*(uint*)(*(int*)p + 64)))((nint)p);
		}
		return num;
	}

	public void AssociateWlanDeviceComplete(int hr)
	{
		if (m_AssociateWlanDeviceComplete != null)
		{
			m_AssociateWlanDeviceComplete(this, hr);
		}
	}

	public unsafe int UnassociateWlanDevice()
	{
		if (m_spEndpointHost.p == null)
		{
			global::_003CModule_003E._ZuneShipAssert(1002u, 1921u);
			return -2147418113;
		}
		int num = LoadWlanProvider();
		if (num >= 0)
		{
			IWlanProvider* p = m_spWlanProvider.p;
			num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, int>)(int)(*(uint*)(*(int*)p + 68)))((nint)p);
		}
		return num;
	}

	public void UnassociateWlanDeviceComplete(int hr)
	{
		if (m_UnassociateWlanDeviceComplete != null)
		{
			m_UnassociateWlanDeviceComplete(this, hr);
		}
	}

	public unsafe int UnassociateWlanDeviceUuid(string strDeviceUuid)
	{
		if (m_spEndpointHost.p == null)
		{
			global::_003CModule_003E._ZuneShipAssert(1002u, 1949u);
			return -2147418113;
		}
		int num = 0;
		fixed (ushort* ptr = &Unsafe.As<char, ushort>(ref global::_003CModule_003E.PtrToStringChars(strDeviceUuid)))
		{
			if (ptr != null)
			{
				num = LoadWlanProvider();
				if (num >= 0)
				{
					IWlanProvider* p = m_spWlanProvider.p;
					int num2 = *(int*)p + 72;
					num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, ushort*, int>)(int)(*(uint*)num2))((nint)p, ptr);
				}
			}
			return num;
		}
	}

	public unsafe int IsWlanDeviceDisabled(ref bool bDisabled)
	{
		if (m_spEndpointHost.p == null)
		{
			global::_003CModule_003E._ZuneShipAssert(1002u, 1973u);
			return -2147418113;
		}
		int num = 1;
		int num2 = LoadWlanProvider();
		if (num2 >= 0)
		{
			IWlanProvider* p = m_spWlanProvider.p;
			num2 = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, int*, int>)(int)(*(uint*)(*(int*)p + 76)))((nint)p, &num);
		}
		bool flag = ((num != 0) ? true : false);
		bDisabled = flag;
		return num2;
	}

	public unsafe int IsWlanDeviceUuidDisabled(string strDeviceUuid, ref bool bDisabled)
	{
		if (m_spEndpointHost.p == null)
		{
			global::_003CModule_003E._ZuneShipAssert(1002u, 1995u);
			return -2147418113;
		}
		int num = 0;
		int num2 = 1;
		fixed (ushort* ptr = &Unsafe.As<char, ushort>(ref global::_003CModule_003E.PtrToStringChars(strDeviceUuid)))
		{
			if (ptr != null)
			{
				num = LoadWlanProvider();
				if (num >= 0)
				{
					IWlanProvider* p = m_spWlanProvider.p;
					int num3 = *(int*)p + 80;
					num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, ushort*, int*, int>)(int)(*(uint*)num3))((nint)p, ptr, &num2);
				}
			}
			bool flag = ((num2 != 0) ? true : false);
			bDisabled = flag;
			return num;
		}
	}

	public unsafe int TestDeviceWlan()
	{
		if (m_spEndpointHost.p == null)
		{
			global::_003CModule_003E._ZuneShipAssert(1002u, 2022u);
			return -2147418113;
		}
		int num = LoadWlanProvider();
		if (num >= 0)
		{
			IWlanProvider* p = m_spWlanProvider.p;
			num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, int>)(int)(*(uint*)(*(int*)p + 84)))((nint)p);
		}
		return num;
	}

	public unsafe int CancelTestDeviceWlan()
	{
		if (m_spEndpointHost.p == null)
		{
			global::_003CModule_003E._ZuneShipAssert(1002u, 2041u);
			return -2147418113;
		}
		int num = LoadWlanProvider();
		if (num >= 0)
		{
			IWlanProvider* p = m_spWlanProvider.p;
			num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, int>)(int)(*(uint*)(*(int*)p + 88)))((nint)p);
		}
		return num;
	}

	public unsafe void TestDeviceWlanComplete(int hr)
	{
		uint result = 7u;
		int num = LoadWlanProvider();
		if (num >= 0)
		{
			IWlanProvider* p = m_spWlanProvider.p;
			num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint*, int>)(int)(*(uint*)(*(int*)p + 92)))((nint)p, &result);
			if (num >= 0)
			{
				goto IL_0033;
			}
		}
		if (hr >= 0)
		{
			hr = num;
		}
		goto IL_0033;
		IL_0033:
		if (m_TestDeviceWlanComplete != null)
		{
			m_TestDeviceWlanComplete(this, (WlanTestResultCode)result, hr);
		}
	}

	public unsafe int GetDeviceWlanConnectedSSID(ref string strSSID)
	{
		if (m_spEndpointHost.p == null)
		{
			global::_003CModule_003E._ZuneShipAssert(1002u, 2089u);
			return -2147418113;
		}
		ushort* ptr = null;
		int num = LoadWlanProvider();
		if (num >= 0)
		{
			IWlanProvider* p = m_spWlanProvider.p;
			num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, ushort**, int>)(int)(*(uint*)(*(int*)p + 96)))((nint)p, &ptr);
			if (num >= 0)
			{
				strSSID = new string((char*)ptr);
				global::_003CModule_003E.SysFreeString(ptr);
			}
		}
		return num;
	}

	public unsafe int GetDeviceWlanMediaSyncSSID(ref string strSSID)
	{
		if (m_spEndpointHost.p == null)
		{
			global::_003CModule_003E._ZuneShipAssert(1002u, 2116u);
			return -2147418113;
		}
		ushort* ptr = null;
		int num = LoadWlanProvider();
		if (num >= 0)
		{
			IWlanProvider* p = m_spWlanProvider.p;
			num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, ushort**, int>)(int)(*(uint*)(*(int*)p + 100)))((nint)p, &ptr);
			if (num >= 0)
			{
				strSSID = new string((char*)ptr);
				global::_003CModule_003E.SysFreeString(ptr);
			}
		}
		return num;
	}

	public unsafe int SetDeviceWlanMediaSyncSSID(string strSSID)
	{
		if (m_spEndpointHost.p == null)
		{
			global::_003CModule_003E._ZuneShipAssert(1002u, 2143u);
			return -2147418113;
		}
		fixed (ushort* ptr = &Unsafe.As<char, ushort>(ref global::_003CModule_003E.PtrToStringChars(strSSID)))
		{
			int num;
			if (ptr != null)
			{
				num = LoadWlanProvider();
				if (num >= 0)
				{
					IWlanProvider* p = m_spWlanProvider.p;
					int num2 = *(int*)p + 104;
					num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, ushort*, int>)(int)(*(uint*)num2))((nint)p, ptr);
				}
			}
			else
			{
				num = -2147467261;
			}
			return num;
		}
	}

	public unsafe int GetSyncRelationship(ref ESyncRelationship relationship)
	{
		IEndpointHost* p = m_spEndpointHost.p;
		if (p == null)
		{
			global::_003CModule_003E._ZuneShipAssert(1002u, 2172u);
			return -2147418113;
		}
		ESyncRelationship eSyncRelationship = ESyncRelationship.srNone;
		int num = global::_003CModule_003E.GetEnumProperty_003Cstruct_0020IEndpointHost_002Cenum_0020EEndpointHostProperty_002Cenum_0020ESyncRelationship_003E(p, EEndpointHostProperty.eEndpointHostPropertySyncRelationship, &eSyncRelationship);
		if (num >= 0)
		{
			relationship = eSyncRelationship;
		}
		return num;
	}

	public unsafe int SetSyncRelationship(ESyncRelationship relationship)
	{
		IEndpointHost* p = m_spEndpointHost.p;
		if (p == null)
		{
			global::_003CModule_003E._ZuneShipAssert(1002u, 2196u);
			return -2147418113;
		}
		return ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, EEndpointHostProperty, int, int>)(int)(*(uint*)(*(int*)p + 100)))((nint)p, EEndpointHostProperty.eEndpointHostPropertySyncRelationship, (int)relationship);
	}

	public unsafe int GetPromptGuest(ref bool bPromptGuest)
	{
		IEndpointHost* p = m_spEndpointHost.p;
		if (p == null)
		{
			global::_003CModule_003E._ZuneShipAssert(1002u, 2206u);
			return -2147418113;
		}
		Unsafe.SkipInit(out bool flag);
		int num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, EEndpointHostSetting, bool*, int>)(int)(*(uint*)(*(int*)p + 124)))((nint)p, EEndpointHostSetting.eEndpointHostSettingPromptGuest, &flag);
		if (num >= 0)
		{
			bPromptGuest = flag;
		}
		return num;
	}

	public unsafe int SetPromptGuest([MarshalAs(UnmanagedType.U1)] bool bPromptGuest)
	{
		IEndpointHost* p = m_spEndpointHost.p;
		if (p == null)
		{
			global::_003CModule_003E._ZuneShipAssert(1002u, 2225u);
			return -2147418113;
		}
		return ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, EEndpointHostSetting, byte, int>)(int)(*(uint*)(*(int*)p + 140)))((nint)p, EEndpointHostSetting.eEndpointHostSettingPromptGuest, bPromptGuest ? ((byte)1) : ((byte)0));
	}

	public unsafe int GetPromptLink(ref bool bPromptLink)
	{
		IEndpointHost* p = m_spEndpointHost.p;
		if (p == null)
		{
			global::_003CModule_003E._ZuneShipAssert(1002u, 2236u);
			return -2147418113;
		}
		Unsafe.SkipInit(out bool flag);
		int num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, EEndpointHostSetting, bool*, int>)(int)(*(uint*)(*(int*)p + 124)))((nint)p, EEndpointHostSetting.eEndpointHostSettingPromptLink, &flag);
		if (num >= 0)
		{
			bPromptLink = flag;
		}
		return num;
	}

	public unsafe int SetPromptLink([MarshalAs(UnmanagedType.U1)] bool bPromptLink)
	{
		IEndpointHost* p = m_spEndpointHost.p;
		if (p == null)
		{
			global::_003CModule_003E._ZuneShipAssert(1002u, 2255u);
			return -2147418113;
		}
		return ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, EEndpointHostSetting, byte, int>)(int)(*(uint*)(*(int*)p + 140)))((nint)p, EEndpointHostSetting.eEndpointHostSettingPromptLink, bPromptLink ? ((byte)1) : ((byte)0));
	}

	public unsafe int GetFirmwareVersion(ref string strFirmwareVersion)
	{
		CComPtrMgd_003CIEndpointHost_003E spEndpointHost = m_spEndpointHost;
		if (spEndpointHost.p == null)
		{
			global::_003CModule_003E._ZuneShipAssert(1002u, 2266u);
			return -2147418113;
		}
		ushort* ptr = null;
		IEndpointHost* p = spEndpointHost.p;
		int num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, EEndpointHostProperty, ushort**, int>)(int)(*(uint*)(*(int*)p + 60)))((nint)p, EEndpointHostProperty.eEndpointHostPropertyFirmwareVersion, &ptr);
		if (num >= 0 && ptr != null)
		{
			strFirmwareVersion = new string((char*)ptr);
			global::_003CModule_003E.SysFreeString(ptr);
		}
		return num;
	}

	public unsafe int GetSpaceFree(ref ulong ui64SpaceFree)
	{
		CComPtrMgd_003CIEndpointHost_003E spEndpointHost = m_spEndpointHost;
		if (spEndpointHost.p == null)
		{
			global::_003CModule_003E._ZuneShipAssert(1002u, 2286u);
			return -2147418113;
		}
		ulong num = 0uL;
		IEndpointHost* p = spEndpointHost.p;
		int num2 = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, EEndpointHostProperty, ulong*, int>)(int)(*(uint*)(*(int*)p + 64)))((nint)p, EEndpointHostProperty.eEndpointHostPropertyAvailableSpace, &num);
		if (num2 >= 0)
		{
			ui64SpaceFree = num;
		}
		return num2;
	}

	public unsafe int GetSyncOnConnect(ref bool bSyncOnConnect)
	{
		IEndpointHost* p = m_spEndpointHost.p;
		if (p == null)
		{
			global::_003CModule_003E._ZuneShipAssert(1002u, 2306u);
			return -2147418113;
		}
		Unsafe.SkipInit(out bool flag);
		int num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, EEndpointHostSetting, bool*, int>)(int)(*(uint*)(*(int*)p + 124)))((nint)p, EEndpointHostSetting.eEndpointHostSettingSyncOnConnect, &flag);
		if (num >= 0)
		{
			bSyncOnConnect = flag;
		}
		return num;
	}

	public unsafe int SetSyncOnConnect([MarshalAs(UnmanagedType.U1)] bool bSyncOnConnect)
	{
		IEndpointHost* p = m_spEndpointHost.p;
		if (p == null)
		{
			global::_003CModule_003E._ZuneShipAssert(1002u, 2326u);
			return -2147418113;
		}
		return ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, EEndpointHostSetting, byte, int>)(int)(*(uint*)(*(int*)p + 140)))((nint)p, EEndpointHostSetting.eEndpointHostSettingSyncOnConnect, bSyncOnConnect ? ((byte)1) : ((byte)0));
	}

	public unsafe int GetPercentSpaceReserved(ref uint ulPercentage)
	{
		IEndpointHost* p = m_spEndpointHost.p;
		if (p == null)
		{
			global::_003CModule_003E._ZuneShipAssert(1002u, 2336u);
			return -2147418113;
		}
		Unsafe.SkipInit(out uint num2);
		int num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, EEndpointHostProperty, uint*, int>)(int)(*(uint*)(*(int*)p + 72)))((nint)p, EEndpointHostProperty.eEndpointHostPropertyPercentSpaceReserved, &num2);
		if (num >= 0)
		{
			ulPercentage = num2;
		}
		return num;
	}

	public unsafe int SetPercentSpaceReserved(uint ulPercentage)
	{
		IEndpointHost* p = m_spEndpointHost.p;
		if (p == null)
		{
			global::_003CModule_003E._ZuneShipAssert(1002u, 2356u);
			return -2147418113;
		}
		return ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, EEndpointHostProperty, uint, int>)(int)(*(uint*)(*(int*)p + 104)))((nint)p, EEndpointHostProperty.eEndpointHostPropertyPercentSpaceReserved, ulPercentage);
	}

	public unsafe int DeleteMedia(int[] rgIds, EMediaTypes mediaType, ref ESyncOperationStatus operationStatus)
	{
		if (m_spSyncEngine.p == null)
		{
			global::_003CModule_003E._ZuneShipAssert(1002u, 2366u);
			return -2147418113;
		}
		if (global::_003CModule_003E.WPP_GLOBAL_Control != Unsafe.AsPointer(ref global::_003CModule_003E.WPP_GLOBAL_Control) && (((int*)global::_003CModule_003E.WPP_GLOBAL_Control)[15] & 2) != 0 && (uint)((byte*)global::_003CModule_003E.WPP_GLOBAL_Control)[57] >= 5u)
		{
			global::_003CModule_003E.WPP_SF_d(((ulong*)global::_003CModule_003E.WPP_GLOBAL_Control)[6], 37, (_GUID*)Unsafe.AsPointer(ref global::_003CModule_003E._003FA0xbf2368f8_002EWPP_DeviceAPI_cpp_Traceguids), m_iDeviceID);
		}
		fixed (int* ptr = &rgIds[0])
		{
			int num = ((EMediaTypes.eMediaTypeFolder != mediaType) ? m_syncRules.Remove(rgIds, mediaType, fDeviceFolderIds: false) : m_syncRules.Remove(rgIds, EMediaTypes.eMediaTypeFolder, fDeviceFolderIds: true));
			if (num >= 0)
			{
				num = m_syncRules.Exclude(rgIds, mediaType);
			}
			int num2 = rgIds.Length;
			int[] array = new int[num2];
			int num3 = 0;
			if (0 < num2)
			{
				do
				{
					array[num3] = (int)mediaType;
					num3++;
				}
				while (num3 < (nint)rgIds.LongLength);
			}
			fixed (int* ptr2 = &array[0])
			{
				try
				{
					int* ptr3 = ptr2;
					ESyncOperationStatus eSyncOperationStatus = ESyncOperationStatus.osInvalid;
					if (num >= 0)
					{
						ISyncEngine* p = m_spSyncEngine.p;
						num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, int, int*, int*, ESyncOperationStatus*, int>)(int)(*(uint*)(*(int*)p + 100)))((nint)p, rgIds.Length, ptr, ptr3, &eSyncOperationStatus);
						if (num >= 0)
						{
							operationStatus = eSyncOperationStatus;
						}
					}
					if (global::_003CModule_003E.WPP_GLOBAL_Control != Unsafe.AsPointer(ref global::_003CModule_003E.WPP_GLOBAL_Control) && (((int*)global::_003CModule_003E.WPP_GLOBAL_Control)[15] & 2) != 0 && (uint)((byte*)global::_003CModule_003E.WPP_GLOBAL_Control)[57] >= 5u)
					{
						global::_003CModule_003E.WPP_SF_d(((ulong*)global::_003CModule_003E.WPP_GLOBAL_Control)[6], 38, (_GUID*)Unsafe.AsPointer(ref global::_003CModule_003E._003FA0xbf2368f8_002EWPP_DeviceAPI_cpp_Traceguids), num);
					}
				}
				catch
				{
					//try-fault
					ptr2 = null;
					throw;
				}
			}
			return num;
		}
	}

	public unsafe int ReverseSync(int[] rgIds, EMediaTypes mediaType, ref ESyncOperationStatus operationStatus)
	{
		if (m_spSyncEngine.p == null)
		{
			global::_003CModule_003E._ZuneShipAssert(1002u, 2461u);
			return -2147418113;
		}
		if (global::_003CModule_003E.WPP_GLOBAL_Control != Unsafe.AsPointer(ref global::_003CModule_003E.WPP_GLOBAL_Control) && (((int*)global::_003CModule_003E.WPP_GLOBAL_Control)[15] & 2) != 0 && (uint)((byte*)global::_003CModule_003E.WPP_GLOBAL_Control)[57] >= 5u)
		{
			global::_003CModule_003E.WPP_SF_d(((ulong*)global::_003CModule_003E.WPP_GLOBAL_Control)[6], 39, (_GUID*)Unsafe.AsPointer(ref global::_003CModule_003E._003FA0xbf2368f8_002EWPP_DeviceAPI_cpp_Traceguids), m_iDeviceID);
		}
		fixed (int* ptr = &rgIds[0])
		{
			int num = rgIds.Length;
			int[] array = new int[num];
			int num2 = 0;
			if (0 < num)
			{
				do
				{
					array[num2] = (int)mediaType;
					num2++;
				}
				while (num2 < (nint)rgIds.LongLength);
			}
			int num3;
			fixed (int* ptr2 = &array[0])
			{
				try
				{
					int* ptr3 = ptr2;
					ESyncOperationStatus eSyncOperationStatus = ESyncOperationStatus.osInvalid;
					ISyncEngine* p = m_spSyncEngine.p;
					num3 = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, int, int*, int*, ESyncOperationStatus*, int>)(int)(*(uint*)(*(int*)p + 104)))((nint)p, rgIds.Length, ptr, ptr3, &eSyncOperationStatus);
					if (num3 >= 0)
					{
						operationStatus = eSyncOperationStatus;
					}
					if (global::_003CModule_003E.WPP_GLOBAL_Control != Unsafe.AsPointer(ref global::_003CModule_003E.WPP_GLOBAL_Control) && (((int*)global::_003CModule_003E.WPP_GLOBAL_Control)[15] & 2) != 0 && (uint)((byte*)global::_003CModule_003E.WPP_GLOBAL_Control)[57] >= 5u)
					{
						global::_003CModule_003E.WPP_SF_d(((ulong*)global::_003CModule_003E.WPP_GLOBAL_Control)[6], 40, (_GUID*)Unsafe.AsPointer(ref global::_003CModule_003E._003FA0xbf2368f8_002EWPP_DeviceAPI_cpp_Traceguids), num3);
					}
				}
				catch
				{
					//try-fault
					ptr2 = null;
					throw;
				}
			}
			return num3;
		}
	}

	public unsafe int GetDeviceVideoDeleteSet(out int[] rgIds)
	{
		Unsafe.SkipInit(out CComPtrNtv_003CIMetadataManager_003E cComPtrNtv_003CIMetadataManager_003E);
		*(int*)(&cComPtrNtv_003CIMetadataManager_003E) = 0;
		int num;
		try
		{
			Unsafe.SkipInit(out CComPtrNtv_003CIDeviceContentProvider_003E cComPtrNtv_003CIDeviceContentProvider_003E);
			*(int*)(&cComPtrNtv_003CIDeviceContentProvider_003E) = 0;
			try
			{
				Unsafe.SkipInit(out IntSet intSet);
				global::_003CModule_003E.DataStructs_002EIntSet_002EInitialize(&intSet);
				try
				{
					num = global::_003CModule_003E.GetSingleton((_GUID)global::_003CModule_003E._GUID_6dd7146d_7a19_4fbb_9235_9e6c382fcc71, (void**)(&cComPtrNtv_003CIMetadataManager_003E));
					if (num >= 0)
					{
						num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, _GUID, void**, int>)(int)(*(uint*)(*(int*)(int)(*(uint*)(&cComPtrNtv_003CIMetadataManager_003E)) + 12)))((IntPtr)(*(int*)(&cComPtrNtv_003CIMetadataManager_003E)), (_GUID)global::_003CModule_003E._GUID_7472ae89_073d_420b_9828_51f9d80ca2a6, (void**)(&cComPtrNtv_003CIDeviceContentProvider_003E));
						if (num >= 0)
						{
							num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, int, IntSet*, int>)(int)(*(uint*)(*(int*)(int)(*(uint*)(&cComPtrNtv_003CIDeviceContentProvider_003E)) + 48)))((IntPtr)(*(int*)(&cComPtrNtv_003CIDeviceContentProvider_003E)), m_iDeviceID, &intSet);
							if (num >= 0 && Unsafe.As<IntSet, int>(ref Unsafe.AddByteOffset(ref intSet, 8)) != -1)
							{
								rgIds = new int[global::_003CModule_003E.DataStructs_002EIntSet_002EMemberCount(&intSet)];
								int num2 = Unsafe.As<IntSet, int>(ref Unsafe.AddByteOffset(ref intSet, 4));
								int num3 = 0;
								if (Unsafe.As<IntSet, int>(ref Unsafe.AddByteOffset(ref intSet, 4)) != -1)
								{
									do
									{
										rgIds[num3] = num2;
										num3++;
										num2 = global::_003CModule_003E.DataStructs_002EIntSet_002EGetNextMember(&intSet, num2);
									}
									while (num2 != -1);
								}
							}
						}
					}
				}
				catch
				{
					//try-fault
					global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<IntSet*, void>)(&global::_003CModule_003E.DataStructs_002EIntSet_002E_007Bdtor_007D), &intSet);
					throw;
				}
				global::_003CModule_003E.DataStructs_002EIntSet_002EFreeData(&intSet);
			}
			catch
			{
				//try-fault
				global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<CComPtrNtv_003CIDeviceContentProvider_003E*, void>)(&global::_003CModule_003E.CComPtrNtv_003CIDeviceContentProvider_003E_002E_007Bdtor_007D), &cComPtrNtv_003CIDeviceContentProvider_003E);
				throw;
			}
			global::_003CModule_003E.CComPtrNtv_003CIDeviceContentProvider_003E_002ERelease(&cComPtrNtv_003CIDeviceContentProvider_003E);
		}
		catch
		{
			//try-fault
			global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<CComPtrNtv_003CIMetadataManager_003E*, void>)(&global::_003CModule_003E.CComPtrNtv_003CIMetadataManager_003E_002E_007Bdtor_007D), &cComPtrNtv_003CIMetadataManager_003E);
			throw;
		}
		global::_003CModule_003E.CComPtrNtv_003CIMetadataManager_003E_002ERelease(&cComPtrNtv_003CIMetadataManager_003E);
		return num;
	}

	public unsafe int GetVideoTranscodeOptimization(ref ETranscodeOptimization transcodeOptimization)
	{
		CComPtrMgd_003CIEndpointHost_003E spEndpointHost = m_spEndpointHost;
		if (spEndpointHost.p == null)
		{
			global::_003CModule_003E._ZuneShipAssert(1002u, 2500u);
			return -2147418113;
		}
		ETranscodeOptimization eTranscodeOptimization = ETranscodeOptimization.toOptimizeForSize;
		IEndpointHost* p = spEndpointHost.p;
		int num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, EEndpointHostSetting, int*, int>)(int)(*(uint*)(*(int*)p + 116)))((nint)p, EEndpointHostSetting.eEndpointHostSettingVideoTranscodeOptimization, (int*)(&eTranscodeOptimization));
		if (num >= 0)
		{
			transcodeOptimization = eTranscodeOptimization;
		}
		return num;
	}

	public unsafe int SetVideoTranscodeOptimization(ETranscodeOptimization transcodeOptimization)
	{
		IEndpointHost* p = m_spEndpointHost.p;
		if (p == null)
		{
			global::_003CModule_003E._ZuneShipAssert(1002u, 2519u);
			return -2147418113;
		}
		return ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, EEndpointHostSetting, int, int>)(int)(*(uint*)(*(int*)p + 132)))((nint)p, EEndpointHostSetting.eEndpointHostSettingVideoTranscodeOptimization, (int)transcodeOptimization);
	}

	public unsafe int GetPhotoVideoReverseSync(ref bool bReverseSync)
	{
		CComPtrMgd_003CIEndpointHost_003E spEndpointHost = m_spEndpointHost;
		if (spEndpointHost.p == null)
		{
			global::_003CModule_003E._ZuneShipAssert(1002u, 2530u);
			return -2147418113;
		}
		bool flag = false;
		IEndpointHost* p = spEndpointHost.p;
		int num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, EEndpointHostSetting, bool*, int>)(int)(*(uint*)(*(int*)p + 124)))((nint)p, EEndpointHostSetting.eEndpointHostSettingReverseSyncUGC, &flag);
		if (num >= 0)
		{
			bReverseSync = flag;
		}
		return num;
	}

	public unsafe int SetPhotoVideoReverseSync([MarshalAs(UnmanagedType.U1)] bool bReverseSync)
	{
		IEndpointHost* p = m_spEndpointHost.p;
		if (p == null)
		{
			global::_003CModule_003E._ZuneShipAssert(1002u, 2551u);
			return -2147418113;
		}
		return ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, EEndpointHostSetting, byte, int>)(int)(*(uint*)(*(int*)p + 140)))((nint)p, EEndpointHostSetting.eEndpointHostSettingReverseSyncUGC, bReverseSync ? ((byte)1) : ((byte)0));
	}

	public unsafe int GetDeletePhotoVideoAfterReverseSync(ref bool bDeleteAfterSync)
	{
		CComPtrMgd_003CIEndpointHost_003E spEndpointHost = m_spEndpointHost;
		if (spEndpointHost.p == null)
		{
			global::_003CModule_003E._ZuneShipAssert(1002u, 2563u);
			return -2147418113;
		}
		bool flag = false;
		IEndpointHost* p = spEndpointHost.p;
		int num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, EEndpointHostSetting, bool*, int>)(int)(*(uint*)(*(int*)p + 124)))((nint)p, EEndpointHostSetting.eEndpointHostSettingDeleteUGCAfterSync, &flag);
		if (num >= 0)
		{
			bDeleteAfterSync = flag;
		}
		return num;
	}

	public unsafe int SetDeletePhotoVideoAfterReverseSync([MarshalAs(UnmanagedType.U1)] bool bDeleteAfterSync)
	{
		IEndpointHost* p = m_spEndpointHost.p;
		if (p == null)
		{
			global::_003CModule_003E._ZuneShipAssert(1002u, 2584u);
			return -2147418113;
		}
		return ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, EEndpointHostSetting, byte, int>)(int)(*(uint*)(*(int*)p + 140)))((nint)p, EEndpointHostSetting.eEndpointHostSettingDeleteUGCAfterSync, bDeleteAfterSync ? ((byte)1) : ((byte)0));
	}

	public unsafe int GetCameraRollDestinationFolder(ref string strDestinationFolder)
	{
		CComPtrMgd_003CIEndpointHost_003E spEndpointHost = m_spEndpointHost;
		if (spEndpointHost.p == null)
		{
			global::_003CModule_003E._ZuneShipAssert(1002u, 2596u);
			return -2147418113;
		}
		ushort* ptr = null;
		IEndpointHost* p = spEndpointHost.p;
		int num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, EEndpointHostSetting, ushort**, int>)(int)(*(uint*)(*(int*)p + 112)))((nint)p, EEndpointHostSetting.eEndpointHostCameraRollDestinationFolder, &ptr);
		if (num >= 0 && ptr != null)
		{
			strDestinationFolder = new string((char*)ptr);
			global::_003CModule_003E.SysFreeString(ptr);
		}
		return num;
	}

	public unsafe int SetCameraRollDestinationFolder(string strDestinationFolder)
	{
		if (m_spEndpointHost.p == null)
		{
			global::_003CModule_003E._ZuneShipAssert(1002u, 2619u);
			return -2147418113;
		}
		fixed (ushort* ptr = &Unsafe.As<char, ushort>(ref global::_003CModule_003E.PtrToStringChars(strDestinationFolder)))
		{
			int result;
			if (ptr != null)
			{
				IEndpointHost* p = m_spEndpointHost.p;
				int num = *(int*)p + 128;
				result = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, EEndpointHostSetting, ushort*, int>)(int)(*(uint*)num))((nint)p, EEndpointHostSetting.eEndpointHostCameraRollDestinationFolder, ptr);
			}
			else
			{
				result = -2147418113;
			}
			return result;
		}
	}

	public unsafe int GetSavedDestinationFolder(ref string strDestinationFolder)
	{
		CComPtrMgd_003CIEndpointHost_003E spEndpointHost = m_spEndpointHost;
		if (spEndpointHost.p == null)
		{
			global::_003CModule_003E._ZuneShipAssert(1002u, 2643u);
			return -2147418113;
		}
		ushort* ptr = null;
		IEndpointHost* p = spEndpointHost.p;
		int num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, EEndpointHostSetting, ushort**, int>)(int)(*(uint*)(*(int*)p + 112)))((nint)p, EEndpointHostSetting.eEndpointHostSavedDestinationFolder, &ptr);
		if (num >= 0 && ptr != null)
		{
			strDestinationFolder = new string((char*)ptr);
			global::_003CModule_003E.SysFreeString(ptr);
		}
		return num;
	}

	public unsafe int SetSavedDestinationFolder(string strDestinationFolder)
	{
		if (m_spEndpointHost.p == null)
		{
			global::_003CModule_003E._ZuneShipAssert(1002u, 2666u);
			return -2147418113;
		}
		fixed (ushort* ptr = &Unsafe.As<char, ushort>(ref global::_003CModule_003E.PtrToStringChars(strDestinationFolder)))
		{
			int result;
			if (ptr != null)
			{
				IEndpointHost* p = m_spEndpointHost.p;
				int num = *(int*)p + 128;
				result = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, EEndpointHostSetting, ushort*, int>)(int)(*(uint*)num))((nint)p, EEndpointHostSetting.eEndpointHostSavedDestinationFolder, ptr);
			}
			else
			{
				result = -2147418113;
			}
			return result;
		}
	}

	public unsafe int GetPhotoTranscodeSetting(ref ETranscodePhotoSetting ePhotoSetting)
	{
		CComPtrMgd_003CIEndpointHost_003E spEndpointHost = m_spEndpointHost;
		if (spEndpointHost.p == null)
		{
			global::_003CModule_003E._ZuneShipAssert(1002u, 2690u);
			return -2147418113;
		}
		ETranscodePhotoSetting eTranscodePhotoSetting = ETranscodePhotoSetting.tsPhotoSettingDevicePreferred;
		IEndpointHost* p = spEndpointHost.p;
		int num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, EEndpointHostSetting, int*, int>)(int)(*(uint*)(*(int*)p + 116)))((nint)p, EEndpointHostSetting.eEndpointHostSettingPhotoTranscodeSetting, (int*)(&eTranscodePhotoSetting));
		if (num >= 0)
		{
			ePhotoSetting = eTranscodePhotoSetting;
		}
		return num;
	}

	public unsafe int SetPhotoTranscodeSetting(ETranscodePhotoSetting ePhotoSetting)
	{
		IEndpointHost* p = m_spEndpointHost.p;
		if (p == null)
		{
			global::_003CModule_003E._ZuneShipAssert(1002u, 2709u);
			return -2147418113;
		}
		return ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, EEndpointHostSetting, int, int>)(int)(*(uint*)(*(int*)p + 132)))((nint)p, EEndpointHostSetting.eEndpointHostSettingPhotoTranscodeSetting, (int)ePhotoSetting);
	}

	public unsafe int GetAudioTranscodeParams(ref int audioThresholdBitRate, ref int audioTargetBitRate)
	{
		uint num = 0u;
		uint num2 = 0u;
		IEndpointHost* p = m_spEndpointHost.p;
		int num3 = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, EEndpointHostSetting, uint*, int>)(int)(*(uint*)(*(int*)p + 120)))((nint)p, EEndpointHostSetting.eEndpointHostSettingAudioThresholdBitRate, &num);
		if (num3 >= 0)
		{
			p = m_spEndpointHost.p;
			num3 = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, EEndpointHostSetting, uint*, int>)(int)(*(uint*)(*(int*)p + 120)))((nint)p, EEndpointHostSetting.eEndpointHostSettingAudioTargetBitRate, &num2);
			if (num3 >= 0)
			{
				audioThresholdBitRate = (int)num;
				audioTargetBitRate = (int)num2;
			}
		}
		return num3;
	}

	public unsafe int SetAudioTranscodeParams(int audioThresholdBitRate, int audioTargetBitRate)
	{
		IEndpointHost* p = m_spEndpointHost.p;
		int num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, EEndpointHostSetting, uint, int>)(int)(*(uint*)(*(int*)p + 136)))((nint)p, EEndpointHostSetting.eEndpointHostSettingAudioThresholdBitRate, (uint)audioThresholdBitRate);
		if (num >= 0)
		{
			p = m_spEndpointHost.p;
			num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, EEndpointHostSetting, uint, int>)(int)(*(uint*)(*(int*)p + 136)))((nint)p, EEndpointHostSetting.eEndpointHostSettingAudioTargetBitRate, (uint)audioTargetBitRate);
		}
		return num;
	}

	public unsafe int GetLocalizedDevicePath(ref string strPath)
	{
		if (m_spEndpointHost.p == null)
		{
			global::_003CModule_003E._ZuneShipAssert(1002u, 2770u);
			return -2147418113;
		}
		ushort* ptr = null;
		fixed (ushort* ptr2 = &Unsafe.As<char, ushort>(ref global::_003CModule_003E.PtrToStringChars(strPath)))
		{
			IEndpointHost* p = m_spEndpointHost.p;
			int num = *(int*)p + 144;
			int num2 = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, EEndpointHostAction, ushort*, ushort**, int>)(int)(*(uint*)num))((nint)p, EEndpointHostAction.eEndpointHostActionLocalizeDevicePath, ptr2, &ptr);
			if (num2 >= 0 && ptr != null)
			{
				strPath = new string((char*)ptr);
				global::_003CModule_003E.SysFreeString(ptr);
			}
			return num2;
		}
	}

	public unsafe int ClearCache()
	{
		if (m_spEndpointHost.p == null)
		{
			global::_003CModule_003E._ZuneShipAssert(1002u, 2794u);
			return -2147418113;
		}
		DeviceList instance = DeviceList.Instance;
		int result;
		if (instance != null)
		{
			IEndpointHost* p = m_spEndpointHost.p;
			result = instance.ForgetEndpoint(p);
		}
		else
		{
			result = -2147418113;
		}
		return result;
	}

	public unsafe int ClearRules()
	{
		IEndpointHost* p = m_spEndpointHost.p;
		if (p == null)
		{
			global::_003CModule_003E._ZuneShipAssert(1002u, 2815u);
			return -2147418113;
		}
		return ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, EEndpointHostAction, int>)(int)(*(uint*)(*(int*)p + 148)))((nint)p, EEndpointHostAction.eEndpointHostActionClearRules);
	}

	public unsafe int ClearManualModeRules()
	{
		IEndpointHost* p = m_spEndpointHost.p;
		if (p == null)
		{
			global::_003CModule_003E._ZuneShipAssert(1002u, 2826u);
			return -2147418113;
		}
		return ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, EEndpointHostAction, int>)(int)(*(uint*)(*(int*)p + 148)))((nint)p, EEndpointHostAction.eEndpointHostActionClearManualModeRules);
	}

	public unsafe int DeleteAllGuestContent(ref ESyncOperationStatus operationStatus)
	{
		CComPtrMgd_003CISyncEngine_003E spSyncEngine = m_spSyncEngine;
		if (spSyncEngine.p == null)
		{
			global::_003CModule_003E._ZuneShipAssert(1002u, 2836u);
			return -2147418113;
		}
		ESyncOperationStatus eSyncOperationStatus = ESyncOperationStatus.osInvalid;
		ISyncEngine* p = spSyncEngine.p;
		int num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, ESyncOperationStatus*, int>)(int)(*(uint*)(*(int*)p + 112)))((nint)p, &eSyncOperationStatus);
		if (num >= 0)
		{
			operationStatus = eSyncOperationStatus;
		}
		return num;
	}

	public unsafe int ForceAppUpdate()
	{
		IEndpointHost* p = m_spEndpointHost.p;
		if (p == null)
		{
			global::_003CModule_003E._ZuneShipAssert(1002u, 2853u);
			return -2147418113;
		}
		return ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, EEndpointHostAction, int>)(int)(*(uint*)(*(int*)p + 148)))((nint)p, EEndpointHostAction.eEndpointHostActionForceAppUpdate);
	}

	public int SyncBeginCallback()
	{
		if (m_SyncBegan != null)
		{
			m_SyncBegan(this);
		}
		return 0;
	}

	public unsafe int SyncProgressCallback(uint uiPercentComplete, uint uiPercentItemComplete, uint uiPercentTranscodeComplete, ushort* bstrGroup, ushort* bstrTitle, ESyncEngineState engineState)
	{
		if (m_SyncProgressed != null)
		{
			m_SyncProgressed(this, uiPercentComplete, uiPercentItemComplete, uiPercentTranscodeComplete, new string((char*)bstrGroup), new string((char*)bstrTitle), engineState);
		}
		return 0;
	}

	public int SyncCompleteCallback(int hrResult)
	{
		if (m_SyncCompleted != null)
		{
			ESyncEventReason reason = ((hrResult < 0) ? ESyncEventReason.eSyncEventFailed : ESyncEventReason.eSyncEventSucceeded);
			m_SyncCompleted(this, reason);
		}
		return 0;
	}

	public unsafe void FriendlyNameChanged(ushort* wszFriendlyName)
	{
		if (m_FriendlyNameChanged != null)
		{
			m_FriendlyNameChanged(this, new string((char*)wszFriendlyName));
		}
	}

	public unsafe void EndpointStatusChanged(int hrEnumeration, EEndpointStatus eDeviceStatus)
	{
		if (global::_003CModule_003E.WPP_GLOBAL_Control != Unsafe.AsPointer(ref global::_003CModule_003E.WPP_GLOBAL_Control) && (((int*)global::_003CModule_003E.WPP_GLOBAL_Control)[15] & 2) != 0 && (uint)((byte*)global::_003CModule_003E.WPP_GLOBAL_Control)[57] >= 5u)
		{
			global::_003CModule_003E.WPP_SF_Dd(((ulong*)global::_003CModule_003E.WPP_GLOBAL_Control)[6], 41, (_GUID*)Unsafe.AsPointer(ref global::_003CModule_003E._003FA0xbf2368f8_002EWPP_DeviceAPI_cpp_Traceguids), (uint)hrEnumeration, m_iDeviceID);
		}
		m_hrEnumeration = hrEnumeration;
		if (eDeviceStatus == EEndpointStatus.eEndpointStatusAvailable && !m_fInitializationCompleted)
		{
			int num = CompleteInitialization();
			if (-1072885173 == num || -1072885172 == num)
			{
				DeviceList.Instance.HideDevice(this);
			}
		}
		m_eLastDeviceStatus = eDeviceStatus;
		if (m_DeviceStatusChanged != null)
		{
			m_DeviceStatusChanged(this, m_hrEnumeration, eDeviceStatus);
		}
		if (global::_003CModule_003E.WPP_GLOBAL_Control != Unsafe.AsPointer(ref global::_003CModule_003E.WPP_GLOBAL_Control) && (((int*)global::_003CModule_003E.WPP_GLOBAL_Control)[15] & 2) != 0 && (uint)((byte*)global::_003CModule_003E.WPP_GLOBAL_Control)[57] >= 5u)
		{
			global::_003CModule_003E.WPP_SF_(((ulong*)global::_003CModule_003E.WPP_GLOBAL_Control)[6], 42, (_GUID*)Unsafe.AsPointer(ref global::_003CModule_003E._003FA0xbf2368f8_002EWPP_DeviceAPI_cpp_Traceguids));
		}
	}

	public void FormatComplete(int hrResult)
	{
		if (m_FormatComplete != null)
		{
			m_FormatComplete(this, hrResult);
		}
	}

	public unsafe int FileExistsForTranscode(int iMediaId, EMediaTypes eMediaType, ref string strTranscodedFileName)
	{
		CComPtrMgd_003CISyncEngine_003E spSyncEngine = m_spSyncEngine;
		if (spSyncEngine.p == null)
		{
			global::_003CModule_003E._ZuneShipAssert(1002u, 2986u);
			return -2147418113;
		}
		ushort* ptr = null;
		ISyncEngine* p = spSyncEngine.p;
		int num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, int, int, ushort**, int>)(int)(*(uint*)(*(int*)p + 96)))((nint)p, iMediaId, (int)eMediaType, &ptr);
		if (num >= 0 && ptr != null)
		{
			strTranscodedFileName = new string((char*)ptr);
			global::_003CModule_003E.SysFreeString(ptr);
		}
		return num;
	}

	internal unsafe Device()
	{
		CComPtrMgd_003CIEndpointHost_003E spEndpointHost = new CComPtrMgd_003CIEndpointHost_003E();
		try
		{
			m_spEndpointHost = spEndpointHost;
			CComPtrMgd_003CISyncEngine_003E spSyncEngine = new CComPtrMgd_003CISyncEngine_003E();
			try
			{
				m_spSyncEngine = spSyncEngine;
				CComPtrMgd_003CIWlanProvider_003E spWlanProvider = new CComPtrMgd_003CIWlanProvider_003E();
				try
				{
					m_spWlanProvider = spWlanProvider;
					CComPtrMgd_003CDeviceMediator_003E spDeviceMediator = new CComPtrMgd_003CDeviceMediator_003E();
					try
					{
						m_spDeviceMediator = spDeviceMediator;
						base._002Ector();
						if (global::_003CModule_003E.WPP_GLOBAL_Control != Unsafe.AsPointer(ref global::_003CModule_003E.WPP_GLOBAL_Control) && (((int*)global::_003CModule_003E.WPP_GLOBAL_Control)[15] & 2) != 0 && (uint)((byte*)global::_003CModule_003E.WPP_GLOBAL_Control)[57] >= 5u)
						{
							global::_003CModule_003E.WPP_SF_(((ulong*)global::_003CModule_003E.WPP_GLOBAL_Control)[6], 10, (_GUID*)Unsafe.AsPointer(ref global::_003CModule_003E._003FA0xbf2368f8_002EWPP_DeviceAPI_cpp_Traceguids));
						}
						m_Lock = new object();
						m_eLastDeviceStatus = EEndpointStatus.eEndpointStatusUndefined;
						if (global::_003CModule_003E.WPP_GLOBAL_Control != Unsafe.AsPointer(ref global::_003CModule_003E.WPP_GLOBAL_Control) && (((int*)global::_003CModule_003E.WPP_GLOBAL_Control)[15] & 2) != 0 && (uint)((byte*)global::_003CModule_003E.WPP_GLOBAL_Control)[57] >= 5u)
						{
							global::_003CModule_003E.WPP_SF_(((ulong*)global::_003CModule_003E.WPP_GLOBAL_Control)[6], 11, (_GUID*)Unsafe.AsPointer(ref global::_003CModule_003E._003FA0xbf2368f8_002EWPP_DeviceAPI_cpp_Traceguids));
						}
						return;
					}
					catch
					{
						//try-fault
						((IDisposable)m_spDeviceMediator).Dispose();
						throw;
					}
				}
				catch
				{
					//try-fault
					((IDisposable)m_spWlanProvider).Dispose();
					throw;
				}
			}
			catch
			{
				//try-fault
				((IDisposable)m_spSyncEngine).Dispose();
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

	internal unsafe int Initialize(IEndpointHost* pEndpointHost)
	{
		ManagedLock managedLock = null;
		int num = 0;
		if (pEndpointHost == null)
		{
			if (global::_003CModule_003E.WPP_GLOBAL_Control != Unsafe.AsPointer(ref global::_003CModule_003E.WPP_GLOBAL_Control) && (((int*)global::_003CModule_003E.WPP_GLOBAL_Control)[15] & 2) != 0 && (uint)((byte*)global::_003CModule_003E.WPP_GLOBAL_Control)[57] >= 5u)
			{
				global::_003CModule_003E.WPP_SF_(((ulong*)global::_003CModule_003E.WPP_GLOBAL_Control)[6], 14, (_GUID*)Unsafe.AsPointer(ref global::_003CModule_003E._003FA0xbf2368f8_002EWPP_DeviceAPI_cpp_Traceguids));
			}
			return -2147467261;
		}
		if (global::_003CModule_003E.WPP_GLOBAL_Control != Unsafe.AsPointer(ref global::_003CModule_003E.WPP_GLOBAL_Control) && (((int*)global::_003CModule_003E.WPP_GLOBAL_Control)[15] & 2) != 0 && (uint)((byte*)global::_003CModule_003E.WPP_GLOBAL_Control)[57] >= 5u)
		{
			global::_003CModule_003E.WPP_SF_(((ulong*)global::_003CModule_003E.WPP_GLOBAL_Control)[6], 15, (_GUID*)Unsafe.AsPointer(ref global::_003CModule_003E._003FA0xbf2368f8_002EWPP_DeviceAPI_cpp_Traceguids));
		}
		ManagedLock managedLock2 = new ManagedLock(m_Lock);
		try
		{
			managedLock = managedLock2;
			m_fInitializationCompleted = false;
			CComPtrMgd_003CIEndpointHost_003E spEndpointHost = m_spEndpointHost;
			if (spEndpointHost.p != pEndpointHost)
			{
				spEndpointHost.op_Assign(pEndpointHost);
			}
			EEndpointStatus eLastDeviceStatus = EEndpointStatus.eEndpointStatusUndefined;
			if (global::_003CModule_003E.GetEnumProperty_003Cstruct_0020IEndpointHost_002Cenum_0020EEndpointHostProperty_002Cenum_0020EEndpointStatus_003E(m_spEndpointHost.p, EEndpointHostProperty.eEndpointHostPropertyEndpointStatus, &eLastDeviceStatus) >= 0)
			{
				m_eLastDeviceStatus = eLastDeviceStatus;
			}
			else
			{
				m_eLastDeviceStatus = EEndpointStatus.eEndpointStatusUndefined;
			}
			SyncRules syncRules = m_syncRules;
			if (syncRules != null)
			{
				((IDisposable)syncRules).Dispose();
				m_syncRules = null;
			}
			if ((m_syncRules = new SyncRules(pEndpointHost)) == null)
			{
				if (global::_003CModule_003E.WPP_GLOBAL_Control != Unsafe.AsPointer(ref global::_003CModule_003E.WPP_GLOBAL_Control) && (((int*)global::_003CModule_003E.WPP_GLOBAL_Control)[15] & 2) != 0 && (uint)((byte*)global::_003CModule_003E.WPP_GLOBAL_Control)[57] >= 2u)
				{
					global::_003CModule_003E.WPP_SF_d(((ulong*)global::_003CModule_003E.WPP_GLOBAL_Control)[6], 16, (_GUID*)Unsafe.AsPointer(ref global::_003CModule_003E._003FA0xbf2368f8_002EWPP_DeviceAPI_cpp_Traceguids), 0);
				}
				num = -2147024882;
			}
			m_spSyncEngine.Release();
			DeviceMediator* p = m_spDeviceMediator.p;
			Unsafe.SkipInit(out CComPtrNtv_003CDeviceMediator_003E cComPtrNtv_003CDeviceMediator_003E);
			global::_003CModule_003E.CComPtrNtv_003CDeviceMediator_003E_002E_007Bctor_007D(&cComPtrNtv_003CDeviceMediator_003E, p);
			try
			{
				if (num >= 0)
				{
					DeviceMediator* ptr = (DeviceMediator*)global::_003CModule_003E.@new(36u);
					DeviceMediator* lp;
					try
					{
						if (ptr != null)
						{
							IEndpointHost* p2 = m_spEndpointHost.p;
							lp = global::_003CModule_003E.DeviceMediator_002E_007Bctor_007D(ptr, this, p2);
						}
						else
						{
							lp = null;
						}
					}
					catch
					{
						//try-fault
						global::_003CModule_003E.delete(ptr);
						throw;
					}
					m_spDeviceMediator.op_Assign(lp);
					if (m_spDeviceMediator.p == null)
					{
						num = -2147024882;
					}
				}
				if (*(int*)(&cComPtrNtv_003CDeviceMediator_003E) != 0)
				{
					global::_003CModule_003E.DeviceMediator_002EShutdown((DeviceMediator*)(int)(*(uint*)(&cComPtrNtv_003CDeviceMediator_003E)));
				}
				global::_003CModule_003E.CComPtrNtv_003CDeviceMediator_003E_002ERelease(&cComPtrNtv_003CDeviceMediator_003E);
				m_spWlanProvider.Release();
				m_fClientUpdateRequired = false;
				m_fFirmwareUpdateRequired = false;
				m_hrEnumeration = 0;
				if (num >= 0)
				{
					IEndpointHost* p3 = m_spEndpointHost.p;
					Unsafe.SkipInit(out int iDeviceID);
					num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, EEndpointHostProperty, int*, int>)(int)(*(uint*)(*(int*)p3 + 68)))((nint)p3, EEndpointHostProperty.eEndpointHostPropertyDatabaseEndpointId, &iDeviceID);
					m_iDeviceID = iDeviceID;
					if (num >= 0)
					{
						ushort* ptr2 = null;
						IEndpointHost* p4 = m_spEndpointHost.p;
						num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, EEndpointHostProperty, ushort**, int>)(int)(*(uint*)(*(int*)p4 + 60)))((nint)p4, EEndpointHostProperty.eEndpointHostPropertyEndpointId, &ptr2);
						if (num >= 0 && ptr2 != null)
						{
							m_strEndpointId = new string((char*)ptr2);
						}
						global::_003CModule_003E.SysFreeString(ptr2);
						if (num >= 0)
						{
							Unsafe.SkipInit(out CComPtrNtv_003CIDeviceAssetProvider_003E cComPtrNtv_003CIDeviceAssetProvider_003E);
							*(int*)(&cComPtrNtv_003CIDeviceAssetProvider_003E) = 0;
							try
							{
								if (global::_003CModule_003E.GetEndpointHostInterfaceProperty_003Cstruct_0020IDeviceAssetProvider_003E(m_spEndpointHost.p, EEndpointHostProperty.eEndpointHostPropertyDeviceAssetProvider, (IDeviceAssetProvider**)(&cComPtrNtv_003CIDeviceAssetProvider_003E)) >= 0)
								{
									CreateDeviceAssetSet((IDeviceAssetProvider*)(int)(*(uint*)(&cComPtrNtv_003CIDeviceAssetProvider_003E)));
								}
							}
							catch
							{
								//try-fault
								global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<CComPtrNtv_003CIDeviceAssetProvider_003E*, void>)(&global::_003CModule_003E.CComPtrNtv_003CIDeviceAssetProvider_003E_002E_007Bdtor_007D), &cComPtrNtv_003CIDeviceAssetProvider_003E);
								throw;
							}
							global::_003CModule_003E.CComPtrNtv_003CIDeviceAssetProvider_003E_002ERelease(&cComPtrNtv_003CIDeviceAssetProvider_003E);
						}
					}
				}
				if (global::_003CModule_003E.WPP_GLOBAL_Control != Unsafe.AsPointer(ref global::_003CModule_003E.WPP_GLOBAL_Control) && (((int*)global::_003CModule_003E.WPP_GLOBAL_Control)[15] & 2) != 0 && (uint)((byte*)global::_003CModule_003E.WPP_GLOBAL_Control)[57] >= 5u)
				{
					global::_003CModule_003E.WPP_SF_(((ulong*)global::_003CModule_003E.WPP_GLOBAL_Control)[6], 17, (_GUID*)Unsafe.AsPointer(ref global::_003CModule_003E._003FA0xbf2368f8_002EWPP_DeviceAPI_cpp_Traceguids));
				}
			}
			catch
			{
				//try-fault
				global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<CComPtrNtv_003CDeviceMediator_003E*, void>)(&global::_003CModule_003E.CComPtrNtv_003CDeviceMediator_003E_002E_007Bdtor_007D), &cComPtrNtv_003CDeviceMediator_003E);
				throw;
			}
			global::_003CModule_003E.CComPtrNtv_003CDeviceMediator_003E_002ERelease(&cComPtrNtv_003CDeviceMediator_003E);
		}
		catch
		{
			//try-fault
			((IDisposable)managedLock).Dispose();
			throw;
		}
		((IDisposable)managedLock).Dispose();
		return num;
	}

	internal unsafe int CompleteInitialization()
	{
		ManagedLock managedLock = null;
		if (global::_003CModule_003E.WPP_GLOBAL_Control != Unsafe.AsPointer(ref global::_003CModule_003E.WPP_GLOBAL_Control) && (((int*)global::_003CModule_003E.WPP_GLOBAL_Control)[15] & 2) != 0 && (uint)((byte*)global::_003CModule_003E.WPP_GLOBAL_Control)[57] >= 5u)
		{
			global::_003CModule_003E.WPP_SF_(((ulong*)global::_003CModule_003E.WPP_GLOBAL_Control)[6], 18, (_GUID*)Unsafe.AsPointer(ref global::_003CModule_003E._003FA0xbf2368f8_002EWPP_DeviceAPI_cpp_Traceguids));
		}
		bool flag = false;
		bool flag2 = false;
		ManagedLock managedLock2 = new ManagedLock(m_Lock);
		int num;
		try
		{
			managedLock = managedLock2;
			num = GetCapability(EEndpointCapability.eEndpointCapabilityFirmwareUpdate, ref m_fFirmwareUpdateSupported);
			bool flag3 = false;
			if (num >= 0)
			{
				IEndpointHost* p = m_spEndpointHost.p;
				num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, EEndpointHostProperty, bool*, int>)(int)(*(uint*)(*(int*)p + 76)))((nint)p, EEndpointHostProperty.eEndpointHostPropertyIsAvailable, &flag3);
				if (flag3)
				{
					if (num < 0)
					{
						goto IL_03eb;
					}
					num = IsFirmwareProcessInProgress(&flag, &flag2);
				}
				if (num >= 0)
				{
					if (flag2)
					{
						num = m_firmwareUpdater.Restorer.ContinueFirmwareProcess(OnFirmwareProcessComplete);
						if (num < 0)
						{
							if (global::_003CModule_003E.WPP_GLOBAL_Control != Unsafe.AsPointer(ref global::_003CModule_003E.WPP_GLOBAL_Control) && (((int*)global::_003CModule_003E.WPP_GLOBAL_Control)[15] & 2) != 0 && (uint)((byte*)global::_003CModule_003E.WPP_GLOBAL_Control)[57] >= 2u)
							{
								global::_003CModule_003E.WPP_SF_d(((ulong*)global::_003CModule_003E.WPP_GLOBAL_Control)[6], 19, (_GUID*)Unsafe.AsPointer(ref global::_003CModule_003E._003FA0xbf2368f8_002EWPP_DeviceAPI_cpp_Traceguids), num);
							}
							goto IL_03e3;
						}
					}
					else
					{
						if (!flag)
						{
							ESyncRelationship eSyncRelationship = ESyncRelationship.srNone;
							EEndpointCompatibilityStatus eEndpointCompatibilityStatus = (EEndpointCompatibilityStatus)0;
							if (!flag3)
							{
								goto IL_032a;
							}
							ushort* ptr = null;
							IEndpointHost* p2 = m_spEndpointHost.p;
							num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, EEndpointHostProperty, ushort**, int>)(int)(*(uint*)(*(int*)p2 + 60)))((nint)p2, EEndpointHostProperty.eEndpointHostPropertyCanonicalName, &ptr);
							if (num >= 0 && ptr != null)
							{
								m_strCanonicalName = new string((char*)ptr);
							}
							global::_003CModule_003E.SysFreeString(ptr);
							if (num >= 0)
							{
								global::_003CModule_003E.GetEnumProperty_003Cstruct_0020IEndpointHost_002Cenum_0020EEndpointHostProperty_002Cenum_0020ESyncRelationship_003E(m_spEndpointHost.p, EEndpointHostProperty.eEndpointHostPropertySyncRelationship, &eSyncRelationship);
								if (IsConnectedWirelessly && eSyncRelationship != ESyncRelationship.srSyncWithThisMachine)
								{
									if (global::_003CModule_003E.WPP_GLOBAL_Control != Unsafe.AsPointer(ref global::_003CModule_003E.WPP_GLOBAL_Control) && (((int*)global::_003CModule_003E.WPP_GLOBAL_Control)[15] & 2) != 0 && (uint)((byte*)global::_003CModule_003E.WPP_GLOBAL_Control)[57] >= 5u)
									{
										global::_003CModule_003E.WPP_SF_D(((ulong*)global::_003CModule_003E.WPP_GLOBAL_Control)[6], 21, (_GUID*)Unsafe.AsPointer(ref global::_003CModule_003E._003FA0xbf2368f8_002EWPP_DeviceAPI_cpp_Traceguids), (uint)eSyncRelationship);
									}
									num = -1072885172;
								}
								if (num >= 0)
								{
									num = global::_003CModule_003E.GetEnumProperty_003Cstruct_0020IEndpointHost_002Cenum_0020EEndpointHostProperty_002Cenum_0020EEndpointCompatibilityStatus_003E(m_spEndpointHost.p, EEndpointHostProperty.eEndpointHostPropertyCompatibilityStatus, &eEndpointCompatibilityStatus);
									if (num >= 0)
									{
										if ((EEndpointCompatibilityStatus)3 == eEndpointCompatibilityStatus)
										{
											m_fClientUpdateRequired = true;
										}
										else if ((EEndpointCompatibilityStatus)2 == eEndpointCompatibilityStatus)
										{
											m_fFirmwareUpdateRequired = true;
										}
										if ((m_fClientUpdateRequired || m_fFirmwareUpdateRequired) && IsConnectedWirelessly)
										{
											if (global::_003CModule_003E.WPP_GLOBAL_Control != Unsafe.AsPointer(ref global::_003CModule_003E.WPP_GLOBAL_Control) && (((int*)global::_003CModule_003E.WPP_GLOBAL_Control)[15] & 2) != 0 && (uint)((byte*)global::_003CModule_003E.WPP_GLOBAL_Control)[57] >= 5u)
											{
												global::_003CModule_003E.WPP_SF_(((ulong*)global::_003CModule_003E.WPP_GLOBAL_Control)[6], 22, (_GUID*)Unsafe.AsPointer(ref global::_003CModule_003E._003FA0xbf2368f8_002EWPP_DeviceAPI_cpp_Traceguids));
											}
											num = -1072885173;
										}
										if (num >= 0)
										{
											Unsafe.SkipInit(out CComPtrNtv_003CISyncEngine_003E cComPtrNtv_003CISyncEngine_003E);
											*(int*)(&cComPtrNtv_003CISyncEngine_003E) = 0;
											try
											{
												m_spSyncEngine.Release();
												num = global::_003CModule_003E.GetInterfaceProperty_003Cstruct_0020IEndpointHost_002Cenum_0020EEndpointHostProperty_002Cstruct_0020ISyncEngine_003E(m_spEndpointHost.p, EEndpointHostProperty.eEndpointHostPropertySyncEngine, (ISyncEngine**)(&cComPtrNtv_003CISyncEngine_003E));
												if (num >= 0)
												{
													ISyncEngine* p3 = (ISyncEngine*)(int)(*(uint*)(&cComPtrNtv_003CISyncEngine_003E));
													*(int*)(&cComPtrNtv_003CISyncEngine_003E) = 0;
													m_spSyncEngine.Attach(p3);
												}
											}
											catch
											{
												//try-fault
												global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<CComPtrNtv_003CISyncEngine_003E*, void>)(&global::_003CModule_003E.CComPtrNtv_003CISyncEngine_003E_002E_007Bdtor_007D), &cComPtrNtv_003CISyncEngine_003E);
												throw;
											}
											global::_003CModule_003E.CComPtrNtv_003CISyncEngine_003E_002ERelease(&cComPtrNtv_003CISyncEngine_003E);
											goto IL_032a;
										}
									}
								}
							}
							goto IL_03eb;
						}
						num = m_firmwareUpdater.ContinueFirmwareProcess(OnFirmwareProcessComplete);
						if (num < 0)
						{
							if (global::_003CModule_003E.WPP_GLOBAL_Control != Unsafe.AsPointer(ref global::_003CModule_003E.WPP_GLOBAL_Control) && (((int*)global::_003CModule_003E.WPP_GLOBAL_Control)[15] & 2) != 0 && (uint)((byte*)global::_003CModule_003E.WPP_GLOBAL_Control)[57] >= 2u)
							{
								global::_003CModule_003E.WPP_SF_d(((ulong*)global::_003CModule_003E.WPP_GLOBAL_Control)[6], 20, (_GUID*)Unsafe.AsPointer(ref global::_003CModule_003E._003FA0xbf2368f8_002EWPP_DeviceAPI_cpp_Traceguids), num);
							}
							goto IL_03e3;
						}
					}
					goto IL_03e7;
				}
			}
			goto IL_03eb;
			IL_032a:
			if (num >= 0)
			{
				Unsafe.SkipInit(out CComPtrNtv_003CIGasGauge_003E cComPtrNtv_003CIGasGauge_003E);
				*(int*)(&cComPtrNtv_003CIGasGauge_003E) = 0;
				try
				{
					Unsafe.SkipInit(out CComPtrNtv_003CIGasGauge_003E cComPtrNtv_003CIGasGauge_003E2);
					*(int*)(&cComPtrNtv_003CIGasGauge_003E2) = 0;
					try
					{
						num = global::_003CModule_003E.GetInterfaceProperty_003Cstruct_0020IEndpointHost_002Cenum_0020EEndpointHostProperty_002Cstruct_0020IGasGauge_003E(m_spEndpointHost.p, EEndpointHostProperty.eEndpointHostPropertyPredictedGasGauge, (IGasGauge**)(&cComPtrNtv_003CIGasGauge_003E));
						if (num >= 0)
						{
							if (null == m_predictedGasGauge)
							{
								m_predictedGasGauge = new GasGauge((IGasGauge*)(int)(*(uint*)(&cComPtrNtv_003CIGasGauge_003E)));
							}
							num = global::_003CModule_003E.GetInterfaceProperty_003Cstruct_0020IEndpointHost_002Cenum_0020EEndpointHostProperty_002Cstruct_0020IGasGauge_003E(m_spEndpointHost.p, EEndpointHostProperty.eEndpointHostPropertyActualGasGauge, (IGasGauge**)(&cComPtrNtv_003CIGasGauge_003E2));
							if (num >= 0)
							{
								if (null == m_actualGasGauge)
								{
									m_actualGasGauge = new GasGauge((IGasGauge*)(int)(*(uint*)(&cComPtrNtv_003CIGasGauge_003E2)));
								}
								if (m_predictedGasGauge == null || m_actualGasGauge == null)
								{
									num = -2147024882;
								}
							}
						}
					}
					catch
					{
						//try-fault
						global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<CComPtrNtv_003CIGasGauge_003E*, void>)(&global::_003CModule_003E.CComPtrNtv_003CIGasGauge_003E_002E_007Bdtor_007D), &cComPtrNtv_003CIGasGauge_003E2);
						throw;
					}
					global::_003CModule_003E.CComPtrNtv_003CIGasGauge_003E_002ERelease(&cComPtrNtv_003CIGasGauge_003E2);
				}
				catch
				{
					//try-fault
					global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<CComPtrNtv_003CIGasGauge_003E*, void>)(&global::_003CModule_003E.CComPtrNtv_003CIGasGauge_003E_002E_007Bdtor_007D), &cComPtrNtv_003CIGasGauge_003E);
					throw;
				}
				global::_003CModule_003E.CComPtrNtv_003CIGasGauge_003E_002ERelease(&cComPtrNtv_003CIGasGauge_003E);
				goto IL_03e3;
			}
			goto IL_03eb;
			IL_03ed:
			int fInitializationCompleted;
			m_fInitializationCompleted = (byte)fInitializationCompleted != 0;
			if (global::_003CModule_003E.WPP_GLOBAL_Control != Unsafe.AsPointer(ref global::_003CModule_003E.WPP_GLOBAL_Control) && (((int*)global::_003CModule_003E.WPP_GLOBAL_Control)[15] & 2) != 0 && (uint)((byte*)global::_003CModule_003E.WPP_GLOBAL_Control)[57] >= 5u)
			{
				global::_003CModule_003E.WPP_SF_dd(((ulong*)global::_003CModule_003E.WPP_GLOBAL_Control)[6], 23, (_GUID*)Unsafe.AsPointer(ref global::_003CModule_003E._003FA0xbf2368f8_002EWPP_DeviceAPI_cpp_Traceguids), num, m_iDeviceID);
			}
			goto end_IL_004f;
			IL_03e7:
			fInitializationCompleted = 1;
			goto IL_03ed;
			IL_03eb:
			fInitializationCompleted = 0;
			goto IL_03ed;
			IL_03e3:
			if (num >= 0)
			{
				goto IL_03e7;
			}
			goto IL_03eb;
			end_IL_004f:;
		}
		catch
		{
			//try-fault
			((IDisposable)managedLock).Dispose();
			throw;
		}
		((IDisposable)managedLock).Dispose();
		return num;
	}

	private unsafe void OnFirmwareProcessComplete(int hr)
	{
		if (global::_003CModule_003E.WPP_GLOBAL_Control != Unsafe.AsPointer(ref global::_003CModule_003E.WPP_GLOBAL_Control) && (((int*)global::_003CModule_003E.WPP_GLOBAL_Control)[15] & 2) != 0 && (uint)((byte*)global::_003CModule_003E.WPP_GLOBAL_Control)[57] >= 5u)
		{
			global::_003CModule_003E.WPP_SF_d(((ulong*)global::_003CModule_003E.WPP_GLOBAL_Control)[6], 27, (_GUID*)Unsafe.AsPointer(ref global::_003CModule_003E._003FA0xbf2368f8_002EWPP_DeviceAPI_cpp_Traceguids), hr);
		}
		Initialize(m_spEndpointHost.p);
		CompleteInitialization();
		if (global::_003CModule_003E.WPP_GLOBAL_Control != Unsafe.AsPointer(ref global::_003CModule_003E.WPP_GLOBAL_Control) && (((int*)global::_003CModule_003E.WPP_GLOBAL_Control)[15] & 2) != 0 && (uint)((byte*)global::_003CModule_003E.WPP_GLOBAL_Control)[57] >= 5u)
		{
			global::_003CModule_003E.WPP_SF_(((ulong*)global::_003CModule_003E.WPP_GLOBAL_Control)[6], 28, (_GUID*)Unsafe.AsPointer(ref global::_003CModule_003E._003FA0xbf2368f8_002EWPP_DeviceAPI_cpp_Traceguids));
		}
	}

	private unsafe int IsFirmwareProcessInProgress(bool* pfFirmwareUpdateInProgress, bool* pfFirmwareRestoreInProgress)
	{
		if (pfFirmwareUpdateInProgress == null)
		{
			global::_003CModule_003E._ZuneShipAssert(1001u, 647u);
			return -2147467261;
		}
		if (pfFirmwareRestoreInProgress == null)
		{
			global::_003CModule_003E._ZuneShipAssert(1001u, 648u);
			return -2147467261;
		}
		*pfFirmwareUpdateInProgress = false;
		*pfFirmwareRestoreInProgress = false;
		int num = 0;
		try
		{
			Monitor.Enter(m_Lock);
			if (global::_003CModule_003E.WPP_GLOBAL_Control != Unsafe.AsPointer(ref global::_003CModule_003E.WPP_GLOBAL_Control) && (((int*)global::_003CModule_003E.WPP_GLOBAL_Control)[15] & 2) != 0 && (uint)((byte*)global::_003CModule_003E.WPP_GLOBAL_Control)[57] >= 5u)
			{
				global::_003CModule_003E.WPP_SF_(((ulong*)global::_003CModule_003E.WPP_GLOBAL_Control)[6], 24, (_GUID*)Unsafe.AsPointer(ref global::_003CModule_003E._003FA0xbf2368f8_002EWPP_DeviceAPI_cpp_Traceguids));
				int num2 = 1;
			}
			else
			{
				int num2 = 0;
			}
			FirmwareUpdater firmwareUpdater = m_firmwareUpdater;
			if (firmwareUpdater == null)
			{
				IEndpointHost* p = m_spEndpointHost.p;
				m_firmwareUpdater = new FirmwareUpdater(p, m_fFirmwareUpdateSupported);
			}
			else
			{
				num = firmwareUpdater.Reset(deviceRebooting: false);
			}
			if (m_fFirmwareUpdateSupported && num >= 0)
			{
				int num3 = ((m_firmwareUpdater.IsUpdateInProgress() != UpdateAction.None) ? 1 : 0);
				*pfFirmwareUpdateInProgress = (byte)num3 != 0;
				byte b = (m_firmwareUpdater.Restorer.IsRestoreInProgress() ? ((byte)1) : ((byte)0));
				*pfFirmwareRestoreInProgress = b != 0;
				if (*pfFirmwareUpdateInProgress || b != 0)
				{
					if (global::_003CModule_003E.WPP_GLOBAL_Control != Unsafe.AsPointer(ref global::_003CModule_003E.WPP_GLOBAL_Control) && (((int*)global::_003CModule_003E.WPP_GLOBAL_Control)[15] & 2) != 0 && (uint)((byte*)global::_003CModule_003E.WPP_GLOBAL_Control)[57] >= 5u)
					{
						global::_003CModule_003E.WPP_SF_d(((ulong*)global::_003CModule_003E.WPP_GLOBAL_Control)[6], 25, (_GUID*)Unsafe.AsPointer(ref global::_003CModule_003E._003FA0xbf2368f8_002EWPP_DeviceAPI_cpp_Traceguids), m_iDeviceID);
						int num4 = 1;
					}
					else
					{
						int num5 = 0;
					}
				}
			}
			else if (global::_003CModule_003E.WPP_GLOBAL_Control != Unsafe.AsPointer(ref global::_003CModule_003E.WPP_GLOBAL_Control) && (((int*)global::_003CModule_003E.WPP_GLOBAL_Control)[15] & 2) != 0 && (uint)((byte*)global::_003CModule_003E.WPP_GLOBAL_Control)[57] >= 2u)
			{
				global::_003CModule_003E.WPP_SF_d(((ulong*)global::_003CModule_003E.WPP_GLOBAL_Control)[6], 26, (_GUID*)Unsafe.AsPointer(ref global::_003CModule_003E._003FA0xbf2368f8_002EWPP_DeviceAPI_cpp_Traceguids), num);
				int num6 = 1;
			}
			else
			{
				int num7 = 0;
			}
		}
		finally
		{
			Monitor.Exit(m_Lock);
		}
		return num;
	}

	private unsafe int CreateDeviceAssetSet(IDeviceAssetProvider* pDeviceAssetProvider)
	{
		if (global::_003CModule_003E.WPP_GLOBAL_Control != Unsafe.AsPointer(ref global::_003CModule_003E.WPP_GLOBAL_Control) && (((int*)global::_003CModule_003E.WPP_GLOBAL_Control)[15] & 2) != 0 && (uint)((byte*)global::_003CModule_003E.WPP_GLOBAL_Control)[57] >= 5u)
		{
			global::_003CModule_003E.WPP_SF_(((ulong*)global::_003CModule_003E.WPP_GLOBAL_Control)[6], 43, (_GUID*)Unsafe.AsPointer(ref global::_003CModule_003E._003FA0xbf2368f8_002EWPP_DeviceAPI_cpp_Traceguids));
		}
		uint* ptr = null;
		int num = 0;
		ushort** ptr2 = null;
		int num2 = 0;
		int num3 = 0;
		if (-2147024774 != ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, ushort**, int, int*, int>)(int)(*(uint*)(*(int*)pDeviceAssetProvider + 20)))((nint)pDeviceAssetProvider, null, 0, &num2))
		{
			global::_003CModule_003E._ZuneShipAssert(1004u, 3551u);
		}
		ushort** ptr3 = (ushort**)global::_003CModule_003E.new_005B_005D(((uint)num2 > 1073741823u) ? uint.MaxValue : ((uint)(num2 << 2)));
		int num4;
		if (ptr3 == null)
		{
			num4 = -2147024882;
			global::_003CModule_003E._ZuneShipAssert(1012u, 3554u);
		}
		else
		{
			num4 = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, ushort**, int, int*, int>)(int)(*(uint*)(*(int*)pDeviceAssetProvider + 20)))((nint)pDeviceAssetProvider, ptr3, num2, &num2);
			if (num4 < 0)
			{
				global::_003CModule_003E._ZuneShipAssertForHr(num4, 3557u);
			}
			else
			{
				if (-2147024774 != ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, ushort**, int, int*, int>)(int)(*(uint*)(*(int*)pDeviceAssetProvider + 24)))((nint)pDeviceAssetProvider, null, 0, &num3))
				{
					global::_003CModule_003E._ZuneShipAssert(1004u, 3563u);
				}
				ptr2 = (ushort**)global::_003CModule_003E.new_005B_005D(((uint)num3 > 1073741823u) ? uint.MaxValue : ((uint)(num3 << 2)));
				num4 = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, ushort**, int, int*, int>)(int)(*(uint*)(*(int*)pDeviceAssetProvider + 24)))((nint)pDeviceAssetProvider, ptr2, num3, &num3);
				if (num4 < 0)
				{
					global::_003CModule_003E._ZuneShipAssertForHr(num4, 3569u);
				}
				else
				{
					if (-2147024774 != ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint*, int, int*, int>)(int)(*(uint*)(*(int*)pDeviceAssetProvider + 28)))((nint)pDeviceAssetProvider, null, 0, &num))
					{
						global::_003CModule_003E._ZuneShipAssert(1004u, 3575u);
					}
					ptr = (uint*)global::_003CModule_003E.new_005B_005D(((uint)num > 1073741823u) ? uint.MaxValue : ((uint)(num << 2)));
					if (ptr == null)
					{
						num4 = -2147024882;
						global::_003CModule_003E._ZuneShipAssert(1012u, 3578u);
					}
					else
					{
						num4 = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint*, int, int*, int>)(int)(*(uint*)(*(int*)pDeviceAssetProvider + 28)))((nint)pDeviceAssetProvider, ptr, num, &num);
						if (num4 < 0)
						{
							global::_003CModule_003E._ZuneShipAssertForHr(num4, 3581u);
						}
						else
						{
							string[] array = new string[num2];
							int num5 = 0;
							if (0 < num2)
							{
								do
								{
									ushort** ptr4 = (ushort**)(num5 * 4 + (byte*)ptr3);
									if (*(int*)ptr4 != 0)
									{
										array[num5] = new string((char*)(int)(*(uint*)ptr4));
									}
									num5++;
								}
								while (num5 < num2);
							}
							string[] array2 = new string[num3];
							int num6 = 0;
							if (0 < num3)
							{
								do
								{
									ushort** ptr5 = (ushort**)(num6 * 4 + (byte*)ptr2);
									if (*(int*)ptr5 != 0)
									{
										array2[num6] = new string((char*)(int)(*(uint*)ptr5));
									}
									num6++;
								}
								while (num6 < num3);
							}
							uint[] array3 = new uint[num];
							int num7 = 0;
							if (0 < num)
							{
								do
								{
									int num8 = num7;
									array3[num8] = *(uint*)(num8 * 4 + (byte*)ptr);
									num7++;
								}
								while (num7 < num);
							}
							m_DeviceAssetSet = new DeviceAssetSet(array, array2, array3);
						}
					}
				}
			}
			int num9 = 0;
			if (0 < num2)
			{
				ushort** ptr6 = ptr3;
				do
				{
					global::_003CModule_003E.WString_002ESysFreeString(ptr6);
					num9++;
					ptr6 = (ushort**)((byte*)ptr6 + 4);
				}
				while (num9 < num2);
			}
			if (ptr2 != null)
			{
				int num10 = 0;
				if (0 < num3)
				{
					ushort** ptr7 = ptr2;
					do
					{
						global::_003CModule_003E.WString_002ESysFreeString(ptr7);
						num10++;
						ptr7 = (ushort**)((byte*)ptr7 + 4);
					}
					while (num10 < num3);
				}
			}
		}
		global::_003CModule_003E.SafeDeleteArray_003Cunsigned_0020short_0020_002A_003E(&ptr3);
		global::_003CModule_003E.SafeDeleteArray_003Cunsigned_0020short_0020_002A_003E(&ptr2);
		global::_003CModule_003E.SafeDeleteArray_003Cunsigned_0020long_003E(&ptr);
		if (global::_003CModule_003E.WPP_GLOBAL_Control != Unsafe.AsPointer(ref global::_003CModule_003E.WPP_GLOBAL_Control) && (((int*)global::_003CModule_003E.WPP_GLOBAL_Control)[15] & 2) != 0 && (uint)((byte*)global::_003CModule_003E.WPP_GLOBAL_Control)[57] >= 5u)
		{
			global::_003CModule_003E.WPP_SF_d(((ulong*)global::_003CModule_003E.WPP_GLOBAL_Control)[6], 44, (_GUID*)Unsafe.AsPointer(ref global::_003CModule_003E._003FA0xbf2368f8_002EWPP_DeviceAPI_cpp_Traceguids), num4);
		}
		return num4;
	}

	protected virtual void Dispose([MarshalAs(UnmanagedType.U1)] bool P_0)
	{
		if (P_0)
		{
			try
			{
				_007EDevice();
				return;
			}
			finally
			{
				try
				{
					((IDisposable)m_spDeviceMediator).Dispose();
				}
				finally
				{
					try
					{
						((IDisposable)m_spWlanProvider).Dispose();
					}
					finally
					{
						try
						{
							((IDisposable)m_spSyncEngine).Dispose();
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
