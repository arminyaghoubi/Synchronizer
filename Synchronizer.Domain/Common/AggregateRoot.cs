namespace Synchronizer.Domain.Common;

public abstract class AggregateRoot<TId> : Entity<TId>
    where TId : notnull
{
    private readonly List<IDomainEvent> _events = new();
    public IReadOnlyList<IDomainEvent> Events => _events.AsReadOnly();

    protected AggregateRoot() { }
    protected AggregateRoot(TId id) : base(id) { }


    public void AddDomainEvent(IDomainEvent domainEvent) => _events.Add(domainEvent);
    public void ClearDomainEvents() => _events.Clear();
}
