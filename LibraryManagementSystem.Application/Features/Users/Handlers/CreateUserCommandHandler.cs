using AutoMapper;
using LibraryManagementSystem.Application.Common.Models;
using LibraryManagementSystem.Application.Features.Users.Commands;
using LibraryManagementSystem.Application.Features.Users.DTOS;
using LibraryManagementSystem.Core.Entities;
using LibraryManagementSystem.Core.Interfaces.Repositories;
using MediatR;

namespace LibraryManagementSystem.Application.Features.Users.Handlers;

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand , ApiResponseModel<CreateUserResponseDto>>
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;
    
    public CreateUserCommandHandler(IUserRepository userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }
    
    public async Task<ApiResponseModel<CreateUserResponseDto>> Handle(CreateUserCommand command, CancellationToken cancellationToken)
    {
        var user = _mapper.Map<User>(command.CreateUserDto);
        
        user.CreatedAt = DateTime.UtcNow;
        user.IsActive = true;
        user.Role = "User";
        await _userRepository.AddUserAsync(user);

        var result = _mapper.Map<CreateUserResponseDto>(user);
        
        var response = ApiResponseModel<CreateUserResponseDto>.
            SuccessResponse(
                result,
                "User Created Successfully",
                201);
        
        return response;
    }
}