using LibraryManagementSystem.Core.Entities;
using MediatR;

namespace LibraryManagementSystem.Application.Features.Borrow.UserBorrowRequests.Commands;

public record ApproveBorrowRequestCommand(int BorrowRequestId) : IRequest<string>;