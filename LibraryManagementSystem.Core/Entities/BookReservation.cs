namespace LibraryManagementSystem.Core.Entities;

public class BookReservation
{
    public int Id { get; set; }

    // Who reserved
    public int UserId { get; set; }

    // Which book
    public int BookId { get; set; }

    // Queue order
    public DateTime ReservedAt { get; set; }

    // Reservation lifecycle
    public bool IsFulfilled { get; set; }

    public bool IsCancelled { get; set; }

    // Notification tracking
    public bool IsUserNotified { get; set; }

    // Expiry support (future-ready)
    public DateTime? ExpiryDate { get; set; }
    
    public bool IsDeleted { get; set; }

    public DateTime? DeletedAt { get; set; }
    
    public Book Book { get; set; }
}