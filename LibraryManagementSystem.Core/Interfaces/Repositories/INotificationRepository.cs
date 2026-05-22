using LibraryManagementSystem.Core.Entities;

namespace LibraryManagementSystem.Core.Interfaces.Repositories;

public interface INotificationRepository
{
    Task AddAsync(Notification notification);

    Task<List<Notification>> GetUnreadByUserIdAsync(int userId, int pageNumber, int pageSize);
    Task<List<Notification>> GetUnreadByUserIdAsync(int userId);

    Task MarkAllAsReadAsync(int userId);
}