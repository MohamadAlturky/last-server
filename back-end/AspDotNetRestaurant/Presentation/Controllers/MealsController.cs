using Application.ExecutorProvider;
using Application.MealEntries.UseCases.CancelMealEntry;
using Application.MealInformations.UseCases.GetPage;
using Application.Meals.UseCases.Delete;
using Application.Meals.UseCases.GetMealEntries;
using Application.Meals.UseCases.GetMealEntriesByDate;
using Application.Meals.UseCases.GetMealsByName;
using Application.Meals.UseCases.GetMealsSchedule;
using Application.Meals.UseCases.GetWeeklyMeals;
using Application.Meals.UseCases.PrepareNewMeal;
using Application.Meals.UseCases.Update;
using Application.UseCases.Meals.Create;
using Application.UseCases.Meals.GetAll;
using Domain.MealInformations.ReadModels;
using Domain.Meals.Repositories;
using Domain.Meals.ValueObjects;
using Infrastructure.Authentication.Permissions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Presentation.ApiModels;
using Presentation.ApiModels.MealEntry;
using Presentation.ApiModels.Meals;
using Presentation.Mappers;
using Presentation.PermissionsContainer;
using Presentation.Services.MealsImagesSaver;
using SharedKernal.Repositories;
using SharedKernal.Utilities.Errors;
using SharedKernal.Utilities.Result;

namespace Presentation.Controllers;


[Route("api/[controller]")]
[ApiController]
public class MealsController : APIController
{
    private readonly ILogger<MealsController> _logger;
    private readonly IAssetsSaver _assetsSaver;
    private readonly IExecutorIdentityProvider _identityProvider;
    private readonly IMealEntryRepository mealEntryRepository;
    private readonly IUnitOfWork _unitOfWork;

    public MealsController(IUnitOfWork unitOfWork, ISender sender,
        IMapper mapper,
        ILogger<MealsController> logger,
        IAssetsSaver assetsSaver,
        IExecutorIdentityProvider identityProvider,
        IMealEntryRepository mealEntryRepository
        )
        : base(sender, mapper)
    {
        _unitOfWork = unitOfWork;
        this.mealEntryRepository = mealEntryRepository;
        _assetsSaver = assetsSaver;
        _logger = logger;
        _identityProvider = identityProvider;
    }


    [HttpPost("Create")]
    [HasPermission(AuthorizationPermissions.CreateSystemInformation)]
    public async Task<IActionResult> Create([FromForm] CreateMealRequest meal)
    {
        try
        {
            MealDTO mealDTO = new MealDTO()
            {
                Id = 0,
                ImagePath = "",
                Type = meal.Type,
                Name = meal.Name,
                Description = meal.Description,
                NumberOfCalories = meal.NumberOfCalories
            };
            IFormFile file = meal.ImageFile;
            if (!ModelState.IsValid || !file.ContentType.StartsWith("image/"))
            {
                return BadRequest(Result.Failure(new Error("Model State", "Model State is not valid")));
            }
            var imagePathResult = await _assetsSaver.SaveFile(file, "MealsImages");
            if (imagePathResult.IsSuccess)
            {
                mealDTO.ImagePath = imagePathResult.Value;
            }
            else
            {
                throw new Exception(imagePathResult.Error.Message);
            }
            Result response = await _sender.Send(new CreateMealCommand(_mapper.Map(mealDTO)));

            if (response.IsFailure)
            {
                return BadRequest(Result.Failure(response.Error));
            }

            return Ok(response);
        }
        catch (Exception exception)
        {
            return BadRequest(Result.Failure(new Error(exception.StackTrace.ToString(), exception.Message)));
        }
    }

    [HttpPut("Update")]
    [HasPermission(AuthorizationPermissions.CreateSystemInformation)]
    public async Task<IActionResult> Update([FromForm] UpdateMealRequest meal)
    {
        try
        {
            MealDTO mealDTO = new MealDTO()
            {
                Id = meal.Id,
                Type = meal.Type,
                Name = meal.Name,
                Description = meal.Description,
                NumberOfCalories = meal.NumberOfCalories
            };
            Result response = await _sender.Send(new UpdateMealCommand(_mapper.Map(mealDTO)));

            if (response.IsFailure)
            {
                return BadRequest(Result.Failure(response.Error));
            }

            return Ok(response);
        }
        catch (Exception exception)
        {
            return BadRequest(Result.Failure(new Error(exception.StackTrace.ToString(), exception.Message)));
        }
    }

    //[HttpPost("CreateFile")]
    //public async Task<IActionResult> CreateFile(IFormFile ImageFile)
    //{
    //	return Ok("ImageFile");
    //}

    [HttpDelete("DeleteMealEntry/{mealEntryId}")]
    [HasPermission(AuthorizationPermissions.CreateSystemInformation)]
    public async Task<IActionResult> DeleteMealEntry(long mealEntryId)
    {
        var response = await _sender.Send(new CancelMealEntryCommand(mealEntryId));

        if (response.IsFailure)
        {
            return BadRequest(Result.Failure(response.Error));
        }

        return Ok(Result.Success());
    }

    [HttpDelete("DeleteMealInformation/{mealId}")]
    [HasPermission(AuthorizationPermissions.CreateSystemInformation)]
    public async Task<IActionResult> DeleteMealInformation(long mealId)
    {
        var response = await _sender.Send(new DeleteMealInformationCommand(mealId));

        if (response.IsFailure)
        {
            return BadRequest(Result.Failure(response.Error));
        }

        return Ok(Result.Success());
    }


    [HttpGet("GetAllMeals")]
    [HasPermission(AuthorizationPermissions.ReadSystemInformation)]
    public async Task<IActionResult> GetAllMeals()
    {
        var response = await _sender.Send(new GetMealsQuery());

        if (response.IsFailure)
        {
            return BadRequest(Result.Failure(response.Error));
        }

        return Ok(Result.Success(response.Value.Select(meal => _mapper.Map(meal)).ToList()));
    }

    [HttpGet("GetMealsPage/{pageNumber}")]
    [HasPermission(AuthorizationPermissions.ReadSystemInformation)]
    public async Task<IActionResult> GetMealsPage(int pageNumber)
    {
        Result<MealsInformationReadModel> response = await _sender.Send(new GetMealsInformationPageQuery(pageNumber));

        if (response.IsFailure)
        {
            return BadRequest(Result.Failure(response.Error));
        }

        return Ok(response);
    }

    [HttpGet("GetMealsByNameAndType/{mealName}/{mealType}")]
    [HasPermission(AuthorizationPermissions.ReadSystemInformation)]
    public async Task<IActionResult> GetMealsByNameAndType(string mealName, string mealType)
    {
        MealType type = Enum.Parse<MealType>(mealType);
        var response = await _sender.Send(new GetMealsByNameQuery(mealName, type));

        if (response.IsFailure)
        {
            return BadRequest(Result.Failure(response.Error));
        }

        return Ok(Result.Success(response.Value.Select(meal => _mapper.Map(meal)).ToList()));
    }

    [HttpGet("GetMealEntries")]
    [HasPermission(AuthorizationPermissions.ReadSystemInformation)]
    public async Task<IActionResult> GetMealEntries(long Id)
    {
        var response = await _sender.Send(new GetMealEntriesQuery(Id));

        if (response.IsFailure)
        {
            return BadRequest(Result.Failure(response.Error));
        }

        return Ok(response.Value);
    }

    [HttpGet("GetMealEntriesByDate")]
    [HasPermission(AuthorizationPermissions.ReadSystemInformation)]
    public async Task<IActionResult> GetMealEntriesByDate(string date)
    {

        try
        {
            DateOnly dateFilter = DateOnly.Parse(date);

            var response = await _sender.Send(new GetMealEntriesByDateQuery(dateFilter));

            if (response.IsFailure)
            {
                return BadRequest(Result.Failure(response.Error));
            }
            return Ok(Result.Success(response.Value.Select(entry => _mapper.Map(entry))));

        }
        catch (Exception exception)
        {
            return BadRequest(Result.Failure(new Error("", exception.Message)));
        }
    }


    [HttpGet("GetMealEntriesByDateForConsume")]
    [HasPermission(AuthorizationPermissions.ConsumeReservations)]
    public async Task<IActionResult> GetMealEntriesByDateForConsume(string date)
    {

        try
        {
            DateOnly dateFilter = DateOnly.Parse(date);

            var response = await _sender.Send(new GetMealEntriesByDateQuery(dateFilter));

            if (response.IsFailure)
            {
                return BadRequest(Result.Failure(response.Error));
            }
            return Ok(Result.Success(response.Value.Select(entry => _mapper.Map(entry))));

        }
        catch (Exception exception)
        {
            return BadRequest(Result.Failure(new Error("", exception.Message)));
        }
    }


    [HttpGet("GetWeeklyMeals")]
    [HasPermission(AuthorizationPermissions.ReadContent)]
    public async Task<IActionResult> GetWeeklyMeals(int WeekNumber)
    {
        try
        {
            DateTime target = DateTime.Now.AddDays(7 * WeekNumber);

            DateOnly dateFilter = new DateOnly(target.Year, target.Month, target.Day);

            long id = long.Parse(_identityProvider.GetExecutorId());

            var response = await _sender.Send(new GetWeeklyMealsQuery(dateFilter, id));

            if (response.IsFailure)
            {
                return BadRequest(Result.Failure(response.Error));
            }
            return Ok(Result.Success(response.Value));

        }
        catch (Exception exception)
        {
            return BadRequest(Result.Failure(new Error("", exception.Message)));
        }
    }
    /// <summary>
    /// ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /// </summary>
    /// <param name="WeekNumber"></param>
    /// <returns></returns>

    private readonly Random random = new();
    [HttpGet("GetWeeklyMealsFromRoute/{WeekNumber}")]
    [HasPermission(AuthorizationPermissions.ReadContent)]
    public async Task<IActionResult> GetWeeklyMealsFromRoute(int weekNumber)
    {
        try
        {

            int rand = random.Next() % 100;
            DateTime target = DateTime.Now.AddDays(7 * rand);

            DateOnly dateFilter = new DateOnly(target.Year, target.Month, target.Day);

            long id = long.Parse(_identityProvider.GetExecutorId());
            var response = await _sender.Send(new GetWeeklyMealsQuery(dateFilter, id));

            if (response.IsFailure)
            {
                return BadRequest(Result.Failure(response.Error));
            }

            return Ok(Result.Success(response.Value));

        }
        catch (Exception exception)
        {
            return BadRequest(Result.Failure(new Error("", exception.Message)));
        }
    }


    [HttpGet("GetMealsSchedule")]
    public async Task<IActionResult> GetMealsSchedule(int WeekNumber)
    {
        try
        {
            DateTime target = DateTime.Now.AddDays(7 * WeekNumber);

            DateOnly dateFilter = new DateOnly(target.Year, target.Month, target.Day);

            var response = await _sender.Send(new GetMealsScheduleQuery(dateFilter));

            if (response.IsFailure)
            {
                return BadRequest(Result.Failure(response.Error));
            }
            return Ok(Result.Success(response.Value));

        }
        catch (Exception exception)
        {
            return BadRequest(Result.Failure(new Error("", exception.Message)));
        }
    }


    [HttpPost("PrepareMeal")]
    [HasPermission(AuthorizationPermissions.CreateSystemInformation)]
    public async Task<IActionResult> PrepareMeal([FromForm] PrepareRequest request)
    {
        try
        {
            DateOnly atDayValue = DateOnly.Parse(request.atDay);
            Result response = await _sender.Send(new PrepareNewMealCommand(request.mealId, atDayValue, request.numberOfUnits));

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
