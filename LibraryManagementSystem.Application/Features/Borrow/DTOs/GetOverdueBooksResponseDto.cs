namespace LibraryManagementSystem.Application.Features.Borrow.DTOs;

public class GetOverdueBooksResponseDto
{
    public int BorrowId { get; set; }

    // Book Info
    public int BookId { get; set; }
    public string BookTitle { get; set; } = string.Empty;

    // User Info
    public int UserId { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;

    // 📖 Borrow Info
    public DateTime BorrowedAt { get; set; }
    public DateTime DueDate { get; set; }

    //  Overdue Info
    public int DaysLate { get; set; }

    //  Fine
    public decimal FineAmount { get; set; }
}