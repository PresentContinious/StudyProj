using System.Net;
using System.Net.Mail;
using System.Text;
using Microsoft.Extensions.Options;
using StudentHub.Core.DTOs;
using StudentHub.Core.Interfaces;

namespace StudentHub.Core.Services;

public class EmailService : IEmailService
{
    private readonly SmtpConfigModel _smtpConfig;

    public EmailService(IOptions<SmtpConfigModel> smtpConfig)
    {
        _smtpConfig = smtpConfig.Value;
    }

    public async Task SendEmailAsync(string email, string subject, string message)
    {
        var mail = new MailMessage
        {
            From = new MailAddress(_smtpConfig.SenderAddress, _smtpConfig.SenderDisplayName),
            Subject = subject,
            Body = message,
            IsBodyHtml = _smtpConfig.IsBodyHtml,
            BodyEncoding = Encoding.Default
        };

        mail.To.Add(new MailAddress(email));

        SmtpClient smtpClient = new SmtpClient
        {
            Host = _smtpConfig.Host,
            Port = _smtpConfig.Port,
            EnableSsl = _smtpConfig.EnableSsl,
            UseDefaultCredentials = _smtpConfig.UseDefaultCredentials
        };

        smtpClient.Credentials = new NetworkCredential(_smtpConfig.Username, _smtpConfig.Password);

        await smtpClient.SendMailAsync(mail);
    }
}
