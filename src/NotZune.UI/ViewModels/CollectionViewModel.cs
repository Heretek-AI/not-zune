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

    private CollectionSubPivot _activeSubPivot = CollectionSubPivot.Artists;
    private string _searchQuery = string.Empty;
    private Artist? _selectedArtist;
    private string? _selectedGenre;

    public ObservableCollection<Artist> Artists { get; } = new();
    public ObservableCollection<Album> Albums { get; } = new();
    public ObservableCollection<Track> Songs { get; } = new();
    public ObservableCollection<Album> SelectedArtistAlbums { get; } = new();
    public ObservableCollection<string> Genres { get; } = new();
    public ObservableCollection<Track> SelectedGenreSongs { get; } = new();

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

    public Artist? SelectedArtist
    {
        get => _selectedArtist;
        set
        {
            if (SetProperty(ref _selectedArtist, value))
            {
                UpdateSelectedArtistAlbums();
            }
        }
    }

    public string? SelectedGenre
    {
        get => _selectedGenre;
        set
        {
            if (SetProperty(ref _selectedGenre, value))
            {
                UpdateSelectedGenreSongs();
            }
        }
    }

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
    public ICommand SelectArtistCommand { get; }
    public ICommand SelectGenreCommand { get; }
    public ICommand PlaySongCommand { get; }
    public ICommand PlayAlbumCommand { get; }
    public ICommand PlayArtistCommand { get; }
    public ICommand PlayGenreCommand { get; }
    public ICommand PlayNextCommand { get; }
    public ICommand EnqueueTrackCommand { get; }
    public ICommand ToggleFavoriteCommand { get; }
    public ICommand ToggleDislikeCommand { get; }

    public CollectionViewModel(
        IPlayerCoordinator playerCoordinator,
        IMediaLibraryService libraryService)
    {
        _playerCoordinator = playerCoordinator;
        _libraryService = libraryService;

        SelectSubPivotCommand = new RelayCommand<CollectionSubPivot>(pivot => ActiveSubPivot = pivot);
        SelectArtistCommand = new RelayCommand<Artist>(artist => SelectedArtist = artist);
        SelectGenreCommand = new RelayCommand<string>(genre => SelectedGenre = genre);
        PlaySongCommand = new AsyncRelayCommand<Track>(OnPlaySongAsync);
        PlayAlbumCommand = new AsyncRelayCommand<Album>(OnPlayAlbumAsync);
        PlayArtistCommand = new AsyncRelayCommand<Artist>(OnPlayArtistAsync);
        PlayGenreCommand = new AsyncRelayCommand<string>(OnPlayGenreAsync);
        PlayNextCommand = new RelayCommand<Track>(track =>
        {
            if (track != null) _playerCoordinator.PlayNext(new[] { track });
        });
        EnqueueTrackCommand = new RelayCommand<Track>(track =>
        {
            if (track != null) _playerCoordinator.Enqueue(new[] { track });
        });
        ToggleFavoriteCommand = new AsyncRelayCommand<Track>(OnToggleFavoriteAsync);
        ToggleDislikeCommand = new AsyncRelayCommand<Track>(OnToggleDislikeAsync);

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
        var uniqueGenres = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
        foreach (var song in songs)
        {
            Songs.Add(song);
            if (!string.IsNullOrWhiteSpace(song.Genre))
            {
                uniqueGenres.Add(song.Genre);
            }
        }

        Genres.Clear();
        foreach (var g in uniqueGenres.OrderBy(x => x))
        {
            Genres.Add(g);
        }

        if (Artists.Count > 0 && SelectedArtist == null)
        {
            SelectedArtist = Artists[0];
        }

        if (Genres.Count > 0 && SelectedGenre == null)
        {
            SelectedGenre = Genres[0];
        }
    }

    private void UpdateSelectedArtistAlbums()
    {
        SelectedArtistAlbums.Clear();
        if (_selectedArtist == null) return;

        var artistAlbums = Albums.Where(a => a.ArtistId == _selectedArtist.Id || 
                                             string.Equals(a.ArtistName, _selectedArtist.Name, StringComparison.OrdinalIgnoreCase))
                                 .OrderByDescending(a => a.Year);
        foreach (var album in artistAlbums)
        {
            SelectedArtistAlbums.Add(album);
        }
    }

    private void UpdateSelectedGenreSongs()
    {
        SelectedGenreSongs.Clear();
        if (string.IsNullOrEmpty(_selectedGenre)) return;

        var genreTracks = Songs.Where(t => string.Equals(t.Genre, _selectedGenre, StringComparison.OrdinalIgnoreCase));
        foreach (var track in genreTracks)
        {
            SelectedGenreSongs.Add(track);
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

    private async Task OnPlayArtistAsync(Artist? artist)
    {
        if (artist == null) return;
        var artistTracks = Songs.Where(t => t.ArtistId == artist.Id || 
                                           string.Equals(t.ArtistName, artist.Name, StringComparison.OrdinalIgnoreCase))
                                .ToList();
        if (artistTracks.Count > 0)
        {
            await _playerCoordinator.PlayTrackAsync(artistTracks[0], artistTracks);
        }
    }

    private async Task OnPlayGenreAsync(string? genre)
    {
        if (string.IsNullOrEmpty(genre)) return;
        var genreTracks = Songs.Where(t => string.Equals(t.Genre, genre, StringComparison.OrdinalIgnoreCase)).ToList();
        if (genreTracks.Count > 0)
        {
            await _playerCoordinator.PlayTrackAsync(genreTracks[0], genreTracks);
        }
    }

    private async Task OnToggleFavoriteAsync(Track? track)
    {
        if (track == null) return;
        var newRating = track.Rating == HeartRating.Favorite ? HeartRating.None : HeartRating.Favorite;
        track.Rating = newRating;
        await _playerCoordinator.SetRatingAsync(track.Id, newRating);
        await _libraryService.SetTrackRatingAsync(track.Id, newRating);
    }

    private async Task OnToggleDislikeAsync(Track? track)
    {
        if (track == null) return;
        var newRating = track.Rating == HeartRating.Dislike ? HeartRating.None : HeartRating.Dislike;
        track.Rating = newRating;
        await _playerCoordinator.SetRatingAsync(track.Id, newRating);
        await _libraryService.SetTrackRatingAsync(track.Id, newRating);
    }
}
