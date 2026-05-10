using LibraryManagementSystem.Application.Features.Notification.Commands;
using LibraryManagementSystem.Application.Features.Notification.DTOs;
using LibraryManagementSystem.Core.Interfaces.Repositories;
using MediatR;

namespace LibraryManagementSystem.Application.Features.Notification.Handlers;

public class GetMyNotificationsQueryHandler : IRequestHandler<GetMyNotificationsQuery, List<NotificationDto>>
{
    private readonly INotificationRepository _notificationRepository;
    
    public GetMyNotificationsQueryHandler(INotificationRepository notificationRepository)
    {
        _notificationRepository = notificationRepository;
    }

    public async Task<List<NotificationDto>> Handle(GetMyNotificationsQuery request,
        CancellationToken cancellationToken)
    {
        var notifications = await _notificationRepository.GetByUserIdAsync(request.userId);
        
        var response = notifications.Select(x => new NotificationDto

        {

            Id = x.Id,

            Title = x.Title,

            Message = x.Message,

            Type = x.Type,

            IsRead = x.IsRead,

            CreatedAt = x.CreatedAt

        }).ToList();

        return response;
    }
}