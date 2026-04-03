namespace LibraryManagementSystem.Application.Features.Borrow.DTOs;

public class BorrowBookRequestDto
{
    public int UserId { get; set; }
    public int BookId { get; set; }
    public string? Notes { get; set; }
}