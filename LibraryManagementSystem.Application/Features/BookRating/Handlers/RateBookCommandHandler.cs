using LibraryManagementSystem.Application.Features.BookRating.Commands;
using LibraryManagementSystem.Application.Features.BookRating.DTOs;
using LibraryManagementSystem.Core.Interfaces.Repositories;
using MediatR;

namespace LibraryManagementSystem.Application.Features.BookRating.Handlers;

public class RateBookCommandHandler : IRequestHandler<RateBookCommand, RateBookResponseDto>
{
    private readonly IBookRatingRepository _bookRatingRepository;
    private readonly IBorrowRepository _borrowRepository;

    public RateBookCommandHandler(IBookRatingRepository bookRatingRepository, IBorrowRepository borrowRepository)
    {
        _bookRatingRepository = bookRatingRepository;
        _borrowRepository = borrowRepository;
    }

    public async Task<RateBookResponseDto> Handle(RateBookCommand command, CancellationToken cancellationToken)
    {
        var request = command.rateBookDto;

        var hasReturnedBook = await _borrowRepository.HasUserReturnedBook(request.UserId, request.BookId);

        if (!hasReturnedBook)
        {
            return new RateBookResponseDto
            {
                Message = "You can rate only returned books"
            };
        }

        var alreadyRated = await _bookRatingRepository.HasUserRatedBook(request.UserId, request.BookId);
        
        if (alreadyRated)
        {
            return new RateBookResponseDto
            {
                Message = "You already rated this book"
            };
        }
        
        if (request.Rating < 1 || request.Rating > 5)
        {
            return new RateBookResponseDto
            {
                Message = "Rating must be between 1 and 5"
            };

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

        return new RateBookResponseDto
        {
            Message = "Book rated successfully"
        };
        // return "Book rated successfully";

    }
}