namespace LibraryManagementSystem.Application.Features.Users.DTOS;

public class GetUserByIdDto
{
    public int Id { get; set; }

    //  Authentication
    public string Username { get; set; }
    public string Email { get; set; }

    //  Personal Info
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string City { get; set; }
    public string PhoneNumber { get; set; }
    public bool IsActive { get; set; }
}