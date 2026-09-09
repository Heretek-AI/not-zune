using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading;
using Microsoft.Iris;
using Microsoft.Zune.Configuration;
using Microsoft.Zune.ErrorMapperApi;
using Microsoft.Zune.PerfTrace;
using Microsoft.Zune.QuickMix;
using Microsoft.Zune.Service;
using Microsoft.Zune.Util;
using MicrosoftZuneLibrary;
using MicrosoftZunePlayback;
using UIXControls;

namespace ZuneUI;

public class TransportControls : SingletonModelItem<TransportControls>
{
	private struct SpectrumOutputConfig
	{
		public uint SourceId;

		public uint NumBands;

		public bool Frequency;

		public bool Waveform;

		public bool Stereo;

		public bool IsConnected;
	}

	private enum PlayerState
	{
		Stopped,
		Playing,
		Paused
	}

	internal const long c_TicksPerSecond = 10000000L;

	private const long _rewindDelay = 50000000L;

	private const float c_overscanFactor = 0.1f;

	private const int c_maxConsecutiveErrors = 5;

	private const int c_ratingUnrated = -1;

	private const string c_knownInvalidUri = ".:* INVALID URI *:.";

	private PlayerInterop _playbackWrapper;

	private BooleanChoice _shuffling;

	private BooleanChoice _repeating;

	private BooleanChoice _muted;

	private BooleanChoice _showTotalTime;

	private BooleanChoice _showNowPlayingList;

	private BooleanChoice _fastforwarding;

	private BooleanChoice _rewinding;

	private RangedValue _volume;

	private Command _play;

	private Command _pause;

	private Command _back;

	private Command _forward;

	private Command _stop;

	private bool _playingVideo;

	private bool _opening;

	private bool _buffering;

	private bool _seekEnabled;

	private int _zoomScaleFactor;

	private Command _fastforwardhotkey;

	private Command _rewindhotkey;

	private NowPlayingList _playlistPending;

	private NowPlayingList _playlistCurrent;

	private Timer _timerDelayedConfigPersist;

	private VideoStream _videoStream;

	private MCPlayerState _lastKnownPlayerState;

	private MCTransportState _lastKnownTransportState;

	private long _lastKnownPosition;

	private Notification _nowPlayingNotification;

	private float _currentTrackDuration;

	private float _currentTrackPosition;

	private float _downloadProgress;

	private PlaybackTrack _lastKnownPreparedTrack;

	private PlaybackTrack _lastKnownPlaybackTrack;

	private List<PlaybackTrack> _tracksSubmittedToPlayer = new List<PlaybackTrack>(2);

	private Dictionary<PlaybackTrack, int> _errors = new Dictionary<PlaybackTrack, int>();

	private int _consecutiveErrors;

	private bool _showErrors = true;

	private int _lastKnownSetUriCallID;

	private static string _savedNowPlayingFilename = Path.Combine(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), Shell.LoadString(StringId.IDS_APPDATAFOLDERNAME).TrimStart(new char[1] { '\\' })), "NowPlaying.dat");

	private Shell _shellInstance;

	private PlayerState _playerState;

	private bool _streamingReportIsOpen;

	private Guid _streamingReportMediaId;

	private Guid _streamingReportMediaInstanceId;

	private TaskbarPlayer _taskbarPlayer;

	private bool _isInitialized;

	private bool _resumeLastNowPlayingRequested;

	private bool _shuffleAllRequested;

	private JumpListPin _requestedJumpListPin;

	private PlaybackContext _pagePlaybackContext;

	private PlaybackTrack _currentTrack;

	private bool _isPlaying;

	private bool _hasPlaylist;

	private bool _playlistSupportsShuffle = true;

	private bool _hasPlayed;

	private int _currentTrackIndex = -1;

	private ArrayListDataSet _currentPlaylist;

	private bool _isContextCompatible;

	private bool _isStreamingVideo;

	private bool _supressDownloads;

	private DateTime _currentPlayStartTime = DateTime.MinValue;

	private List<SpectrumOutputConfig> _spectrumConfigList;

	private bool _isSpectrumAvailable;

	private int _lastKnownCurrentTrackRating;

	private EventHandler _currentTrackRatingChangedEventHandler;

	private Timer _isStreamingTimeoutTimer;

	private bool _dontPlayMarketplaceTracks;

	private int _bandwidthCapacity;

	private BandwidthUpdateArgs _bandwidthUpdateInfo;

	public bool IsInitialized
	{
		get
		{
			return _isInitialized;
		}
		private set
		{
			if (_isInitialized != value)
			{
				_isInitialized = value;
				((ModelItem)this).FirePropertyChanged("IsInitialized");
			}
		}
	}

	public bool HasPlayed
	{
		get
		{
			return _hasPlayed;
		}
		private set
		{
			if (_hasPlayed != value)
			{
				_hasPlayed = value;
				((ModelItem)this).FirePropertyChanged("HasPlayed");
			}
		}
	}

	public BooleanChoice Shuffling => _shuffling;

	public BooleanChoice Repeating => _repeating;

	public BooleanChoice Muted => _muted;

	public BooleanChoice ShowTotalTime => _showTotalTime;

	public BooleanChoice ShowNowPlayingList => _showNowPlayingList;

	public BooleanChoice Fastforwarding => _fastforwarding;

	public BooleanChoice Rewinding => _rewinding;

	public RangedValue Volume => _volume;

	public Command Play => _play;

	public Command Pause => _pause;

	public Command Stop => _stop;

	public Command Back => _back;

	public Command Forward => _forward;

	public bool Opening
	{
		get
		{
			return _opening;
		}
		private set
		{
			if (_opening != value)
			{
				_opening = value;
				((ModelItem)this).FirePropertyChanged("Opening");
			}
		}
	}

	public bool Buffering
	{
		get
		{
			return _buffering;
		}
		private set
		{
			if (_buffering != value)
			{
				_buffering = value;
				((ModelItem)this).FirePropertyChanged("Buffering");
			}
		}
	}

	public bool IsSeekEnabled
	{
		get
		{
			return _seekEnabled;
		}
		private set
		{
			if (_seekEnabled != value)
			{
				_seekEnabled = value;
				((ModelItem)this).FirePropertyChanged("IsSeekEnabled");
			}
		}
	}

	public bool IsStreamingVideo
	{
		get
		{
			return _isStreamingVideo;
		}
		private set
		{
			if (_isStreamingVideo != value)
			{
				_isStreamingVideo = value;
				((ModelItem)this).FirePropertyChanged("IsStreamingVideo");
			}
		}
	}

	public bool SupressDownloads
	{
		get
		{
			return _supressDownloads;
		}
		private set
		{
			if (_supressDownloads != value)
			{
				_supressDownloads = value;
				((ModelItem)this).FirePropertyChanged("SupressDownloads");
			}
		}
	}

	public int ZoomScaleFactor
	{
		get
		{
			return _zoomScaleFactor;
		}
		set
		{
			_zoomScaleFactor = value;
			((ModelItem)this).FirePropertyChanged("ZoomScaleFactor");
		}
	}

	public bool Playing => _isPlaying;

	public bool PlayingVideo
	{
		get
		{
			return _playingVideo;
		}
		private set
		{
			if (_playingVideo != value)
			{
				_playingVideo = value;
				((ModelItem)this).FirePropertyChanged("PlayingVideo");
			}
		}
	}

	public Command FastforwardHotkey => _fastforwardhotkey;

	public Command RewindHotkey => _rewindhotkey;

	public int CurrentTrackIndex => _currentTrackIndex;

	public float CurrentTrackDuration
	{
		get
		{
			return _currentTrackDuration;
		}
		private set
		{
			if (_currentTrackDuration != value)
			{
				_currentTrackDuration = value;
				((ModelItem)this).FirePropertyChanged("CurrentTrackDuration");
			}
		}
	}

	public float CurrentTrackPosition
	{
		get
		{
			return _currentTrackPosition;
		}
		private set
		{
			if (_currentTrackPosition != value)
			{
				_currentTrackPosition = value;
				((ModelItem)this).FirePropertyChanged("CurrentTrackPosition");
			}
		}
	}

	public float CurrentTrackDownloadProgress
	{
		get
		{
			return _downloadProgress;
		}
		private set
		{
			if (_downloadProgress != value)
			{
				_downloadProgress = value;
				((ModelItem)this).FirePropertyChanged("CurrentTrackDownloadProgress");
			}
		}
	}

	public PlaybackTrack CurrentTrack => _currentTrack;

	public int CurrentTrackRating
	{
		get
		{
			if (CurrentTrack == null || !CurrentTrack.CanRate)
			{
				return 0;
			}
			return CurrentTrack.UserRating;
		}
	}

	public bool ShowErrors
	{
		get
		{
			return _showErrors;
		}
		set
		{
			if (_showErrors != value)
			{
				_showErrors = value;
				((ModelItem)this).FirePropertyChanged("ShowErrors");
			}
		}
	}

	public int ErrorCount => _errors.Count;

	public VideoStream VideoStream => _videoStream;

	public bool CanRender3DVideo => (int)Application.RenderingType != 0;

	public bool HasPlaylist => _hasPlaylist;

	public bool PlaylistSupportsShuffle => _playlistSupportsShuffle;

	public ArrayListDataSet CurrentPlaylist => _currentPlaylist;

	public string QuickMixTitle
	{
		get
		{
			string result = string.Empty;
			if (_playlistCurrent != null)
			{
				result = _playlistCurrent.QuickMixTitle;
			}
			return result;
		}
	}

	public EQuickMixType QuickMixType
	{
		get
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			//IL_0016: Unknown result type (might be due to invalid IL or missing references)
			//IL_0010: Unknown result type (might be due to invalid IL or missing references)
			//IL_0015: Unknown result type (might be due to invalid IL or missing references)
			EQuickMixType result = (EQuickMixType)(-1);
			if (_playlistCurrent != null)
			{
				result = _playlistCurrent.QuickMixType;
			}
			return result;
		}
	}

	public bool DontPlayMarketplaceTracks
	{
		get
		{
			return _dontPlayMarketplaceTracks;
		}
		set
		{
			if (value != _dontPlayMarketplaceTracks)
			{
				_dontPlayMarketplaceTracks = value;
				if (_playlistCurrent != null)
				{
					_playlistCurrent.DontPlayMarketplaceTracks = _dontPlayMarketplaceTracks;
				}
				if (_playlistPending != null)
				{
					_playlistPending.DontPlayMarketplaceTracks = _dontPlayMarketplaceTracks;
				}
			}
		}
	}

	public QuickMixSession QuickMixSession
	{
		get
		{
			QuickMixSession result = null;
			if (_playlistCurrent != null)
			{
				result = _playlistCurrent.QuickMixSession;
			}
			return result;
		}
	}

	public bool IsPlaybackContextCompatible => _isContextCompatible;

	public JumpListPin RequestedJumpListPin
	{
		get
		{
			if (!IsInitialized)
			{
				return null;
			}
			return _requestedJumpListPin;
		}
		set
		{
			if (_requestedJumpListPin != value)
			{
				_requestedJumpListPin = value;
				if (IsInitialized)
				{
					((ModelItem)this).FirePropertyChanged("RequestedJumpListPin");
				}
			}
		}
	}

	public bool ShuffleAllRequested
	{
		get
		{
			if (!IsInitialized)
			{
				return false;
			}
			return _shuffleAllRequested;
		}
		set
		{
			if (_shuffleAllRequested != value)
			{
				_shuffleAllRequested = value;
				if (IsInitialized)
				{
					((ModelItem)this).FirePropertyChanged("ShuffleAllRequested");
				}
			}
		}
	}

	public int BandwidthCapacity
	{
		get
		{
			return _bandwidthCapacity;
		}
		private set
		{
			_bandwidthCapacity = value;
			((ModelItem)this).FirePropertyChanged("BandwidthCapacity");
		}
	}

	public BandwidthUpdateArgs BandwidthUpdateInfo
	{
		get
		{
			return _bandwidthUpdateInfo;
		}
		private set
		{
			_bandwidthUpdateInfo = value;
			((ModelItem)this).FirePropertyChanged("BandwidthUpdateInfo");
		}
	}

	public event EventHandler PlaybackStopped;

	public TransportControls()
	{
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		//IL_005e: Expected O, but got Unknown
		//IL_007f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0089: Expected O, but got Unknown
		//IL_00bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c7: Expected O, but got Unknown
		//IL_00fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0105: Expected O, but got Unknown
		//IL_0139: Unknown result type (might be due to invalid IL or missing references)
		//IL_0143: Expected O, but got Unknown
		//IL_0171: Unknown result type (might be due to invalid IL or missing references)
		//IL_017b: Expected O, but got Unknown
		//IL_01af: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b9: Expected O, but got Unknown
		//IL_01d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01dc: Expected O, but got Unknown
		//IL_0217: Unknown result type (might be due to invalid IL or missing references)
		//IL_0221: Expected O, but got Unknown
		//IL_027c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0286: Expected O, but got Unknown
		//IL_02aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b4: Expected O, but got Unknown
		//IL_02d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e2: Expected O, but got Unknown
		//IL_0306: Unknown result type (might be due to invalid IL or missing references)
		//IL_0310: Expected O, but got Unknown
		//IL_0334: Unknown result type (might be due to invalid IL or missing references)
		//IL_033e: Expected O, but got Unknown
		//IL_0358: Unknown result type (might be due to invalid IL or missing references)
		//IL_0362: Expected O, but got Unknown
		//IL_0370: Unknown result type (might be due to invalid IL or missing references)
		//IL_037a: Expected O, but got Unknown
		//IL_03e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ed: Expected O, but got Unknown
		//IL_03fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_0404: Expected O, but got Unknown
		//IL_0411: Unknown result type (might be due to invalid IL or missing references)
		//IL_041b: Expected O, but got Unknown
		//IL_0422: Unknown result type (might be due to invalid IL or missing references)
		//IL_0427: Unknown result type (might be due to invalid IL or missing references)
		//IL_0433: Unknown result type (might be due to invalid IL or missing references)
		//IL_0438: Unknown result type (might be due to invalid IL or missing references)
		//IL_0476: Unknown result type (might be due to invalid IL or missing references)
		//IL_0480: Expected O, but got Unknown
		//IL_04e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_04f3: Expected O, but got Unknown
		//IL_050e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0518: Expected O, but got Unknown
		_playbackWrapper = PlayerInterop.Instance;
		_taskbarPlayer = TaskbarPlayer.Instance;
		_videoStream = new VideoStream();
		if (!CanRender3DVideo)
		{
			_videoStream.DisplayDetailsChanged += OnVideoDetailsChanged;
		}
		_shuffling = new BooleanChoice((IModelItemOwner)(object)this);
		_shuffling.Value = ClientConfiguration.Playback.ModeShuffle;
		((Choice)_shuffling).ChosenChanged += OnShufflingChanged;
		UpdateShufflingDescription();
		_repeating = new BooleanChoice((IModelItemOwner)(object)this);
		_repeating.Value = ClientConfiguration.Playback.ModeLoop;
		((Choice)_repeating).ChosenChanged += OnRepeatingChanged;
		UpdateRepeatingDescription();
		_muted = new BooleanChoice((IModelItemOwner)(object)this);
		_muted.Value = ClientConfiguration.Playback.Mute;
		((Choice)_muted).ChosenChanged += OnMutingChanged;
		UpdateMutingDescription();
		_showTotalTime = new BooleanChoice((IModelItemOwner)(object)this);
		_showTotalTime.Value = ClientConfiguration.Playback.ShowTotalTime;
		((Choice)_showTotalTime).ChosenChanged += OnShowTotalTimeChanged;
		_showNowPlayingList = new BooleanChoice((IModelItemOwner)(object)this);
		_showNowPlayingList.Value = ClientConfiguration.Playback.ShowNowPlayingList;
		((Choice)_showNowPlayingList).ChosenChanged += OnShowNowPlayingListChanged;
		UpdateShowNowPlayingListDescription();
		_fastforwarding = new BooleanChoice((IModelItemOwner)(object)this);
		((Choice)_fastforwarding).ChosenChanged += OnFastforwardingChanged;
		_rewinding = new BooleanChoice((IModelItemOwner)(object)this);
		((Choice)_rewinding).ChosenChanged += OnRewindingChanged;
		float num = ClientConfiguration.Playback.Volume;
		if (num < 0f || num > 100f)
		{
			num = 50f;
		}
		_volume = new RangedValue((IModelItemOwner)(object)this);
		_volume.MinValue = 0f;
		_volume.MaxValue = 100f;
		_volume.Value = num;
		((ModelItem)_volume).PropertyChanged += OnVolumeControlChanged;
		_play = new Command((IModelItemOwner)(object)this, Shell.LoadString(StringId.IDS_PLAY), (EventHandler)OnPlayClicked);
		_play.Available = false;
		_pause = new Command((IModelItemOwner)(object)this, Shell.LoadString(StringId.IDS_PAUSE), (EventHandler)OnPauseClicked);
		_pause.Available = false;
		_back = new Command((IModelItemOwner)(object)this, Shell.LoadString(StringId.IDS_PREVIOUS), (EventHandler)OnBackClicked);
		_back.Available = false;
		_forward = new Command((IModelItemOwner)(object)this, Shell.LoadString(StringId.IDS_NEXT), (EventHandler)OnForwardClicked);
		_forward.Available = false;
		_stop = new Command((IModelItemOwner)(object)this, Shell.LoadString(StringId.IDS_STOP), (EventHandler)OnStopClicked);
		_stop.Available = false;
		_fastforwardhotkey = new Command((IModelItemOwner)(object)this, (EventHandler)OnFastforwardHotkeyPressed);
		_rewindhotkey = new Command((IModelItemOwner)(object)this, (EventHandler)OnRewindHotkeyPressed);
		_playbackWrapper.StatusChanged += OnPlaybackStatusChanged;
		_playbackWrapper.TransportStatusChanged += OnTransportStatusChanged;
		_playbackWrapper.TransportPositionChanged += OnTransportPositionChanged;
		_playbackWrapper.UriSet += OnUriSet;
		_playbackWrapper.AlertSent += new AnnouncementHandler(OnAlertSent);
		_playbackWrapper.PlayerPropertyChanged += new PlayerPropertyChangedEventHandler(OnPlayerPropertyChanged);
		_playbackWrapper.PlayerBandwithUpdate += new PlayerBandwithUpdateEventHandler(OnBandwidthCapacityUpdate);
		_lastKnownPlayerState = _playbackWrapper.State;
		_lastKnownTransportState = _playbackWrapper.TransportState;
		_lastKnownPosition = _playbackWrapper.Position;
		_shellInstance = (Shell)ZuneShell.DefaultInstance;
		((ModelItem)_shellInstance).PropertyChanged += OnShellPropertyChanged;
		_timerDelayedConfigPersist = new Timer();
		_timerDelayedConfigPersist.Interval = 500;
		_timerDelayedConfigPersist.AutoRepeat = false;
		_timerDelayedConfigPersist.Tick += OnDelayedConfigPersistTimerTick;
		_playerState = PlayerState.Stopped;
		_spectrumConfigList = new List<SpectrumOutputConfig>();
		_isSpectrumAvailable = false;
		IsSeekEnabled = _playbackWrapper.CanSeek;
		Download.Instance.DownloadProgressEvent += new DownloadEventProgressHandler(OnDownloadProgressed);
		_lastKnownCurrentTrackRating = -1;
		_currentTrackRatingChangedEventHandler = OnCurrentTrackRatingChanged;
		_isStreamingTimeoutTimer = new Timer((IModelItemOwner)(object)this);
		_isStreamingTimeoutTimer.Interval = 30000;
		_isStreamingTimeoutTimer.AutoRepeat = false;
		_isStreamingTimeoutTimer.Tick += OnIsStreamingTimeout;
		SignIn.Instance.SignInStatusUpdatedEvent += OnSignInEvent;
	}

	protected override void OnDispose(bool fDisposing)
	{
		//IL_00ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f8: Expected O, but got Unknown
		//IL_0105: Unknown result type (might be due to invalid IL or missing references)
		//IL_010f: Expected O, but got Unknown
		//IL_01ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b8: Expected O, but got Unknown
		((ModelItem)this).OnDispose(fDisposing);
		if (!fDisposing)
		{
			return;
		}
		DisconnectAllSpectrumAnimationSources();
		_isSpectrumAvailable = false;
		Notification.ResetNowPlaying();
		if (WillSaveCurrentPlaylistOnShutdown())
		{
			try
			{
				using Stream serializationStream = File.Create(_savedNowPlayingFilename);
				IFormatter formatter = new BinaryFormatter();
				formatter.Serialize(serializationStream, _playlistCurrent);
			}
			catch (Exception)
			{
			}
		}
		else if (File.Exists(_savedNowPlayingFilename))
		{
			try
			{
				File.Delete(_savedNowPlayingFilename);
			}
			catch (Exception)
			{
			}
		}
		if (_lastKnownPlaybackTrack != null)
		{
			_lastKnownPlaybackTrack.OnEndPlayback(endOfMedia: false);
		}
		_playbackWrapper.StatusChanged -= OnPlaybackStatusChanged;
		_playbackWrapper.TransportStatusChanged -= OnTransportStatusChanged;
		_playbackWrapper.TransportPositionChanged -= OnTransportPositionChanged;
		_playbackWrapper.UriSet -= OnUriSet;
		_playbackWrapper.AlertSent -= new AnnouncementHandler(OnAlertSent);
		_playbackWrapper.PlayerPropertyChanged -= new PlayerPropertyChangedEventHandler(OnPlayerPropertyChanged);
		if (_videoStream != null && !CanRender3DVideo)
		{
			_videoStream.DisplayDetailsChanged -= OnVideoDetailsChanged;
		}
		PlayerInterop.Instance.Dispose();
		if (_videoStream != null)
		{
			_videoStream.Dispose();
			_videoStream = null;
		}
		if (_timerDelayedConfigPersist != null)
		{
			_timerDelayedConfigPersist.Tick -= OnDelayedConfigPersistTimerTick;
			((ModelItem)_timerDelayedConfigPersist).Dispose();
			_timerDelayedConfigPersist = null;
		}
		((ModelItem)_shellInstance).PropertyChanged -= OnShellPropertyChanged;
		Download.Instance.DownloadProgressEvent -= new DownloadEventProgressHandler(OnDownloadProgressed);
		if (_currentTrack != null)
		{
			_currentTrack.RatingChanged.Invoked -= _currentTrackRatingChangedEventHandler;
		}
		SignIn.Instance.SignInStatusUpdatedEvent -= OnSignInEvent;
	}

	private void OnSignInEvent(object sender, EventArgs args)
	{
		if (_playlistCurrent != null)
		{
			_playlistCurrent.UpdateTracks();
		}
		if (_playlistPending != null)
		{
			_playlistPending.UpdateTracks();
		}
	}

	private void PersistSettings()
	{
		if (_timerDelayedConfigPersist != null)
		{
			_timerDelayedConfigPersist.Stop();
			_timerDelayedConfigPersist.Start();
		}
	}

	private void OnDelayedConfigPersistTimerTick(object sender, EventArgs args)
	{
		ClientConfiguration.Playback.ModeShuffle = _shuffling.Value;
		ClientConfiguration.Playback.ModeLoop = _repeating.Value;
		ClientConfiguration.Playback.Mute = _muted.Value;
		ClientConfiguration.Playback.Volume = (int)_volume.Value;
		ClientConfiguration.Playback.ShowTotalTime = _showTotalTime.Value;
		ClientConfiguration.Playback.ShowNowPlayingList = _showNowPlayingList.Value;
	}

	private void OnCurrentTrackRatingChanged(object sender, EventArgs args)
	{
		int num = -1;
		if (CurrentTrack != null && CurrentTrack.CanRate)
		{
			num = CurrentTrackRating;
		}
		if (num != _lastKnownCurrentTrackRating)
		{
			((ModelItem)this).FirePropertyChanged("CurrentTrackRating");
			_lastKnownCurrentTrackRating = num;
		}
	}

	public void TrackRatingUpdatedExternally(int mediaID, int newRating)
	{
		if (CurrentPlaylist == null)
		{
			return;
		}
		foreach (PlaybackTrack item in (ListDataSet)CurrentPlaylist)
		{
			if (item is LibraryPlaybackTrack libraryPlaybackTrack && libraryPlaybackTrack.MediaId == mediaID)
			{
				libraryPlaybackTrack.RatingUpdatedExternally(newRating);
			}
		}
	}

	private void OnIsStreamingTimeout(object sender, EventArgs args)
	{
		SupressDownloads = false;
	}

	private void DeserializeNowPlayingList(object arg)
	{
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		//IL_004f: Expected O, but got Unknown
		string text = arg as string;
		object obj = null;
		if (text != null && File.Exists(text))
		{
			try
			{
				using Stream serializationStream = File.OpenRead(text);
				IFormatter formatter = new BinaryFormatter();
				obj = formatter.Deserialize(serializationStream);
			}
			catch (Exception)
			{
			}
		}
		if (obj != null)
		{
			Application.DeferredInvoke(new DeferredInvokeHandler(DeserializationComplete), obj);
		}
	}

	private void DeserializationComplete(object arg)
	{
		NowPlayingList nowPlayingList = arg as NowPlayingList;
		if (_playlistCurrent == null && nowPlayingList != null)
		{
			_playlistCurrent = nowPlayingList;
			ShowNotification();
			UpdatePropertiesAndCommands();
			if (_resumeLastNowPlayingRequested)
			{
				Play.Invoke();
			}
		}
	}

	public bool WillSaveCurrentPlaylistOnShutdown()
	{
		if (_playlistCurrent != null && _playlistCurrent.CurrentTrack != null && !_playlistCurrent.CurrentTrack.IsVideo)
		{
			return _playlistCurrent.QuickMixSession == null;
		}
		return false;
	}

	public void StartPlayingAt(PlaybackTrack track)
	{
		if (_playlistCurrent != null && _playlistCurrent.TrackList != null)
		{
			int num = ((ListDataSet)_playlistCurrent.TrackList).IndexOf((object)track);
			if (num > -1)
			{
				StartPlayingAt(num);
			}
		}
	}

	public void StartPlayingAt(int newCurrentIndex)
	{
		if (_playlistCurrent == null)
		{
			return;
		}
		_playlistCurrent.MoveToTrackIndex(newCurrentIndex);
		if (_playerState == PlayerState.Playing)
		{
			SetUriOnPlayer();
			return;
		}
		_playlistPending = _playlistCurrent;
		if (_playerState == PlayerState.Paused)
		{
			_playbackWrapper.Stop();
		}
		else
		{
			PlayPendingList();
		}
	}

	public void CloseCurrentSession()
	{
		Stop.Invoke();
	}

	public void SeekToPosition(float value)
	{
		long num = (long)((double)value * 10000000.0);
		if (_playbackWrapper != null)
		{
			_playbackWrapper.SeekToAbsolutePosition(num);
		}
		_rewinding.Value = false;
		_fastforwarding.Value = false;
	}

	public void ClearAllErrors()
	{
		if (_errors.Count > 0)
		{
			_errors.Clear();
			((ModelItem)this).FirePropertyChanged("ErrorCount");
		}
	}

	public bool IsCurrentTrack(Guid zuneMediaId)
	{
		PlaybackTrack currentTrack = CurrentTrack;
		if (currentTrack != null && !GuidHelper.IsEmpty(currentTrack.ZuneMediaId))
		{
			return currentTrack.ZuneMediaId == zuneMediaId;
		}
		return false;
	}

	public int GetErrorCode(Guid zuneMediaId)
	{
		if (_errors.Count > 0)
		{
			foreach (KeyValuePair<PlaybackTrack, int> error in _errors)
			{
				PlaybackTrack key = error.Key;
				if (key != null && key.ZuneMediaId == zuneMediaId)
				{
					return error.Value;
				}
			}
		}
		return 0;
	}

	public bool IsCurrentTrack(int id, MediaType type, Guid zuneMediaId)
	{
		if (CurrentTrack is LibraryPlaybackTrack libraryPlaybackTrack)
		{
			if (libraryPlaybackTrack.MediaId == id)
			{
				return libraryPlaybackTrack.MediaType == type;
			}
			return false;
		}
		return IsCurrentTrack(zuneMediaId);
	}

	public int GetLibraryErrorCode(int id, MediaType type)
	{
		if (_errors.Count > 0)
		{
			foreach (KeyValuePair<PlaybackTrack, int> error in _errors)
			{
				if (error.Key is LibraryPlaybackTrack libraryPlaybackTrack && libraryPlaybackTrack.MediaId == id && libraryPlaybackTrack.MediaType == type)
				{
					return error.Value;
				}
			}
		}
		return 0;
	}

	public int GetLibraryErrorCode(PlaybackTrack track)
	{
		if (_errors.TryGetValue(track, out var value))
		{
			return value;
		}
		return 0;
	}

	internal void ClearError(PlaybackTrack track)
	{
		if (_errors.Remove(track))
		{
			((ModelItem)this).FirePropertyChanged("ErrorCount");
		}
	}

	private void OnBandwidthCapacityUpdate(object sender, BandwidthUpdateArgs args)
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_0012: Expected O, but got Unknown
		Application.DeferredInvoke(new DeferredInvokeHandler(OnBandwidthCapacityUpdateOnApp), (object)args);
	}

	private void OnBandwidthCapacityUpdateOnApp(object obj)
	{
		//IL_0005: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Expected O, but got Unknown
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Invalid comparison between Unknown and I4
		if (obj != null)
		{
			BandwidthUpdateArgs val = (BandwidthUpdateArgs)obj;
			if (val != null && (int)val.currentState == 3)
			{
				BandwidthCapacity = val.RecentAverageBandwidth;
				BandwidthUpdateInfo = val;
			}
		}
	}

	private void OnStreamingRestrictionResponse(HRESULT hr)
	{
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		//IL_0082: Unknown result type (might be due to invalid IL or missing references)
		//IL_008d: Expected O, but got Unknown
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		if (((HRESULT)(ref hr)).IsError)
		{
			string title = Shell.LoadString(StringId.IDS_PLAYBACK_CANNOT_PLAY);
			string description;
			if (hr == HRESULT._ZEST_E_MAX_CONCURRENTSTREAMING_EXCEEDED || hr == HRESULT._ZEST_E_MULTITUNER_CONCURRENTSTREAMING_DETECTED || hr == HRESULT._ZEST_E_MEDIAINSTANCE_STREAMING_OCCUPIED)
			{
				ErrorMapperResult mappedErrorDescriptionAndUrl = ErrorMapperApi.GetMappedErrorDescriptionAndUrl(((HRESULT)(ref hr)).Int);
				description = mappedErrorDescriptionAndUrl.Description;
			}
			else
			{
				description = Shell.LoadString(StringId.IDS_PLAYBACK_UNKNOWN_CONCURRENT_STREAMING_RESTRICTION);
			}
			Application.DeferredInvoke((DeferredInvokeHandler)delegate
			{
				Stop.Invoke();
				MessageBox.Show(title, description, (EventHandler)null);
			}, (DeferredInvokePriority)1);
		}
	}

	private void ReportStreamingAction(PlayerState previousPlayerState)
	{
		//IL_0172: Unknown result type (might be due to invalid IL or missing references)
		//IL_017c: Expected O, but got Unknown
		//IL_0069: Unknown result type (might be due to invalid IL or missing references)
		//IL_0073: Expected O, but got Unknown
		//IL_0118: Unknown result type (might be due to invalid IL or missing references)
		//IL_0122: Expected O, but got Unknown
		//IL_00f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fb: Expected O, but got Unknown
		//IL_00c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d0: Expected O, but got Unknown
		//IL_0145: Unknown result type (might be due to invalid IL or missing references)
		//IL_014f: Expected O, but got Unknown
		if (CurrentTrack != null && CurrentTrack.IsVideo && CurrentTrack.IsStreaming)
		{
			Guid zuneMediaId = CurrentTrack.ZuneMediaId;
			if (zuneMediaId != _streamingReportMediaId && _streamingReportIsOpen)
			{
				_streamingReportIsOpen = false;
				previousPlayerState = PlayerState.Stopped;
				Service.Instance.ReportStreamingAction((EStreamingActionType)1, _streamingReportMediaInstanceId, new AsyncCompleteHandler(OnStreamingRestrictionResponse));
			}
			if (previousPlayerState == PlayerState.Stopped && _playerState == PlayerState.Playing)
			{
				_streamingReportMediaInstanceId = CurrentTrack.ZuneMediaInstanceId;
				if (_streamingReportMediaInstanceId != Guid.Empty)
				{
					_streamingReportIsOpen = true;
					_streamingReportMediaId = zuneMediaId;
					Service.Instance.ReportStreamingAction((EStreamingActionType)0, _streamingReportMediaInstanceId, new AsyncCompleteHandler(OnStreamingRestrictionResponse));
				}
			}
			else if (previousPlayerState == PlayerState.Paused && _playerState == PlayerState.Playing)
			{
				Service.Instance.ReportStreamingAction((EStreamingActionType)3, _streamingReportMediaInstanceId, new AsyncCompleteHandler(OnStreamingRestrictionResponse));
			}
			else if (_playerState == PlayerState.Paused)
			{
				Service.Instance.ReportStreamingAction((EStreamingActionType)2, _streamingReportMediaInstanceId, new AsyncCompleteHandler(OnStreamingRestrictionResponse));
			}
			else if (_playerState == PlayerState.Stopped)
			{
				_streamingReportIsOpen = false;
				Service.Instance.ReportStreamingAction((EStreamingActionType)1, _streamingReportMediaInstanceId, new AsyncCompleteHandler(OnStreamingRestrictionResponse));
			}
		}
		else if (_streamingReportIsOpen)
		{
			_streamingReportIsOpen = false;
			Service.Instance.ReportStreamingAction((EStreamingActionType)1, _streamingReportMediaInstanceId, new AsyncCompleteHandler(OnStreamingRestrictionResponse));
		}
	}

	private void SetPlayerState(PlayerState stateNew)
	{
		PlayerState playerState = _playerState;
		if (stateNew != _playerState)
		{
			_playerState = stateNew;
			if (_playerState == PlayerState.Stopped)
			{
				_rewinding.Value = false;
				_fastforwarding.Value = false;
				_lastKnownSetUriCallID++;
				((ModelItem)this).FirePropertyChanged("PlaybackStopped");
				if (this.PlaybackStopped != null)
				{
					this.PlaybackStopped(this, null);
				}
			}
		}
		UpdatePropertiesAndCommands();
		ReportStreamingAction(playerState);
	}

	public void PlayItem(object item, PlayNavigationOptions playNavigationOptions, PlaybackContext playbackContext)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Expected O, but got Unknown
		ArrayListDataSet val = new ArrayListDataSet();
		((ListDataSet)val).Add(item);
		PlayItemsWorker((IList)val, -1, clearQueue: true, playNavigationOptions, playbackContext, null);
	}

	public void PlayItem(object item)
	{
		PlayItem(item, PlayNavigationOptions.NavigateVideosToNowPlaying);
	}

	public void PlayItem(object item, PlayNavigationOptions playNavigationOptions)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Expected O, but got Unknown
		ArrayListDataSet val = new ArrayListDataSet();
		((ListDataSet)val).Add(item);
		PlayItemsWorker((IList)val, -1, clearQueue: true, playNavigationOptions, null);
	}

	public void PlayItem(object item, PlaybackContext playbackContext)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Expected O, but got Unknown
		ArrayListDataSet val = new ArrayListDataSet();
		((ListDataSet)val).Add(item);
		PlayItemsWorker((IList)val, -1, clearQueue: true, PlayNavigationOptions.NavigateVideosToNowPlaying, playbackContext, null);
	}

	public void PlayItems(IList items)
	{
		PlayItemsWorker(items, -1, clearQueue: true, PlayNavigationOptions.NavigateVideosToNowPlaying, null);
	}

	public void PlayItems(IList items, PlayNavigationOptions playNavigationOptions)
	{
		PlayItemsWorker(items, -1, clearQueue: true, playNavigationOptions, null);
	}

	public void PlayItems(IList items, PlayNavigationOptions playNavigationOptions, ContainerPlayMarker containerPlayMarker)
	{
		PlayItemsWorker(items, -1, clearQueue: true, playNavigationOptions, containerPlayMarker);
	}

	public void PlayItems(IList items, PlayNavigationOptions playNavigationOptions, PlaybackContext playbackContext)
	{
		PlayItemsWorker(items, -1, clearQueue: true, playNavigationOptions, playbackContext, null);
	}

	public void PlayItems(IList items, PlaybackContext playbackContext)
	{
		PlayItemsWorker(items, -1, clearQueue: true, PlayNavigationOptions.NavigateVideosToNowPlaying, playbackContext, null);
	}

	public void PlayItems(IList items, int startIndex)
	{
		PlayItemsWorker(items, startIndex, clearQueue: true, PlayNavigationOptions.NavigateVideosToNowPlaying, null);
	}

	public void PlayItems(IList items, int startIndex, PlayNavigationOptions playNavigationOptions, ContainerPlayMarker containerPlayMarker)
	{
		PlayItemsWorker(items, startIndex, clearQueue: true, PlayNavigationOptions.NavigateVideosToNowPlaying, containerPlayMarker);
	}

	public void AddToNowPlaying(IList items)
	{
		int num = PlayItemsWorker(items, -1, clearQueue: false, PlayNavigationOptions.NavigateVideosToNowPlaying, null);
		if (num > 0)
		{
			PlaylistManager.Instance.NotifyItemsAdded(-1, num);
		}
	}

	private int PlayItemsWorker(IList items, int startIndex, bool clearQueue, PlayNavigationOptions playNavigationOptions, ContainerPlayMarker containerPlayMarker)
	{
		return PlayItemsWorker(items, startIndex, clearQueue, playNavigationOptions, _shellInstance.CurrentPage.PlaybackContext, containerPlayMarker);
	}

	private int PlayItemsWorker(IList items, int startIndex, bool clearQueue, PlayNavigationOptions playNavigationOptions, PlaybackContext playbackContext, ContainerPlayMarker containerPlayMarker)
	{
		//IL_0087: Unknown result type (might be due to invalid IL or missing references)
		//IL_008d: Invalid comparison between Unknown and I4
		//IL_0090: Unknown result type (might be due to invalid IL or missing references)
		//IL_0096: Invalid comparison between Unknown and I4
		//IL_00e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ef: Invalid comparison between Unknown and I4
		//IL_00f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f8: Invalid comparison between Unknown and I4
		PerfTrace.TraceUICollectionEvent(UICollectionEvent.PlayRequestIssued, "");
		int num = 0;
		bool flag = _playlistCurrent != null;
		if (clearQueue || !flag)
		{
			if (clearQueue || _playlistPending == null)
			{
				if (_playlistPending != null)
				{
					_playlistPending.Dispose();
				}
				_playlistPending = new NowPlayingList(items, startIndex, playbackContext, playNavigationOptions, _shuffling.Value, containerPlayMarker, _dontPlayMarketplaceTracks);
				num = _playlistPending.Count;
			}
			else
			{
				num = _playlistPending.AddItems(items);
			}
			if (playbackContext == PlaybackContext.QuickMix)
			{
				if ((int)_lastKnownTransportState == 2 || (int)_lastKnownTransportState == 3)
				{
					_playbackWrapper.Stop();
				}
				else
				{
					_playlistPending.PlayWhenReady = true;
				}
			}
			else if (_playlistPending.Count == 0)
			{
				_playlistPending.Dispose();
				_playlistPending = null;
				Stop.Invoke();
			}
			else if (flag && ((int)_lastKnownTransportState == 2 || (int)_lastKnownTransportState == 3))
			{
				_playbackWrapper.Stop();
			}
			else
			{
				PlayPendingList();
			}
		}
		else
		{
			num = _playlistCurrent.AddItems(items);
			if (_playlistPending != null)
			{
				_playlistPending.Dispose();
			}
			_playlistPending = null;
			UpdateNextTrack();
		}
		return num;
	}

	internal void PlayPendingList()
	{
		if (_playlistPending == null)
		{
			return;
		}
		if (_playlistCurrent != null && _playlistCurrent != _playlistPending)
		{
			_playlistCurrent.Dispose();
		}
		_playlistCurrent = _playlistPending;
		_playlistPending = null;
		_playlistCurrent.SetShuffling(_shuffling.Value);
		_playlistCurrent.SetRepeating(_repeating.Value);
		_consecutiveErrors = 0;
		if (_errors.Count > 0)
		{
			((ModelItem)this).FirePropertyChanged("ErrorCount");
			_errors.Clear();
		}
		SetPlayerState(PlayerState.Playing);
		SetUriOnPlayer();
		bool flag = false;
		if (_playlistCurrent != null)
		{
			PlaybackTrack currentTrack = _playlistCurrent.CurrentTrack;
			PlayNavigationOptions playNavigationOptions = _playlistCurrent.PlayNavigationOptions;
			bool flag2 = false;
			if (currentTrack != null && currentTrack.IsVideo)
			{
				flag = true;
			}
			switch (playNavigationOptions)
			{
			case PlayNavigationOptions.NavigateVideosToNowPlaying:
				if (flag)
				{
					flag2 = true;
				}
				break;
			default:
				flag2 = true;
				_playlistCurrent.PlayNavigationOptions = PlayNavigationOptions.NavigateVideosToNowPlaying;
				break;
			case PlayNavigationOptions.None:
				break;
			}
			if (flag2)
			{
				NowPlayingLand.NavigateToLand(playNavigationOptions == PlayNavigationOptions.NavigateToNowPlayingWithMix, exitOnPlaybackStopped: true);
			}
		}
		PlayingVideo = flag;
	}

	public void RemoveFromNowPlaying(IList indices)
	{
		if (_playlistCurrent != null)
		{
			bool flag = _playlistCurrent.Remove(indices);
			if (_playlistCurrent.Count == 0)
			{
				Stop.Invoke();
			}
			else if (flag)
			{
				SetUriOnPlayer();
			}
			else
			{
				UpdateNextTrack();
			}
		}
	}

	public void ReorderNowPlaying(IList indices, int targetIndex)
	{
		if (_playlistCurrent != null)
		{
			_playlistCurrent.Reorder(indices, targetIndex);
			UpdateNextTrack();
		}
	}

	public IList GetNextTracks(int count)
	{
		IList result = null;
		if (_playlistCurrent != null)
		{
			result = _playlistCurrent.GetNextTracks(count);
		}
		return result;
	}

	public IList CreateAlbumListForBackground(IList allAlbums, int totalDesired)
	{
		//IL_007c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0083: Expected O, but got Unknown
		//IL_01ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d0: Expected O, but got Unknown
		List<object> list = new List<object>(totalDesired);
		Dictionary<int, object> dictionary = new Dictionary<int, object>();
		if (_playlistCurrent != null)
		{
			int num = Math.Min(_playlistCurrent.Count, totalDesired);
			for (int i = 0; i < num; i++)
			{
				if (((ListDataSet)_playlistCurrent.TrackList)[i] is LibraryPlaybackTrack { MediaType: MediaType.Track } libraryPlaybackTrack)
				{
					dictionary[libraryPlaybackTrack.AlbumLibraryId] = null;
				}
			}
		}
		List<object> list2 = new List<object>(totalDesired);
		for (int j = 0; j < allAlbums.Count; j++)
		{
			DataProviderObject val = (DataProviderObject)allAlbums[j];
			int key = (int)val.GetProperty("LibraryId");
			if ((bool)val.GetProperty("HasAlbumArt"))
			{
				if (dictionary.ContainsKey(key))
				{
					list.Add(val);
				}
				else if (list2.Count < totalDesired)
				{
					list2.Add(val);
				}
			}
			if (list.Count >= totalDesired)
			{
				break;
			}
		}
		for (int k = 0; k < list2.Count; k++)
		{
			if (list.Count >= totalDesired)
			{
				break;
			}
			list.Add(list2[k]);
		}
		if (list.Count == 0)
		{
			return null;
		}
		foreach (object item in list)
		{
			DisableSlowDataThumbnailExtraction(item);
		}
		int num2 = 0;
		while (list.Count < totalDesired)
		{
			list.Add(list[num2++]);
		}
		Random random = new Random();
		for (int num3 = list.Count - 1; num3 > 0; num3--)
		{
			int index = random.Next(num3 + 1);
			object value = list[index];
			list[index] = list[num3];
			list[num3] = value;
		}
		return (IList)new ListDataSet((IList)list);
	}

	public void DisableSlowDataThumbnailExtraction(object album)
	{
		LibraryDataProviderItemBase val = (LibraryDataProviderItemBase)((album is LibraryDataProviderItemBase) ? album : null);
		if (val != null)
		{
			val.SetSlowDataThumbnailExtraction(false);
		}
	}

	public void Phase2Init()
	{
		if (!CanRender3DVideo)
		{
			_playbackWrapper.WindowHandle = Application.Window.Handle;
		}
		else
		{
			_playbackWrapper.DynamicImage = _videoStream.StreamID;
		}
		ThreadPool.QueueUserWorkItem(AsyncPhase2Init, null);
	}

	private void AsyncPhase2Init(object arg)
	{
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Expected O, but got Unknown
		_playbackWrapper.Initialize();
		Application.DeferredInvoke(new DeferredInvokeHandler(CompletePhase2Init), (object)null);
	}

	private void CompletePhase2Init(object obj)
	{
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		//IL_005b: Expected O, but got Unknown
		_playbackWrapper.Volume = (int)_volume.Value;
		_playbackWrapper.Mute = _muted.Value;
		_isSpectrumAvailable = true;
		ConnectAllSpectrumAnimationSources();
		_taskbarPlayer.Initialize(Application.Window.Handle, new TaskbarPlayerCommandHandler(OnTaskbarPlayerCommand));
		IsInitialized = true;
		ThreadPool.QueueUserWorkItem(DeserializeNowPlayingList, _savedNowPlayingFilename);
		if (RequestedJumpListPin != null)
		{
			((ModelItem)this).FirePropertyChanged("RequestedJumpListPin");
		}
		if (ShuffleAllRequested)
		{
			((ModelItem)this).FirePropertyChanged("ShuffleAllRequested");
		}
	}

	private void OnTaskbarPlayerCommand(ETaskbarPlayerCommand command, int value)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_002c: Expected I4, but got Unknown
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_002f: Invalid comparison between Unknown and I4
		//IL_007f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0080: Unknown result type (might be due to invalid IL or missing references)
		//IL_0083: Invalid comparison between Unknown and I4
		//IL_0085: Unknown result type (might be due to invalid IL or missing references)
		//IL_0088: Invalid comparison between Unknown and I4
		//IL_008a: Unknown result type (might be due to invalid IL or missing references)
		//IL_008d: Invalid comparison between Unknown and I4
		switch ((int)command)
		{
		case 0:
			UpdateTaskbarPlayer();
			return;
		case 1:
			Play.Invoke();
			return;
		case 2:
			Pause.Invoke();
			return;
		case 8:
			Back.Invoke();
			return;
		case 4:
			Forward.Invoke();
			return;
		case 3:
		case 5:
		case 6:
		case 7:
			return;
		}
		if ((int)command != 16 || CurrentTrack == null || !CurrentTrack.CanRate)
		{
			return;
		}
		ETaskbarPlayerState val = (ETaskbarPlayerState)value;
		if ((int)val != 16)
		{
			if ((int)val != 32)
			{
				if ((int)val == 64)
				{
					CurrentTrack.UserRating = 2;
				}
			}
			else
			{
				CurrentTrack.UserRating = 8;
			}
		}
		else
		{
			CurrentTrack.UserRating = 0;
		}
		UpdateTaskbarPlayer();
	}

	private void UpdateTaskbarPlayer()
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		//IL_006e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0071: Unknown result type (might be due to invalid IL or missing references)
		//IL_0072: Unknown result type (might be due to invalid IL or missing references)
		//IL_0073: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		//IL_0059: Unknown result type (might be due to invalid IL or missing references)
		//IL_0089: Unknown result type (might be due to invalid IL or missing references)
		//IL_008a: Unknown result type (might be due to invalid IL or missing references)
		//IL_008b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0067: Unknown result type (might be due to invalid IL or missing references)
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		//IL_006b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0060: Unknown result type (might be due to invalid IL or missing references)
		//IL_0063: Unknown result type (might be due to invalid IL or missing references)
		//IL_0064: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ef: Unknown result type (might be due to invalid IL or missing references)
		ETaskbarPlayerState val = (ETaskbarPlayerState)0;
		val = (ETaskbarPlayerState)(val | ((_playerState == PlayerState.Playing) ? 2 : 0));
		val = (ETaskbarPlayerState)(val | ((_playerState == PlayerState.Paused) ? 4 : 0));
		val = (ETaskbarPlayerState)(val | (_playerState == PlayerState.Stopped));
		if (CurrentTrack != null && CurrentTrack.CanRate)
		{
			RatingConstants userRating = (RatingConstants)CurrentTrack.UserRating;
			val = (ETaskbarPlayerState)((userRating == RatingConstants.Unrated) ? (val | 0x10) : ((userRating > RatingConstants.MaxHateIt) ? (val | 0x20) : (val | 0x40)));
		}
		else
		{
			val = (ETaskbarPlayerState)(val | 0x10);
		}
		val = (ETaskbarPlayerState)(val | (Play.Available ? 256 : 0));
		val = (ETaskbarPlayerState)(val | (Pause.Available ? 512 : 0));
		val = (ETaskbarPlayerState)(val | (Forward.Available ? 1024 : 0));
		val = (ETaskbarPlayerState)(val | (Back.Available ? 2048 : 0));
		if (CurrentTrack != null && CurrentTrack.CanRate)
		{
			val = (ETaskbarPlayerState)(val | 0x1000);
		}
		_taskbarPlayer.UpdateToolbar(val);
	}

	public int CreateSpectrumAnimationSource(int numBands, bool outputFrequencyData, bool outputWaveformData, bool enableStereoOutput)
	{
		Dictionary<string, int> dictionary = new Dictionary<string, int>();
		for (int i = 0; i < numBands; i++)
		{
			if (outputFrequencyData)
			{
				if (!enableStereoOutput)
				{
					dictionary[$"Frequency{i}"] = i;
				}
				else
				{
					dictionary[$"FrequencyL{i}"] = i;
					dictionary[$"FrequencyR{i}"] = i + 1024;
				}
			}
			if (outputWaveformData)
			{
				if (!enableStereoOutput)
				{
					dictionary[$"Waveform{i}"] = i + 2048;
					continue;
				}
				dictionary[$"WaveformL{i}"] = i + 2048;
				dictionary[$"WaveformR{i}"] = i + 3072;
			}
		}
		int num = Application.CreateExternalAnimationInput((IDictionary<string, int>)dictionary);
		SpectrumOutputConfig item = new SpectrumOutputConfig
		{
			SourceId = (uint)num,
			NumBands = (uint)numBands,
			Frequency = outputFrequencyData,
			Waveform = outputWaveformData,
			Stereo = enableStereoOutput,
			IsConnected = _isSpectrumAvailable
		};
		_spectrumConfigList.Add(item);
		if (_isSpectrumAvailable)
		{
			_playbackWrapper.ConnectAnimationsToSpectrumAnalyzer(item.SourceId, item.NumBands, item.Frequency, item.Waveform, item.Stereo);
		}
		return num;
	}

	public void DisposeSpectrumAnimationSource(int inputSourceId)
	{
		if (inputSourceId <= 0)
		{
			return;
		}
		for (int i = 0; i < _spectrumConfigList.Count; i++)
		{
			SpectrumOutputConfig spectrumOutputConfig = _spectrumConfigList[i];
			if (spectrumOutputConfig.SourceId == inputSourceId)
			{
				if (spectrumOutputConfig.IsConnected)
				{
					_playbackWrapper.DisconnectAnimationsFromSpectrumAnalyzer(spectrumOutputConfig.SourceId);
					spectrumOutputConfig.IsConnected = false;
				}
				Application.DisposeExternalAnimationInput(inputSourceId);
				_spectrumConfigList.RemoveAt(i);
				break;
			}
		}
	}

	public void ConnectAllSpectrumAnimationSources()
	{
		for (int i = 0; i < _spectrumConfigList.Count; i++)
		{
			SpectrumOutputConfig value = _spectrumConfigList[i];
			if (!value.IsConnected)
			{
				_playbackWrapper.ConnectAnimationsToSpectrumAnalyzer(value.SourceId, value.NumBands, value.Frequency, value.Waveform, value.Stereo);
				value.IsConnected = true;
				_spectrumConfigList[i] = value;
			}
		}
	}

	public void DisconnectAllSpectrumAnimationSources()
	{
		if (!_isSpectrumAvailable)
		{
			return;
		}
		for (int i = 0; i < _spectrumConfigList.Count; i++)
		{
			SpectrumOutputConfig value = _spectrumConfigList[i];
			if (value.IsConnected)
			{
				_playbackWrapper.DisconnectAnimationsFromSpectrumAnalyzer(value.SourceId);
				value.IsConnected = false;
				_spectrumConfigList[i] = value;
			}
		}
	}

	public void ResumeLastNowPlayingHandler()
	{
		if (!IsInitialized)
		{
			_resumeLastNowPlayingRequested = true;
		}
		else if (!_isPlaying)
		{
			Play.Invoke();
		}
	}

	private void OnShellPropertyChanged(object sender, PropertyChangedEventArgs args)
	{
		if (args.PropertyName == "CurrentPage")
		{
			_pagePlaybackContext = _shellInstance.CurrentPage.PlaybackContext;
			UpdatePropertiesAndCommands();
		}
	}

	private void OnDownloadProgressed(Guid zuneMediaId, float percent)
	{
		if (CurrentTrack != null && CurrentTrack.ZuneMediaId == zuneMediaId)
		{
			CurrentTrackDownloadProgress = percent;
		}
	}

	private void OnShufflingChanged(object sender, EventArgs e)
	{
		if (_playlistCurrent != null)
		{
			_playlistCurrent.SetShuffling(_shuffling.Value);
			UpdateNextTrack();
		}
		UpdateShufflingDescription();
		PersistSettings();
		SQMLog.Log((SQMDataId)136, 1);
	}

	private void UpdateShufflingDescription()
	{
		StringId stringId = ((!_shuffling.Value) ? StringId.IDS_SHUFFLE_ON : StringId.IDS_SHUFFLE_OFF);
		((ModelItem)_shuffling).Description = Shell.LoadString(stringId);
	}

	private void OnRepeatingChanged(object sender, EventArgs e)
	{
		if (_playlistCurrent != null)
		{
			_playlistCurrent.SetRepeating(_repeating.Value);
			UpdateNextTrack();
		}
		SQMLog.Log((SQMDataId)135, 1);
		UpdateRepeatingDescription();
		PersistSettings();
	}

	private void UpdateRepeatingDescription()
	{
		StringId stringId = ((!_repeating.Value) ? StringId.IDS_REPEAT_ON : StringId.IDS_REPEAT_OFF);
		((ModelItem)_repeating).Description = Shell.LoadString(stringId);
	}

	private void OnMutingChanged(object sender, EventArgs e)
	{
		UpdateMutingDescription();
		_playbackWrapper.Mute = _muted.Value;
		SQMLog.Log((SQMDataId)138, 1);
		PersistSettings();
	}

	private void UpdateMutingDescription()
	{
		StringId stringId = ((!_muted.Value) ? StringId.IDS_MUTE : StringId.IDS_UNMUTE);
		((ModelItem)_muted).Description = Shell.LoadString(stringId);
	}

	private void OnShowTotalTimeChanged(object sender, EventArgs e)
	{
		PersistSettings();
	}

	private void OnShowNowPlayingListChanged(object sender, EventArgs e)
	{
		UpdateShowNowPlayingListDescription();
		PersistSettings();
	}

	private void UpdateShowNowPlayingListDescription()
	{
		StringId stringId = ((!_showNowPlayingList.Value) ? StringId.IDS_NOWPLAYINGLIST_ON : StringId.IDS_NOWPLAYINGLIST_OFF);
		((ModelItem)_showNowPlayingList).Description = Shell.LoadString(stringId);
	}

	private void OnPlayingChanged(object sender, EventArgs e)
	{
	}

	private void OnPlayClicked(object sender, EventArgs e)
	{
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Invalid comparison between Unknown and I4
		if (!_play.Available)
		{
			return;
		}
		SQMLog.Log((SQMDataId)130, 1);
		if (Playing || _playlistCurrent == null)
		{
			return;
		}
		if ((int)_lastKnownPlayerState != 1)
		{
			SetPlayerState(PlayerState.Playing);
			_playbackWrapper.Play();
			if (PlayingVideo && _playlistCurrent.PlayNavigationOptions == PlayNavigationOptions.NavigateVideosToNowPlaying)
			{
				NowPlayingLand.NavigateToLand();
			}
		}
		else
		{
			if (_playlistPending != null)
			{
				_playlistPending.Dispose();
			}
			_playlistPending = _playlistCurrent;
			PlayPendingList();
		}
	}

	private void OnPauseClicked(object sender, EventArgs e)
	{
		if (_pause.Available)
		{
			SQMLog.Log((SQMDataId)131, 1);
			if (Playing)
			{
				SetPlayerState(PlayerState.Paused);
				_playbackWrapper.Pause();
			}
		}
	}

	private void OnStopClicked(object sender, EventArgs e)
	{
		if (_stop.Available)
		{
			SQMLog.Log((SQMDataId)132, 1);
			if (_playlistPending != null)
			{
				_playlistPending.Dispose();
			}
			_playlistPending = null;
			if (_playlistCurrent != null)
			{
				_playlistCurrent.Dispose();
			}
			_playlistCurrent = null;
			if (_playerState != PlayerState.Stopped)
			{
				SetPlayerState(PlayerState.Stopped);
				_playbackWrapper.Stop();
			}
			else
			{
				UpdatePropertiesAndCommands();
			}
		}
	}

	private void OnBackClicked(object sender, EventArgs e)
	{
		if (_back.Available && _playlistCurrent != null)
		{
			SQMLog.Log((SQMDataId)134, 1);
			if (_lastKnownPosition > 50000000 || !_playlistCurrent.CanRetreat)
			{
				_playbackWrapper.SeekToAbsolutePosition(0L);
				return;
			}
			_playlistCurrent.Retreat();
			SetUriOnPlayer();
		}
	}

	private void OnForwardClicked(object sender, EventArgs e)
	{
		if (!_forward.Available || _playlistCurrent == null)
		{
			return;
		}
		SQMLog.Log((SQMDataId)133, 1);
		if (_currentTrack == null || !_currentTrack.IsVideo)
		{
			if (_lastKnownPlaybackTrack != null)
			{
				_lastKnownPlaybackTrack.OnSkip();
			}
			if (_playlistCurrent.CanAdvance)
			{
				_playlistCurrent.Advance();
				SetUriOnPlayer();
			}
			else
			{
				SetPlayerState(PlayerState.Stopped);
				_playbackWrapper.Stop();
			}
		}
	}

	private void OnFastforwardingChanged(object sender, EventArgs e)
	{
		if ((_currentTrack == null || !_currentTrack.IsVideo || _playbackWrapper.CanChangeVideoRate) && _fastforwarding.Value)
		{
			_rewinding.Value = false;
			_playbackWrapper.Rate = 5f;
		}
		else
		{
			_playbackWrapper.Rate = 1f;
		}
	}

	private void OnRewindingChanged(object sender, EventArgs e)
	{
		if ((_currentTrack == null || !_currentTrack.IsVideo || _playbackWrapper.CanChangeVideoRate) && _rewinding.Value)
		{
			_fastforwarding.Value = false;
			_playbackWrapper.Rate = -5f;
		}
		else
		{
			_playbackWrapper.Rate = 1f;
		}
	}

	private void OnFastforwardHotkeyPressed(object sender, EventArgs e)
	{
		_fastforwarding.Value = !_fastforwarding.Value;
	}

	private void OnRewindHotkeyPressed(object sender, EventArgs e)
	{
		_rewinding.Value = !_rewinding.Value;
	}

	private void OnPlaybackStatusChanged(object sender, EventArgs e)
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Expected O, but got Unknown
		Application.DeferredInvoke(new DeferredInvokeHandler(DeferredPlaybackStatusChanged), (object)new object[2] { _playbackWrapper.State, _playbackWrapper.EndOfMedia });
	}

	private void OnTransportStatusChanged(object sender, EventArgs e)
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0052: Expected O, but got Unknown
		Application.DeferredInvoke(new DeferredInvokeHandler(DeferredTransportStatusChanged), (object)new object[3] { _playbackWrapper.TransportState, _playbackWrapper.EndOfMedia, _playbackWrapper.CanSeek });
	}

	private void OnTransportPositionChanged(object sender, EventArgs e)
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Expected O, but got Unknown
		Application.DeferredInvoke(new DeferredInvokeHandler(DeferredTransportPositionChanged), (object)_playbackWrapper.Position);
	}

	private void OnUriSet(object sender, EventArgs e)
	{
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Expected O, but got Unknown
		object[] array = new object[2] { _playbackWrapper.CurrentUri, _playbackWrapper.CurrentUriID };
		Application.DeferredInvoke(new DeferredInvokeHandler(DeferredUriSet), (object)array);
	}

	private void OnAlertSent(Announcement alert)
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_0012: Expected O, but got Unknown
		Application.DeferredInvoke(new DeferredInvokeHandler(DeferredAlertHandler), (object)alert);
	}

	private void OnPlayerPropertyChanged(object sender, PlayerPropertyChangedEventArgs e)
	{
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_0029: Expected O, but got Unknown
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_0053: Expected O, but got Unknown
		//IL_006d: Unknown result type (might be due to invalid IL or missing references)
		//IL_007d: Expected O, but got Unknown
		if (e.Key == "presentationinfo")
		{
			Application.DeferredInvoke(new DeferredInvokeHandler(DeferredPresentationInfoChangedHandler), e.Value);
		}
		else if (e.Key == "volumeinfo")
		{
			Application.DeferredInvoke(new DeferredInvokeHandler(DeferredVolumeInfoChangedHandler), e.Value);
		}
		else if (e.Key == "canchangevideorate")
		{
			Application.DeferredInvoke(new DeferredInvokeHandler(DeferredCanChangeVideoRateHandler), e.Value);
		}
	}

	private void OnVolumeControlChanged(object sender, PropertyChangedEventArgs e)
	{
		if (e.PropertyName == "Value")
		{
			_muted.Value = false;
			_playbackWrapper.Mute = false;
			_playbackWrapper.Volume = (int)_volume.Value;
			SQMLog.Log((SQMDataId)137, 1);
			PersistSettings();
		}
	}

	private void OnVideoDetailsChanged(object sender, EventArgs args)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		//IL_0059: Unknown result type (might be due to invalid IL or missing references)
		//IL_0068: Unknown result type (might be due to invalid IL or missing references)
		//IL_006d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0077: Unknown result type (might be due to invalid IL or missing references)
		//IL_007d: Expected O, but got Unknown
		WindowPosition displayPosition = _videoStream.DisplayPosition;
		int x = ((WindowPosition)(ref displayPosition)).X;
		WindowPosition displayPosition2 = _videoStream.DisplayPosition;
		int y = ((WindowPosition)(ref displayPosition2)).Y;
		WindowPosition displayPosition3 = _videoStream.DisplayPosition;
		int x2 = ((WindowPosition)(ref displayPosition3)).X;
		WindowSize displaySize = _videoStream.DisplaySize;
		int num = x2 + ((WindowSize)(ref displaySize)).Width;
		WindowPosition displayPosition4 = _videoStream.DisplayPosition;
		int y2 = ((WindowPosition)(ref displayPosition4)).Y;
		WindowSize displaySize2 = _videoStream.DisplaySize;
		VideoWindow videoPosition = new VideoWindow(x, y, num, y2 + ((WindowSize)(ref displaySize2)).Height);
		_playbackWrapper.VideoPosition = videoPosition;
		_playbackWrapper.ShowGDIVideo = _videoStream.DisplayVisibility;
	}

	private void DeferredPlaybackStatusChanged(object obj)
	{
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Invalid comparison between Unknown and I4
		//IL_00b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b2: Invalid comparison between Unknown and I4
		//IL_00d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d2: Invalid comparison between Unknown and I4
		//IL_00e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e4: Unknown result type (might be due to invalid IL or missing references)
		if (((ModelItem)this).IsDisposed)
		{
			return;
		}
		object[] array = (object[])obj;
		MCPlayerState val = (MCPlayerState)array[0];
		bool endOfMedia = (bool)array[1];
		bool flag = false;
		if ((int)val == 3)
		{
			if (Playing && _lastKnownPlaybackTrack != null)
			{
				_lastKnownPlaybackTrack.OnEndPlayback(endOfMedia);
				_lastKnownPlaybackTrack = _lastKnownPreparedTrack;
				_lastKnownPreparedTrack = null;
				if (_lastKnownPlaybackTrack != null)
				{
					_lastKnownPlaybackTrack.OnBeginPlayback(_playbackWrapper);
				}
			}
			if (_playbackWrapper.Duration < 0)
			{
				CurrentTrackDuration = 0f;
			}
			else
			{
				CurrentTrackDuration = (float)((double)_playbackWrapper.Duration / 10000000.0);
			}
		}
		else if ((int)val == 2)
		{
			_rewinding.Value = false;
			_fastforwarding.Value = false;
			flag = true;
		}
		else if ((int)val == 1 && Playing)
		{
			SetUriOnPlayer();
		}
		_lastKnownPlayerState = val;
		if (_opening != flag)
		{
			Opening = flag;
		}
	}

	private void ShowNotification()
	{
		if (_nowPlayingNotification == null)
		{
			_nowPlayingNotification = new NowPlayingNotification();
			NotificationArea.Instance.Add(_nowPlayingNotification);
		}
		else
		{
			NotificationArea.Instance.ForceToFront(_nowPlayingNotification);
		}
	}

	private void HideNotification()
	{
		if (_nowPlayingNotification != null)
		{
			NotificationArea.Instance.Remove(_nowPlayingNotification);
			_nowPlayingNotification = null;
		}
	}

	public void ShowPreparingNotification()
	{
		HideNotification();
		HidePreparingNotification(restoreNowPlayingNotification: false);
		NotificationArea.Instance.Add(new PreparingPlayNotification());
	}

	public void HidePreparingNotification()
	{
		HidePreparingNotification(restoreNowPlayingNotification: true);
	}

	private void HidePreparingNotification(bool restoreNowPlayingNotification)
	{
		NotificationArea.Instance.RemoveAll(NotificationTask.PreparingPlay, NotificationState.Normal);
		if (restoreNowPlayingNotification && _playlistCurrent != null)
		{
			ShowNotification();
		}
	}

	private void DeferredTransportStatusChanged(object obj)
	{
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Invalid comparison between Unknown and I4
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		//IL_005f: Invalid comparison between Unknown and I4
		//IL_01a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0193: Unknown result type (might be due to invalid IL or missing references)
		//IL_0195: Invalid comparison between Unknown and I4
		if (((ModelItem)this).IsDisposed)
		{
			return;
		}
		object[] array = (object[])obj;
		MCTransportState val = (MCTransportState)array[0];
		bool flag = (bool)array[1];
		IsSeekEnabled = (bool)array[2];
		if ((int)val == 4)
		{
			Buffering = true;
			UpdatePropertiesAndCommands();
		}
		else
		{
			if (Buffering)
			{
				Buffering = false;
				UpdatePropertiesAndCommands();
			}
			if ((int)val == 1)
			{
				bool flag2 = _playlistCurrent != null && _playlistCurrent.Count == 1 && _repeating.Value && flag;
				if (_lastKnownPlaybackTrack != null)
				{
					_lastKnownPlaybackTrack.OnEndPlayback(flag);
					if (!flag2)
					{
						_lastKnownPlaybackTrack = null;
					}
				}
				if (_playlistPending != null)
				{
					if (_playlistPending.TrackList != null && ((ListDataSet)_playlistPending.TrackList).Count > 0)
					{
						PlayPendingList();
					}
					else
					{
						SetPlayerState(PlayerState.Stopped);
						_playlistPending.PlayWhenReady = true;
					}
				}
				else if (_lastKnownPreparedTrack != null)
				{
					_lastKnownPlaybackTrack = _lastKnownPreparedTrack;
					_lastKnownPlaybackTrack.OnBeginPlayback(_playbackWrapper);
					if (Playing)
					{
						_playbackWrapper.Play();
					}
					_lastKnownPreparedTrack = null;
				}
				else if (flag2 && _lastKnownPlaybackTrack != null)
				{
					_lastKnownPlaybackTrack.OnBeginPlayback(_playbackWrapper);
					_playbackWrapper.SeekToAbsolutePosition(0L);
					_playbackWrapper.Play();
				}
				else
				{
					if (_playlistCurrent != null)
					{
						_playlistCurrent.ResetForReplay();
					}
					SetPlayerState(PlayerState.Stopped);
					_playbackWrapper.Close();
				}
			}
			else if ((int)val == 2)
			{
				PerfTrace.TraceUICollectionEvent(UICollectionEvent.PlayRequestComplete, "");
			}
		}
		_lastKnownTransportState = val;
	}

	private void DeferredTransportPositionChanged(object obj)
	{
		if (((ModelItem)this).IsDisposed)
		{
			return;
		}
		_lastKnownPosition = (long)obj;
		CurrentTrackPosition = (float)((double)_lastKnownPosition / 10000000.0);
		if (_lastKnownPlaybackTrack != null)
		{
			if (_lastKnownPosition > 0)
			{
				_consecutiveErrors = 0;
			}
			_lastKnownPlaybackTrack.OnPositionChanged(_lastKnownPosition);
		}
	}

	private void DeferredUriSet(object obj)
	{
		if (((ModelItem)this).IsDisposed)
		{
			return;
		}
		object[] array = (object[])obj;
		_ = (string)array[0];
		int num = (int)array[1];
		_lastKnownPreparedTrack = null;
		int i;
		for (i = 0; i < _tracksSubmittedToPlayer.Count; i++)
		{
			PlaybackTrack playbackTrack = _tracksSubmittedToPlayer[i];
			if (playbackTrack.PlaybackID == num)
			{
				_lastKnownPreparedTrack = playbackTrack;
				i++;
				break;
			}
		}
		_tracksSubmittedToPlayer.RemoveRange(0, i);
		if (_lastKnownPreparedTrack != null && _tracksSubmittedToPlayer.Count == 0)
		{
			if (_playlistCurrent != null)
			{
				_playlistCurrent.SyncCurrentTrackTo(_lastKnownPreparedTrack);
			}
			UpdateNextTrack();
		}
	}

	private void DeferredAlertHandler(object obj)
	{
		//IL_00c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cb: Unknown result type (might be due to invalid IL or missing references)
		if (((ModelItem)this).IsDisposed)
		{
			return;
		}
		Announcement val = (Announcement)((obj is Announcement) ? obj : null);
		if (val == null)
		{
			return;
		}
		PlaybackTrack playbackTrack = null;
		if (CurrentTrack != null && CurrentTrack.PlaybackID == val.PlaybackID)
		{
			playbackTrack = CurrentTrack;
		}
		else
		{
			for (int num = _tracksSubmittedToPlayer.Count - 1; num >= 0; num--)
			{
				if (_tracksSubmittedToPlayer[num].PlaybackID == val.PlaybackID)
				{
					playbackTrack = _tracksSubmittedToPlayer[num];
					break;
				}
			}
		}
		if (playbackTrack != null)
		{
			_consecutiveErrors++;
			_errors[playbackTrack] = val.HResult;
			if (_playlistCurrent != null)
			{
				_playlistCurrent.SyncCurrentTrackTo(playbackTrack);
			}
			((ModelItem)this).FirePropertyChanged("ErrorCount");
		}
		bool flag = HRESULT.op_Implicit(val.HResult) == HRESULT._NS_E_CD_BUSY;
		if (_consecutiveErrors < 5 && !flag && Playing && _playlistCurrent != null && _playlistCurrent.CanAdvance)
		{
			_playlistCurrent.Advance();
			_playbackWrapper.Close();
			return;
		}
		SetPlayerState(PlayerState.Stopped);
		_playbackWrapper.Close();
		if (_lastKnownPlaybackTrack != null)
		{
			_lastKnownPlaybackTrack.OnEndPlayback(endOfMedia: false);
			_lastKnownPlaybackTrack = null;
		}
		if (!flag && ShowErrors)
		{
			ErrorDialogInfo.Show(val.HResult, Shell.LoadString(StringId.IDS_PLAYBACK_ERROR));
		}
	}

	private void DeferredPresentationInfoChangedHandler(object obj)
	{
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_0017: Expected O, but got Unknown
		if (!((ModelItem)this).IsDisposed && _videoStream != null)
		{
			PresentationInfo val = (PresentationInfo)obj;
			_videoStream.ContentWidth = val.ContentWidth;
			_videoStream.ContentHeight = val.ContentHeight;
			_videoStream.ContentAspectWidth = val.ContentAspectWidth;
			_videoStream.ContentAspectHeight = val.ContentAspectHeight;
			_videoStream.ContentOverscanPercent = (val.NeedOverscan ? 0.1f : 0f);
			((ModelItem)this).FirePropertyChanged("VideoStream");
		}
	}

	private void DeferredVolumeInfoChangedHandler(object obj)
	{
		_volume.Value = _playbackWrapper.Volume;
		((ModelItem)this).FirePropertyChanged("Volume");
		_muted.Value = _playbackWrapper.Mute;
		((ModelItem)this).FirePropertyChanged("Mute");
	}

	private void DeferredCanChangeVideoRateHandler(object obj)
	{
		if (_currentTrack != null && _currentTrack.IsVideo)
		{
			_forward.Available = _playbackWrapper.CanChangeVideoRate;
		}
	}

	private void SetUriOnPlayer()
	{
		PlaybackTrack playbackTrack = null;
		if (_playlistCurrent != null)
		{
			playbackTrack = _playlistCurrent.CurrentTrack;
		}
		PlaybackTrack nextTrack = null;
		if (_playlistCurrent != null && _playlistCurrent.Count > 1)
		{
			nextTrack = _playlistCurrent.NextTrack;
		}
		if (playbackTrack != null)
		{
			SetUrisOnPlayerAsync(playbackTrack, nextTrack);
			return;
		}
		SetPlayerState(PlayerState.Stopped);
		_playbackWrapper.Close();
		UpdatePropertiesAndCommands();
	}

	private void SetNextUriOnPlayer()
	{
		PlaybackTrack playbackTrack = null;
		if (_playlistCurrent != null)
		{
			playbackTrack = _playlistCurrent.NextTrack;
		}
		if (playbackTrack != null && _playlistCurrent.Count > 1)
		{
			SetUrisOnPlayerAsync(null, playbackTrack);
		}
		else
		{
			_playbackWrapper.CancelNext();
		}
	}

	private void SetUrisOnPlayerAsync(PlaybackTrack track, PlaybackTrack nextTrack)
	{
		int myID = ++_lastKnownSetUriCallID;
		ThreadPool.QueueUserWorkItem(delegate
		{
			//IL_0026: Unknown result type (might be due to invalid IL or missing references)
			//IL_002b: Unknown result type (might be due to invalid IL or missing references)
			//IL_00bd: Unknown result type (might be due to invalid IL or missing references)
			//IL_00c2: Unknown result type (might be due to invalid IL or missing references)
			//IL_0066: Unknown result type (might be due to invalid IL or missing references)
			//IL_006c: Expected O, but got Unknown
			//IL_0057: Unknown result type (might be due to invalid IL or missing references)
			//IL_005d: Expected O, but got Unknown
			//IL_00f3: Unknown result type (might be due to invalid IL or missing references)
			//IL_00fa: Expected O, but got Unknown
			if (track != null)
			{
				DeferredInvokeHandler val = null;
				string trackUri;
				HRESULT uRI = track.GetURI(out trackUri);
				if (string.IsNullOrEmpty(trackUri))
				{
					trackUri = ".:* INVALID URI *:.";
				}
				if (((HRESULT)(ref uRI)).IsSuccess)
				{
					if (val == null)
					{
						val = (DeferredInvokeHandler)delegate
						{
							if (!((ModelItem)this).IsDisposed && myID == _lastKnownSetUriCallID)
							{
								_playbackWrapper.SetUri(trackUri, 0L, track.PlaybackID);
								ReportStreamingAction(PlayerState.Stopped);
								_tracksSubmittedToPlayer.Remove(track);
								_tracksSubmittedToPlayer.Add(track);
								if (nextTrack == null)
								{
									_playbackWrapper.CancelNext();
									UpdatePropertiesAndCommands();
								}
							}
						};
					}
					Application.DeferredInvoke(val, (object)null);
				}
				else
				{
					Announcement val2 = new Announcement();
					val2.HResult = ((HRESULT)(ref uRI)).Int;
					val2.PlaybackID = track.PlaybackID;
					OnAlertSent(val2);
				}
			}
			if (nextTrack != null)
			{
				DeferredInvokeHandler val3 = null;
				string nextTrackUri;
				HRESULT uRI2 = nextTrack.GetURI(out nextTrackUri);
				if (string.IsNullOrEmpty(nextTrackUri))
				{
					nextTrackUri = ".:* INVALID URI *:.";
				}
				if (((HRESULT)(ref uRI2)).IsSuccess)
				{
					if (val3 == null)
					{
						val3 = (DeferredInvokeHandler)delegate
						{
							if (!((ModelItem)this).IsDisposed && myID == _lastKnownSetUriCallID)
							{
								_playbackWrapper.SetNextUri(nextTrackUri, 0L, nextTrack.PlaybackID);
								_tracksSubmittedToPlayer.Remove(nextTrack);
								_tracksSubmittedToPlayer.Add(nextTrack);
								UpdatePropertiesAndCommands();
							}
						};
					}
					Application.DeferredInvoke(val3, (object)null);
				}
			}
		}, null);
	}

	private void UpdateNextTrack()
	{
		SetNextUriOnPlayer();
		UpdatePropertiesAndCommands();
	}

	private bool AreContextsCompatible(PlaybackContext contextCurrent, PlaybackContext contextNext)
	{
		if (contextCurrent == PlaybackContext.None)
		{
			return true;
		}
		return contextNext == contextCurrent;
	}

	public bool IsCurrentPlaylistContextCompatible(PlaybackContext context)
	{
		bool result = true;
		if (_playlistCurrent != null)
		{
			result = AreContextsCompatible(context, _playlistCurrent.PlaybackContext);
		}
		return result;
	}

	private void UpdatePropertiesAndCommands()
	{
		bool flag = _playerState == PlayerState.Playing;
		ArrayListDataSet val;
		bool flag2;
		bool flag3;
		PlaybackTrack playbackTrack;
		int num;
		bool flag4;
		if (_playlistCurrent != null)
		{
			val = _playlistCurrent.TrackList;
			flag2 = true;
			flag3 = IsCurrentPlaylistContextCompatible(_pagePlaybackContext);
			playbackTrack = _playlistCurrent.CurrentTrack;
			num = _playlistCurrent.ListIndexOfCurrentTrack;
			flag4 = _playlistCurrent.QuickMixSession == null;
		}
		else
		{
			flag2 = false;
			val = null;
			flag3 = true;
			playbackTrack = null;
			num = -1;
			PlayingVideo = false;
			flag4 = true;
		}
		if (val != _currentPlaylist)
		{
			_currentPlaylist = val;
			((ModelItem)this).FirePropertyChanged("CurrentPlaylist");
		}
		if (flag2 != _hasPlaylist)
		{
			_hasPlaylist = flag2;
			((ModelItem)this).FirePropertyChanged("HasPlaylist");
		}
		if (flag4 != _playlistSupportsShuffle)
		{
			_playlistSupportsShuffle = flag4;
			((ModelItem)this).FirePropertyChanged("PlaylistSupportsShuffle");
		}
		if (flag != _isPlaying)
		{
			_isPlaying = flag;
			((ModelItem)this).FirePropertyChanged("Playing");
			if (_isPlaying)
			{
				HasPlayed = true;
				_currentPlayStartTime = DateTime.Now;
			}
			else if (_currentPlayStartTime != DateTime.MinValue)
			{
				TimeSpan timeSpan = DateTime.Now.Subtract(_currentPlayStartTime);
				Telemetry.Instance.ReportPlaybackTime((int)timeSpan.TotalSeconds);
				_currentPlayStartTime = DateTime.MinValue;
			}
		}
		if (num != _currentTrackIndex)
		{
			if (_currentPlayStartTime != DateTime.MinValue)
			{
				TimeSpan timeSpan2 = DateTime.Now.Subtract(_currentPlayStartTime);
				Telemetry.Instance.ReportPlaybackTime((int)timeSpan2.TotalSeconds);
				_currentPlayStartTime = DateTime.Now;
			}
			_currentTrackIndex = num;
			((ModelItem)this).FirePropertyChanged("CurrentTrackIndex");
		}
		if (!object.ReferenceEquals(playbackTrack, _currentTrack))
		{
			if (_currentTrack != null)
			{
				_currentTrack.RatingChanged.Invoked -= _currentTrackRatingChangedEventHandler;
			}
			_currentTrack = playbackTrack;
			if (_currentTrack != null)
			{
				_currentTrack.RatingChanged.Invoked += _currentTrackRatingChangedEventHandler;
			}
			((ModelItem)this).FirePropertyChanged("CurrentTrack");
			OnCurrentTrackRatingChanged(this, null);
			CurrentTrackDownloadProgress = 0f;
			if (_currentTrack != null)
			{
				ShowNotification();
			}
			else
			{
				HideNotification();
			}
			ZoomScaleFactor = 0;
		}
		if (flag3 != _isContextCompatible)
		{
			_isContextCompatible = flag3;
			((ModelItem)this).FirePropertyChanged("IsPlaybackContextCompatible");
		}
		UpdateAvailabilityOfCommands();
		if (Playing && CurrentTrack != null && CurrentTrack.IsVideo && CurrentTrack.IsStreaming)
		{
			_isStreamingTimeoutTimer.Enabled = false;
			IsStreamingVideo = true;
			SupressDownloads = true;
			return;
		}
		if (SupressDownloads)
		{
			_isStreamingTimeoutTimer.Enabled = true;
		}
		IsStreamingVideo = false;
	}

	private void UpdateAvailabilityOfCommands()
	{
		bool playing = Playing;
		bool flag = _playlistCurrent != null;
		_play.Available = !playing && flag && !Buffering;
		_pause.Available = playing && !Buffering;
		_stop.Available = flag;
		if (_playerState == PlayerState.Stopped || Buffering)
		{
			_forward.Available = false;
			_back.Available = false;
		}
		else if (_playerState == PlayerState.Playing)
		{
			if (_currentTrack == null)
			{
				_forward.Available = false;
				_back.Available = false;
			}
			else
			{
				if (_currentTrack.IsVideo)
				{
					_forward.Available = _playbackWrapper.CanChangeVideoRate;
				}
				else
				{
					_forward.Available = true;
				}
				_back.Available = true;
			}
		}
		else
		{
			if (_currentTrack != null)
			{
				_forward.Available = _playlistCurrent != null && !_currentTrack.IsVideo;
			}
			else
			{
				_forward.Available = _playlistCurrent != null;
			}
			_back.Available = _playlistCurrent != null && _playlistCurrent.CanRetreat;
		}
		UpdateTaskbarPlayer();
	}

	public static string FormatDuration(float seconds, bool prefixWithNegative)
	{
		TimeSpan time = new TimeSpan(0, 0, (int)seconds);
		return Shell.TimeSpanToString(time, prefixWithNegative);
	}

	public static string FormatDuration(float seconds)
	{
		return FormatDuration(seconds, prefixWithNegative: false);
	}

	[Conditional("DEBUG_TRANSPORT")]
	private static void _DEBUG_Trace(string message, params object[] args)
	{
	}

	[Conditional("DEBUG_TRANSPORT_PROPERTIES")]
	private static void _DEBUG_TracePropChange(string name, object arg)
	{
	}
}
