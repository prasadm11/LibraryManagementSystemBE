using LibraryManagementSystem.Application.Features.Book.DTOs;
using LibraryManagementSystem.Application.Features.Borrow.Commands;
using LibraryManagementSystem.Application.Features.Borrow.DTOs;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementSystem.API.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class BorrowController : ControllerBase
{
    private readonly IMediator  _mediator;

    public BorrowController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> BorrowBook([FromBody] BorrowBookRequestDto borrowBookRequestDto)
    {
        var result = await _mediator.Send(new BorrowBookCommand(borrowBookRequestDto));
        return Ok(result);
    }
}