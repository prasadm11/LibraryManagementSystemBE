using AutoMapper;
using LibraryManagementSystem.Application.Features.Book.Commands;
using LibraryManagementSystem.Application.Features.Book.DTOs;
using LibraryManagementSystem.Core.Interfaces.Repositories;
using MediatR;

namespace LibraryManagementSystem.Application.Features.Book.Handlers;

public class GetBookByIdQueryHandler : IRequestHandler<GetBookByIdQuery , BookResponseDto>
{
    private readonly IBookRepository _bookRepository;
    private readonly IMapper _mapper;

    public GetBookByIdQueryHandler(IBookRepository bookRepository, IMapper mapper)
    {
        _bookRepository = bookRepository;
        _mapper = mapper;
    }

    public async Task<BookResponseDto> Handle(GetBookByIdQuery query, CancellationToken cancellationToken)
    {
        var book = await _bookRepository.GetBookByIdAsync(query.id);

        if (book == null)
        {
            throw new KeyNotFoundException($"Book with ID {query.id} not found");
        }
        return _mapper.Map<BookResponseDto>(book);
    }
}