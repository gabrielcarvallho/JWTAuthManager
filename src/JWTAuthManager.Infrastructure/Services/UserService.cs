using JWTAuthManager.Application.Common.Interfaces.Services;
using JWTAuthManager.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace JWTAuthManager.Infrastructure.Services;

public class UserService : IUserService
{
    private readonly IPasswordHasher<User> _passwordHasher;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public UserService(IPasswordHasher<User> passwordHasher, IHttpContextAccessor httpContextAccessor)
    {
        _passwordHasher = passwordHasher;
        _httpContextAccessor = httpContextAccessor;
    }

    public string GetCurrentUserId()
    {
        try
        {
            return GetClaimValue(ClaimTypes.NameIdentifier)
                ?? GetClaimValue("sub")
                ?? GetClaimValue("user_id")
                ?? "system";
        }
        catch (Exception ex)
        {
            return "system";
        }
    }

    public string? GetClaimValue(string claimType)
    {
        try
        {
            var context = _httpContextAccessor.HttpContext;
            var claim = context?.User?.Claims?.FirstOrDefault(c => c.Type == claimType);

            if (claim == null)
            {
                return null;
            }

            return claim?.Value;
        }
        catch (Exception ex)
        {
            return null;
        }
    }
}