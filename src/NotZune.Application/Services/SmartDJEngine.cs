using NotZune.Application.Interfaces;
using NotZune.Domain.Enums;
using NotZune.Domain.Models;

namespace NotZune.Application.Services;

public class SmartDJEngine : ISmartDJService
{
    private readonly Random _random = new();

    public Task<IReadOnlyList<Track>> GenerateMixAsync(SmartDJSeed seed, IReadOnlyList<Track> libraryTracks)
    {
        var candidates = libraryTracks.AsEnumerable();

        // 1. Exclude disliked tracks if requested
        if (seed.ExcludeDisliked)
        {
            candidates = candidates.Where(t => t.Rating != HeartRating.Dislike);
        }

        // 2. Identify seed criteria
        Track? seedTrack = null;
        if (seed.SeedTrackId.HasValue)
        {
            seedTrack = libraryTracks.FirstOrDefault(t => t.Id == seed.SeedTrackId.Value);
        }

        Guid? targetAlbumId = seed.SeedAlbumId ?? seedTrack?.AlbumId;
        Guid? targetArtistId = seed.SeedArtistId ?? seedTrack?.ArtistId;
        string? targetGenre = !string.IsNullOrEmpty(seed.SeedGenre) ? seed.SeedGenre : seedTrack?.Genre;

        if (!targetArtistId.HasValue && targetAlbumId.HasValue)
        {
            var albumTrack = libraryTracks.FirstOrDefault(t => t.AlbumId == targetAlbumId.Value);
            if (albumTrack != null)
            {
                targetArtistId = albumTrack.ArtistId;
                if (string.IsNullOrEmpty(targetGenre))
                {
                    targetGenre = albumTrack.Genre;
                }
            }
        }

        // 3. Score candidates based on similarity:
        // - Same album: high weight (+12)
        // - Same artist: high weight (+10)
        // - Same genre: medium weight (+5)
        // - Favorite rating: bonus (+4)
        // - Random jitter to create dynamic mixes (+0 to +3)
        var scoredList = candidates.Select(track =>
        {
            double score = 0;
            if (targetAlbumId.HasValue && track.AlbumId == targetAlbumId.Value)
            {
                score += 12.0;
            }
            if (targetArtistId.HasValue && track.ArtistId == targetArtistId.Value)
            {
                score += 10.0;
            }
            if (!string.IsNullOrEmpty(targetGenre) && 
                string.Equals(track.Genre, targetGenre, StringComparison.OrdinalIgnoreCase))
            {
                score += 5.0;
            }
            if (track.Rating == HeartRating.Favorite)
            {
                score += 4.0;
            }

            score += _random.NextDouble() * 3.0;
            return new { Track = track, Score = score };
        })
        .OrderByDescending(x => x.Score)
        .Take(seed.TargetTrackCount)
        .Select(x => x.Track)
        .ToList();

        return Task.FromResult<IReadOnlyList<Track>>(scoredList);
    }
}
