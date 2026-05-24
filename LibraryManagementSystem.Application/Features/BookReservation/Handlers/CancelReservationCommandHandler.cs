using LibraryManagementSystem.Application.Common.Models;
using LibraryManagementSystem.Application.Features.BookReservation.Commands;
using LibraryManagementSystem.Application.Features.BookReservation.DTOs;
using LibraryManagementSystem.Core.Interfaces.Repositories;
using MediatR;

namespace LibraryManagementSystem.Application.Features.BookReservation.Handlers;

public class CancelReservationCommandHandler : IRequestHandler<CancelReservationCommand, ApiResponseModel<CancelReservationResponseDto>>
{
    private readonly IBookReservationRepository _bookReservationRepository;
    public CancelReservationCommandHandler(IBookReservationRepository bookReservationRepository)
    {
        _bookReservationRepository = bookReservationRepository;
    }

    public async Task<ApiResponseModel<CancelReservationResponseDto>> Handle(CancelReservationCommand request,
        CancellationToken cancellationToken)
    {
        var reservation =
            await _bookReservationRepository.GetByIdAsync(request.cancelReservationRequestDto.ReservationId);
        if (reservation== null)
        {
            throw new KeyNotFoundException("Reservation not found");
        }

        reservation.IsCancelled = true;
        reservation.IsDeleted = true;
        reservation.DeletedAt = DateTime.UtcNow;

        await _bookReservationRepository.DeleteAsync(reservation);

        var result = new CancelReservationResponseDto
        {
            Message = "Reservation cancelled sucessfully",
        };

        var response = ApiResponseModel<CancelReservationResponseDto>.SuccessResponse(
            result,
            "Reservation cancelled sucessfully",
            200
        );
        return response;
    }
}