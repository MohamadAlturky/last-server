using Infrastructure.Authentication.JWTOptions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Presentation.OptionsSetup.JWTBearerOptionsSetup;

public class JwtBearerOptionsSetup : IConfigureOptions<JwtBearerOptions>
{
	private readonly JwtOptions _jwtOptions;

	public JwtBearerOptionsSetup(IOptions<JwtOptions> options)
	{
		_jwtOptions = options.Value;
	}

	public void Configure(JwtBearerOptions options)
	{
		options.TokenValidationParameters = new TokenValidationParameters
		{
			ValidateIssuerSigningKey = true,
			IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_jwtOptions.SecretKey)),
			ValidateIssuer = false,
			ValidateAudience = false,
			RequireExpirationTime = false,
			ValidateLifetime = true
		};
	}
}
