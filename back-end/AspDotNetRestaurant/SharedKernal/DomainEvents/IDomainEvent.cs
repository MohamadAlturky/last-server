using MediatR;

namespace SharedKernal.DomainEvents;

public interface IDomainEvent : INotification
{
	Guid Id { get; }

	DateTime CreatedAtUtc { get; }
}
