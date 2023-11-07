namespace Domain.Reservations.Exceptions;


public class InvalidNumberOfOrderedMealsException : Exception
{
	public InvalidNumberOfOrderedMealsException() 
		: base("the number of ordered meals is invalid") { }
}
