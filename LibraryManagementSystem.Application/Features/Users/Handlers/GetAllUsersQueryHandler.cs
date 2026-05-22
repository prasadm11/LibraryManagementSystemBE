using AutoMapper;
using LibraryManagementSystem.Application.Common.Models;
using LibraryManagementSystem.Application.Features.Users.Commands;
using LibraryManagementSystem.Application.Features.Users.DTOS;
using LibraryManagementSystem.Core.Interfaces.Repositories;
using MediatR;

namespace LibraryManagementSystem.Application.Features.Users.Handlers;

public class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, ApiResponseModel<List<GetAllUsersResponseDto>>>
{
    private readonly IMapper _mapper;
    private readonly IUserRepository _userRepository;

    public GetAllUsersQueryHandler(IMapper mapper, IUserRepository userRepository)
    {
        _mapper = mapper;
        _userRepository = userRepository;
    }

    public async Task<ApiResponseModel<List<GetAllUsersResponseDto>>> Handle(GetAllUsersQuery query, CancellationToken cancellationToken)
    {
        var users = await _userRepository.GetAllUsersAsync(query.pageNumber, query.pageSize);
        var result = _mapper.Map<List<GetAllUsersResponseDto>>(users);
        var response = ApiResponseModel<List<GetAllUsersResponseDto> >.SuccessResponse(
            result, 
            "Successfully retrieved all users",
            200);
        return response;

    }

}