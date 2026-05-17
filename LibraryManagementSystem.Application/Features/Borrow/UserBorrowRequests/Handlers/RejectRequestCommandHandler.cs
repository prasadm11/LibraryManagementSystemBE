using LibraryManagementSystem.Application.Features.Borrow.UserBorrowRequests.Commands;
using LibraryManagementSystem.Application.Features.Borrow.UserBorrowRequests.DTOs;
using LibraryManagementSystem.Core.Enums;
using LibraryManagementSystem.Core.Interfaces.Repositories;
using MediatR;
using LibraryManagementSystem.Application.Common.Models;
using LibraryManagementSystem.Core.Entities;

namespace LibraryManagementSystem.Application.Features.Borrow.UserBorrowRequests.Handlers;

public class RejectRequestCommandHandler  : IRequestHandler<RejectRequestCommand, ApiResponseModel<ApproveRequestResponseDto>>
{
    private readonly IBorrowRequestRepository _borrowRequestRepository;
    private readonly INotificationRepository _notificationRepository;

    public RejectRequestCommandHandler(
        IBorrowRequestRepository borrowRequestRepository,
        INotificationRepository notificationRepository)
    {
        _borrowRequestRepository = borrowRequestRepository;
        _notificationRepository = notificationRepository;
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
        await _notificationRepository.AddAsync(new Core.Entities.Notification
        {
            UserId = request.UserId,
            Title = "Request Rejected",
            Message = $"Your {request.Type} request has been rejected",
            Type = $"{request.Type}Rejected",
            IsRead = false,
            CreatedAt = DateTime.UtcNow
        });

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