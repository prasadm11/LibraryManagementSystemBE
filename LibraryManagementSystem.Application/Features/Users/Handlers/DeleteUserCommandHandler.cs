using LibraryManagementSystem.Application.Features.Users.Commands;
using LibraryManagementSystem.Core.Interfaces.Repositories;
using MediatR;

namespace LibraryManagementSystem.Application.Features.Users.Handlers;

public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, string>
{
    private readonly IUserRepository _userRepository;

    public DeleteUserCommandHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<string> Handle(DeleteUserCommand command, CancellationToken cancellationToken)
    {
        // 1. Fetch user
        var user = await _userRepository.GetUserByIdAsync(command.UserId);

        if (user == null)
            throw new KeyNotFoundException($"User not found with Id {command.UserId}");

        // 2. Already inactive?
        if (!user.IsActive)
            throw new InvalidOperationException("User is already deactivated");

        // 3. Soft delete
        user.IsActive = false;

        // 4. Persist
        await _userRepository.UpdateUserAsync(user);

        return "User deactivated successfully";
    }
}