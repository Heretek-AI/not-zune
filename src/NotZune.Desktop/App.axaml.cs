using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NotZune.Application.Interfaces;
using NotZune.Application.Services;
using NotZune.Domain.Enums;
using NotZune.Domain.Models;
using NotZune.Infrastructure.Audio;
using NotZune.Infrastructure.Devices;
using NotZune.Infrastructure.Persistence;
using NotZune.UI.ViewModels;

namespace NotZune.Desktop;

public partial class App : Avalonia.Application
{
    private ServiceProvider? _serviceProvider;

    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        var services = new ServiceCollection();
        ConfigureServices(services);
        _serviceProvider = services.BuildServiceProvider();

        // Seed initial demo data
        SeedDemoData(_serviceProvider);

        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            var shellVm = _serviceProvider.GetRequiredService<MainShellViewModel>();
            desktop.MainWindow = new MainWindow
            {
                DataContext = shellVm
            };
        }

        base.OnFrameworkInitializationCompleted();
    }

    private static void ConfigureServices(IServiceCollection services)
    {
        // 1. Persistence
        services.AddDbContextFactory<AppDbContext>(options =>
        {
            var dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "NotZune", "notzune.db");
            var dir = Path.GetDirectoryName(dbPath);
            if (!string.IsNullOrEmpty(dir) && !Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }
            options.UseSqlite($"Data Source={dbPath}");
        });

        // 2. Application Core Services
        services.AddSingleton<IPlayerCoordinator, PlaybackQueueCoordinator>();
        services.AddSingleton<IMediaLibraryService, MediaLibraryService>();
        services.AddSingleton<IDeviceSyncService, ZuneDeviceSyncService>();
        services.AddSingleton<ISmartDJService, SmartDJEngine>();

        // 3. Audio & Hardware Subsystems
        services.AddSingleton<AudioEngine>();
        services.AddSingleton<ZuneUsbHttpInterceptor>();

        // 4. ViewModels
        services.AddSingleton<MainShellViewModel>();
    }

    private static void SeedDemoData(IServiceProvider provider)
    {
        try
        {
            var factory = provider.GetRequiredService<IDbContextFactory<AppDbContext>>();
            using var ctx = factory.CreateDbContext();
            ctx.Database.EnsureCreated();

            if (!ctx.Tracks.Any())
            {
                var rush = new Artist { Name = "Rush", SortName = "Rush" };
                var daftPunk = new Artist { Name = "Daft Punk", SortName = "Daft Punk" };
                var pinkFloyd = new Artist { Name = "Pink Floyd", SortName = "Pink Floyd" };

                ctx.Artists.AddRange(rush, daftPunk, pinkFloyd);

                var albumSignals = new Album { Title = "Signals", ArtistId = rush.Id, ArtistName = "Rush", Year = 1982, Genre = "Progressive Rock" };
                var albumDiscovery = new Album { Title = "Discovery", ArtistId = daftPunk.Id, ArtistName = "Daft Punk", Year = 2001, Genre = "Electronic" };
                var albumDarkSide = new Album { Title = "The Dark Side of the Moon", ArtistId = pinkFloyd.Id, ArtistName = "Pink Floyd", Year = 1973, Genre = "Progressive Rock" };

                ctx.Albums.AddRange(albumSignals, albumDiscovery, albumDarkSide);

                var track1 = new Track
                {
                    Title = "Subdivisions",
                    ArtistId = rush.Id,
                    ArtistName = "Rush",
                    AlbumId = albumSignals.Id,
                    AlbumTitle = "Signals",
                    TrackNumber = 1,
                    Duration = TimeSpan.FromMinutes(5) + TimeSpan.FromSeconds(34),
                    Year = 1982,
                    Genre = "Progressive Rock",
                    Rating = HeartRating.Favorite
                };

                var track2 = new Track
                {
                    Title = "Voyager",
                    ArtistId = daftPunk.Id,
                    ArtistName = "Daft Punk",
                    AlbumId = albumDiscovery.Id,
                    AlbumTitle = "Discovery",
                    TrackNumber = 6,
                    Duration = TimeSpan.FromMinutes(3) + TimeSpan.FromSeconds(47),
                    Year = 2001,
                    Genre = "Electronic",
                    Rating = HeartRating.Favorite
                };

                var track3 = new Track
                {
                    Title = "Time",
                    ArtistId = pinkFloyd.Id,
                    ArtistName = "Pink Floyd",
                    AlbumId = albumDarkSide.Id,
                    AlbumTitle = "The Dark Side of the Moon",
                    TrackNumber = 4,
                    Duration = TimeSpan.FromMinutes(6) + TimeSpan.FromSeconds(53),
                    Year = 1973,
                    Genre = "Progressive Rock",
                    Rating = HeartRating.None
                };

                ctx.Tracks.AddRange(track1, track2, track3);

                // History
                ctx.PlayHistory.Add(new PlayHistoryEntry
                {
                    TrackId = track1.Id,
                    TrackTitle = track1.Title,
                    ArtistName = track1.ArtistName,
                    AlbumTitle = track1.AlbumTitle,
                    PlayedAtUtc = DateTime.UtcNow.AddHours(-1),
                    DurationPlayed = track1.Duration,
                    Completed = true
                });

                ctx.PlayHistory.Add(new PlayHistoryEntry
                {
                    TrackId = track2.Id,
                    TrackTitle = track2.Title,
                    ArtistName = track2.ArtistName,
                    AlbumTitle = track2.AlbumTitle,
                    PlayedAtUtc = DateTime.UtcNow.AddMinutes(-25),
                    DurationPlayed = track2.Duration,
                    Completed = true
                });

                ctx.SaveChanges();
            }
        }
        catch
        {
            // Non-critical demo seeding fallback
        }
    }
}
