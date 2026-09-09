using System;
using System.ComponentModel;
using Microsoft.Zune.Playlist;

namespace ZuneUI;

public abstract class PlaylistFactory : INotifyPropertyChanged, IDisposable
{
	private bool _ready;

	protected readonly bool _navigateOnCreate;

	public bool Ready
	{
		get
		{
			return _ready;
		}
		protected set
		{
			if (_ready != value)
			{
				_ready = value;
				NotifyPropertyChanged("Ready");
			}
		}
	}

	public bool NavigateOnCreate => _navigateOnCreate;

	public event PropertyChangedEventHandler PropertyChanged;

	protected PlaylistFactory(bool navigateOnCreate)
	{
		_navigateOnCreate = navigateOnCreate;
	}

	public virtual void Dispose()
	{
	}

	public abstract string GetUniqueTitle();

	public abstract PlaylistResult CreatePlaylist(string title, CreatePlaylistOption option);

	protected void NotifyPropertyChanged(string propertyName)
	{
		if (this.PropertyChanged != null)
		{
			this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}
