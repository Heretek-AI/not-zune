using NotZune.Application.Events;
using NotZune.Domain.Enums;
using NotZune.Domain.Models;

namespace NotZune.Application.Interfaces;

public interface IPlayerCoordinator
{
    PlaybackState State { get; }
    Track? CurrentTrack { get; }
    TimeSpan CurrentPosition { get; }
    TimeSpan Duration { get; }
    double Volume { get; set; }
    bool IsMuted { get; set; }
    bool Shuffle { get; set; }
    bool Repeat { get; set; }
    IReadOnlyList<Track> Queue { get; }

    Task PlayTrackAsync(Track track, IEnumerable<Track>? contextQueue = null);
    Task PlayPauseAsync();
    Task StopAsync();
    Task NextAsync();
    Task PreviousAsync();
    Task SeekAsync(TimeSpan position);
    Task SetRatingAsync(Guid trackId, HeartRating rating);
    void Enqueue(IEnumerable<Track> tracks);
    void PlayNext(IEnumerable<Track> tracks);

    event EventHandler<TrackChangedEventArgs>? TrackChanged;
    event EventHandler<PlaybackStateChangedEventArgs>? StateChanged;
    event EventHandler<HeartRatingChangedEventArgs>? RatingChanged;
}

public interface IMediaLibraryService
{
    Task<IReadOnlyList<Track>> GetAllTracksAsync();
    Task<IReadOnlyList<Album>> GetAllAlbumsAsync();
    Task<IReadOnlyList<Artist>> GetAllArtistsAsync();
    Task<IReadOnlyList<Playlist>> GetAllPlaylistsAsync();
    Task<IReadOnlyList<PlayHistoryEntry>> GetRecentHistoryAsync(int count = 20);
    Task<IReadOnlyList<Album>> GetRecentlyAddedAlbumsAsync(int count = 12);
    Task<IReadOnlyList<Track>> SearchAsync(string query);
    Task SetTrackRatingAsync(Guid trackId, HeartRating rating);
    Task ScanDirectoryAsync(string directoryPath, IProgress<double>? progress = null);
    Task ClearDemoDataAsync();
    event EventHandler? LibraryUpdated;
}

public interface IDeviceSyncService
{
    IReadOnlyList<ZuneDevice> ConnectedDevices { get; }
    Task StartMonitoringAsync(CancellationToken cancellationToken);
    Task SyncDeviceAsync(string serialNumber, IProgress<double>? progress = null);
    event EventHandler<ZuneDevice>? DeviceConnected;
    event EventHandler<string>? DeviceDisconnected;
}

public interface ISmartDJService
{
    Task<IReadOnlyList<Track>> GenerateMixAsync(SmartDJSeed seed, IReadOnlyList<Track> libraryTracks);
}
