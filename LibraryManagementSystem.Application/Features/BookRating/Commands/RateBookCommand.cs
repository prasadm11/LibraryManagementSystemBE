using LibraryManagementSystem.Application.Common.Models;
using LibraryManagementSystem.Application.Features.BookRating.DTOs;
using MediatR;

namespace LibraryManagementSystem.Application.Features.BookRating.Commands;

public record RateBookCommand(RateBookDto rateBookDto) : IRequest<ApiResponseModel<RateBookResponseDto>>;