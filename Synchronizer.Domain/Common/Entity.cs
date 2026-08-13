namespace Synchronizer.Domain.Common;

public abstract class Entity<TId>
    where TId : notnull
{
    public TId Id { get; set; } = default!;

    protected Entity() { }

    protected Entity(TId id)
    {
        Id = id;
    }

    public override bool Equals(object? obj)
    {
        if (obj is not Entity<TId> other)
            return false;

        if(ReferenceEquals(this, other)) 
            return true;

        return Id.Equals(other.Id);
    }
}
