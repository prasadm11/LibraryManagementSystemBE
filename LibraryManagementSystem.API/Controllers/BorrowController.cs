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

    [HttpPost]
    public async Task<IActionResult> ReturnBook([FromBody] ReturnBookRequestDto returnBookRequestDto)
    {
        var result = await _mediator.Send(new ReturnBookCommand(returnBookRequestDto));
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> GetBooksByStatus(GetBookBorrowStatusRequestDto getBookBorrowStatusRequestDto)
    {
        var result = await _mediator.Send(new GetBookbyStatusQuery(getBookBorrowStatusRequestDto));
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> GetUserBorrowHistory(GetUserBorrowHistoryRequestDto getUserBorrowHistoryRequestDto)
    {
        var result = await _mediator.Send(new GetUserBorrowHistoryQuery (getUserBorrowHistoryRequestDto));
        return Ok(result);
    }
    
    [HttpPost]
    public async Task<IActionResult> GetOverdueBooksResponseDto()
    {
        var result = await _mediator.Send(new GetOverdueBooksQuery());
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> RenewBook(RenewBookRequestDto renewBookRequestDto)
    {
        var result = await _mediator.Send(new RenewBookCommand(renewBookRequestDto));
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> SearchBooks(SearchBooksRequestDto searchBooksRequestDto)
    {
        var result = await _mediator.Send(new SearchBooksQuery(searchBooksRequestDto));
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> GetBorrowSummary()
    {
        var result = await _mediator.Send(new GetBorrowSummaryQuery());
        return Ok(result);
    }
}