using LibraryManagementSystem.Core.Entities;
using LibraryManagementSystem.Core.Interfaces.Repositories;
using LibraryManagementSystem.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementSystem.Infrastructure.Repositories;

public class NotificationRepository : INotificationRepository
{
    private readonly ApplicationDbContext _dbContext;

    public NotificationRepository(ApplicationDbContext dbContext)

    {

        _dbContext = dbContext;

    }

    public async Task AddAsync(Notification notification)

    {

        await _dbContext.Notifications.AddAsync(notification);

        await _dbContext.SaveChangesAsync();

    }

    public async Task<List<Notification>> GetByUserIdAsync(int userId)

    {

        var response = await _dbContext.Notifications

            .Where(x => x.UserId == userId)

            .OrderByDescending(x => x.CreatedAt)

            .ToListAsync();
        
        return response;

    }

    public async Task MarkAsReadAsync(int userId)

    {

        var notifications = await _dbContext.Notifications.Where(x => x.UserId == userId && !x.IsRead).ToListAsync();

        foreach (var notification in notifications)
        {
            notification.IsRead = true;
        }
        
        await _dbContext.SaveChangesAsync();

    }
}