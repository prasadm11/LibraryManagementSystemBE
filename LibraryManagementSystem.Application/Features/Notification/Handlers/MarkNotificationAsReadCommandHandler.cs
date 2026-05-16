using AutoMapper;
using LibraryManagementSystem.Application.Common.Models;
using LibraryManagementSystem.Application.Features.Notification.Commands;
using LibraryManagementSystem.Application.Features.Notification.DTOs;
using LibraryManagementSystem.Core.Interfaces.Repositories;
using MediatR;

namespace LibraryManagementSystem.Application.Features.Notification.Handlers;

public class MarkNotificationAsReadCommandHandler : IRequestHandler<MarkNotificationAsReadCommand, ApiResponseModel<string>>
{
    private readonly INotificationRepository _notificationRepository;
    private readonly IMapper _mapper;

    public MarkNotificationAsReadCommandHandler(INotificationRepository notificationRepository,IMapper mapper)
    {
        _notificationRepository = notificationRepository;
        _mapper = mapper;
    }

    public async Task<ApiResponseModel<string>> Handle(MarkNotificationAsReadCommand request, CancellationToken cancellationToken)
    {
        var notification = await _notificationRepository.GetByUserIdAsync(request.UserId);
        if (notification == null)
        {
            return ApiResponseModel<string>.FailureResponse("Notification not found",404);
        }
        await _notificationRepository.MarkAsReadAsync(request.UserId);

        var response = ApiResponseModel<string>.SuccessResponse(
            "Sucess",
            "All notifications marked as read",
            200);
        
        return response;
    }
}