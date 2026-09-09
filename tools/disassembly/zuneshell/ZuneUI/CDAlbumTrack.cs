using Microsoft.Iris;

namespace ZuneUI;

public class CDAlbumTrack : ModelItem
{
	private RipState _ripState = RipState.NotInLibrary;

	private bool _ripTrack;

	private int _percentComplete;

	private uint _trackIndex;

	private CDAlbumCommand _album;

	private int _ripErrorCode;

	public bool RipTrack
	{
		get
		{
			return _ripTrack;
		}
		set
		{
			if (_ripTrack != value)
			{
				_ripTrack = value;
				if (_ripTrack)
				{
					_album.AddTrackToRip(this);
				}
				else
				{
					_album.RemoveTrackToRip(this);
				}
				((ModelItem)this).FirePropertyChanged("RipTrack");
			}
		}
	}

	public RipState RipState
	{
		get
		{
			return _ripState;
		}
		set
		{
			if (_ripState != value)
			{
				_ripState = value;
				((ModelItem)this).FirePropertyChanged("RipState");
			}
		}
	}

	public int PercentComplete
	{
		get
		{
			return _percentComplete;
		}
		set
		{
			if (_percentComplete != value)
			{
				_percentComplete = value;
				((ModelItem)this).FirePropertyChanged("PercentComplete");
			}
		}
	}

	public int RipErrorCode
	{
		get
		{
			return _ripErrorCode;
		}
		internal set
		{
			if (_ripErrorCode != value)
			{
				_ripErrorCode = value;
				((ModelItem)this).FirePropertyChanged("RipErrorCode");
			}
		}
	}

	public CDAlbumCommand Album => _album;

	public uint TrackIndex => _trackIndex;

	internal CDAlbumTrack(CDAlbumCommand album, uint trackIndex)
	{
		_album = album;
		_trackIndex = trackIndex;
	}
}
