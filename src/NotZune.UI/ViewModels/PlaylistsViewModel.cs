using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;
using NotZune.Application.Interfaces;
using NotZune.Domain.Models;

namespace NotZune.UI.ViewModels;

public class PlaylistsViewModel : ViewModelBase
{
    private readonly IMediaLibraryService _libraryService;
    private readonly IPlayerCoordinator _playerCoordinator;

    public ObservableCollection<Playlist> Playlists { get; } = new();
    public ObservableCollection<Track> SelectedPlaylistTracks { get; } = new();

    private Playlist? _selectedPlaylist;
    public Playlist? SelectedPlaylist
    {
        get => _selectedPlaylist;
        set
        {
            if (SetProperty(ref _selectedPlaylist, value))
            {
                OnPropertyChanged(nameof(HasSelectedPlaylist));
                OnPropertyChanged(nameof(PlaylistTitle));
                OnPropertyChanged(nameof(PlaylistStatsText));
                _ = LoadSelectedPlaylistTracksAsync(value);
            }
        }
    }

    public bool HasSelectedPlaylist => SelectedPlaylist != null;
    public string PlaylistTitle => SelectedPlaylist?.Name ?? "Select a Playlist";

    public string PlaylistStatsText
    {
        get
        {
            if (SelectedPlaylist == null) return string.Empty;
            int count = SelectedPlaylistTracks.Count;
            var totalDuration = TimeSpan.FromSeconds(SelectedPlaylistTracks.Sum(t => t.Duration.TotalSeconds));
            return $"{count} {(count == 1 ? "song" : "songs")} • {totalDuration.Hours * 60 + totalDuration.Minutes} mins";
        }
    }

    private string _newPlaylistName = string.Empty;
    public string NewPlaylistName
    {
        get => _newPlaylistName;
        set => SetProperty(ref _newPlaylistName, value);
    }

    private string? _statusMessage;
    public string? StatusMessage
    {
        get => _statusMessage;
        set => SetProperty(ref _statusMessage, value);
    }

    public ICommand CreatePlaylistCommand { get; }
    public ICommand DeletePlaylistCommand { get; }
    public ICommand PlayPlaylistCommand { get; }
    public ICommand PlayTrackCommand { get; }
    public ICommand RemoveTrackCommand { get; }
    public ICommand ExportZplCommand { get; }

    public PlaylistsViewModel(IMediaLibraryService libraryService, IPlayerCoordinator playerCoordinator)
    {
        _libraryService = libraryService;
        _playerCoordinator = playerCoordinator;

        CreatePlaylistCommand = new AsyncRelayCommand(OnCreatePlaylistAsync);
        DeletePlaylistCommand = new AsyncRelayCommand<Playlist>(OnDeletePlaylistAsync);
        PlayPlaylistCommand = new AsyncRelayCommand(OnPlayPlaylistAsync);
        PlayTrackCommand = new AsyncRelayCommand<Track>(OnPlayTrackAsync);
        RemoveTrackCommand = new AsyncRelayCommand<Track>(OnRemoveTrackAsync);
        ExportZplCommand = new AsyncRelayCommand(OnExportZplAsync);

        _ = LoadPlaylistsAsync();
    }

    public async Task LoadPlaylistsAsync()
    {
        var list = await _libraryService.GetAllPlaylistsAsync();
        Playlists.Clear();
        foreach (var p in list)
        {
            Playlists.Add(p);
        }

        if (SelectedPlaylist == null || !Playlists.Any(p => p.Id == SelectedPlaylist.Id))
        {
            SelectedPlaylist = Playlists.FirstOrDefault();
        }
        else
        {
            await LoadSelectedPlaylistTracksAsync(SelectedPlaylist);
        }
    }

    private async Task LoadSelectedPlaylistTracksAsync(Playlist? playlist)
    {
        SelectedPlaylistTracks.Clear();
        if (playlist == null)
        {
            OnPropertyChanged(nameof(PlaylistStatsText));
            return;
        }

        var tracks = await _libraryService.GetPlaylistTracksAsync(playlist.Id);
        foreach (var t in tracks)
        {
            SelectedPlaylistTracks.Add(t);
        }
        OnPropertyChanged(nameof(PlaylistStatsText));
    }

    private async Task OnCreatePlaylistAsync()
    {
        var name = string.IsNullOrWhiteSpace(NewPlaylistName) ? "New Playlist" : NewPlaylistName.Trim();
        var playlist = await _libraryService.CreatePlaylistAsync(name);
        NewPlaylistName = string.Empty;
        await LoadPlaylistsAsync();
        SelectedPlaylist = Playlists.FirstOrDefault(p => p.Id == playlist.Id);
        StatusMessage = $"Created playlist '{playlist.Name}'";
    }

    private async Task OnDeletePlaylistAsync(Playlist? playlist)
    {
        var target = playlist ?? SelectedPlaylist;
        if (target == null) return;

        await _libraryService.DeletePlaylistAsync(target.Id);
        await LoadPlaylistsAsync();
        StatusMessage = $"Deleted playlist '{target.Name}'";
    }

    private Task OnPlayPlaylistAsync()
    {
        if (SelectedPlaylistTracks.Count > 0)
        {
            _playerCoordinator.Enqueue(SelectedPlaylistTracks.ToList());
            _ = _playerCoordinator.NextAsync();
        }
        return Task.CompletedTask;
    }

    private Task OnPlayTrackAsync(Track? track)
    {
        if (track != null)
        {
            _playerCoordinator.Enqueue(new[] { track });
            _ = _playerCoordinator.NextAsync();
        }
        return Task.CompletedTask;
    }

    private async Task OnRemoveTrackAsync(Track? track)
    {
        if (SelectedPlaylist == null || track == null) return;
        await _libraryService.RemoveTrackFromPlaylistAsync(SelectedPlaylist.Id, track.Id);
        await LoadSelectedPlaylistTracksAsync(SelectedPlaylist);
    }

    private async Task OnExportZplAsync()
    {
        if (SelectedPlaylist == null) return;
        try
        {
            var musicDir = Environment.GetFolderPath(Environment.SpecialFolder.MyMusic);
            var safeName = string.Join("_", SelectedPlaylist.Name.Split(Path.GetInvalidFileNameChars()));
            var outPath = Path.Combine(musicDir, "Playlists", $"{safeName}.zpl");
            await _libraryService.ExportPlaylistToZplAsync(SelectedPlaylist.Id, outPath);
            StatusMessage = $"Exported to {outPath}";
        }
        catch (Exception ex)
        {
            StatusMessage = $"Export failed: {ex.Message}";
        }
    }
}
