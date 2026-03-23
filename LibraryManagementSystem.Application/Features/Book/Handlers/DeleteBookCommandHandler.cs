using AutoMapper;
using LibraryManagementSystem.Application.Features.Book.Commands;
using LibraryManagementSystem.Core.Interfaces.Repositories;
using MediatR;

namespace LibraryManagementSystem.Application.Features.Book.Handlers;

public class DeleteBookCommandHandler : IRequestHandler<DeleteBookCommand,string>
{
    private readonly IBookRepository _bookRepository;
    private readonly IMapper _mapper;

    public DeleteBookCommandHandler(IBookRepository bookRepository, IMapper mapper)
    {
        _bookRepository = bookRepository;
        _mapper = mapper;
    }

    public async Task<string> Handle(DeleteBookCommand command, CancellationToken cancellationToken)
    {
        var book =await _bookRepository.GetBookByIdAsync(command.Id);
        if (book == null)
            throw new KeyNotFoundException($"Book with ID {command.Id} not found");

        await _bookRepository.DeleteBookAsync(command.Id);
        return "Book deleted successfully";
    }
}