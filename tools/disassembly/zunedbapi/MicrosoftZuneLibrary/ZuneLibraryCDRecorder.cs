using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using Microsoft.Iris;

namespace MicrosoftZuneLibrary;

public class ZuneLibraryCDRecorder : IDisposable
{
	private int m_RefCount;

	private bool m_disposed;

	private bool m_fAdvised;

	private uint m_dwAdviseCookie;

	private unsafe IRecordManager* m_pRecordManager;

	internal OnRecordStartHandler m_RecordStartHandler;

	internal OnRecordProgressHandler m_RecordProgressHandler;

	internal OnRecordStopHandler m_RecordStopHandler;

	internal OnRecordPauseHandler m_RecordPauseHandler;

	internal OnRecordResumeHandler m_RecordResumeHandler;

	[SpecialName]
	public virtual event OnRecordResumeHandler RecordResumeHandler
	{
		add
		{
			m_RecordResumeHandler = (OnRecordResumeHandler)Delegate.Combine(m_RecordResumeHandler, value);
		}
		remove
		{
			m_RecordResumeHandler = (OnRecordResumeHandler)Delegate.Remove(m_RecordResumeHandler, value);
		}
	}

	[SpecialName]
	public virtual event OnRecordPauseHandler RecordPauseHandler
	{
		add
		{
			m_RecordPauseHandler = (OnRecordPauseHandler)Delegate.Combine(m_RecordPauseHandler, value);
		}
		remove
		{
			m_RecordPauseHandler = (OnRecordPauseHandler)Delegate.Remove(m_RecordPauseHandler, value);
		}
	}

	[SpecialName]
	public virtual event OnRecordStopHandler RecordStopHandler
	{
		add
		{
			m_RecordStopHandler = (OnRecordStopHandler)Delegate.Combine(m_RecordStopHandler, value);
		}
		remove
		{
			m_RecordStopHandler = (OnRecordStopHandler)Delegate.Remove(m_RecordStopHandler, value);
		}
	}

	[SpecialName]
	public virtual event OnRecordProgressHandler RecordProgressHandler
	{
		add
		{
			m_RecordProgressHandler = (OnRecordProgressHandler)Delegate.Combine(m_RecordProgressHandler, value);
		}
		remove
		{
			m_RecordProgressHandler = (OnRecordProgressHandler)Delegate.Remove(m_RecordProgressHandler, value);
		}
	}

	[SpecialName]
	public virtual event OnRecordStartHandler RecordStartHandler
	{
		add
		{
			m_RecordStartHandler = (OnRecordStartHandler)Delegate.Combine(m_RecordStartHandler, value);
		}
		remove
		{
			m_RecordStartHandler = (OnRecordStartHandler)Delegate.Remove(m_RecordStartHandler, value);
		}
	}

	internal unsafe ZuneLibraryCDRecorder(IRecordManager* pRecordManager)
	{
		m_RefCount = 0;
		m_disposed = false;
		m_fAdvised = false;
		m_pRecordManager = pRecordManager;
		base._002Ector();
		IRecordManager* pRecordManager2 = m_pRecordManager;
		if (pRecordManager2 == null)
		{
			return;
		}
		((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint>)(int)(*(uint*)(*(int*)pRecordManager2 + 4)))((nint)pRecordManager2);
		RecordManagerCallback* ptr = (RecordManagerCallback*)global::_003CModule_003E.@new(12u);
		RecordManagerCallback* ptr2;
		try
		{
			ptr2 = ((ptr == null) ? null : global::_003CModule_003E.MicrosoftZuneLibrary_002ERecordManagerCallback_002E_007Bctor_007D(ptr, this));
		}
		catch
		{
			//try-fault
			global::_003CModule_003E.delete(ptr);
			throw;
		}
		Unsafe.SkipInit(out IRecordManagerCallback* ptr3);
		if (ptr2 == null || ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, _GUID*, void**, int>)(int)(*(uint*)(int)(*(uint*)ptr2)))((nint)ptr2, (_GUID*)Unsafe.AsPointer(ref global::_003CModule_003E._GUID_dbb19183_e14e_49cc_a75a_0dbf88f7cc57), (void**)(&ptr3)) < 0)
		{
			return;
		}
		fixed (uint* ptr4 = &m_dwAdviseCookie)
		{
			try
			{
				int num = *(int*)m_pRecordManager + 36;
				if (((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, IRecordManagerCallback*, uint*, int>)(int)(*(uint*)num))((nint)m_pRecordManager, ptr3, ptr4) >= 0)
				{
					m_fAdvised = true;
				}
				IRecordManagerCallback* intPtr = ptr3;
				((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint>)(int)(*(uint*)(*(int*)intPtr + 8)))((nint)intPtr);
			}
			catch
			{
				//try-fault
				ptr4 = null;
				throw;
			}
		}
	}

	private void _007EZuneLibraryCDRecorder()
	{
		m_RecordStartHandler = null;
		m_RecordProgressHandler = null;
		m_RecordStopHandler = null;
		_0021ZuneLibraryCDRecorder();
	}

	private unsafe void _0021ZuneLibraryCDRecorder()
	{
		if (m_disposed)
		{
			return;
		}
		IRecordManager* pRecordManager = m_pRecordManager;
		if (pRecordManager != null)
		{
			if (m_fAdvised)
			{
				((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint, int>)(int)(*(uint*)(*(int*)pRecordManager + 40)))((nint)pRecordManager, m_dwAdviseCookie);
				m_fAdvised = false;
			}
			pRecordManager = m_pRecordManager;
			IRecordManager* intPtr = pRecordManager;
			((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint>)(int)(*(uint*)(*(int*)intPtr + 8)))((nint)intPtr);
			m_pRecordManager = null;
		}
		m_disposed = true;
	}

	public uint AddRef()
	{
		return (uint)Interlocked.Increment(ref m_RefCount);
	}

	public uint Release()
	{
		int num = Interlocked.Decrement(ref m_RefCount);
		if (0 == num)
		{
			_0021ZuneLibraryCDRecorder();
		}
		return (uint)num;
	}

	public unsafe int AddRecordingRequest(ZuneLibraryCDDevice device, uint dwTrackNumber)
	{
		int num = -2147418113;
		if (m_pRecordManager != null && device != null)
		{
			StringBuilder stringBuilder = new StringBuilder();
			num = device.GetTrackUrl(dwTrackNumber, stringBuilder);
			if (num >= 0)
			{
				fixed (ushort* ptr = &Unsafe.As<char, ushort>(ref global::_003CModule_003E.PtrToStringChars(stringBuilder.ToString())))
				{
					try
					{
						int num2 = *(int*)m_pRecordManager + 20;
						num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, ushort*, int>)(int)(*(uint*)num2))((nint)m_pRecordManager, ptr);
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

	public unsafe int RemoveRecordingRequest(ZuneLibraryCDDevice device, uint dwTrackNumber)
	{
		int num = -2147418113;
		if (m_pRecordManager != null && device != null)
		{
			StringBuilder stringBuilder = new StringBuilder();
			num = device.GetTrackUrl(dwTrackNumber, stringBuilder);
			if (num >= 0)
			{
				fixed (ushort* ptr = &Unsafe.As<char, ushort>(ref global::_003CModule_003E.PtrToStringChars(stringBuilder.ToString())))
				{
					try
					{
						int num2 = *(int*)m_pRecordManager + 24;
						num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, ushort*, int>)(int)(*(uint*)num2))((nint)m_pRecordManager, ptr);
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

	[return: MarshalAs(UnmanagedType.U1)]
	public unsafe bool IsScheduledForRecording(ZuneLibraryCDDevice device, uint dwTrackNumber)
	{
		int num = 0;
		if (m_pRecordManager != null && device != null)
		{
			StringBuilder stringBuilder = new StringBuilder();
			if (device.GetTrackUrl(dwTrackNumber, stringBuilder) >= 0)
			{
				fixed (ushort* ptr = &Unsafe.As<char, ushort>(ref global::_003CModule_003E.PtrToStringChars(stringBuilder.ToString())))
				{
					try
					{
						int num2 = *(int*)m_pRecordManager + 28;
						((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, ushort*, int*, int>)(int)(*(uint*)num2))((nint)m_pRecordManager, ptr, &num);
					}
					catch
					{
						//try-fault
						ptr = null;
						throw;
					}
				}
			}
			if (num != 0)
			{
				return true;
			}
		}
		return false;
	}

	public unsafe int StopRecording()
	{
		int result = 1;
		IRecordManager* pRecordManager = m_pRecordManager;
		if (pRecordManager != null)
		{
			result = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, int>)(int)(*(uint*)(*(int*)pRecordManager + 32)))((nint)pRecordManager);
		}
		return result;
	}

	public unsafe int AsyncAddRecordingRequest(ZuneLibraryCDDevice device, uint dwTrackNumber)
	{
		int num = -2147418113;
		if (m_pRecordManager != null && device != null)
		{
			StringBuilder stringBuilder = new StringBuilder();
			num = device.GetTrackUrl(dwTrackNumber, stringBuilder);
			if (num >= 0)
			{
				fixed (ushort* ptr = &Unsafe.As<char, ushort>(ref global::_003CModule_003E.PtrToStringChars(stringBuilder.ToString())))
				{
					try
					{
						int num2 = *(int*)m_pRecordManager + 12;
						num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, ushort*, int>)(int)(*(uint*)num2))((nint)m_pRecordManager, ptr);
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

	public unsafe int AsyncRemoveRecordingRequest(ZuneLibraryCDDevice device, uint dwTrackNumber)
	{
		int num = -2147418113;
		if (m_pRecordManager != null && device != null)
		{
			StringBuilder stringBuilder = new StringBuilder();
			num = device.GetTrackUrl(dwTrackNumber, stringBuilder);
			if (num >= 0)
			{
				fixed (ushort* ptr = &Unsafe.As<char, ushort>(ref global::_003CModule_003E.PtrToStringChars(stringBuilder.ToString())))
				{
					try
					{
						int num2 = *(int*)m_pRecordManager + 16;
						num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, ushort*, int>)(int)(*(uint*)num2))((nint)m_pRecordManager, ptr);
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

	internal unsafe void OnRecordStart(ushort* pwszUrl)
	{
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Expected O, but got Unknown
		if (m_RecordStartHandler != null)
		{
			string text = new string((char*)pwszUrl);
			object[] array = new object[2] { this, text };
			Application.DeferredInvoke(new DeferredInvokeHandler(DeferredOnRecordStart), (object)array);
		}
	}

	internal unsafe void OnRecordProgress(ushort* pwszUrl, int iTicks)
	{
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Expected O, but got Unknown
		if (m_RecordProgressHandler != null)
		{
			string text = new string((char*)pwszUrl);
			object[] array = new object[3] { this, text, iTicks };
			Application.DeferredInvoke(new DeferredInvokeHandler(DeferredOnRecordProgress), (object)array);
		}
	}

	internal unsafe void OnRecordStop(ushort* pwszUrl, int hr)
	{
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Expected O, but got Unknown
		if (m_RecordStopHandler != null)
		{
			string text = new string((char*)pwszUrl);
			object[] array = new object[3] { this, text, hr };
			Application.DeferredInvoke(new DeferredInvokeHandler(DeferredOnRecordStop), (object)array);
		}
	}

	internal unsafe void OnRecordPause(ushort* pwszUrl)
	{
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Expected O, but got Unknown
		if (m_RecordPauseHandler != null)
		{
			string text = new string((char*)pwszUrl);
			object[] array = new object[2] { this, text };
			Application.DeferredInvoke(new DeferredInvokeHandler(DeferredOnRecordPause), (object)array);
		}
	}

	internal unsafe void OnRecordResume(ushort* pwszUrl)
	{
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Expected O, but got Unknown
		if (m_RecordResumeHandler != null)
		{
			string text = new string((char*)pwszUrl);
			object[] array = new object[2] { this, text };
			Application.DeferredInvoke(new DeferredInvokeHandler(DeferredOnRecordResume), (object)array);
		}
	}

	internal static void DeferredOnRecordStart(object args)
	{
		if (args is object[] array && (nint)array.LongLength == 2)
		{
			ZuneLibraryCDRecorder obj = (ZuneLibraryCDRecorder)array[0];
			string sourceUrl = (string)array[1];
			obj.m_RecordStartHandler?.Invoke(sourceUrl);
		}
	}

	internal static void DeferredOnRecordProgress(object args)
	{
		if (args is object[] array && (nint)array.LongLength == 3)
		{
			ZuneLibraryCDRecorder obj = (ZuneLibraryCDRecorder)array[0];
			string sourceUrl = (string)array[1];
			int iTicks = (int)array[2];
			obj.m_RecordProgressHandler?.Invoke(sourceUrl, iTicks);
		}
	}

	internal static void DeferredOnRecordStop(object args)
	{
		if (args is object[] array && (nint)array.LongLength == 3)
		{
			ZuneLibraryCDRecorder obj = (ZuneLibraryCDRecorder)array[0];
			string sourceUrl = (string)array[1];
			int hr = (int)array[2];
			obj.m_RecordStopHandler?.Invoke(sourceUrl, hr);
		}
	}

	internal static void DeferredOnRecordPause(object args)
	{
		if (args is object[] array && (nint)array.LongLength == 2)
		{
			ZuneLibraryCDRecorder obj = (ZuneLibraryCDRecorder)array[0];
			string sourceUrl = (string)array[1];
			obj.m_RecordPauseHandler?.Invoke(sourceUrl);
		}
	}

	internal static void DeferredOnRecordResume(object args)
	{
		if (args is object[] array && (nint)array.LongLength == 2)
		{
			ZuneLibraryCDRecorder obj = (ZuneLibraryCDRecorder)array[0];
			string sourceUrl = (string)array[1];
			obj.m_RecordResumeHandler?.Invoke(sourceUrl);
		}
	}

	protected virtual void Dispose([MarshalAs(UnmanagedType.U1)] bool P_0)
	{
		if (P_0)
		{
			_007EZuneLibraryCDRecorder();
			return;
		}
		try
		{
			_0021ZuneLibraryCDRecorder();
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

	~ZuneLibraryCDRecorder()
	{
		Dispose(false);
	}
}
