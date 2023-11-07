using Infrastructure.Authentication.Models;
using Infrastructure.Authentication.PasswordHashing;
using Infrastructure.DataAccess.DBContext;
using Infrastructure.DataAccess.UserPersistence.Models;
using Infrastructure.ForgetPasswordHandling.Models;
using Microsoft.EntityFrameworkCore;
using SharedKernal.Utilities.Result;

namespace Infrastructure.DataAccess.UserPersistence;

public class UserPersistenceService : IUserPersistenceService
{
	private readonly static int CUSTOMERS_PAGE_SIZE = int.Parse(Properties.StaticValues.PaginationSize);
	private readonly RestaurantContext _context;
	private readonly IHashHandler _hashHandler;

	public UserPersistenceService(RestaurantContext context, IHashHandler hashHandler)
	{
		_context = context;
		_hashHandler = hashHandler;
	}

	public void AddRolesToUser(long userId, List<Role> roles)
	{
		foreach (var role in roles)
		{
			_context.Set<UserRole>().Add(new UserRole()
			{
				UserId = userId,
				RoleId = role.Id
			});
		}
	}

	public async Task ChangePassword(long userId, string oldPassword, string newPassword)
	{
		User? user = _context.Set<User>().FirstOrDefault(user => user.Id == userId);

		if (user is null)
		{
			throw new Exception("if(user is null)");
		}

		string oldHashedPassword = _hashHandler.GetHash(oldPassword);

		if (oldHashedPassword != user.HashedPassword)
		{
			throw new Exception("if (hashedPassword != user.HashedPassword)");
		}

		string newHashedPassword = _hashHandler.GetHash(newPassword);
		user.HashedPassword = newHashedPassword;

		_context.Set<User>().Update(user);
		await _context.SaveChangesAsync();
	}

	public Result CheckPasswordValidity(int serialNumber, string password)
	{
		string hashedPassword = _hashHandler.GetHash(password);
		User? user = _context.Set<User>()
			.Include(user => user.Customer)
			.Where(user => user.Customer.SerialNumber == serialNumber)
			.FirstOrDefault();
		if (user is null)
		{
			return Result.Failure(new SharedKernal.Utilities.Errors.Error("", "if(user is null)"));
		}
		if (hashedPassword != user.HashedPassword)
		{
			return Result.Failure(new SharedKernal.Utilities.Errors.Error("", "if(hashedPassword != user.HashedPassword)"));
		}
		return Result.Success();
	}

	public void CreateUser(User user)
	{
		_context.Set<User>().Add(user);
	}

	public ForgetPasswordEntry? GetForgetPasswordEntryOnThisDay(long userId)
	{
		DateTime toDay = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
		DateTime tomorrow = toDay.AddDays(1);

		return _context.Set<ForgetPasswordEntry>()
					.Where(entry => entry.UserId == userId)
					.Where(entry => entry.AtDay < tomorrow && entry.AtDay >= toDay)
					.FirstOrDefault();
	}

	public User? GetUser(int serialNumber)
	{
		return _context.Set<User>()
			.Include(user => user.Customer)
			.Where(user => user.Customer.SerialNumber == serialNumber)
			.FirstOrDefault();
	}

	public async Task<User?> GetUserAsync(int serialNumber)
	{
		return await _context.Set<User>()
			.Include(user => user.Customer)
			.Where(user => user.Customer.SerialNumber == serialNumber)
			.FirstOrDefaultAsync();
	}

	public async Task<CustomersPaginiationResponse> GetUserPaginatedAsync(int pageNumber)
	{
		IOrderedQueryable<User> queryableUsers =
			_context.Set<User>()
			.Where(user => user.Id != 1 && user.Id != 2 && user.Id != 3)
			.Include(user => user.Customer)
			.OrderByDescending(entry => entry.Id);

		int size = queryableUsers.Count();

		List<User> users = queryableUsers
			.Skip(CUSTOMERS_PAGE_SIZE * (pageNumber - 1))
			.Take(CUSTOMERS_PAGE_SIZE)
			.ToList();

		await _context.SaveChangesAsync();

		List<CustomerInformation> customers = users.Select(user => new CustomerInformation()
		{
			Balance = user.Customer.Balance,
			BelongsToDepartment = user.Customer.BelongsToDepartment,
			Category = user.Customer.Category,
			FirstName = user.Customer.FirstName,
			HiastMail = user.HiastMail,
			Id = user.Id,
			LastName = user.Customer.LastName,
			Notes = user.Customer.Notes,
			SerialNumber = user.Customer.SerialNumber
		}).ToList();

		CustomersPaginiationResponse model = new CustomersPaginiationResponse()
		{
			Count = size,
			Customers = customers
		};
		return model;
	}

	public void UpdateUserInformation(User user)
	{
		_context.Set<User>().Update(user);
		_context.SaveChanges();
	}

	public void UpdateUserPassword(int serialNumber, string password)
	{
		User? user = _context.Set<User>()
			.Where(user => user.Customer.SerialNumber == serialNumber)
			.FirstOrDefault();

		if (user is null)
		{
			throw new Exception("if(user is null)");
		}
		user.HashedPassword = _hashHandler.GetHash(password);

		_context.Set<User>().Update(user);
		_context.SaveChanges();
	}
}
