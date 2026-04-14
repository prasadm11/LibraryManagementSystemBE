using MediatR;

namespace LibraryManagementSystem.Application.Features.Borrow.UserBorrowRequests.Commands;

public record RejectBorrowRequestCommand(int BorrowRequestId) : IRequest<string>;