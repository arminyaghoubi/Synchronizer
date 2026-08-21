using Microsoft.Extensions.DependencyInjection;
using Synchronizer.Application.Abstractions;
using Synchronizer.Application.Services;

namespace Synchronizer.Application;

public static class ServiceRegistration
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<ISyncOrchestrationService, SyncOrchestrationService>();

        return services;
    }
}
