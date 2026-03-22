using AutoMapper;
using LibraryManagementSystem.Application.Features.Users.Commands;
using LibraryManagementSystem.Core.Entities;
using LibraryManagementSystem.Core.Interfaces.Repositories;
using MediatR;

namespace LibraryManagementSystem.Application.Features.Users.Handlers;

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand , object>
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;
    
    public CreateUserCommandHandler(IUserRepository userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }
    
    public async Task<object> Handle(CreateUserCommand command, CancellationToken cancellationToken)
    {
        var user = _mapper.Map<User>(command.CreateUserDto);

        user.CreatedAt = DateTime.UtcNow;
        user.IsActive = true;
        user.Role = "Member";

        await _userRepository.AddUserAsync(user);

        return new { Message = "User created successfully" };
    }
}