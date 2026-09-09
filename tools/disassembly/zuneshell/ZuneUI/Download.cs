using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Microsoft.Iris;
using Microsoft.Zune.Configuration;
using Microsoft.Zune.Service;
using Microsoft.Zune.Shell;
using Microsoft.Zune.Util;
using ZuneXml;

namespace ZuneUI;

public class Download : ModelItem
{
	private DownloadEventHandler m_DownloadEvent;

	private DownloadEventProgressHandler m_DownloadProgressEvent;

	private bool m_disposed;

	private bool m_updatePending;

	private ProgressNotification m_notification;

	private Dictionary<Guid, int> m_errors = new Dictionary<Guid, int>();

	private Dictionary<Guid, int> m_historyErrors = new Dictionary<Guid, int>();

	private EDownloadContextEvent m_clientContextEvent;

	private string m_clientContextEventValue;

	private static Download s_instance;

	private static object s_cs = new object();

	private static string s_downloadCompleteMessage = Shell.LoadString(StringId.IDS_DOWNLOAD_COMPLETE_NOTIFICATION);

	private static string s_downloadFailedMessage = Shell.LoadString(StringId.IDS_DOWNLOAD_FAILED_NOTIFICATION);

	private static string s_downloadProgressMessage = Shell.LoadString(StringId.IDS_DOWNLOAD_PROGRESS_NOTIFICATION);

	private static string s_downloadCurrentMessage = Shell.LoadString(StringId.IDS_DOWNLOAD_CURRENT_NOTIFICATION);

	private static string s_downloadPausedMessage = Shell.LoadString(StringId.IDS_DOWNLOAD_PAUSED_NOTIFICATION);

	private static string s_downloadMBRPausedMessage = Shell.LoadString(StringId.IDS_DOWNLOAD_MBR_PAUSED_NOTIFICATION);

	private static string s_downloadSignInMessage = Shell.LoadString(StringId.IDS_DOWNLOAD_SIGNIN_NOTIFICATION);

	public static Download Instance
	{
		get
		{
			if (s_instance == null)
			{
				lock (s_cs)
				{
					if (s_instance == null)
					{
						s_instance = new Download();
					}
				}
			}
			return s_instance;
		}
	}

	public static bool IsCreated => s_instance != null;

	public int ErrorCount => m_errors.Count;

	public int HistoryErrorCount => m_historyErrors.Count;

	public ProgressNotification Notification
	{
		get
		{
			return m_notification;
		}
		private set
		{
			if (m_notification != value)
			{
				if (m_notification != null)
				{
					NotificationArea.Instance.Remove(m_notification);
				}
				m_notification = value;
				if (m_notification != null)
				{
					NotificationArea.Instance.Add(m_notification);
				}
				((ModelItem)this).FirePropertyChanged("Notification");
			}
		}
	}

	public DownloadTaskList DownloadTaskList => DownloadTaskList.Instance;

	public EDownloadContextEvent ClientContextEvent
	{
		get
		{
			//IL_000b: Unknown result type (might be due to invalid IL or missing references)
			if (!ShouldTrackUsage())
			{
				return (EDownloadContextEvent)(-1);
			}
			return m_clientContextEvent;
		}
	}

	public string ClientContextEventValue
	{
		get
		{
			if (!ShouldTrackUsage())
			{
				return null;
			}
			return m_clientContextEventValue;
		}
	}

	public event DownloadEventHandler DownloadEvent
	{
		[MethodImpl(MethodImplOptions.Synchronized)]
		add
		{
			//IL_000d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0017: Expected O, but got Unknown
			this.m_DownloadEvent = (DownloadEventHandler)Delegate.Combine((Delegate?)(object)this.m_DownloadEvent, (Delegate?)(object)value);
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		remove
		{
			//IL_000d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0017: Expected O, but got Unknown
			this.m_DownloadEvent = (DownloadEventHandler)Delegate.Remove((Delegate?)(object)this.m_DownloadEvent, (Delegate?)(object)value);
		}
	}

	public event DownloadEventProgressHandler DownloadProgressEvent
	{
		[MethodImpl(MethodImplOptions.Synchronized)]
		add
		{
			//IL_000d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0017: Expected O, but got Unknown
			this.m_DownloadProgressEvent = (DownloadEventProgressHandler)Delegate.Combine((Delegate?)(object)this.m_DownloadProgressEvent, (Delegate?)(object)value);
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		remove
		{
			//IL_000d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0017: Expected O, but got Unknown
			this.m_DownloadProgressEvent = (DownloadEventProgressHandler)Delegate.Remove((Delegate?)(object)this.m_DownloadProgressEvent, (Delegate?)(object)value);
		}
	}

	public event EventHandler DownloadAllPendingEvent;

	public void DownloadContent(IList items, EDownloadFlags eDownloadFlags)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		DownloadContent(items, eDownloadFlags, null);
	}

	internal void DownloadContent(IList items, EDownloadFlags eDownloadFlags, string deviceEndpointId)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		DownloadContent(items, eDownloadFlags, deviceEndpointId, OnAllPending);
	}

	public void DownloadContent(IList items, EDownloadFlags eDownloadFlags, string deviceEndpointId, EventHandler onAllPending)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0009: Unknown result type (might be due to invalid IL or missing references)
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_0033: Expected O, but got Unknown
		//IL_0033: Expected O, but got Unknown
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_004c: Unknown result type (might be due to invalid IL or missing references)
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		ZuneApplication.Service.Download(items, eDownloadFlags, deviceEndpointId, ClientContextEvent, ClientContextEventValue, new DownloadEventHandler(OnDownloadEvent), new DownloadEventProgressHandler(OnDownloadProgressEvent), onAllPending);
		if ((eDownloadFlags & -21) == 0)
		{
			bool flag = ZuneShell.DefaultInstance.CurrentPage is InboxPage;
			if ((eDownloadFlags & 0x10) != 0)
			{
				SQMLog.Log((SQMDataId)(flag ? 75 : 85), 1);
			}
			else
			{
				SQMLog.Log((SQMDataId)(flag ? 76 : 84), 1);
			}
		}
	}

	public void AddToCollection(IList items)
	{
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_003e: Expected O, but got Unknown
		//IL_003e: Expected O, but got Unknown
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0063: Expected O, but got Unknown
		ZuneApplication.Service.Download(items, (EDownloadFlags)16, (string)null, ClientContextEvent, ClientContextEventValue, new DownloadEventHandler(OnDownloadEvent), new DownloadEventProgressHandler(OnDownloadProgressEvent), (EventHandler)OnAllPending);
		foreach (object item in items)
		{
			if (item is DataProviderObject)
			{
				DataProviderObject val = (DataProviderObject)item;
				if (val.TypeName == "PlaylistContentItem")
				{
					PlaylistManager.GetPlaylistId((int)val.GetProperty("LibraryId"));
					UsageDataService.ReportTrackAddToCollection((Guid)val.GetProperty("ZuneMediaId"), PlaylistManager.GetFieldValue((int)val.GetProperty("MediaId"), (EListType)2, 358, 0).ToString());
				}
				else if (val is Track)
				{
					Track track = (Track)(object)val;
					UsageDataService.ReportTrackAddToCollection(track.Id, track.ReferrerContext);
				}
			}
		}
	}

	public bool IsDownloadingOrPending(Guid mediaId, EContentType eContentType)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		if (!IsDownloading(mediaId, eContentType, out var fPending))
		{
			return fPending;
		}
		return true;
	}

	public bool IsDownloading(Guid mediaId, EContentType eContentType, out bool fPending)
	{
		//IL_0008: Unknown result type (might be due to invalid IL or missing references)
		bool flag = false;
		return ZuneApplication.Service.IsDownloading(mediaId, eContentType, ref fPending, ref flag);
	}

	public void CancelDownload(Guid mediaId)
	{
		ZuneApplication.Service.CancelDownload(mediaId, (EContentType)0);
	}

	public int GetErrorCode(Guid mediaId)
	{
		if (!m_errors.TryGetValue(mediaId, out var value))
		{
			return 0;
		}
		return value;
	}

	internal void SetErrorCode(Guid mediaId, int errorCode)
	{
		m_errors[mediaId] = errorCode;
		((ModelItem)this).FirePropertyChanged("ErrorCount");
	}

	internal void Phase2Init()
	{
		DownloadManager.CreateInstance();
	}

	internal void Phase3Init()
	{
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Expected O, but got Unknown
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0044: Expected O, but got Unknown
		//IL_0044: Expected O, but got Unknown
		DownloadManager.Instance.OnProgressChanged += new DownloadManagerUpdateHandler(OnProgressChanged);
		ZuneApplication.Service.RegisterForDownloadNotification(new DownloadEventHandler(OnDownloadEvent), new DownloadEventProgressHandler(OnDownloadProgressEvent), (EventHandler)OnAllPending);
		((ModelItem)SingletonModelItem<TransportControls>.Instance).PropertyChanged += OnTransportControlPropertyChanged;
	}

	private void ClearErrorCode(Guid mediaId)
	{
		m_errors.Remove(mediaId);
		((ModelItem)this).FirePropertyChanged("ErrorCount");
	}

	public int GetHistoryErrorCode(Guid mediaId)
	{
		if (!m_historyErrors.TryGetValue(mediaId, out var value))
		{
			return 0;
		}
		return value;
	}

	internal void SetHistoryErrorCode(Guid mediaId, int errorCode)
	{
		m_historyErrors[mediaId] = errorCode;
		((ModelItem)this).FirePropertyChanged("HistoryErrorCount");
	}

	private void OnDownloadEvent(Guid mediaId, HRESULT hr)
	{
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_0033: Expected O, but got Unknown
		if (!m_disposed)
		{
			Application.DeferredInvoke(new DeferredInvokeHandler(DeferredDownloadEvent), (object)new object[2] { mediaId, hr });
		}
	}

	private void OnAllPending(object sender, EventArgs e)
	{
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		//IL_001a: Expected O, but got Unknown
		if (!m_disposed)
		{
			Application.DeferredInvoke(new DeferredInvokeHandler(DefferedAllPending), (object)null);
		}
	}

	private void DeferredDownloadEvent(object arg)
	{
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		object[] array = (object[])arg;
		Guid guid = (Guid)array[0];
		HRESULT val = (HRESULT)array[1];
		if (((HRESULT)(ref val)).IsSuccess || val == HRESULT._E_ABORT || val == HRESULT._E_PENDING || val == HRESULT._E_ALREADY_EXISTS)
		{
			ClearErrorCode(guid);
		}
		else
		{
			SetErrorCode(guid, val.hr);
		}
		if (this.DownloadEvent != null)
		{
			this.DownloadEvent.Invoke(guid, val);
			((ModelItem)this).FirePropertyChanged("DownloadEvent");
		}
	}

	private void DefferedAllPending(object notUsed)
	{
		if (this.DownloadAllPendingEvent != null)
		{
			this.DownloadAllPendingEvent(this, null);
		}
	}

	private void OnDownloadProgressEvent(Guid mediaId, float percent)
	{
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0033: Expected O, but got Unknown
		if (!m_disposed)
		{
			Application.DeferredInvoke(new DeferredInvokeHandler(DeferredDownloadProgressEvent), (object)new object[2] { mediaId, percent });
		}
	}

	private void DeferredDownloadProgressEvent(object arg)
	{
		if (this.DownloadProgressEvent != null)
		{
			object[] array = (object[])arg;
			this.DownloadProgressEvent.Invoke((Guid)array[0], (float)array[1]);
			((ModelItem)this).FirePropertyChanged("DownloadProgressEvent");
		}
	}

	private void OnTransportControlPropertyChanged(object sender, PropertyChangedEventArgs args)
	{
		if (args.PropertyName == "SupressDownloads")
		{
			if (SingletonModelItem<TransportControls>.Instance.SupressDownloads)
			{
				DownloadManager.Instance.PauseQueue();
			}
			else
			{
				DownloadManager.Instance.ResumeQueue();
			}
		}
	}

	private void ShowCompletedMessage(bool cancellations, bool failures)
	{
		//IL_0067: Unknown result type (might be due to invalid IL or missing references)
		//IL_0080: Expected O, but got Unknown
		if (Notification != null && Notification.Type != NotificationState.Completed)
		{
			Notification.Type = NotificationState.Completed;
			if (failures)
			{
				Notification.Message = s_downloadFailedMessage;
			}
			else
			{
				Notification.Message = s_downloadCompleteMessage;
				Notification.Percentage = 100;
			}
			Notification.SubMessage = null;
			Application.DeferredInvoke(new DeferredInvokeHandler(HideCompletedMessage), (object)null, TimeSpan.FromSeconds(5.0));
		}
	}

	private void HideCompletedMessage(object args)
	{
		if (Notification != null && Notification.Type == NotificationState.Completed)
		{
			Notification = null;
		}
	}

	private void OnProgressChanged(DownloadManagerUpdateArguments args)
	{
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Expected O, but got Unknown
		DeferredInvokeHandler val = null;
		if (m_updatePending)
		{
			return;
		}
		m_updatePending = true;
		if (val == null)
		{
			val = (DeferredInvokeHandler)delegate
			{
				m_updatePending = false;
				int totalItems = DownloadManager.Instance.TotalItems;
				int activeItem = DownloadManager.Instance.ActiveItem;
				if (totalItems == 0)
				{
					Notification = null;
				}
				else
				{
					if (Notification == null)
					{
						Notification = new ProgressNotification(s_downloadProgressMessage, NotificationTask.Download, NotificationState.Normal, 0);
					}
					else
					{
						Notification.Type = NotificationState.Normal;
						Notification.Message = s_downloadProgressMessage;
					}
					if (DownloadManager.Instance.Finished)
					{
						ShowCompletedMessage(DownloadManager.Instance.HadCancellations, DownloadManager.Instance.HadFailures);
					}
					else
					{
						Notification.Percentage = (int)DownloadManager.Instance.Percentage;
						Notification.SubMessage = string.Format(s_downloadCurrentMessage, activeItem, totalItems);
						if (DownloadManager.Instance.IsQueuePaused)
						{
							if (SingletonModelItem<TransportControls>.Instance.IsStreamingVideo)
							{
								Notification.Message = s_downloadMBRPausedMessage;
							}
							else
							{
								Notification.Message = s_downloadPausedMessage;
							}
						}
						else if (!SignIn.Instance.SignedIn && DownloadManager.Instance.SignInRequired())
						{
							Notification.Message = s_downloadSignInMessage;
						}
					}
				}
				bool flag = !DownloadManager.Instance.Finished || ((ListDataSet)DownloadManager.Instance.FailedDownloads).Count > 0 || DownloadManager.Instance.HadFailures;
				Shell.MainFrame.Marketplace.UpdateDownloadPivot(flag);
				Shell.MainFrame.Collection.UpdateDownloadPivot(flag);
				if (!flag && ZuneShell.DefaultInstance.CurrentPage is DownloadsPage)
				{
					ZuneShell.DefaultInstance.NavigateBack();
				}
			};
		}
		Application.DeferredInvoke(val, (object)null);
	}

	private Download()
	{
	}

	protected override void OnDispose(bool fDisposing)
	{
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Expected O, but got Unknown
		((ModelItem)this).OnDispose(fDisposing);
		if (fDisposing)
		{
			DownloadManager.Instance.OnProgressChanged -= new DownloadManagerUpdateHandler(OnProgressChanged);
			((ModelItem)SingletonModelItem<TransportControls>.Instance).PropertyChanged -= OnTransportControlPropertyChanged;
		}
		m_disposed = true;
	}

	private bool ShouldTrackUsage()
	{
		if (ClientConfiguration.SQM.UsageTracking)
		{
			return ClientConfiguration.FUE.AcceptedPrivacyStatement;
		}
		return false;
	}

	public void ReportClientContextEvent(EDownloadContextEvent clientContextEvent, Guid clientContextEventValue)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		EDownloadContextEvent val = (EDownloadContextEvent)(-1);
		string text = null;
		if (clientContextEventValue != Guid.Empty)
		{
			val = clientContextEvent;
			text = clientContextEventValue.ToString();
		}
		bool flag = m_clientContextEvent != val;
		bool flag2 = m_clientContextEventValue != text;
		if (flag)
		{
			m_clientContextEvent = val;
			((ModelItem)this).FirePropertyChanged("ClientContextEvent");
		}
		if (flag || flag2)
		{
			m_clientContextEventValue = text;
			((ModelItem)this).FirePropertyChanged("ClientContextEventValue");
		}
	}
}
