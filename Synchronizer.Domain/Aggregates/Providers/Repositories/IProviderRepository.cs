using Synchronizer.Domain.Aggregates.Providers.Entities;

namespace Synchronizer.Domain.Aggregates.Providers.Repositories;

public interface IProviderRepository
{
    Task<Provider?> GetByIdAsync(int id, CancellationToken cancellation);
    Task<IReadOnlyList<Provider>> GetAllAsync(CancellationToken cancellation);
    Task<IReadOnlyList<Provider>> GetActiveAsync(CancellationToken cancellation);
    Task AddAsync(Provider provider, CancellationToken cancellation);
    Task UpdateAsync(Provider provider, CancellationToken cancellation);
}
