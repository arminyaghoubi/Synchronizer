using Synchronizer.Domain.Aggregates.Providers.Events;
using Synchronizer.Domain.Aggregates.Providers.ValueObjects;
using Synchronizer.Domain.Common;
using System.Net;

namespace Synchronizer.Domain.Aggregates.Providers.Entities;

public sealed class Provider : AggregateRoot<int>
{
    private readonly List<Product> _products = new();

    public string Name { get; private set; } = null!;
    public ProviderEndpoint Endpoint { get; private set; }
    public SyncSchedule Schedule { get; private set; }
    public bool IsActive { get; private set; }
    public DateTime CreatedAt { get; private set; }

    public IReadOnlyCollection<Product> Products => _products.AsReadOnly();

    private Provider() { }

    private Provider(
        int id,
        string name,
        ProviderEndpoint endpoint,
        SyncSchedule schedule,
        bool isActive,
        DateTime createdAt) : base(id)
    {
        Name = name;
        Endpoint = endpoint;
        Schedule = schedule;
        IsActive = isActive;
        CreatedAt = createdAt;
    }

    public static Result<Provider> Create(
        string name,
        ProviderEndpoint endpoint,
        SyncSchedule schedule)
    {
        if (string.IsNullOrEmpty(name))
            return Result<Provider>.Failure("Provider name is required");

        Provider provider = new(0, name, endpoint, schedule, true, DateTime.Now);
        provider.AddDomainEvent(new ProviderCreatedDomainEvent(provider.Id, provider.Name));

        return Result<Provider>.Success(provider);
    }

    public Result SyncProducts(IEnumerable<(string ExternalId, string Name, decimal Price, decimal VAT, string ExtraData)> incoming)
    {
        var incomingIds = incoming.Select(x => x.ExternalId);

        foreach (var product in _products.Where(p => p.IsActive && !incomingIds.Contains(p.ExternalId)))
            product.Deactivate();

        foreach (var item in incoming)
        {
            var existing = _products.FirstOrDefault(x => x.ExternalId == item.ExternalId);

            if (existing is null)
            {
                var result = Product.Create(item.Name, item.Price, item.VAT, item.ExtraData, Id, item.ExternalId);
                if (result.IsSuccess)
                    _products.Add(result.Data!);
            }
            else
            {
                if (!existing.IsActive)
                    existing.Activate();

                existing.Update(item.Name, item.Price, item.VAT, item.ExtraData);
            }
        }

        return Result.Success();
    }
}
