using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Synchronizer.Domain.Aggregates.Providers.Entities;

namespace Synchronizer.Infrastructure.Persistence.Configurations;

public sealed class ProviderConfiguration : IEntityTypeConfiguration<Provider>
{
    public void Configure(EntityTypeBuilder<Provider> builder)
    {
        builder.HasKey(x=> x.Id);

        builder.HasIndex(x => x.Name)
            .IsUnique();

        builder.Property(x => x.Name)
            .HasMaxLength(256)
            .IsRequired();

        builder.OwnsOne(x => x.Endpoint, e =>
        {
            e.Property(x => x.Value).HasColumnName("EndpointUrl").HasMaxLength(500).IsRequired();

            e.Property(x => x.TimeoutSeconds).HasColumnName("EndpointTimeout").IsRequired();
        });

        builder.OwnsOne(x => x.Schedule, e =>
        {
            e.Property(x => x.CronExpression).HasColumnName("ScheduleCron").HasMaxLength(100).IsRequired();

            e.Property(x => x.MaxRetryCount).HasColumnName("ScheduleMaxRetryCount").IsRequired();
        });

        builder.HasMany(x => x.Products)
            .WithOne()
            .HasForeignKey(x => x.ProviderId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}
