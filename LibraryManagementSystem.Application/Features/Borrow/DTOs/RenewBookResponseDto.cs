namespace LibraryManagementSystem.Application.Features.Borrow.DTOs;

public class RenewBookResponseDto
{
    public int BorrowId { get; set; }
    public DateTime OldDueDate { get; set; }
    public DateTime NewDueDate { get; set; }

    public string Message { get; set; } = string.Empty;
}