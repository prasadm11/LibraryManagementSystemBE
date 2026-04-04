namespace LibraryManagementSystem.Application.Features.Borrow.DTOs;

public class BorrowSummaryResponseDto
{
    public int TotalBorrowed { get; set; }
    public int Active { get; set; }
    public int Returned { get; set; }
    public int Overdue { get; set; }
    public int ReturnedLate { get; set; }
    public decimal TotalFineCollected { get; set; }
}