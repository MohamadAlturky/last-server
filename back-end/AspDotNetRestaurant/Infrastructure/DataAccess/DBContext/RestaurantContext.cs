using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.DataAccess.DBContext;
public class RestaurantContext : DbContext
{
	private readonly IConfiguration _configuration;
	public RestaurantContext(DbContextOptions<RestaurantContext> options, IConfiguration configuration)
			: base(options)
	{
		AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
		_configuration = configuration;
	}
	protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
	{
		if (!optionsBuilder.IsConfigured)
		{
			optionsBuilder.UseNpgsql(_configuration.GetConnectionString("DefaultConnection"));
			//optionsBuilder.UseSqlServer("server=DESKTOP-OO326C9\\SQLEXPRESS;database= TheRestaurant;Trusted_Connection=True; Encrypt=False;");
		}
	}
	protected override void OnModelCreating(ModelBuilder builder)
	{
		builder.ApplyConfigurationsFromAssembly(typeof(RestaurantContext).Assembly);
	}

	public override DbSet<TEntity> Set<TEntity>()
	{
		try
		{
			return base.Set<TEntity>();
		}
		catch (Exception)
		{
			throw new Exception("حدثت مشكلة عند الاتصال مع قاعدة المعطيات");
		}
	}
}
