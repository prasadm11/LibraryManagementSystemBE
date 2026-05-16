namespace LibraryManagementSystem.Application.Features.Users.DTOS;

public class CreateUserResponseDto
{
    public int UserId { get; set; }

    // Authentication Info

    public string Username { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    // Personal Info

    public string FirstName { get; set; } = string.Empty;

    public string LastName { get; set; } = string.Empty;

    public string City { get; set; } = string.Empty;

    public string PhoneNumber { get; set; } = string.Empty;

    // Role

    public string Role { get; set; } = string.Empty;

    // Metadata

    public DateTime CreatedAt { get; set; }

    public bool IsActive { get; set; }

    public string? ProfileImageUrl { get; set; }
}