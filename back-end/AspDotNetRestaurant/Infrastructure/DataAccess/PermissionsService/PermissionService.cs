using Infrastructure.Authentication.Models;
using Infrastructure.DataAccess.DBContext;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.DataAccess.PermissionsService;
public class PermissionService : IPermissionService
{
	private readonly RestaurantContext _context;

	public PermissionService(RestaurantContext context)
	{
		_context = context;
	}

	public async Task<HashSet<string>> GetPermissions(long userId)
	{
		ICollection<Role>[] roles = await _context.Set<User>()
			.Include(user=>user.Roles)
			.ThenInclude(role=>role.Permissions)
			.Where(user => user.Id == userId)
			.Select(user => user.Roles).ToArrayAsync();

		return roles
			.SelectMany(role => role)	
			.SelectMany(role => role.Permissions)
			.Select(permission => permission.Name)
			.ToHashSet();
	}
}
