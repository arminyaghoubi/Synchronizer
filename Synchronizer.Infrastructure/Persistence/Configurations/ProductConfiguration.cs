using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Synchronizer.Domain.Aggregates.Providers.Entities;

namespace Synchronizer.Infrastructure.Persistence.Configurations;

public sealed class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(p => p.Name).HasMaxLength(300).IsRequired();
        builder.Property(p => p.Price).HasPrecision(18, 4);
        builder.Property(p => p.VAT).HasPrecision(5, 2);
        builder.Property(p => p.ExternalId).HasMaxLength(100).IsRequired();
        builder.Property(p => p.ExtraData).HasColumnType("JSON");

        builder.HasIndex(p => new { p.ProviderId, p.ExternalId }).IsUnique();
    }
}
