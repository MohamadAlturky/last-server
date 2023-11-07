using Microsoft.AspNetCore.Authorization;

namespace Infrastructure.Authorization.AuthorizationHandlers;
public class PermissionRequirement : IAuthorizationRequirement
{
	public PermissionRequirement(string permission)
	{
		Permission = permission;
	}

	public string Permission { get;}
}
