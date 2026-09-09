using Microsoft.Iris;
using Microsoft.Zune.Configuration;
using Microsoft.Zune.Util;
using MicrosoftZuneLibrary;

namespace ZuneUI;

public class CDAlbumCommand : Node
{
	private CDAction _autoPlayAction;

	private int _libraryID = -1;

	private bool _insertedDuringSession;

	private string _TOC;

	private bool _isRipping;

	private int _ripCount;

	private CDAlbumTrack[] _trackList;

	private int _trackCount;

	private CDAccess _cdAccess;

	private ZuneLibraryCDDevice _device;

	private BurnableCD _burnCD;

	public int RipCount
	{
		get
		{
			return _ripCount;
		}
		private set
		{
			if (_ripCount != value)
			{
				_ripCount = value;
				if (_ripCount == 0)
				{
					IsRipping = false;
				}
				((ModelItem)this).FirePropertyChanged("RipCount");
			}
		}
	}

	public CDAction AutoPlayAction
	{
		get
		{
			return _autoPlayAction;
		}
		internal set
		{
			if (_autoPlayAction != value)
			{
				_autoPlayAction = value;
				((ModelItem)this).FirePropertyChanged("AutoPlayAction");
			}
		}
	}

	public int TrackCount
	{
		get
		{
			return _trackCount;
		}
		set
		{
			if (_trackCount == value)
			{
				return;
			}
			_trackCount = value;
			if (value == 0)
			{
				_trackList = null;
				RipCount = 0;
			}
			else
			{
				_trackList = new CDAlbumTrack[value];
				for (int i = 0; i < value; i++)
				{
					_trackList[i] = new CDAlbumTrack(this, (uint)i);
				}
			}
			((ModelItem)this).FirePropertyChanged("TrackCount");
		}
	}

	public bool InsertedDuringSession => _insertedDuringSession;

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

	public CDAlbumTrack[] TrackList => _trackList;

	public ZuneLibraryCDDevice CDDevice => _device;

	public string TOC => _TOC;

	public bool IsMediaLoaded
	{
		get
		{
			if (_device == null)
			{
				return false;
			}
			return _device.IsMediaLoaded;
		}
	}

	public bool CanErase
	{
		get
		{
			if (_device == null)
			{
				return false;
			}
			return _device.IsCDRW;
		}
	}

	public bool CanWrite
	{
		get
		{
			if (_device == null)
			{
				return false;
			}
			return _device.IsBlank;
		}
	}

	public BurnableCD BurnCD
	{
		get
		{
			if (_burnCD == null && _device != null)
			{
				_burnCD = new BurnableCD(_cdAccess, _device);
			}
			return _burnCD;
		}
	}

	public int LibraryID
	{
		get
		{
			return _libraryID;
		}
		set
		{
			if (_libraryID != value && value != -1)
			{
				_libraryID = value;
				TrackCount = 0;
				((ModelItem)this).FirePropertyChanged("LibraryID");
			}
		}
	}

	public CDAlbumCommand(Experience owner, StringId id)
		: base(owner, id, null, (SQMDataId)129)
	{
	}

	public CDAlbumCommand(Experience owner, CDAccess cdAccess, ZuneLibraryCDDevice device, bool insertedDuringSession)
		: base(owner, null, (SQMDataId)129)
	{
		_cdAccess = cdAccess;
		_device = device;
		_insertedDuringSession = insertedDuringSession;
		_TOC = device.TOC;
	}

	protected override void OnDispose(bool fDisposing)
	{
		((ModelItem)this).OnDispose(fDisposing);
		if (fDisposing)
		{
			if (_burnCD != null)
			{
				((ModelItem)_burnCD).Dispose();
				_burnCD = null;
			}
			if (_device != null)
			{
				_device.Dispose();
				_device = null;
			}
		}
	}

	public void ClearAutoPlayAction()
	{
		_autoPlayAction = CDAction.None;
	}

	public CDAlbumTrack GetTrack(int trackIndex)
	{
		if (_trackList == null)
		{
			return null;
		}
		return _trackList[trackIndex];
	}

	public void ToggleRipAll()
	{
		int trackCount = TrackCount;
		bool ripTrack = RipCount < trackCount;
		for (int i = 0; i < trackCount; i++)
		{
			CDAlbumTrack cDAlbumTrack = TrackList[i];
			if (cDAlbumTrack.RipState != RipState.InProgress)
			{
				cDAlbumTrack.RipTrack = ripTrack;
			}
		}
	}

	internal void AddTrackToRip(CDAlbumTrack track)
	{
		RipCount++;
		if (IsRipping)
		{
			if (_cdAccess.Recorder.AsyncAddRecordingRequest(CDDevice, track.TrackIndex) == 0)
			{
				track.RipState = RipState.Pending;
			}
			_cdAccess.StartRip(1);
		}
	}

	internal void RemoveTrackToRip(CDAlbumTrack track)
	{
		RipCount--;
		if (IsRipping && track.RipState == RipState.Pending)
		{
			if (_cdAccess.Recorder.AsyncRemoveRecordingRequest(CDDevice, track.TrackIndex) == 0)
			{
				track.RipState = RipState.NotInLibrary;
			}
			_cdAccess.StopRip(1);
		}
	}

	public void StartRip()
	{
		if (IsRipping || RipCount <= 0)
		{
			return;
		}
		ZuneLibraryCDDevice cDDevice = CDDevice;
		if (_cdAccess.Recorder == null || cDDevice == null)
		{
			return;
		}
		int num = 0;
		CDAlbumTrack[] trackList = TrackList;
		foreach (CDAlbumTrack cDAlbumTrack in trackList)
		{
			if (cDAlbumTrack.RipTrack)
			{
				num++;
				if (_cdAccess.Recorder.AsyncAddRecordingRequest(cDDevice, cDAlbumTrack.TrackIndex) == 0)
				{
					cDAlbumTrack.RipState = RipState.Pending;
				}
			}
		}
		_cdAccess.StartRip(num);
		IsRipping = true;
		SQMLog.LogToStream((SQMDataId)91, (uint)ClientConfiguration.Recorder.RecordMode);
	}

	public void StopRip()
	{
		int num = 0;
		for (int num2 = TrackList.Length - 1; num2 >= 0; num2--)
		{
			CDAlbumTrack cDAlbumTrack = TrackList[num2];
			if (cDAlbumTrack.RipTrack)
			{
				num++;
				if (_cdAccess.Recorder.AsyncRemoveRecordingRequest(CDDevice, cDAlbumTrack.TrackIndex) == 0)
				{
					cDAlbumTrack.RipState = RipState.Incomplete;
				}
			}
		}
		_cdAccess.StopRip(num);
		IsRipping = false;
	}

	protected override void Execute(Shell shell)
	{
		shell.NavigateToPage(new CDLand(this));
	}
}
