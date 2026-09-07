using NotZune.Domain.Models;

namespace NotZune.Application.Interfaces;

public interface IUserStatsService
{
    Task<ZuneProfile> GetProfileAsync();
    Task<IReadOnlyList<TopArtistStat>> GetTopArtistsAsync(int count = 5);
    Task<IReadOnlyList<ZuneBadge>> GetBadgesAsync();
    Task RecordTrackPlayedAsync(Track track);
}
