using Infrastructure.Authentication.Claims;
using Microsoft.AspNetCore.Authorization;
using System.Security.Authentication;

namespace Infrastructure.Authorization.AuthorizationHandlers;

// this service will be singelton
public class PermissionAuthorizationHandler : AuthorizationHandler<PermissionRequirement>
{

	public PermissionAuthorizationHandler() { }

	protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionRequirement requirement)
	{
		if (context.User is null)
		{
			throw new AuthenticationException();
		}

		HashSet<string> permissions = context.User.Claims
			.Where(claim => claim.Type == CustomClaims.Permissions)
			.Select(claim => claim.Value).ToHashSet();

		if (permissions.Contains(requirement.Permission))
		{
			context.Succeed(requirement);
		}

		return Task.CompletedTask;
	}
}


/*
 
 using Infrastructure.Authentication.Claims;
using Infrastructure.DataAccess.PermissionsService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Authorization.AuthorizationHandlers;

// this service will be singelton
public class PermissionAuthorizationHandler : AuthorizationHandler<PermissionRequirement>
{
	private readonly IServiceScopeFactory _serviceScopeFactory;

	public PermissionAuthorizationHandler(IServiceScopeFactory serviceScopeFactory)
	{
		_serviceScopeFactory = serviceScopeFactory;
	}

	protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionRequirement requirement)
	{
		string? userId = context.User.Claims
			.FirstOrDefault(claim => claim.Type == CustomClaims.Id)?.Value;

		if(!long.TryParse(userId, out long parsedId))
		{
			return;
		}
		
		using IServiceScope scope = _serviceScopeFactory.CreateScope();
		
		IPermissionService permissionService = scope.ServiceProvider
			.GetRequiredService<IPermissionService>();

		HashSet<string> permissions = await permissionService.GetPermissions(parsedId);

		if (permissions.Contains(requirement.Permission))
		{
			context.Succeed(requirement);
		}
	}
}

 
 
 
 
 
 
 
 */