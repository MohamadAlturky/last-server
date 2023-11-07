using Infrastructure.Authentication.Models;

namespace Infrastructure.Authentication.JWTProvider;
public interface IJwtProvider
{
	Task<string> Generate(User user);
}
