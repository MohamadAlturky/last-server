using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Diagnostics;
using SharedKernal.DomainEvents;
using SharedKernal.Entities;

namespace Infrastructure.DataAccess.Interceptors;
public class AudtingSaveChangesInterceptor : SaveChangesInterceptor
{
	public override async ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData,
		InterceptionResult<int> result,
		CancellationToken cancellationToken = default)
	{
		DbContext? dbContext = eventData.Context;

		if (dbContext is null)
		{
			return await base.SavingChangesAsync(eventData, result, cancellationToken);
		}

		IEnumerable<EntityEntry<Entity>> entities = dbContext.ChangeTracker.Entries<Entity>();


		foreach (var entity in entities)
		{
			if(entity.State == EntityState.Added)
			{
				entity.Property(entry => entry.CreatedAt).CurrentValue = DateTime.Now;
			}
			if (entity.State == EntityState.Modified)
			{
				entity.Property(entry => entry.CreatedAt).CurrentValue = DateTime.Now;
			}
		}

		return await base.SavingChangesAsync(eventData, result, cancellationToken);
	}
}
