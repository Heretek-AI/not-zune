namespace NotZune.Application;

/// <summary>
/// Central application identity, used by the About page, the What's New dialog, and window titles.
/// </summary>
public static class AppInfo
{
    public const string Version = "1.1.0";

    public static string VersionDisplay => $"Not-Zune v{Version} (True-Parity Engine)";

    /// <summary>Highlights shown in the What's New dialog when the version changes.</summary>
    public static IReadOnlyList<string> WhatsNewHighlights { get; } = new List<string>
    {
        "Real audio playback — BASS engine with gapless chaining and equal-power crossfade",
        "ReplayGain volume leveling and a live FFT spectrum visualizer",
        "Smart playlists — build rule-based auto playlists that grow with your collection",
        "Find Album Info now matches tracks on MusicBrainz with a review dialog",
        "Podcasts are fully audible — episodes stream over the internet",
        "Search autocomplete, back-stack navigation (Escape / back arrow), and Mixview like/hate/info/add tiles"
    };
}
