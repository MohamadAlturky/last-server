using Domain.Reservations.Aggregate;
using SharedKernal.CQRS.Queries;

namespace Application.Reservations.UseCases.GetBetweenTwoDates;
public sealed record GetReservationsBetweenTwoDatesQuery(DateOnly start,DateOnly end) : IQuery<List<Reservation>>;
