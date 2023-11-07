using Infrastructure.Authentication.Models;
using Microsoft.AspNetCore.Authorization;

namespace Infrastructure.Authentication.Permissions;
public sealed class HasPermissionAttribute : AuthorizeAttribute
{
	public HasPermissionAttribute(string permission) : base(policy: permission) { }
}
