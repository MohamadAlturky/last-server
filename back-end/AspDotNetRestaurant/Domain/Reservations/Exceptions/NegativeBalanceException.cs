namespace Domain.Reservations.Exceptions;

internal class NegativeBalanceException : Exception
{
	public NegativeBalanceException() 
		: base("the balance shouldn't be negative") { }
}
