using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NotZune.Application.Interfaces;
using NotZune.Domain.Enums;
using NotZune.Domain.Models;

namespace NotZune.Infrastructure.Persistence;

public class MediaLibraryService : IMediaLibraryService
{
    private readonly IDbContextFactory<AppDbContext> _contextFactory;

    public event EventHandler? LibraryUpdated;

    public MediaLibraryService(IDbContextFactory<AppDbContext> contextFactory)
    {
        _contextFactory = contextFactory;
    }

    public async Task<IReadOnlyList<Track>> GetAllTracksAsync()
    {
        await using var ctx = await _contextFactory.CreateDbContextAsync();
        return await ctx.Tracks.OrderBy(t => t.Title).ToListAsync();
    }

    public async Task<IReadOnlyList<Album>> GetAllAlbumsAsync()
    {
        await using var ctx = await _contextFactory.CreateDbContextAsync();
        return await ctx.Albums.Include(a => a.Tracks).OrderBy(a => a.Title).ToListAsync();
    }

    public async Task<IReadOnlyList<Artist>> GetAllArtistsAsync()
    {
        await using var ctx = await _contextFactory.CreateDbContextAsync();
        return await ctx.Artists.OrderBy(a => a.Name).ToListAsync();
    }

    public async Task<IReadOnlyList<Playlist>> GetAllPlaylistsAsync()
    {
        await using var ctx = await _contextFactory.CreateDbContextAsync();
        return await ctx.Playlists.OrderBy(p => p.Name).ToListAsync();
    }

    public async Task<IReadOnlyList<PlayHistoryEntry>> GetRecentHistoryAsync(int count = 20)
    {
        await using var ctx = await _contextFactory.CreateDbContextAsync();
        return await ctx.PlayHistory
            .OrderByDescending(h => h.PlayedAtUtc)
            .Take(count)
            .ToListAsync();
    }

    public async Task<IReadOnlyList<Album>> GetRecentlyAddedAlbumsAsync(int count = 12)
    {
        await using var ctx = await _contextFactory.CreateDbContextAsync();
        return await ctx.Albums
            .OrderByDescending(a => a.Year)
            .Take(count)
            .ToListAsync();
    }

    public async Task<IReadOnlyList<Track>> SearchAsync(string query)
    {
        if (string.IsNullOrWhiteSpace(query))
        {
            return await GetAllTracksAsync();
        }

        await using var ctx = await _contextFactory.CreateDbContextAsync();
        var q = query.Trim().ToLower();

        return await ctx.Tracks
            .Where(t => t.Title.ToLower().Contains(q) ||
                        t.ArtistName.ToLower().Contains(q) ||
                        t.AlbumTitle.ToLower().Contains(q) ||
                        t.Genre.ToLower().Contains(q))
            .OrderBy(t => t.Title)
            .Take(100)
            .ToListAsync();
    }

    public async Task SetTrackRatingAsync(Guid trackId, HeartRating rating)
    {
        await using var ctx = await _contextFactory.CreateDbContextAsync();
        var track = await ctx.Tracks.FindAsync(trackId);
        if (track != null)
        {
            track.Rating = rating;
            await ctx.SaveChangesAsync();
            LibraryUpdated?.Invoke(this, EventArgs.Empty);
        }
    }

    public async Task ClearDemoDataAsync()
    {
        await using var ctx = await _contextFactory.CreateDbContextAsync();
        
        // Remove tracks with null or empty FilePath (placeholders)
        var demoTracks = await ctx.Tracks.Where(t => string.IsNullOrEmpty(t.FilePath)).ToListAsync();
        if (demoTracks.Count > 0)
        {
            var demoTrackIds = demoTracks.Select(t => t.Id).ToHashSet();
            
            // Remove demo play history referencing these tracks
            var demoHistory = await ctx.PlayHistory.Where(h => demoTrackIds.Contains(h.TrackId)).ToListAsync();
            ctx.PlayHistory.RemoveRange(demoHistory);

            ctx.Tracks.RemoveRange(demoTracks);
        }

        // Clean up empty demo albums that have no real tracks
        var emptyAlbums = await ctx.Albums.Include(a => a.Tracks)
            .Where(a => !a.Tracks.Any(t => !string.IsNullOrEmpty(t.FilePath)))
            .ToListAsync();
        ctx.Albums.RemoveRange(emptyAlbums);

        // Clean up empty demo artists that have no remaining albums
        var emptyArtists = await ctx.Artists
            .Where(a => !ctx.Tracks.Any(t => t.ArtistId == a.Id && !string.IsNullOrEmpty(t.FilePath)))
            .ToListAsync();
        ctx.Artists.RemoveRange(emptyArtists);

        await ctx.SaveChangesAsync();
        LibraryUpdated?.Invoke(this, EventArgs.Empty);
    }

    public async Task ScanDirectoryAsync(string directoryPath, IProgress<double>? progress = null)
    {
        if (!Directory.Exists(directoryPath)) return;

        var supportedExtensions = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
        {
            ".mp3", ".flac", ".m4a", ".ogg", ".wma", ".wav", ".aac", ".opus"
        };

        var files = Directory.EnumerateFiles(directoryPath, "*.*", SearchOption.AllDirectories)
            .Where(f => supportedExtensions.Contains(Path.GetExtension(f)))
            .ToList();

        if (files.Count == 0) return;

        await using var ctx = await _contextFactory.CreateDbContextAsync();

        // Check if library currently only contains placeholder demo tracks
        var hasRealTracks = await ctx.Tracks.AnyAsync(t => !string.IsNullOrEmpty(t.FilePath));
        if (!hasRealTracks)
        {
            // Clear placeholder demo tracks when scanning the user's first real library
            var demoTracks = await ctx.Tracks.Where(t => string.IsNullOrEmpty(t.FilePath)).ToListAsync();
            var demoAlbums = await ctx.Albums.ToListAsync();
            var demoArtists = await ctx.Artists.ToListAsync();
            var demoHistory = await ctx.PlayHistory.ToListAsync();

            ctx.PlayHistory.RemoveRange(demoHistory);
            ctx.Tracks.RemoveRange(demoTracks);
            ctx.Albums.RemoveRange(demoAlbums);
            ctx.Artists.RemoveRange(demoArtists);
            await ctx.SaveChangesAsync();
        }

        // Cache existing artists and albums to minimize DB roundtrips
        var existingArtists = (await ctx.Artists.ToListAsync())
            .GroupBy(a => a.Name.Trim().ToLower())
            .ToDictionary(g => g.Key, g => g.First());

        var existingAlbums = (await ctx.Albums.ToListAsync())
            .GroupBy(a => $"{a.ArtistName.Trim().ToLower()}||{a.Title.Trim().ToLower()}")
            .ToDictionary(g => g.Key, g => g.First());

        var existingFilePaths = (await ctx.Tracks
            .Where(t => !string.IsNullOrEmpty(t.FilePath))
            .Select(t => t.FilePath)
            .ToListAsync())
            .ToHashSet(StringComparer.OrdinalIgnoreCase);

        int total = files.Count;
        int current = 0;

        foreach (var file in files)
        {
            current++;
            progress?.Report((double)current / total);

            if (existingFilePaths.Contains(file)) continue;

            string title = Path.GetFileNameWithoutExtension(file);
            string artistName = "Unknown Artist";
            string albumTitle = "Unknown Album";
            int year = DateTime.UtcNow.Year;
            string genre = "Music";
            int trackNumber = 1;
            TimeSpan duration = TimeSpan.FromMinutes(3);

            try
            {
                using var tagFile = TagLib.File.Create(file);
                if (tagFile.Tag != null)
                {
                    if (!string.IsNullOrWhiteSpace(tagFile.Tag.Title))
                        title = tagFile.Tag.Title.Trim();

                    if (!string.IsNullOrWhiteSpace(tagFile.Tag.FirstPerformer))
                        artistName = tagFile.Tag.FirstPerformer.Trim();
                    else if (!string.IsNullOrWhiteSpace(tagFile.Tag.FirstAlbumArtist))
                        artistName = tagFile.Tag.FirstAlbumArtist.Trim();

                    if (!string.IsNullOrWhiteSpace(tagFile.Tag.Album))
                        albumTitle = tagFile.Tag.Album.Trim();

                    if (tagFile.Tag.Year > 0)
                        year = (int)tagFile.Tag.Year;

                    if (!string.IsNullOrWhiteSpace(tagFile.Tag.FirstGenre))
                        genre = tagFile.Tag.FirstGenre.Trim();

                    if (tagFile.Tag.Track > 0)
                        trackNumber = (int)tagFile.Tag.Track;
                }

                if (tagFile.Properties != null && tagFile.Properties.Duration > TimeSpan.Zero)
                {
                    duration = tagFile.Properties.Duration;
                }
            }
            catch
            {
                // Fallback: try parsing "Artist - Title.mp3" or "01 Title.mp3" from filename
                var baseName = Path.GetFileNameWithoutExtension(file);
                var parts = baseName.Split('-', 2);
                if (parts.Length == 2)
                {
                    artistName = parts[0].Trim();
                    title = parts[1].Trim();
                }
            }

            // 1. Ensure Artist exists
            var artistKey = artistName.Trim().ToLower();
            if (!existingArtists.TryGetValue(artistKey, out var artist))
            {
                artist = new Artist
                {
                    Name = artistName,
                    SortName = artistName
                };
                ctx.Artists.Add(artist);
                existingArtists[artistKey] = artist;
            }

            // 2. Ensure Album exists
            var albumKey = $"{artistName.Trim().ToLower()}||{albumTitle.Trim().ToLower()}";
            if (!existingAlbums.TryGetValue(albumKey, out var album))
            {
                album = new Album
                {
                    Title = albumTitle,
                    ArtistId = artist.Id,
                    ArtistName = artist.Name,
                    Year = year,
                    Genre = genre
                };
                ctx.Albums.Add(album);
                existingAlbums[albumKey] = album;
            }

            // 3. Create Track
            var track = new Track
            {
                Title = title,
                FilePath = file,
                ArtistId = artist.Id,
                ArtistName = artist.Name,
                AlbumId = album.Id,
                AlbumTitle = album.Title,
                TrackNumber = trackNumber,
                Duration = duration,
                Year = year,
                Genre = genre
            };

            ctx.Tracks.Add(track);
            existingFilePaths.Add(file);

            // Save in batches of 50 to maintain performance
            if (current % 50 == 0)
            {
                await ctx.SaveChangesAsync();
            }
        }

        await ctx.SaveChangesAsync();
        LibraryUpdated?.Invoke(this, EventArgs.Empty);
    }
}
