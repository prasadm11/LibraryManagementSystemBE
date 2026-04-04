namespace LibraryManagementSystem.Application.Features.Users.DTOS;

public class UpdateUserDto
{
    public int Id { get; set; }

    // Personal Info
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;

    // Optional updates
    public string? Email { get; set; }
    public string? Username { get; set; }
}