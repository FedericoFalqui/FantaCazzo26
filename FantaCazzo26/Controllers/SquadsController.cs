using FantaCazzo26.Models;
using FantaCazzo26.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FantaCazzo26.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SquadsController : ControllerBase
{
    private readonly ISquadsService _service;

    public SquadsController(ISquadsService service)
    {
        _service = service;
    }

    [HttpGet("find-all")]
    public async Task<ActionResult<List<Squad>>> GetAll()
    {
        return Ok(await _service.GetAll());
    }

    [HttpPost("add-team")]
    public async Task<ActionResult<Squad>> AddTeam(
        string name,
        string president,
        int credits)
    {
        var squad = await _service.AddTeam(name, president, credits);

        return Ok(squad);
    }

    [HttpDelete("delete-team/{id}")]
    public async Task<IActionResult> Delete(long id)
    {
        await _service.DeleteTeam(id);

        return Ok();
    }

    [HttpPost("add-credits/{id}")]
    public async Task<IActionResult> AddCredits(long id, int credits)
    {
        await _service.AddCredits(id, credits);

        return Ok();
    }
}