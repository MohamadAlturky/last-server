namespace Infrastructure.DataAccess.PermissionsService;
public interface IPermissionService
{
	Task<HashSet<string>> GetPermissions(long userId);
}
