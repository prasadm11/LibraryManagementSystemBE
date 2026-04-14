namespace LibraryManagementSystem.Application.Features.Borrow.UserBorrowRequests.DTOs;

public class GetAllPendingBorrowRequestsResponseDto
{
    public int Id { get; set; }

    public int UserId { get; set; }
    public int BookId { get; set; }

    public int? BorrowRecordId { get; set; }

    public string Type { get; set; } = string.Empty;
    
    public string Status { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; }

    public string? Notes { get; set; }
}