using System.Collections.ObjectModel;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;
using NotZune.Application.Events;
using NotZune.Application.Interfaces;
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

    private NowPlayingMode _mode = NowPlayingMode.ArtistCanvas;

    public ObservableCollection<Album> MosaicWallAlbums { get; } = new();

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

    public Track? CurrentTrack => _playerCoordinator.CurrentTrack;
    public string TrackTitle => CurrentTrack?.Title ?? "No Track Playing";
    public string ArtistName => CurrentTrack?.ArtistName ?? "Zune Player";
    public string AlbumTitle => CurrentTrack?.AlbumTitle ?? string.Empty;

    public ICommand ToggleModeCommand { get; }

    public NowPlayingViewModel(
        IPlayerCoordinator playerCoordinator,
        IMediaLibraryService libraryService)
    {
        _playerCoordinator = playerCoordinator;
        _libraryService = libraryService;

        ToggleModeCommand = new RelayCommand(() =>
        {
            Mode = Mode == NowPlayingMode.ArtistCanvas ? NowPlayingMode.MosaicWall : NowPlayingMode.ArtistCanvas;
        });

        _playerCoordinator.TrackChanged += OnTrackChanged;
        _ = LoadMosaicWallAsync();
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

    private void OnTrackChanged(object? sender, TrackChangedEventArgs e)
    {
        OnPropertyChanged(nameof(CurrentTrack));
        OnPropertyChanged(nameof(TrackTitle));
        OnPropertyChanged(nameof(ArtistName));
        OnPropertyChanged(nameof(AlbumTitle));
    }
}
