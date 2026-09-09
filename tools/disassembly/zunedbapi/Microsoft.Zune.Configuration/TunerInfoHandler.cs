using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Microsoft.Zune.Configuration;

internal class TunerInfoHandler : ITunerInfoHandler, IDisposable
{
	private EventHandler _003Cbacking_store_003EOnChanged;

	private IList<TunerInfo> m_PCsList;

	private IList<TunerInfo> m_devicesList;

	private IList<TunerInfo> m_appStoreDevicesList;

	private DateTime m_nextPCDeregistrationDate;

	private DateTime m_nextSubscriptionDeviceDeregistrationDate;

	private DateTime m_nextAppStoreDeviceDeregistrationDate;

	[SpecialName]
	public virtual event EventHandler OnChanged
	{
		[MethodImpl(MethodImplOptions.Synchronized)]
		add
		{
			_003Cbacking_store_003EOnChanged = (EventHandler)Delegate.Combine(_003Cbacking_store_003EOnChanged, value);
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		remove
		{
			_003Cbacking_store_003EOnChanged = (EventHandler)Delegate.Remove(_003Cbacking_store_003EOnChanged, value);
		}
	}

	public TunerInfoHandler()
	{
		m_PCsList = new List<TunerInfo>();
		m_devicesList = new List<TunerInfo>();
		m_appStoreDevicesList = new List<TunerInfo>();
	}

	private void _007ETunerInfoHandler()
	{
	}

	[return: MarshalAs(UnmanagedType.U1)]
	public unsafe virtual bool CanQueryTunerList()
	{
		IService* ptr = null;
		int num = 0;
		if (global::_003CModule_003E.GetSingleton((_GUID)global::_003CModule_003E._GUID_bb2d1edd_1bd5_4be1_8d38_36d4f0849911, (void**)(&ptr)) >= 0)
		{
			IService* intPtr = ptr;
			num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, int>)(int)(*(uint*)(*(int*)intPtr + 100)))((nint)intPtr);
		}
		if (ptr != null)
		{
			IService* intPtr2 = ptr;
			((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint>)(int)(*(uint*)(*(int*)intPtr2 + 8)))((nint)intPtr2);
		}
		return (byte)((num != 0) ? 1u : 0u) != 0;
	}

	public virtual IList<TunerInfo> GetPCsList()
	{
		return m_PCsList;
	}

	public virtual IList<TunerInfo> GetDevicesList()
	{
		return m_devicesList;
	}

	public virtual IList<TunerInfo> GetAppStoreDevicesList()
	{
		return m_appStoreDevicesList;
	}

	public virtual DateTime GetNextPCDeregistrationDate()
	{
		return m_nextPCDeregistrationDate;
	}

	public virtual DateTime GetNextSubscriptionDeviceDeregistrationDate()
	{
		return m_nextSubscriptionDeviceDeregistrationDate;
	}

	public virtual DateTime GetNextAppStoreDeviceDeregistrationDate()
	{
		return m_nextAppStoreDeviceDeregistrationDate;
	}

	public unsafe virtual void RefreshTunerList()
	{
		IService* ptr = null;
		int num = global::_003CModule_003E.GetSingleton((_GUID)global::_003CModule_003E._GUID_bb2d1edd_1bd5_4be1_8d38_36d4f0849911, (void**)(&ptr));
		RefreshCallback* ptr2 = (RefreshCallback*)global::_003CModule_003E.@new(16u);
		RefreshCallback* ptr3;
		try
		{
			ptr3 = ((ptr2 == null) ? null : global::_003CModule_003E.Microsoft_002EZune_002EConfiguration_002ERefreshCallback_002E_007Bctor_007D(ptr2, this));
		}
		catch
		{
			//try-fault
			global::_003CModule_003E.delete(ptr2);
			throw;
		}
		if (ptr3 == null)
		{
			num = -2147024882;
		}
		if (num >= 0)
		{
			num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, IRefreshTunerListCallback*, int>)(int)(*(uint*)(*(int*)ptr + 276)))((nint)ptr, (IRefreshTunerListCallback*)ptr3);
		}
		if (ptr3 != null)
		{
			((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint>)(int)(*(uint*)(*(int*)ptr3 + 8)))((nint)ptr3);
		}
		if (ptr != null)
		{
			IService* intPtr = ptr;
			((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint>)(int)(*(uint*)(*(int*)intPtr + 8)))((nint)intPtr);
			ptr = null;
		}
		if (num < 0 && global::_003CModule_003E.WPP_GLOBAL_Control != Unsafe.AsPointer(ref global::_003CModule_003E.WPP_GLOBAL_Control) && (((int*)global::_003CModule_003E.WPP_GLOBAL_Control)[39] & 1) != 0 && (uint)((byte*)global::_003CModule_003E.WPP_GLOBAL_Control)[153] >= 5u)
		{
			global::_003CModule_003E.WPP_SF_D(((ulong*)global::_003CModule_003E.WPP_GLOBAL_Control)[18], 10, (_GUID*)Unsafe.AsPointer(ref global::_003CModule_003E._003FA0xc57fc1f0_002EWPP_RegisteredDevicesApi_cpp_Traceguids), (uint)num);
		}
	}

	public unsafe virtual void DeregisterTuner(TunerInfo info)
	{
		IService* ptr = null;
		int num = global::_003CModule_003E.GetSingleton((_GUID)global::_003CModule_003E._GUID_bb2d1edd_1bd5_4be1_8d38_36d4f0849911, (void**)(&ptr));
		DeregisterCallback* ptr2 = (DeregisterCallback*)global::_003CModule_003E.@new(16u);
		DeregisterCallback* ptr3;
		try
		{
			ptr3 = ((ptr2 == null) ? null : global::_003CModule_003E.Microsoft_002EZune_002EConfiguration_002EDeregisterCallback_002E_007Bctor_007D(ptr2, this));
		}
		catch
		{
			//try-fault
			global::_003CModule_003E.delete(ptr2);
			throw;
		}
		if (ptr3 == null)
		{
			num = -2147024882;
		}
		if (num >= 0)
		{
			fixed (ushort* ptr4 = &Unsafe.As<char, ushort>(ref global::_003CModule_003E.PtrToStringChars(info.m_tunerId)))
			{
				try
				{
					int num2 = *(int*)ptr + 280;
					num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, ushort*, ETunerType, ETunerRegisterType, IDeregisterTunerCallback*, int>)(int)(*(uint*)num2))((nint)ptr, ptr4, (ETunerType)info.TunerType, (ETunerRegisterType)info.TunerRegisterType, (IDeregisterTunerCallback*)ptr3);
				}
				catch
				{
					//try-fault
					ptr4 = null;
					throw;
				}
			}
		}
		if (ptr != null)
		{
			IService* intPtr = ptr;
			((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint>)(int)(*(uint*)(*(int*)intPtr + 8)))((nint)intPtr);
			ptr = null;
		}
		if (num < 0 && global::_003CModule_003E.WPP_GLOBAL_Control != Unsafe.AsPointer(ref global::_003CModule_003E.WPP_GLOBAL_Control) && (((int*)global::_003CModule_003E.WPP_GLOBAL_Control)[39] & 1) != 0 && (uint)((byte*)global::_003CModule_003E.WPP_GLOBAL_Control)[153] >= 5u)
		{
			global::_003CModule_003E.WPP_SF_D(((ulong*)global::_003CModule_003E.WPP_GLOBAL_Control)[18], 11, (_GUID*)Unsafe.AsPointer(ref global::_003CModule_003E._003FA0xc57fc1f0_002EWPP_RegisteredDevicesApi_cpp_Traceguids), (uint)num);
		}
	}

	[SpecialName]
	protected virtual void raise_OnChanged(object value0, EventArgs value1)
	{
		_003Cbacking_store_003EOnChanged?.Invoke(value0, value1);
	}

	internal unsafe void UpdateTunerInfoLists(int cTunerInfo, global::TunerInfo* rgTunerInfo, ushort* pwszNextPCDeregistrationDate, ushort* pwszNextSubscriptionDeviceDeregistrationDate, ushort* pwszNextAppStoreDeviceDeregistrationDate)
	{
		m_PCsList.Clear();
		m_devicesList.Clear();
		m_appStoreDevicesList.Clear();
		if (0 < cTunerInfo)
		{
			global::TunerInfo* ptr = (global::TunerInfo*)((byte*)rgTunerInfo + 12);
			int num = cTunerInfo;
			do
			{
				string name = new string((char*)(int)(*((uint*)ptr - 1)));
				string tunerId = new string((char*)(int)(*(uint*)ptr));
				string tunerVersion = new string((char*)(int)((uint*)ptr)[1]);
				string dateCreated = new string((char*)(int)((uint*)ptr)[2]);
				string dateLastUsed = new string((char*)(int)((uint*)ptr)[3]);
				TunerType tunerType = *((TunerType*)ptr - 3);
				TunerRegisterType tunerRegisterType = *((TunerRegisterType*)ptr - 2);
				TunerInfo item = new TunerInfo(name, tunerId, tunerVersion, tunerType, tunerRegisterType, dateCreated, dateLastUsed);
				switch (tunerType)
				{
				case TunerType.PC:
					m_PCsList.Add(item);
					break;
				case TunerType.ZuneDevice:
				case TunerType.MobileDevice:
					switch (tunerRegisterType)
					{
					case TunerRegisterType.Subscription:
						m_devicesList.Add(item);
						break;
					case TunerRegisterType.AppStore:
						m_appStoreDevicesList.Add(item);
						break;
					}
					break;
				}
				ptr = (global::TunerInfo*)((byte*)ptr + 28);
				num--;
			}
			while (num != 0);
		}
		if (pwszNextPCDeregistrationDate == null || !DateTime.TryParse(new string((char*)pwszNextPCDeregistrationDate), out m_nextPCDeregistrationDate))
		{
			m_nextPCDeregistrationDate = DateTime.MinValue;
		}
		if (pwszNextSubscriptionDeviceDeregistrationDate == null || !DateTime.TryParse(new string((char*)pwszNextSubscriptionDeviceDeregistrationDate), out m_nextSubscriptionDeviceDeregistrationDate))
		{
			m_nextSubscriptionDeviceDeregistrationDate = DateTime.MinValue;
		}
		if (pwszNextAppStoreDeviceDeregistrationDate == null || !DateTime.TryParse(new string((char*)pwszNextAppStoreDeviceDeregistrationDate), out m_nextAppStoreDeviceDeregistrationDate))
		{
			m_nextAppStoreDeviceDeregistrationDate = DateTime.MinValue;
		}
		raise_OnChanged(this, null);
	}

	internal unsafe void FinishRemoveTunerInfo(ushort* pwszTunerId, TunerType tunerType, TunerRegisterType tunerRegisterType)
	{
		IList<TunerInfo> list = null;
		switch (tunerType)
		{
		case TunerType.PC:
			list = m_PCsList;
			break;
		case TunerType.ZuneDevice:
		case TunerType.MobileDevice:
			switch (tunerRegisterType)
			{
			case TunerRegisterType.Subscription:
				list = m_devicesList;
				break;
			case TunerRegisterType.AppStore:
				list = m_appStoreDevicesList;
				break;
			}
			break;
		}
		int num = 0;
		if (0 >= list.Count)
		{
			return;
		}
		do
		{
			fixed (ushort* ptr = &Unsafe.As<char, ushort>(ref global::_003CModule_003E.PtrToStringChars(list[num].m_tunerId)))
			{
				try
				{
					if (2 == global::_003CModule_003E.CompareStringW(1033u, 1u, pwszTunerId, -1, ptr, -1))
					{
						goto IL_0075;
					}
				}
				catch
				{
					//try-fault
					ptr = null;
					throw;
				}
				goto end_IL_004a;
				IL_0075:
				try
				{
					list.RemoveAt(num);
					raise_OnChanged(this, null);
				}
				catch
				{
					//try-fault
					ptr = null;
					throw;
				}
				break;
				end_IL_004a:;
			}
			num++;
		}
		while (num < list.Count);
	}

	internal void ReportError(int hrError)
	{
		EventArgsHR eventArgsHR = new EventArgsHR();
		eventArgsHR.HResult = hrError;
		raise_OnChanged(this, eventArgsHR);
	}

	protected virtual void Dispose([MarshalAs(UnmanagedType.U1)] bool P_0)
	{
		if (!P_0)
		{
			base.Finalize();
		}
	}

	public virtual sealed void Dispose()
	{
		Dispose(true);
		GC.SuppressFinalize(this);
	}
}
