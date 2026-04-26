namespace LibraryManagementSystem.Core.Interfaces.Services;

public interface INotificationService
{
    Task SendOverdueEmails();
}