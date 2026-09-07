using Microsoft.EntityFrameworkCore;
using NotZune.Application.Interfaces;
using NotZune.Domain.Enums;
using NotZune.Domain.Models;

namespace NotZune.Infrastructure.Persistence;

public class MediaLibraryService : IMediaLibraryService
{
    private readonly IDbContextFactory<AppDbContext> _contextFactory;

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
        }
    }

    public async Task ScanDirectoryAsync(string directoryPath, IProgress<double>? progress = null)
    {
        if (!Directory.Exists(directoryPath)) return;

        var supportedExtensions = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
        {
            ".mp3", ".flac", ".m4a", ".ogg", ".wma", ".wav"
        };

        var files = Directory.EnumerateFiles(directoryPath, "*.*", SearchOption.AllDirectories)
            .Where(f => supportedExtensions.Contains(Path.GetExtension(f)))
            .ToList();

        if (files.Count == 0) return;

        await using var ctx = await _contextFactory.CreateDbContextAsync();
        int total = files.Count;
        int current = 0;

        foreach (var file in files)
        {
            var fileName = Path.GetFileNameWithoutExtension(file);
            var exists = await ctx.Tracks.AnyAsync(t => t.FilePath == file);
            if (!exists)
            {
                var track = new Track
                {
                    Title = fileName,
                    FilePath = file,
                    ArtistName = "Unknown Artist",
                    AlbumTitle = "Unknown Album",
                    Duration = TimeSpan.FromMinutes(3)
                };
                ctx.Tracks.Add(track);
            }

            current++;
            progress?.Report((double)current / total);
        }

        await ctx.SaveChangesAsync();
    }
}
