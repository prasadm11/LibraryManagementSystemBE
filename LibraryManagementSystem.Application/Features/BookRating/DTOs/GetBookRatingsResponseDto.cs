namespace LibraryManagementSystem.Application.Features.BookRating.DTOs;

public class GetBookRatingsResponseDto
{
    public double Rating { get; set; }

    public string? Review { get; set; }
    
    public int UserId { get; set; }
    
    public string? ImageUrl { get; set; }

    public string Username { get; set; }

    public DateTime CreatedAt { get; set; }
}