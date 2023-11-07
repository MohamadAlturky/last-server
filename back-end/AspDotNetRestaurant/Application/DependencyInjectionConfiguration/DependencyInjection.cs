using Application.Loggers.Implementations;
using Application.Loggers.Interfaces;
using Domain.Anticorruption;
using Domain.Customers.SupDomainProxy;
using Domain.MealEntries.Services;
using Domain.MealEntries.SubDomainProxy;
using Domain.Pricing.SupDomainProxy;
using Domain.Reservations.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.DependencyInjectionConfiguration;
public static class DependencyInjection
{
	public static void AddApplication(this IServiceCollection services)
	{
		services.AddScoped(typeof(ICommandLogger<>), typeof(CommandLogger<>));
		services.AddScoped(typeof(IQueryLogger<>), typeof(QueryLogger<>));
		services.AddScoped<IReservationsService,ReservationsService>();
		services.AddScoped<IMealEntryService, MealEntryService>();
		/// proxies
		services.AddScoped<ICustomersSupDomainProxy, CustomersSupDomainProxy>();
		services.AddScoped<IPricingRecordsSupDomainProxy, PricingRecordsSupDomainProxy>();
		services.AddScoped<IMealEntriesSupDomainProxy, MealEntriesSupDomainProxy>();

	}
}
