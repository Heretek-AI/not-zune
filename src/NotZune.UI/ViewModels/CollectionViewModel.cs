using System.Collections.ObjectModel;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;
using NotZune.Application.Interfaces;
using NotZune.Domain.Enums;
using NotZune.Domain.Models;

namespace NotZune.UI.ViewModels;

public enum CollectionSubPivot
{
    Artists,
    Albums,
    Songs,
    Genres
}

public class CollectionViewModel : ViewModelBase
{
    private readonly IPlayerCoordinator _playerCoordinator;
    private readonly IMediaLibraryService _libraryService;

    private CollectionSubPivot _activeSubPivot = CollectionSubPivot.Albums;
    private string _searchQuery = string.Empty;

    public ObservableCollection<Artist> Artists { get; } = new();
    public ObservableCollection<Album> Albums { get; } = new();
    public ObservableCollection<Track> Songs { get; } = new();

    public CollectionSubPivot ActiveSubPivot
    {
        get => _activeSubPivot;
        set
        {
            if (SetProperty(ref _activeSubPivot, value))
            {
                OnPropertyChanged(nameof(IsArtistsActive));
                OnPropertyChanged(nameof(IsAlbumsActive));
                OnPropertyChanged(nameof(IsSongsActive));
                OnPropertyChanged(nameof(IsGenresActive));
            }
        }
    }

    public bool IsArtistsActive => _activeSubPivot == CollectionSubPivot.Artists;
    public bool IsAlbumsActive => _activeSubPivot == CollectionSubPivot.Albums;
    public bool IsSongsActive => _activeSubPivot == CollectionSubPivot.Songs;
    public bool IsGenresActive => _activeSubPivot == CollectionSubPivot.Genres;

    public string SearchQuery
    {
        get => _searchQuery;
        set
        {
            if (SetProperty(ref _searchQuery, value))
            {
                _ = PerformSearchAsync();
            }
        }
    }

    public ICommand SelectSubPivotCommand { get; }
    public ICommand PlaySongCommand { get; }
    public ICommand PlayAlbumCommand { get; }

    public CollectionViewModel(
        IPlayerCoordinator playerCoordinator,
        IMediaLibraryService libraryService)
    {
        _playerCoordinator = playerCoordinator;
        _libraryService = libraryService;

        SelectSubPivotCommand = new RelayCommand<CollectionSubPivot>(pivot => ActiveSubPivot = pivot);
        PlaySongCommand = new AsyncRelayCommand<Track>(OnPlaySongAsync);
        PlayAlbumCommand = new AsyncRelayCommand<Album>(OnPlayAlbumAsync);

        _ = RefreshDataAsync();
    }

    public async Task RefreshDataAsync()
    {
        var albums = await _libraryService.GetAllAlbumsAsync();
        Albums.Clear();
        foreach (var album in albums)
        {
            Albums.Add(album);
        }

        var artists = await _libraryService.GetAllArtistsAsync();
        Artists.Clear();
        foreach (var artist in artists)
        {
            Artists.Add(artist);
        }

        var songs = await _libraryService.GetAllTracksAsync();
        Songs.Clear();
        foreach (var song in songs)
        {
            Songs.Add(song);
        }
    }

    private async Task PerformSearchAsync()
    {
        var results = await _libraryService.SearchAsync(_searchQuery);
        Songs.Clear();
        foreach (var s in results)
        {
            Songs.Add(s);
        }
    }

    private async Task OnPlaySongAsync(Track? track)
    {
        if (track == null) return;
        await _playerCoordinator.PlayTrackAsync(track, Songs);
    }

    private async Task OnPlayAlbumAsync(Album? album)
    {
        if (album == null || album.Tracks.Count == 0) return;
        await _playerCoordinator.PlayTrackAsync(album.Tracks[0], album.Tracks);
    }
}
