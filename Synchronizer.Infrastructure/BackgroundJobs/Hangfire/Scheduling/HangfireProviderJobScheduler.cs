using Hangfire;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Synchronizer.Domain.Aggregates.Providers.Repositories;
using Synchronizer.Infrastructure.BackgroundJobs.Hangfire.Jobs;

namespace Synchronizer.Infrastructure.BackgroundJobs.Hangfire.Scheduling;

public sealed class HangfireProviderJobScheduler(
    IServiceScopeFactory scopeFactory,
    IRecurringJobManager jobManager) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken cancellation)
    {
        PeriodicTimer periodicTimer = new(TimeSpan.FromSeconds(30));
        do
        {
            using var scope = scopeFactory.CreateScope();
            var providerRepository = scope.ServiceProvider.GetRequiredService<IProviderRepository>();
            var providers = await providerRepository.GetAllAsync(cancellation);

            foreach (var provider in providers)
            {
                var jobId = $"Provider-{provider.Name}-{provider.Id}";

                if (provider.IsActive)
                {
                    jobManager.AddOrUpdate<ProviderSynchronizerJob>(
                        jobId,
                        j => j.ExecutionAsync(provider.Id),
                        provider.Schedule.CronExpression);
                }
                else
                {
                    jobManager.RemoveIfExists(jobId);
                }
            }
        } while (await periodicTimer.WaitForNextTickAsync(cancellation));
    }
}
