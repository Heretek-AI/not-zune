using Microsoft.Iris;
using Microsoft.Zune.Util;

namespace ZuneUI;

public abstract class DownloadCommand : ProgressCommand
{
	private DownloadProgressHandler m_progressHandler;

	private EDownloadTaskState m_downloadState = (EDownloadTaskState)7;

	private DownloadTask m_downloadTask;

	private string m_taskId;

	private int m_progress;

	private volatile object m_myLock;

	public string TaskId
	{
		set
		{
			m_taskId = value;
			Refresh();
		}
	}

	public DownloadCommand(IModelItem owner)
		: base(owner)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_002c: Expected O, but got Unknown
		base.Progress = -1f;
		m_progressHandler = new DownloadProgressHandler(OnProgressChanged);
		m_myLock = new object();
	}

	public void Refresh()
	{
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Invalid comparison between Unknown and I4
		lock (m_myLock)
		{
			m_downloadState = GetDownloadState();
			if ((int)m_downloadState == 2)
			{
				UpdateDownloadTask();
			}
		}
		UpdateCommandState(null);
	}

	protected abstract EDownloadTaskState GetDownloadState();

	protected virtual string GetDownloadString(EDownloadTaskState downloadState)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0024: Expected I4, but got Unknown
		switch ((int)downloadState)
		{
		case 0:
		case 1:
			return Shell.LoadString(StringId.IDS_PENDING);
		case 2:
		case 3:
			return string.Format(Shell.LoadString(StringId.IDS_DOWNLOAD_PROGRESS), m_progress.ToString());
		case 6:
			return Shell.LoadString(StringId.IDS_INCOLLECTION);
		default:
			return Shell.LoadString(StringId.IDS_DOWNLOAD);
		}
	}

	protected override void OnDispose(bool disposing)
	{
		((ModelItem)this).OnDispose(disposing);
		if (!disposing)
		{
			return;
		}
		lock (m_myLock)
		{
			if (m_downloadTask != null)
			{
				m_downloadTask.OnProgressChanged -= m_progressHandler;
				m_downloadTask = null;
			}
		}
	}

	protected virtual void OnProgressChanged(DownloadEventArguments args)
	{
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_002f: Invalid comparison between Unknown and I4
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Invalid comparison between Unknown and I4
		//IL_006e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0079: Expected O, but got Unknown
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Invalid comparison between Unknown and I4
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		//IL_0059: Unknown result type (might be due to invalid IL or missing references)
		lock (m_myLock)
		{
			m_downloadState = args.State;
			m_progress = (int)args.Progress;
			if ((int)m_downloadState != 2 && (int)m_downloadState != 3 && (int)m_downloadState != 1 && (int)m_downloadState != 0)
			{
				m_downloadTask = null;
				m_downloadState = GetDownloadState();
			}
		}
		Application.DeferredInvoke(new DeferredInvokeHandler(UpdateCommandState), (DeferredInvokePriority)0);
	}

	private void UpdateCommandState(object args)
	{
		//IL_0003: Unknown result type (might be due to invalid IL or missing references)
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_001a: Invalid comparison between Unknown and I4
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_002b: Invalid comparison between Unknown and I4
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Invalid comparison between Unknown and I4
		((ModelItem)this).Description = GetDownloadString(m_downloadState);
		((Command)this).Available = (int)m_downloadState != 6;
		if ((int)m_downloadState == 2 || (int)m_downloadState == 3)
		{
			base.Progress = (float)((double)m_progress / 100.0);
		}
		else
		{
			base.Progress = -1f;
		}
	}

	private void UpdateDownloadTask()
	{
		//IL_0093: Unknown result type (might be due to invalid IL or missing references)
		//IL_0098: Unknown result type (might be due to invalid IL or missing references)
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Invalid comparison between Unknown and I4
		//IL_0081: Unknown result type (might be due to invalid IL or missing references)
		//IL_0086: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		//IL_004c: Invalid comparison between Unknown and I4
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Invalid comparison between Unknown and I4
		if (m_downloadTask == null)
		{
			if (m_taskId == null)
			{
				return;
			}
			m_downloadTask = DownloadManager.Instance.GetTask(m_taskId);
			if (m_downloadTask != null)
			{
				if ((int)m_downloadState == 0 || (int)m_downloadState == 1 || (int)m_downloadState == 2 || (int)m_downloadState == 3)
				{
					m_downloadTask.OnProgressChanged += m_progressHandler;
				}
				m_progress = (int)m_downloadTask.GetProgress();
				m_downloadState = m_downloadTask.GetState();
			}
		}
		else
		{
			m_downloadState = m_downloadTask.GetState();
		}
	}
}
