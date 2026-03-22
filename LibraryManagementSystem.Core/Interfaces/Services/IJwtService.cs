using LibraryManagementSystem.Core.Entities;

namespace LibraryManagementSystem.Core.Interfaces.Services;

public interface IJwtService
{
    public string GenerateJwtToken(User user);

}