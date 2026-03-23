using AutoMapper;
using LibraryManagementSystem.Application.Features.Book.Commands;
using LibraryManagementSystem.Core.Interfaces.Repositories;
using MediatR;

namespace LibraryManagementSystem.Application.Features.Book.Handlers;

public class UpdateBookCommandHandler : IRequestHandler<UpdateBookCommand,string>
{
    private readonly IBookRepository _bookRepository;
    private readonly IMapper _mapper;

    public UpdateBookCommandHandler(IBookRepository bookRepository, IMapper mapper)
    {
        _bookRepository = bookRepository;
        _mapper = mapper;
    }

    public async Task<string> Handle(UpdateBookCommand command, CancellationToken cancellationToken)
    {
        var book = await _bookRepository.GetBookByIdAsync(command.UudateBookDto.Id);
        if (book == null)
            throw new KeyNotFoundException($"Book with ID {command.UudateBookDto.Id} not found");
        
        _mapper.Map(command.UudateBookDto, book);
        await _bookRepository.UpdateBookAsync(book);
        return "Book Updated Sucessfully";
    }
}