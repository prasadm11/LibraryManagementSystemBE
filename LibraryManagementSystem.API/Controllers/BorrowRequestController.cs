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
    
    //BORROW FLOW

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
    public async Task<IActionResult> ApproveRequest(int id)
    {
        var result = await _mediator.Send(new ApproveRequestCommand(id));
        return Ok(result);
    }
    
    [HttpPost]
    public async Task<IActionResult> RejectRequest([FromQuery] int id)
    {
        var result = await _mediator.Send(new RejectRequestCommand(id));
        return Ok(result);
    }
    
    //RETURN FLOW
    [HttpPost]
    public async Task<IActionResult> CreateReturnBookRequest([FromBody] CreateReturnBookRequestDto createReturnBookRequestDto)
    {
        var result = await _mediator.Send(new CreateReturnBookRequestCommand(createReturnBookRequestDto));
        return Ok(result);
    }
    
    //RENEW 
    [HttpPost]
    public async Task<IActionResult> CreateRenewBookRequest([FromBody] CreateRenewBookRequestDto dto)
    {
        var result = await _mediator.Send(new CreateRenewBookRequestCommand(dto));
        return Ok(result);
    }
}