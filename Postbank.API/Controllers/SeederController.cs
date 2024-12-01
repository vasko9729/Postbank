using Microsoft.AspNetCore.Mvc;
using Postbank.Application.Seeder;

namespace Postbank.API.Controllers;


[Route("api/[controller]")]
[ApiController]
public class SeederController : ControllerBase
{
	private readonly ISeeder _seeder;
    public SeederController(ISeeder seeder)
    {
        _seeder = seeder;
    }

    [HttpPost("database")]
    public async Task<IActionResult> Seed()
    {
        await _seeder.SeedAsync();

        return Ok();
    }
}
