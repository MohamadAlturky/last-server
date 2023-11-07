using Infrastructure.DataAccess.PermissionsService;
using Infrastructure.Authentication.JWTOptions;
using Infrastructure.Authentication.Claims;
using Infrastructure.Authentication.Models;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using System.Text;

namespace Infrastructure.Authentication.JWTProvider;
public class JwtProvider : IJwtProvider
{
	private readonly IPermissionService _permissionService;
	private readonly JwtOptions _options;

	public JwtProvider(IOptions<JwtOptions> options, IPermissionService permissionService)
	{
		_options = options.Value;
		_permissionService = permissionService;
	}

	public async Task<string> Generate(User user)
	{
		if (user.Customer is null)
		{
			throw new Exception("if(user.Customer is null)");
		}
		List<Claim> Claims = new List<Claim>()
		{
			new Claim(CustomClaims.Id,user.Id.ToString()),
			new Claim(CustomClaims.SerialNumber,user.Customer.SerialNumber.ToString()),
			new Claim(CustomClaims.Name,user.Customer.FirstName.ToString()+" "+ user.Customer.LastName.ToString()),
			new Claim(CustomClaims.IsActive,user.Customer.IsActive.ToString())
		};

		HashSet<string> userPermissions = await _permissionService.GetPermissions(user.Id);


		foreach (var permission in userPermissions)
		{
			Claims.Add(new Claim(CustomClaims.Permissions, permission));
		}

		SigningCredentials signingCredentials = new SigningCredentials(
			new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.SecretKey)),
			SecurityAlgorithms.HmacSha256);


		var token = new JwtSecurityToken(
			_options.Issuer,
			_options.Audience,
			Claims,
			null,
			DateTime.UtcNow.AddMinutes(60),
			signingCredentials);

		return new JwtSecurityTokenHandler().WriteToken(token);
	}
}
