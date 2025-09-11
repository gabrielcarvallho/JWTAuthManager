using JWTAuthManager.Domain.Entities;

namespace JWTAuthManager.Application.Common.Interfaces.Services;

public interface IUserService
{
    string GetCurrentUserId();
    string? GetClaimValue(string claimType);
}