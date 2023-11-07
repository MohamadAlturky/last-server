using SharedKernal.CQRS.Commands;

namespace Application.Reservations.UseCases.CollectPassedReservations;
public sealed record CollectPassedReservationsCommand() : ICommand;
