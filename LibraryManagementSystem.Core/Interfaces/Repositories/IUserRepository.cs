using LibraryManagementSystem.Core.Entities;

namespace LibraryManagementSystem.Core.Interfaces.Repositories;

public interface IUserRepository
{
   Task<List<User>> GetAllUsersAsync(int pageNumber, int pageSize);
    Task AddUserAsync(User user);
    
    Task<User?> GetUserByIdAsync(int id);
    
    Task<User?> GetUserByEmailAsync(string email);

    Task UpdateUserAsync(User user);
    
    Task<List<User>> GetAllAdminsAsync();
}