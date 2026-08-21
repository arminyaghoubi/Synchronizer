using Synchronizer.Domain.Aggregates.Providers.Entities;
using Synchronizer.Domain.Common;

namespace Synchronizer.Application.Abstractions;

public interface ISyncOrchestrationService
{
    Task<Result> SyncProviderAsync(int providerId, CancellationToken cancellation);
}
