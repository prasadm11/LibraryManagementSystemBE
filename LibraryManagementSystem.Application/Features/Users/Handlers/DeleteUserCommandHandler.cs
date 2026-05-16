using AutoMapper;
using LibraryManagementSystem.Application.Common.Models;
using LibraryManagementSystem.Application.Features.Users.Commands;
using LibraryManagementSystem.Application.Features.Users.DTOS;
using LibraryManagementSystem.Core.Interfaces.Repositories;
using MediatR;

namespace LibraryManagementSystem.Application.Features.Users.Handlers;

public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, ApiResponseModel<DeleteUserResponseDto>>
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public DeleteUserCommandHandler(IUserRepository userRepository,IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public async Task<ApiResponseModel<DeleteUserResponseDto>> Handle(DeleteUserCommand command, CancellationToken cancellationToken)
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
        
        var result = _mapper.Map<DeleteUserResponseDto>(user);

        // return "User deactivated successfully";
        var response = ApiResponseModel<DeleteUserResponseDto>.SuccessResponse(
            result,
            "User Deactivated Successfully",
            200
            );
        
        return response;
    }
}