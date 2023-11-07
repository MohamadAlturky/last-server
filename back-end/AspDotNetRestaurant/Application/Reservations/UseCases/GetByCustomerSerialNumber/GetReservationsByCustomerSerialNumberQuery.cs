using Domain.Reservations.Aggregate;
using SharedKernal.CQRS.Queries;

namespace Application.Reservations.UseCases.GetByCustomerSerialNumber;
public sealed record GetReservationsByCustomerSerialNumberQuery(int serialNumber) : IQuery<List<Reservation>>;
