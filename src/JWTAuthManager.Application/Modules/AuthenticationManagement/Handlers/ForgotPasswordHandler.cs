using JWTAuthManager.Application.Common.Interfaces.Messaging.Commands;
using JWTAuthManager.Application.Common.Interfaces.Services;
using JWTAuthManager.Application.Common.Models;
using JWTAuthManager.Application.Modules.AuthenticationManagement.Commands;
using JWTAuthManager.Domain.Interfaces.Repositories;
using Microsoft.Extensions.Logging;

namespace JWTAuthManager.Application.Modules.AuthenticationManagement.Handlers;

public class ForgotPasswordHandler : ICommandHandler<ForgotPasswordCommand, Result<bool>>
{
    private readonly IUnityOfWork _unitOfWork;
    private readonly IJwtTokenService _jwtTokenService;
    private readonly IEmailService _emailService;
    private readonly ILogger<ForgotPasswordHandler> _logger;

    public ForgotPasswordHandler(
        IUnityOfWork unityOfWork, 
        IJwtTokenService jwtTokenService, 
        IEmailService emailService,
        ILogger<ForgotPasswordHandler> logger)
    {
        _unitOfWork = unityOfWork;
        _jwtTokenService = jwtTokenService;
        _emailService = emailService;
        _logger = logger;
    }

    public async Task<Result<bool>> Handle(ForgotPasswordCommand request, CancellationToken cancellationToken)
    {
        var user = await _unitOfWork.Users.FirstOrDefaultAsync(u => u.Email == request.Email, cancellationToken);
        if (user == null)
            return Result<bool>.Success(true);

        if (!user.IsActive)
            return Result<bool>.Success(true);

        try
        {
            var fullName = $"{user.FirstName} {user.LastName}".Trim();
            var token = _jwtTokenService.GeneratePasswordResetToken(user);

            await _emailService.SendPasswordResetEmailAsync(user.Email, fullName, token);
            _logger.LogInformation("Password reset email sent successfully to user {Email}", user.Email);
        }
        catch(Exception ex)
        {
            _logger.LogWarning(ex, "Failed to send password reset email to {Email}. Process completed successfully.", user.Email);
        }
        
        return Result<bool>.Success(true);
    }
}