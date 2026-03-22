using LibraryManagementSystem.Application.Features.Users.DTOS;
using MediatR;

namespace LibraryManagementSystem.Application.Features.Users.Commands;

public record GetUserByIdQuery(int Id) : IRequest<GetUserByIdDto>;