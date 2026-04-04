using MediatR;

namespace LibraryManagementSystem.Application.Features.Users.Commands;

public record DeleteUserCommand(int UserId) : IRequest<string>;
