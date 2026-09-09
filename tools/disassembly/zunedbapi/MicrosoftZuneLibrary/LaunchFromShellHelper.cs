using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using Microsoft.Iris;

namespace MicrosoftZuneLibrary;

public class LaunchFromShellHelper
{
	private unsafe delegate void FileFoundCallback(ushort* fileName, EMediaTypes mediaType);

	private string _003Cbacking_store_003ETaskName;

	private DeferredInvokeHandler _completeHandler;

	private string _startParam;

	private string _eventName;

	private bool _startParamIsDataObject;

	private bool _cancelled;

	private List<FileEntry> _files;

	public string TaskName
	{
		get
		{
			return _003Cbacking_store_003ETaskName;
		}
		set
		{
			_003Cbacking_store_003ETaskName = value;
		}
	}

	public List<FileEntry> Files => _files;

	public LaunchFromShellHelper(string taskName, string marshalledDataObject, string eventName)
	{
		Init(taskName, marshalledDataObject, eventName, startParamIsDataObject: true);
	}

	public LaunchFromShellHelper(string taskName, string initialUrl)
	{
		Init(taskName, initialUrl, null, startParamIsDataObject: false);
	}

	public void Go(DeferredInvokeHandler completeHandler)
	{
		_completeHandler = completeHandler;
		Thread thread = new Thread(ThreadProc);
		thread.SetApartmentState(ApartmentState.STA);
		thread.IsBackground = true;
		thread.Start();
	}

	public void Cancel()
	{
		_cancelled = true;
	}

	private void Init(string taskName, string startParam, string eventName, [MarshalAs(UnmanagedType.U1)] bool startParamIsDataObject)
	{
		_003Cbacking_store_003ETaskName = taskName.ToLowerInvariant();
		_startParam = startParam;
		_eventName = eventName;
		_startParamIsDataObject = startParamIsDataObject;
		_files = new List<FileEntry>();
	}

	private unsafe void ThreadProc()
	{
		//IL_00e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ee: Expected O, but got Unknown
		EventWaitHandle eventWaitHandle = null;
		IDataObjectEnumerator* ptr = null;
		fixed (ushort* ptr2 = &Unsafe.As<char, ushort>(ref global::_003CModule_003E.PtrToStringChars(_startParam)))
		{
			FileFoundCallback fileFoundCallback = FileFound;
			delegate* unmanaged[Stdcall, Stdcall]<ushort*, EMediaTypes, void> delegate_002A = (delegate* unmanaged[Stdcall, Stdcall]<ushort*, EMediaTypes, void>)Marshal.GetFunctionPointerForDelegate((Delegate)fileFoundCallback).ToPointer();
			int num = global::_003CModule_003E.ZuneLibraryExports_002ECreateDataObjectEnum(&ptr);
			try
			{
				if (num >= 0)
				{
					int num2 = *(int*)ptr;
					num = ((delegate* unmanaged[Thiscall, Thiscall]<IntPtr, ushort*, int, delegate* unmanaged[Stdcall, Stdcall]<ushort*, EMediaTypes, void>, int>)(int)(*(uint*)num2))((nint)ptr, ptr2, _startParamIsDataObject ? 1 : 0, delegate_002A);
				}
				if (_eventName != null)
				{
					try
					{
						eventWaitHandle = EventWaitHandle.OpenExisting(_eventName);
						eventWaitHandle.Set();
					}
					catch (ArgumentException)
					{
					}
					catch (WaitHandleCannotBeOpenedException)
					{
					}
					catch (IOException)
					{
					}
					catch (UnauthorizedAccessException)
					{
					}
					finally
					{
						((IDisposable)eventWaitHandle)?.Dispose();
					}
				}
				if (num >= 0)
				{
					bool flag = true;
					while (!_cancelled && num >= 0 && flag)
					{
						num = ((delegate* unmanaged[Thiscall, Thiscall]<IntPtr, bool*, int>)(int)(*(uint*)(*(int*)ptr + 4)))((nint)ptr, &flag);
					}
				}
			}
			finally
			{
				if (ptr != null)
				{
					global::_003CModule_003E.ZuneLibraryExports_002EDestroyDataObjectEnum(ptr);
					ptr = null;
				}
			}
			GC.KeepAlive(fileFoundCallback);
			if (!_cancelled)
			{
				Application.DeferredInvoke(new DeferredInvokeHandler(CompletedOnAppThread), (object)null);
			}
		}
	}

	private void CompletedOnAppThread(object unused)
	{
		if (!_cancelled)
		{
			_completeHandler.Invoke((object)this);
		}
	}

	private unsafe void FileFound(ushort* fileName, EMediaTypes mediaType)
	{
		_files.Add(new FileEntry(fileName, mediaType));
	}
}
