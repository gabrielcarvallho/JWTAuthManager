namespace JWTAuthManager.Application.Common.Interfaces.Services;

public interface IEmailService
{
    Task SendEmailAsync(string toEmail, string subject, string body, bool isHtml = true);
    Task SendWelcomeEmailAsync(string toEmail, string userName, string resetToken);
    Task SendPasswordResetEmailAsync(string toEmail, string userName, string resetToken);
    bool ValidateEmail(string email);
}