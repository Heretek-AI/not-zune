using NotZune.Application.Models;
using NotZune.Domain.Models;

namespace NotZune.Application.Interfaces;

public interface IExternalMetadataService
{
    Task<ArtistMetadataResult?> FetchArtistMetadataAsync(string artistName, CancellationToken cancellationToken = default);

    Task<IReadOnlyList<string>> FetchArtistBackgroundUrlsAsync(string artistName, string fanartTvApiKey, CancellationToken cancellationToken = default);

    Task<AlbumArtworkResult?> FindAlbumArtworkAsync(string artistName, string albumTitle, int? year = null, CancellationToken cancellationToken = default);

    Task<IReadOnlyList<TrackMatchCandidate>> FindTrackMatchesAsync(string artistName, string albumTitle, IReadOnlyList<Track> tracks, CancellationToken cancellationToken = default);

    Task<LyricsResult?> FetchLyricsAsync(string artistName, string trackTitle, TimeSpan? duration = null, CancellationToken cancellationToken = default);
}
