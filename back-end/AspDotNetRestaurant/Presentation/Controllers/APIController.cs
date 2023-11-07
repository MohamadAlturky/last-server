using MediatR;
using Microsoft.AspNetCore.Mvc;
using Presentation.Mappers;

namespace Presentation.Controllers;
[Route("api/[controller]")]
[ApiController]
public class APIController : ControllerBase
{
	protected ISender _sender;
	protected IMapper _mapper;

	public APIController(ISender sender ,IMapper mapper)
	{
		_sender = sender;
		_mapper = mapper;
	}
}
