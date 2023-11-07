using Domain.Customers.Aggregate;
using Domain.Customers.Repositories;
using Infrastructure.Authentication.Models;
using Infrastructure.Authentication.PasswordHashing;
using Infrastructure.DataAccess.UserPersistence;
using SharedKernal.CQRS.Commands;
using SharedKernal.Repositories;
using SharedKernal.Utilities.Result;

namespace Infrastructure.Authentication.Register;
internal class RegisterNewCustomerCommandHandler : ICommandHandler<RegisterNewCustomerCommand>
{
	private readonly IUserPersistenceService _userPersistence;
	private readonly IUnitOfWork _unitOfWork;
	private readonly ICustomerRepository _customerRepository;
	private readonly IHashHandler _hashHandler;

	public RegisterNewCustomerCommandHandler(IUserPersistenceService userPersistence, 
		IUnitOfWork unitOfWork, ICustomerRepository customerRepository, IHashHandler hashHandler)
	{
		_userPersistence = userPersistence;
		_unitOfWork = unitOfWork;
		_customerRepository = customerRepository;
		_hashHandler = hashHandler;
	}

	public async Task<Result> Handle(RegisterNewCustomerCommand request, 
		CancellationToken cancellationToken)
	{
		using (var transaction = _unitOfWork.BeginTransaction()) {
			try
			{
				User user = request.user;
				if (user.Customer is null)
				{
					throw new Exception("if(user.Customer is null)");
				}
				Customer customer = user.Customer;
				List<Role> roles = user.Roles.ToList();
				string hashedPassword = _hashHandler.GetHash(request.password);


				_customerRepository.Add(customer);
				await _unitOfWork.SaveChangesAsync();

				user.Roles = null!;

				user.HashedPassword = hashedPassword;


				_userPersistence.CreateUser(user);
				await _unitOfWork.SaveChangesAsync();


				_userPersistence.AddRolesToUser(user.Id, roles);
				await _unitOfWork.SaveChangesAsync();

				transaction.Commit();
				_unitOfWork.EndTransaction();
				return Result.Success();
			}
			catch (Exception exception)
			{
				transaction.Rollback();
				_unitOfWork.EndTransaction();
				return Result.Failure(new SharedKernal.Utilities.Errors.Error("", exception.Message));
			}
		}
	}
}
