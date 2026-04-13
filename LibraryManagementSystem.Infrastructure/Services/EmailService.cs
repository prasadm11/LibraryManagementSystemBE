using System.Net;
using System.Net.Mail;
using LibraryManagementSystem.Core.Interfaces.Services;
using Microsoft.Extensions.Configuration;

namespace LibraryManagementSystem.Infrastructure.Services;

public class EmailService : IEmailService
{
    private readonly IConfiguration _configuration;

    public EmailService(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    
    public async Task SendEmailAsync(string to, string subject, string body)
    {
        var host = _configuration["SmtpSettings:Host"];
        var port = int.Parse(_configuration["SmtpSettings:Port"]);
        var email = _configuration["SmtpSettings:Email"];
        var password = _configuration["SmtpSettings:Password"];
        var enableSsl = bool.Parse(_configuration["SmtpSettings:EnableSsl"]);
        
        var smtpClient = new SmtpClient(host, port)
        {
            Credentials = new NetworkCredential(email, password),
            EnableSsl = enableSsl
        };
        
        var mail = new MailMessage
        {
            From = new MailAddress(email),
            Subject = subject,
            Body = body,
            IsBodyHtml = true  
        };
        
        mail.To.Add(to);
        
        await smtpClient.SendMailAsync(mail);
    }
    
}