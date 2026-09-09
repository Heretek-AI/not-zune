using System;
using System.Collections;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using Microsoft.Iris;

namespace Microsoft.Zune.Util;

public class DownloadManager : IDisposable
{
	private unsafe DownloadManagerProxy* m_pDownloadManagerProxy;

	private static DownloadManager sm_downloadManager = null;

	private static object sm_lock = new object();

	private ArrayListDataSet m_activeDownloadTasks;

	private ArrayListDataSet m_completedDownloadTasks;

	private ArrayListDataSet m_cancelledDownloadTasks;

	private ArrayListDataSet m_failedDownloadTasks;

	private DownloadManagerUpdateHandler m_defaultUpdateHandler;

	public ArrayListDataSet CancelledDownloads => m_cancelledDownloadTasks;

	public ArrayListDataSet FailedDownloads => m_failedDownloadTasks;

	public ArrayListDataSet CompletedDownloads => m_completedDownloadTasks;

	public ArrayListDataSet ActiveDownloads => m_activeDownloadTasks;

	public unsafe float Percentage => ((float*)m_pDownloadManagerProxy)[5];

	public unsafe bool HadFailures
	{
		[return: MarshalAs(UnmanagedType.U1)]
		get
		{
			return ((int*)m_pDownloadManagerProxy)[4] > 0;
		}
	}

	public unsafe bool HadCancellations
	{
		[return: MarshalAs(UnmanagedType.U1)]
		get
		{
			return ((int*)m_pDownloadManagerProxy)[3] > 0;
		}
	}

	public unsafe bool IsQueuePaused
	{
		[return: MarshalAs(UnmanagedType.U1)]
		get
		{
			bool result = false;
			IDownloadManager* ptr = null;
			if (global::_003CModule_003E.GetSingleton((_GUID)global::_003CModule_003E._GUID_399f851b_a600_4e88_90c3_03b8f2770076, (void**)(&ptr)) >= 0)
			{
				IDownloadManager* intPtr = ptr;
				result = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, byte>)(int)(*(uint*)(*(int*)intPtr + 72)))((nint)intPtr) != 0;
			}
			if (null != ptr)
			{
				IDownloadManager* intPtr2 = ptr;
				((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint>)(int)(*(uint*)(*(int*)intPtr2 + 8)))((nint)intPtr2);
			}
			return result;
		}
	}

	public unsafe bool Finished
	{
		[return: MarshalAs(UnmanagedType.U1)]
		get
		{
			return ((int*)m_pDownloadManagerProxy)[1] <= 0;
		}
	}

	public unsafe int ActiveItem
	{
		get
		{
			DownloadManagerProxy* pDownloadManagerProxy = m_pDownloadManagerProxy;
			return ((int*)pDownloadManagerProxy)[2] + (((int*)pDownloadManagerProxy)[4] + ((int*)pDownloadManagerProxy)[3]) + 1;
		}
	}

	public unsafe int TotalInProgressItems => ((int*)m_pDownloadManagerProxy)[1];

	public unsafe int TotalItems
	{
		get
		{
			DownloadManagerProxy* pDownloadManagerProxy = m_pDownloadManagerProxy;
			return ((int*)pDownloadManagerProxy)[4] + ((int*)pDownloadManagerProxy)[3] + ((int*)pDownloadManagerProxy)[2] + ((int*)pDownloadManagerProxy)[1];
		}
	}

	public static DownloadManager Instance
	{
		get
		{
			if (sm_downloadManager == null)
			{
				try
				{
					Monitor.Enter(sm_lock);
					if (sm_downloadManager == null)
					{
						DownloadManager downloadManager = new DownloadManager();
						Thread.MemoryBarrier();
						sm_downloadManager = downloadManager;
					}
				}
				finally
				{
					Monitor.Exit(sm_lock);
				}
			}
			return sm_downloadManager;
		}
	}

	[SpecialName]
	public unsafe event DownloadManagerUpdateHandler OnProgressChanged
	{
		add
		{
			global::_003CModule_003E.Microsoft_002EZune_002EUtil_002EDownloadManagerProxy_002EAddDelegate(m_pDownloadManagerProxy, value);
		}
		remove
		{
			global::_003CModule_003E.Microsoft_002EZune_002EUtil_002EDownloadManagerProxy_002ERemoveDelegate(m_pDownloadManagerProxy, value);
		}
	}

	private void _007EDownloadManager()
	{
		if (m_defaultUpdateHandler != null)
		{
			OnProgressChanged -= m_defaultUpdateHandler;
			m_defaultUpdateHandler = null;
		}
		_0021DownloadManager();
	}

	private unsafe void _0021DownloadManager()
	{
		DownloadManagerProxy* pDownloadManagerProxy = m_pDownloadManagerProxy;
		if (null != pDownloadManagerProxy)
		{
			((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint>)(int)(*(uint*)(*(int*)pDownloadManagerProxy + 8)))((nint)pDownloadManagerProxy);
			m_pDownloadManagerProxy = null;
		}
	}

	public static DownloadManager CreateInstance()
	{
		return Instance;
	}

	public unsafe DownloadTask GetTask(string taskId)
	{
		IDownloadManager* ptr = null;
		IDownloadTask* ptr2 = null;
		DownloadTask result = null;
		fixed (ushort* ptr3 = &Unsafe.As<char, ushort>(ref global::_003CModule_003E.PtrToStringChars(taskId)))
		{
			int singleton = global::_003CModule_003E.GetSingleton((_GUID)global::_003CModule_003E._GUID_399f851b_a600_4e88_90c3_03b8f2770076, (void**)(&ptr));
			if (singleton >= 0)
			{
				int num = *(int*)ptr + 36;
				singleton = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, ushort*, IDownloadTask**, int>)(int)(*(uint*)num))((nint)ptr, ptr3, &ptr2);
				if (singleton >= 0)
				{
					result = new DownloadTask(ptr2);
				}
			}
			if (null != ptr)
			{
				IDownloadManager* intPtr = ptr;
				((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint>)(int)(*(uint*)(*(int*)intPtr + 8)))((nint)intPtr);
				ptr = null;
			}
			if (null != ptr2)
			{
				IDownloadTask* intPtr2 = ptr2;
				((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint>)(int)(*(uint*)(*(int*)intPtr2 + 8)))((nint)intPtr2);
			}
			return result;
		}
	}

	public int SetPosition(IList list, int position)
	{
		int num = 0;
		if (list != null && position >= 0)
		{
			ArrayList arrayList = new ArrayList(list.Count);
			int num2 = list.Count - 1;
			if (num2 >= 0)
			{
				do
				{
					if (list[num2] is DownloadTask downloadTask && downloadTask.CanReorder() && downloadTask.SetPosition(position))
					{
						arrayList.Add(downloadTask);
						num++;
					}
					num2--;
				}
				while (num2 >= 0);
				if (num > 0)
				{
					DownloadManagerMoveArguments args = new DownloadManagerMoveArguments(arrayList, position);
					UpdatePositions(args);
				}
			}
		}
		return num;
	}

	public void DownloadNext(DownloadTask task)
	{
		if (task != null && task.CanReorder())
		{
			task.DownloadNext();
			UpdateActiveList();
		}
	}

	public void RemoveFailed(DownloadTask task)
	{
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Expected O, but got Unknown
		if (Application.IsApplicationThread)
		{
			DeferredRemoveFailed(task);
		}
		else
		{
			Application.DeferredInvoke(new DeferredInvokeHandler(DeferredRemoveFailed), (object)task);
		}
	}

	public unsafe void PauseQueue()
	{
		global::_003CModule_003E.Microsoft_002EZune_002EUtil_002EDownloadManagerProxy_002EPauseQueue(m_pDownloadManagerProxy);
	}

	public unsafe void ResumeQueue()
	{
		global::_003CModule_003E.Microsoft_002EZune_002EUtil_002EDownloadManagerProxy_002EResumeQueue(m_pDownloadManagerProxy);
	}

	[return: MarshalAs(UnmanagedType.U1)]
	public unsafe bool SignInRequired()
	{
		bool flag = false;
		IDownloadManager* ptr = null;
		if (global::_003CModule_003E.GetSingleton((_GUID)global::_003CModule_003E._GUID_399f851b_a600_4e88_90c3_03b8f2770076, (void**)(&ptr)) >= 0)
		{
			IDownloadManager* intPtr = ptr;
			int num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, int>)(int)(*(uint*)(*(int*)intPtr + 44)))((nint)intPtr);
			int num2 = 0;
			if (0 < num)
			{
				while (!flag)
				{
					IDownloadTask* ptr2 = null;
					if (((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, int, IDownloadTask**, int>)(int)(*(uint*)(*(int*)ptr + 52)))((nint)ptr, num2, &ptr2) >= 0)
					{
						IDownloadTask* intPtr2 = ptr2;
						if (((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, EDownloadType>)(int)(*(uint*)(*(int*)intPtr2 + 72)))((nint)intPtr2) == (EDownloadType)0 && ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, int*, EDownloadTaskState>)(int)(*(uint*)(*(int*)ptr2 + 100)))((nint)ptr2, null) == EDownloadTaskState.DLTaskPendingAttach)
						{
							flag = true;
						}
					}
					if (null != ptr2)
					{
						IDownloadTask* intPtr3 = ptr2;
						((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint>)(int)(*(uint*)(*(int*)intPtr3 + 8)))((nint)intPtr3);
					}
					num2++;
					if (num2 >= num)
					{
						break;
					}
				}
			}
		}
		if (null != ptr)
		{
			IDownloadManager* intPtr4 = ptr;
			((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint>)(int)(*(uint*)(*(int*)intPtr4 + 8)))((nint)intPtr4);
		}
		return flag;
	}

	private unsafe DownloadManager()
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Expected O, but got Unknown
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Expected O, but got Unknown
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Expected O, but got Unknown
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_0032: Expected O, but got Unknown
		m_activeDownloadTasks = new ArrayListDataSet();
		m_completedDownloadTasks = new ArrayListDataSet();
		m_failedDownloadTasks = new ArrayListDataSet();
		m_cancelledDownloadTasks = new ArrayListDataSet();
		DownloadManagerProxy* ptr = (DownloadManagerProxy*)global::_003CModule_003E.@new(76u);
		DownloadManagerProxy* ptr2;
		try
		{
			ptr2 = ((ptr == null) ? null : global::_003CModule_003E.Microsoft_002EZune_002EUtil_002EDownloadManagerProxy_002E_007Bctor_007D(ptr));
		}
		catch
		{
			//try-fault
			global::_003CModule_003E.delete(ptr);
			throw;
		}
		m_pDownloadManagerProxy = ptr2;
		if (ptr2 != null && global::_003CModule_003E.Microsoft_002EZune_002EUtil_002EDownloadManagerProxy_002EInitialize(ptr2) >= 0)
		{
			OnProgressChanged += (m_defaultUpdateHandler = DefaultUpdateHandler);
		}
	}

	private void DefaultUpdateHandler(DownloadManagerUpdateArguments args)
	{
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Expected O, but got Unknown
		if (args.Type == EDownloadManagerUpdateType.TaskAdded)
		{
			AddActiveTask(args);
		}
		else if (args.Type != EDownloadManagerUpdateType.TaskProgressChanged)
		{
			Application.DeferredInvoke(new DeferredInvokeHandler(DeferredUpdateInactiveLists), (object)args);
		}
	}

	private void UpdatePositions(DownloadManagerMoveArguments args)
	{
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Expected O, but got Unknown
		if (Application.IsApplicationThread)
		{
			DeferredUpdatePositions(args);
		}
		else
		{
			Application.DeferredInvoke(new DeferredInvokeHandler(DeferredUpdatePositions), (object)args);
		}
	}

	private void AddActiveTask(DownloadManagerUpdateArguments args)
	{
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Expected O, but got Unknown
		if (Application.IsApplicationThread)
		{
			DeferredAddActiveTask(args);
		}
		else
		{
			Application.DeferredInvoke(new DeferredInvokeHandler(DeferredAddActiveTask), (object)args);
		}
	}

	private void UpdateActiveList()
	{
		ThreadPool.QueueUserWorkItem(UpdateActiveListOnWorkerThread, this);
	}

	private unsafe static void UpdateActiveListOnWorkerThread(object args)
	{
		//IL_00bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c6: Expected O, but got Unknown
		Unsafe.SkipInit(out CComPtrNtv_003CIDownloadManager_003E cComPtrNtv_003CIDownloadManager_003E);
		*(int*)(&cComPtrNtv_003CIDownloadManager_003E) = 0;
		try
		{
			int singleton = global::_003CModule_003E.GetSingleton((_GUID)global::_003CModule_003E._GUID_399f851b_a600_4e88_90c3_03b8f2770076, (void**)(&cComPtrNtv_003CIDownloadManager_003E));
			int num = 0;
			if (singleton >= 0)
			{
				num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, int>)(int)(*(uint*)(*(int*)(int)(*(uint*)(&cComPtrNtv_003CIDownloadManager_003E)) + 44)))((IntPtr)(*(int*)(&cComPtrNtv_003CIDownloadManager_003E)));
				if (num < 0)
				{
					num = 0;
				}
			}
			IList list = new ArrayList(num);
			int num2 = 0;
			if (0 < num)
			{
				Unsafe.SkipInit(out CComPtrNtv_003CIDownloadTask_003E cComPtrNtv_003CIDownloadTask_003E);
				do
				{
					*(int*)(&cComPtrNtv_003CIDownloadTask_003E) = 0;
					try
					{
						singleton = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, int, IDownloadTask**, int>)(int)(*(uint*)(*(int*)(int)(*(uint*)(&cComPtrNtv_003CIDownloadManager_003E)) + 52)))((IntPtr)(*(int*)(&cComPtrNtv_003CIDownloadManager_003E)), num2, (IDownloadTask**)(&cComPtrNtv_003CIDownloadTask_003E));
						if (singleton >= 0 && ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, EDownloadType>)(int)(*(uint*)(*(int*)(int)(*(uint*)(&cComPtrNtv_003CIDownloadTask_003E)) + 72)))((IntPtr)(*(int*)(&cComPtrNtv_003CIDownloadTask_003E))) == (EDownloadType)0 && ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, int*, EDownloadTaskState>)(int)(*(uint*)(*(int*)(int)(*(uint*)(&cComPtrNtv_003CIDownloadTask_003E)) + 100)))((IntPtr)(*(int*)(&cComPtrNtv_003CIDownloadTask_003E)), null) != EDownloadTaskState.DLTaskComplete)
						{
							list.Add(new DownloadTask((IDownloadTask*)(int)(*(uint*)(&cComPtrNtv_003CIDownloadTask_003E))));
						}
					}
					catch
					{
						//try-fault
						global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<CComPtrNtv_003CIDownloadTask_003E*, void>)(&global::_003CModule_003E.CComPtrNtv_003CIDownloadTask_003E_002E_007Bdtor_007D), &cComPtrNtv_003CIDownloadTask_003E);
						throw;
					}
					global::_003CModule_003E.CComPtrNtv_003CIDownloadTask_003E_002ERelease(&cComPtrNtv_003CIDownloadTask_003E);
					num2++;
				}
				while (num2 < num);
			}
			Application.DeferredInvoke(new DeferredInvokeHandler(((DownloadManager)args).DeferredUpdateActiveList), (object)list);
		}
		catch
		{
			//try-fault
			global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<CComPtrNtv_003CIDownloadManager_003E*, void>)(&global::_003CModule_003E.CComPtrNtv_003CIDownloadManager_003E_002E_007Bdtor_007D), &cComPtrNtv_003CIDownloadManager_003E);
			throw;
		}
		global::_003CModule_003E.CComPtrNtv_003CIDownloadManager_003E_002ERelease(&cComPtrNtv_003CIDownloadManager_003E);
	}

	private void DeferredUpdateActiveList(object args)
	{
		((ListDataSet)m_activeDownloadTasks).Source = args as ArrayList;
	}

	private void DeferredUpdateInactiveLists(object args)
	{
		if (args is DownloadManagerUpdateArguments downloadManagerUpdateArguments)
		{
			if (downloadManagerUpdateArguments.Type == EDownloadManagerUpdateType.TaskCompleted)
			{
				((ListDataSet)m_completedDownloadTasks).Add((object)downloadManagerUpdateArguments.Task);
				((ListDataSet)m_activeDownloadTasks).Remove((object)downloadManagerUpdateArguments.Task);
			}
			else if (downloadManagerUpdateArguments.Type == EDownloadManagerUpdateType.TaskFailed)
			{
				((ListDataSet)m_failedDownloadTasks).Add((object)downloadManagerUpdateArguments.Task);
				((ListDataSet)m_activeDownloadTasks).Remove((object)downloadManagerUpdateArguments.Task);
			}
			else if (downloadManagerUpdateArguments.Type == EDownloadManagerUpdateType.TaskCancelled)
			{
				((ListDataSet)m_cancelledDownloadTasks).Add((object)downloadManagerUpdateArguments.Task);
				((ListDataSet)m_activeDownloadTasks).Remove((object)downloadManagerUpdateArguments.Task);
			}
			if (((ListDataSet)m_activeDownloadTasks).Count == 0)
			{
				DeferredClearLists(null);
			}
		}
	}

	private void DeferredClearLists(object args)
	{
		((ListDataSet)m_activeDownloadTasks).Clear();
		((ListDataSet)m_completedDownloadTasks).Clear();
		((ListDataSet)m_cancelledDownloadTasks).Clear();
	}

	private unsafe void DeferredRemoveFailed(object args)
	{
		if (!(args is DownloadTask downloadTask))
		{
			return;
		}
		switch (downloadTask.GetState())
		{
		case EDownloadTaskState.DLTaskCancelled:
		{
			int num2 = ((ListDataSet)m_cancelledDownloadTasks).IndexOf((object)downloadTask);
			if (num2 >= 0)
			{
				((ListDataSet)m_cancelledDownloadTasks).RemoveAt(num2);
				global::_003CModule_003E.Microsoft_002EZune_002EUtil_002EDownloadManagerProxy_002ERemoveCancelledCount(m_pDownloadManagerProxy);
			}
			break;
		}
		case EDownloadTaskState.DLTaskFailed:
		{
			int num = ((ListDataSet)m_failedDownloadTasks).IndexOf((object)downloadTask);
			if (num >= 0)
			{
				((ListDataSet)m_failedDownloadTasks).RemoveAt(num);
				global::_003CModule_003E.Microsoft_002EZune_002EUtil_002EDownloadManagerProxy_002ERemoveFailedCount(m_pDownloadManagerProxy);
			}
			break;
		}
		}
	}

	private void DeferredUpdatePositions(object args)
	{
		if (!(args is DownloadManagerMoveArguments { Position: var position } downloadManagerMoveArguments))
		{
			return;
		}
		if (position <= ((ListDataSet)m_activeDownloadTasks).Count && position >= 0)
		{
			int num = 0;
			if (0 >= downloadManagerMoveArguments.Tasks.Count)
			{
				return;
			}
			do
			{
				if (downloadManagerMoveArguments.Tasks[num] is DownloadTask downloadTask)
				{
					int num2 = ((ListDataSet)m_activeDownloadTasks).IndexOf((object)downloadTask);
					if (num2 >= 0)
					{
						((ListDataSet)m_activeDownloadTasks).Move(num2, position);
					}
				}
				num++;
			}
			while (num < downloadManagerMoveArguments.Tasks.Count);
		}
		else
		{
			UpdateActiveList();
		}
	}

	private unsafe void DeferredAddActiveTask(object args)
	{
		if (!(args is DownloadManagerUpdateArguments downloadManagerUpdateArguments))
		{
			return;
		}
		int num = ((int*)m_pDownloadManagerProxy)[1];
		if (num <= ((ListDataSet)m_activeDownloadTasks).Count + 1 && downloadManagerUpdateArguments.QueuePosition <= ((ListDataSet)m_activeDownloadTasks).Count && downloadManagerUpdateArguments.QueuePosition >= 0)
		{
			if (num != ((ListDataSet)m_activeDownloadTasks).Count || !((ListDataSet)m_activeDownloadTasks).Contains((object)downloadManagerUpdateArguments.Task))
			{
				((ListDataSet)m_activeDownloadTasks).Insert(downloadManagerUpdateArguments.QueuePosition, (object)downloadManagerUpdateArguments.Task);
			}
		}
		else
		{
			UpdateActiveList();
		}
	}

	protected virtual void Dispose([MarshalAs(UnmanagedType.U1)] bool P_0)
	{
		if (P_0)
		{
			_007EDownloadManager();
			return;
		}
		try
		{
			_0021DownloadManager();
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

	~DownloadManager()
	{
		Dispose(false);
	}
}
