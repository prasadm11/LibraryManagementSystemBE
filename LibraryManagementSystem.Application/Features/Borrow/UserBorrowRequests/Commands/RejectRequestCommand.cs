using MediatR;

namespace LibraryManagementSystem.Application.Features.Borrow.UserBorrowRequests.Commands;

public record RejectRequestCommand(int id) : IRequest<string>;