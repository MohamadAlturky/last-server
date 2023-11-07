namespace Domain.Reservations.ValueObjects;


public enum OrderStatus
{
	OrderCompletedButNotRegisteredYet,
	Waiting,
	Consumed,
	Reserved,
	Canceled ,
	Passed,
	OnTheCanceledListButNotCanceledYet
}
