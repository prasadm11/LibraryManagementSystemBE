using LibraryManagementSystem.Application.Common.Models;
using MediatR;

namespace LibraryManagementSystem.Application.Features.Notification.Commands;

public record MarkNotificationAsReadCommand(int UserId) : IRequest<ApiResponseModel<string>>;