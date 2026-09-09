using System;
using Microsoft.Iris;

namespace ZuneUI;

public class BurnSessionItem : ModelItem
{
	private BurnableCD _burnCD;

	private int _burnListIndex;

	private int _playlistContentId;

	private bool _burnComplete;

	private int _errorCode;

	private int _burnProgress;

	private bool _downloading;

	private int _downloadProgress;

	private bool _burnCanceled;

	private Guid _zuneMediaId;

	public int PlaylistContentId => _playlistContentId;

	public Guid ZuneMediaId => _zuneMediaId;

	public int ErrorCode
	{
		get
		{
			return _errorCode;
		}
		internal set
		{
			if (_errorCode != value)
			{
				_errorCode = value;
				((ModelItem)this).FirePropertyChanged("ErrorCode");
			}
		}
	}

	public int BurnProgress
	{
		get
		{
			return _burnProgress;
		}
		internal set
		{
			if (_burnProgress != value)
			{
				_burnProgress = value;
				((ModelItem)this).FirePropertyChanged("BurnProgress");
			}
		}
	}

	public bool BurnComplete
	{
		get
		{
			return _burnComplete;
		}
		internal set
		{
			if (_burnComplete != value)
			{
				_burnComplete = value;
				((ModelItem)this).FirePropertyChanged("BurnComplete");
			}
		}
	}

	public bool BurnCanceled
	{
		get
		{
			return _burnCanceled;
		}
		internal set
		{
			if (_burnCanceled != value)
			{
				_burnCanceled = value;
				((ModelItem)this).FirePropertyChanged("BurnCanceled");
			}
		}
	}

	public bool Downloading
	{
		get
		{
			return _downloading;
		}
		internal set
		{
			if (_downloading != value)
			{
				_downloading = value;
				((ModelItem)this).FirePropertyChanged("Downloading");
			}
		}
	}

	public int DownloadProgress
	{
		get
		{
			return _downloadProgress;
		}
		internal set
		{
			if (_downloadProgress != value)
			{
				_downloadProgress = value;
				((ModelItem)this).FirePropertyChanged("DownloadProgress");
			}
		}
	}

	internal int BurnListIndex => _burnListIndex;

	internal BurnSessionItem(BurnableCD burnCD, int burnListIndex, int playlistContentId, Guid zuneMediaId)
	{
		_burnCD = burnCD;
		_burnListIndex = burnListIndex;
		_playlistContentId = playlistContentId;
		_burnProgress = -1;
		_zuneMediaId = zuneMediaId;
	}
}
