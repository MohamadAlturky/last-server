using Domain.Customers.ValueObjects;
using Domain.Meals.ValueObjects;
using Domain.Reservations.ValueObjects;
using SharedKernal.Entities;

namespace Domain.Pricing.Aggregate;
public class PricingRecord : AggregateRoot
{

	private Price _price = new Price(0);
	private CustomerType _customerType = CustomerType.Visitor;
	private MealType _mealType = MealType.UnDefined;

	 
	public int Price { get => _price.Value; set => _price = new Price(value); }
	public string CustomerTypeValue { get => _customerType.ToString(); set => _customerType = Enum.Parse<CustomerType>(value); }
	public string MealTypeValue { get => _mealType.ToString(); set => _mealType = Enum.Parse<MealType>(value); }


	public void ChangePrice(int newValue)
	{
		_price = new Price(newValue);
	}

	// constructor
	public PricingRecord(int id, string customerType, string mealType, int price) : base(id)
	{
		CustomerTypeValue = customerType;
		MealTypeValue = mealType;
		_price = new Price(0);
	}
	public PricingRecord() : base(0) { }
}
