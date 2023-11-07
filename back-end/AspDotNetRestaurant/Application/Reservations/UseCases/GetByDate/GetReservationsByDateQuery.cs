using Domain.Reservations.Aggregate;
using SharedKernal.CQRS.Queries;

namespace Application.Reservations.UseCases.GetByDate;
public sealed record GetReservationsByDateQuery(DateOnly day) : IQuery<List<Reservation>>;