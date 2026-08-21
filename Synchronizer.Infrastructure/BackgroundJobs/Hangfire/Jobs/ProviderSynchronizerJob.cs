using Hangfire;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Synchronizer.Application.Abstractions;
using Synchronizer.Domain.Aggregates.Providers.Entities;
using Synchronizer.Domain.Aggregates.Providers.Repositories;

namespace Synchronizer.Infrastructure.BackgroundJobs.Hangfire.Jobs;

public sealed class ProviderSynchronizerJob(
    ILogger<ProviderSynchronizerJob> logger,
    ISyncOrchestrationService syncOrchestrationService)
{
    [DisableConcurrentExecution(timeoutInSeconds: 300)]
    public async Task ExecutionAsync(int providerId, CancellationToken cancellation)
    {
        logger.LogInformation($"Synchronizer Started({providerId})...");



        var syncResult = await syncOrchestrationService.SyncProviderAsync(providerId, cancellation);

        if (!syncResult.IsSuccess)
        {
            logger.LogError(syncResult.Error);
            //throw new Exception(syncResult.Error);
        }

        logger.LogInformation($"{providerId} Synchronization Completed in {DateTime.Now}");
    }
}
