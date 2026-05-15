using LibraryManagementSystem.Application.Features.BookRating.Commands;
using LibraryManagementSystem.Application.Features.BookRating.DTOs;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementSystem.API.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class BookRatingController : ControllerBase
{
    // GET
    // public IActionResult Index()
    // {
    //     return View();
    // }
    private readonly IMediator _mediator;

    public BookRatingController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HttpPost]
    public async Task<IActionResult> RateBook(RateBookDto rateBookDto)
    {
        var result = await _mediator.Send(new RateBookCommand(rateBookDto));
        return Ok(result);
    }
    
    [HttpGet]
    public async Task<IActionResult> GetBookRatings(int bookId)
    {
        var result = await _mediator.Send(new GetBookRatingsQuery(bookId));
        return Ok(result);
    }
}