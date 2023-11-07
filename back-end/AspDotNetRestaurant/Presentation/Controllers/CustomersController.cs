using Application.Customers.UseCases.DecreaseCustomerBalance;
using Application.Customers.UseCases.GetAccountTransactions;
using Application.Customers.UseCases.GetAll;
using Application.Customers.UseCases.GetSumOfCustomersBalances;
using Application.ExecutorProvider;
using Application.UseCases.Customers.GetByFilter;
using Application.UseCases.Customers.GetPage;
using Application.UseCases.Customers.IncreaseCustomerBalance;
using Domain.Customers.Aggregate;
using Domain.Customers.ValueObjects;
using Infrastructure.Authentication.EditUserInformation;
using Infrastructure.Authentication.Models;
using Infrastructure.Authentication.Permissions;
using Infrastructure.Authentication.Register;
using Infrastructure.DataAccess.UserPersistence;
using Infrastructure.DataAccess.UserPersistence.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Presentation.ApiModels;
using Presentation.ApiModels.Customers;
using Presentation.ApiModels.Register;
using Presentation.ApiModels.User;
using Presentation.Factories;
using Presentation.Mappers;
using Presentation.PermissionsContainer;
using SharedKernal.Utilities.Errors;
using SharedKernal.Utilities.Result;

namespace Presentation.Controllers;


[Route("api/[controller]")]
[ApiController]
public class CustomersController : APIController
{
    private readonly IUserPersistenceService _userPersistenceService;
    private readonly ILogger<CustomersController> _logger;
    private readonly IExecutorIdentityProvider _executorIdentityProvider;
    public CustomersController(IUserPersistenceService userPersistenceService, IExecutorIdentityProvider executorIdentityProvider, ISender sender, IMapper mapper, ILogger<CustomersController> logger) : base(sender, mapper)
    {
        _logger = logger;
        _executorIdentityProvider = executorIdentityProvider;
        _userPersistenceService = userPersistenceService;
    }

    [HttpGet("GetAllCustomers")]
    [HasPermission(AuthorizationPermissions.ReadSystemInformation)]
    public async Task<IActionResult> GetAllCustomers()
    {
        Result<List<Customer>> response = await _sender.Send(new GetAllCustomersQuery());

        if (response.IsFailure)
        {
            return BadRequest(Result.Failure(response.Error));
        }

        return Ok(response.Value.Select(customer => _mapper.Map(customer)).ToList());
    }

    [HttpGet("GetCustomersPage/{pageNumber}")]
    [HasPermission(AuthorizationPermissions.ReadSystemInformation)]
    public async Task<IActionResult> GetCustomersPage(int pageNumber)
    {
        try
        {
            CustomersPaginiationResponse customers = await _userPersistenceService.GetUserPaginatedAsync(pageNumber);


            return Ok(Result.Success(customers));
        }
        catch (Exception exception)
        {
            return BadRequest(Result.Failure(new Error("", exception.Message)));
        }
    }

    [HttpGet("GetCustomerBySerialNumber/{serialNumber}")]
    [HasPermission(AuthorizationPermissions.ReadSystemInformation)]
    public async Task<IActionResult> GetCustomerBySerialNumber(int serialNumber)
    {
        Result<Customer> response = await _sender.Send(new GetCustomerBySerialNumberQuery(serialNumber));

        if (response.IsFailure)
        {
            return BadRequest(Result.Failure(response.Error));
        }

        return Ok(Result.Success(_mapper.Map(response.Value)));
    }

    [HttpGet("GetSumOfCustomersBalances")]
    [HasPermission(AuthorizationPermissions.ReadSystemInformation)]
    public async Task<IActionResult> GetSumOfCustomersBalances()
    {
        Result<long> response = await _sender.Send(new GetSumOfCustomersBalancesQuery());

        if (response.IsFailure)
        {
            return BadRequest(Result.Failure(response.Error));
        }

        return Ok(response.Value);
    }

    [HttpPut("IncreaseCustomerBalance")]
    [HasPermission(AuthorizationPermissions.EditBalances)]
    public async Task<IActionResult> IncreaseCustomerBalance([FromForm] IncreaseBalanceRequest balanceRequest)
    {
        Result response = await _sender.Send(new IncreaseCustomerBalanceCommand(balanceRequest.serialNumber, balanceRequest.valueToAdd));

        if (response.IsFailure)
        {
            return BadRequest(Result.Failure(response.Error));
        }

        return Ok(response);
    }

    [HttpPut("DecreaseCustomerBalance")]
    [HasPermission(AuthorizationPermissions.CreateSystemInformation)]
    public async Task<IActionResult> DecreaseCustomerBalance(int serialNumber, int valueToRemove)
    {
        Result response = await _sender.Send(new DecreaseCustomerBalanceCommand(serialNumber, valueToRemove));

        if (response.IsFailure)
        {
            return BadRequest(Result.Failure(response.Error));
        }

        return Ok(response);
    }

    [HttpGet("AccountTransactionsPage/{pageNumber}")]
    [HasPermission(AuthorizationPermissions.ReadContent)]
    public async Task<IActionResult> GetAccountTransactionsPage(int pageNumber)
    {
        int serialNumber = int.Parse(_executorIdentityProvider.GetExecutorSerialNumber());
        Result<Domain.Customers.ReadModels.AccountTransactionsReadModel> response = await _sender.Send(
            new GetAccountTransactionsQuery(serialNumber, pageNumber));

        if (response.IsFailure)
        {
            return BadRequest(Result.Failure(response.Error));
        }

        return Ok(response);
    }

    [HttpGet("GetCustomerInformation/{serialNumber}")]
    [HasPermission(AuthorizationPermissions.ReadSystemInformation)]
    public async Task<IActionResult> GetCustomerInformation(int serialNumber)
    {

        try
        {
            User? user = await _userPersistenceService.GetUserAsync(serialNumber);
            if (user is null)
            {
                return BadRequest(Result.Failure(new("", "if (user is null)")));
            }

            return Ok(Result.Success(new CustomerInformation()
            {
                Id = user.Id,
                HiastMail = user.HiastMail,
                SerialNumber = user.Customer.SerialNumber,
                FirstName = user.Customer.FirstName,
                Notes = user.Customer.Notes,
                LastName = user.Customer.LastName,
                Balance = user.Customer.Balance,
                Category = user.Customer.Category,
                BelongsToDepartment = user.Customer.BelongsToDepartment,
            }));
        }
        catch (Exception exception)
        {
            return BadRequest(Result.Failure(new Error("", exception.Message)));
        }
    }

    [HttpPut("EditCustomerInformation")]
    [HasPermission(AuthorizationPermissions.RegisterCustomer)]
    public async Task<IActionResult> EditCustomerInformation([FromForm] EditCustomerInformationRequest model)
    {
        //if (!ModelState.IsValid)
        //{
        //	return BadRequest(Result.Failure(new Error("Model State", "Model State is not valid")));
        //}

        try
        {
            Result response = await _sender.Send(new EditUserInformationCommand(UserFactory.Create(model)));

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
}
