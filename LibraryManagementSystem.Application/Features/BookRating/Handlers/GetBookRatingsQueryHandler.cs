using LibraryManagementSystem.Application.Features.BookRating.Commands;
using LibraryManagementSystem.Application.Features.BookRating.DTOs;
using LibraryManagementSystem.Core.Interfaces.Repositories;
using MediatR;
using LibraryManagementSystem.Application.Common.Models;

namespace LibraryManagementSystem.Application.Features.BookRating.Handlers;

public class GetBookRatingsQueryHandler: IRequestHandler<GetBookRatingsQuery, ApiResponseModel<List<GetBookRatingsResponseDto>>>
{
    private readonly IBookRatingRepository _bookRatingRepository;
    private readonly IUserRepository _userRepository;

    public GetBookRatingsQueryHandler(IBookRatingRepository bookRatingRepository, IUserRepository userRepository)
    {
        _bookRatingRepository = bookRatingRepository;
        _userRepository = userRepository;
    }
    
    public async Task<ApiResponseModel<List<GetBookRatingsResponseDto>>> Handle(GetBookRatingsQuery request, CancellationToken cancellationToken)
    {
        var ratings = await _bookRatingRepository.GetBookRatings(request.BookId,request.pageNumber, request.pageSize);
        var result = new List<GetBookRatingsResponseDto>();

        foreach (var rating in ratings)
        {
            var user = await _userRepository.GetUserByIdAsync(rating.UserId);
            result.Add(new GetBookRatingsResponseDto
            {
                Rating = rating.Rating,
                Review = rating.Review,
                UserId =  user.Id,
                UserProfileImageUrl = user.ProfileImageUrl,
                Username = user.Username,
                CreatedAt = rating.CreatedAt
            });
        }
        var response = ApiResponseModel<List<GetBookRatingsResponseDto>>
            .SuccessResponse(
                result,
                "Book ratings fetched successfully",
                200
            );

        return response;
    }
}