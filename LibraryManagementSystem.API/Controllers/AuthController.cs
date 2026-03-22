using LibraryManagementSystem.Application.Features.Users.Commands;
using LibraryManagementSystem.Application.Features.Users.DTOS;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementSystem.API.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class AuthController : ControllerBase
{
    private readonly IMediator _mediator;
    public AuthController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpPost]
    public async Task<IActionResult> LoginUser(LoginRequestDto loginRequestDto)
    {
        var response = await _mediator.Send(new LoginCommand(loginRequestDto));
        if (response == "Invalid login attempt.")
        {
            return BadRequest(new { message = "Invalid login attempt." });
        }
        if (response == "Invalid email or password")
        {
            return BadRequest(new { message = "Invalid email or password" });
        }
        return Ok(new{message = "Login successful!", token = response});
    }
}