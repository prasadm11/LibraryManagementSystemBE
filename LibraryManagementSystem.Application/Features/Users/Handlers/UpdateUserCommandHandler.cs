using AutoMapper;
using LibraryManagementSystem.Application.Common.Models;
using LibraryManagementSystem.Application.Features.Users.Commands;
using LibraryManagementSystem.Application.Features.Users.DTOS;
using LibraryManagementSystem.Core.Interfaces.Repositories;
using MediatR;

namespace LibraryManagementSystem.Application.Features.Users.Handlers;

public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, ApiResponseModel<UpdateUserResponseDto>>
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public UpdateUserCommandHandler(IUserRepository userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public async Task<ApiResponseModel<UpdateUserResponseDto>> Handle(UpdateUserCommand command, CancellationToken cancellationToken)
    {
        var request = command.UpdateUserDto;

        // Fetch user
        var user = await _userRepository.GetUserByIdAsync(request.Id);

        if (user == null)
            throw new KeyNotFoundException($"User not found with Id {request.Id}");

        //  Update allowed fields
        user.FirstName = request.FirstName;
        user.LastName = request.LastName;
        user.City = request.City;
        user.PhoneNumber = request.PhoneNumber;

        // Optional updates
        if (!string.IsNullOrWhiteSpace(request.Email))
            user.Email = request.Email;

        if (!string.IsNullOrWhiteSpace(request.Username))
            user.Username = request.Username;

        //  Save changes
        await _userRepository.UpdateUserAsync(user);
        
        var result = _mapper.Map<UpdateUserResponseDto>(user);

        var response = ApiResponseModel<UpdateUserResponseDto>.SuccessResponse(
            result,
            "User successfully updated",
            200);
        
        return response;
    }
}