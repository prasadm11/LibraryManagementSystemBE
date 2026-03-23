using AutoMapper;
using LibraryManagementSystem.Application.Features.Book.Commands;
using LibraryManagementSystem.Application.Features.Book.DTOs;
using LibraryManagementSystem.Core.Interfaces.Repositories;
using MediatR;

namespace LibraryManagementSystem.Application.Features.Book.Handlers;

public class GetAllBooksQueryHandler : IRequestHandler<GetAllBooksQuery, List<BookResponseDto>>
{
    private readonly IBookRepository _bookRepository;
    private readonly IMapper _mapper;
    
    public GetAllBooksQueryHandler(IBookRepository bookRepository, IMapper mapper)
    {
        _bookRepository = bookRepository;
        _mapper = mapper;
    }

    public async Task<List<BookResponseDto>> Handle(GetAllBooksQuery request, CancellationToken cancellationToken)
    {
        var response = await _bookRepository.GetAllBooksAsync();
        return _mapper.Map<List<BookResponseDto>>(response);
    }
}