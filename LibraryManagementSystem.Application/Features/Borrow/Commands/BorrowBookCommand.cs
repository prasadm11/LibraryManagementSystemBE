using LibraryManagementSystem.Application.Features.Borrow.DTOs;
using MediatR;

namespace LibraryManagementSystem.Application.Features.Borrow.Commands;

public record BorrowBookCommand(BorrowBookRequestDto BorrowBookRequestDto) : IRequest<BorrowBookResponseDto>;