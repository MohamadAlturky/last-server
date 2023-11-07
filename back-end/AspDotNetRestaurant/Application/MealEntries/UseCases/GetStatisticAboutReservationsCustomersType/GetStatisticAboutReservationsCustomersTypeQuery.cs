using Domain.Reservations.ReadModels;
using SharedKernal.CQRS.Queries;

namespace Application.MealEntries.UseCases.GetStatisticAboutReservationsCustomersType;
public sealed record GetStatisticAboutReservationsCustomersTypeQuery(long mealEntryId) 
	: IQuery<List<ReservationsCustomerTypeReadModel>>;
