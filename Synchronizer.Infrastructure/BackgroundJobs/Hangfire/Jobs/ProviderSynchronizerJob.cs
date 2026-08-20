using Hangfire;
using Microsoft.Extensions.Logging;

namespace Synchronizer.Infrastructure.BackgroundJobs.Hangfire.Jobs;

public sealed class ProviderSynchronizerJob(ILogger<ProviderSynchronizerJob> logger)
{
    [DisableConcurrentExecution(timeoutInSeconds:300)]
    public Task ExecutionAsync(int providerId)
    {
        logger.LogInformation($"Synchronizer Started({providerId})...");

        return Task.CompletedTask;
    }
}
