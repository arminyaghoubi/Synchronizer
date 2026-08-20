using Microsoft.EntityFrameworkCore;
using Synchronizer.Domain.Aggregates.Providers.Entities;
using System.Reflection;

namespace Synchronizer.Infrastructure.Persistence;

public sealed class SynchronizerDbContext : DbContext
{
    public DbSet<Provider> Providers { get; set; }

    public SynchronizerDbContext(DbContextOptions<SynchronizerDbContext> options)
        : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
