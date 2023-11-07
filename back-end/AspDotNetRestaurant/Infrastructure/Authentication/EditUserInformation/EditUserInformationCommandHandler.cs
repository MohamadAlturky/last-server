using Domain.Customers.Aggregate;
using Domain.Customers.Repositories;
using Infrastructure.Authentication.Models;
using Infrastructure.Authentication.PasswordHashing;
using Infrastructure.DataAccess.UserPersistence;
using SharedKernal.CQRS.Commands;
using SharedKernal.Repositories;
using SharedKernal.Utilities.Result;

namespace Infrastructure.Authentication.EditUserInformation;
internal class EditUserInformationCommandHandler : ICommandHandler<EditUserInformationCommand>
{
	private readonly IUserPersistenceService _userPersistence;
	private readonly IUnitOfWork _unitOfWork;
	private readonly ICustomerRepository _customerRepository;

	public EditUserInformationCommandHandler(IUserPersistenceService userPersistence, 
		IUnitOfWork unitOfWork, 
		ICustomerRepository customerRepository)
	{
		_userPersistence = userPersistence;
		_unitOfWork = unitOfWork;
		_customerRepository = customerRepository;
	}

	public async Task<Result> Handle(EditUserInformationCommand request, CancellationToken cancellationToken)
	{
		try
		{
			User? user = _userPersistence.GetUser(request.user.Customer.SerialNumber);

			if(user is null){
				throw new Exception("User? user = _userPersistence.GetUser(request.user.Customer.SerialNumber);\r\n\r\n\t\t\tif(user is null){");
			}

			user.HiastMail = request.user.HiastMail;
			//user.Customer.SerialNumber= request.user.Customer.SerialNumber;

			user.Customer.FirstName= request.user.Customer.FirstName;
			user.Customer.LastName= request.user.Customer.LastName;
			
			user.Customer.Notes= request.user.Customer.Notes;
			user.Customer.Category = request.user.Customer.Category;

			_userPersistence.UpdateUserInformation(user);
			await _unitOfWork.SaveChangesAsync();

			return Result.Success();
		}
		catch (Exception exception)
		{
			return Result.Failure(new SharedKernal.Utilities.Errors.Error("", exception.Message));
		}
	}
}
