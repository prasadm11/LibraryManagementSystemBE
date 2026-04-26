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
        }
        
    }
    
    public async Task SendDueSoonEmails()
    {

        var dueSoonBooks = await _mediator.Send(new GetDueSoonBooksQuery(2));

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
        }
    }
}