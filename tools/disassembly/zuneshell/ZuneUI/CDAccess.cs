using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.Iris;
using Microsoft.Zune.Shell;
using Microsoft.Zune.Util;
using MicrosoftZuneLibrary;
using UIXControls;

namespace ZuneUI;

public class CDAccess : ModelItem
{
	private class AutoplayInfo
	{
		public char DriveLetter;

		public CDAction Action;

		public AutoplayInfo(char driveLetter, CDAction action)
		{
			DriveLetter = driveLetter;
			Action = action;
		}
	}

	private class ChangeInfo
	{
		public ZuneLibraryCDDevice Device;

		public int Index;

		public char DriveLetter;

		public bool MediaArrived;

		public ChangeInfo(char driveLetter, bool mediaArrived)
		{
			DriveLetter = driveLetter;
			MediaArrived = mediaArrived;
		}
	}

	private static string _ripCompleteMessage = Shell.LoadString(StringId.IDS_RIP_COMPLETE_NOTIFICATION);

	private static string _ripCanceledMessage = Shell.LoadString(StringId.IDS_RIP_CANCELED_NOTIFICATION);

	private static string _ripFailedMessage = Shell.LoadString(StringId.IDS_RIP_FAILED_NOTIFICATION);

	private static string _ripProgressMessage = Shell.LoadString(StringId.IDS_RIP_PROGRESS_NOTIFICATION);

	private static string _ripCurrentMessage = Shell.LoadString(StringId.IDS_RIP_CURRENT_NOTIFICATION);

	private static CDAccess _singletonInstance;

	private static bool s_phase2Complete = false;

	private ZuneLibraryCDDeviceList _cdDeviceList;

	private ArrayListDataSet _CDs;

	private IList _discardedCDs;

	private AutoplayInfo _pendingAutoPlay;

	private ZuneLibraryCDRecorder _recorder;

	private bool _isRipping;

	private ProgressNotification _ripNotification;

	private int _ripTotalTracks;

	private int _ripPercent;

	private int _ripCurrentTrack = 1;

	private int _ripTrackProgress;

	private char _activeDriveLetter;

	private bool _isBurning;

	private bool _isErasing;

	private bool _hasBurner;

	private BurnableCD _activeBurnCD;

	private BurnableCD _burnCDForNextSession;

	private int _burnListId = int.MinValue;

	private bool _isBurnListEmpty = true;

	private bool _hasBurnedBurnList;

	private DRMCanDoQuery _drmQuery;

	private ProgressNotification _burnNotification;

	public static CDAccess Instance
	{
		get
		{
			if (_singletonInstance == null)
			{
				_singletonInstance = new CDAccess();
			}
			return _singletonInstance;
		}
	}

	public ArrayListDataSet CDs => _CDs;

	public BurnableCD BurnCDForNextSession
	{
		get
		{
			return _burnCDForNextSession;
		}
		private set
		{
			if (_burnCDForNextSession != value)
			{
				_burnCDForNextSession = value;
				EnsureBurnList(forceCreate: false);
				((ModelItem)this).FirePropertyChanged("BurnCDForNextSession");
			}
		}
	}

	internal ZuneLibraryCDRecorder Recorder => _recorder;

	public bool IsRipping
	{
		get
		{
			return _isRipping;
		}
		set
		{
			if (_isRipping != value)
			{
				_isRipping = value;
				((ModelItem)this).FirePropertyChanged("IsRipping");
			}
		}
	}

	public bool IsAudioBurn => ((NamedIntOption)ZuneShell.DefaultInstance.Management.BurnFormat.ChosenValue).Value == 0;

	public bool IsBurning
	{
		get
		{
			return _isBurning;
		}
		internal set
		{
			if (_isBurning != value)
			{
				_isBurning = value;
				((ModelItem)this).FirePropertyChanged("IsBurning");
			}
		}
	}

	public bool IsErasing
	{
		get
		{
			return _isErasing;
		}
		internal set
		{
			if (_isErasing != value)
			{
				_isErasing = value;
				((ModelItem)this).FirePropertyChanged("IsErasing");
			}
		}
	}

	public char ActiveDriveLetter
	{
		get
		{
			return _activeDriveLetter;
		}
		set
		{
			_activeDriveLetter = value;
		}
	}

	public bool IsBurnCanceling
	{
		get
		{
			if (_activeBurnCD != null)
			{
				return _activeBurnCD.IsBurnCanceling;
			}
			return false;
		}
	}

	public bool HasBurner
	{
		get
		{
			return _hasBurner;
		}
		private set
		{
			if (_hasBurner != value)
			{
				_hasBurner = value;
				((ModelItem)this).FirePropertyChanged("HasBurner");
			}
		}
	}

	public TimeSpan DefaultBurnTimeAvailable => TimeSpan.FromMinutes(0.0);

	public long DefaultBurnSpaceAvailable => 0L;

	public ProgressNotification BurnNotification
	{
		get
		{
			return _burnNotification;
		}
		internal set
		{
			if (_burnNotification != value)
			{
				_burnNotification = value;
				((ModelItem)this).FirePropertyChanged("BurnNotification");
			}
		}
	}

	public int BurnListId => _burnListId;

	public bool IsBurnListEmpty => _isBurnListEmpty;

	public bool HasLoadedMedia
	{
		get
		{
			foreach (CDAlbumCommand item in (ListDataSet)_CDs)
			{
				if (item != null && item.IsMediaLoaded && (item.TOC != null || item.CanWrite))
				{
					return true;
				}
			}
			return false;
		}
	}

	public IList DiscardedCDs
	{
		get
		{
			return _discardedCDs;
		}
		set
		{
			_discardedCDs = value;
		}
	}

	public ProgressNotification Notification
	{
		get
		{
			return _ripNotification;
		}
		set
		{
			if (_ripNotification != value)
			{
				_ripNotification = value;
				((ModelItem)this).FirePropertyChanged("Notification");
			}
		}
	}

	public event EventHandler MediaChanged;

	private CDAccess()
	{
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_002a: Expected O, but got Unknown
		_CDs = new ArrayListDataSet();
		if (s_phase2Complete)
		{
			Initialize();
		}
	}

	public static void Phase2Catchup()
	{
		s_phase2Complete = true;
		if (Instance != null)
		{
			Instance.Initialize();
		}
	}

	private void Initialize()
	{
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		//IL_003a: Expected O, but got Unknown
		//IL_0088: Unknown result type (might be due to invalid IL or missing references)
		//IL_0092: Expected O, but got Unknown
		//IL_009f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a9: Expected O, but got Unknown
		_cdDeviceList = ZuneApplication.ZuneLibrary.GetCDDeviceList();
		DiscExperience disc = Shell.MainFrame.Disc;
		if (_cdDeviceList != null)
		{
			_cdDeviceList.MediaChangedHandler += new OnMediaChangedHandler(OnMediaChanged);
			for (int i = 0; i < _cdDeviceList.Count; i++)
			{
				((ListDataSet)_CDs).Add((object)null);
			}
		}
		disc.RecalculateAvailableNodes();
		_recorder = ZuneApplication.ZuneLibrary.GetRecorder();
		if (_recorder != null)
		{
			_recorder.RecordStopHandler += new OnRecordStopHandler(OnRecordStop);
			_recorder.RecordProgressHandler += new OnRecordProgressHandler(OnRecordProgress);
		}
	}

	protected override void OnDispose(bool fDisposing)
	{
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_002c: Expected O, but got Unknown
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		//IL_005e: Expected O, but got Unknown
		//IL_006b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0075: Expected O, but got Unknown
		((ModelItem)this).OnDispose(fDisposing);
		if (fDisposing)
		{
			if (_cdDeviceList != null)
			{
				_cdDeviceList.MediaChangedHandler -= new OnMediaChangedHandler(OnMediaChanged);
				_cdDeviceList.Release();
				_cdDeviceList = null;
			}
			if (_recorder != null)
			{
				_recorder.RecordStopHandler -= new OnRecordStopHandler(OnRecordStop);
				_recorder.RecordProgressHandler -= new OnRecordProgressHandler(OnRecordProgress);
				_recorder = null;
			}
			foreach (CDAlbumCommand item in (ListDataSet)_CDs)
			{
				if (item != null)
				{
					((ModelItem)item).Dispose();
				}
			}
			((ListDataSet)_CDs).Clear();
		}
		if (_drmQuery != null)
		{
			_drmQuery.Dispose();
		}
	}

	public static void HandleDiskFromAutoplay(string path, CDAction action)
	{
		int num = path.IndexOf(':');
		if (num > 0)
		{
			char c = char.ToUpperInvariant(path[num - 1]);
			if (c >= 'A' && c <= 'Z')
			{
				HandleDiskFromAutoplay(c, action);
			}
		}
	}

	public static void HandleDiskFromAutoplay(char driveLetter, CDAction action)
	{
		CDAlbumCommand cDAlbumCommand = null;
		foreach (CDAlbumCommand item in (ListDataSet)Instance.CDs)
		{
			if (item != null && item.CDDevice.DrivePath == driveLetter)
			{
				cDAlbumCommand = item;
				break;
			}
		}
		if (cDAlbumCommand == null)
		{
			Instance._pendingAutoPlay = new AutoplayInfo(driveLetter, action);
			return;
		}
		((Command)cDAlbumCommand).Invoke();
		if ((action != CDAction.Rip || !cDAlbumCommand.InsertedDuringSession || !ZuneShell.DefaultInstance.Management.AutoCopyCD.Value) && cDAlbumCommand.CDDevice.IsDriveReady)
		{
			cDAlbumCommand.AutoPlayAction = action;
		}
	}

	internal void UpdateIsAudioBurn()
	{
		((ModelItem)this).FirePropertyChanged("IsAudioBurn");
	}

	public void AddToBurnList(IList items)
	{
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_0049: Expected O, but got Unknown
		//IL_006d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0073: Expected O, but got Unknown
		if (_burnListId >= 0 && _hasBurnedBurnList && !_isBurnListEmpty)
		{
			Command val = new Command((IModelItemOwner)null, Shell.LoadString(StringId.IDS_CREATE_BURN_LIST_YES_CREATE), (EventHandler)null);
			val.Invoked += delegate
			{
				AddToBurnPlaylist(items, createNew: true);
			};
			Command val2 = new Command((IModelItemOwner)null, Shell.LoadString(StringId.IDS_CREATE_BURN_LIST_NO_ADD), (EventHandler)null);
			val2.Invoked += delegate
			{
				AddToBurnPlaylist(items, createNew: false);
			};
			MessageBox.Show(Shell.LoadString(StringId.IDS_CREATE_BURN_LIST_DIALOG_TITLE), Shell.LoadString(StringId.IDS_CREATE_BURN_LIST_QUESTION), val, val2, (BooleanChoice)null);
		}
		else
		{
			AddToBurnPlaylist(items, createNew: false);
		}
	}

	private void AddToBurnPlaylist(IList items, bool createNew)
	{
		EnsureBurnList(createNew);
		PlaylistManager.Instance.AddToPlaylist(_burnListId, items, rememberAsDefault: false);
		_hasBurnedBurnList = false;
		_isBurnListEmpty = false;
		Shell.MainFrame.Disc.RecalculateAvailableNodes();
		Shell.MainFrame.Disc.Nodes.ChosenValue = Shell.MainFrame.Disc.BurnList;
	}

	private void EnsureBurnList(bool forceCreate)
	{
		if (_burnListId < 0 || forceCreate)
		{
			string title = Shell.LoadString(StringId.IDS_PLAYLIST_BURN_LIST);
			PlaylistResult playlistResult = PlaylistManager.Instance.CreatePlaylist(title, privatePlaylist: true);
			_burnListId = playlistResult.PlaylistId;
			_isBurnListEmpty = true;
			Shell.MainFrame.Disc.RecalculateAvailableNodes();
			((ModelItem)this).FirePropertyChanged("BurnListId");
		}
	}

	public void ClearBurnList()
	{
		EnsureBurnList(forceCreate: true);
	}

	public bool DRMCanBurnFile(string filePath)
	{
		//IL_0009: Unknown result type (might be due to invalid IL or missing references)
		//IL_0013: Expected O, but got Unknown
		if (_drmQuery == null)
		{
			try
			{
				_drmQuery = new DRMCanDoQuery();
			}
			catch (ApplicationException)
			{
			}
		}
		if (_drmQuery != null)
		{
			return _drmQuery.CanBurnFile(filePath);
		}
		return true;
	}

	public void PrepareForBurn(string burnTitle, int playlistId, IList burnListItems)
	{
		if (_burnCDForNextSession != null)
		{
			IsBurning = true;
			_activeBurnCD = _burnCDForNextSession;
			_activeDriveLetter = _activeBurnCD.DriveLetter;
			_hasBurnedBurnList = true;
			_activeBurnCD.PrepareForBurn(burnTitle, playlistId, burnListItems);
		}
	}

	public void StartBurn()
	{
		_activeBurnCD.StartBurn();
	}

	public void CancelBurn()
	{
		if (_activeBurnCD != null)
		{
			_activeBurnCD.CancelBurn();
		}
	}

	public BurnSessionItem GetBurnItemByPlaylistContentId(int playlistContentId)
	{
		if (_activeBurnCD != null)
		{
			return _activeBurnCD.GetBurnItemByPlaylistContentId(playlistContentId);
		}
		return null;
	}

	public string TimeSpanToStringForBurnTime(TimeSpan time)
	{
		return string.Format("{1:0}{0}{2:00}", CultureInfo.CurrentCulture.DateTimeFormat.TimeSeparator, (int)time.TotalMinutes, time.Seconds);
	}

	private void OnMediaChanged(char driveLetter, bool fMediaArrived)
	{
		ChangeInfo changeInfo = new ChangeInfo(driveLetter, fMediaArrived);
		for (int i = 0; i < _cdDeviceList.Count; i++)
		{
			ZuneLibraryCDDevice item = _cdDeviceList.GetItem(i);
			if (item.DrivePath == changeInfo.DriveLetter)
			{
				changeInfo.Index = i;
				changeInfo.Device = item;
				break;
			}
		}
		WaitForDrive(changeInfo);
	}

	private void WaitForDrive(object arg)
	{
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Expected O, but got Unknown
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Expected O, but got Unknown
		ChangeInfo changeInfo = (ChangeInfo)arg;
		if (changeInfo.Device == null || !changeInfo.MediaArrived || changeInfo.Device.IsDriveReady)
		{
			Application.DeferredInvoke(new DeferredInvokeHandler(DeferredOnMediaChanged), (object)changeInfo);
		}
		else
		{
			Application.DeferredInvoke(new DeferredInvokeHandler(WaitForDrive), (object)changeInfo, TimeSpan.FromMilliseconds(500.0));
		}
	}

	private void DeferredOnMediaChanged(object arg)
	{
		ChangeInfo changeInfo = (ChangeInfo)arg;
		ZuneLibraryCDDevice device = changeInfo.Device;
		int index = changeInfo.Index;
		bool flag = _activeDriveLetter == changeInfo.DriveLetter && !changeInfo.MediaArrived;
		if (device != null)
		{
			CDAlbumCommand cDAlbumCommand = (CDAlbumCommand)((ListDataSet)_CDs)[index];
			if (!ZuneLibraryCDDevice.IsImapiv2Installed && flag && (cDAlbumCommand.BurnCD.IsBurning || cDAlbumCommand.BurnCD.IsErasing))
			{
				device.Dispose();
				return;
			}
			if (cDAlbumCommand == null || !changeInfo.MediaArrived || device.TOC != cDAlbumCommand.TOC)
			{
				CDAlbumCommand cDAlbumCommand2 = null;
				if (changeInfo.MediaArrived)
				{
					cDAlbumCommand2 = new CDAlbumCommand(Shell.MainFrame.Disc, this, device, insertedDuringSession: true);
				}
				((ListDataSet)_CDs)[index] = cDAlbumCommand2;
				if (cDAlbumCommand != null)
				{
					if (_discardedCDs == null)
					{
						_discardedCDs = new List<object>();
					}
					_discardedCDs.Add(cDAlbumCommand);
				}
				if (cDAlbumCommand2 == null)
				{
					device.Dispose();
				}
				else
				{
					device.Close();
				}
			}
			else
			{
				device.Dispose();
			}
		}
		if (_pendingAutoPlay != null && _pendingAutoPlay.DriveLetter == changeInfo.DriveLetter)
		{
			HandleDiskFromAutoplay(_pendingAutoPlay.DriveLetter, _pendingAutoPlay.Action);
			_pendingAutoPlay = null;
		}
		if (flag && _activeBurnCD != null)
		{
			if (_activeBurnCD.IsBurning)
			{
				_activeBurnCD.CancelBurn();
			}
			else
			{
				_activeBurnCD.ClearBurnItems();
			}
			_activeDriveLetter = '\0';
		}
		UpdateBurnState();
		Shell.MainFrame.Disc.UpdateNodes(this, changeInfo.DriveLetter, changeInfo.MediaArrived);
		NotifyMediaChanged();
	}

	private void UpdateBurnState()
	{
		BurnableCD burnCDForNextSession = null;
		foreach (CDAlbumCommand item2 in (ListDataSet)_CDs)
		{
			if (item2 != null && item2.CanWrite)
			{
				burnCDForNextSession = item2.BurnCD;
				break;
			}
		}
		BurnCDForNextSession = burnCDForNextSession;
		bool flag = false;
		for (int i = 0; i < _cdDeviceList.Count; i++)
		{
			ZuneLibraryCDDevice item = _cdDeviceList.GetItem(i);
			flag |= item.IsBurner;
			item.Dispose();
		}
		HasBurner = flag;
	}

	private void NotifyMediaChanged()
	{
		((ModelItem)this).FirePropertyChanged("MediaChanged");
		if (this.MediaChanged != null)
		{
			this.MediaChanged(this, new EventArgs());
		}
	}

	internal void StartRip(int trackCount)
	{
		_ripTotalTracks += trackCount;
		IsRipping = true;
		UpdateMessage(trackProgress: false);
	}

	internal void StopRip(int trackCount)
	{
		_ripTotalTracks -= trackCount;
		if (_ripCurrentTrack > _ripTotalTracks)
		{
			ShowCompletedMessage(RipState.Incomplete);
		}
		else
		{
			UpdateMessage(trackProgress: false);
		}
	}

	private void OnRecordProgress(string strSourceUrl, int percentComplete)
	{
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Expected O, but got Unknown
		Application.DeferredInvoke((DeferredInvokeHandler)delegate
		{
			_ripTrackProgress = percentComplete;
			UpdateMessage(trackProgress: true);
			CDAlbumTrack trackBySourceUrl = GetTrackBySourceUrl(strSourceUrl);
			if (trackBySourceUrl != null)
			{
				trackBySourceUrl.RipState = RipState.InProgress;
				trackBySourceUrl.PercentComplete = percentComplete;
			}
		}, (object)null);
	}

	private void OnRecordStop(string strSourceUrl, int hr)
	{
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Expected O, but got Unknown
		Application.DeferredInvoke((DeferredInvokeHandler)delegate
		{
			//IL_0055: Unknown result type (might be due to invalid IL or missing references)
			//IL_005a: Unknown result type (might be due to invalid IL or missing references)
			_ripCurrentTrack++;
			_ripTrackProgress = 0;
			bool flag = _ripCurrentTrack > _ripTotalTracks;
			CDAlbumTrack trackBySourceUrl = GetTrackBySourceUrl(strSourceUrl);
			RipState ripState = RipState.InLibrary;
			if (trackBySourceUrl != null)
			{
				if (HRESULT.op_Implicit(hr) == HRESULT._E_ABORT)
				{
					ripState = RipState.Incomplete;
				}
				else if (hr < 0)
				{
					ripState = RipState.Error;
					trackBySourceUrl.RipErrorCode = hr;
				}
				else
				{
					ripState = RipState.InLibrary;
					trackBySourceUrl.RipTrack = false;
					if (flag && ZuneShell.DefaultInstance.Management.AutoEjectCD.Value)
					{
						trackBySourceUrl.Album.CDDevice.Eject();
					}
				}
				trackBySourceUrl.RipState = ripState;
				if (flag)
				{
					trackBySourceUrl.Album.IsRipping = false;
				}
			}
			if (flag)
			{
				_ripTotalTracks = 0;
				_ripCurrentTrack = 1;
				_ripPercent = 0;
				_ripTrackProgress = 0;
				ShowCompletedMessage(ripState);
			}
			else
			{
				UpdateMessage(trackProgress: false);
			}
		}, (object)null);
	}

	private void ShowCompletedMessage(RipState ripState)
	{
		IsRipping = false;
		if (_ripNotification != null && _ripNotification.Type != NotificationState.Completed)
		{
			_ripNotification.Type = NotificationState.Completed;
			switch (ripState)
			{
			case RipState.Incomplete:
				_ripNotification.Message = _ripCanceledMessage;
				break;
			case RipState.Error:
				_ripNotification.Message = _ripFailedMessage;
				break;
			default:
				_ripNotification.Message = _ripCompleteMessage;
				_ripNotification.Percentage = 100;
				break;
			}
			_ripNotification.SubMessage = null;
			Notification = null;
		}
		SoundHelper.Play(SoundId.RipComplete);
	}

	private void UpdateMessage(bool trackProgress)
	{
		if (_ripNotification == null)
		{
			Notification = new ProgressNotification(_ripProgressMessage, NotificationTask.Rip, NotificationState.Normal, 0);
		}
		else
		{
			_ripNotification.Type = NotificationState.Normal;
			_ripNotification.Message = _ripProgressMessage;
		}
		int num = 0;
		if (_ripTotalTracks != 0)
		{
			num = ((_ripCurrentTrack - 1) * 100 + _ripTrackProgress) / _ripTotalTracks;
		}
		if (!trackProgress || _ripPercent != num)
		{
			_ripPercent = num;
			_ripNotification.Percentage = num;
			_ripNotification.SubMessage = string.Format(_ripCurrentMessage, _ripCurrentTrack, _ripTotalTracks);
		}
	}

	private CDAlbumTrack GetTrackBySourceUrl(string strSourceUrl)
	{
		int num = strSourceUrl.LastIndexOf('/');
		int num2 = strSourceUrl.LastIndexOf('/', num - 1);
		int num3 = int.Parse(strSourceUrl.Substring(num + 1));
		int num4 = int.Parse(strSourceUrl.Substring(num2 + 1, num - num2 - 1));
		ZuneLibraryCDDevice item = _cdDeviceList.GetItem(num4);
		for (int i = 0; i < ((ListDataSet)CDs).Count; i++)
		{
			CDAlbumCommand cDAlbumCommand = (CDAlbumCommand)((ListDataSet)CDs)[i];
			if (cDAlbumCommand != null && cDAlbumCommand.CDDevice.DrivePath == item.DrivePath)
			{
				if (cDAlbumCommand.CDDevice.IsMediaLoaded)
				{
					return cDAlbumCommand.GetTrack(num3 - 1);
				}
				return null;
			}
		}
		return null;
	}
}
