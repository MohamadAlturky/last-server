using Domain.Pricing.Aggregate;
using Domain.Shared.Repositories;
using SharedKernal.CQRS.Queries;
using SharedKernal.Repositories;
using SharedKernal.Utilities.Result;

namespace Application.Pricing.UseCases.GetAll;
internal class GetAllPricingRecordsQueryHandler : IQueryHandler<GetAllPricingRecordsQuery, List<PricingRecord>>
{
	private readonly IUnitOfWork _unitOfWork;
	private readonly IPricingRepository _pricingRepository;

	public GetAllPricingRecordsQueryHandler(IUnitOfWork unitOfWork, IPricingRepository pricingRepository)
	{
		_unitOfWork = unitOfWork;
		_pricingRepository = pricingRepository;
	}

	public async Task<Result<List<PricingRecord>>> Handle(GetAllPricingRecordsQuery request, CancellationToken cancellationToken)
	{
		try
		{
			List<PricingRecord> response = _pricingRepository.GetAll().ToList();
			await _unitOfWork.SaveChangesAsync();
			return Result.Success(response);
		}
		catch (Exception exception)
		{
			return Result.Failure<List<PricingRecord>>(new SharedKernal.Utilities.Errors.Error("", exception.Message));
		}
	}
}
