using System.Net;
using System.Net.Mail;
using UniversityScheduler.Api.Core.Enums;
using UniversityScheduler.Api.Core.Models.Attributes;
using UniversityScheduler.Api.Core.Utils.Email;

namespace UniversityScheduler.Api.Core.Models;

[Registration(Type = RegistrationKind.Singleton)]
public class ConfirmationEmailSender : IConfirmationEmailSender
{
    public async Task SendEmailAsync(string userEmail, int userId)
    {
        var mailMessage = new MailMessage
        {
            From = new MailAddress("uarasaka@gmail.com"),
            Subject = "Confirm your email!",
            IsBodyHtml = true,
            Body = $"<a href='http://localhost:5000/users/confirm-email?userId={userId}'>Click here to verify your email!</a>" 
                
        };
        
        mailMessage.To.Add(new MailAddress(userEmail));

        var client = new SmtpClient
        {
            Host =  "smtp.gmail.com",
            Port = 587,
            EnableSsl = true,
            Credentials = new NetworkCredential("uarasaka@gmail.com", "ilfwhwrwnwparpgy")
        };
        
        await client.SendMailAsync(mailMessage);
    }
}