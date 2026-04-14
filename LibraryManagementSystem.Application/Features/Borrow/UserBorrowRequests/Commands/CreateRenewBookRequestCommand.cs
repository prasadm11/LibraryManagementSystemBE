using LibraryManagementSystem.Application.Features.Borrow.UserBorrowRequests.DTOs;
using MediatR;

namespace LibraryManagementSystem.Application.Features.Borrow.UserBorrowRequests.Commands;

public record CreateRenewBookRequestCommand(CreateRenewBookRequestDto Dto) : IRequest<string>;