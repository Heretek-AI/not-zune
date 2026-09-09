using System.Text.Json;

namespace NotZune.Infrastructure.External;

/// <summary>
/// Minimal MusicBrainz Web Service v2 client (search endpoints only, JSON format).
/// </summary>
public sealed class MusicBrainzClient
{
    private readonly HttpClient _http;

    public MusicBrainzClient(HttpClient httpClient)
    {
        _http = httpClient;
    }

    public async Task<(string? MbId, int Score)?> SearchArtistAsync(string artistName, CancellationToken cancellationToken)
    {
        var url = $"https://musicbrainz.org/ws/2/artist/?query=artist:%22{Uri.EscapeDataString(artistName)}%22&fmt=json&limit=1";
        using var document = await GetJsonDocumentAsync(url, cancellationToken).ConfigureAwait(false);
        if (document == null)
        {
            return null;
        }

        if (!document.RootElement.TryGetProperty("artists", out var artists) || artists.GetArrayLength() == 0)
        {
            return null;
        }

        var first = artists[0];
        if (!first.TryGetProperty("id", out var idElement) || idElement.ValueKind != JsonValueKind.String)
        {
            return null;
        }

        var score = 0;
        if (first.TryGetProperty("score", out var scoreElement) && scoreElement.TryGetInt32(out var parsedScore))
        {
            score = parsedScore;
        }

        return (idElement.GetString(), score);
    }

    public async Task<(string? MbId, string? Title, int Score)?> SearchReleaseGroupAsync(string artistName, string albumTitle, CancellationToken cancellationToken)
    {
        var query = $"releasegroup:%22{Uri.EscapeDataString(albumTitle)}%22%20AND%20artist:%22{Uri.EscapeDataString(artistName)}%22";
        var url = $"https://musicbrainz.org/ws/2/release-group/?query={query}&fmt=json&limit=5";
        using var document = await GetJsonDocumentAsync(url, cancellationToken).ConfigureAwait(false);
        if (document == null)
        {
            return null;
        }

        if (!document.RootElement.TryGetProperty("release-groups", out var groups) || groups.GetArrayLength() == 0)
        {
            return null;
        }

        (string MbId, string Title, int Score) best = default;
        foreach (var group in groups.EnumerateArray())
        {
            var id = group.TryGetProperty("id", out var idElement) && idElement.ValueKind == JsonValueKind.String ? idElement.GetString() : null;
            if (string.IsNullOrEmpty(id))
            {
                continue;
            }

            var title = group.TryGetProperty("title", out var titleElement) && titleElement.ValueKind == JsonValueKind.String ? titleElement.GetString() : null;
            var score = group.TryGetProperty("score", out var scoreElement) && scoreElement.TryGetInt32(out var parsedScore) ? parsedScore : 0;

            if (best.MbId == null || score > best.Score)
            {
                best = (id, title ?? string.Empty, score);
            }
        }

        return best.MbId == null ? null : best;
    }

    private async Task<JsonDocument?> GetJsonDocumentAsync(string url, CancellationToken cancellationToken)
    {
        try
        {
            using var response = await _http.GetAsync(url, cancellationToken).ConfigureAwait(false);
            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            var json = await response.Content.ReadAsStringAsync(cancellationToken).ConfigureAwait(false);
            return JsonDocument.Parse(json);
        }
        catch (Exception ex) when (ex is HttpRequestException or JsonException)
        {
            return null;
        }
    }
}
