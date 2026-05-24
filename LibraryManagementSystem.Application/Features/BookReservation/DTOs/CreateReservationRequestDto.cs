namespace LibraryManagementSystem.Application.Features.BookReservation.DTOs;

public class CreateReservationRequestDto
{
    public int UserId { get; set; }

    public int BookId { get; set; }
}