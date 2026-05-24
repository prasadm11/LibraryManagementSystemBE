using LibraryManagementSystem.Application.Common.Models;
using LibraryManagementSystem.Application.Features.BookReservation.Commands;
using LibraryManagementSystem.Application.Features.BookReservation.DTOs;
using LibraryManagementSystem.Core.Interfaces.Repositories;
using MediatR;

namespace LibraryManagementSystem.Application.Features.BookReservation.Handlers;

public class NotifyNextReservationUserCommandHandler : IRequestHandler<NotifyNextReservationUserCommand, ApiResponseModel<NotifyNextReservationUserResponseDto>>
{
    private readonly IBookReservationRepository _bookReservationRepository;
    private readonly INotificationRepository _notificationRepository;

    public NotifyNextReservationUserCommandHandler(IBookReservationRepository bookReservationRepository, INotificationRepository notificationRepository)
    {
        _bookReservationRepository = bookReservationRepository;
        _notificationRepository = notificationRepository;
    }

    public async Task<ApiResponseModel<NotifyNextReservationUserResponseDto>> Handle(
        NotifyNextReservationUserCommand command, CancellationToken cancellationToken)
    {
        var request = command.notifyNextReservationUserRequestDto;

        var reservation = await _bookReservationRepository.GetNextReservationAsync(request.BookId);

        if (reservation == null)
        {
            throw new KeyNotFoundException("No Reservation Found");
        }
        
        reservation.IsUserNotified = true;
        reservation.ExpiryDate = DateTime.UtcNow.AddDays(1);

        await _bookReservationRepository.UpdateAsync(reservation);

        await _notificationRepository.AddAsync(new Core.Entities.Notification
        {
            UserId = reservation.UserId,
            Title = "Book Available",
            Message = $"{request.BookTitle} is now avialable for borrowing",
            Type = "ReservationAvailable",
            IsRead =  false,
            CreatedAt =  DateTime.UtcNow
        });
        
        var result = new NotifyNextReservationUserResponseDto
        {
            Message = "Next reservation user notified successfully"
        };
        
        var response = ApiResponseModel<NotifyNextReservationUserResponseDto>
            .SuccessResponse(
                result,
                "Next reservation user notified successfully",
                200
            );

        return response;
    }
}