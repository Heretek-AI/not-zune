using NotZune.Domain.Models;

namespace NotZune.Application.Interfaces;

public interface ISyncGroupService
{
    Task<SyncGroup?> GetForDeviceAsync(string deviceSerialNumber);

    Task SaveAsync(SyncGroup group);

    Task DeleteAsync(Guid groupId);
}
