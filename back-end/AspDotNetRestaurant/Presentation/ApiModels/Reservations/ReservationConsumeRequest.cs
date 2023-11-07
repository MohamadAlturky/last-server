namespace Presentation.ApiModels.Reservations;

public class ReservationConsumeRequest
{
	public int SerialNumber { get; set; }
	public string Password { get; set; }= string.Empty;
	public long MealEntryId { get; set; }
}
