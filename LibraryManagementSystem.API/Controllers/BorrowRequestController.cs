using LibraryManagementSystem.Application.Features.Borrow.UserBorrowRequests.Commands;
using LibraryManagementSystem.Application.Features.Borrow.UserBorrowRequests.DTOs;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementSystem.API.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class BorrowRequestController : ControllerBase
{
    private readonly IMediator _mediator;

    public BorrowRequestController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> CreateBorrowRequest(CreateBorrowRequestDto createBorrowRequestDto)
    {
        var result = await _mediator.Send(new CreateBorrowRequestCommand(createBorrowRequestDto));
        return Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllPendingBorrowRequests()
    {
        var result =await _mediator.Send(new GetAllPendingBorrowRequestsQuery());
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> ApproveBorrrowRequest(int borrowRequestId)
    {
        var result = await _mediator.Send(new ApproveBorrowRequestCommand(borrowRequestId));
        return Ok(result);
    }
    
    [HttpPost]
    public async Task<IActionResult> RejectBorrowRequest([FromQuery] int borrowRequestId)
    {
        var result = await _mediator.Send(new RejectBorrowRequestCommand(borrowRequestId));
        return Ok(result);
    }
}