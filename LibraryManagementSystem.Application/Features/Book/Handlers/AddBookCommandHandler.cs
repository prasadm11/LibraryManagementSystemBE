using AutoMapper;
using LibraryManagementSystem.Application.Features.Book.Commands;
using LibraryManagementSystem.Application.Features.Book.DTOs;
using LibraryManagementSystem.Core.Interfaces.Repositories;
using MediatR;

namespace LibraryManagementSystem.Application.Features.Book.Handlers;

public class AddBookCommandHandler : IRequestHandler<AddBookCommand,string>
{
    private readonly IBookRepository _bookRepository;
    private readonly IMapper _mapper;

    public AddBookCommandHandler(IBookRepository bookRepository, IMapper mapper)
    {
        _bookRepository = bookRepository;
        _mapper = mapper;
    }

    public async Task<string> Handle(AddBookCommand command, CancellationToken cancellationToken)
    {
        var book = _mapper.Map<Core.Entities.Book>(command.addBookDto);
        book.CreatedAt = DateTime.UtcNow;
        await _bookRepository.AddBookAsync(book);
        return  "Book Added Sucesssfully";
    }
    
}