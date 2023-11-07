using Domain.Pricing.Aggregate;
using Domain.Shared.Repositories;
using SharedKernal.CQRS.Commands;
using SharedKernal.Repositories;
using SharedKernal.Utilities.Result;

namespace Application.Pricing.UseCases.Delete;
internal class DeletePricingRecordCommandHandler : ICommandHandler<DeletePricingRecordCommand>
{
	private readonly IUnitOfWork _unitOfWork;
	private readonly IPricingRepository _pricingRepository;

	public DeletePricingRecordCommandHandler(IUnitOfWork unitOfWork, IPricingRepository pricingRepository)
	{
		_unitOfWork = unitOfWork;
		_pricingRepository = pricingRepository;
	}

	public async Task<Result> Handle(DeletePricingRecordCommand request, CancellationToken cancellationToken)
	{
		try
		{
			PricingRecord? record = _pricingRepository.GetById(request.id);

			if(record is null)
			{
				throw new Exception("");
			}

			_pricingRepository.Delete(record);

			await _unitOfWork.SaveChangesAsync();

			return Result.Success();
		}
		catch (Exception exception)
		{
			return Result.Failure(new SharedKernal.Utilities.Errors.Error("", exception.Message));
		}
	}
}
