namespace NotZune.Domain.Models;

public class ZuneProfile
{
    public string ZuneTag { get; set; } = "ZuneUser";
    public string StatusMessage { get; set; } = "Listening to great music on Not-Zune";
    public DateTime MemberSinceUtc { get; set; } = DateTime.UtcNow.AddMonths(-6);
    public int TotalTracksPlayed { get; set; }
    public TimeSpan TotalListeningTime { get; set; }
    public string AvatarUri { get; set; } = "avares://NotZune.UI/Assets/Zune/Branding/ZUNEUSER.PNG";
}

public class TopArtistStat
{
    public string ArtistName { get; set; } = string.Empty;
    public int PlayCount { get; set; }
    public double Percentage { get; set; }
}

public class ZuneBadge
{
    public string Id { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Category { get; set; } = "Milestone";
    public bool IsUnlocked { get; set; }
    public DateTime? UnlockedAtUtc { get; set; }
    public string IconUri { get; set; } = "avares://NotZune.UI/Assets/Zune/Social/PROFILE.BADGE.SEAL.PNG";
}
