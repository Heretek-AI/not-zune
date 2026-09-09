using System;
using System.Threading;
using System.Threading.Tasks;
using NotZune.Domain.Models;

namespace NotZune.Application.Interfaces;

public interface IMixviewService
{
    Task<MixConstellation> GenerateConstellationAsync(
        string seedName, 
        MixNodeType seedType, 
        Guid? seedId = null, 
        CancellationToken cancellationToken = default);
}
