using AutoMapper;
using LibraryManagementSystem.Application.Common.Models;
using LibraryManagementSystem.Application.Features.Borrow.Commands;
using LibraryManagementSystem.Application.Features.Borrow.DTOs;
using LibraryManagementSystem.Core.Enums;
using LibraryManagementSystem.Core.Interfaces.Repositories;
using MediatR;

namespace LibraryManagementSystem.Application.Features.Borrow.Handlers;

public class GetBookbyStatusQueryHandler : IRequestHandler<GetBookbyStatusQuery , ApiResponseModel<List<GetBookBorrowStatusResponseDto>>>
{
    private readonly IMapper _mapper;
    private readonly IBorrowRepository _borrowRepository;

    public GetBookbyStatusQueryHandler(IMapper mapper, IBorrowRepository borrowRepository)
    {
        _mapper = mapper;
        _borrowRepository = borrowRepository;
    }

    public async Task<ApiResponseModel<List<GetBookBorrowStatusResponseDto>>> Handle(GetBookbyStatusQuery query, CancellationToken cancellationToken)
    {
        var request = query.GetBookBorrowStatusRequestDto;

        if (string.IsNullOrWhiteSpace(request.status))
        {
            throw new ArgumentException("Status is required");
        }

        if (!Enum.TryParse(request.status, true, out BorrowStatus status))
        {
            throw new KeyNotFoundException("Invalid borrow status");
        }
        
        
        var records = await _borrowRepository.GetByStatusAsync(status,query.pageNumber,query.pageSize);

        var result = _mapper.Map<List<GetBookBorrowStatusResponseDto>>(records);
        
        var response = ApiResponseModel<List<GetBookBorrowStatusResponseDto>>.SuccessResponse(
            result, 
            "Borrow records fetched successfully",
            200);

        return response;

    }
}