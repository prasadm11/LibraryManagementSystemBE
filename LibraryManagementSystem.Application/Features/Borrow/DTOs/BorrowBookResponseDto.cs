namespace LibraryManagementSystem.Application.Features.Borrow.DTOs;

public class BorrowBookResponseDto
{
    public int BorrowId { get; set; }

    public int UserId { get; set; }
    public int BookId { get; set; }

    public DateTime BorrowedAt { get; set; }
    public DateTime DueDate { get; set; }

    public string Status { get; set; } = string.Empty;

    public string? Message { get; set; }
}