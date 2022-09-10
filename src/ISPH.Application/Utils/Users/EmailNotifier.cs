using System.Net.Mail;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using System.Net;
using ISPH.Domain.Configuration;
using ISPH.Domain.Models.Base;

namespace ISPH.Application.Utils.Users;

public class EmailNotifier
{
    private readonly EmailSettings _settings;
    public EmailNotifier(IOptions<EmailSettings> options)
    {
        _settings = options.Value;
    }
    /*public async Task SendConfirmationMessage<TRegisterDto, TId>(TRegisterDto user, string role, CancellationToken token = default) 
        where TRegisterDto : RegisterDto<TId> where TId : struct
    {
        var message = new MailMessage();
        message.From = new(_settings.Address)
        var message = new MimeMessage();
        message.From.Add(new MailboxAddress("ISPH support", _settings.Address));
        message.To.Add(new MailboxAddress("", user.Email));
        message.Subject = "Address confirmation";
        message.Body = new TextPart("html")
        {
            Text = $"<b><i>Dear {user.FirstName} {user.LastName} {user.Patronymic}! \n" +
                   "Please, confirm your registration at ISPH by proceeding to the link below: \n" +
                   $@"<a href='{_settings.ConfirmationLink}?role={role}&email={user.Email}'></a></i></b>"
        };
        using SmtpClient client = new();
        await client.ConnectAsync("smtp.mailtrap.io", 2525, true, token);
        await client.AuthenticateAsync(_settings.Address, _settings.Password, token);
        await client.SendAsync(message, token);
        await client.DisconnectAsync(true, token);
    }*/
    
    public async Task SendEmailUpdateMessageAsync<TUser, TId>(TUser user, CancellationToken token = default) where TUser : BaseUser<TId>
    {
        var message = new MailMessage();
        message.From = new MailAddress(_settings.Address);
        message.To.Add(new MailAddress(user.Email));
        message.Subject = "Email update";
        message.Body = $"<b><i>Dear {user.FirstName} {user.LastName} {user.Patronymic}! \n" +
                       "Please, confirm that you want to change your email: \n" +
                       $@"<a href='{_settings.EmailUpdateLink}'>Change email</a></i></b>";
        message.IsBodyHtml = true;
        using SmtpClient client = new("smtp.mailtrap.io", 2525)
        {
            Credentials = new NetworkCredential("fd60441529bf17", "aa24d7d0f08a44"),
            EnableSsl = true
        };
        await client.SendMailAsync(message, token);
    }
}