using JWTAuthManager.Application.Common.Interfaces.Messaging.Commands;
using JWTAuthManager.Application.Common.Interfaces.Services;
using JWTAuthManager.Application.Common.Models;
using JWTAuthManager.Application.Modules.AuthenticationManagement.Commands;
using JWTAuthManager.Domain.Entities;
using JWTAuthManager.Domain.Interfaces.Repositories;
using Microsoft.AspNetCore.Identity;

namespace JWTAuthManager.Application.Modules.AuthenticationManagement.Handlers;

public class ResetPasswordHandler : ICommandHandler<ResetPasswordCommand, Result<bool>>
{
    private readonly IUnityOfWork _unitOfWork;
    private readonly IJwtTokenService _jwtTokenService;
    private readonly IPasswordHasher<User> _passwordHasher;

    public ResetPasswordHandler(IUnityOfWork unityOfWork, IJwtTokenService jwtTokenService, IPasswordHasher<User> passwordHasher)
    {
        _unitOfWork = unityOfWork;
        _jwtTokenService = jwtTokenService;
        _passwordHasher = passwordHasher;
    }

    public async Task<Result<bool>> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
    {
        var user = await _unitOfWork.Users.
            FirstOrDefaultAsync(u => u.Email == request.Email && u.IsActive, cancellationToken);

        if (user == null)
            return Result<bool>.Failure("User not found");

        if (!user.IsActive)
            return Result<bool>.Failure("User account is inactive");

        if (!_jwtTokenService.ValidatePasswordResetToken(request.Token, user))
            return Result<bool>.Failure("Invalid or expired token");

        user.PasswordHash = _passwordHasher.HashPassword(user, request.NewPassword);
        user.UpdatedAt = DateTime.UtcNow;

        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return Result<bool>.Success(true);
    }
}