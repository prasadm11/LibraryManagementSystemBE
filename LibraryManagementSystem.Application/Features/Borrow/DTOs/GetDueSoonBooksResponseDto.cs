namespace LibraryManagementSystem.Application.Features.Borrow.DTOs;

public class GetDueSoonBooksResponseDto
{
    public int BorrowId { get; set; }

    // Book Info
    public string BookTitle { get; set; } = string.Empty;

    // User Info
    public int UserId { get; set; }
    public string UserName { get; set; } = string.Empty;
    
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;

    // Due Info
    public DateTime DueDate { get; set; }

    public int DaysRemaining { get; set; }
}