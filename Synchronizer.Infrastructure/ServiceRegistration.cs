using Hangfire;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Synchronizer.Application.Abstractions;
using Synchronizer.Domain.Aggregates.Providers.Repositories;
using Synchronizer.Infrastructure.Adapters.TMobile;
using Synchronizer.Infrastructure.BackgroundJobs.Hangfire;
using Synchronizer.Infrastructure.BackgroundJobs.Hangfire.Scheduling;
using Synchronizer.Infrastructure.Persistence;
using Synchronizer.Infrastructure.Persistence.Repositories;

namespace Synchronizer.Infrastructure;

public static class ServiceRegistration
{
    public static IServiceCollection AddPersistenceServices(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<SynchronizerDbContext>(options =>
        {
            options.UseSqlServer(
                configuration.GetConnectionString("SynchronizerDbContext"),
                sqlServer =>
                {
                    sqlServer.UseCompatibilityLevel(170);// SQL Server 2025
                });
        });

        services.AddScoped<IProviderRepository, ProviderRepository>();

        return services;
    }

    public static IServiceCollection AddBackgroundJobServices(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddHangfire(config =>
        {
            config.UseSqlServerStorage(configuration.GetConnectionString("SynchronizerDbContext"));
        });

        services.AddHangfireServer();

        services.AddHostedService<HangfireProviderJobScheduler>();

        return services;
    }

    public static IServiceCollection AddProviderAdapterServices(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddKeyedScoped<IProviderAdapter, TMobileAdapter>(1);

        return services;
    }
}
