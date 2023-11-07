using Domain.Pricing.Aggregate;
using Domain.Shared.Repositories;
using SharedKernal.CQRS.LogableCommand;
using SharedKernal.Repositories;
using SharedKernal.Utilities.Result;

namespace Application.Pricing.UseCases.Update;
internal class UpdatePricingRecordCommandHandler : LogableCommandHandler<UpdatePricingRecordCommand>
{
	private readonly IUnitOfWork _unitOfWork;
	private readonly IPricingRepository _pricingRepository;

	public UpdatePricingRecordCommandHandler(IUnitOfWork unitOfWork, IPricingRepository pricingRepository)
	{
		_unitOfWork = unitOfWork;
		_pricingRepository = pricingRepository;
	}

	public async Task<Result> Handle(UpdatePricingRecordCommand request, CancellationToken cancellationToken)
	{
		try
		{
			PricingRecord? record = _pricingRepository.GetPriceByCustomerTypeJoinMealType(request.customerType,request.mealType);
			
			if(record is null)
			{
				throw new Exception("_pricingRepository.GetById(request.Id) is null");
			}

			record.ChangePrice(request.price);

			_pricingRepository.Update(record);


			await _unitOfWork.SaveChangesAsync();
			
			return Result.Success();
		}
		catch (Exception exception)
		{
			return Result.Failure(new SharedKernal.Utilities.Errors.Error("", exception.Message));
		}
	}
}
