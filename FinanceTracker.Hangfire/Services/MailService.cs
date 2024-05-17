using FinanceTracker.Hangfire.Dtos;
using FinanceTracker.Hangfire.Interface;
using FinanceTracker.Hangfire.Model;
using Hangfire;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;

namespace FinanceTracker.Hangfire.Services;

public class MailService : IMailService
{
    private readonly MailSettings _mailSettings;

    public MailService(IOptions<MailSettings> mailSettings)
    {
        _mailSettings = mailSettings.Value;
    }

    public async Task SendEmailAdmin(string userName)
    {
        AdminEmail adminEmail = new AdminEmail(_mailSettings.LoginUrl);
        var email = new MimeMessage();
        var UrlDto = new UrlDto()
        {
            LoginUrl = _mailSettings.LoginUrl
        };
        email.Sender = MailboxAddress.Parse(_mailSettings.Mail);
        email.To.Add(MailboxAddress.Parse(adminEmail.ToEmail));
        email.Subject = adminEmail.Subject + userName;
        var builder = new BodyBuilder();
        builder.HtmlBody = adminEmail.Body;
        email.Body = builder.ToMessageBody();

        using var smtp = new SmtpClient();
        await smtp.ConnectAsync(_mailSettings.Host, _mailSettings.Port, SecureSocketOptions.StartTls);
        await smtp.AuthenticateAsync(_mailSettings.Mail, _mailSettings.Password);
        await smtp.SendAsync(email);
        await smtp.DisconnectAsync(true);
        BackgroundJob.Enqueue(() => Console.WriteLine("Request Mail sent successfully"));
    }
    public async Task SendApprovalMail(int id, string userEmail, string RejectionReason, byte statusId)
    {
        var builder = new BodyBuilder();
        var email = new MimeMessage();
        var UrlDto = new UrlDto()
        {
            LoginUrl = _mailSettings.LoginUrl
        };
        ApprovalEmail approveEmail = new ApprovalEmail(_mailSettings.LoginUrl);
        email.Sender = MailboxAddress.Parse(_mailSettings.Mail);
        email.To.Add(MailboxAddress.Parse(userEmail));
        email.Subject = approveEmail.Subject;
        builder.HtmlBody = approveEmail.Body;
        email.Body = builder.ToMessageBody();
        using var smtp = new SmtpClient();
        await smtp.ConnectAsync(_mailSettings.Host, _mailSettings.Port, SecureSocketOptions.StartTls);
        await smtp.AuthenticateAsync(_mailSettings.Mail, _mailSettings.Password);
        await smtp.SendAsync(email);
        await smtp.DisconnectAsync(true);
        BackgroundJob.Enqueue(() => Console.WriteLine("Approval Mail sent successfully"));
    }
    public async Task SendRejectMail(int id, string userEmail, string RejectionReason, byte statusId, RejectionEmail rejectEmail)
    {
        var builder = new BodyBuilder();
        var email = new MimeMessage();
        var UrlDto = new UrlDto()
        {
            LoginUrl = _mailSettings.LoginUrl
        };
        email.Sender = MailboxAddress.Parse(_mailSettings.Mail);
        email.To.Add(MailboxAddress.Parse(userEmail));
        email.Subject = rejectEmail.Subject;
        builder.HtmlBody = string.Format(rejectEmail.Body);
        email.Body = builder.ToMessageBody();

        using var smtp = new SmtpClient();
        await smtp.ConnectAsync(_mailSettings.Host, _mailSettings.Port, SecureSocketOptions.StartTls);
        await smtp.AuthenticateAsync(_mailSettings.Mail, _mailSettings.Password);
        await smtp.SendAsync(email);
        await smtp.DisconnectAsync(true);
        BackgroundJob.Enqueue(() => Console.WriteLine("Rejection Mail sent successfully"));
    }
}