using AutoMapper;
using LibraryManagementSystem.Application.Features.Book.DTOs;
using LibraryManagementSystem.Application.Features.Borrow.Commands;
using LibraryManagementSystem.Core.Interfaces.Repositories;
using MediatR;

namespace LibraryManagementSystem.Application.Features.Borrow.Handlers;

public class SearchBooksQueryHandler : IRequestHandler<SearchBooksQuery , List<BookResponseDto>>
{
    private readonly IBorrowRepository _borrowRepository;
    private readonly IMapper _mapper;

    public SearchBooksQueryHandler(IBorrowRepository borrowRepository, IMapper mapper)
    {
        _borrowRepository = borrowRepository;
        _mapper = mapper;
    }

    public async Task<List<BookResponseDto>> Handle(SearchBooksQuery request, CancellationToken cancellationToken)
    {
        var record = request.SearchBooksRequestDto;
        var result = await _borrowRepository.SearchBooksAsync(record.Keyword);

        if (string.IsNullOrWhiteSpace(record.Keyword))
            throw new ArgumentException("Keyword is required");
        
        var response = _mapper.Map<List<BookResponseDto>>(result);
        return response;
    }
}