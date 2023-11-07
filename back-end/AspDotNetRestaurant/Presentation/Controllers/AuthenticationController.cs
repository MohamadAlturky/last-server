using MediatR;
using Presentation.Mappers;
using Microsoft.AspNetCore.Mvc;
using SharedKernal.Utilities.Errors;
using SharedKernal.Utilities.Result;
using Infrastructure.Authentication.LogIn;
using Infrastructure.Authentication.Models;
using Presentation.ApiModels.Register;
using Infrastructure.Authentication.Register;
using Presentation.Factories;
using Infrastructure.Authentication.Permissions;
using Presentation.PermissionsContainer;
using Presentation.ApiModels;
using Infrastructure.DataAccess.UserPersistence;
using Infrastructure.ForgetPasswordHandling.ForgetPasswordServices;
using Presentation.ApiModels.User;
using Application.ExecutorProvider;

namespace Presentation.Controllers;
[Route("api/[controller]")]
[ApiController]
public class AuthenticationController : APIController
{
    private readonly ILogger<AuthenticationController> _logger;
    private readonly IUserPersistenceService _userPersistenceService;
    private readonly IExecutorIdentityProvider _identityProvider;
    private readonly IForgetPasswordService _forgetPasswordService;
    public AuthenticationController(IForgetPasswordService forgetPasswordService, IExecutorIdentityProvider identityProvider, IUserPersistenceService persistenceService, ILogger<AuthenticationController> logger, ISender sender, IMapper mapper)
        : base(sender, mapper)
    {
        _forgetPasswordService = forgetPasswordService;
        _identityProvider = identityProvider;
        _logger = logger;
        _userPersistenceService = persistenceService;
    }


    [HttpPost("LogIn")]
    public async Task<IActionResult> LogIn([FromBody] LogInModel model)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(Result.Failure(new Error("Model State", "Model State is not valid")));
        }

        try
        {
            Result<string> response = await _sender.Send(new LogInCommand(model));

            if (response.IsFailure)
            {
                Result result = Result.Failure(response.Error);
                return BadRequest(result);
            }
            //Thread.Sleep(1000);

            return Ok(response);
        }
        catch (Exception exception)
        {
            return BadRequest(Result.Failure(new Error("Model State", exception.Message)));
        }
    }





    [HttpPut("ChangePassword")]
    [HasPermission(AuthorizationPermissions.ReadContent)]
    public async Task<IActionResult> ChangePassword([FromForm] ChangePasswordModel model)
    {
        try
        {
            long id = long.Parse(_identityProvider.GetExecutorId());

            await _userPersistenceService.ChangePassword(id, model.OldPassword, model.NewPassword);

            return Ok();
        }
        catch (Exception exception)
        {
            return BadRequest(Result.Failure(new Error("Problem", exception.Message)));
        }
    }



    [HttpPost("Register")]
    [HasPermission(AuthorizationPermissions.RegisterCustomer)]
    public async Task<IActionResult> Register([FromForm] RegistrationModel model)
    {
        //if (!ModelState.IsValid)
        //{
        //	return BadRequest(Result.Failure(new Error("Model State", "Model State is not valid")));
        //}

        try
        {
            Result response = await _sender.Send(new RegisterNewCustomerCommand(UserFactory.Create(model), model.Password));

            if (response.IsFailure)
            {
                return BadRequest(Result.Failure(response.Error));
            }

            return Ok(response);
        }
        catch (Exception exception)
        {
            return BadRequest(Result.Failure(new Error("Model State", exception.Message)));
        }
    }


    [HttpPost("ForgetPassword")]
    public async Task<IActionResult> ForgetPassword([FromBody] int serialNumber)
    {

        try
        {
            Result<long> response = await _forgetPasswordService.SendCodeVIAMailAsync(serialNumber);

            if (response.IsFailure)
            {
                return BadRequest(Result.Failure(response.Error));
            }

            return Ok(response);
        }
        catch (Exception exception)
        {
            return BadRequest(Result.Failure(new Error("Model State", exception.Message)));
        }
    }

    [HttpPost("VerifyCode")]
    public async Task<IActionResult> VerifyCode([FromForm] VerifyCodeRequest verifyCode)
    {
        try
        {
            Result response = await _forgetPasswordService.VerifyCode(verifyCode.EntryId, verifyCode.Code);

            if (response.IsFailure)
            {
                return BadRequest(Result.Failure(response.Error));
            }

            _userPersistenceService.UpdateUserPassword(verifyCode.SerialNumber, verifyCode.Password);

            return Ok(response);
        }
        catch (Exception exception)
        {
            return BadRequest(Result.Failure(new Error("Model State", exception.Message)));
        }
    }

    [HttpGet("GetAccountInformation")]
    [HasPermission(AuthorizationPermissions.SeePublicContent)]
    public async Task<IActionResult> GetAccountInformation()
    {

        try
        {
            User? user = await _userPersistenceService.GetUserAsync(int.Parse(_identityProvider.GetExecutorSerialNumber()));
            if (user is null)
            {
                return BadRequest(Result.Failure(new("", "if (user is null)")));
            }

            return Ok(Result.Success(new UserDTO()
            {
                Id = user.Id,
                FirstName = user.Customer.FirstName,
                LastName = user.Customer.LastName,
                HiastMail = user.HiastMail,
                MacAddress = _identityProvider.GetMacAddress(),
                Balance = user.Customer.Balance
            }));
        }
        catch (Exception exception)
        {
            return BadRequest(Result.Failure(new Error("", exception.Message)));
        }
    }
}
