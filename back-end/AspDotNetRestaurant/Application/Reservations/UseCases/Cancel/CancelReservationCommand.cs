using SharedKernal.CQRS.Commands;

namespace Application.Reservations.UseCases.Cancel;
public sealed record CancelReservationCommand(long reservationId) : ICommand<string>;
