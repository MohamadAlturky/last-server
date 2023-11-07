using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Controllers;
[ApiController]
[Route("[controller]")]
public class DemoController : ControllerBase
{

	private readonly ILogger<DemoController> _logger;

	public DemoController(ILogger<DemoController> logger)
	{
		_logger = logger;
		_logger.LogInformation("go>>>>>>");
	}

	[HttpGet(Name = "get")]
	public string Get()
	{
		_logger.LogInformation("go>>>>>>");
		return "hello";
	}
}
