using AutoMapper;
using LibraryManagementSystem.Application.Features.Book.Commands;
using LibraryManagementSystem.Application.Features.Book.DTOs;
using LibraryManagementSystem.Core.Interfaces.Repositories;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace LibraryManagementSystem.API.Controllers;


[ApiController]
[Route("api/[controller]/[action]")]
public class BooksController : ControllerBase
{
    private readonly IMediator  _mediator;

    public BooksController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllBooks()
    {
        var response = await _mediator.Send(new GetAllBooksQuery());
        return Ok(response);

    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetBookById(int id)
    {
        var response = await _mediator.Send(new GetBookByIdQuery(id));
        return Ok(response);
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> AddBook([FromBody] AddBookDto addBookDto)
    {
        var book =await _mediator.Send(new AddBookCommand(addBookDto));
        return Ok(book);
    }
    
    [HttpPut]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> UpdateBook([FromBody] UpdateBookDto updateBookDto)
    {
        var result = await _mediator.Send(new UpdateBookCommand(updateBookDto));
        return Ok(result);
    }
    
    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> DeleteBook(int id)
    {
        var result = await _mediator.Send(new DeleteBookCommand(id));
        return Ok(result);
    }
    
}