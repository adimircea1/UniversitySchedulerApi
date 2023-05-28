namespace UniversityScheduler.Api.Core.Utils.Email;

public interface IConfirmationEmailSender
{
    public Task SendEmailAsync(string userEmail, int userId);
}