using Microsoft.EntityFrameworkCore;
using NotZune.Domain.Enums;
using NotZune.Domain.Models;

namespace NotZune.Infrastructure.Persistence;

public class AppDbContext : DbContext
{
    public DbSet<Track> Tracks => Set<Track>();
    public DbSet<Album> Albums => Set<Album>();
    public DbSet<Artist> Artists => Set<Artist>();
    public DbSet<Playlist> Playlists => Set<Playlist>();
    public DbSet<PlayHistoryEntry> PlayHistory => Set<PlayHistoryEntry>();
    public DbSet<SmartPlaylist> SmartPlaylists => Set<SmartPlaylist>();

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Track>(entity =>
        {
            entity.HasKey(t => t.Id);
            entity.HasIndex(t => t.Title);
            entity.HasIndex(t => t.ArtistName);
            entity.HasIndex(t => t.AlbumTitle);
            entity.HasIndex(t => t.Rating);
        });

        modelBuilder.Entity<Album>(entity =>
        {
            entity.HasKey(a => a.Id);
            entity.HasIndex(a => a.Title);
            entity.HasIndex(a => a.ArtistName);
        });

        modelBuilder.Entity<Artist>(entity =>
        {
            entity.HasKey(a => a.Id);
            entity.HasIndex(a => a.Name);
        });

        var guidListComparer = new Microsoft.EntityFrameworkCore.ChangeTracking.ValueComparer<List<Guid>>(
            (c1, c2) => (c1 == null && c2 == null) || (c1 != null && c2 != null && c1.SequenceEqual(c2)),
            c => c.Aggregate(0, (a, v) => HashCode.Combine(a, v.GetHashCode())),
            c => c.ToList());

        modelBuilder.Entity<Playlist>(entity =>
        {
            entity.HasKey(p => p.Id);
            entity.Property(p => p.TrackIds)
                .HasConversion(
                    v => string.Join(',', v),
                    v => string.IsNullOrEmpty(v) ? new List<Guid>() : v.Split(',', StringSplitOptions.RemoveEmptyEntries).Select(Guid.Parse).ToList())
                .Metadata.SetValueComparer(guidListComparer);
        });

        modelBuilder.Entity<PlayHistoryEntry>(entity =>
        {
            entity.HasKey(h => h.Id);
            entity.HasIndex(h => h.PlayedAtUtc);
        });

        modelBuilder.Entity<SmartPlaylist>(entity =>
        {
            entity.HasKey(p => p.Id);
            entity.Property(p => p.Rules)
                .HasConversion(
                    v => System.Text.Json.JsonSerializer.Serialize(v, (System.Text.Json.JsonSerializerOptions?)null),
                    v => System.Text.Json.JsonSerializer.Deserialize<List<SmartPlaylistRule>>(string.IsNullOrEmpty(v) ? "[]" : v, (System.Text.Json.JsonSerializerOptions?)null) ?? new List<SmartPlaylistRule>());
        });
    }
}
