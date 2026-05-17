using LibraryManagementSystem.Application.Features.Users.Commands;
using LibraryManagementSystem.Application.Features.Users.DTOS;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementSystem.API.Controllers;

[ApiController]
[Authorize(Roles = "Admin,User")]
[Route("api/[controller]/[action]")]
public class UserController : ControllerBase
{
    private readonly IMediator _mediator;

    public UserController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpGet]
    public async Task<IActionResult> GetAllUsers()
    {
        var result = await _mediator.Send(new GetAllUsersQuery());
        return Ok(result);
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateUser([FromBody] CreateUserDto createUserDto)
    {
        var result = await _mediator.Send(new CreateUserCommand(createUserDto));
        return StatusCode(result.StatusCode,result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetUserById(int id)
    {
        var result = await _mediator.Send(new GetUserByIdQuery(id));
        return Ok(result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUser(int id)
    {
        var result = await _mediator.Send(new DeleteUserCommand(id));
        return StatusCode(result.StatusCode,result);
    }
    

    [HttpPut]
    public async Task<IActionResult> UpdateUser([FromBody] UpdateUserDto updateUserDto)
    {
        var result = await _mediator.Send(new UpdateUserCommand(updateUserDto));
        return StatusCode(result.StatusCode,result);
    }
}