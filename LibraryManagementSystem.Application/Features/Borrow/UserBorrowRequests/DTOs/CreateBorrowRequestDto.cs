namespace LibraryManagementSystem.Application.Features.Borrow.UserBorrowRequests.DTOs;

public class CreateBorrowRequestDto
{
    public int UserId { get; set; }
    public int BookId { get; set; }
    public string? Notes { get; set; }
}