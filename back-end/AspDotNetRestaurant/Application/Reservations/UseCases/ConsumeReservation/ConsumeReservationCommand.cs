using Domain.Reservations.Aggregate;
using SharedKernal.CQRS.Commands;

namespace Application.Reservations.UseCases.ConsumeReservation;
public record ConsumeReservationCommand(int serialNumber, long mealEntryId) : ICommand<Reservation>; 
