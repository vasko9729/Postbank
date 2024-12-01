using Microsoft.AspNetCore.Mvc;
using Postbank.Application.Client;

namespace Postbank.API.Controllers;
[Route("api/[controller]")]
[ApiController]
public class HomeController : ControllerBase
{
	private readonly IClientService _clientService;
	public HomeController(IClientService clientService)
    {
		_clientService = clientService;
	}

	[HttpGet("configuration")]
	public async Task<IActionResult> GetHomeConfiguration(CancellationToken cancellationToken)
	{
		var homeConfiguration = await _clientService.GetHomeConfiguration(cancellationToken);
		return Ok(homeConfiguration);
	}
}
