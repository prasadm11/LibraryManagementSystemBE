namespace LibraryManagementSystem.Core.Entities;

public class User
{
    public int Id { get; set; }

    //  Authentication
    public string Username { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }

    //  Personal Info
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string City { get; set; }
    public string PhoneNumber { get; set; }

    //  Role Management
    public string Role { get; set; } // Admin / Member

    //  Metadata
    public DateTime CreatedAt { get; set; }
    public bool IsActive { get; set; }
    
    public string? ProfileImageUrl { get; set; }
}