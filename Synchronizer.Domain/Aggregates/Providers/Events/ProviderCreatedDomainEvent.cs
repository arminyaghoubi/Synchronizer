using Synchronizer.Domain.Common;

namespace Synchronizer.Domain.Aggregates.Providers.Events;

public sealed record ProviderCreatedDomainEvent(int ProviderId, string Name) : IDomainEvent
{
    public DateTime OccurredOn => DateTime.Now;
}
