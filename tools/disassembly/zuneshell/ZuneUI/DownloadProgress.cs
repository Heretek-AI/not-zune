using System;
using Microsoft.Iris;
using Microsoft.Zune.Service;
using Microsoft.Zune.Util;

namespace ZuneUI;

public class DownloadProgress : ProgressCommand
{
	private EDownloadTaskState m_downloadState = (EDownloadTaskState)7;

	private DownloadTask m_downloadTask;

	protected EContentType ContentType
	{
		get
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			//IL_0021: Unknown result type (might be due to invalid IL or missing references)
			//IL_0032: Unknown result type (might be due to invalid IL or missing references)
			//IL_0055: Unknown result type (might be due to invalid IL or missing references)
			//IL_0043: Unknown result type (might be due to invalid IL or missing references)
			//IL_0054: Unknown result type (might be due to invalid IL or missing references)
			EContentType result = (EContentType)(-1);
			switch (m_downloadTask.GetProperty("Type"))
			{
			case "type:musictrack":
				result = (EContentType)0;
				break;
			case "type:musicvideo":
				result = (EContentType)3;
				break;
			case "type:podcast":
				result = (EContentType)5;
				break;
			case "type:app":
				result = (EContentType)7;
				break;
			}
			return result;
		}
	}

	protected Guid ServiceId
	{
		get
		{
			Guid result = Guid.Empty;
			string property = m_downloadTask.GetProperty("ServiceId");
			if (!string.IsNullOrEmpty(property))
			{
				result = new Guid(property);
			}
			return result;
		}
	}

	protected int PodcastEpisodeId => m_downloadTask.GetPropertyInt("MediaId");

	protected int CollectionId
	{
		get
		{
			//IL_000c: Unknown result type (might be due to invalid IL or missing references)
			int result = default(int);
			bool flag = default(bool);
			if (!Service.Instance.InCompleteCollection(ServiceId, ContentType, ref result, ref flag))
			{
				return -1;
			}
			return result;
		}
	}

	protected EDownloadFlags DownloadFlags => (EDownloadFlags)m_downloadTask.GetPropertyInt("DownloadFlags");

	protected int SubscriptionMediaId => m_downloadTask.GetPropertyInt("SubscriptionMediaId");

	protected int SubscriptionItemMediaId => m_downloadTask.GetPropertyInt("SubscriptionItemMediaId");

	protected int PlaylistId => m_downloadTask.GetPropertyInt("PlaylistId");

	public DownloadProgress(DownloadTask downloadTask)
		: base(downloadTask)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Expected O, but got Unknown
		m_downloadTask = downloadTask;
		m_downloadState = m_downloadTask.GetState();
		m_downloadTask.OnProgressChanged += new DownloadProgressHandler(OnProgressChanged);
		((Command)this).Available = false;
		UpdateState(null);
	}

	protected override void OnDispose(bool disposing)
	{
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0029: Expected O, but got Unknown
		((ModelItem)this).OnDispose(disposing);
		if (disposing && m_downloadTask != null)
		{
			m_downloadTask.OnProgressChanged -= new DownloadProgressHandler(OnProgressChanged);
			m_downloadTask = null;
		}
	}

	public void FindInCollection()
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_0009: Invalid comparison between Unknown and I4
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		//IL_005b: Unknown result type (might be due to invalid IL or missing references)
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		//IL_006e: Invalid comparison between Unknown and I4
		//IL_007c: Unknown result type (might be due to invalid IL or missing references)
		//IL_007e: Invalid comparison between Unknown and I4
		EContentType contentType = ContentType;
		if ((int)contentType == 5)
		{
			PodcastLibraryPage.FindInCollection(-1, PodcastEpisodeId);
			return;
		}
		if ((DownloadFlags & 1) != 0)
		{
			if (PlaylistId > 0)
			{
				MusicLibraryPage.FindPlaylistInCollection(PlaylistId, CollectionId, selectTrack: true);
			}
			else
			{
				ChannelLibraryPage.FindInCollection(SubscriptionMediaId, SubscriptionItemMediaId);
			}
			return;
		}
		int collectionId = CollectionId;
		if (collectionId >= 0)
		{
			if ((int)contentType == 0)
			{
				MusicLibraryPage.FindInCollection(-1, -1, CollectionId);
			}
			else if ((int)contentType == 3)
			{
				VideoLibraryPage.FindInCollection(CollectionId);
			}
			else if ((int)contentType == 7)
			{
				ApplicationLibraryPage.FindInCollection(CollectionId);
			}
		}
	}

	private void OnProgressChanged(DownloadEventArguments args)
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_0013: Expected O, but got Unknown
		Application.DeferredInvoke(new DeferredInvokeHandler(UpdateState), (object)args, (DeferredInvokePriority)0);
	}

	private void UpdateState(object args)
	{
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0015: Expected O, but got Unknown
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0070: Expected I4, but got Unknown
		base.Progress = -1f;
		float progress;
		if (args != null)
		{
			DownloadEventArguments val = (DownloadEventArguments)args;
			m_downloadState = val.State;
			progress = val.Progress;
		}
		else
		{
			m_downloadState = m_downloadTask.GetState();
			progress = m_downloadTask.GetProgress();
		}
		EDownloadTaskState downloadState = m_downloadState;
		switch ((int)downloadState)
		{
		case 0:
		case 1:
			((ModelItem)this).Description = Shell.LoadString(StringId.IDS_PENDING);
			((Command)this).Available = false;
			break;
		case 2:
			UpdateProgress(Guid.Empty, progress);
			if (base.SecondsToProgressivePlayback == 0)
			{
				((ModelItem)this).Description = Shell.LoadString(StringId.IDS_PLAY_SONG);
			}
			else
			{
				((ModelItem)this).Description = string.Format(Shell.LoadString(StringId.IDS_DOWNLOAD_PROGRESS), (int)progress);
			}
			((Command)this).Available = true;
			break;
		case 3:
			((ModelItem)this).Description = string.Format(Shell.LoadString(StringId.IDS_DOWNLOAD_PROGRESS), (int)progress);
			base.Progress = progress / 100f;
			((Command)this).Available = false;
			break;
		case 6:
			((ModelItem)this).Description = Shell.LoadString(StringId.IDS_INCOLLECTION);
			((Command)this).Available = true;
			break;
		case 4:
			((ModelItem)this).Description = Shell.LoadString(StringId.IDS_CANCELLED);
			((Command)this).Available = false;
			break;
		case 5:
			((ModelItem)this).Description = Shell.LoadString(StringId.IDS_FAILED);
			((Command)this).Available = false;
			break;
		default:
			((ModelItem)this).Description = Shell.LoadString(StringId.IDS_PENDING);
			((Command)this).Available = false;
			break;
		}
	}

	protected override void OnInvoked()
	{
		if (((Command)this).Available)
		{
			if (base.SecondsToProgressivePlayback == 0)
			{
				InvokeProgressivePlayback();
			}
			else
			{
				FindInCollection();
			}
		}
	}
}
