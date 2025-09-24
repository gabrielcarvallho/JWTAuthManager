using JWTAuthManager.Domain.Entities;
using JWTAuthManager.Domain.Entities.Token;
using System.Security.Claims;

namespace JWTAuthManager.Application.Common.Interfaces.Services;

public interface IJwtTokenService
{
    string GenerateAccessToken(User user);
    string GenerateRefreshToken();
    string GeneratePasswordResetToken(User user);
    ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
    bool ValidateToken(string token);
    bool ValidatePasswordResetToken(string token, User user);
    BlacklistedToken BlacklistTokenAsync(string token);
    Task<bool> IsTokenBlacklistedAsync(string token, CancellationToken cancellationToken = default);
}