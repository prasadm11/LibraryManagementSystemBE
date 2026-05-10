using LibraryManagementSystem.Application.Features.Notification.Commands;
using LibraryManagementSystem.Core.Interfaces.Repositories;
using MediatR;

namespace LibraryManagementSystem.Application.Features.Notification.Handlers;

public class MarkNotificationAsReadCommandHandler : IRequestHandler<MarkNotificationAsReadCommand, string>
{
    private readonly INotificationRepository _notificationRepository;

    public MarkNotificationAsReadCommandHandler(

        INotificationRepository notificationRepository)

    {

        _notificationRepository = notificationRepository;

    }

    public async Task<string> Handle(

        MarkNotificationAsReadCommand request,

        CancellationToken cancellationToken)

    {

        await _notificationRepository

            .MarkAsReadAsync(request.NotificationId);

        return "Notification marked as read";

    }
}