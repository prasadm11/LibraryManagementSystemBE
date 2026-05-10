using LibraryManagementSystem.Application.Features.Notification.DTOs;
using MediatR;

namespace LibraryManagementSystem.Application.Features.Notification.Commands;

public record GetMyNotificationsQuery(int userId) : IRequest<List<NotificationDto>>;