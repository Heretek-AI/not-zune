using System.Net.Http.Json;
using System.Text.Json.Serialization;

namespace NotZune.Infrastructure.External;

public class ArtistMetadataResult
{
    public string Name { get; set; } = string.Empty;
    public string? MusicBrainzId { get; set; }
    public string? Biography { get; set; }
    public string? ThumbnailUrl { get; set; }
    public List<string> BackgroundImageUrls { get; set; } = new();
}

public class ExternalMetadataService
{
    private readonly HttpClient _httpClient;

    public ExternalMetadataService(HttpClient? httpClient = null)
    {
        _httpClient = httpClient ?? new HttpClient();
        _httpClient.DefaultRequestHeaders.UserAgent.ParseAdd("NotZune/1.0 (contact@notzune.org)");
    }

    public async Task<ArtistMetadataResult?> FetchArtistMetadataAsync(string artistName, CancellationToken cancellationToken = default)
    {
        // Mock fallback / safe client query
        await Task.Delay(100, cancellationToken);

        return new ArtistMetadataResult
        {
            Name = artistName,
            Biography = $"{artistName} is a renowned musical artist cataloged in the Not-Zune library.",
            ThumbnailUrl = null,
            BackgroundImageUrls = new List<string>()
        };
    }
}
