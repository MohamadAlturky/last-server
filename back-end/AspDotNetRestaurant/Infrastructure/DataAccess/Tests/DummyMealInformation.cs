using Domain.MealEntries.Aggregate;
using Domain.Meals.ValueObjects;
using Microsoft.AspNetCore.Mvc;
using SharedKernal.DomainEvents;
using System.ComponentModel.DataAnnotations;

namespace Infrastructure.DataAccess.Tests;
public class DummyMealInformation
{
	public long Id { get; set; }
	public int NumberOfCalories { get; set; }
	public string Type { get; set; }
	public string Description { get; set; }
	public string ImagePath { get; set; } = string.Empty;
	public string Name { get; set; } = string.Empty;
	public DateTime CreatedAt { get; set; }
	public DateTime UpdatedAt { get; set; }

	[Timestamp]
	public byte[] RowVersion { get; set; }


	private readonly List<IDomainEvent> _domainEvents = new List<IDomainEvent>();
	public IReadOnlyList<IDomainEvent> DomainEvents { get => this._domainEvents.AsReadOnly(); }

	public void Raise(IDomainEvent domainEvent)
	{
		this._domainEvents.Add(domainEvent);
	}


	public void ClearDomainEvents()
	{
		this._domainEvents.Clear();
	}
}
