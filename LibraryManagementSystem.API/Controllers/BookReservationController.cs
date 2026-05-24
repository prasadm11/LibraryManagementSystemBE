using LibraryManagementSystem.Application.Features.BookReservation.Commands;
using LibraryManagementSystem.Application.Features.BookReservation.DTOs;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementSystem.API.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class BookReservationController : ControllerBase
{
    private readonly IMediator _mediator;
    public BookReservationController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> CreateReservation(CreateReservationRequestDto createReservationRequestDto)
    {
        var result = await _mediator.Send(new CreateReservationCommand(createReservationRequestDto));
        return StatusCode(result.StatusCode, result);
    }

    [HttpPost]
    public async Task<IActionResult> CancelReservation(CancelReservationRequestDto cancelReservationRequestDto)
    {
        var result = await _mediator.Send(new CancelReservationCommand(cancelReservationRequestDto));
        return StatusCode(result.StatusCode, result);
    }

    [HttpGet]
    public async Task<IActionResult> GetBookReservations([FromQuery] int bookId, [FromQuery] int pageNumber, [FromQuery] int pageSize)
    {
        var dto = new GetBookReservationsRequestDto
        {
            BookId = bookId
        };

        var result = await _mediator.Send(new GetBookReservationsQuery(dto, pageNumber, pageSize));
        
        return Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetUserReservations([FromQuery] int userId, [FromQuery] int pageNumber, [FromQuery] int pageSize)
    {
        var dto = new GetUserReservationsRequestDto
        {
            UserId = userId
        };

        var result = await _mediator.Send(new GetUserReservationsQuery(dto, pageNumber, pageSize));

        return Ok(result);
    }
}