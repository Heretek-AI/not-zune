using System;
using System.Collections;
using System.Collections.Specialized;
using System.ComponentModel;
using Microsoft.Iris;

namespace ZuneUI;

public class ZunePage : Page, IZunePage, IPage, INotifyPropertyChanged
{
	private enum Bits : uint
	{
		ShowBackArrow = 1u,
		ShowDeviceIcon = 2u,
		ShowPlaylistIcon = 4u,
		ShowCDIcon = 8u,
		ShowNowPlayingX = 0x10u,
		NotificationAreaVisible = 0x20u,
		AutoHideToolbars = 0x40u,
		ShowAppBackground = 0x80u,
		IsRootPage = 0x100u,
		ShouldHandleBack = 0x200u,
		ShowLogo = 0x400u,
		ShowPivots = 0x800u,
		ShowSearch = 0x1000u,
		ShowSettings = 0x2000u,
		TakeFocusOnNavigate = 0x4000u,
		ShowingVideoPreview = 0x8000u,
		ShowNowPlayingBackgroundOnIdle = 0x10000u,
		ShouldHandleEscape = 0x20000u,
		CanEnterCompactMode = 0x40000u,
		NoStackPage = 0x80000u,
		TransportControlsVisible = 0x100000u
	}

	private string _pageUI;

	private string _pageUIPath;

	private string _backgroundUI;

	private string _bottomBarUI;

	private string _overlayUI;

	private Hashtable _overlayState;

	private IDictionary _navigationArguments;

	private string _navigationCommand;

	private ICommandHandler _commandHandler;

	private ComputerIconState _showComputerIcon;

	private TransportControlStyle _transportStyle;

	private PlaybackContext _playbackContext;

	private Node _pivotPreference;

	private Command _releaseCommand;

	private Command _navigateAwayCommand;

	private Command _navigateToCommand;

	private object _temporaryPageState;

	private BitVector32 _bits;

	public string UI
	{
		get
		{
			return _pageUI;
		}
		set
		{
			if (_pageUI != value)
			{
				_pageUI = value;
				((ModelItem)this).FirePropertyChanged("UI");
			}
		}
	}

	public string UIPath
	{
		get
		{
			return _pageUIPath;
		}
		set
		{
			if (_pageUIPath != value)
			{
				_pageUIPath = value;
				((ModelItem)this).FirePropertyChanged("UIPath");
			}
		}
	}

	public string BackgroundUI
	{
		get
		{
			return _backgroundUI;
		}
		set
		{
			if (_backgroundUI != value)
			{
				_backgroundUI = value;
				((ModelItem)this).FirePropertyChanged("BackgroundUI");
			}
		}
	}

	public string BottomBarUI
	{
		get
		{
			return _bottomBarUI;
		}
		set
		{
			if (_bottomBarUI != value)
			{
				_bottomBarUI = value;
				((ModelItem)this).FirePropertyChanged("BottomBarUI");
			}
		}
	}

	public string OverlayUI
	{
		get
		{
			return _overlayUI;
		}
		set
		{
			if (_overlayUI != value)
			{
				_overlayUI = value;
				((ModelItem)this).FirePropertyChanged("OverlayUI");
			}
		}
	}

	public IDictionary NavigationArguments
	{
		get
		{
			return _navigationArguments;
		}
		set
		{
			_navigationArguments = value;
		}
	}

	public string NavigationCommand
	{
		get
		{
			return _navigationCommand;
		}
		set
		{
			_navigationCommand = value;
		}
	}

	public ICommandHandler CommandHandler
	{
		get
		{
			return _commandHandler;
		}
		set
		{
			_commandHandler = value;
		}
	}

	public bool ShowBackArrow
	{
		get
		{
			return GetBit(Bits.ShowBackArrow);
		}
		set
		{
			if (ChangeBit(Bits.ShowBackArrow, value))
			{
				((ModelItem)this).FirePropertyChanged("ShowBackArrow");
			}
		}
	}

	public bool ShowDeviceIcon
	{
		get
		{
			return GetBit(Bits.ShowDeviceIcon);
		}
		set
		{
			if (ChangeBit(Bits.ShowDeviceIcon, value))
			{
				((ModelItem)this).FirePropertyChanged("ShowDeviceIcon");
			}
		}
	}

	public bool ShowPlaylistIcon
	{
		get
		{
			return GetBit(Bits.ShowPlaylistIcon);
		}
		set
		{
			if (ChangeBit(Bits.ShowPlaylistIcon, value))
			{
				((ModelItem)this).FirePropertyChanged("ShowPlaylistIcon");
			}
		}
	}

	public bool ShowCDIcon
	{
		get
		{
			return GetBit(Bits.ShowCDIcon);
		}
		set
		{
			if (ChangeBit(Bits.ShowCDIcon, value))
			{
				((ModelItem)this).FirePropertyChanged("ShowCDIcon");
			}
		}
	}

	public bool ShowNowPlayingX
	{
		get
		{
			return GetBit(Bits.ShowNowPlayingX);
		}
		set
		{
			if (ChangeBit(Bits.ShowNowPlayingX, value))
			{
				((ModelItem)this).FirePropertyChanged("ShowNowPlayingX");
			}
		}
	}

	public bool ShowNowPlayingBackgroundOnIdle
	{
		get
		{
			return GetBit(Bits.ShowNowPlayingBackgroundOnIdle);
		}
		set
		{
			if (ChangeBit(Bits.ShowNowPlayingBackgroundOnIdle, value))
			{
				((ModelItem)this).FirePropertyChanged("ShowNowPlayingBackgroundOnIdle");
			}
		}
	}

	public bool CanEnterCompactMode
	{
		get
		{
			return GetBit(Bits.CanEnterCompactMode);
		}
		set
		{
			if (ChangeBit(Bits.CanEnterCompactMode, value))
			{
				((ModelItem)this).FirePropertyChanged("CanEnterCompactMode");
			}
		}
	}

	public bool NoStackPage
	{
		get
		{
			return GetBit(Bits.NoStackPage);
		}
		set
		{
			if (ChangeBit(Bits.NoStackPage, value))
			{
				((ModelItem)this).FirePropertyChanged("NoStackPage");
			}
		}
	}

	public bool NotificationAreaVisible
	{
		get
		{
			return GetBit(Bits.NotificationAreaVisible);
		}
		set
		{
			if (ChangeBit(Bits.NotificationAreaVisible, value))
			{
				((ModelItem)this).FirePropertyChanged("NotificationAreaVisible");
			}
		}
	}

	public bool TransportControlsVisible
	{
		get
		{
			return GetBit(Bits.TransportControlsVisible);
		}
		set
		{
			if (ChangeBit(Bits.TransportControlsVisible, value))
			{
				((ModelItem)this).FirePropertyChanged("TransportControlsVisible");
			}
		}
	}

	public bool AutoHideToolbars
	{
		get
		{
			return GetBit(Bits.AutoHideToolbars);
		}
		set
		{
			if (ChangeBit(Bits.AutoHideToolbars, value))
			{
				((ModelItem)this).FirePropertyChanged("AutoHideToolbars");
			}
		}
	}

	public bool ShowAppBackground
	{
		get
		{
			return GetBit(Bits.ShowAppBackground);
		}
		set
		{
			if (ChangeBit(Bits.ShowAppBackground, value))
			{
				((ModelItem)this).FirePropertyChanged("ShowAppBackground");
			}
		}
	}

	public ComputerIconState ShowComputerIcon
	{
		get
		{
			return _showComputerIcon;
		}
		set
		{
			if (_showComputerIcon != value)
			{
				_showComputerIcon = value;
				((ModelItem)this).FirePropertyChanged("ShowComputerIcon");
			}
		}
	}

	public bool ShowingVideoPreview
	{
		get
		{
			return GetBit(Bits.ShowingVideoPreview);
		}
		set
		{
			if (ChangeBit(Bits.ShowingVideoPreview, value))
			{
				((ModelItem)this).FirePropertyChanged("ShowingVideoPreview");
			}
		}
	}

	public bool ShowLogo
	{
		get
		{
			return GetBit(Bits.ShowLogo);
		}
		set
		{
			SetBit(Bits.ShowLogo, value);
		}
	}

	public bool ShowPivots
	{
		get
		{
			return GetBit(Bits.ShowPivots);
		}
		set
		{
			ChangeBit(Bits.ShowPivots, value);
		}
	}

	public bool ShowSearch
	{
		get
		{
			return GetBit(Bits.ShowSearch);
		}
		set
		{
			SetBit(Bits.ShowSearch, value);
		}
	}

	public bool ShowSettings
	{
		get
		{
			return GetBit(Bits.ShowSettings);
		}
		set
		{
			SetBit(Bits.ShowSettings, value);
		}
	}

	public bool TakeFocusOnNavigate
	{
		get
		{
			return GetBit(Bits.TakeFocusOnNavigate);
		}
		set
		{
			SetBit(Bits.TakeFocusOnNavigate, value);
		}
	}

	public TransportControlStyle TransportControlStyle
	{
		get
		{
			return _transportStyle;
		}
		set
		{
			if (_transportStyle != value)
			{
				_transportStyle = value;
				((ModelItem)this).FirePropertyChanged("TransportControlStyle");
			}
		}
	}

	public PlaybackContext PlaybackContext
	{
		get
		{
			return _playbackContext;
		}
		set
		{
			if (_playbackContext != value)
			{
				_playbackContext = value;
				((ModelItem)this).FirePropertyChanged("PlaybackContext");
			}
		}
	}

	public Node PivotPreference
	{
		get
		{
			return _pivotPreference;
		}
		set
		{
			_pivotPreference = value;
		}
	}

	public bool IsRootPage
	{
		get
		{
			return GetBit(Bits.IsRootPage);
		}
		set
		{
			SetBit(Bits.IsRootPage, value);
		}
	}

	public object TemporaryPageState
	{
		get
		{
			return _temporaryPageState;
		}
		set
		{
			if (_temporaryPageState != value)
			{
				_temporaryPageState = value;
				((ModelItem)this).FirePropertyChanged("TemporaryPageState");
			}
		}
	}

	public bool ShouldHandleBack
	{
		get
		{
			return GetBit(Bits.ShouldHandleBack);
		}
		set
		{
			if (ChangeBit(Bits.ShouldHandleBack, value))
			{
				((ModelItem)this).FirePropertyChanged("ShouldHandleBack");
			}
		}
	}

	public bool ShouldHandleEscape
	{
		get
		{
			return GetBit(Bits.ShouldHandleEscape);
		}
		set
		{
			if (ChangeBit(Bits.ShouldHandleEscape, value))
			{
				((ModelItem)this).FirePropertyChanged("ShouldHandleEscape");
			}
		}
	}

	public Command ReleaseCommand
	{
		get
		{
			return _releaseCommand;
		}
		set
		{
			if (_releaseCommand != value)
			{
				_releaseCommand = value;
				((ModelItem)this).FirePropertyChanged("ReleaseCommand");
			}
		}
	}

	public Command NavigateToCommand
	{
		get
		{
			return _navigateToCommand;
		}
		set
		{
			if (_navigateToCommand != value)
			{
				_navigateToCommand = value;
				((ModelItem)this).FirePropertyChanged("NavigateToCommand");
			}
		}
	}

	public Command NavigateAway
	{
		get
		{
			return _navigateAwayCommand;
		}
		set
		{
			if (_navigateAwayCommand != value)
			{
				_navigateAwayCommand = value;
				((ModelItem)this).FirePropertyChanged("NavigateAwayCommand");
			}
		}
	}

	public event EventHandler BackHandled;

	public event EventHandler EscapeHandled;

	public event EventHandler Refresh;

	public ZunePage()
	{
		SetBit(Bits.ShowDeviceIcon, value: true);
		SetBit(Bits.ShowPlaylistIcon, value: true);
		SetBit(Bits.ShowCDIcon, value: true);
		SetBit(Bits.ShowBackArrow, value: true);
		SetBit(Bits.NotificationAreaVisible, value: true);
		SetBit(Bits.TransportControlsVisible, value: true);
		SetBit(Bits.ShowLogo, value: true);
		SetBit(Bits.ShowPivots, value: true);
		SetBit(Bits.ShowSearch, value: true);
		SetBit(Bits.ShowSettings, value: true);
		SetBit(Bits.ShowAppBackground, value: true);
		SetBit(Bits.TakeFocusOnNavigate, value: true);
		SetBit(Bits.ShowNowPlayingBackgroundOnIdle, value: true);
		SetBit(Bits.CanEnterCompactMode, value: true);
		SetBit(Bits.NoStackPage, value: false);
		_showComputerIcon = ComputerIconState.Hide;
		_transportStyle = TransportControlStyle.Music;
		_playbackContext = PlaybackContext.None;
	}

	protected override void OnDispose(bool disposing)
	{
		if (disposing)
		{
			UI = null;
			BackgroundUI = null;
			BottomBarUI = null;
			OverlayUI = null;
		}
		((ModelItem)this).OnDispose(disposing);
	}

	public void SetOverlayState(string overlayKey, object state)
	{
		if (_overlayState == null)
		{
			_overlayState = new Hashtable(1);
		}
		_overlayState[overlayKey] = state;
	}

	public object GetOverlayState(string overlayKey)
	{
		if (_overlayState != null)
		{
			return _overlayState[overlayKey];
		}
		return null;
	}

	public virtual void InvokeSettings()
	{
		((Command)Shell.SettingsFrame.Settings).Invoke();
	}

	public virtual bool HandleBack()
	{
		if (ShouldHandleBack)
		{
			if (this.BackHandled != null)
			{
				this.BackHandled(this, EventArgs.Empty);
			}
			((ModelItem)this).FirePropertyChanged("BackHandled");
			return true;
		}
		return false;
	}

	public virtual bool HandleEscape()
	{
		if (ShouldHandleEscape)
		{
			if (this.EscapeHandled != null)
			{
				this.EscapeHandled(this, EventArgs.Empty);
			}
			((ModelItem)this).FirePropertyChanged("EscapeHandled");
			return true;
		}
		return false;
	}

	public virtual bool CanNavigateForwardTo(IZunePage destination)
	{
		return true;
	}

	public void RefreshPage()
	{
		if (this.Refresh != null)
		{
			this.Refresh(this, EventArgs.Empty);
		}
		((ModelItem)this).FirePropertyChanged("Refresh");
	}

	public override void Release()
	{
		if (_releaseCommand != null)
		{
			_releaseCommand.Invoke();
		}
		base.Release();
	}

	protected override void OnNavigatedToWorker()
	{
		if (_navigateToCommand != null)
		{
			_navigateToCommand.Invoke();
		}
		base.OnNavigatedToWorker();
		Telemetry.Instance.ReportNavigation(_pageUIPath, _navigationArguments);
	}

	protected override void OnNavigatedAwayWorker(IPage destination)
	{
		if (_navigateAwayCommand != null)
		{
			_navigateAwayCommand.Invoke();
		}
		TemporaryPageState = null;
		base.OnNavigatedAwayWorker(destination);
	}

	public override IPageState SaveAndRelease()
	{
		if (NoStackPage)
		{
			Release();
			return null;
		}
		return base.SaveAndRelease();
	}

	private bool GetBit(Bits lookupBit)
	{
		return _bits[(int)lookupBit];
	}

	private void SetBit(Bits changeBit, bool value)
	{
		_bits[(int)changeBit] = value;
	}

	private bool ChangeBit(Bits bit, bool value)
	{
		if (_bits[(int)bit] == value)
		{
			return false;
		}
		_bits[(int)bit] = value;
		return true;
	}
}
