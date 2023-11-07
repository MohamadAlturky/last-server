using Domain.Localization;
using Infrastructure.Authentication.JWTProvider;
using Infrastructure.Authentication.Models;
using Infrastructure.Authentication.PasswordHashing;
using Infrastructure.DataAccess.DBContext;
using Microsoft.EntityFrameworkCore;
using SharedKernal.CQRS.Commands;
using SharedKernal.Utilities.Result;

namespace Infrastructure.Authentication.LogIn;
internal class LogInCommandHandler : ICommandHandler<LogInCommand, string>
{
	private readonly RestaurantContext _context;
	private readonly IJwtProvider _jWTProvider;
	private readonly IHashHandler _hashHandler;

	public LogInCommandHandler(RestaurantContext context, IJwtProvider jWTProvider, IHashHandler hashHandler)
	{
		_context = context;
		_jWTProvider = jWTProvider;
		_hashHandler = hashHandler;
	}

	public async Task<Result<string>> Handle(LogInCommand request, CancellationToken cancellationToken)
	{
		try
		{
			User? user = _context.Set<User>()
				.Include(user=>user.Customer)
				.Where(user => user.Customer.SerialNumber == request.model.SerialNumber)
				.FirstOrDefault();


			if (user is null)
			{
				throw new Exception("if (user is null)");
			}
			string hashedPassword = user.HashedPassword;
			if (hashedPassword != _hashHandler.GetHash(request.model.Password))
			{
				throw new Exception(LocalizationProvider.GetResource(DomainResourcesKeys.PasswordMisMatch));
			}

			string token = await _jWTProvider.Generate(user);

			await _context.SaveChangesAsync();

			return Result.Success(token);
		}
		catch (Exception exception)
		{
			return Result.Failure<string>(new SharedKernal.Utilities.Errors.Error("", exception.Message));
		}
	}
}
