using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using SharedKernal.DomainEvents;
using SharedKernal.Entities;

namespace Infrastructure.DataAccess.Interceptors;
public class DomainEventsCollectorInterceptor : SaveChangesInterceptor
{
	private readonly IPublisher _publisher;

	public DomainEventsCollectorInterceptor(IPublisher publisher)
	{
		_publisher = publisher;
	}

	public override async ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData,
		InterceptionResult<int> result,
		CancellationToken cancellationToken = default)
	{
		DbContext? dbContext = eventData.Context;

		if (dbContext is null)
		{
			return await base.SavingChangesAsync(eventData, result, cancellationToken);
		}

		var domainEvents = dbContext.ChangeTracker.Entries<AggregateRoot>()
			.Select(tracker => tracker.Entity)
			.SelectMany(entity =>
			{
				IReadOnlyList<IDomainEvent> events = entity.DomainEvents;
				entity.ClearDomainEvents();
				return events;
			});
		
		var response = await base.SavingChangesAsync(eventData, result, cancellationToken);
		
		foreach (var domainEvent in domainEvents)
		{
			await _publisher.Publish(domainEvent);
		}

		return response;
	}
}
