using LibraryManagementSystem.Application.Common.Models;
using LibraryManagementSystem.Application.Features.Users.DTOS;
using MediatR;

namespace LibraryManagementSystem.Application.Features.Users.Commands;

public record UpdateUserCommand(UpdateUserDto UpdateUserDto) : IRequest<ApiResponseModel<UpdateUserResponseDto>>;