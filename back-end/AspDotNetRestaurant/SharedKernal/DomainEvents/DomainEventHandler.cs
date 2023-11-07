using MediatR;

namespace SharedKernal.DomainEvents;
public interface DomainEventHandler<T> : INotificationHandler<T> where T:IDomainEvent{ }
