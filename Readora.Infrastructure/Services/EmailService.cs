using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using Readora.Application.Interfaces.Services;
using Readora.Infrastructure.Authentication;
using System.Threading.Tasks;

namespace Readora.Infrastructure.Services;

public class EmailService : IEmailService
{
    private readonly EmailSettings _settings;

    public EmailService(IOptions<EmailSettings> options)
    {
        _settings = options.Value;
    }

    public async Task SendEmailAsync(string to, string subject, string body)
    {
        var emailMessage = new MimeMessage();
        emailMessage.From.Add(new MailboxAddress(_settings.SenderName, _settings.SenderEmail));
        emailMessage.To.Add(MailboxAddress.Parse(to));
        emailMessage.Subject = subject;

        var builder = new BodyBuilder
        {
            HtmlBody = body
        };
        emailMessage.Body = builder.ToMessageBody();

        using var client = new SmtpClient();
        await client.ConnectAsync(_settings.SmtpHost, _settings.SmtpPort, SecureSocketOptions.StartTls);
        
        var Email = _settings.SenderEmail;
        var password = _settings.Password;
        
        if (!string.IsNullOrEmpty(Email) && !string.IsNullOrEmpty(password))
        {
            await client.AuthenticateAsync(Email, password);
        }

        await client.SendAsync(emailMessage);
        await client.DisconnectAsync(true);
    }
}
