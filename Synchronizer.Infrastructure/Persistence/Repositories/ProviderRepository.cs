using Microsoft.EntityFrameworkCore;
using Synchronizer.Domain.Aggregates.Providers.Entities;
using Synchronizer.Domain.Aggregates.Providers.Repositories;

namespace Synchronizer.Infrastructure.Persistence.Repositories;

public sealed class ProviderRepository(SynchronizerDbContext context) : IProviderRepository
{
    public async Task AddAsync(Provider provider, CancellationToken cancellation)
    {
        await context.AddAsync(provider, cancellation);
        await context.SaveChangesAsync(cancellation);
    }

    public async Task<IReadOnlyList<Provider>> GetActiveAsync(CancellationToken cancellation)
    {
        var providers = await context.Providers.Where(p => p.IsActive).ToListAsync(cancellation);
        return providers;
    }

    public async Task<IReadOnlyList<Provider>> GetAllAsync(CancellationToken cancellation)
    {
        var providers = await context.Providers.ToListAsync(cancellation);
        return providers;
    }

    public async Task<Provider?> GetByIdAsync(int id, CancellationToken cancellation)
    {
        var provider = await context.Providers.FindAsync(id, cancellation);
        return provider;
    }

    public async Task UpdateAsync(Provider provider, CancellationToken cancellation)
    {
        context.Update(provider);
        await context.SaveChangesAsync(cancellation);
    }
}
