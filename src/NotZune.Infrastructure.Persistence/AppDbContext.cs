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

        modelBuilder.Entity<Playlist>(entity =>
        {
            entity.HasKey(p => p.Id);
        });

        modelBuilder.Entity<PlayHistoryEntry>(entity =>
        {
            entity.HasKey(h => h.Id);
            entity.HasIndex(h => h.PlayedAtUtc);
        });
    }
}
