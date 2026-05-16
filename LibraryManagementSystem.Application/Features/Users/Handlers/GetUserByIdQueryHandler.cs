using AutoMapper;
using LibraryManagementSystem.Application.Common.Models;
using LibraryManagementSystem.Application.Features.Users.Commands;
using LibraryManagementSystem.Application.Features.Users.DTOS;
using LibraryManagementSystem.Core.Interfaces.Repositories;
using MediatR;

namespace LibraryManagementSystem.Application.Features.Users.Handlers;

public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, ApiResponseModel<GetUserByIdDto>>
{
    private readonly IMapper _mapper;
    private readonly IUserRepository _userRepository;

    public GetUserByIdQueryHandler(IMapper mapper, IUserRepository userRepository)
    {
        _mapper = mapper;
        _userRepository = userRepository;
    }

    public async Task<ApiResponseModel<GetUserByIdDto>> Handle(GetUserByIdQuery query, CancellationToken cancellationToken)
    {
        var user =await _userRepository.GetUserByIdAsync(query.Id);
        var result = _mapper.Map<GetUserByIdDto>(user);
        var response = ApiResponseModel<GetUserByIdDto>.SuccessResponse(
            result,
            "Sucessfully retrieved User",
            200
            );
        return response;
    }
    
}