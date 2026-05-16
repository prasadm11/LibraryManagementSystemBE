using AutoMapper;
using LibraryManagementSystem.Application.Features.Book.Commands;
using LibraryManagementSystem.Application.Features.Book.DTOs;
using LibraryManagementSystem.Core.Interfaces.Repositories;
using MediatR;
using LibraryManagementSystem.Application.Common.Models;

namespace LibraryManagementSystem.Application.Features.Book.Handlers;

public class AddBookCommandHandler : IRequestHandler<AddBookCommand, ApiResponseModel<AddBookDto>>
{
    private readonly IBookRepository _bookRepository;
    private readonly IMapper _mapper;

    public AddBookCommandHandler(IBookRepository bookRepository, IMapper mapper)
    {
        _bookRepository = bookRepository;
        _mapper = mapper;
    }

    public async Task<ApiResponseModel<AddBookDto>> Handle(AddBookCommand command, CancellationToken cancellationToken)
    {
        var book = _mapper.Map<Core.Entities.Book>(command.addBookDto);
        book.CreatedAt = DateTime.UtcNow;
        await _bookRepository.AddBookAsync(book);

        var result = _mapper.Map<AddBookDto>(book);

        var response = ApiResponseModel<AddBookDto>
            .SuccessResponse(
                result,
                "Book added successfully",
                201
            );

        return response;
    }
    
}