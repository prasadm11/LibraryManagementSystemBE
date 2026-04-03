namespace LibraryManagementSystem.Core.Enums;

public enum BorrowStatus
{
    Active,      // Book is currently borrowed
    Returned,    // Returned on time
    Overdue,     // Past due date, not returned
    ReturnedLate // Returned but after due date
}