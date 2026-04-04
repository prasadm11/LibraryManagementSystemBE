namespace LibraryManagementSystem.Application.Features.Borrow.DTOs;

public class ReturnBookResponseDto
{
    public int BorrowId { get; set; }

    public int UserId { get; set; }
    public int BookId { get; set; }

    public DateTime BorrowedAt { get; set; }
    public DateTime DueDate { get; set; }
    public DateTime ReturnedAt { get; set; }

    public string Status { get; set; } = string.Empty;

    public decimal FineAmount { get; set; }
    public bool FinePaid { get; set; }

    public string Message { get; set; } = string.Empty;
}