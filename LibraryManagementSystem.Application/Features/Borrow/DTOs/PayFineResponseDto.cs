namespace LibraryManagementSystem.Application.Features.Borrow.DTOs;

public class PayFineResponseDto
{
    public int BorrowId { get; set; }

    public decimal FineAmount { get; set; }

    public bool FinePaid { get; set; }

    public string Message { get; set; } = string.Empty;
}