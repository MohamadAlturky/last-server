using SharedKernal.DomainEvents;

namespace SharedKernal.Entities;

public abstract class AggregateRoot : Entity, IAggregateRoot
{
	private readonly List<IDomainEvent> _domainEvents = new List<IDomainEvent>();


	public IReadOnlyList<IDomainEvent> DomainEvents { get => this._domainEvents.AsReadOnly(); }


	protected AggregateRoot(long id) : base(id) { }



	public void Raise(IDomainEvent domainEvent)
	{
		this._domainEvents.Add(domainEvent);
	}


	public void ClearDomainEvents()
	{
		this._domainEvents.Clear();
	}
}