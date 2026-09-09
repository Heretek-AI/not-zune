using NotZune.Domain.Models;

namespace NotZune.Application.Interfaces;

public interface IVideoLibraryService
{
    Task<IReadOnlyList<Video>> GetAllVideosAsync();

    Task ScanDirectoryAsync(string directoryPath, IProgress<double>? progress = null);

    Task MarkPlayedAsync(Guid videoId);
}
