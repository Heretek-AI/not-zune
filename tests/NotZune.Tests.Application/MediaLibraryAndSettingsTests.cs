using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NotZune.Application.Interfaces;
using NotZune.Domain.Enums;
using NotZune.Domain.Models;
using NotZune.Infrastructure.Persistence;
using NotZune.UI.ViewModels;
using Xunit;

namespace NotZune.Tests.Application;

public class MediaLibraryAndSettingsTests : IDisposable
{
    private readonly string _dbPath;
    private readonly IDbContextFactory<AppDbContext> _dbFactory;

    public MediaLibraryAndSettingsTests()
    {
        _dbPath = Path.Combine(Path.GetTempPath(), $"notzune_test_{Guid.NewGuid():N}.db");
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseSqlite($"Data Source={_dbPath}")
            .Options;

        _dbFactory = new TestDbContextFactory(options);

        using var ctx = _dbFactory.CreateDbContext();
        ctx.Database.EnsureCreated();
    }

    public void Dispose()
    {
        if (File.Exists(_dbPath))
        {
            try { File.Delete(_dbPath); } catch { }
        }
    }

    private class TestDbContextFactory : IDbContextFactory<AppDbContext>
    {
        private readonly DbContextOptions<AppDbContext> _options;
        public TestDbContextFactory(DbContextOptions<AppDbContext> options) => _options = options;
        public AppDbContext CreateDbContext() => new AppDbContext(_options);
    }

    private class FakeFolderPickerService : IFolderPickerService
    {
        public string? SelectedPath { get; set; }
        public Task<string?> PickFolderAsync(string title = "Select Music Collection Folder")
        {
            return Task.FromResult(SelectedPath);
        }
    }

    [Fact]
    public async Task ClearDemoDataAsync_PurgesPlaceholderTracks_PreservesRealTracks()
    {
        // Arrange
        var service = new MediaLibraryService(_dbFactory);

        using (var ctx = _dbFactory.CreateDbContext())
        {
            var demoArtist = new Artist { Name = "Demo Rush" };
            var realArtist = new Artist { Name = "Real Daft Punk" };
            ctx.Artists.AddRange(demoArtist, realArtist);

            var demoAlbum = new Album { Title = "Demo Signals", ArtistId = demoArtist.Id, ArtistName = demoArtist.Name };
            var realAlbum = new Album { Title = "Real Discovery", ArtistId = realArtist.Id, ArtistName = realArtist.Name };
            ctx.Albums.AddRange(demoAlbum, realAlbum);

            // Placeholder track has empty FilePath
            var demoTrack = new Track
            {
                Title = "Subdivisions (Demo)",
                ArtistId = demoArtist.Id,
                ArtistName = demoArtist.Name,
                AlbumId = demoAlbum.Id,
                AlbumTitle = demoAlbum.Title,
                FilePath = string.Empty
            };

            // Real track has valid FilePath
            var realTrack = new Track
            {
                Title = "One More Time (Real)",
                ArtistId = realArtist.Id,
                ArtistName = realArtist.Name,
                AlbumId = realAlbum.Id,
                AlbumTitle = realAlbum.Title,
                FilePath = "/music/one_more_time.mp3"
            };

            ctx.Tracks.AddRange(demoTrack, realTrack);

            ctx.PlayHistory.Add(new PlayHistoryEntry
            {
                TrackId = demoTrack.Id,
                TrackTitle = demoTrack.Title,
                ArtistName = demoTrack.ArtistName
            });

            await ctx.SaveChangesAsync();
        }

        bool eventFired = false;
        service.LibraryUpdated += (_, _) => eventFired = true;

        // Act
        await service.ClearDemoDataAsync();

        // Assert
        Assert.True(eventFired);

        var tracks = await service.GetAllTracksAsync();
        Assert.Single(tracks);
        Assert.Equal("One More Time (Real)", tracks[0].Title);
        Assert.Equal("/music/one_more_time.mp3", tracks[0].FilePath);

        var artists = await service.GetAllArtistsAsync();
        Assert.Single(artists);
        Assert.Equal("Real Daft Punk", artists[0].Name);

        var albums = await service.GetAllAlbumsAsync();
        Assert.Single(albums);
        Assert.Equal("Real Discovery", albums[0].Title);
    }

    [Fact]
    public async Task ScanDirectoryAsync_ParsesAudioFiles_AndExtractsMetadata()
    {
        // Arrange
        var tempMusicDir = Path.Combine(Path.GetTempPath(), $"music_scan_{Guid.NewGuid():N}");
        Directory.CreateDirectory(tempMusicDir);

        try
        {
            // Create audio test files (using fallback parsing test: "Artist - Title.mp3")
            var file1 = Path.Combine(tempMusicDir, "Pink Floyd - Time.mp3");
            var file2 = Path.Combine(tempMusicDir, "Fleetwood Mac - Dreams.flac");
            await File.WriteAllBytesAsync(file1, new byte[128]);
            await File.WriteAllBytesAsync(file2, new byte[128]);

            var service = new MediaLibraryService(_dbFactory);

            bool updatedFired = false;
            service.LibraryUpdated += (_, _) => updatedFired = true;

            double reportedProgress = 0.0;
            var progress = new Progress<double>(p => reportedProgress = p);

            // Act
            await service.ScanDirectoryAsync(tempMusicDir, progress);

            // Assert
            Assert.True(updatedFired);

            var tracks = await service.GetAllTracksAsync();
            Assert.Equal(2, tracks.Count);

            var timeTrack = tracks.FirstOrDefault(t => t.Title == "Time");
            Assert.NotNull(timeTrack);
            Assert.Equal("Pink Floyd", timeTrack.ArtistName);

            var dreamsTrack = tracks.FirstOrDefault(t => t.Title == "Dreams");
            Assert.NotNull(dreamsTrack);
            Assert.Equal("Fleetwood Mac", dreamsTrack.ArtistName);

            var artists = await service.GetAllArtistsAsync();
            Assert.Contains(artists, a => a.Name == "Pink Floyd");
            Assert.Contains(artists, a => a.Name == "Fleetwood Mac");
        }
        finally
        {
            if (Directory.Exists(tempMusicDir))
            {
                try { Directory.Delete(tempMusicDir, true); } catch { }
            }
        }
    }

    [Fact]
    public async Task SettingsViewModel_SelectFolderCommand_ExecutesAndSetsPath()
    {
        // Arrange
        var tempFolder = Path.Combine(Path.GetTempPath(), $"settings_test_{Guid.NewGuid():N}");
        Directory.CreateDirectory(tempFolder);

        try
        {
            var fakePicker = new FakeFolderPickerService { SelectedPath = tempFolder };
            var service = new MediaLibraryService(_dbFactory);
            var vm = new SettingsViewModel(null, fakePicker, service);

            // Act
            await ((CommunityToolkit.Mvvm.Input.IAsyncRelayCommand)vm.SelectFolderCommand).ExecuteAsync(null);

            // Assert
            Assert.Equal(tempFolder, vm.MusicFolderPath);
            Assert.Equal("Library scan complete.", vm.ScanStatusText);
            Assert.False(vm.IsScanning);
        }
        finally
        {
            if (Directory.Exists(tempFolder))
            {
                try { Directory.Delete(tempFolder, true); } catch { }
            }
        }
    }

    [Fact]
    public async Task SettingsViewModel_ClearDemoLibraryCommand_ClearsDataAndSetsStatus()
    {
        // Arrange
        var service = new MediaLibraryService(_dbFactory);
        var vm = new SettingsViewModel(null, null, service);

        // Act
        await ((CommunityToolkit.Mvvm.Input.IAsyncRelayCommand)vm.ClearDemoLibraryCommand).ExecuteAsync(null);

        // Assert
        Assert.Equal("Demo placeholder data cleared.", vm.ScanStatusText);
        Assert.True(vm.HasScanStatus);
    }
}
