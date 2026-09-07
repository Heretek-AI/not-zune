using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;
using NotZune.Application.Events;
using NotZune.Application.Interfaces;
using NotZune.Domain.Enums;
using NotZune.Domain.Models;

namespace NotZune.UI.ViewModels;

public enum NavigationPivot
{
    Quickplay,
    Collection,
    NowPlaying,
    Device,
    Settings
}

public class MainShellViewModel : ViewModelBase
{
    private readonly IPlayerCoordinator _playerCoordinator;
    private readonly IMediaLibraryService _libraryService;
    private readonly IDeviceSyncService _deviceSyncService;

    private NavigationPivot _activePivot = NavigationPivot.Quickplay;
    private ViewModelBase _currentView;

    public QuickplayViewModel QuickplayVM { get; }
    public CollectionViewModel CollectionVM { get; }
    public NowPlayingViewModel NowPlayingVM { get; }
    public DeviceViewModel DeviceVM { get; }
    public SettingsViewModel SettingsVM { get; }

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

                CurrentView = _activePivot switch
                {
                    NavigationPivot.Quickplay => QuickplayVM,
                    NavigationPivot.Collection => CollectionVM,
                    NavigationPivot.NowPlaying => NowPlayingVM,
                    NavigationPivot.Device => DeviceVM,
                    NavigationPivot.Settings => SettingsVM,
                    _ => QuickplayVM
                };
            }
        }
    }

    public bool IsQuickplayActive => _activePivot == NavigationPivot.Quickplay;
    public bool IsCollectionActive => _activePivot == NavigationPivot.Collection;
    public bool IsNowPlayingActive => _activePivot == NavigationPivot.NowPlaying;
    public bool IsDeviceActive => _activePivot == NavigationPivot.Device;
    public bool IsSettingsActive => _activePivot == NavigationPivot.Settings;

    public ViewModelBase CurrentView
    {
        get => _currentView;
        private set => SetProperty(ref _currentView, value);
    }

    // Playback HUD properties
    public Track? CurrentTrack => _playerCoordinator.CurrentTrack;
    public PlaybackState State => _playerCoordinator.State;
    public bool IsPlaying => State == PlaybackState.Playing;
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

    public MainShellViewModel(
        IPlayerCoordinator playerCoordinator,
        IMediaLibraryService libraryService,
        IDeviceSyncService deviceSyncService,
        ISmartDJService smartDJService)
    {
        _playerCoordinator = playerCoordinator;
        _libraryService = libraryService;
        _deviceSyncService = deviceSyncService;

        // Child ViewModels
        QuickplayVM = new QuickplayViewModel(playerCoordinator, libraryService, smartDJService);
        CollectionVM = new CollectionViewModel(playerCoordinator, libraryService);
        NowPlayingVM = new NowPlayingViewModel(playerCoordinator, libraryService);
        DeviceVM = new DeviceViewModel(deviceSyncService);
        SettingsVM = new SettingsViewModel();

        _currentView = QuickplayVM;

        // Wire events
        _playerCoordinator.TrackChanged += OnPlayerTrackChanged;
        _playerCoordinator.StateChanged += OnPlayerStateChanged;
        _playerCoordinator.RatingChanged += OnPlayerRatingChanged;

        // Setup commands
        SelectPivotCommand = new RelayCommand<NavigationPivot>(pivot => ActivePivot = pivot);
        PlayPauseCommand = new AsyncRelayCommand(() => _playerCoordinator.PlayPauseAsync());
        NextCommand = new AsyncRelayCommand(() => _playerCoordinator.NextAsync());
        PreviousCommand = new AsyncRelayCommand(() => _playerCoordinator.PreviousAsync());
        ToggleFavoriteCommand = new AsyncRelayCommand(OnToggleFavoriteAsync);
        ToggleDislikeCommand = new AsyncRelayCommand(OnToggleDislikeAsync);
        ToggleShuffleCommand = new RelayCommand(() => Shuffle = !Shuffle);
        ToggleRepeatCommand = new RelayCommand(() => Repeat = !Repeat);
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
            // Auto skip disliked song
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
    }

    private void OnPlayerStateChanged(object? sender, PlaybackStateChangedEventArgs e)
    {
        OnPropertyChanged(nameof(State));
        OnPropertyChanged(nameof(IsPlaying));
        OnPropertyChanged(nameof(CurrentPosition));
        OnPropertyChanged(nameof(ProgressPercentage));
        OnPropertyChanged(nameof(ElapsedTimeText));
        OnPropertyChanged(nameof(RemainingTimeText));
        OnPropertyChanged(nameof(Shuffle));
        OnPropertyChanged(nameof(Repeat));
    }

    private void OnPlayerRatingChanged(object? sender, HeartRatingChangedEventArgs e)
    {
        OnPropertyChanged(nameof(CurrentRating));
        OnPropertyChanged(nameof(IsFavorite));
        OnPropertyChanged(nameof(IsDisliked));
    }
}
