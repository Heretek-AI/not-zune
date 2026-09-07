using System.Collections.ObjectModel;
using System.Timers;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;
using NotZune.Application.Events;
using NotZune.Application.Interfaces;
using NotZune.Domain.Enums;
using NotZune.Domain.Models;

namespace NotZune.UI.ViewModels;

public enum NowPlayingMode
{
    ArtistCanvas,
    MosaicWall
}

public class NowPlayingViewModel : ViewModelBase
{
    private readonly IPlayerCoordinator _playerCoordinator;
    private readonly IMediaLibraryService _libraryService;
    private readonly System.Timers.Timer _hudIdleTimer;

    private NowPlayingMode _mode = NowPlayingMode.ArtistCanvas;
    private bool _isHudVisible = true;
    private bool _isBioDrawerOpen = false;
    private bool _isShowlistOpen = false;

    public ObservableCollection<Album> MosaicWallAlbums { get; } = new();
    public ObservableCollection<Track> UpcomingQueue { get; } = new();

    public NowPlayingMode Mode
    {
        get => _mode;
        set
        {
            if (SetProperty(ref _mode, value))
            {
                OnPropertyChanged(nameof(IsArtistCanvasMode));
                OnPropertyChanged(nameof(IsMosaicWallMode));
            }
        }
    }

    public bool IsArtistCanvasMode => _mode == NowPlayingMode.ArtistCanvas;
    public bool IsMosaicWallMode => _mode == NowPlayingMode.MosaicWall;

    public bool IsHudVisible
    {
        get => _isHudVisible;
        set => SetProperty(ref _isHudVisible, value);
    }

    public bool IsBioDrawerOpen
    {
        get => _isBioDrawerOpen;
        set => SetProperty(ref _isBioDrawerOpen, value);
    }

    public bool IsShowlistOpen
    {
        get => _isShowlistOpen;
        set => SetProperty(ref _isShowlistOpen, value);
    }

    public int UpcomingQueueCount => UpcomingQueue.Count;

    public Track? CurrentTrack => _playerCoordinator.CurrentTrack;
    public string TrackTitle => CurrentTrack?.Title ?? "No Track Playing";
    public string ArtistName => CurrentTrack?.ArtistName ?? "Zune Player";
    public string AlbumTitle => CurrentTrack?.AlbumTitle ?? string.Empty;
    public string Genre => CurrentTrack?.Genre ?? "Music";
    public int Year => CurrentTrack?.Year ?? 0;

    public bool IsPlaying => _playerCoordinator.State == PlaybackState.Playing;
    public string PlayPauseIcon => IsPlaying ? "⏸" : "▶";
    public bool IsFavorite => CurrentTrack?.Rating == HeartRating.Favorite;
    public bool IsDisliked => CurrentTrack?.Rating == HeartRating.Dislike;

    public TimeSpan CurrentPosition => _playerCoordinator.CurrentPosition;
    public TimeSpan Duration => _playerCoordinator.Duration;
    public string ElapsedTimeText => CurrentPosition.ToString(@"m\:ss");
    public string RemainingTimeText => (Duration - CurrentPosition).ToString(@"\-m\:ss");

    public double ProgressPercentage
    {
        get
        {
            if (Duration.TotalSeconds <= 0) return 0.0;
            return Math.Clamp(CurrentPosition.TotalSeconds / Duration.TotalSeconds, 0.0, 1.0);
        }
    }

    public string BiographyText => $"Formed in Toronto, Canada, {ArtistName} has created a prolific catalog blending intricate rhythms and visionary songwriting. Their critically acclaimed works continue to inspire generations of listeners across the globe.";
    public string LyricsText => $"[Verse 1]\nSprawling on the fringes of the city in geometric order\nAn insulated border in between the bright light and the far unlit unknown\n\n[Chorus]\nSubdivisions in the high school halls\nIn the shopping malls, conform or be cast out\nSubdivisions in the basement bars\nIn the backs of cars, be cool or be cast out";

    public ICommand ToggleModeCommand { get; }
    public ICommand ToggleBioDrawerCommand { get; }
    public ICommand ToggleShowlistCommand { get; }
    public ICommand PlayQueueTrackCommand { get; }
    public ICommand ResetHudTimerCommand { get; }
    public ICommand PlayPauseCommand { get; }
    public ICommand NextCommand { get; }
    public ICommand PreviousCommand { get; }
    public ICommand ToggleFavoriteCommand { get; }
    public ICommand ToggleDislikeCommand { get; }

    public NowPlayingViewModel(
        IPlayerCoordinator playerCoordinator,
        IMediaLibraryService libraryService)
    {
        _playerCoordinator = playerCoordinator;
        _libraryService = libraryService;

        // Auto-hiding HUD timer (fades out after 3.5 seconds of idle)
        _hudIdleTimer = new System.Timers.Timer(3500) { AutoReset = false };
        _hudIdleTimer.Elapsed += (s, e) =>
        {
            if (IsPlaying)
            {
                IsHudVisible = false;
            }
        };

        ToggleModeCommand = new RelayCommand(() =>
        {
            Mode = Mode == NowPlayingMode.ArtistCanvas ? NowPlayingMode.MosaicWall : NowPlayingMode.ArtistCanvas;
        });

        ToggleBioDrawerCommand = new RelayCommand(() =>
        {
            IsBioDrawerOpen = !IsBioDrawerOpen;
            if (IsBioDrawerOpen) IsShowlistOpen = false;
            TriggerHudActivity();
        });

        ToggleShowlistCommand = new RelayCommand(() =>
        {
            IsShowlistOpen = !IsShowlistOpen;
            if (IsShowlistOpen)
            {
                IsBioDrawerOpen = false;
                UpdateUpcomingQueue();
            }
            TriggerHudActivity();
        });

        PlayQueueTrackCommand = new AsyncRelayCommand<Track>(async track =>
        {
            if (track != null)
            {
                await _playerCoordinator.PlayTrackAsync(track);
                TriggerHudActivity();
            }
        });

        ResetHudTimerCommand = new RelayCommand(TriggerHudActivity);

        PlayPauseCommand = new AsyncRelayCommand(async () =>
        {
            await _playerCoordinator.PlayPauseAsync();
            TriggerHudActivity();
        });

        NextCommand = new AsyncRelayCommand(async () =>
        {
            await _playerCoordinator.NextAsync();
            TriggerHudActivity();
        });

        PreviousCommand = new AsyncRelayCommand(async () =>
        {
            await _playerCoordinator.PreviousAsync();
            TriggerHudActivity();
        });

        ToggleFavoriteCommand = new AsyncRelayCommand(async () =>
        {
            if (CurrentTrack == null) return;
            var newRating = CurrentTrack.Rating == HeartRating.Favorite ? HeartRating.None : HeartRating.Favorite;
            await _playerCoordinator.SetRatingAsync(CurrentTrack.Id, newRating);
            await _libraryService.SetTrackRatingAsync(CurrentTrack.Id, newRating);
            TriggerHudActivity();
        });

        ToggleDislikeCommand = new AsyncRelayCommand(async () =>
        {
            if (CurrentTrack == null) return;
            var newRating = CurrentTrack.Rating == HeartRating.Dislike ? HeartRating.None : HeartRating.Dislike;
            await _playerCoordinator.SetRatingAsync(CurrentTrack.Id, newRating);
            await _libraryService.SetTrackRatingAsync(CurrentTrack.Id, newRating);
            if (newRating == HeartRating.Dislike)
            {
                await _playerCoordinator.NextAsync();
            }
            TriggerHudActivity();
        });

        _playerCoordinator.TrackChanged += OnTrackChanged;
        _playerCoordinator.StateChanged += OnStateChanged;
        _playerCoordinator.RatingChanged += OnRatingChanged;

        _ = LoadMosaicWallAsync();
        UpdateUpcomingQueue();
        TriggerHudActivity();
    }

    public void TriggerHudActivity()
    {
        IsHudVisible = true;
        _hudIdleTimer.Stop();
        _hudIdleTimer.Start();
    }

    private async Task LoadMosaicWallAsync()
    {
        var albums = await _libraryService.GetAllAlbumsAsync();
        MosaicWallAlbums.Clear();
        foreach (var album in albums)
        {
            MosaicWallAlbums.Add(album);
        }
    }

    public void UpdateUpcomingQueue()
    {
        UpcomingQueue.Clear();
        foreach (var track in _playerCoordinator.Queue)
        {
            UpcomingQueue.Add(track);
        }
        OnPropertyChanged(nameof(UpcomingQueueCount));
    }

    private void OnTrackChanged(object? sender, TrackChangedEventArgs e)
    {
        OnPropertyChanged(nameof(CurrentTrack));
        OnPropertyChanged(nameof(TrackTitle));
        OnPropertyChanged(nameof(ArtistName));
        OnPropertyChanged(nameof(AlbumTitle));
        OnPropertyChanged(nameof(Genre));
        OnPropertyChanged(nameof(Year));
        OnPropertyChanged(nameof(Duration));
        OnPropertyChanged(nameof(CurrentPosition));
        OnPropertyChanged(nameof(ProgressPercentage));
        OnPropertyChanged(nameof(ElapsedTimeText));
        OnPropertyChanged(nameof(RemainingTimeText));
        OnPropertyChanged(nameof(IsFavorite));
        OnPropertyChanged(nameof(IsDisliked));
        OnPropertyChanged(nameof(BiographyText));
        UpdateUpcomingQueue();
        TriggerHudActivity();
    }

    private void OnStateChanged(object? sender, PlaybackStateChangedEventArgs e)
    {
        OnPropertyChanged(nameof(IsPlaying));
        OnPropertyChanged(nameof(PlayPauseIcon));
        OnPropertyChanged(nameof(CurrentPosition));
        OnPropertyChanged(nameof(ProgressPercentage));
        OnPropertyChanged(nameof(ElapsedTimeText));
        OnPropertyChanged(nameof(RemainingTimeText));
    }

    private void OnRatingChanged(object? sender, HeartRatingChangedEventArgs e)
    {
        OnPropertyChanged(nameof(IsFavorite));
        OnPropertyChanged(nameof(IsDisliked));
    }
}
