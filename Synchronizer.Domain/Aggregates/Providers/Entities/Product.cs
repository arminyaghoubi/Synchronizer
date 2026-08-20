using Synchronizer.Domain.Common;

namespace Synchronizer.Domain.Aggregates.Providers.Entities;

public sealed class Product : Entity<int>
{
    public string Name { get; private set; } = null!;
    public decimal Price { get; private set; }
    public decimal VAT { get; private set; }
    public string ExtraData { get; private set; } = null!;
    public bool IsActive { get; private set; }
    public int ProviderId { get; private set; }
    public string ExternalId { get; private set; } = null!;

    private Product() { }

    internal static Result<Product> Create(
        string name,
        decimal price,
        decimal vat,
        string extraData,
        int providerId,
        string externalId)
    {
        if (string.IsNullOrEmpty(name))
            return Result<Product>.Failure("Product name is required");

        if (string.IsNullOrEmpty(externalId))
            return Result<Product>.Failure("ExternalId is required");

        return Result<Product>.Success(new()
        {
            Name = name,
            Price = price,
            VAT = vat,
            ExtraData = extraData,
            IsActive = true,
            ProviderId = providerId,
            ExternalId = externalId
        });
    }

    internal Result Update(
        string name,
        decimal price,
        decimal vat,
        string extraData)
    {
        if (string.IsNullOrEmpty(name))
            return Result<Product>.Failure("Product name is required");

        Name = name;
        Price = price;
        VAT = vat;

        return Result.Success();
    }

    internal Result Deactivate()
    {
        if (!IsActive)
            return Result.Failure("Product already deactivated");

        IsActive = false;
        return Result.Success();
    }

    internal Result Activate()
    {
        if (!IsActive)
            return Result.Failure("Product already activated");

        IsActive = true;
        return Result.Success();
    }
}
