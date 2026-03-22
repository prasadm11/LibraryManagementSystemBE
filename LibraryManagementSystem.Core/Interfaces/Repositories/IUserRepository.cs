using LibraryManagementSystem.Core.Entities;

namespace LibraryManagementSystem.Core.Interfaces.Repositories;

public interface IUserRepository
{
    Task<List<User>> GetAllUsersAsync();
    Task AddUserAsync(User user);
    
    Task<User?> GetUserByIdAsync(int id);
    
    Task<User?> GetUserByEmailAsync(string email);
}