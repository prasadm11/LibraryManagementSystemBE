namespace LibraryManagementSystem.Application.Features.BookRating.DTOs;

public class RateBookDto
{
    public int UserId { get; set; }

    public int BookId { get; set; }

    public double Rating { get; set; }

    public string? Review { get; set; }
}