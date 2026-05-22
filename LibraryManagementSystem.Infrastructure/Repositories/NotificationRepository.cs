using LibraryManagementSystem.Core.Entities;
using LibraryManagementSystem.Core.Interfaces.Repositories;
using LibraryManagementSystem.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementSystem.Infrastructure.Repositories;

public class NotificationRepository : INotificationRepository
{
    private readonly ApplicationDbContext _dbContext;
    private readonly IUserRepository _userRepository;

    public NotificationRepository(ApplicationDbContext dbContext,IUserRepository userRepository)
    {
        _dbContext = dbContext;
        _userRepository = userRepository;
    }

    public async Task AddAsync(Notification notification)
    {
        await _dbContext.Notifications.AddAsync(notification);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<List<Notification>> GetUnreadByUserIdAsync(int userId,int pageNumber, int pageSize)
    {
        var user = await _userRepository.GetUserByIdAsync(userId);
        if (user == null)
        {
            throw new KeyNotFoundException($"User with given id {userId} does not exist");
        }
        var response = await _dbContext.Notifications
            .AsNoTracking()
            .Where(x => x.UserId == userId && !x.IsRead)
            .OrderByDescending(x => x.CreatedAt)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
        return response;
    }
    
    public async Task<List<Notification>> GetUnreadByUserIdAsync(int userId)
    {
        var user = await _userRepository.GetUserByIdAsync(userId);
        if (user == null)
        {
            throw new KeyNotFoundException($"User with given id {userId} does not exist");
        }
        var response = await _dbContext.Notifications
            .AsNoTracking()
            .Where(x => x.UserId == userId && !x.IsRead)
            .OrderByDescending(x => x.CreatedAt)
            .ToListAsync();
        return response;
    }

    public async Task MarkAllAsReadAsync(int userId)
    {
        var user = await _userRepository.GetUserByIdAsync(userId);
        if (user == null)
        {
            throw new KeyNotFoundException($"User with given id {userId} does not exist");
        }
        var notifications = await _dbContext.Notifications.Where(x => x.UserId == userId && !x.IsRead).ToListAsync();
        foreach (var notification in notifications)
        {
            notification.IsRead = true;
        }
        
        await _dbContext.SaveChangesAsync();
    }
}