using SharedKernal.DomainEvents;

namespace SharedKernal.Entities;

public interface IAggregateRoot : IEntity
{
	IReadOnlyList<IDomainEvent> DomainEvents { get; }

	void Raise(IDomainEvent domainEvent);

	void ClearDomainEvents();
}
