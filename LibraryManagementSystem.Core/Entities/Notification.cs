namespace LibraryManagementSystem.Core.Entities;

public class Notification
{
    public int Id { get; set; }

    // Who receives notification

    public int UserId { get; set; }

    // Notification content

    public string Title { get; set; }

    public string Message { get; set; }

    // Notification type

    public string Type { get; set; } // DueSoon / Overdue / Fine / RequestApproved

    // Status

    public bool IsRead { get; set; }

    // Metadata

    public DateTime CreatedAt { get; set; }
}