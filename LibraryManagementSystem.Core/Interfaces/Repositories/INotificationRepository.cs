using LibraryManagementSystem.Core.Entities;

namespace LibraryManagementSystem.Core.Interfaces.Repositories;

public interface INotificationRepository
{
    Task AddAsync(Notification notification);

    Task<List<Notification>> GetByUserIdAsync(int userId);

    Task MarkAsReadAsync(int notificationId);
}