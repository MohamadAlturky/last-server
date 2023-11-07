using System.Net.NetworkInformation;
using Infrastructure.Authentication.Claims;
using Infrastructure.Authentication.JWTOptions;
using Microsoft.Extensions.Options;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Application.ExecutorProvider;

namespace Presentation.IdentityChecker;

public class JWTUserIdentityProvider : IExecutorIdentityProvider
{
	private readonly IHttpContextAccessor _httpContextAccessor;
	private readonly IOptions<JwtOptions> _options;
	public JWTUserIdentityProvider(IHttpContextAccessor httpContextAccessor, IOptions<JwtOptions> options)
	{
		_httpContextAccessor = httpContextAccessor;
		_options = options;
	}

	public string GetExecutorSerialNumber()
	{
		if (_httpContextAccessor.HttpContext is null)
		{
			throw new Exception("_httpContextAccessor.HttpContext is null");
		}
		string? jwt = _httpContextAccessor.HttpContext.Request.Headers["Authorization"];

		if (jwt is null)
		{
			throw new Exception("jwt is null");
		}

		jwt = jwt.Substring("Bearer ".Length).Trim();

		var tokenHandler = new JwtSecurityTokenHandler();

		var key = Encoding.ASCII.GetBytes(_options.Value.SecretKey);

		var token = tokenHandler.ReadJwtToken(jwt);

		string userSerialNumber = token.Claims.First(claim => claim.Type == CustomClaims.SerialNumber).Value;

		return userSerialNumber;
	}


	public string GetExecutorId()
	{
		if (_httpContextAccessor.HttpContext is null)
		{
			throw new Exception("_httpContextAccessor.HttpContext is null");
		}
		string? jwt = _httpContextAccessor.HttpContext.Request.Headers["Authorization"];

		if (jwt is null)
		{
			throw new Exception("jwt is null");
		}
		jwt = jwt.Substring("Bearer ".Length).Trim();

		var tokenHandler = new JwtSecurityTokenHandler();

		var key = Encoding.ASCII.GetBytes(_options.Value.SecretKey);

		var token = tokenHandler.ReadJwtToken(jwt);

		string userSerialNumber = token.Claims.First(claim => claim.Type == CustomClaims.Id).Value;

		return userSerialNumber;
	}
	public string GetMacAddress()
	{
		var mac = NetworkInterface
			.GetAllNetworkInterfaces()
			.Where(nic => nic.OperationalStatus == OperationalStatus.Up)
			.Select(nic => nic.GetPhysicalAddress().ToString())
			.FirstOrDefault();
		if(mac is null)
		{
			throw new Exception("if(mac is null)");
		}
		return mac;
	}
}
