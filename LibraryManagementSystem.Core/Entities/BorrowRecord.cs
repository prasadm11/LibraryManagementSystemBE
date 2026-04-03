using LibraryManagementSystem.Core.Enums;

namespace LibraryManagementSystem.Core.Entities;

public class BorrowRecord
{
    public int Id { get; set; }

    // Foreign Keys
    public int UserId { get; set; }
    public int BookId { get; set; }

    // Navigation Properties
    public User User { get; set; }
    public Book Book { get; set; }

    // Borrow Lifecycle
    public DateTime BorrowedAt { get; set; }
    public DateTime DueDate { get; set; }
    public DateTime? ReturnedAt { get; set; }

    // Status
    public BorrowStatus Status { get; set; }

    // Fine Tracking
    public decimal FineAmount { get; set; }
    public bool FinePaid { get; set; }

    // Metadata
    public string? Notes { get; set; }
}