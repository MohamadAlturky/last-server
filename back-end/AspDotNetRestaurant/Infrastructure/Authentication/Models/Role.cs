namespace Infrastructure.Authentication.Models;
public class Role
{
	public int Id { get; set; }
	public string Name { get; set; } = string.Empty;

	public ICollection<Permission> Permissions { get; set; } = new List<Permission>();
	public ICollection<User> Users { get; set; } = new List<User>();
}
