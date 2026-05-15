using LibraryManagementSystem.Application.Features.BookRating.DTOs;
using MediatR;

namespace LibraryManagementSystem.Application.Features.BookRating.Commands;

public record RateBookCommand(RateBookDto rateBookDto) : IRequest<RateBookResponseDto>;