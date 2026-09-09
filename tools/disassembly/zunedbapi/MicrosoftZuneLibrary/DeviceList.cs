using System;
using System.Collections;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace MicrosoftZuneLibrary;

public class DeviceList : IDisposable
{
	private static DeviceList m_singletonInstance = null;

	private DeviceAddedHandler _003Cbacking_store_003EAdded;

	private readonly CComPtrMgd_003CIEndpointHostManager_003E m_spEndpointHostManager;

	private readonly CComPtrMgd_003CEndpointHostManagerMediator_003E m_spEndpointHostManagerMediator;

	private SortedList m_slDevices;

	private object m_lock;

	private bool m_fInitialized;

	public unsafe bool Initialized
	{
		[return: MarshalAs(UnmanagedType.U1)]
		get
		{
			if (global::_003CModule_003E.WPP_GLOBAL_Control != Unsafe.AsPointer(ref global::_003CModule_003E.WPP_GLOBAL_Control) && (((int*)global::_003CModule_003E.WPP_GLOBAL_Control)[15] & 2) != 0 && (uint)((byte*)global::_003CModule_003E.WPP_GLOBAL_Control)[57] >= 5u)
			{
				global::_003CModule_003E.WPP_SF_l(((ulong*)global::_003CModule_003E.WPP_GLOBAL_Control)[6], 39, (_GUID*)Unsafe.AsPointer(ref global::_003CModule_003E._003FA0x7cb2c9e3_002EWPP_DeviceListAPI_cpp_Traceguids), m_fInitialized ? 1 : 0);
			}
			return m_fInitialized;
		}
		[param: MarshalAs(UnmanagedType.U1)]
		set
		{
			if (global::_003CModule_003E.WPP_GLOBAL_Control != Unsafe.AsPointer(ref global::_003CModule_003E.WPP_GLOBAL_Control) && (((int*)global::_003CModule_003E.WPP_GLOBAL_Control)[15] & 2) != 0 && (uint)((byte*)global::_003CModule_003E.WPP_GLOBAL_Control)[57] >= 5u)
			{
				global::_003CModule_003E.WPP_SF_l(((ulong*)global::_003CModule_003E.WPP_GLOBAL_Control)[6], 40, (_GUID*)Unsafe.AsPointer(ref global::_003CModule_003E._003FA0x7cb2c9e3_002EWPP_DeviceListAPI_cpp_Traceguids), value ? 1 : 0);
			}
			m_fInitialized = value;
		}
	}

	public unsafe int Count
	{
		get
		{
			ManagedLock managedLock = null;
			if (global::_003CModule_003E.WPP_GLOBAL_Control != Unsafe.AsPointer(ref global::_003CModule_003E.WPP_GLOBAL_Control) && (((int*)global::_003CModule_003E.WPP_GLOBAL_Control)[15] & 2) != 0 && (uint)((byte*)global::_003CModule_003E.WPP_GLOBAL_Control)[57] >= 5u)
			{
				global::_003CModule_003E.WPP_SF_d(((ulong*)global::_003CModule_003E.WPP_GLOBAL_Control)[6], 37, (_GUID*)Unsafe.AsPointer(ref global::_003CModule_003E._003FA0x7cb2c9e3_002EWPP_DeviceListAPI_cpp_Traceguids), m_slDevices.Count);
			}
			if (!m_fInitialized && global::_003CModule_003E.WPP_GLOBAL_Control != Unsafe.AsPointer(ref global::_003CModule_003E.WPP_GLOBAL_Control) && (((int*)global::_003CModule_003E.WPP_GLOBAL_Control)[15] & 2) != 0 && (uint)((byte*)global::_003CModule_003E.WPP_GLOBAL_Control)[57] >= 5u)
			{
				global::_003CModule_003E.WPP_SF_(((ulong*)global::_003CModule_003E.WPP_GLOBAL_Control)[6], 38, (_GUID*)Unsafe.AsPointer(ref global::_003CModule_003E._003FA0x7cb2c9e3_002EWPP_DeviceListAPI_cpp_Traceguids));
			}
			object obj = m_lock;
			if (obj != null && m_slDevices != null)
			{
				ManagedLock managedLock2 = new ManagedLock(obj);
				int count;
				try
				{
					managedLock = managedLock2;
					count = m_slDevices.Count;
				}
				catch
				{
					//try-fault
					((IDisposable)managedLock).Dispose();
					throw;
				}
				((IDisposable)managedLock).Dispose();
				return count;
			}
			return 0;
		}
	}

	public static DeviceList Instance
	{
		get
		{
			if (m_singletonInstance == null)
			{
				m_singletonInstance = new DeviceList();
			}
			return m_singletonInstance;
		}
	}

	[SpecialName]
	public event DeviceAddedHandler Added
	{
		[MethodImpl(MethodImplOptions.Synchronized)]
		add
		{
			_003Cbacking_store_003EAdded = (DeviceAddedHandler)Delegate.Combine(_003Cbacking_store_003EAdded, value);
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		remove
		{
			_003Cbacking_store_003EAdded = (DeviceAddedHandler)Delegate.Remove(_003Cbacking_store_003EAdded, value);
		}
	}

	private unsafe DeviceList()
	{
		CComPtrMgd_003CIEndpointHostManager_003E spEndpointHostManager = new CComPtrMgd_003CIEndpointHostManager_003E();
		try
		{
			m_spEndpointHostManager = spEndpointHostManager;
			CComPtrMgd_003CEndpointHostManagerMediator_003E spEndpointHostManagerMediator = new CComPtrMgd_003CEndpointHostManagerMediator_003E();
			try
			{
				m_spEndpointHostManagerMediator = spEndpointHostManagerMediator;
				base._002Ector();
				if (global::_003CModule_003E.WPP_GLOBAL_Control != Unsafe.AsPointer(ref global::_003CModule_003E.WPP_GLOBAL_Control) && (((int*)global::_003CModule_003E.WPP_GLOBAL_Control)[15] & 2) != 0 && (uint)((byte*)global::_003CModule_003E.WPP_GLOBAL_Control)[57] >= 5u)
				{
					global::_003CModule_003E.WPP_SF_(((ulong*)global::_003CModule_003E.WPP_GLOBAL_Control)[6], 10, (_GUID*)Unsafe.AsPointer(ref global::_003CModule_003E._003FA0x7cb2c9e3_002EWPP_DeviceListAPI_cpp_Traceguids));
				}
				m_fInitialized = false;
				m_lock = new object();
				m_slDevices = new SortedList();
				EndpointHostManagerMediator* ptr = (EndpointHostManagerMediator*)global::_003CModule_003E.@new(32u);
				EndpointHostManagerMediator* lp;
				try
				{
					lp = ((ptr == null) ? null : global::_003CModule_003E.EndpointHostManagerMediator_002E_007Bctor_007D(ptr, this));
				}
				catch
				{
					//try-fault
					global::_003CModule_003E.delete(ptr);
					throw;
				}
				m_spEndpointHostManagerMediator.op_Assign(lp);
				if (global::_003CModule_003E.WPP_GLOBAL_Control != Unsafe.AsPointer(ref global::_003CModule_003E.WPP_GLOBAL_Control) && (((int*)global::_003CModule_003E.WPP_GLOBAL_Control)[15] & 2) != 0 && (uint)((byte*)global::_003CModule_003E.WPP_GLOBAL_Control)[57] >= 5u)
				{
					global::_003CModule_003E.WPP_SF_(((ulong*)global::_003CModule_003E.WPP_GLOBAL_Control)[6], 11, (_GUID*)Unsafe.AsPointer(ref global::_003CModule_003E._003FA0x7cb2c9e3_002EWPP_DeviceListAPI_cpp_Traceguids));
				}
				return;
			}
			catch
			{
				//try-fault
				((IDisposable)m_spEndpointHostManagerMediator).Dispose();
				throw;
			}
		}
		catch
		{
			//try-fault
			((IDisposable)m_spEndpointHostManager).Dispose();
			throw;
		}
	}

	private unsafe void _007EDeviceList()
	{
		ManagedLock managedLock = null;
		if (global::_003CModule_003E.WPP_GLOBAL_Control != Unsafe.AsPointer(ref global::_003CModule_003E.WPP_GLOBAL_Control) && (((int*)global::_003CModule_003E.WPP_GLOBAL_Control)[15] & 2) != 0 && (uint)((byte*)global::_003CModule_003E.WPP_GLOBAL_Control)[57] >= 5u)
		{
			global::_003CModule_003E.WPP_SF_(((ulong*)global::_003CModule_003E.WPP_GLOBAL_Control)[6], 12, (_GUID*)Unsafe.AsPointer(ref global::_003CModule_003E._003FA0x7cb2c9e3_002EWPP_DeviceListAPI_cpp_Traceguids));
		}
		m_spEndpointHostManager.Release();
		CComPtrMgd_003CEndpointHostManagerMediator_003E spEndpointHostManagerMediator = m_spEndpointHostManagerMediator;
		if (spEndpointHostManagerMediator.p == null)
		{
			global::_003CModule_003E.EndpointHostManagerMediator_002EShutdown(spEndpointHostManagerMediator.p);
		}
		m_spEndpointHostManagerMediator.Release();
		ManagedLock managedLock2 = new ManagedLock(m_lock);
		try
		{
			managedLock = managedLock2;
			foreach (object slDevice in m_slDevices)
			{
				DictionaryEntry dictionaryEntry = (DictionaryEntry)slDevice;
				if (dictionaryEntry.Value is IDisposable disposable)
				{
					disposable.Dispose();
				}
			}
			m_slDevices = null;
		}
		catch
		{
			//try-fault
			((IDisposable)managedLock).Dispose();
			throw;
		}
		((IDisposable)managedLock).Dispose();
		m_lock = null;
		m_singletonInstance = null;
		if (global::_003CModule_003E.WPP_GLOBAL_Control != Unsafe.AsPointer(ref global::_003CModule_003E.WPP_GLOBAL_Control) && (((int*)global::_003CModule_003E.WPP_GLOBAL_Control)[15] & 2) != 0 && (uint)((byte*)global::_003CModule_003E.WPP_GLOBAL_Control)[57] >= 5u)
		{
			global::_003CModule_003E.WPP_SF_(((ulong*)global::_003CModule_003E.WPP_GLOBAL_Control)[6], 13, (_GUID*)Unsafe.AsPointer(ref global::_003CModule_003E._003FA0x7cb2c9e3_002EWPP_DeviceListAPI_cpp_Traceguids));
		}
	}

	public unsafe int InitializeAndEnumerate()
	{
		if (global::_003CModule_003E.WPP_GLOBAL_Control != Unsafe.AsPointer(ref global::_003CModule_003E.WPP_GLOBAL_Control) && (((int*)global::_003CModule_003E.WPP_GLOBAL_Control)[15] & 2) != 0 && (uint)((byte*)global::_003CModule_003E.WPP_GLOBAL_Control)[57] >= 5u)
		{
			global::_003CModule_003E.WPP_SF_(((ulong*)global::_003CModule_003E.WPP_GLOBAL_Control)[6], 14, (_GUID*)Unsafe.AsPointer(ref global::_003CModule_003E._003FA0x7cb2c9e3_002EWPP_DeviceListAPI_cpp_Traceguids));
		}
		int num = global::_003CModule_003E.EndpointHostManagerMediator_002EInitializeAndEnumerate(m_spEndpointHostManagerMediator.p);
		if (global::_003CModule_003E.WPP_GLOBAL_Control != Unsafe.AsPointer(ref global::_003CModule_003E.WPP_GLOBAL_Control) && (((int*)global::_003CModule_003E.WPP_GLOBAL_Control)[15] & 2) != 0 && (uint)((byte*)global::_003CModule_003E.WPP_GLOBAL_Control)[57] >= 5u)
		{
			global::_003CModule_003E.WPP_SF_d(((ulong*)global::_003CModule_003E.WPP_GLOBAL_Control)[6], 15, (_GUID*)Unsafe.AsPointer(ref global::_003CModule_003E._003FA0x7cb2c9e3_002EWPP_DeviceListAPI_cpp_Traceguids), num);
		}
		return num;
	}

	public unsafe void SetEndpointHostManager(IEndpointHostManager* pEndpointHostManager)
	{
		if (global::_003CModule_003E.WPP_GLOBAL_Control != Unsafe.AsPointer(ref global::_003CModule_003E.WPP_GLOBAL_Control) && (((int*)global::_003CModule_003E.WPP_GLOBAL_Control)[15] & 2) != 0 && (uint)((byte*)global::_003CModule_003E.WPP_GLOBAL_Control)[57] >= 5u)
		{
			global::_003CModule_003E.WPP_SF_(((ulong*)global::_003CModule_003E.WPP_GLOBAL_Control)[6], 16, (_GUID*)Unsafe.AsPointer(ref global::_003CModule_003E._003FA0x7cb2c9e3_002EWPP_DeviceListAPI_cpp_Traceguids));
		}
		m_spEndpointHostManager.op_Assign(pEndpointHostManager);
		if (global::_003CModule_003E.WPP_GLOBAL_Control != Unsafe.AsPointer(ref global::_003CModule_003E.WPP_GLOBAL_Control) && (((int*)global::_003CModule_003E.WPP_GLOBAL_Control)[15] & 2) != 0 && (uint)((byte*)global::_003CModule_003E.WPP_GLOBAL_Control)[57] >= 5u)
		{
			global::_003CModule_003E.WPP_SF_(((ulong*)global::_003CModule_003E.WPP_GLOBAL_Control)[6], 17, (_GUID*)Unsafe.AsPointer(ref global::_003CModule_003E._003FA0x7cb2c9e3_002EWPP_DeviceListAPI_cpp_Traceguids));
		}
	}

	public unsafe Device GetItem(int idx)
	{
		ManagedLock managedLock = null;
		if (global::_003CModule_003E.WPP_GLOBAL_Control != Unsafe.AsPointer(ref global::_003CModule_003E.WPP_GLOBAL_Control) && (((int*)global::_003CModule_003E.WPP_GLOBAL_Control)[15] & 2) != 0 && (uint)((byte*)global::_003CModule_003E.WPP_GLOBAL_Control)[57] >= 5u)
		{
			global::_003CModule_003E.WPP_SF_d(((ulong*)global::_003CModule_003E.WPP_GLOBAL_Control)[6], 18, (_GUID*)Unsafe.AsPointer(ref global::_003CModule_003E._003FA0x7cb2c9e3_002EWPP_DeviceListAPI_cpp_Traceguids), idx);
		}
		ManagedLock managedLock2 = new ManagedLock(m_lock);
		Device result;
		try
		{
			managedLock = managedLock2;
			if (!m_fInitialized && global::_003CModule_003E.WPP_GLOBAL_Control != Unsafe.AsPointer(ref global::_003CModule_003E.WPP_GLOBAL_Control) && (((int*)global::_003CModule_003E.WPP_GLOBAL_Control)[15] & 2) != 0 && (uint)((byte*)global::_003CModule_003E.WPP_GLOBAL_Control)[57] >= 5u)
			{
				global::_003CModule_003E.WPP_SF_(((ulong*)global::_003CModule_003E.WPP_GLOBAL_Control)[6], 19, (_GUID*)Unsafe.AsPointer(ref global::_003CModule_003E._003FA0x7cb2c9e3_002EWPP_DeviceListAPI_cpp_Traceguids));
			}
			SortedList slDevices = m_slDevices;
			if (slDevices != null && idx < slDevices.Count)
			{
				object byIndex = m_slDevices.GetByIndex(idx);
				if (byIndex != null)
				{
					if (global::_003CModule_003E.WPP_GLOBAL_Control != Unsafe.AsPointer(ref global::_003CModule_003E.WPP_GLOBAL_Control) && (((int*)global::_003CModule_003E.WPP_GLOBAL_Control)[15] & 2) != 0 && (uint)((byte*)global::_003CModule_003E.WPP_GLOBAL_Control)[57] >= 5u)
					{
						global::_003CModule_003E.WPP_SF_(((ulong*)global::_003CModule_003E.WPP_GLOBAL_Control)[6], 20, (_GUID*)Unsafe.AsPointer(ref global::_003CModule_003E._003FA0x7cb2c9e3_002EWPP_DeviceListAPI_cpp_Traceguids));
					}
					result = (Device)byIndex;
					goto IL_0103;
				}
			}
		}
		catch
		{
			//try-fault
			((IDisposable)managedLock).Dispose();
			throw;
		}
		Device result2;
		try
		{
			if (global::_003CModule_003E.WPP_GLOBAL_Control != Unsafe.AsPointer(ref global::_003CModule_003E.WPP_GLOBAL_Control) && (((int*)global::_003CModule_003E.WPP_GLOBAL_Control)[15] & 2) != 0 && (uint)((byte*)global::_003CModule_003E.WPP_GLOBAL_Control)[57] >= 5u)
			{
				global::_003CModule_003E.WPP_SF_(((ulong*)global::_003CModule_003E.WPP_GLOBAL_Control)[6], 21, (_GUID*)Unsafe.AsPointer(ref global::_003CModule_003E._003FA0x7cb2c9e3_002EWPP_DeviceListAPI_cpp_Traceguids));
			}
			result2 = null;
		}
		catch
		{
			//try-fault
			((IDisposable)managedLock).Dispose();
			throw;
		}
		((IDisposable)managedLock).Dispose();
		return result2;
		IL_0103:
		((IDisposable)managedLock).Dispose();
		return result;
	}

	public unsafe void HideDevice(Device device)
	{
		string strName = null;
		device.GetFriendlyName(ref strName);
		fixed (ushort* a = &Unsafe.As<char, ushort>(ref global::_003CModule_003E.PtrToStringChars(strName)))
		{
			if (global::_003CModule_003E.WPP_GLOBAL_Control != Unsafe.AsPointer(ref global::_003CModule_003E.WPP_GLOBAL_Control) && (((int*)global::_003CModule_003E.WPP_GLOBAL_Control)[15] & 4) != 0 && (uint)((byte*)global::_003CModule_003E.WPP_GLOBAL_Control)[57] >= 5u)
			{
				global::_003CModule_003E.WPP_SF_S(((ulong*)global::_003CModule_003E.WPP_GLOBAL_Control)[6], 22, (_GUID*)Unsafe.AsPointer(ref global::_003CModule_003E._003FA0x7cb2c9e3_002EWPP_DeviceListAPI_cpp_Traceguids), a);
			}
			if (!m_fInitialized && global::_003CModule_003E.WPP_GLOBAL_Control != Unsafe.AsPointer(ref global::_003CModule_003E.WPP_GLOBAL_Control) && (((int*)global::_003CModule_003E.WPP_GLOBAL_Control)[15] & 2) != 0 && (uint)((byte*)global::_003CModule_003E.WPP_GLOBAL_Control)[57] >= 5u)
			{
				global::_003CModule_003E.WPP_SF_(((ulong*)global::_003CModule_003E.WPP_GLOBAL_Control)[6], 23, (_GUID*)Unsafe.AsPointer(ref global::_003CModule_003E._003FA0x7cb2c9e3_002EWPP_DeviceListAPI_cpp_Traceguids));
			}
			HideDeviceInternal(device.EndpointId, fHide: true);
			if (global::_003CModule_003E.WPP_GLOBAL_Control != Unsafe.AsPointer(ref global::_003CModule_003E.WPP_GLOBAL_Control) && (((int*)global::_003CModule_003E.WPP_GLOBAL_Control)[15] & 2) != 0 && (uint)((byte*)global::_003CModule_003E.WPP_GLOBAL_Control)[57] >= 5u)
			{
				global::_003CModule_003E.WPP_SF_(((ulong*)global::_003CModule_003E.WPP_GLOBAL_Control)[6], 24, (_GUID*)Unsafe.AsPointer(ref global::_003CModule_003E._003FA0x7cb2c9e3_002EWPP_DeviceListAPI_cpp_Traceguids));
			}
		}
	}

	public unsafe void UnhideDevice(Device device)
	{
		string strName = null;
		device.GetFriendlyName(ref strName);
		fixed (ushort* a = &Unsafe.As<char, ushort>(ref global::_003CModule_003E.PtrToStringChars(strName)))
		{
			if (global::_003CModule_003E.WPP_GLOBAL_Control != Unsafe.AsPointer(ref global::_003CModule_003E.WPP_GLOBAL_Control) && (((int*)global::_003CModule_003E.WPP_GLOBAL_Control)[15] & 4) != 0 && (uint)((byte*)global::_003CModule_003E.WPP_GLOBAL_Control)[57] >= 5u)
			{
				global::_003CModule_003E.WPP_SF_S(((ulong*)global::_003CModule_003E.WPP_GLOBAL_Control)[6], 25, (_GUID*)Unsafe.AsPointer(ref global::_003CModule_003E._003FA0x7cb2c9e3_002EWPP_DeviceListAPI_cpp_Traceguids), a);
			}
			if (!m_fInitialized && global::_003CModule_003E.WPP_GLOBAL_Control != Unsafe.AsPointer(ref global::_003CModule_003E.WPP_GLOBAL_Control) && (((int*)global::_003CModule_003E.WPP_GLOBAL_Control)[15] & 2) != 0 && (uint)((byte*)global::_003CModule_003E.WPP_GLOBAL_Control)[57] >= 5u)
			{
				global::_003CModule_003E.WPP_SF_(((ulong*)global::_003CModule_003E.WPP_GLOBAL_Control)[6], 26, (_GUID*)Unsafe.AsPointer(ref global::_003CModule_003E._003FA0x7cb2c9e3_002EWPP_DeviceListAPI_cpp_Traceguids));
			}
			HideDeviceInternal(device.EndpointId, fHide: false);
			if (global::_003CModule_003E.WPP_GLOBAL_Control != Unsafe.AsPointer(ref global::_003CModule_003E.WPP_GLOBAL_Control) && (((int*)global::_003CModule_003E.WPP_GLOBAL_Control)[15] & 2) != 0 && (uint)((byte*)global::_003CModule_003E.WPP_GLOBAL_Control)[57] >= 5u)
			{
				global::_003CModule_003E.WPP_SF_(((ulong*)global::_003CModule_003E.WPP_GLOBAL_Control)[6], 27, (_GUID*)Unsafe.AsPointer(ref global::_003CModule_003E._003FA0x7cb2c9e3_002EWPP_DeviceListAPI_cpp_Traceguids));
			}
		}
	}

	public unsafe virtual int DeviceArrived(IEndpointHost* pEndpointHost)
	{
		Device device = null;
		if (pEndpointHost == null)
		{
			global::_003CModule_003E._ZuneShipAssert(1001u, 184u);
			return -2147467261;
		}
		int num = -1;
		device = null;
		if (global::_003CModule_003E.WPP_GLOBAL_Control != Unsafe.AsPointer(ref global::_003CModule_003E.WPP_GLOBAL_Control) && (((int*)global::_003CModule_003E.WPP_GLOBAL_Control)[15] & 2) != 0 && (uint)((byte*)global::_003CModule_003E.WPP_GLOBAL_Control)[57] >= 5u)
		{
			global::_003CModule_003E.WPP_SF_(((ulong*)global::_003CModule_003E.WPP_GLOBAL_Control)[6], 28, (_GUID*)Unsafe.AsPointer(ref global::_003CModule_003E._003FA0x7cb2c9e3_002EWPP_DeviceListAPI_cpp_Traceguids));
		}
		int num2 = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, EEndpointHostProperty, int*, int>)(int)(*(uint*)(*(int*)pEndpointHost + 68)))((nint)pEndpointHost, EEndpointHostProperty.eEndpointHostPropertyDatabaseEndpointId, &num);
		if (num2 >= 0 && num > 0)
		{
			if (global::_003CModule_003E.WPP_GLOBAL_Control != Unsafe.AsPointer(ref global::_003CModule_003E.WPP_GLOBAL_Control) && (((int*)global::_003CModule_003E.WPP_GLOBAL_Control)[15] & 2) != 0 && (uint)((byte*)global::_003CModule_003E.WPP_GLOBAL_Control)[57] >= 5u)
			{
				global::_003CModule_003E.WPP_SF_(((ulong*)global::_003CModule_003E.WPP_GLOBAL_Control)[6], 29, (_GUID*)Unsafe.AsPointer(ref global::_003CModule_003E._003FA0x7cb2c9e3_002EWPP_DeviceListAPI_cpp_Traceguids));
			}
			num2 = AddDeviceToList(pEndpointHost, ref device);
		}
		if (global::_003CModule_003E.WPP_GLOBAL_Control != Unsafe.AsPointer(ref global::_003CModule_003E.WPP_GLOBAL_Control) && (((int*)global::_003CModule_003E.WPP_GLOBAL_Control)[15] & 2) != 0 && (uint)((byte*)global::_003CModule_003E.WPP_GLOBAL_Control)[57] >= 5u)
		{
			global::_003CModule_003E.WPP_SF_d(((ulong*)global::_003CModule_003E.WPP_GLOBAL_Control)[6], 30, (_GUID*)Unsafe.AsPointer(ref global::_003CModule_003E._003FA0x7cb2c9e3_002EWPP_DeviceListAPI_cpp_Traceguids), num2);
		}
		return num2;
	}

	public unsafe virtual int DeviceDisconnected(IEndpointHost* pEndpointHost)
	{
		ManagedLock managedLock = null;
		if (pEndpointHost == null)
		{
			global::_003CModule_003E._ZuneShipAssert(1001u, 284u);
			return -2147467261;
		}
		object obj = m_lock;
		if (obj == null)
		{
			return -2147467261;
		}
		ManagedLock managedLock2 = new ManagedLock(obj);
		int num;
		try
		{
			managedLock = managedLock2;
			int nDeviceId = -1;
			if (global::_003CModule_003E.WPP_GLOBAL_Control != Unsafe.AsPointer(ref global::_003CModule_003E.WPP_GLOBAL_Control) && (((int*)global::_003CModule_003E.WPP_GLOBAL_Control)[15] & 2) != 0 && (uint)((byte*)global::_003CModule_003E.WPP_GLOBAL_Control)[57] >= 5u)
			{
				global::_003CModule_003E.WPP_SF_(((ulong*)global::_003CModule_003E.WPP_GLOBAL_Control)[6], 33, (_GUID*)Unsafe.AsPointer(ref global::_003CModule_003E._003FA0x7cb2c9e3_002EWPP_DeviceListAPI_cpp_Traceguids));
			}
			if (m_slDevices == null)
			{
				num = -2147418113;
			}
			else
			{
				num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, EEndpointHostProperty, int*, int>)(int)(*(uint*)(*(int*)pEndpointHost + 68)))((nint)pEndpointHost, EEndpointHostProperty.eEndpointHostPropertyDatabaseEndpointId, &nDeviceId);
				if (num >= 0)
				{
					ResetUpdater(nDeviceId, fFireDisconnect: true);
				}
			}
			if (global::_003CModule_003E.WPP_GLOBAL_Control != Unsafe.AsPointer(ref global::_003CModule_003E.WPP_GLOBAL_Control) && (((int*)global::_003CModule_003E.WPP_GLOBAL_Control)[15] & 2) != 0 && (uint)((byte*)global::_003CModule_003E.WPP_GLOBAL_Control)[57] >= 5u)
			{
				global::_003CModule_003E.WPP_SF_d(((ulong*)global::_003CModule_003E.WPP_GLOBAL_Control)[6], 34, (_GUID*)Unsafe.AsPointer(ref global::_003CModule_003E._003FA0x7cb2c9e3_002EWPP_DeviceListAPI_cpp_Traceguids), num);
			}
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

	[SpecialName]
	protected void raise_Added(Device value0)
	{
		_003Cbacking_store_003EAdded?.Invoke(value0);
	}

	public unsafe int GetTranscodedFilesCachePath(ref string strCachePath)
	{
		ushort* ptr = null;
		IEndpointHostManager* p = m_spEndpointHostManager.p;
		if (p == null)
		{
			global::_003CModule_003E._ZuneShipAssert(1002u, 329u);
			return -2147418113;
		}
		int num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, ushort**, int>)(int)(*(uint*)(*(int*)p + 60)))((nint)p, &ptr);
		if (num >= 0 && ptr != null)
		{
			strCachePath = new string((char*)ptr);
			global::_003CModule_003E.SysFreeString(ptr);
		}
		return num;
	}

	public unsafe int SetTranscodedFilesCachePath(string strCachePath)
	{
		fixed (ushort* ptr = &Unsafe.As<char, ushort>(ref global::_003CModule_003E.PtrToStringChars(strCachePath)))
		{
			int result;
			if (ptr != null)
			{
				ushort* ptr2 = global::_003CModule_003E.SysAllocString(ptr);
				IEndpointHostManager* p = m_spEndpointHostManager.p;
				if (p == null)
				{
					global::_003CModule_003E._ZuneShipAssert(1002u, 353u);
					return -2147418113;
				}
				result = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, ushort*, int>)(int)(*(uint*)(*(int*)p + 64)))((nint)p, ptr2);
				global::_003CModule_003E.SysFreeString(ptr2);
			}
			else
			{
				result = -2147418113;
			}
			return result;
		}
	}

	public unsafe int SetTranscodedFilesCacheSize(int lCacheSize)
	{
		IEndpointHostManager* p = m_spEndpointHostManager.p;
		if (p == null)
		{
			global::_003CModule_003E._ZuneShipAssert(1002u, 372u);
			return -2147418113;
		}
		IEndpointHostManager* ptr = p;
		return ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, int, int>)(int)(*(uint*)(*(int*)ptr + 52)))((nint)ptr, lCacheSize);
	}

	public unsafe int ClearTranscodeCache()
	{
		IEndpointHostManager* p = m_spEndpointHostManager.p;
		if (p == null)
		{
			global::_003CModule_003E._ZuneShipAssert(1002u, 384u);
			return -2147418113;
		}
		return ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, int>)(int)(*(uint*)(*(int*)p + 56)))((nint)p);
	}

	internal unsafe virtual int AddDeviceToList(IEndpointHost* pEndpointHost, ref Device device)
	{
		ManagedLock managedLock = null;
		if (pEndpointHost == null)
		{
			global::_003CModule_003E._ZuneShipAssert(1001u, 220u);
			return -2147467261;
		}
		if (m_lock == null)
		{
			return -2147467261;
		}
		int num = -1;
		if (global::_003CModule_003E.WPP_GLOBAL_Control != Unsafe.AsPointer(ref global::_003CModule_003E.WPP_GLOBAL_Control) && (((int*)global::_003CModule_003E.WPP_GLOBAL_Control)[15] & 2) != 0 && (uint)((byte*)global::_003CModule_003E.WPP_GLOBAL_Control)[57] >= 5u)
		{
			global::_003CModule_003E.WPP_SF_(((ulong*)global::_003CModule_003E.WPP_GLOBAL_Control)[6], 31, (_GUID*)Unsafe.AsPointer(ref global::_003CModule_003E._003FA0x7cb2c9e3_002EWPP_DeviceListAPI_cpp_Traceguids));
		}
		ManagedLock managedLock2 = new ManagedLock(m_lock);
		int num2;
		try
		{
			managedLock = managedLock2;
			if (m_slDevices == null)
			{
				num2 = -2147418113;
			}
			else
			{
				num2 = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, EEndpointHostProperty, int*, int>)(int)(*(uint*)(*(int*)pEndpointHost + 68)))((nint)pEndpointHost, EEndpointHostProperty.eEndpointHostPropertyDatabaseEndpointId, &num);
				if (num2 >= 0)
				{
					if (m_slDevices.ContainsKey(num))
					{
						num2 = (device = (Device)m_slDevices[num]).Initialize(pEndpointHost);
					}
					else
					{
						Device device2 = (device = new Device());
						if (device2 != null)
						{
							num2 = device2.Initialize(pEndpointHost);
							if (num2 >= 0)
							{
								Device device3 = device;
								m_slDevices.Add(device3.DeviceID, device3);
								raise_Added(device);
							}
						}
						else
						{
							num2 = -2147024882;
						}
					}
				}
			}
			if (global::_003CModule_003E.WPP_GLOBAL_Control != Unsafe.AsPointer(ref global::_003CModule_003E.WPP_GLOBAL_Control) && (((int*)global::_003CModule_003E.WPP_GLOBAL_Control)[15] & 2) != 0 && (uint)((byte*)global::_003CModule_003E.WPP_GLOBAL_Control)[57] >= 5u)
			{
				global::_003CModule_003E.WPP_SF_d(((ulong*)global::_003CModule_003E.WPP_GLOBAL_Control)[6], 32, (_GUID*)Unsafe.AsPointer(ref global::_003CModule_003E._003FA0x7cb2c9e3_002EWPP_DeviceListAPI_cpp_Traceguids), num2);
			}
		}
		catch
		{
			//try-fault
			((IDisposable)managedLock).Dispose();
			throw;
		}
		((IDisposable)managedLock).Dispose();
		return num2;
	}

	internal unsafe int ForgetEndpoint(IEndpointHost* pEndpointHost)
	{
		ManagedLock managedLock = null;
		if (m_spEndpointHostManager.p == null)
		{
			global::_003CModule_003E._ZuneShipAssert(1002u, 397u);
			return -2147418113;
		}
		if (pEndpointHost == null)
		{
			global::_003CModule_003E._ZuneShipAssert(1001u, 398u);
			return -2147467261;
		}
		object obj = m_lock;
		if (obj == null)
		{
			return -2147467261;
		}
		ManagedLock managedLock2 = new ManagedLock(obj);
		int num2;
		try
		{
			managedLock = managedLock2;
			int num = 0;
			if (global::_003CModule_003E.WPP_GLOBAL_Control != Unsafe.AsPointer(ref global::_003CModule_003E.WPP_GLOBAL_Control) && (((int*)global::_003CModule_003E.WPP_GLOBAL_Control)[15] & 2) != 0 && (uint)((byte*)global::_003CModule_003E.WPP_GLOBAL_Control)[57] >= 5u)
			{
				global::_003CModule_003E.WPP_SF_(((ulong*)global::_003CModule_003E.WPP_GLOBAL_Control)[6], 35, (_GUID*)Unsafe.AsPointer(ref global::_003CModule_003E._003FA0x7cb2c9e3_002EWPP_DeviceListAPI_cpp_Traceguids));
			}
			num2 = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, EEndpointHostProperty, int*, int>)(int)(*(uint*)(*(int*)pEndpointHost + 68)))((nint)pEndpointHost, EEndpointHostProperty.eEndpointHostPropertyDatabaseEndpointId, &num);
			if (num2 >= 0)
			{
				IEndpointHostManager* p = m_spEndpointHostManager.p;
				num2 = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, IEndpointHost*, int>)(int)(*(uint*)(*(int*)p + 40)))((nint)p, pEndpointHost);
			}
			ResetUpdater(num, fFireDisconnect: false);
			if (num2 >= 0)
			{
				m_slDevices?.Remove(num);
			}
			if (global::_003CModule_003E.WPP_GLOBAL_Control != Unsafe.AsPointer(ref global::_003CModule_003E.WPP_GLOBAL_Control) && (((int*)global::_003CModule_003E.WPP_GLOBAL_Control)[15] & 2) != 0 && (uint)((byte*)global::_003CModule_003E.WPP_GLOBAL_Control)[57] >= 5u)
			{
				global::_003CModule_003E.WPP_SF_d(((ulong*)global::_003CModule_003E.WPP_GLOBAL_Control)[6], 36, (_GUID*)Unsafe.AsPointer(ref global::_003CModule_003E._003FA0x7cb2c9e3_002EWPP_DeviceListAPI_cpp_Traceguids), num2);
			}
		}
		catch
		{
			//try-fault
			((IDisposable)managedLock).Dispose();
			throw;
		}
		((IDisposable)managedLock).Dispose();
		return num2;
	}

	protected unsafe void HideDeviceInternal(string strEndpointId, [MarshalAs(UnmanagedType.U1)] bool fHide)
	{
		if (!(strEndpointId != null))
		{
			return;
		}
		Unsafe.SkipInit(out CComPtrNtv_003CIEndpointNotification_003E cComPtrNtv_003CIEndpointNotification_003E);
		*(int*)(&cComPtrNtv_003CIEndpointNotification_003E) = 0;
		try
		{
			fixed (ushort* ptr = &Unsafe.As<char, ushort>(ref global::_003CModule_003E.PtrToStringChars(strEndpointId)))
			{
				try
				{
					CComPtrMgd_003CIEndpointHostManager_003E spEndpointHostManager = m_spEndpointHostManager;
					if (spEndpointHostManager.p != null && global::_003CModule_003E.IUnknown_002EQueryInterface_003Cstruct_0020IEndpointNotification_003E((IUnknown*)spEndpointHostManager.p, (IEndpointNotification**)(&cComPtrNtv_003CIEndpointNotification_003E)) >= 0)
					{
						if (fHide)
						{
							int num = *(int*)(int)(*(uint*)(&cComPtrNtv_003CIEndpointNotification_003E)) + 16;
							((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, ushort*, int>)(int)(*(uint*)num))((IntPtr)(*(int*)(&cComPtrNtv_003CIEndpointNotification_003E)), ptr);
						}
						else
						{
							int num2 = *(int*)(int)(*(uint*)(&cComPtrNtv_003CIEndpointNotification_003E)) + 12;
							((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, ushort*, int>)(int)(*(uint*)num2))((IntPtr)(*(int*)(&cComPtrNtv_003CIEndpointNotification_003E)), ptr);
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
		catch
		{
			//try-fault
			global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<CComPtrNtv_003CIEndpointNotification_003E*, void>)(&global::_003CModule_003E.CComPtrNtv_003CIEndpointNotification_003E_002E_007Bdtor_007D), &cComPtrNtv_003CIEndpointNotification_003E);
			throw;
		}
		global::_003CModule_003E.CComPtrNtv_003CIEndpointNotification_003E_002ERelease(&cComPtrNtv_003CIEndpointNotification_003E);
	}

	private void ResetUpdater(int nDeviceId, [MarshalAs(UnmanagedType.U1)] bool fFireDisconnect)
	{
		if (!m_slDevices.ContainsKey(nDeviceId))
		{
			return;
		}
		Device device = (Device)m_slDevices[nDeviceId];
		if (device == null)
		{
			return;
		}
		FirmwareUpdater firmwareUpdater = device.FirmwareUpdater;
		if (firmwareUpdater != null)
		{
			bool flag = firmwareUpdater.Restorer.IsDeviceRebooting();
			if (!flag)
			{
				flag = firmwareUpdater.IsDeviceRebooting();
			}
			firmwareUpdater.Reset(flag);
		}
	}

	protected virtual void Dispose([MarshalAs(UnmanagedType.U1)] bool P_0)
	{
		if (P_0)
		{
			try
			{
				_007EDeviceList();
				return;
			}
			finally
			{
				try
				{
					((IDisposable)m_spEndpointHostManagerMediator).Dispose();
				}
				finally
				{
					try
					{
						((IDisposable)m_spEndpointHostManager).Dispose();
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
