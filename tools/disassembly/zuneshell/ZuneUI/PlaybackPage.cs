using Microsoft.Iris;

namespace ZuneUI;

public abstract class PlaybackPage : ZunePage
{
	private bool _showingArtistBio;

	private TransportControlStyle _activeStyle;

	private bool _showMixOnEntry;

	private bool _exitOnPlaybackStopped;

	private int _initialPlaybackID;

	public TransportControlStyle ActiveTransportControlStyle
	{
		get
		{
			return _activeStyle;
		}
		set
		{
			if (_activeStyle != value)
			{
				_activeStyle = value;
				((ModelItem)this).FirePropertyChanged("ActiveTransportControlStyle");
			}
		}
	}

	public bool ShowingArtistBio
	{
		get
		{
			return _showingArtistBio;
		}
		set
		{
			if (_showingArtistBio != value)
			{
				_showingArtistBio = value;
				((ModelItem)this).FirePropertyChanged("ShowingArtistBio");
			}
		}
	}

	public bool ShowMixOnEntry
	{
		get
		{
			return _showMixOnEntry;
		}
		set
		{
			if (_showMixOnEntry != value)
			{
				_showMixOnEntry = value;
				((ModelItem)this).FirePropertyChanged("ShowMixOnEntry");
			}
		}
	}

	public bool ExitOnPlaybackStopped
	{
		get
		{
			return _exitOnPlaybackStopped;
		}
		set
		{
			if (_exitOnPlaybackStopped != value)
			{
				_exitOnPlaybackStopped = value;
				((ModelItem)this).FirePropertyChanged("ExitOnPlaybackStopped");
			}
		}
	}

	public int InitialPlaybackID => _initialPlaybackID;

	private static string LandUI => "res://ZuneShellResources!NowPlayingLand.uix#NowPlayingLand";

	public PlaybackPage()
	{
		base.UI = LandUI;
		base.ShowAppBackground = false;
		base.ShowBackArrow = true;
		base.ShowCDIcon = false;
		base.ShowDeviceIcon = false;
		base.ShowComputerIcon = ComputerIconState.Hide;
		base.ShowNowPlayingBackgroundOnIdle = false;
		base.ShowNowPlayingX = true;
		base.ShowPivots = false;
		base.ShowPlaylistIcon = false;
		base.ShowSearch = false;
		base.ShowSettings = false;
		base.ShowLogo = false;
		if (SingletonModelItem<TransportControls>.Instance.CurrentTrack != null)
		{
			_initialPlaybackID = SingletonModelItem<TransportControls>.Instance.CurrentTrack.PlaybackID;
		}
	}

	public override bool HandleEscape()
	{
		if (base.ShouldHandleEscape)
		{
			return base.HandleEscape();
		}
		ZuneShell.DefaultInstance.NavigateBack();
		return true;
	}

	public override bool CanNavigateForwardTo(IZunePage destination)
	{
		if (!(destination is CDLand))
		{
			return !(destination is Deviceland);
		}
		return false;
	}
}
