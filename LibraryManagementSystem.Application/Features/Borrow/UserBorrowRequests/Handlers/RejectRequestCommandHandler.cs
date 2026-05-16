using LibraryManagementSystem.Application.Features.Borrow.UserBorrowRequests.Commands;
using LibraryManagementSystem.Application.Features.Borrow.UserBorrowRequests.DTOs;
using LibraryManagementSystem.Core.Enums;
using LibraryManagementSystem.Core.Interfaces.Repositories;
using MediatR;
using LibraryManagementSystem.Application.Common.Models;

namespace LibraryManagementSystem.Application.Features.Borrow.UserBorrowRequests.Handlers;

public class RejectRequestCommandHandler  : IRequestHandler<RejectRequestCommand, ApiResponseModel<ApproveRequestResponseDto>>
{
    private readonly IBorrowRequestRepository _borrowRequestRepository;

    public RejectRequestCommandHandler(IBorrowRequestRepository borrowRequestRepository)
    {
        _borrowRequestRepository = borrowRequestRepository;
    }

    public async Task<ApiResponseModel<ApproveRequestResponseDto>> Handle(RejectRequestCommand command, CancellationToken cancellationToken)
    {
        var request = await _borrowRequestRepository.GetByIdAsync(command.id);
        
        if (request == null)
            throw new KeyNotFoundException("Request not found");
        
        if (request.Status != BorrowRequestStatus.Pending)
            throw new Exception("Request already processed");
        
        //Handler
        
        request.Status = BorrowRequestStatus.Rejected;
        request.ApprovedAt = DateTime.UtcNow;
        
        await _borrowRequestRepository.UpdateAsync(request);

        var result = new ApproveRequestResponseDto
        {
            Message = "Request rejected successfully"
        };

        var response = ApiResponseModel<ApproveRequestResponseDto>
            .SuccessResponse(
                result,
                "Request rejected successfully",
                200
            );

        return response;
    }
}