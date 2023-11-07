//using Application.CQRSAbstractions.Commands;
//using Domain.Customers.Repositories;
//using SharedKernal.Repositories;
//using SharedKernal.Utilities.Result;

//namespace Application.UseCases.Customers.UpdateInformation;
//internal class UpdateCustomerInformationCommandHandler : ICommandHandler<UpdateCustomerInformationCommand>
//{
//	private ICustomerRepository _customerRepository;

//	private IUnitOfWork _unitOfWork;

//	public UpdateCustomerInformationCommandHandler(ICustomerRepository customerRepository, IUnitOfWork _unitOfWork)
//	{
//		_customerRepository = customerRepository;
//		_unitOfWork = _unitOfWork;
//	}

//	public async Task<Result> Handle(UpdateCustomerInformationCommand request, CancellationToken cancellationToken)
//	{
//		try
//		{
//			_customerRepository.UpdateInformation(request.customer);
//		}
//		catch (Exception exception){
//			return Result.Failure(new SharedKernal.Utilities.Errors.Error("", exception.Message));
//		}

//		await _unitOfWork.SaveChanges();

//		return Result.Success();
//	}
//}
