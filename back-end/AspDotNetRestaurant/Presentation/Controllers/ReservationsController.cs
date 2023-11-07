using Application.ExecutorProvider;
using Application.MealEntries.UseCases.GetStatisticAboutReservationsCustomersType;
using Application.Reservations.UseCases.Cancel;
using Application.Reservations.UseCases.ConsumeReservation;
using Application.Reservations.UseCases.Create;
using Application.Reservations.UseCases.GetBetweenTwoDates;
using Application.Reservations.UseCases.GetByCustomerSerialNumber;
using Application.Reservations.UseCases.GetByDate;
using Application.Reservations.UseCases.GetByMealId;
using Infrastructure.Authentication.Permissions;
using Infrastructure.DataAccess.UserPersistence;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Presentation.ApiModels.Reservations;
using Presentation.Mappers;
using Presentation.PermissionsContainer;
using SharedKernal.Utilities.Errors;
using SharedKernal.Utilities.Result;
using System;

namespace Presentation.Controllers;
[Route("api/[controller]")]
[ApiController]
public class ReservationsController : APIController
{
    private readonly ILogger<ReservationsController> _logger;
    private readonly IExecutorIdentityProvider _identityProvider;
    private readonly IUserPersistenceService _userPersistenceService;
    public ReservationsController(IUserPersistenceService userPersistenceService, ILogger<ReservationsController> logger, ISender sender, IMapper mapper, IExecutorIdentityProvider identityProvider)
        : base(sender, mapper)
    {
        _logger = logger;
        _identityProvider = identityProvider;
        _userPersistenceService = userPersistenceService;
    }


    [HttpPost("Create")]
    [HasPermission(AuthorizationPermissions.OrderContent)]
    public async Task<IActionResult> Create([FromForm] long orderedMealId)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(Result.Failure(new Error("Model State", "Model State is not valid")));
        }

        try
        {

            long customerId = long.Parse(_identityProvider.GetExecutorId());
            Result<CreateReservationResponse> response = await _sender.Send(new CreateReservationCommand(customerId, orderedMealId));

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

    [HttpPost("Cancel")]
    [HasPermission(AuthorizationPermissions.OrderContent)]
    public async Task<IActionResult> Cancel([FromForm] long reservationId)
    {
        try
        {
            Result response = await _sender.Send(new CancelReservationCommand(reservationId));

            if (response.IsFailure)
            {
                return BadRequest(Result.Failure(response.Error));
            }

            return Ok(response);
        }
        catch (Exception exception)
        {
            return BadRequest(Result.Failure(new Error("", exception.Message)));
        }
    }

    [HttpGet("GetReservationsByDate")]
    [HasPermission(AuthorizationPermissions.ReadSystemInformation)]
    public async Task<IActionResult> GetReservationsByDate(string date)
    {
        try
        {
            DateOnly dateFilter = DateOnly.Parse(date);

            var response = await _sender.Send(new GetReservationsByDateQuery(dateFilter));

            if (response.IsFailure)
            {
                return BadRequest(Result.Failure(response.Error));
            }
            return Ok(response.Value);

        }
        catch (Exception exception)
        {
            return BadRequest(Result.Failure(new Error("", exception.Message)));
        }
    }

    [HttpGet("GetReservationsBetweenTwoDates")]
    [HasPermission(AuthorizationPermissions.ReadSystemInformation)]
    public async Task<IActionResult> GetReservationsBetweenTwoDates(string startDate, string endDate)
    {
        try
        {
            DateOnly start = DateOnly.Parse(startDate);
            DateOnly end = DateOnly.Parse(endDate);

            if (start > end)
            {
                throw new Exception("start > end");
            }

            var response = await _sender.Send(new GetReservationsBetweenTwoDatesQuery(start, end));

            if (response.IsFailure)
            {
                return BadRequest(Result.Failure(response.Error));
            }
            return Ok(response.Value);

        }
        catch (Exception exception)
        {
            return BadRequest(Result.Failure(new Error("", exception.Message)));
        }
    }




    [HttpGet("GetReservationsByCustomerSerialNumber")]
    [HasPermission(AuthorizationPermissions.ReadSystemInformation)]
    public async Task<IActionResult> GetReservationsByCustomerSerialNumber(int serialNumber)
    {
        try
        {
            var response = await _sender.Send(new GetReservationsByCustomerSerialNumberQuery(serialNumber));

            if (response.IsFailure)
            {
                return BadRequest(Result.Failure(response.Error));
            }
            return Ok(response.Value);

        }
        catch (Exception exception)
        {
            return BadRequest(Result.Failure(new Error("", exception.Message)));
        }
    }



    [HttpGet("GetStatisticAboutReservationsCustomersType/{mealEntryId}")]
    [HasPermission(AuthorizationPermissions.ReadSystemInformation)]
    public async Task<IActionResult> GetStatisticAboutReservationsCustomersType(long mealEntryId)
    {
        try
        {
            var response = await _sender.Send(new GetStatisticAboutReservationsCustomersTypeQuery(mealEntryId));

            if (response.IsFailure)
            {
                return BadRequest(Result.Failure(response.Error));
            }
            return Ok(response);

        }
        catch (Exception exception)
        {
            return BadRequest(Result.Failure(new Error("", exception.Message)));
        }
    }

    [HttpGet("GetReservationsByMealId/{mealEntryId}")]
    [HasPermission(AuthorizationPermissions.ReadSystemInformation)]
    public async Task<IActionResult> GetReservationsByMealId(long mealEntryId)
    {
        try
        {
            var response = await _sender.Send(new GetReservationsByMealIdQuery(mealEntryId));

            if (response.IsFailure)
            {
                return BadRequest(Result.Failure(response.Error));
            }
            return Ok(response);

        }
        catch (Exception exception)
        {
            return BadRequest(Result.Failure(new Error("", exception.Message)));
        }
    }



    [HttpPut("Consume")]
    [HasPermission(AuthorizationPermissions.ConsumeReservations)]
    public async Task<IActionResult> Consume([FromForm] ReservationConsumeRequest request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(Result.Failure(new Error("Model State", "Model State is not valid")));
        }
        try
        {
            Result checkUserInformation = _userPersistenceService.CheckPasswordValidity(request.SerialNumber, request.Password);


            if (checkUserInformation.IsFailure)
            {
                return BadRequest(Result.Failure(checkUserInformation.Error));
            }

            Result<Domain.Reservations.Aggregate.Reservation> response = await _sender.Send(new ConsumeReservationCommand(request.SerialNumber,
                request.MealEntryId));

            if (response.IsFailure)
            {
                return BadRequest(Result.Failure(response.Error));
            }

            var answer = new ConsumeReservationResponse()
            {
                CustomerName = response.Value.Customer.FirstName + " " + response.Value.Customer.LastName
            };

            return Ok(Result.Success(answer));
        }
        catch (Exception exception)
        {
            return BadRequest(Result.Failure(new Error("Model State", exception.Message)));
        }
    }
}
