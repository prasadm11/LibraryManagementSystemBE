using LibraryManagementSystem.Application.Features.BookRating.Commands;
using LibraryManagementSystem.Application.Features.BookRating.DTOs;
using LibraryManagementSystem.Core.Interfaces.Repositories;
using MediatR;
using LibraryManagementSystem.Application.Common.Models;

namespace LibraryManagementSystem.Application.Features.BookRating.Handlers;

public class RateBookCommandHandler : IRequestHandler<RateBookCommand, ApiResponseModel<RateBookResponseDto>>
{
    private readonly IBookRatingRepository _bookRatingRepository;
    private readonly IBorrowRepository _borrowRepository;

    public RateBookCommandHandler(IBookRatingRepository bookRatingRepository, IBorrowRepository borrowRepository)
    {
        _bookRatingRepository = bookRatingRepository;
        _borrowRepository = borrowRepository;
    }

    public async Task<ApiResponseModel<RateBookResponseDto>> Handle(RateBookCommand command, CancellationToken cancellationToken)
    {
        var request = command.rateBookDto;

        var hasReturnedBook = await _borrowRepository.HasUserReturnedBook(request.UserId, request.BookId);

        if (!hasReturnedBook)
        {
            return ApiResponseModel<RateBookResponseDto>
                .FailureResponse(
                    "You can rate only returned books",
                    400
                );
        }

        var alreadyRated = await _bookRatingRepository.HasUserRatedBook(request.UserId, request.BookId);
        
        if (alreadyRated)
        {
            return ApiResponseModel<RateBookResponseDto>
                .FailureResponse(
                    "You already rated this book",
                    400
                );
        }
        
        if (request.Rating < 1 || request.Rating > 5)
        {
            return ApiResponseModel<RateBookResponseDto>
                .FailureResponse(
                    "Rating must be between 1 and 5",
                    400
                );
        }

        var rating = new Core.Entities.BookRating
        {
            UserId = request.UserId,
            BookId = request.BookId,
            Rating = request.Rating,
            Review = request.Review,
            CreatedAt = DateTime.UtcNow
        };
        
        await _bookRatingRepository.AddAsync(rating);

        var result = new RateBookResponseDto
        {
            Message = "Book rated successfully"
        };

        var response = ApiResponseModel<RateBookResponseDto>
            .SuccessResponse(
                result,
                "Book rated successfully",
                200
            );

        return response;
    }
}