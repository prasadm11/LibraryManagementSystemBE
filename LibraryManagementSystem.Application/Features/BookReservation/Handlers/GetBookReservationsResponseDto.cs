namespace LibraryManagementSystem.Application.Features.BookReservation.Handlers;

public class GetBookReservationsResponseDto
{
    public int ReservationId { get; set; }

    public int UserId { get; set; }

    public string UserName { get; set; }

    public string? ProfileImageUrl { get; set; }

    public DateTime ReservedAt { get; set; }

    public bool IsFulfilled { get; set; }

    public bool IsCancelled { get; set; }
}