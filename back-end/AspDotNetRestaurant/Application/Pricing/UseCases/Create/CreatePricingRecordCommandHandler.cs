using Domain.Shared.Repositories;
using SharedKernal.CQRS.Commands;
using SharedKernal.Repositories;
using SharedKernal.Utilities.Result;

namespace Application.Pricing.UseCases.Create;
internal class CreatePricingRecordCommandHandler : ICommandHandler<CreatePricingRecordCommand>
{
	private readonly IUnitOfWork _unitOfWork;
	private readonly IPricingRepository _pricingRepository;

	public CreatePricingRecordCommandHandler(IUnitOfWork unitOfWork, IPricingRepository pricingRepository)
	{
		_unitOfWork = unitOfWork;
		_pricingRepository = pricingRepository;
	}

	public async Task<Result> Handle(CreatePricingRecordCommand request, CancellationToken cancellationToken)
	{
		try
		{
			_pricingRepository.Add(request.record);
			await _unitOfWork.SaveChangesAsync();
			return Result.Success();
		}
		catch(Exception exception)
		{
			return Result.Failure(new SharedKernal.Utilities.Errors.Error("", exception.Message));
		}
	}
}
