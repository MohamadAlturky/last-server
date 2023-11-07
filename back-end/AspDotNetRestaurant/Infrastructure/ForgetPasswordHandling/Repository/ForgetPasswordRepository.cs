using Infrastructure.DataAccess.DBContext;
using Infrastructure.ForgetPasswordHandling.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.ForgetPasswordHandling.Repository;
internal class ForgetPasswordRepository : IForgetPasswordRepository
{

	private readonly RestaurantContext _context;

	public ForgetPasswordRepository(RestaurantContext context)
	{
		_context = context;
	}

	public async Task<ForgetPasswordEntry?> GetForgetPasswordEntryAsync(long id)
	{
		return await _context.Set<ForgetPasswordEntry>().SingleOrDefaultAsync(entry => entry.Id == id);
	}

	public void SaveInforamtion(ForgetPasswordEntry forgetPasswordEntry)
	{
		forgetPasswordEntry.Id = 0;
		_context.Set<ForgetPasswordEntry>().Add(forgetPasswordEntry);
	}
}
