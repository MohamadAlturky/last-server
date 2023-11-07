using Application.Reservations.UseCases.Cancel;
using SharedKernal.CQRS.Commands;

namespace Application.Reservations.UseCases.Create;
public record CreateReservationCommand(long customerId, long orderedMealId) : ICommand<CreateReservationResponse>;
