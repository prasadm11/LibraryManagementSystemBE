namespace LibraryManagementSystem.Application.Features.Borrow.DTOs;

public class GetBookBorrowStatusResponseDto
{
    public int BorrowId { get; set; }

    //Book Infor
    public int BookId { get; set; }
    public string BookTitle { get; set; } = string.Empty;
    
    //User Info
    public int UserId { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    
    // Borrow Details
    public DateTime BorrowedAt { get; set; }
    public DateTime DueDate { get; set; }
    public DateTime? ReturnedAt { get; set; }

    public string Status { get; set; } = string.Empty;

    // Fine
    public decimal FineAmount { get; set; }
    public bool FinePaid { get; set; }
}