using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Avalonia.Headless.XUnit;
using NotZune.Application.Interfaces;
using NotZune.Application.Services;
using NotZune.Domain.Enums;
using NotZune.Domain.Models;
using NotZune.Infrastructure.Audio;
using NotZune.UI;
using NotZune.UI.ViewModels;
using NotZune.UI.Views;
using Xunit;

namespace NotZune.Tests.Application;

public class ZuneFeatureGapTests
{
    private class TestMediaLibraryService : IMediaLibraryService
    {
        public List<Track> Tracks { get; set; } = new();
        public List<PlayHistoryEntry> History { get; set; } = new();

        public Task<IReadOnlyList<Track>> GetAllTracksAsync() => Task.FromResult<IReadOnlyList<Track>>(Tracks);
        public Task<IReadOnlyList<Album>> GetAllAlbumsAsync() => Task.FromResult<IReadOnlyList<Album>>(new List<Album>());
        public Task<IReadOnlyList<Artist>> GetAllArtistsAsync() => Task.FromResult<IReadOnlyList<Artist>>(new List<Artist>());
        public Task<IReadOnlyList<Playlist>> GetAllPlaylistsAsync() => Task.FromResult<IReadOnlyList<Playlist>>(new List<Playlist>());
        public Task<IReadOnlyList<PlayHistoryEntry>> GetRecentHistoryAsync(int count = 20) => Task.FromResult<IReadOnlyList<PlayHistoryEntry>>(History);
        public Task<IReadOnlyList<Album>> GetRecentlyAddedAlbumsAsync(int count = 12) => Task.FromResult<IReadOnlyList<Album>>(new List<Album>());
        public Task<IReadOnlyList<Track>> SearchAsync(string query) => Task.FromResult<IReadOnlyList<Track>>(new List<Track>());
        public Task SetTrackRatingAsync(Guid trackId, HeartRating rating) => Task.CompletedTask;
        public Task ScanDirectoryAsync(string directoryPath, IProgress<double>? progress = null) => Task.CompletedTask;
        public Task ClearDemoDataAsync() => Task.CompletedTask;
#pragma warning disable CS0067
        public event EventHandler? LibraryUpdated;
#pragma warning restore CS0067
    }

    private class TestDeviceSyncService : IDeviceSyncService
    {
        public IReadOnlyList<ZuneDevice> ConnectedDevices => new List<ZuneDevice>();
        public Task StartMonitoringAsync(CancellationToken cancellationToken) => Task.CompletedTask;
        public Task SyncDeviceAsync(string serialNumber, IProgress<double>? progress = null) => Task.CompletedTask;
#pragma warning disable CS0067
        public event EventHandler<ZuneDevice>? DeviceConnected;
        public event EventHandler<string>? DeviceDisconnected;
#pragma warning restore CS0067
    }

    [Fact]
    public async Task UserStatsService_CalculatesProfile_AndTopArtistsCorrectly()
    {
        var lib = new TestMediaLibraryService();
        lib.History.Add(new PlayHistoryEntry
        {
            TrackTitle = "Subdivisions",
            ArtistName = "Rush",
            DurationPlayed = TimeSpan.FromMinutes(5)
        });
        lib.History.Add(new PlayHistoryEntry
        {
            TrackTitle = "Tom Sawyer",
            ArtistName = "Rush",
            DurationPlayed = TimeSpan.FromMinutes(4)
        });
        lib.History.Add(new PlayHistoryEntry
        {
            TrackTitle = "One More Time",
            ArtistName = "Daft Punk",
            DurationPlayed = TimeSpan.FromMinutes(5)
        });

        var statsService = new UserStatsService(lib);
        var profile = await statsService.GetProfileAsync();

        Assert.Equal("ZuneFan_2006", profile.ZuneTag);
        Assert.Equal(3, profile.TotalTracksPlayed);
        Assert.Equal(TimeSpan.FromMinutes(14), profile.TotalListeningTime);

        var topArtists = await statsService.GetTopArtistsAsync(5);
        Assert.NotEmpty(topArtists);
        Assert.Equal("Rush", topArtists[0].ArtistName);
        Assert.Equal(2, topArtists[0].PlayCount);
        Assert.Equal(66.7, topArtists[0].Percentage);
    }

    [Fact]
    public async Task UserStatsService_Badges_EvaluatesMilestones()
    {
        var lib = new TestMediaLibraryService();
        for (int i = 0; i < 50; i++)
        {
            lib.History.Add(new PlayHistoryEntry
            {
                TrackTitle = $"Track {i}",
                ArtistName = "Rush",
                DurationPlayed = TimeSpan.FromMinutes(3)
            });
        }

        var statsService = new UserStatsService(lib);
        var badges = await statsService.GetBadgesAsync();

        // Should unlock Early Adopter badge
        var pioneerBadge = badges.FirstOrDefault(b => b.Title == "Early Adopter");
        Assert.NotNull(pioneerBadge);
        Assert.True(pioneerBadge.IsUnlocked);

        // Should unlock Heavy Rotation badge (total plays >= 3)
        var heavyRotationBadge = badges.FirstOrDefault(b => b.Title == "Heavy Rotation");
        Assert.NotNull(heavyRotationBadge);
        Assert.True(heavyRotationBadge.IsUnlocked);
    }

    [Fact]
    public async Task PodcastService_SeededFeeds_CanBeLoadedAndPlayed()
    {
        var coordinator = new PlaybackQueueCoordinator();
        var podcastService = new PodcastService(coordinator);

        var podcasts = await podcastService.GetAllPodcastsAsync();
        Assert.NotEmpty(podcasts);
        Assert.Equal("All Songs Considered", podcasts[0].Title);
        Assert.NotEmpty(podcasts[0].Episodes);

        var firstEp = podcasts[0].Episodes[0];
        await podcastService.PlayEpisodeAsync(firstEp);

        Assert.True(firstEp.IsPlayed);
        Assert.NotNull(coordinator.CurrentTrack);
        Assert.Equal(firstEp.Title, coordinator.CurrentTrack.Title);
        Assert.Equal(firstEp.SeriesTitle, coordinator.CurrentTrack.ArtistName);
    }

    [Fact]
    public void SoundEffectService_CanToggleAndPlay()
    {
        var soundService = new SoundEffectService();
        Assert.True(soundService.SoundEffectsEnabled);

        soundService.SoundEffectsEnabled = false;
        Assert.False(soundService.SoundEffectsEnabled);

        // Verify calls do not throw even if audio hardware is absent
        soundService.PlaySyncComplete();
        soundService.PlayRipComplete();
        soundService.PlayDownloadComplete();
        soundService.PlayNotification();
    }

    [Fact]
    public void MainShellViewModel_CompactMode_TogglesAndFiresEvent()
    {
        var coordinator = new PlaybackQueueCoordinator();
        var smartDj = new SmartDJEngine();
        var lib = new TestMediaLibraryService();
        var dev = new TestDeviceSyncService();

        var shellVm = new MainShellViewModel(coordinator, lib, dev, smartDj);

        bool eventFired = false;
        bool compactState = false;
        shellVm.CompactModeChanged += (s, isCompact) =>
        {
            eventFired = true;
            compactState = isCompact;
        };

        Assert.False(shellVm.IsCompactMode);
        shellVm.ToggleCompactModeCommand.Execute(null);

        Assert.True(shellVm.IsCompactMode);
        Assert.True(eventFired);
        Assert.True(compactState);

        shellVm.ToggleCompactModeCommand.Execute(null);
        Assert.False(shellVm.IsCompactMode);
        Assert.False(compactState);
    }

    [Fact]
    public void MainShellViewModel_SocialPivot_SwitchesToZuneCard()
    {
        var coordinator = new PlaybackQueueCoordinator();
        var smartDj = new SmartDJEngine();
        var lib = new TestMediaLibraryService();
        var dev = new TestDeviceSyncService();

        var shellVm = new MainShellViewModel(coordinator, lib, dev, smartDj);

        shellVm.SelectPivotCommand.Execute(NavigationPivot.Social);
        Assert.IsType<ZuneCardViewModel>(shellVm.CurrentView);
        Assert.True(shellVm.IsSocialActive);

        // Toggle user card button
        shellVm.ToggleZuneCardCommand.Execute(null);
        Assert.Equal(NavigationPivot.Collection, shellVm.ActivePivot);

        shellVm.ToggleZuneCardCommand.Execute(null);
        Assert.Equal(NavigationPivot.Social, shellVm.ActivePivot);
    }

    [Fact]
    public void SettingsViewModel_BackgroundThemes_CanBeSelected()
    {
        var settingsVm = new SettingsViewModel();
        Assert.NotEmpty(settingsVm.BackgroundThemes);

        string? selectedUri = null;
        settingsVm.BackgroundArtChanged += (s, uri) => selectedUri = uri;

        var aurora = settingsVm.BackgroundThemes.First(t => t.Name.Contains("Aurora"));
        settingsVm.SelectBackgroundCommand.Execute(aurora);

        Assert.Equal(aurora, settingsVm.SelectedBackground);
        Assert.Equal(aurora.AssetUri, selectedUri);
    }

    [AvaloniaFact]
    public void ViewLocator_ResolvesZuneCardAndPodcasts()
    {
        var locator = new ViewLocator();
        var coordinator = new PlaybackQueueCoordinator();
        var lib = new TestMediaLibraryService();
        var stats = new UserStatsService(lib);
        var podService = new PodcastService(coordinator);

        var zuneCardVm = new ZuneCardViewModel(stats);
        var podcastsVm = new PodcastsViewModel(podService);

        Assert.True(locator.Match(zuneCardVm));
        Assert.IsType<ZuneCardView>(locator.Build(zuneCardVm));

        Assert.True(locator.Match(podcastsVm));
        Assert.IsType<PodcastsView>(locator.Build(podcastsVm));
    }
}
