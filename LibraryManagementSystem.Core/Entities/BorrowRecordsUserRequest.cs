using LibraryManagementSystem.Core.Enums;

namespace LibraryManagementSystem.Core.Entities;

public class BorrowRecordsUserRequest
{
    public int Id { get; set; }

    // Who requested
    public int UserId { get; set; }

    // Which book (used for Borrow)
    public int BookId { get; set; }

    // Used for Return / Renew
    public int? BorrowRecordId { get; set; }

    // What type of request
    public BorrowRequestType Type { get; set; }

    // Current status of request
    public BorrowRequestStatus Status { get; set; }

    // When user created request
    public DateTime CreatedAt { get; set; }

    // Admin actions
    public int? ApprovedBy { get; set; }
    public DateTime? ApprovedAt { get; set; }
    
    public string? Notes { get; set; }
}