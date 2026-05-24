namespace LibraryManagementSystem.Application.Features.BookReservation.DTOs;

public class GetUserReservationsResponseDto
{
    public int ReservationId { get; set; }

    public int BookId { get; set; }

    public string BookTitle { get; set; }

    public string? BookImageUrl { get; set; }

    public DateTime ReservedAt { get; set; }

    public DateTime? ExpiryDate { get; set; }

    public bool IsFulfilled { get; set; }

    public bool IsCancelled { get; set; }
}