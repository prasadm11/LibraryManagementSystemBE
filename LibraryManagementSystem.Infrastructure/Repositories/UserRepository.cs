using LibraryManagementSystem.Core.Entities;
using LibraryManagementSystem.Core.Interfaces.Repositories;
using LibraryManagementSystem.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementSystem.Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly ApplicationDbContext _dbContext;
    
    public UserRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<User>> GetAllUsersAsync()
    {
        var response = await _dbContext.Users.ToListAsync();
        return response;

    }

    public async Task AddUserAsync(User user)
    {
        await _dbContext.Users.AddAsync(user);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<User?> GetUserByIdAsync(int id)
    {
        var response = await _dbContext.Users.FindAsync(id);
        return response;
    }

    public async Task<User?> GetUserByEmailAsync(string email)
    {
        var response = await _dbContext.Users.FirstOrDefaultAsync(u => u.Email == email);
        return response;
    }
    
    public async Task UpdateUserAsync(User user)
    {
        _dbContext.Users.Update(user);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<List<User>> GetAllAdminsAsync()
    {
        var response = await _dbContext.Users.Where(x => x.Role == "Admin" && x.IsActive).ToListAsync();
        return response;
    }
}