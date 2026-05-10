using MediatR;

namespace LibraryManagementSystem.Application.Features.Notification.Commands;

public record MarkNotificationAsReadCommand(int NotificationId) : IRequest<string>;