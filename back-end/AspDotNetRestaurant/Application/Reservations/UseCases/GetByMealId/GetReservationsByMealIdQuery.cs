using Domain.Reservations.ReadModels;
using SharedKernal.CQRS.Queries;

namespace Application.Reservations.UseCases.GetByMealId;
public record GetReservationsByMealIdQuery(long mealEntryId):IQuery<ReservationsReadModel>;
