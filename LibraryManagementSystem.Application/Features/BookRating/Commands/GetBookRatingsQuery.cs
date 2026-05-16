using LibraryManagementSystem.Application.Common.Models;
using LibraryManagementSystem.Application.Features.BookRating.DTOs;
using MediatR;

namespace LibraryManagementSystem.Application.Features.BookRating.Commands;

public record GetBookRatingsQuery(int BookId) : IRequest<ApiResponseModel<List<GetBookRatingsResponseDto>>>;