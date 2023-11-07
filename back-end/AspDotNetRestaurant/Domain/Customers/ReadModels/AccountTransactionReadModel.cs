namespace Domain.Customers.ReadModels;
public class AccountTransactionReadModel
{
	public string Type { get; set; }=String.Empty;
	public int Value { get; set; }
	public string CreatedAtDay { get; set; }
	public string Date { get; set; }
	public string By { get; set; }
}
