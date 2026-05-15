namespace LibraryManagementSystem.Core.Entities;

public class BookRating
{
    public int Id { get; set; }

    // Relations

    public int UserId { get; set; }

    public int BookId { get; set; }

    // Rating

    public double Rating { get; set; } // 1 to 5

    public string? Review { get; set; }

    // Metadata

    public DateTime CreatedAt { get; set; }
}