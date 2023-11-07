// using Infrastructure.Authentication.PasswordHashing;
// using MediatR;
// using Microsoft.AspNetCore.Mvc;
// using Microsoft.Extensions.Localization;
// using Presentation.Mappers;
// using System.Net.Mail;
// using Localization;
// using Domain.Shared.Utilities;
// using System.Net;
// using Domain.Localization;
// using Application.Meals.UseCases.PrepareNewMeal;
// using SharedKernal.Utilities.Result;
// using Application.Reservations.UseCases.Create;
// using SharedKernal.Utilities.Errors;
// using Infrastructure.Authentication.Register;
// using Infrastructure.Authentication.Models;
// using Domain.Customers.Aggregate;
// using Infrastructure.Authorization.Dictionaries;
// using Domain.Customers.ValueObjects;

// namespace Presentation.Controllers;
// [Route("api/[controller]")]
// [ApiController]
// public class DevelopmentController : APIController
// {
// 	private readonly IHashHandler _hashHandler;
// 	private readonly IStringLocalizer<SharedResourcesProvider> _localizer;
// 	public DevelopmentController(ISender sender, IMapper mapper, IHashHandler hashHandler, IStringLocalizer<SharedResourcesProvider> localizer) : base(sender, mapper)
// 	{
// 		_hashHandler = hashHandler;
// 		_localizer = localizer;
// 	}
// 	//[HttpPost("SeedSuperUsers")]
// 	//public async Task<IActionResult> SeedSuperUsers(string password)
// 	//{
// 	//	try
// 	//	{
// 	//		// admin123 
// 	//		if (_hashHandler.GetHash(password) != "JAvlGPq9JyTdtvBO6x2llnRI1+gxwIyPqCKAn3THIKk=")
// 	//		{
// 	//			throw new Exception("خطأ في كلمة المرور");
// 	//		}

// 	//		var response = await _sender.Send(new SeedAdminCommad());

// 	//		return Ok(response);
// 	//	}
// 	//	catch (Exception exception)
// 	//	{
// 	//		return BadRequest(exception.Message);
// 	//	}
// 	//}

// 	//[HttpPost("PricingSeeder")]
// 	//public async Task<IActionResult> SeedPricing(string password)
// 	//{
// 	//	try
// 	//	{
// 	//		// admin123 
// 	//		if (_hashHandler.GetHash(password) != "JAvlGPq9JyTdtvBO6x2llnRI1+gxwIyPqCKAn3THIKk=")
// 	//		{
// 	//			throw new Exception("خطأ في كلمة المرور");
// 	//		}

// 	//		_pricingSeeder.Seed();

// 	//		return Ok("DONE");
// 	//	}
// 	//	catch (Exception exception)
// 	//	{
// 	//		return BadRequest(exception.Message);
// 	//	}
// 	//}


// 	[HttpPost("SendEmail")]
// 	public async Task<IActionResult> SendEmail()
// 	{
// 		try
// 		{
// 			var fromAddress = new MailAddress("mohamad.alturky@hiast.edu.sy");
// 			var toAddress = new MailAddress("mohammad.salama@hiast.edu.sy");
// 			const string fromPassword = "/.,m0987";
// 			const string subject = "Admitting your Superiority";
// 			const string body = "This is turkey and I recognize you as King of SHABAKAT AND OS";

// 			var smtpClient = new SmtpClient
// 			{
// 				Host = "mail.hiast.edu.sy",
// 				Port = 25,
// 				EnableSsl = false,
// 				DeliveryMethod = SmtpDeliveryMethod.Network,
// 				UseDefaultCredentials = false,
// 				Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
// 			};

// 			var message = new MailMessage(fromAddress, toAddress)
// 			{
// 				Subject = subject,
// 				Body = body
// 			};

// 			await smtpClient.SendMailAsync(message);


// 			//SmtpClient client = new SmtpClient();

// 			//MailAddress from = new MailAddress("jane@contoso.com",
// 			//   "Jane " + (char)0xD8 + " Clayton",
// 			//System.Text.Encoding.UTF8);


// 			//MailAddress to = new MailAddress("ben@contoso.com");


// 			//MailMessage message = new MailMessage(fromAddress, toAddress);
// 			//message.Body = "This is a test email message sent by an application. ";


// 			//string someArrows = new string(new char[] { '\u2190', '\u2191', '\u2192', '\u2193' });
// 			//message.Body += Environment.NewLine + someArrows;
// 			//message.BodyEncoding = System.Text.Encoding.UTF8;
// 			//message.Subject = "test message 1" + someArrows;
// 			//message.SubjectEncoding = System.Text.Encoding.UTF8;



// 			//string userState = "test message1";

// 			//client.SendAsync(message, userState);


// 			return Ok();
// 		}
// 		catch (Exception exception)
// 		{
// 			return BadRequest(exception.Message);
// 		}
// 	}


// 	[HttpGet("TestLocalization")]
// 	public IActionResult TestLocalization()
// 	{
// 		return Ok(_localizer["Name"]);
// 	}

// 	[HttpGet("TestLocalizationProvider")]
// 	public IActionResult TestLocalizationProvider()
// 	{
// 		return Ok(LocalizationProvider
// 			.GetResource(DomainResourcesKeys.Name));
// 	}

// 	[HttpGet("Week")]
// 	public IActionResult Week()
// 	{
// 		//DateTime dt = DateTime.Now;
// 		//List<DayOfWeek> dayOfWeeks = new List<DayOfWeek>();
// 		//foreach (DayOfWeek dayOfWeek in Enum.GetValues(typeof(DayOfWeek)))
// 		//{
// 		//	if(dt.DayOfWeek == dayOfWeek)
// 		//	{
// 		//		return Ok(dayOfWeek.ToString());
// 		//	}
// 		//}
// 		//return NotFound();

// 		List<DateTime> dateTimes = new List<DateTime>();
// 		for (int i = 0; i < 10; i++)
// 		{
// 			dateTimes.Add(Date.LastSaturdayFrom(DateTime.Now.AddDays(i)));
// 		}

// 		return Ok(dateTimes);
// 	}

// 	[HttpGet("SeedMeals")]
// 	public async Task<IActionResult> SeedMeals(long id)
// 	{

// 		for (int i = 0; i < 800; i++)
// 		{

// 			Result response = await _sender.Send(new
// 				PrepareNewMealCommand(id,
// 				new DateOnly(DateTime.Now.AddDays(i).Year,
// 				DateTime.Now.AddDays(i).Month, DateTime.Now.AddDays(i).Day), 114));
// 		}

// 		return Ok();
// 	}
// 	[HttpPost("SeedReservations")]
// 	public async Task<IActionResult> SeedReservations(long customerId, int from)
// 	{
// 		for (int i = from; i < 1500; i++)
// 		{

// 			Result response = await _sender.Send(new
// 				CreateReservationCommand(customerId, i));
// 			if (response.IsFailure)
// 			{
// 				return BadRequest(new
// 				{
// 					response.Error,
// 					i
// 				});
// 			}
// 		}

// 		return Ok();
// 	}

// 	[HttpGet("ResultApproach")]
// 	public IActionResult ResultApproach()
// 	{
// 		Result result = Result.Success();
// 		for (int i = 0; i < 10; i++)
// 		{
// 			if (i == 5)
// 			{
// 				result = Result.Failure(Error.Null);
// 				break;
// 			}
// 		}
// 		if (result.IsSuccess)
// 		{
// 			return BadRequest();
// 		}
// 		else
// 		{
// 			return Ok();
// 		}

// 	}
// 	[HttpGet("ExceptionApproach")]
// 	public IActionResult ExceptionApproach()
// 	{
// 		try
// 		{
// 			for (int i = 0; i < 10; i++)
// 			{
// 				if (i == 5)
// 				{
// 					throw new Exception();
// 				}
// 			}
// 			return BadRequest();
// 		}
// 		catch (Exception)
// 		{
// 			return Ok();
// 		}
// 	}
// 	[HttpPost("SeedCustomers")]
// 	public async Task<IActionResult> SeedCustomers(int to)
// 	{
// 		for (int i = 10000; i <= to; i++)
// 		{
// 			User user = new()
// 			{
// 				Customer = new Customer()
// 				{
// 					Balance =11111111,
// 					SerialNumber = i,
// 					Category=CustomerType.VeryPoorPeaple.ToString(),
// 					Notes="",
// 					BelongsToDepartment=Department.UNDefined.ToString(),
// 					FirstName="شخص",
// 					LastName="منيح",
// 					Id=0
// 				},
// 				Roles = new[]
// 				{
// 					RolesDictionary.Consumer
// 				}
// 			};
// 			Result response = await _sender.Send(new
// 				RegisterNewCustomerCommand(user, i.ToString()));
// 			if (response.IsFailure)
// 			{
// 				return BadRequest(new
// 				{
// 					response.Error,
// 					i
// 				});
// 			}
// 		}

// 		return Ok();
// 	}
// 	[HttpPost("SeedReservations2")]
// 	public async Task<IActionResult> SeedReservations2(long to)
// 	{
// 		for(int j = 500; j < 1000; j++)
// 		{
// 			for (int i = 50; i <= to; i++)
// 			{
// 				Result response = await _sender.Send(new
// 					CreateReservationCommand(j, i));
// 				if (response.IsFailure)
// 				{
// 					return BadRequest(new
// 					{
// 						response.Error,
// 						i
// 					});
// 				}
// 			}
// 		}

// 		return Ok();
// 	}
// }
