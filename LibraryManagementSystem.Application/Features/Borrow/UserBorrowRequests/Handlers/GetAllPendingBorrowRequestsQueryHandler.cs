using AutoMapper;
using LibraryManagementSystem.Application.Features.Borrow.UserBorrowRequests.Commands;
using LibraryManagementSystem.Application.Features.Borrow.UserBorrowRequests.DTOs;
using LibraryManagementSystem.Core.Interfaces.Repositories;
using MediatR;
using LibraryManagementSystem.Application.Common.Models;

namespace LibraryManagementSystem.Application.Features.Borrow.UserBorrowRequests.Handlers;

public class GetAllPendingBorrowRequestsQueryHandler : IRequestHandler<GetAllPendingBorrowRequestsQuery, ApiResponseModel<List<GetAllPendingBorrowRequestsResponseDto>>>
{
    private readonly IBorrowRequestRepository _borrowRequestRepository;
    private readonly IMapper _mapper;

    public GetAllPendingBorrowRequestsQueryHandler(IBorrowRequestRepository borrowRequestRepository, IMapper mapper)
    {
        _borrowRequestRepository = borrowRequestRepository;
        _mapper = mapper;
    }

    public async Task<ApiResponseModel<List<GetAllPendingBorrowRequestsResponseDto>>> Handle(GetAllPendingBorrowRequestsQuery request,
        CancellationToken cancellationToken)
    {

        var result = await _borrowRequestRepository.GetPendingRequestsAsync(request.pageNumber, request.pageSize);
        
        var resultDto = _mapper.Map<List<GetAllPendingBorrowRequestsResponseDto>>(result);

        var response = ApiResponseModel<List<GetAllPendingBorrowRequestsResponseDto>>
            .SuccessResponse(
                resultDto,
                "Pending borrow requests fetched successfully",
                200
            );

        return response;

    }
}