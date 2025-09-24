using JWTAuthManager.Application.Common.Interfaces.Messaging.Commands;
using JWTAuthManager.Application.Common.Interfaces.Services;
using JWTAuthManager.Application.Common.Models;
using JWTAuthManager.Application.Modules.AuthenticationManagement.Commands;
using JWTAuthManager.Domain.Interfaces.Repositories;

namespace JWTAuthManager.Application.Modules.AuthenticationManagement.Handlers;

public class ForgotPasswordHandler : ICommandHandler<ForgotPasswordCommand, Result<bool>>
{
    private readonly IUnityOfWork _unitOfWork;
    private readonly IJwtTokenService _jwtTokenService;
    private readonly IEmailService _emailService;

    public ForgotPasswordHandler(IUnityOfWork unityOfWork, IJwtTokenService jwtTokenService, IEmailService emailService)
    {
        _unitOfWork = unityOfWork;
        _jwtTokenService = jwtTokenService;
        _emailService = emailService;
    }

    public async Task<Result<bool>> Handle(ForgotPasswordCommand request, CancellationToken cancellationToken)
    {
        var user = await _unitOfWork.Users.FirstOrDefaultAsync(u => u.Email == request.Email, cancellationToken);
        if (user == null)
            return Result<bool>.Failure("User with the provided email does not exist.");

        if (!user.IsActive)
            return Result<bool>.Failure("User account is inactive.");

        var fullName = $"{user.FirstName} {user.LastName}".Trim();
        var token = _jwtTokenService.GeneratePasswordResetToken(user);

        await _emailService.SendPasswordResetEmailAsync(user.Email, fullName, token);
        return Result<bool>.Success(true);
    }
}