namespace Domain.Reservations.ReadModels;
public class ReservationRecord
{
	public long Id { get; set; }
	public string CustomerName { get; set; } = string.Empty;
	public int SerialNumber { get; set; }
	public string Status { get; set; } = string.Empty;
	public string CustomerCategory { get; set; } = string.Empty;
}
