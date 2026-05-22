using LibraryManagementSystem.Application.Common.Models;
using LibraryManagementSystem.Application.Features.Notification.DTOs;
using MediatR;

namespace LibraryManagementSystem.Application.Features.Notification.Commands;

public record GetMyNotificationsQuery(int userId,int pageNumber, int pageSize) : IRequest<ApiResponseModel<List<NotificationDto>>>;