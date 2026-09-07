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
    Genres,
    Podcasts,
    Playlists
}

public class CollectionViewModel : ViewModelBase
{
    private readonly IPlayerCoordinator _playerCoordinator;
    private readonly IMediaLibraryService _libraryService;

    private CollectionSubPivot _activeSubPivot = CollectionSubPivot.Artists;
    private string _searchQuery = string.Empty;
    public string SearchQuery
    {
        get => _searchQuery;
        set
        {
            if (SetProperty(ref _searchQuery, value))
            {
                FilterQuery(value);
            }
        }
    }
    private Artist? _selectedArtist;
    private string? _selectedGenre;

    private List<Artist> _allArtists = new();
    private List<Album> _allAlbums = new();
    private List<Track> _allSongs = new();

    public PodcastsViewModel PodcastsVM { get; }
    public PlaylistsViewModel PlaylistsVM { get; }

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
                OnPropertyChanged(nameof(IsPodcastsActive));
                OnPropertyChanged(nameof(IsPlaylistsActive));
            }
        }
    }

    public bool IsArtistsActive => _activeSubPivot == CollectionSubPivot.Artists;
    public bool IsAlbumsActive => _activeSubPivot == CollectionSubPivot.Albums;
    public bool IsSongsActive => _activeSubPivot == CollectionSubPivot.Songs;
    public bool IsGenresActive => _activeSubPivot == CollectionSubPivot.Genres;
    public bool IsPodcastsActive => _activeSubPivot == CollectionSubPivot.Podcasts;
    public bool IsPlaylistsActive => _activeSubPivot == CollectionSubPivot.Playlists;

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
    public ICommand OpenEditMetadataCommand { get; }
    public ICommand CloseEditMetadataCommand { get; }

    private MetadataEditViewModel? _activeEditMetadataVM;
    public MetadataEditViewModel? ActiveEditMetadataVM
    {
        get => _activeEditMetadataVM;
        set
        {
            if (SetProperty(ref _activeEditMetadataVM, value))
            {
                OnPropertyChanged(nameof(IsEditMetadataOpen));
            }
        }
    }

    public bool IsEditMetadataOpen => ActiveEditMetadataVM != null;

    public CollectionViewModel(
        IPlayerCoordinator playerCoordinator,
        IMediaLibraryService libraryService,
        IPodcastService? podcastService = null)
    {
        _playerCoordinator = playerCoordinator;
        _libraryService = libraryService;
        var podService = podcastService ?? new NotZune.Application.Services.PodcastService(playerCoordinator);
        PodcastsVM = new PodcastsViewModel(podService);
        PlaylistsVM = new PlaylistsViewModel(libraryService, playerCoordinator);

        OpenEditMetadataCommand = new RelayCommand<Track>(track =>
        {
            if (track != null)
            {
                var vm = new MetadataEditViewModel(track, _libraryService);
                vm.RequestClose += (_, _) => ActiveEditMetadataVM = null;
                ActiveEditMetadataVM = vm;
            }
        });
        CloseEditMetadataCommand = new RelayCommand(() => ActiveEditMetadataVM = null);

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

    public void FilterQuery(string query)
    {
        _searchQuery = query;
        if (string.IsNullOrWhiteSpace(query))
        {
            Albums.Clear();
            foreach (var a in _allAlbums) Albums.Add(a);

            Artists.Clear();
            foreach (var a in _allArtists) Artists.Add(a);

            Songs.Clear();
            foreach (var s in _allSongs) Songs.Add(s);
            return;
        }

        var lower = query.Trim().ToLowerInvariant();

        Songs.Clear();
        foreach (var s in _allSongs.Where(s => 
            s.Title.ToLowerInvariant().Contains(lower) || 
            s.ArtistName.ToLowerInvariant().Contains(lower) || 
            s.AlbumTitle.ToLowerInvariant().Contains(lower) || 
            s.Genre.ToLowerInvariant().Contains(lower)))
        {
            Songs.Add(s);
        }

        Albums.Clear();
        foreach (var a in _allAlbums.Where(a => 
            a.Title.ToLowerInvariant().Contains(lower) || 
            a.ArtistName.ToLowerInvariant().Contains(lower) || 
            a.Genre.ToLowerInvariant().Contains(lower)))
        {
            Albums.Add(a);
        }

        Artists.Clear();
        foreach (var a in _allArtists.Where(a => 
            a.Name.ToLowerInvariant().Contains(lower)))
        {
            Artists.Add(a);
        }
    }

    public async Task RefreshDataAsync()
    {
        var albums = await _libraryService.GetAllAlbumsAsync();
        _allAlbums = albums.ToList();
        Albums.Clear();
        foreach (var album in albums)
        {
            Albums.Add(album);
        }

        var artists = await _libraryService.GetAllArtistsAsync();
        _allArtists = artists.ToList();
        Artists.Clear();
        foreach (var artist in artists)
        {
            Artists.Add(artist);
        }

        var songs = await _libraryService.GetAllTracksAsync();
        _allSongs = songs.ToList();
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

        await PlaylistsVM.LoadPlaylistsAsync();

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
