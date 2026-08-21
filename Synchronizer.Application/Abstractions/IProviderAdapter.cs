using Synchronizer.Application.Contracts;
using Synchronizer.Domain.Aggregates.Providers.ValueObjects;
using Synchronizer.Domain.Common;

namespace Synchronizer.Application.Abstractions;

public interface IProviderAdapter
{
    Task<Result<IReadOnlyList<ExternalProductDto>>> FetchProductAsync(
        ProviderEndpoint endpoint,
        CancellationToken cancellation);
}
