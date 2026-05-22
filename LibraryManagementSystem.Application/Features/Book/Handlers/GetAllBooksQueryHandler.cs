using AutoMapper;
using LibraryManagementSystem.Application.Features.Book.Commands;
using LibraryManagementSystem.Application.Features.Book.DTOs;
using LibraryManagementSystem.Core.Interfaces.Repositories;
using MediatR;
using LibraryManagementSystem.Application.Common.Models;

namespace LibraryManagementSystem.Application.Features.Book.Handlers;

public class GetAllBooksQueryHandler : IRequestHandler<GetAllBooksQuery, ApiResponseModel<List<BookResponseDto>>>
{
    private readonly IBookRepository _bookRepository;
    private readonly IBookRatingRepository _bookRatingRepository;
    private readonly IMapper _mapper;
    
    public GetAllBooksQueryHandler(IBookRepository bookRepository,IBookRatingRepository bookRatingRepository, IMapper mapper)
    {
        _bookRepository = bookRepository;
        _bookRatingRepository = bookRatingRepository;
        _mapper = mapper;
    }

    public async Task<ApiResponseModel<List<BookResponseDto>>> Handle(GetAllBooksQuery request, CancellationToken cancellationToken)
    {
        var response = await _bookRepository.GetAllBooksAsync(request.pageNumber, request.pageSize);
        var result = _mapper.Map<List<BookResponseDto>>(response);
        foreach (var bookDto in result)
        {
            var ratings = await _bookRatingRepository.GetBookRatingStats(bookDto.Id);
            bookDto.TotalRatings = ratings.TotalRatings;
            bookDto.AverageRating = ratings.AverageRating;
        }
        var responseDto = ApiResponseModel<List<BookResponseDto>>
            .SuccessResponse(
                result,
                "Books fetched successfully",
                200
            );

        return responseDto;
    }
}