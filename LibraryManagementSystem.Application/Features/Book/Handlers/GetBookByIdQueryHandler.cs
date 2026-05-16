using AutoMapper;
using LibraryManagementSystem.Application.Features.Book.Commands;
using LibraryManagementSystem.Application.Features.Book.DTOs;
using LibraryManagementSystem.Core.Interfaces.Repositories;
using MediatR;
using LibraryManagementSystem.Application.Common.Models;

namespace LibraryManagementSystem.Application.Features.Book.Handlers;

public class GetBookByIdQueryHandler : IRequestHandler<GetBookByIdQuery , ApiResponseModel<BookResponseDto>>
{
    private readonly IBookRepository _bookRepository;
    private readonly IMapper _mapper;

    public GetBookByIdQueryHandler(IBookRepository bookRepository, IMapper mapper)
    {
        _bookRepository = bookRepository;
        _mapper = mapper;
    }

    public async Task<ApiResponseModel<BookResponseDto>> Handle(GetBookByIdQuery query, CancellationToken cancellationToken)
    {
        var book = await _bookRepository.GetBookByIdAsync(query.id);

        if (book == null)
        {
            throw new KeyNotFoundException($"Book with ID {query.id} not found");
        }
        var result = _mapper.Map<BookResponseDto>(book);

        var response = ApiResponseModel<BookResponseDto>
            .SuccessResponse(
                result,
                "Book fetched successfully",
                200
            );

        return response;
    }
}