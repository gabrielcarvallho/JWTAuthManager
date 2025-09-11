using JWTAuthManager.Application.Common.Interfaces.Messaging.Commands;
using JWTAuthManager.Application.Common.Interfaces.Services;
using JWTAuthManager.Application.Common.Models;
using JWTAuthManager.Application.Modules.AuthenticationManagement.Commands;
using JWTAuthManager.Application.Modules.AuthenticationManagement.DTOs;
using JWTAuthManager.Domain.Entities.Token;
using JWTAuthManager.Domain.Interfaces.Repositories;
using Microsoft.Extensions.Configuration;

namespace JWTAuthManager.Application.Modules.AuthenticationManagement.Handlers;

public class RefreshTokenHandler : ICommandHandler<RefreshTokenCommand, Result<AuthenticationDto>>
{
    private readonly IUnityOfWork _unitOfWork;
    private readonly IJwtTokenService _jwtTokenService;
    private readonly IConfiguration _configuration;

    public RefreshTokenHandler(IUnityOfWork unitOfWork, IJwtTokenService jwtTokenService, IConfiguration configuration)
    {
        _unitOfWork = unitOfWork;
        _jwtTokenService = jwtTokenService;
        _configuration = configuration;
    }

    public async Task<Result<AuthenticationDto>> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
    {
        var refreshToken = await _unitOfWork.RefreshToken
            .GetByTokenAsync(request.RefreshToken, cancellationToken);

        if (refreshToken == null)
        {
            return Result<AuthenticationDto>.Failure("Invalid refresh token");
        }

        refreshToken.IsRevoked = true;

        var newJwtToken = _jwtTokenService.GenerateAccessToken(refreshToken.User);
        var newRefreshToken = _jwtTokenService.GenerateRefreshToken();

        var refreshTokenEntity = new RefreshToken
        {
            Token = newRefreshToken,
            ExpiresAt = DateTime.UtcNow.AddDays(7),
            UserId = refreshToken.UserId
        };

        _unitOfWork.RefreshToken.Add(refreshTokenEntity);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<AuthenticationDto>.Success(new AuthenticationDto
        {
            AccessToken = newJwtToken,
            RefreshToken = newRefreshToken,
            ExpiresIn = int.Parse(_configuration["JWT:ExpirationInMinutes"]) * 60
        });
    }
}