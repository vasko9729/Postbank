using Microsoft.AspNetCore.Mvc;
using Postbank.Application.Users;
using Postbank.Application.Users.Requests;

namespace Postbank.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsersController : ControllerBase
{
	private readonly IUserService _userService;
	public UsersController(IUserService userService)
	{
		_userService = userService;
	}

	[HttpGet]
	public async Task<IActionResult> GetUsers([FromQuery] UsersRequest request)
	{
		var users = await _userService.GetUsers(request);

		return Ok(users);
	}
}
