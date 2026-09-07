using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Avalonia.Threading;
using CommunityToolkit.Mvvm.Input;
using NotZune.Application.Events;
using NotZune.Application.Interfaces;
using NotZune.Application.Services;
using NotZune.Domain.Enums;
using NotZune.Domain.Models;
using NotZune.Infrastructure.Audio;

namespace NotZune.UI.ViewModels;

public enum NavigationPivot
{
    Quickplay,
    Collection,
    NowPlaying,
    Device,
    Settings,
    Social
}

public class MainShellViewModel : ViewModelBase
{
    private readonly IPlayerCoordinator _playerCoordinator;
    private readonly IMediaLibraryService _libraryService;
    private readonly IDeviceSyncService _deviceSyncService;
    private readonly ISoundEffectService? _soundEffectService;
    private readonly IUserStatsService? _userStatsService;

    private NavigationPivot _activePivot = NavigationPivot.Quickplay;
    private ViewModelBase _currentView;

    private bool _isCompactMode;
    private string? _selectedBackgroundArt = "avares://NotZune.UI/Assets/Zune/Backgrounds/USERBACKGROUND-ART-536X196-10.JPG";

    private int _equalizerFrame = 1;
    private readonly DispatcherTimer? _equalizerTimer;
    private string _nowPlayingIconSource = "avares://NotZune.UI/Assets/Zune/Transport/ICON.NOWPLAYING.ENTER.PNG";

    public QuickplayViewModel QuickplayVM { get; }
    public CollectionViewModel CollectionVM { get; }
    public NowPlayingViewModel NowPlayingVM { get; }
    public DeviceViewModel DeviceVM { get; }
    public SettingsViewModel SettingsVM { get; }
    public ZuneCardViewModel ZuneCardVM { get; }

    public event EventHandler<bool>? CompactModeChanged;

    public bool IsCompactMode
    {
        get => _isCompactMode;
        set
        {
            if (SetProperty(ref _isCompactMode, value))
            {
                CompactModeChanged?.Invoke(this, value);
            }
        }
    }

    public string? SelectedBackgroundArt
    {
        get => _selectedBackgroundArt;
        set
        {
            if (SetProperty(ref _selectedBackgroundArt, value))
            {
                OnPropertyChanged(nameof(HasBackgroundArt));
            }
        }
    }

    public bool HasBackgroundArt => !string.IsNullOrEmpty(SelectedBackgroundArt);

    public string NowPlayingIconSource
    {
        get => _nowPlayingIconSource;
        private set => SetProperty(ref _nowPlayingIconSource, value);
    }

    public NavigationPivot ActivePivot
    {
        get => _activePivot;
        set
        {
            if (SetProperty(ref _activePivot, value))
            {
                OnPropertyChanged(nameof(IsQuickplayActive));
                OnPropertyChanged(nameof(IsCollectionActive));
                OnPropertyChanged(nameof(IsNowPlayingActive));
                OnPropertyChanged(nameof(IsDeviceActive));
                OnPropertyChanged(nameof(IsSettingsActive));
                OnPropertyChanged(nameof(IsSocialActive));

                CurrentView = _activePivot switch
                {
                    NavigationPivot.Quickplay => QuickplayVM,
                    NavigationPivot.Collection => CollectionVM,
                    NavigationPivot.NowPlaying => NowPlayingVM,
                    NavigationPivot.Device => DeviceVM,
                    NavigationPivot.Settings => SettingsVM,
                    NavigationPivot.Social => ZuneCardVM,
                    _ => QuickplayVM
                };

                if (_activePivot == NavigationPivot.Collection)
                {
                    _ = CollectionVM.RefreshDataAsync();
                }
                else if (_activePivot == NavigationPivot.Quickplay)
                {
                    _ = QuickplayVM.LoadInitialDataAsync();
                }
                else if (_activePivot == NavigationPivot.Social)
                {
                    _ = ZuneCardVM.LoadStatsAsync();
                }
            }
        }
    }

    public bool IsQuickplayActive => _activePivot == NavigationPivot.Quickplay;
    public bool IsCollectionActive => _activePivot == NavigationPivot.Collection;
    public bool IsNowPlayingActive => _activePivot == NavigationPivot.NowPlaying;
    public bool IsDeviceActive => _activePivot == NavigationPivot.Device;
    public bool IsSettingsActive => _activePivot == NavigationPivot.Settings;
    public bool IsSocialActive => _activePivot == NavigationPivot.Social;

    public ViewModelBase CurrentView
    {
        get => _currentView;
        private set => SetProperty(ref _currentView, value);
    }

    // Playback HUD properties
    public Track? CurrentTrack => _playerCoordinator.CurrentTrack;
    public PlaybackState State => _playerCoordinator.State;
    public bool IsPlaying => State == PlaybackState.Playing;
    public string PlayPauseIcon => IsPlaying ? "⏸" : "▶";
    public TimeSpan CurrentPosition => _playerCoordinator.CurrentPosition;
    public TimeSpan Duration => _playerCoordinator.Duration;

    public double ProgressPercentage
    {
        get
        {
            if (Duration.TotalSeconds <= 0) return 0.0;
            return Math.Clamp(CurrentPosition.TotalSeconds / Duration.TotalSeconds, 0.0, 1.0);
        }
    }

    public string ElapsedTimeText => CurrentPosition.ToString(@"m\:ss");
    public string RemainingTimeText => (Duration - CurrentPosition).ToString(@"\-m\:ss");

    public double Volume
    {
        get => _playerCoordinator.Volume * 100.0;
        set
        {
            _playerCoordinator.Volume = value / 100.0;
            OnPropertyChanged();
        }
    }

    public HeartRating CurrentRating => CurrentTrack?.Rating ?? HeartRating.None;
    public bool IsFavorite => CurrentRating == HeartRating.Favorite;
    public bool IsDisliked => CurrentRating == HeartRating.Dislike;

    public bool Shuffle
    {
        get => _playerCoordinator.Shuffle;
        set
        {
            _playerCoordinator.Shuffle = value;
            OnPropertyChanged();
        }
    }

    public bool Repeat
    {
        get => _playerCoordinator.Repeat;
        set
        {
            _playerCoordinator.Repeat = value;
            OnPropertyChanged();
        }
    }

    // Commands
    public ICommand SelectPivotCommand { get; }
    public ICommand PlayPauseCommand { get; }
    public ICommand NextCommand { get; }
    public ICommand PreviousCommand { get; }
    public ICommand ToggleFavoriteCommand { get; }
    public ICommand ToggleDislikeCommand { get; }
    public ICommand ToggleNowPlayingCommand { get; }
    public ICommand ToggleShuffleCommand { get; }
    public ICommand ToggleRepeatCommand { get; }
    public ICommand SeekCommand { get; }
    public ICommand ToggleCompactModeCommand { get; }
    public ICommand ToggleZuneCardCommand { get; }

    public MainShellViewModel(
        IPlayerCoordinator playerCoordinator,
        IMediaLibraryService libraryService,
        IDeviceSyncService deviceSyncService,
        ISmartDJService smartDJService,
        ISoundEffectService? soundEffectService = null,
        IUserStatsService? userStatsService = null,
        IPodcastService? podcastService = null,
        IFolderPickerService? folderPickerService = null)
    {
        _playerCoordinator = playerCoordinator;
        _libraryService = libraryService;
        _deviceSyncService = deviceSyncService;
        _soundEffectService = soundEffectService ?? new SoundEffectService();
        _userStatsService = userStatsService ?? new UserStatsService(libraryService);
        var podService = podcastService ?? new PodcastService(playerCoordinator);

        // Child ViewModels
        QuickplayVM = new QuickplayViewModel(playerCoordinator, libraryService, smartDJService);
        CollectionVM = new CollectionViewModel(playerCoordinator, libraryService, podService);
        NowPlayingVM = new NowPlayingViewModel(playerCoordinator, libraryService);
        DeviceVM = new DeviceViewModel(deviceSyncService);
        SettingsVM = new SettingsViewModel(_soundEffectService, folderPickerService, _libraryService);
        ZuneCardVM = new ZuneCardViewModel(_userStatsService);

        _currentView = QuickplayVM;

        // Background sync
        SettingsVM.BackgroundArtChanged += (_, artUri) => SelectedBackgroundArt = artUri;

        // Wire player events
        _playerCoordinator.TrackChanged += OnPlayerTrackChanged;
        _playerCoordinator.StateChanged += OnPlayerStateChanged;
        _playerCoordinator.RatingChanged += OnPlayerRatingChanged;

        // Wire device sync events
        _deviceSyncService.DeviceConnected += (_, _) => _soundEffectService?.PlayNotification();

        // Wire library updates
        _libraryService.LibraryUpdated += async (_, _) =>
        {
            await Dispatcher.UIThread.InvokeAsync(async () =>
            {
                await CollectionVM.RefreshDataAsync();
                await QuickplayVM.LoadInitialDataAsync();
                if (_userStatsService != null)
                {
                    await ZuneCardVM.LoadStatsAsync();
                }
            });
        };

        // Setup commands
        SelectPivotCommand = new RelayCommand<NavigationPivot>(pivot => ActivePivot = pivot);
        PlayPauseCommand = new AsyncRelayCommand(() => _playerCoordinator.PlayPauseAsync());
        NextCommand = new AsyncRelayCommand(() => _playerCoordinator.NextAsync());
        PreviousCommand = new AsyncRelayCommand(() => _playerCoordinator.PreviousAsync());
        ToggleFavoriteCommand = new AsyncRelayCommand(OnToggleFavoriteAsync);
        ToggleDislikeCommand = new AsyncRelayCommand(OnToggleDislikeAsync);
        ToggleShuffleCommand = new RelayCommand(() => Shuffle = !Shuffle);
        ToggleRepeatCommand = new RelayCommand(() => Repeat = !Repeat);
        ToggleCompactModeCommand = new RelayCommand(() => IsCompactMode = !IsCompactMode);
        ToggleZuneCardCommand = new RelayCommand(() =>
        {
            ActivePivot = ActivePivot == NavigationPivot.Social
                ? NavigationPivot.Collection
                : NavigationPivot.Social;
        });

        SeekCommand = new AsyncRelayCommand<double>(async progress =>
        {
            if (Duration.TotalSeconds > 0)
            {
                var target = TimeSpan.FromSeconds(progress * Duration.TotalSeconds);
                await _playerCoordinator.SeekAsync(target);
            }
        });

        ToggleNowPlayingCommand = new RelayCommand(() =>
        {
            ActivePivot = ActivePivot == NavigationPivot.NowPlaying 
                ? NavigationPivot.Collection 
                : NavigationPivot.NowPlaying;
        });

        // Initialize Now Playing equalizer animation timer
        try
        {
            _equalizerTimer = new DispatcherTimer
            {
                Interval = TimeSpan.FromMilliseconds(120)
            };
            _equalizerTimer.Tick += (s, e) =>
            {
                _equalizerFrame = (_equalizerFrame % 10) + 1;
                NowPlayingIconSource = $"avares://NotZune.UI/Assets/Zune/Transport/ICON.NOWPLAYING.FRAME{_equalizerFrame:D2}.PNG";
            };
        }
        catch
        {
            // Fallback for headless test environments where Dispatcher is unavailable
        }
    }

    private async Task OnToggleFavoriteAsync()
    {
        if (CurrentTrack == null) return;
        var newRating = CurrentRating == HeartRating.Favorite ? HeartRating.None : HeartRating.Favorite;
        await _playerCoordinator.SetRatingAsync(CurrentTrack.Id, newRating);
        await _libraryService.SetTrackRatingAsync(CurrentTrack.Id, newRating);
    }

    private async Task OnToggleDislikeAsync()
    {
        if (CurrentTrack == null) return;
        var newRating = CurrentRating == HeartRating.Dislike ? HeartRating.None : HeartRating.Dislike;
        await _playerCoordinator.SetRatingAsync(CurrentTrack.Id, newRating);
        await _libraryService.SetTrackRatingAsync(CurrentTrack.Id, newRating);
        if (newRating == HeartRating.Dislike)
        {
            await _playerCoordinator.NextAsync();
        }
    }

    private void OnPlayerTrackChanged(object? sender, TrackChangedEventArgs e)
    {
        OnPropertyChanged(nameof(CurrentTrack));
        OnPropertyChanged(nameof(Duration));
        OnPropertyChanged(nameof(CurrentPosition));
        OnPropertyChanged(nameof(ProgressPercentage));
        OnPropertyChanged(nameof(ElapsedTimeText));
        OnPropertyChanged(nameof(RemainingTimeText));
        OnPropertyChanged(nameof(CurrentRating));
        OnPropertyChanged(nameof(IsFavorite));
        OnPropertyChanged(nameof(IsDisliked));

        if (e.CurrentTrack != null)
        {
            _ = _userStatsService?.RecordTrackPlayedAsync(e.CurrentTrack);
        }
    }

    private void OnPlayerStateChanged(object? sender, PlaybackStateChangedEventArgs e)
    {
        OnPropertyChanged(nameof(State));
        OnPropertyChanged(nameof(IsPlaying));
        OnPropertyChanged(nameof(PlayPauseIcon));
        OnPropertyChanged(nameof(CurrentPosition));
        OnPropertyChanged(nameof(ProgressPercentage));
        OnPropertyChanged(nameof(ElapsedTimeText));
        OnPropertyChanged(nameof(RemainingTimeText));
        OnPropertyChanged(nameof(Shuffle));
        OnPropertyChanged(nameof(Repeat));

        if (IsPlaying)
        {
            _equalizerTimer?.Start();
        }
        else
        {
            _equalizerTimer?.Stop();
            NowPlayingIconSource = "avares://NotZune.UI/Assets/Zune/Transport/ICON.NOWPLAYING.ENTER.PNG";
        }
    }

    private void OnPlayerRatingChanged(object? sender, HeartRatingChangedEventArgs e)
    {
        OnPropertyChanged(nameof(CurrentRating));
        OnPropertyChanged(nameof(IsFavorite));
        OnPropertyChanged(nameof(IsDisliked));
    }
}
