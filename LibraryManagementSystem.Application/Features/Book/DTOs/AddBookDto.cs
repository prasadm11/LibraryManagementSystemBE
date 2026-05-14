namespace LibraryManagementSystem.Application.Features.Book.DTOs;

public class AddBookDto
{
    public string Title { get; set; }
    public string Author { get; set; }
    public string ISBN { get; set; }
    public string Genre { get; set; }
    public int PublishedYear { get; set; }
    public int TotalCopies { get; set; }
    public int AvailableCopies { get; set; }
    public string? ImageUrl { get; set; }
}