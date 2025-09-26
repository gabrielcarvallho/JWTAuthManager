using JWTAuthManager.Application.Common.Interfaces.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Net.Mail;
using System.Net;

namespace JWTAuthManager.Infrastructure.Services;

public class EmailService : IEmailService
{
    private readonly IConfiguration _configuration;
    private readonly ILogger<EmailService> _logger;

    public EmailService(IConfiguration configuration, ILogger<EmailService> logger)
    {
        _configuration = configuration;
        _logger = logger;
    }

    public async Task SendEmailAsync(string toEmail, string subject, string body, bool isHtml = true)
    {
        try
        {
            if (!ValidateEmail(toEmail))
            {
                _logger.LogError("Invalid email format: {Email}", toEmail);
                throw new ArgumentException($"Invalid email format: {toEmail}", nameof(toEmail));
            }

            var emailSettings = _configuration.GetSection("EmailSettings");

            if (string.IsNullOrEmpty(emailSettings["SmtpHost"]))
            {
                _logger.LogInformation("Email would be sent to {Email} with subject: {Subject}", toEmail, subject);
                _logger.LogDebug("Email body: {Body}", body);
                return;
            }

            using var client = new SmtpClient(emailSettings["SmtpHost"], int.Parse(emailSettings["SmtpPort"]));

            client.EnableSsl = bool.Parse(emailSettings["EnableSsl"] ?? "true");
            
            if (!string.IsNullOrEmpty(emailSettings["Username"]))
            {
                client.Credentials = new NetworkCredential(emailSettings["Username"], emailSettings["Password"]);
            }

            var mailMessage = new MailMessage
            {
                From = new MailAddress(emailSettings["FromEmail"], emailSettings["FromName"]),
                Subject = subject,
                Body = body,
                IsBodyHtml = isHtml
            };

            mailMessage.To.Add(toEmail);

            await client.SendMailAsync(mailMessage);
            _logger.LogInformation("Email sent successfully to {Email}", toEmail);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to send email to {Email}", toEmail );
            throw new InvalidOperationException("Failed to send email.", ex);
        }
    }

    public async Task SendWelcomeEmailAsync(string toEmail, string userName, string resetToken)
    {
        var resetUrl = $"{_configuration["Frontend:BaseUrl"]}/reset-password?token={resetToken}";

        var subject = "Bem-vindo!";
        var body = $@"
            <h2>Bem-vindo {userName}!</h2>
            <p>Sua conta foi criada com sucesso.</p>
            <p>Você já pode começar a usar nossa plataforma.</p>
            <p>Clique no link abaixo para redefinir sua senha:</p>
            <p><a href='{resetUrl}' style='background-color: #007bff; color: white; padding: 10px 20px; text-decoration: none; border-radius: 5px;'>Redefinir Senha</a></p>
            <br>
        ";

        await SendEmailAsync(toEmail, subject, body);
    }

    public async Task SendPasswordResetEmailAsync(string toEmail, string userName, string resetToken)
    {
        var resetUrl = $"{_configuration["Frontend:BaseUrl"]}/reset-password?token={resetToken}";

        var subject = "Redefinição de Senha";
        var body = $@"
            <h2>Olá {userName},</h2>
            <p>Você solicitou a redefinição de sua senha.</p>
            <p>Clique no link abaixo para redefinir sua senha:</p>
            <p><a href='{resetUrl}' style='background-color: #007bff; color: white; padding: 10px 20px; text-decoration: none; border-radius: 5px;'>Redefinir Senha</a></p>
            <p>Este link expira em 1 hora.</p>
            <p>Se você não solicitou esta redefinição, ignore este email.</p>
            <br>
        ";

        await SendEmailAsync(toEmail, subject, body);
    }

    public bool ValidateEmail(string email)
    {
        try
        {
            var addr = new MailAddress(email);
            return addr.Address == email;
        }
        catch
        {
            return false;
        }
    }
}