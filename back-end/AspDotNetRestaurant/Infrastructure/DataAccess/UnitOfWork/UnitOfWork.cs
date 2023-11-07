using Domain.Localization;
using Infrastructure.DataAccess.DBContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Logging;
using SharedKernal.Repositories;
using System.Data;

namespace Infrastructure.DataAccess.UnitOfWork;
public class UnitOfWork : IUnitOfWork
{
	private readonly RestaurantContext _context;
	private readonly ILogger<RestaurantContext> _logger;
	//private readonly IPublisher _publisher;

	//public UnitOfWork(RestaurantContext context, IPublisher publisher)
	//{
	//	_context = context;
	//	_publisher = publisher;
	//}
	//var domainEvents = _context.ChangeTracker
	//	.Entries<AggregateRoot>()
	//	.Select(tracker => tracker.Entity)
	//	.SelectMany(aggregateRoot =>
	//	{
	//		var domainEvents = aggregateRoot.DomainEvents;
	//		aggregateRoot.ClearDomainEvents();
	//		return domainEvents;
	//	})
	//	.ToList();
	//foreach (var domainEvent in domainEvents)
	//{
	//	_publisher.Publish(domainEvent);
	//}
	public UnitOfWork(RestaurantContext context, ILogger<RestaurantContext> logger)
	{
		_logger = logger;
		_context = context;
	}

	public IDbTransaction BeginTransaction()
	{
		IDbContextTransaction transaction = _context.Database.BeginTransaction();
		return transaction.GetDbTransaction();
	}
	public void EndTransaction()
	{
		_context.Database.CloseConnection();
	}


	public void SaveChanges()
	{
		_context.SaveChanges();
	}

	public async Task SaveChangesAsync()
	{
		try
		{
			await _context.SaveChangesAsync();
		}
		catch (DBConcurrencyException exception)
		{
			_logger.LogError("When Saving The Changes At {@DateTimeUTC} {StackTrace}",
				DateTime.UtcNow,exception.StackTrace);
			throw new Exception(LocalizationProvider.GetResource(DomainResourcesKeys.DBConcurrencyException));
		}
		catch (DbUpdateConcurrencyException exception)
		{
			_logger.LogError("When Saving The Changes At {@DateTimeUTC} {StackTrace}",
				DateTime.UtcNow, exception.StackTrace);
			throw new Exception(LocalizationProvider.GetResource(DomainResourcesKeys.DBConcurrencyException));
		}
		catch (Exception exception)
		{
			_logger.LogError("When Saving The Changes At {@DateTimeUTC} {StackTrace}",
				DateTime.UtcNow, exception.StackTrace);
			throw new Exception(LocalizationProvider.GetResource(DomainResourcesKeys.DBConcurrencyException));
		}
	}
}
