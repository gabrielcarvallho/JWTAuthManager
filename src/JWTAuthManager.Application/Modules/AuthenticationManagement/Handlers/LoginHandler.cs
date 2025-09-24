using JWTAuthManager.Application.Common.Interfaces.Messaging.Commands;
using JWTAuthManager.Application.Common.Interfaces.Services;
using JWTAuthManager.Application.Common.Models;
using JWTAuthManager.Application.Modules.AuthenticationManagement.Commands;
using JWTAuthManager.Application.Modules.AuthenticationManagement.DTOs;
using JWTAuthManager.Domain.Entities;
using JWTAuthManager.Domain.Entities.Token;
using JWTAuthManager.Domain.Interfaces.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;

namespace JWTAuthManager.Application.Modules.AuthenticationManagement.Handlers;

public class LoginHandler : ICommandHandler<LoginCommand, Result<AuthenticationDto>>
{
    private readonly IUnityOfWork _unitOfWork;
    private readonly IJwtTokenService _jwtTokenService;
    private readonly IUserService _userService;
    private readonly IPasswordHasher<User> _passwordHasher;
    private readonly IConfiguration _configuration;

    public LoginHandler(
        IUnityOfWork unityOfWork, 
        IJwtTokenService jwtTokenService, 
        IUserService userService,
        IPasswordHasher<User> passwordHasher,
        IConfiguration configuration)
    {
        _unitOfWork = unityOfWork;
        _jwtTokenService = jwtTokenService;
        _userService = userService;
        _passwordHasher = passwordHasher;
        _configuration = configuration;
    }

    public async Task<Result<AuthenticationDto>> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var user = await _unitOfWork.Users
            .FirstOrDefaultAsync(u => u.Email == request.Email && u.IsActive, cancellationToken);

        if (user == null)
        {
            return Result<AuthenticationDto>.Failure("Invalid email or password.");
        }

        var passwordVerificationResult = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash!, request.Password);
        if (passwordVerificationResult == PasswordVerificationResult.Failed)
        {
            return Result<AuthenticationDto>.Failure("Invalid email or password.");
        }

        var accessToken = _jwtTokenService.GenerateAccessToken(user);
        var refreshToken = _jwtTokenService.GenerateRefreshToken();

        var refreshTokenEntity = new RefreshToken
        {
            Token = refreshToken,
            ExpiresAt = DateTime.UtcNow.AddDays(30),
            UserId = user.Id
        };

        _unitOfWork.RefreshToken.Add(refreshTokenEntity);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<AuthenticationDto>.Success(new AuthenticationDto
        {
            AccessToken = accessToken,
            RefreshToken = refreshToken,
            ExpiresIn = int.Parse(_configuration["Jwt:ExpirationInMinutes"]) * 60,
            UserId = user.Id,
            UserEmail = user.Email
        });
    }
}