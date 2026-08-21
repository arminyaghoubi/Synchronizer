using Microsoft.Extensions.DependencyInjection;
using Synchronizer.Application.Abstractions;
using Synchronizer.Domain.Aggregates.Providers.Entities;
using Synchronizer.Domain.Aggregates.Providers.Repositories;
using Synchronizer.Domain.Common;

namespace Synchronizer.Application.Services;

public sealed class SyncOrchestrationService(
    IServiceScopeFactory serviceScopeFactory,
    IProviderRepository providerRepository) : ISyncOrchestrationService
{
    public async Task<Result> SyncProviderAsync(int providerId, CancellationToken cancellation)
    {
        var provider = await providerRepository.GetByIdWithProductsAsync(providerId, cancellation);

        if (provider is null)
            return Result.Failure($"Provider Id: {providerId} not found in Database");

        using var scope = serviceScopeFactory.CreateScope();
        var providerAdapter = scope.ServiceProvider.GetKeyedService<IProviderAdapter>(provider.Id);

        if (providerAdapter is null)
            return Result.Failure($"Provider Adapter for {provider.Id} Keyed not registered in DI Container");

        var fetchResult = await providerAdapter.FetchProductAsync(provider.Endpoint, cancellation);

        if (!fetchResult.IsSuccess)
            return Result.Failure(fetchResult.Error!);

        var incomingProducts = fetchResult.Data!;

        provider.SyncProducts(incomingProducts.Select(x => (x.ExternalId, x.Name, x.Price, x.VAT, x.ExtraData)));

        await providerRepository.UpdateAsync(provider, cancellation);

        return Result.Success();
    }
}
