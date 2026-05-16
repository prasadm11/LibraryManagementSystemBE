using LibraryManagementSystem.Application.Features.Borrow.Commands;
using LibraryManagementSystem.Core.Entities;
using LibraryManagementSystem.Core.Interfaces.Repositories;
using LibraryManagementSystem.Core.Interfaces.Services;
using MediatR;

namespace LibraryManagementSystem.Infrastructure.Services;

public class NotificationService : INotificationService
{
    private readonly IEmailService _emailService;
    private readonly IMediator _mediator;
    private readonly INotificationRepository _notificationRepository;

    public NotificationService(IEmailService emailService , IMediator mediator,INotificationRepository notificationRepository)
    {
        _emailService = emailService;
        _mediator = mediator;
        _notificationRepository = notificationRepository;
    }

    public async Task SendOverdueEmails()
    {
        var overdueBooksResponse = await _mediator.Send(new GetOverdueBooksQuery());
        var overdueBooks = overdueBooksResponse.Data;
        foreach (var item in overdueBooks)
        {
            var subject = "Overdue Books Reminder!";
            var body = $@"
                <h2>📚 Library Reminder</h2>
                <p>Hello {item.FirstName},</p>

                <p>Your book <b>{item.BookTitle}</b> is overdue.</p>

                <p><b>Due Date:</b> {item.DueDate:dd-MM-yyyy}</p>

                <p style='color:red;'>Please return it to avoid penalties.</p>

                <hr/>
                <p>Library Management System</p>
                ";
            await _emailService.SendEmailAsync(item.Email, subject, body);
            
            //call notification and use it in UI side for rendering notification by user
            await _notificationRepository.AddAsync(new Notification
            {
                UserId = item.UserId,
                Title = "Overdue Books Reminder!",
                Message = $"Your book '{item.BookTitle}' is overdue.",
                Type = "Overdue",
                IsRead = false,
                CreatedAt = DateTime.UtcNow
            });
        }
        

    }
    
    public async Task SendDueSoonEmails()
    {

        var dueSoonBooksResponse = await _mediator.Send(new GetDueSoonBooksQuery(2));
        var dueSoonBooks = dueSoonBooksResponse.Data;


        if (dueSoonBooks == null || !dueSoonBooks.Any())
            return;

        foreach (var item in dueSoonBooks)
        {
            var subject = "📚 Book Due Soon Reminder";

            var body = $@"
                <h3>Reminder</h3>
                <p>Hello {item.FirstName},</p>

                <p>Your book <b>{item.BookTitle}</b> is due on 
                <b>{item.DueDate:dd-MM-yyyy}</b>.</p>

                <p>Please return it on time to avoid penalties.</p>
                ";

            await _emailService.SendEmailAsync(item.Email, subject, body);
            
            await _notificationRepository.AddAsync(new Notification

            {

                UserId = item.UserId,

                Title = "Book Due Soon Reminder",

                Message = $"Your book '{item.BookTitle}' is due on {item.DueDate:dd-MM-yyyy}",

                Type = "DueSoon",

                IsRead = false,

                CreatedAt = DateTime.UtcNow

            });
        }
    }
}