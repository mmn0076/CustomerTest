using System.ComponentModel.DataAnnotations.Schema;

namespace CustomerTest.Domain.Common.Models;

public abstract class Entity : IHasDomainEvents

{
    private readonly List<IDomainEvent> _domainEvents = new();

    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }

    public IReadOnlyList<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();

    protected Entity() { }

    protected Entity(Guid id)
    {
        Id = id;
    }

    public override int GetHashCode()
    {
        return Id.GetHashCode();
    }

    public void AddDomainEvent(IDomainEvent domainEvent)
    {
        _domainEvents.Add(domainEvent);
    }

    public void ClearDomainEvents()
    {
        _domainEvents.Clear();
    }




}