using System;
using Microsoft.Iris;
using Microsoft.Zune.Util;
using ZuneXml;

namespace ZuneUI;

public class ProgressCommand : Command
{
	private const int PlaybackBufferSecs = 200;

	private const int PlaybackThresholdSecs = 10;

	private bool m_allowPlayback = true;

	private float m_progress;

	private float m_progressivePlaybackPoint;

	private int m_secondsToDownload = -1;

	private int m_secondsToProgressivePlayback = -1;

	private int m_secondsToPlaybackThreshold = 5;

	private DownloadTask m_downloadTask;

	public bool AllowProgressivePlayback
	{
		get
		{
			return m_allowPlayback;
		}
		set
		{
			if (m_allowPlayback != value)
			{
				m_allowPlayback = value;
				((ModelItem)this).FirePropertyChanged("AllowProgressivePlayback");
			}
		}
	}

	public float Progress
	{
		get
		{
			return m_progress;
		}
		set
		{
			if (m_progress != value)
			{
				m_progress = value;
				((ModelItem)this).FirePropertyChanged("Progress");
			}
		}
	}

	public float ProgressivePlaybackPoint
	{
		get
		{
			return m_progressivePlaybackPoint;
		}
		private set
		{
			if (m_progressivePlaybackPoint != value)
			{
				m_progressivePlaybackPoint = value;
				((ModelItem)this).FirePropertyChanged("ProgressivePlaybackPoint");
			}
		}
	}

	public int SecondsToProgressivePlayback
	{
		get
		{
			return m_secondsToProgressivePlayback;
		}
		private set
		{
			if (m_secondsToProgressivePlayback != value)
			{
				m_secondsToProgressivePlayback = value;
				((ModelItem)this).FirePropertyChanged("SecondsToProgressivePlayback");
			}
		}
	}

	public int SecondsToDownload
	{
		get
		{
			return m_secondsToDownload;
		}
		private set
		{
			if (m_secondsToDownload != value)
			{
				m_secondsToDownload = value;
				((ModelItem)this).FirePropertyChanged("SecondsToDownload");
			}
		}
	}

	public string TimeToProgressivePlayback => TimeFormattingHelper.FormatSeconds(SecondsToProgressivePlayback);

	public string TimeToDownload => TimeFormattingHelper.FormatSeconds(SecondsToDownload);

	public ProgressCommand()
	{
	}

	public ProgressCommand(IModelItem owner)
		: base((IModelItemOwner)(object)owner)
	{
	}

	public ProgressCommand(DownloadTask downloadTask)
	{
		m_downloadTask = downloadTask;
	}

	protected void UpdateProgress(Guid taskId, float percent)
	{
		if (m_downloadTask == null)
		{
			m_downloadTask = DownloadManager.Instance.GetTask(taskId.ToString());
		}
		else if (taskId == Guid.Empty && percent < 0f)
		{
			m_downloadTask = null;
		}
		float num = 0f;
		int num2 = -1;
		int num3 = -1;
		if (m_downloadTask != null)
		{
			num2 = m_downloadTask.GetDownloadSecondsRemaining();
			int propertyInt = m_downloadTask.GetPropertyInt("PlaybackDuration");
			if (propertyInt > 0)
			{
				int downloadFileSecondsRemaining = m_downloadTask.GetDownloadFileSecondsRemaining(0);
				if (downloadFileSecondsRemaining >= 0)
				{
					int num4 = downloadFileSecondsRemaining - propertyInt;
					long bytesDownloaded = (long)m_downloadTask.GetBytesDownloaded();
					long num5 = (long)CalculateMinimumFileSizeForPlayback(m_downloadTask.GetFinalFileSize(0), propertyInt);
					int num6 = m_downloadTask.GetDownloadBytesPerSecond();
					if (num6 <= 0)
					{
						num6 = 1;
					}
					if (AllowProgressivePlayback)
					{
						int num7 = (int)((num5 - bytesDownloaded) / num6);
						num3 = ((num4 > num7) ? num4 : num7) + m_secondsToPlaybackThreshold;
						if (num3 > 0)
						{
							num = (float)((double)percent + (100.0 - (double)percent) * (double)num3 / (double)num2);
							m_secondsToPlaybackThreshold = 10;
						}
						else
						{
							num3 = 0;
							m_secondsToPlaybackThreshold = 0;
						}
					}
				}
			}
		}
		Progress = percent / 100f;
		ProgressivePlaybackPoint = num / 100f;
		SecondsToProgressivePlayback = num3;
		SecondsToDownload = num2;
	}

	public void InvokeProgressivePlayback()
	{
		if (m_downloadTask == null || SecondsToProgressivePlayback != 0)
		{
			return;
		}
		string tempFileName = m_downloadTask.GetTempFileName(0);
		if (string.IsNullOrEmpty(tempFileName))
		{
			return;
		}
		int propertyInt = m_downloadTask.GetPropertyInt("PlaybackDuration");
		if (propertyInt != 0)
		{
			ulong finalFileSize = m_downloadTask.GetFinalFileSize(0);
			ulong bytesDownloaded = m_downloadTask.GetBytesDownloaded();
			if (finalFileSize != 0 && bytesDownloaded > CalculateMinimumFileSizeForPlayback(finalFileSize, propertyInt))
			{
				string uri = $"zuneprogdl://{tempFileName}?duration={propertyInt}&size={finalFileSize}";
				string property = m_downloadTask.GetProperty("Title");
				string property2 = m_downloadTask.GetProperty("Artist");
				Guid zuneMediaId = new Guid(m_downloadTask.GetProperty("ServiceId"));
				VideoPlaybackTrack item = new VideoPlaybackTrack(zuneMediaId, property, property2, uri, isDownloading: true, isStreaming: false, ignoreCollection: false, fallbackToPreview: false, forcePreview: false, VideoDefinitionEnum.None);
				SingletonModelItem<TransportControls>.Instance.PlayItem(item, PlayNavigationOptions.NavigateToNowPlaying);
			}
		}
	}

	private ulong CalculateMinimumFileSizeForPlayback(ulong finalSize, int finalDuration)
	{
		ulong result = 0uL;
		if (m_downloadTask != null)
		{
			result = finalSize / (ulong)finalDuration * 200;
		}
		return result;
	}
}
