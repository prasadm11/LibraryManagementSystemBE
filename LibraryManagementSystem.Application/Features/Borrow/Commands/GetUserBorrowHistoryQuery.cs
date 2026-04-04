using LibraryManagementSystem.Application.Features.Borrow.DTOs;
using LibraryManagementSystem.Core.Entities;
using MediatR;

namespace LibraryManagementSystem.Application.Features.Borrow.Commands;

public record GetUserBorrowHistoryQuery (GetUserBorrowHistoryRequestDto GetUserBorrowHistoryRequestDto) : IRequest<List<GetUserBorrowHistoryResponseDto>>;