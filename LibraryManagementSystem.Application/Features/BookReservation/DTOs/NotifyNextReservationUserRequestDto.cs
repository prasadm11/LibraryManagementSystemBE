namespace LibraryManagementSystem.Application.Features.BookReservation.DTOs;

public class NotifyNextReservationUserRequestDto
{
    public int BookId { get; set; }

    public string BookTitle { get; set; }
}