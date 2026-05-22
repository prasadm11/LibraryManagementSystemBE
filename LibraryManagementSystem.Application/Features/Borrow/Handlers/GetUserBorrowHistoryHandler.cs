using AutoMapper;
using LibraryManagementSystem.Application.Common.Models;
using LibraryManagementSystem.Application.Features.Borrow.Commands;
using LibraryManagementSystem.Core.Interfaces.Repositories;
using MediatR;

namespace LibraryManagementSystem.Application.Features.Borrow.DTOs;

public class GetUserBorrowHistoryHandler : IRequestHandler<GetUserBorrowHistoryQuery, ApiResponseModel<List<GetUserBorrowHistoryResponseDto>>>
{
    private readonly IMapper _mapper;
    private readonly IBorrowRepository _borrowRepository;
    private readonly IUserRepository _userRepository;

    public GetUserBorrowHistoryHandler(IMapper mapper, IBorrowRepository borrowRepository, IUserRepository userRepository)
    {
        _mapper = mapper;
        _borrowRepository = borrowRepository;
        _userRepository = userRepository;
    }

    public async Task<ApiResponseModel<List<GetUserBorrowHistoryResponseDto>>> Handle(GetUserBorrowHistoryQuery command,
        CancellationToken cancellationToken)
    {
        var request = command.GetUserBorrowHistoryRequestDto;
        var user = await _userRepository.GetUserByIdAsync(request.UserId);
        
        if (user == null)
        {
            throw new KeyNotFoundException($"User with given id {request.UserId} does not exist");
        }
        
        var records = await _borrowRepository.GetByUserIdAsync(request.UserId,command.pageNumber, command.pageSize);
        
        var result = _mapper.Map<List<GetUserBorrowHistoryResponseDto>>(records);

        var response = ApiResponseModel<List<GetUserBorrowHistoryResponseDto>>.SuccessResponse(
            result,
            "User borrow history fetched successfully"
            ,200);
        return response;
        
    }
}