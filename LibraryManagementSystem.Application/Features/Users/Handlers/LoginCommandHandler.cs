using AutoMapper;
using LibraryManagementSystem.Application.Features.Users.Commands;
using LibraryManagementSystem.Application.Features.Users.DTOS;
using LibraryManagementSystem.Core.Interfaces.Repositories;
using LibraryManagementSystem.Core.Interfaces.Services;
// using LibraryManagementSystem.Infrastructure.Services;
using MediatR;

namespace LibraryManagementSystem.Application.Features.Users.Handlers;

public class LoginCommandHandler : IRequestHandler<LoginCommand,string>
{
    private readonly IUserRepository _userRepository;
    private readonly IJwtService  _jwtService;

    public LoginCommandHandler(IUserRepository userRepository,IJwtService jwtService)
    {
        _userRepository = userRepository;
        _jwtService = jwtService;
    }

    public async Task<string> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetUserByEmailAsync(request.loginRequestDto.Email);

        if (user == null)
        {
            return ("Invalid login attempt.");
        }

        if (user.Password != request.loginRequestDto.Password)
        {
            return ("Invalid email or password");
        }

        return _jwtService.GenerateJwtToken(user);

    }
    
}