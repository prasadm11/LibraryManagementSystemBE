using LibraryManagementSystem.Application.Features.Borrow.Commands;
using LibraryManagementSystem.Core.Interfaces.Services;
using MediatR;

namespace LibraryManagementSystem.Infrastructure.Services;

public class NotificationService : INotificationService
{
    private readonly IEmailService _emailService;
    private readonly IMediator _mediator;

    public NotificationService(IEmailService emailService , IMediator mediator)
    {
        _emailService = emailService;
        _mediator = mediator;
    }

    public async Task SendOverdueEmails()
    {
        var overdueBooks = await _mediator.Send(new GetOverdueBooksQuery());
        foreach (var item in overdueBooks)
        {
            var subject = "Overdue Books Reminder!";
            var body = $@"Hello {item.FirstName} {item.LastName},
                        The book '{item.BookTitle}' is overdue.
                        Due Date: {item.DueDate:dd-MM-yyyy}
                        Please return it as soon as possible.
                        Thank you,
                        Library Team";
            await _emailService.SendEmailAsync(item.Email, subject, body);
        }
        
    }
}