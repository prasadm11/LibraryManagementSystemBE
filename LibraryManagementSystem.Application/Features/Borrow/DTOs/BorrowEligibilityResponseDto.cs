namespace LibraryManagementSystem.Application.Features.Borrow.DTOs;

public class BorrowEligibilityResponseDto
{
    public int UserId { get; set; }

    public bool IsEligible { get; set; }

    public string Message { get; set; } = string.Empty;

    
    public int ActiveBorrowCount { get; set; }

    public bool HasOverdueBooks { get; set; }

    public bool HasUnpaidFines { get; set; }
}