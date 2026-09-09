using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Iris;
using Microsoft.Zune.Service;
using MicrosoftZuneLibrary;

namespace ZuneUI;

public class BurnableCD : ModelItem
{
	private CDAccess _cdAccess;

	private ZuneLibraryCDDevice _device;

	private bool _isBurning;

	private bool _hasDeviceStartedBurn;

	private bool _isErasing;

	private bool _burnCanceling;

	private List<BurnSessionItem> _items;

	private int _playlistId;

	private string _burnTitle;

	private bool _downloadsComplete;

	private HRESULT _burnError;

	private ProgressNotification _burnNotification;

	private int _sessionTotalTime;

	private int _sessionTimeRemaining;

	private int _sessionPercentComplete;

	private int _sessionCurrentTrackNumber;

	private bool _sessionFinalizing;

	private Timer _progressTimer;

	private int _simulatedTickInterval;

	private string _burnProgressMessage;

	private static string s_burnStartedMessage = Shell.LoadString(StringId.IDS_BURN_STARTED_NOTIFICATION);

	private static string s_burnPreparingMessage = Shell.LoadString(StringId.IDS_BURN_PREPARING);

	private static string s_burnProgressMessage = Shell.LoadString(StringId.IDS_BURN_PROGRESS_NOTIFICATION);

	private static string s_burnProgressDataMessage = Shell.LoadString(StringId.IDS_BURN_PROGRESS_NOTIFICATION_DATA);

	private static string s_burnFinalizingMessage = Shell.LoadString(StringId.IDS_BURN_FINALIZING);

	private static string s_burnSucceededMessage = Shell.LoadString(StringId.IDS_BURN_SUCCEEDED_NOTIFICATION);

	private static string s_burnFailedMessage = Shell.LoadString(StringId.IDS_BURN_FAILED_NOTIFICATION);

	private static string s_burnCanceledMessage = Shell.LoadString(StringId.IDS_BURN_CANCELED_NOTIFICATION);

	private static string s_burnCancelingMessage = Shell.LoadString(StringId.IDS_BURN_CANCELING_NOTIFICATION);

	private static string s_eraseStartedMessage = Shell.LoadString(StringId.IDS_ERASE_STARTED_NOTIFICATION);

	private static string s_eraseFinishedMessage = Shell.LoadString(StringId.IDS_ERASE_COMPLETED_NOTIFICATION);

	internal bool IsWriteable => _device.IsWriteable;

	public bool CanBurnAudio => !_device.IsDVD;

	public ZuneLibraryCDDevice Device => _device;

	public char DriveLetter => _device.DrivePath;

	public TimeSpan TimeAvailable => TimeSpan.FromSeconds((double)_device.TimeAvailable);

	public long SpaceAvailable => _device.SpaceAvailable;

	public bool IsBurning => _isBurning;

	public bool IsErasing => _isErasing;

	public bool IsBurnCanceling => _burnCanceling;

	private ProgressNotification Notification
	{
		get
		{
			return _burnNotification;
		}
		set
		{
			_burnNotification = value;
			_cdAccess.BurnNotification = _burnNotification;
		}
	}

	internal BurnableCD(CDAccess cdAccess, ZuneLibraryCDDevice device)
	{
		_cdAccess = cdAccess;
		_device = device;
	}

	protected override void OnDispose(bool fDisposing)
	{
		((ModelItem)this).OnDispose(fDisposing);
		if (fDisposing)
		{
			_device = null;
			_cdAccess = null;
		}
	}

	public void PrepareForBurn(string burnTitle, int playlistId, IList burnListItems)
	{
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0063: Unknown result type (might be due to invalid IL or missing references)
		//IL_0069: Expected O, but got Unknown
		_isBurning = true;
		_burnError = default(HRESULT);
		_items = new List<BurnSessionItem>(burnListItems.Count);
		_playlistId = playlistId;
		_burnTitle = burnTitle;
		_downloadsComplete = false;
		_burnProgressMessage = (_cdAccess.IsAudioBurn ? s_burnProgressMessage : s_burnProgressDataMessage);
		for (int i = 0; i < burnListItems.Count; i++)
		{
			DataProviderObject val = (DataProviderObject)burnListItems[i];
			int playlistContentId = (int)val.GetProperty("LibraryId");
			Guid zuneMediaId = (Guid)val.GetProperty("ZuneMediaId");
			BurnSessionItem item = new BurnSessionItem(this, i, playlistContentId, zuneMediaId);
			_items.Add(item);
		}
		BeginSession();
		UpdateMessage();
	}

	public void ClearBurnItems()
	{
		_items = null;
	}

	public void StartBurn()
	{
		if (_isBurning)
		{
			WaitForDownloads();
		}
	}

	private void WaitForDownloads()
	{
		//IL_0074: Unknown result type (might be due to invalid IL or missing references)
		//IL_007e: Expected O, but got Unknown
		//IL_008a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0094: Expected O, but got Unknown
		bool flag = false;
		if (_cdAccess.IsAudioBurn)
		{
			foreach (BurnSessionItem item in _items)
			{
				bool fPending = false;
				if (Download.Instance.IsDownloading(item.ZuneMediaId, (EContentType)0, out fPending))
				{
					item.Downloading = true;
					flag = true;
				}
			}
		}
		if (!flag)
		{
			StartBurnForReal();
			return;
		}
		Download.Instance.DownloadEvent += new DownloadEventHandler(OnDownloadEvent);
		Download.Instance.DownloadProgressEvent += new DownloadEventProgressHandler(OnDownloadProgress);
		_downloadsComplete = false;
	}

	private void OnDownloadEvent(Guid zuneMediaId, HRESULT hr)
	{
		//IL_0009: Unknown result type (might be due to invalid IL or missing references)
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		if (_downloadsComplete || hr == HRESULT._E_PENDING || hr == HRESULT._E_ALREADY_EXISTS)
		{
			return;
		}
		bool flag = true;
		int num = 0;
		foreach (BurnSessionItem item in _items)
		{
			if (item.ZuneMediaId == zuneMediaId)
			{
				item.Downloading = false;
				item.ErrorCode = ((HRESULT)(ref hr)).Int;
			}
			else if (item.Downloading)
			{
				flag = false;
			}
			if (num == 0 && item.ErrorCode != 0)
			{
				num = item.ErrorCode;
			}
		}
		if (flag)
		{
			_downloadsComplete = true;
			if (num != 0)
			{
				OnSessionError(num);
				NotifyBurnStopped();
			}
			else
			{
				StartBurnForReal();
			}
		}
	}

	private void OnDownloadProgress(Guid zuneMediaId, float percent)
	{
		foreach (BurnSessionItem item in _items)
		{
			if (item.ZuneMediaId == zuneMediaId)
			{
				item.DownloadProgress = (int)percent;
				break;
			}
		}
	}

	private void StartBurnForReal()
	{
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		//IL_0059: Unknown result type (might be due to invalid IL or missing references)
		//IL_0093: Unknown result type (might be due to invalid IL or missing references)
		//IL_0099: Expected O, but got Unknown
		//IL_0072: Unknown result type (might be due to invalid IL or missing references)
		//IL_0077: Unknown result type (might be due to invalid IL or missing references)
		DeferredInvokeHandler val = null;
		_sessionCurrentTrackNumber = 0;
		HRESULT hr = _device.SetBurnPlaylist(_playlistId);
		if (((HRESULT)(ref hr)).IsSuccess)
		{
			if (!string.IsNullOrEmpty(_burnTitle))
			{
				hr = _device.SetVolumeLabelW(_burnTitle);
			}
			if (((HRESULT)(ref hr)).IsSuccess)
			{
				hr = _device.StartBurn();
			}
		}
		if (((HRESULT)(ref hr)).IsError)
		{
			if (val == null)
			{
				val = (DeferredInvokeHandler)delegate
				{
					OnSessionError(((HRESULT)(ref hr)).Int);
					NotifyBurnStopped();
				};
			}
			Application.DeferredInvoke(val, (object)null);
		}
		else
		{
			_hasDeviceStartedBurn = true;
			UpdateMessage();
		}
	}

	internal void CancelBurn()
	{
		if (_isBurning && !_burnCanceling)
		{
			_burnCanceling = true;
			if (_hasDeviceStartedBurn)
			{
				_device.StopBurn();
			}
			if (_progressTimer != null)
			{
				_progressTimer.Stop();
			}
			UpdateMessage();
			if (!_hasDeviceStartedBurn)
			{
				NotifyBurnStopped();
			}
			_hasDeviceStartedBurn = false;
		}
	}

	private void BeginSession()
	{
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0017: Expected O, but got Unknown
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		//IL_002e: Expected O, but got Unknown
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0045: Expected O, but got Unknown
		//IL_0052: Unknown result type (might be due to invalid IL or missing references)
		//IL_005c: Expected O, but got Unknown
		_device.ItemProgressHandler += new OnItemProgressHandler(OnItemProgress);
		_device.ItemErrorHandler += new OnItemErrorHandler(OnItemError);
		_device.SessionProgressHandler += new OnSessionProgressHandler(OnSessionProgress);
		_device.BurnStateChangeHandler += new OnBurnStateChangeHandler(OnBurnStateChanged);
	}

	private void NotifyBurnStopped()
	{
		EndSession();
		if (_burnCanceling && _items != null)
		{
			foreach (BurnSessionItem item in _items)
			{
				item.BurnCanceled = true;
			}
			_items = null;
		}
		_isBurning = false;
		_burnCanceling = false;
		_sessionFinalizing = false;
		_playlistId = PlaylistManager.InvalidPlaylistId;
		_burnTitle = null;
		_cdAccess.IsBurning = false;
	}

	private void EndSession()
	{
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0017: Expected O, but got Unknown
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		//IL_002e: Expected O, but got Unknown
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0045: Expected O, but got Unknown
		//IL_0052: Unknown result type (might be due to invalid IL or missing references)
		//IL_005c: Expected O, but got Unknown
		//IL_0068: Unknown result type (might be due to invalid IL or missing references)
		//IL_0072: Expected O, but got Unknown
		//IL_007e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0088: Expected O, but got Unknown
		_device.ItemProgressHandler -= new OnItemProgressHandler(OnItemProgress);
		_device.ItemErrorHandler -= new OnItemErrorHandler(OnItemError);
		_device.SessionProgressHandler -= new OnSessionProgressHandler(OnSessionProgress);
		_device.BurnStateChangeHandler -= new OnBurnStateChangeHandler(OnBurnStateChanged);
		Download.Instance.DownloadEvent -= new DownloadEventHandler(OnDownloadEvent);
		Download.Instance.DownloadProgressEvent -= new DownloadEventProgressHandler(OnDownloadProgress);
		_downloadsComplete = false;
		_sessionTotalTime = 0;
		_sessionTimeRemaining = 0;
		_sessionPercentComplete = 0;
		if (_progressTimer != null)
		{
			((ModelItem)_progressTimer).Dispose();
			_progressTimer = null;
		}
		ShowCompletedMessage();
	}

	public void Erase()
	{
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		if (!_isErasing)
		{
			_isErasing = true;
			_cdAccess.ActiveDriveLetter = DriveLetter;
			_cdAccess.IsErasing = true;
			BeginSession();
			HRESULT val = _device.EraseDisc();
			if (((HRESULT)(ref val)).IsError)
			{
				CompleteErase();
			}
		}
	}

	private void CompleteErase()
	{
		EndSession();
		_isErasing = false;
		_cdAccess.IsErasing = false;
	}

	public BurnSessionItem GetBurnItemByPlaylistContentId(int playlistContentId)
	{
		if (_items != null)
		{
			foreach (BurnSessionItem item in _items)
			{
				if (item.PlaylistContentId == playlistContentId)
				{
					return item;
				}
			}
		}
		return null;
	}

	private void OnItemProgress(int index, EBurnProgressStatus eStatus, int nPercent)
	{
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Expected O, but got Unknown
		Application.DeferredInvoke((DeferredInvokeHandler)delegate
		{
			//IL_00b2: Unknown result type (might be due to invalid IL or missing references)
			//IL_00b8: Invalid comparison between Unknown and I4
			//IL_002a: Unknown result type (might be due to invalid IL or missing references)
			//IL_0030: Invalid comparison between Unknown and I4
			//IL_0033: Unknown result type (might be due to invalid IL or missing references)
			//IL_0039: Invalid comparison between Unknown and I4
			//IL_005a: Unknown result type (might be due to invalid IL or missing references)
			//IL_0060: Invalid comparison between Unknown and I4
			//IL_0063: Unknown result type (might be due to invalid IL or missing references)
			//IL_0069: Invalid comparison between Unknown and I4
			bool flag = false;
			if (0 <= index && index < _items.Count)
			{
				if ((int)eStatus == 8 || (int)eStatus == 7)
				{
					_items[index].BurnComplete = true;
				}
				else if ((int)eStatus == 2 || (int)eStatus == 5)
				{
					_items[index].BurnProgress = nPercent;
					int num = index + 1;
					if (_sessionCurrentTrackNumber != num)
					{
						_sessionCurrentTrackNumber = num;
						flag = true;
					}
				}
			}
			if ((int)eStatus == 7)
			{
				_sessionFinalizing = true;
				flag = true;
			}
			if (flag)
			{
				UpdateMessage();
			}
		}, (object)null);
	}

	private void OnItemError(int index, int hrError)
	{
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Expected O, but got Unknown
		Application.DeferredInvoke((DeferredInvokeHandler)delegate
		{
			if (index == -1)
			{
				OnSessionError(hrError);
			}
			else if (0 <= index && index < _items.Count)
			{
				_items[index].ErrorCode = hrError;
			}
		}, (object)null);
	}

	private void OnSessionError(int hrError)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		_burnError = new HRESULT(hrError);
		bool flag = false;
		foreach (BurnSessionItem item in _items)
		{
			if (item.ErrorCode != 0)
			{
				flag = true;
				break;
			}
		}
		foreach (BurnSessionItem item2 in _items)
		{
			if (!flag)
			{
				item2.ErrorCode = hrError;
			}
			else if (item2.ErrorCode == 0)
			{
				item2.BurnCanceled = true;
			}
		}
		Shell.ShowErrorDialog(hrError, StringId.IDS_BURN_FAILED);
	}

	private void OnSessionProgress(int lSessionTimeRemaining, int lSessionTotalTime)
	{
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Expected O, but got Unknown
		Application.DeferredInvoke((DeferredInvokeHandler)delegate
		{
			//IL_0088: Unknown result type (might be due to invalid IL or missing references)
			//IL_0092: Expected O, but got Unknown
			_sessionTotalTime = lSessionTotalTime * 1000;
			_sessionTimeRemaining = lSessionTimeRemaining * 1000;
			UpdateMessage();
			int num = 100 - _sessionPercentComplete;
			if (num > 0)
			{
				_simulatedTickInterval = _sessionTimeRemaining / num;
				if (_simulatedTickInterval > 0)
				{
					if (_progressTimer == null)
					{
						_progressTimer = new Timer();
						_progressTimer.Enabled = true;
						_progressTimer.Tick += OnSimulatedSessionProgress;
					}
					_progressTimer.Interval = _simulatedTickInterval;
				}
			}
		}, (object)null);
	}

	private void OnSimulatedSessionProgress(object sender, EventArgs args)
	{
		_sessionTimeRemaining = Math.Max(_sessionTimeRemaining - _simulatedTickInterval, 0);
		UpdateMessage();
	}

	private void OnBurnStateChanged(EBurnState eBurnState)
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_0008: Unknown result type (might be due to invalid IL or missing references)
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Expected O, but got Unknown
		Application.DeferredInvoke((DeferredInvokeHandler)delegate
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			//IL_0007: Invalid comparison between Unknown and I4
			if ((int)eBurnState == 8)
			{
				if (_isBurning)
				{
					NotifyBurnStopped();
				}
				else if (_isErasing)
				{
					CompleteErase();
				}
			}
		}, (object)null);
	}

	private void UpdateMessage()
	{
		string message = (_isBurning ? s_burnStartedMessage : s_eraseStartedMessage);
		if (_burnNotification == null)
		{
			Notification = new ProgressNotification(message, NotificationTask.Burn, NotificationState.Normal, 0);
		}
		else
		{
			_burnNotification.Type = NotificationState.Normal;
			_burnNotification.Message = message;
		}
		int num = 0;
		if (_sessionTotalTime > 0)
		{
			num = (_sessionTotalTime - _sessionTimeRemaining) * 100 / _sessionTotalTime;
		}
		if (_sessionPercentComplete < num)
		{
			_sessionPercentComplete = num;
		}
		_burnNotification.Percentage = _sessionPercentComplete;
		if (_isBurning)
		{
			if (_burnCanceling)
			{
				_burnNotification.SubMessage = s_burnCancelingMessage;
			}
			else if (_sessionFinalizing)
			{
				_burnNotification.SubMessage = s_burnFinalizingMessage;
			}
			else if (_sessionCurrentTrackNumber == 0)
			{
				_burnNotification.SubMessage = s_burnPreparingMessage;
			}
			else
			{
				_burnNotification.SubMessage = string.Format(_burnProgressMessage, _sessionCurrentTrackNumber, _items.Count);
			}
		}
	}

	private void ShowCompletedMessage()
	{
		if (_burnNotification != null && _burnNotification.Type != NotificationState.Completed)
		{
			_burnNotification.Type = NotificationState.Completed;
			if (_isBurning)
			{
				if (_burnCanceling)
				{
					_burnNotification.Message = s_burnCanceledMessage;
				}
				else if (((HRESULT)(ref _burnError)).IsSuccess)
				{
					_burnNotification.Message = s_burnSucceededMessage;
					_burnNotification.Percentage = 100;
				}
				else
				{
					_burnNotification.Message = s_burnFailedMessage;
				}
			}
			else
			{
				_burnNotification.Message = s_eraseFinishedMessage;
			}
			_burnNotification.SubMessage = null;
			Notification = null;
		}
		SoundHelper.Play(SoundId.BurnComplete);
	}
}
