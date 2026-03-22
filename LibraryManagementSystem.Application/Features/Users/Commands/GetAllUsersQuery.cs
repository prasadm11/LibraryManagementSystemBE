using MediatR;
using LibraryManagementSystem.Application.Features.Users.DTOS;

namespace LibraryManagementSystem.Application.Features.Users.Commands;

public record GetAllUsersQuery()  : IRequest<List<GetAllUsersResponseDto>>;
